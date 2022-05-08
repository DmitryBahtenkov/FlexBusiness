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
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Database.Contract.StoredProcedures.Services;
using FBA.Repository.Helpers;

namespace FBA.Database.Dashboards.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardQueryOperations _dashboardQueryOperations;
        private readonly IStoredProcedureQueryOperations _storedProcedureQueryOperations;
        private readonly IDashboardWriteOperations _dashboardWriteOperations;
        private readonly IStoredProcedureService _storedProcedureService;
        private readonly ISettingsQueryOperations _settingsQueryOperations;


        public DashboardService(
            IDashboardQueryOperations dashboardQueryOperations,
            IDashboardWriteOperations dashboardWriteOperations,
            IStoredProcedureQueryOperations storedProcedureQueryOperations,
            ISettingsQueryOperations settingsQueryOperations,
            IStoredProcedureService storedProcedureService)
        {
            _storedProcedureService = storedProcedureService;
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
                ChartType = request.ChartType,
                Settings = request.Settings,
                Parameters = request.Parameters
            };

            document = await _dashboardWriteOperations.Create(document);

            return Map(document);
        }

        public async Task<object> ExecuteDashboard(string id)
        {
            var dashboard = await ById(id);
            var result = (ExecuteResult)await _storedProcedureService.Execute(dashboard.StoredProcedureId, dashboard.Parameters);
            var values = result.Values.ToList();

            try
            {
                if (dashboard.ChartType is ChartType.Lineal)
                {
                    var linearResults = new List<LinearResult>();
                    var xIndex = result.Headers.IndexOf(dashboard.Settings.X);
                    var xData = values.Select(x => x[xIndex]);

                    foreach (var column in dashboard.Settings.Y)
                    {
                        var cIndex = result.Headers.IndexOf(column);
                        var yData = values.Select(x => x[cIndex]);
                        var data = Enumerable
                            .Zip(xData, yData, (x, y) => new { x, y })
                            .ToDictionary(x => x.x, x => x.y);

                        linearResults.Add(new LinearResult(column, data));
                    }

                    return linearResults;
                }
                else
                {
                    var aggregationResults = new List<AggregationResult>();

                    if (dashboard.Settings.Aggregation is Aggregation.CountUnique)
                    {
                        var index = result.Headers.IndexOf(dashboard.Settings.Columns[0]);
                        var val = values.Select(x => x[index]);
                        var grouped = val.GroupBy(x => x).Select(x => new AggregationResult(x.Key.ToString(), x.Count()));
                        aggregationResults.AddRange(grouped);

                        return aggregationResults;
                    }

                    if (dashboard.Settings.Aggregation is Aggregation.Avg)
                    {
                        foreach (var c in dashboard.Settings.Columns)
                        {
                            var index = result.Headers.IndexOf(c);
                            var val = values.Select(x => x[index]);

                            aggregationResults.Add(new AggregationResult(c, val.Average(x => (decimal)x)));
                        }
                    }

                    if (dashboard.Settings.Aggregation is Aggregation.Max)
                    {
                        foreach (var c in dashboard.Settings.Columns)
                        {
                            var index = result.Headers.IndexOf(c);
                            var val = values.Select(x => x[index]);

                            aggregationResults.Add(new AggregationResult(c, val.Max(x => (decimal)x)));
                        }
                    }

                    if (dashboard.Settings.Aggregation is Aggregation.Min)
                    {
                        foreach (var c in dashboard.Settings.Columns)
                        {
                            var index = result.Headers.IndexOf(c);
                            var val = values.Select(x => x[index]);

                            aggregationResults.Add(new AggregationResult(c, val.Min(x => (decimal)x)));
                        }
                    }

                    return aggregationResults;
                }
            }
            catch (InvalidCastException)
            {
                throw new BusinessException("Некорректный тип данных одного из полей");
            }
        }

        public async Task<DashboardResponse> Update(string id, DashboardRequest request)
        {
            var dashboard = await _dashboardQueryOperations.GetById(id);

            if (dashboard is null)
            {
                throw new NotFoundException();
            }

            dashboard.Settings = request.Settings;
            dashboard.ConnectionId = request.ConnectionId;
            dashboard.StoredProcedureId = request.StoredProcedureId;
            return Map(await _dashboardWriteOperations.Update(dashboard));
        }

        private DashboardResponse Map(DashboardDocument dashboardDocument)
        {
            return new()
            {
                Id = dashboardDocument.Id,
                Name = dashboardDocument.Name,
                ConnectionId = dashboardDocument.ConnectionId,
                StoredProcedureId = dashboardDocument.StoredProcedureId,
                Settings = dashboardDocument.Settings,
                ChartType = dashboardDocument.ChartType,
                Parameters = dashboardDocument.Parameters
            };
        }
    }
}