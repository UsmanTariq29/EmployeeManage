using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Request
{
    public class AdditemsPriceRequestVM
    {

        public int PriceId { get; set; }
        public int ItemId { get; set; }

        public decimal ItemsPrice { get; set; }

        public IEnumerable<SelectListItem> ItemsList { get; set; }
    }
}
