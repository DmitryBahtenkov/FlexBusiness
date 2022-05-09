using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.Diagram.Services;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.Tables;
using Microsoft.Data.SqlClient;

namespace FBA.Database.Tables
{
    public class TableService : ITableService
    {
        private readonly ISettingsQueryOperations _settingsQueryOperations;
        private readonly IDiagramService _diagramService;

        public TableService(
            ISettingsQueryOperations settingsQueryOperations,
            IDiagramService diagramService)
        {
            _diagramService = diagramService;
            _settingsQueryOperations = settingsQueryOperations;
        }


        public async Task<ExecuteResult> ExecuteSelect(string connectionId, string table, SelectQuery selectQuery = null)
        {
            var connection = await _settingsQueryOperations.GetById(connectionId);
            await using var sqlConnection = new SqlConnection(connection.ConnectionString);
            await sqlConnection.OpenAsync();

            SqlCommand command = null;
            if (selectQuery is null)
            {
                command = new SqlCommand($"select * from {table} limit 100", sqlConnection);
            }
            else
            {
                var parameters = new List<SqlParameter>(selectQuery.Fields.Count + 2);
                var sb = new StringBuilder();

                sb.Append("select");

                if(selectQuery.Limit.HasValue)
                {
                    sb.Append($" top {selectQuery.Limit}");
                }

                sb.Append($" * from {table} where ");

                sb.Append(string.Join("AND", selectQuery.Fields.Select(q => $"{q.Field} {q.Operator} @{q.Field}Param")));
                foreach (var query in selectQuery.Fields)
                {
                    parameters.Add(new SqlParameter($"@{query.Field}Param", query.Value));
                }

                if (!string.IsNullOrEmpty(selectQuery.OrderField))
                {
                    sb.Append($" order by {selectQuery.OrderField} {selectQuery.OrderType.ToString().ToUpper()}");
                }

                command = new SqlCommand(sb.ToString(), sqlConnection);
                command.Parameters.AddRange(parameters.ToArray());
            }

            var adapter = new SqlDataAdapter(command);

            var ds = new DataSet();
            // Заполняем Dataset
            adapter.Fill(ds);

            var headers = new List<string>();

            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                headers.Add(column.ColumnName);
            }

            var rows = new List<DataRow>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                rows.Add(row);
            }

            return new ExecuteResult(headers, rows.Select(x => x.ItemArray));
        }

        public async Task<List<TableInfo>> GetTables(string connectionId)
        {
            var diagram = await _diagramService.GetByConnection(connectionId);

            return diagram.Tables
                .Select(x => new TableInfo(x.Title, x.Type))
                .ToList();
        }
    }
}