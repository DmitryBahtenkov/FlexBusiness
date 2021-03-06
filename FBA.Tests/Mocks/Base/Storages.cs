using System.Collections.Generic;
using System.Linq;
using FBA.Auth.Contract.Models;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Repository.Contract.Documents;
using FBA.Tests.Data;
using FBA.Tests.Exceptions;
using FBA.Tests.ProcedureTests;

namespace FBA.Tests.Mocks.Base
{
    public class Storages
    {
        public static List<UserDocument> Users { get; }
        public static List<ConnectionsDocument> Connections { get; }
        public static List<StoredProcedureDocument> StoredProcedures { get; }

        static Storages()
        {
            Users = new List<UserDocument>();
            Connections = new List<ConnectionsDocument>();
            StoredProcedures = new List<StoredProcedureDocument>();
            
            Users.AddRange(TestUsers.GetAllDocuments());
            Connections.AddRange(TestConnections.GetAllDocuments());
            StoredProcedures.Add(TestProcedures.Procedure);
        }

        public static List<TDocument> GetStorage<TDocument>() where TDocument : IDocument
        {
            var props = typeof(Storages).GetProperties();
            var storage = props.FirstOrDefault(x => x.PropertyType == typeof(List<TDocument>));
            if (storage is null)
            {
                throw new StorageNotFoundException(typeof(TDocument));
            }

            if (storage.GetValue(null, null) is List<TDocument> value)
            {
                return value;
            }
            
            throw new StorageNotFoundException(typeof(TDocument));
        }
    }
}