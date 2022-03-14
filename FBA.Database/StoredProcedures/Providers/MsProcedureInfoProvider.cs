using System;
using System.Collections.Generic;
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
        public async Task<List<string>> GetNames(ConnectionsDocument document)
        {
            await using var sqlConnection = new SqlConnection(document.ConnectionString);
            await sqlConnection.OpenAsync();

            var procedures = await QueryProcedures(sqlConnection, document.ConnectionInfo.Database);

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

        private async Task<List<Procedure>> QueryProcedures(SqlConnection connection, string dbName)
        {
            var query = "SELECT SPECIFIC_NAME FROM @db.INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'";
            var dbParam = new SqlParameter("@db", dbName);

            var command = new SqlCommand(query, connection);
            command.Parameters.Add(dbParam);

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