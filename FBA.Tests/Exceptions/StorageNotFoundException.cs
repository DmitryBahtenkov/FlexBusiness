using System;

namespace FBA.Tests.Exceptions
{
    public class StorageNotFoundException : Exception
    {
        public StorageNotFoundException(Type notFoundedType) 
            : base($"Storage with type {notFoundedType.Name} not found")
        { }
    }
}