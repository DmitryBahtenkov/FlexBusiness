using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBA.Database.Contract.Dashboards.Models.Request
{
    public class DashboardRequest
    {
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Выберите базу данных")]
        public string ConnectionId { get; set; }
        [Required(ErrorMessage = "Выберите хранимую процедуру")]
        public string StoredProcedureId { get; set; }
        public string[] Columns { get; set; }
        [Required(ErrorMessage = "Выберите тип графика")]
        public ChartType ChartType { get; set; }
    }
}