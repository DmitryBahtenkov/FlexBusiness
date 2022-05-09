using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Repository.Contract.Documents;

namespace FBA.Database.Contract.SavedProcedures.Models
{
    public class SavedProcedureDocument : IDocument
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StoredProcedureId { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public bool IsArchived { get; set; }
    }
}