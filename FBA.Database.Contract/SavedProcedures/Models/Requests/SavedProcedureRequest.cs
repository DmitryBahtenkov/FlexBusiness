using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBA.Database.Contract.SavedProcedures.Models.Requests
{
    public class SavedProcedureRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string StoredProcedureId { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}