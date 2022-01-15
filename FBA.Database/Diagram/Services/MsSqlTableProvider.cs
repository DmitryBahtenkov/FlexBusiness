using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Diagram.Models;
using FBA.Database.Contract.Diagram.Services;
using Microsoft.Data.SqlClient;

namespace FBA.Database.Diagram.Services
{
    public class MsSqlTableProvider : ITableProvider
    {
        public async Task<List<TableEmbeddedDocument>> GetTables(ConnectionsDocument connection)
        {
            var tables = new List<TableEmbeddedDocument>();
            var results = new List<InformationSchemaResult>();
            
            await using var sqlConnection = new SqlConnection(connection.ConnectionString);
            var query =
                "select  TABLE_NAME, COLUMN_NAME, ORDINAL_POSITION, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH  from INFORMATION_SCHEMA.COLUMNS";
            
            var command = new SqlCommand(query, sqlConnection);
            var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var item = new InformationSchemaResult(reader.GetString(0),
                    reader.GetString(1),
                    (int) reader.GetValue(2),
                    reader.GetString(3),
                    (int?) reader.GetValue(4));
                
                results.Add(item);
            }

            var grouping = results
                .GroupBy(x => x.TableName)
                .ToDictionary(x => x.Key, x => x.ToList());

            foreach (var (table, columns) in grouping)
            {
                var primaryKeys = await GetPrimaryKeysForTable(sqlConnection, table);
                var tableDocument = new TableEmbeddedDocument
                {
                    Title = table,
                    Fields = columns.Select(x=>new FieldEmbeddedDocument
                    {
                        Title = x.ColumnName,
                        DataType = x.CharLength is null ? x.DataType : $"{x.DataType}({x.CharLength})",
                        Position = x.Position,
                        IsPrimaryKey = primaryKeys.Any(p => p.ColumnName == x.ColumnName)
                    }).ToList(),
                    References = await GetReferencesForTable(sqlConnection, table)
                };
                
                tables.Add(tableDocument);
            }

            return tables;
        }

        private async Task<List<ReferenceEmbeddedDocument>> GetReferencesForTable(SqlConnection connection, string tableName)
        {
            var query = $"EXEC sp_fkeys @tableName";
            var nameParam = new SqlParameter("@tableName", tableName);
            var command = new SqlCommand(query, connection);
            command.Parameters.Add(nameParam);
            command.CommandType = CommandType.StoredProcedure;


            var results = new List<ReferenceEmbeddedDocument>();

            var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var item = new ReferenceEmbeddedDocument
                {
                    ToTable = reader.GetString(6)
                };
                
                results.Add(item);
            }

            return results;
        }

        private async Task<List<SpPkKeysResult>> GetPrimaryKeysForTable(SqlConnection connection, string tableName)
        {
            var query = $"EXEC sp_pkeys @tableName";
            var nameParam = new SqlParameter("@tableName", tableName);
            var command = new SqlCommand(query, connection);
            command.Parameters.Add(nameParam);
            command.CommandType = CommandType.StoredProcedure;

            var results = new List<SpPkKeysResult>();

            var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var item = new SpPkKeysResult(reader.GetString(3));
                
                results.Add(item);
            }

            return results;
        }
    }

    record InformationSchemaResult(
        string TableName,
        string ColumnName,
        int Position,
        string DataType,
        int? CharLength);

    record SpPkKeysResult(string ColumnName);
}