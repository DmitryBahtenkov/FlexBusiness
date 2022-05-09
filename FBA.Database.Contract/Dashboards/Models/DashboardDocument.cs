using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public ChartType ChartType { get; set; }
        public DashboardSettings Settings { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string UserId { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChartType
    {
        Pie,
        Bar,
        Lineal
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Aggregation
    {
        CountUnique,
        Avg,
        Min,
        Max
    }

    public record DashboardSettings
    {
        public Aggregation? Aggregation { get; set; }
        public string[] Columns { get; set; }
        public string X { get; set; }
        public string[] Y { get; set; }
    }

    public record LinearResult(string Name, Dictionary<object, object> Data);
    public record AggregationResult(string Name, object Value);

}