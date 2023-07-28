using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Responses
{
    public class EmployeeNetSalaryInfo
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal EmployeeGrossSalary { get; set; }
        //public int AllowanceId { get; set; }
        public decimal? Amount { get; set; }
        //public string AllowanceDetail { get; set; }
    }
}