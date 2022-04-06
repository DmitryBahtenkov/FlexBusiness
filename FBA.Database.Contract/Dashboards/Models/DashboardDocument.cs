using System.Text.Json.Serialization;
using FBA.Repository.Contract.Documents;

namespace FBA.Database.Contract.Dashboards.Models
{
    public class DashboardDocument : IDocument
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsArchived { get; set; }
        public string ConnectionId { get; set; }
        public string StoredProcedureId { get; set; }
        public string[] Columns { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ChartType ChartType { get; set; }
    }

    public enum ChartType
    {
        Pie,
        Bar,
        Lineal
    }
}