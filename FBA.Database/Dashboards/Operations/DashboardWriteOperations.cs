using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Dashboards.Models;
using FBA.Database.Contract.Dashboards.Operations;
using FBA.Repository;
using FBA.Repository.Operations;

namespace FBA.Database.Dashboards.Operations
{
    public class DashboardWriteOperations : WriteOperations<DashboardDocument>, IDashboardWriteOperations
    {
        public DashboardWriteOperations(DbContext dbContext) : base(dbContext)
        {
        }
    }
}