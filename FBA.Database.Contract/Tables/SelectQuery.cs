using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FBA.CrossCutting.Contract.Exceptions;

namespace FBA.Database.Contract.Tables
{
    public record SelectQuery
    {
        public string? OrderField { get; set; }
        public Order? OrderType { get; set; }
        public int? Limit { get; set; }
        public List<SelectQueryInner> Fields { get; set; }
    }

    public record SelectQueryInner
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public void Validate()
        {
            if (Operator is ">" or "<" or "=")
            {
                return;
            }

            throw new BusinessException("Некорректный оператор");
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Order
    {
        Asc,
        Desc
    }
}