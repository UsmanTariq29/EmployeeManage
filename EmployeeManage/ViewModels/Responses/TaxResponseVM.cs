using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Responses
{
    public class TaxResponseVM
    {

        public int? TaxId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
    }
}
