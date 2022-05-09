using System.IO;
using System.Collections.Generic;

namespace FBA.Database.Contract.Dashboards.Models.Response
{
    public class DashboardResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public string StoredProcedureId { get; set; }
        public ChartType ChartType { get; set; }
        public DashboardSettings Settings { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string UserId { get; set; }
    }
}