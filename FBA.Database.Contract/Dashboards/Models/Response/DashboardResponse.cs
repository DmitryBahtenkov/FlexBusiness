namespace FBA.Database.Contract.Dashboards.Models.Response
{
    public class DashboardResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public string StoredProcedureId { get; set; }
        public string[] Columns { get; set; }
        public ChartType ChartType { get; set; }
    }
}