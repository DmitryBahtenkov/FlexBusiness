using System;
using System.Collections.Generic;

namespace FBA.CrossCutting.Contract.Exceptions
{
    /// <summary>
    /// HTTP 422
    /// </summary>
    public class ValidationException : Exception
    {
        public Dictionary<string, string> Fields { get; set; } = new();

        public ValidationException(Dictionary<string, string> fields)
        {
            Fields = fields;
        }
    }
}