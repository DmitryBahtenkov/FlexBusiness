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
        public Task<List<string>> GetTables(string connectionId);
    }
}