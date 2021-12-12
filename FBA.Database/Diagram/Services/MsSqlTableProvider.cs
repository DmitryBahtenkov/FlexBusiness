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
                .ToDictionary(x=>x.Key, x=> x.ToList());

            foreach (var (table, columns) in grouping)
            {
                var tableDocument = new TableEmbeddedDocument
                {
                    Title = table,
                    Fields = columns.Select(x=>new FieldEmbeddedDocument
                    {
                        Title = x.ColumnName,
                        DataType = x.CharLength is null ? x.DataType : $"{x.DataType}({x.CharLength})",
                        Position = x.Position
                    }).ToList()
                };
                
                //todo: exec two stored procedures for refs and PK
                
                tables.Add(tableDocument);
            }

            return tables;
        }
    }

    record InformationSchemaResult(
        string TableName,
        string ColumnName,
        int Position,
        string DataType,
        int? CharLength);
}