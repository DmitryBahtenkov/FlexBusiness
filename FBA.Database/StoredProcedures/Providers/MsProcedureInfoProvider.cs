using System.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Services;
using Microsoft.Data.SqlClient;

namespace FBA.Database.StoredProcedures.Providers
{
    public class MsProcedureInfoProvider : IProcedureInfoProvider
    {
        public async Task<ExecuteResult> ExecuteStoredProcedure(
            ConnectionsDocument document,
            string procedureName, 
            Dictionary<string, string> parameters)
        {
            await using var sqlConnection = new SqlConnection(document.ConnectionString);
            await sqlConnection.OpenAsync();

            var sb = new StringBuilder();
            sb.Append("exec @procName ");
            sb.Append(string.Join(", ", parameters.Select(k => $"{k.Key}")));

            var parameter = new SqlParameter("@procName", procedureName);
            var command = new SqlCommand(sb.ToString(), sqlConnection);
            command.Parameters.Add(parameter);

            foreach(var (k,v) in parameters)
            {
                command.Parameters.Add(new SqlParameter(k, v));
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

        public async Task<List<string>> GetNames(ConnectionsDocument document)
        {
            await using var sqlConnection = new SqlConnection(document.ConnectionString);
            await sqlConnection.OpenAsync();

            var procedures = await QueryProcedures(sqlConnection);

            return procedures.Select(x => x.SpecificName).ToList();
        }

        public async Task<ParameterInfoEmbeddedDocument[]> GetParameters(ConnectionsDocument document, string procedureName)
        {
            await using var sqlConnection = new SqlConnection(document.ConnectionString);
            await sqlConnection.OpenAsync();
            
            var parameters = await QueryParameters(procedureName, sqlConnection);

            return parameters
                .Select(x => x.ToParameterInfo())
                .ToArray();
        }

        private async Task<List<Parameter>> QueryParameters(string procedureName, SqlConnection connection)
        {
            var query = "select name, system_type_id, parameter_id from sys.parameters where object_id = object_id(@procName)";
            var dbParam = new SqlParameter("@procName", procedureName);

            var command = new SqlCommand(query, connection);
            command.Parameters.Add(dbParam);

            var results = new List<Parameter>();

            var reader = await command.ExecuteReaderAsync();

            try
            {
                while (await reader.ReadAsync())
                {
                    var item = new Parameter(reader.GetString(0), reader.GetByte(1), reader.GetInt32(2));

                    results.Add(item);
                }
            }
            finally
            {
                await reader.CloseAsync();
            }

            return results;
        }

        private async Task<List<Procedure>> QueryProcedures(SqlConnection connection)
        {
            var query = "select specific_name from INFORMATION_SCHEMA.ROUTINES where ROUTINE_TYPE = 'PROCEDURE'";
            var command = new SqlCommand(query, connection);
            var results = new List<Procedure>();
            var reader = await command.ExecuteReaderAsync();

            try
            {
                while (await reader.ReadAsync())
                {
                    var item = new Procedure(reader.GetString(0));

                    results.Add(item);
                }
            }
            finally
            {
                await reader.CloseAsync();
            }

            return results;
        }
    }

    public record Procedure(string SpecificName);

    public record Parameter(string Name, byte Type, int Order)
    {
        public ParameterInfoEmbeddedDocument ToParameterInfo()
        {
            return new ParameterInfoEmbeddedDocument
            {
                Name = Name,
                Title = Name,
                Order = Order,
                DataType = ParseDataType()
            };
        }

        private DataType ParseDataType()
        {
            try
            {
                return (DataType)Type;
            }
            catch
            {
                return DataType.Unknown;
            }
        }
    }
}