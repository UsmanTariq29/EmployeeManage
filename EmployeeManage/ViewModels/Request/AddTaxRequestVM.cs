using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Request
{
    public class AddTaxRequestVM
    {
        public int taxId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
    }
}
