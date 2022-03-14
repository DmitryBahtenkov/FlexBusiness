using System.Text.Json.Serialization;

namespace FBA.Database.Contract.StoredProcedures.Models
{
    public class ParameterInfoEmbeddedDocument
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public DataType DataType { get; set; }
        public int Order { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataType 
    {
        Date = 61,
        Varchar = 167,
        Integer = 56,
        Unknown = -1
    }
}