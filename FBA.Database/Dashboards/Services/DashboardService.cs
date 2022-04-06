using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.Dashboards.Models;
using FBA.Database.Contract.Dashboards.Models.Request;
using FBA.Database.Contract.Dashboards.Models.Response;
using FBA.Database.Contract.Dashboards.Operations;
using FBA.Database.Contract.Dashboards.Services;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Repository.Helpers;

namespace FBA.Database.Dashboards.Services
{
    public class DashboardService : IDashboardService
    {
		private readonly IDashboardQueryOperations _dashboardQueryOperations;
		private readonly IStoredProcedureQueryOperations _storedProcedureQueryOperations;
		private readonly IDashboardWriteOperations _dashboardWriteOperations;
		private readonly ISettingsQueryOperations _settingsQueryOperations;

		public DashboardService(
			IDashboardQueryOperations dashboardQueryOperations,
			IDashboardWriteOperations dashboardWriteOperations,
			IStoredProcedureQueryOperations storedProcedureQueryOperations,
			ISettingsQueryOperations settingsQueryOperations)
		{
			_settingsQueryOperations = settingsQueryOperations;
			_storedProcedureQueryOperations = storedProcedureQueryOperations;
			_dashboardWriteOperations = dashboardWriteOperations;
			_dashboardQueryOperations = dashboardQueryOperations;
		}

        public async Task<List<DashboardResponse>> ByConnection(string connectionId)
        {
            var documents = await _dashboardQueryOperations.ByConnection(connectionId);

            return documents.Select(Map).ToList();
        }

        public async Task<DashboardResponse> ById(string id)
        {
            var document = await _dashboardQueryOperations.GetById(id);

            if (document is null)
            {
                throw new NotFoundException();
            }

            return Map(document);
        }

        public async Task<List<DashboardResponse>> ByStoredProcedure(string storedProcedureId)
        {
            var documents = await _dashboardQueryOperations.ByStoredProcedure(storedProcedureId);

            return documents.Select(Map).ToList();
        }

        public async Task<DashboardResponse> Create(DashboardRequest request)
        {
            if (!await _settingsQueryOperations.ExistById(request.ConnectionId))
            {
                throw new BusinessException("Не найдена БД");
            }

            if (!await _storedProcedureQueryOperations.ExistById(request.StoredProcedureId))
            {
                throw new BusinessException("Не найдена хранимая процедура");
            }

            var document = new DashboardDocument
            {
                Id = IdGen.NewId(),
                Name = request.Name,
                ConnectionId = request.ConnectionId,
                StoredProcedureId = request.StoredProcedureId,
                Columns = request.Columns,
                ChartType = request.ChartType
            };

            document = await _dashboardWriteOperations.Create(document);

            return Map(document);
        }

        public async Task<DashboardResponse> Update(string id, DashboardRequest request)
        {
            var dashboard = await _dashboardQueryOperations.GetById(id);

            if(dashboard is null)
            {
                throw new NotFoundException();
            }
            
            dashboard.Columns = request.Columns;
            dashboard.ConnectionId = request.ConnectionId;
            dashboard.StoredProcedureId = request.StoredProcedureId;
            return Map(await _dashboardWriteOperations.Update(dashboard));
        }

        private DashboardResponse Map(DashboardDocument dashboardDocument)
        {
            return new ()
            {
                Id = dashboardDocument.Id,
                Name = dashboardDocument.Name,
                ConnectionId = dashboardDocument.ConnectionId,
                StoredProcedureId = dashboardDocument.StoredProcedureId,
                Columns = dashboardDocument.Columns,
                ChartType = dashboardDocument.ChartType
            };
        }
    }
}