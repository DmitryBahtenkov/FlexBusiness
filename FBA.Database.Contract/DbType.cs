using System.Text.Json.Serialization;

namespace FBA.Database.Contract
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DbType
    {
        MySql,
        Postgres,
        MsSql
    }
}