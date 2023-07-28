using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Request
{
    public class BarcodeRequest
    {
   
        public int ItemId { get; set; }
        public string GeneratedBarcodeId { get; set; }

        public IEnumerable<SelectListItem> ItemsList { get; set; }

    }
}
