using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;

namespace FBA.Database.Contract.Tables
{
    public interface ITableService
    {
        public Task<ExecuteResult> ExecuteSelect(string connectionId, string table, SelectQuery selectQuery = null);
        public Task<List<TableInfo>> GetTables(string connectionId);
    }

    public record TableInfo(string Name, string Type);
}