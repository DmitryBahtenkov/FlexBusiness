using System;

namespace FBA.CrossCutting.Contract.Exceptions
{
    /// <summary>
    /// HTTP 400
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {}
    }
}