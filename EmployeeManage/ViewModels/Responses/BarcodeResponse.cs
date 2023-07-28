using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Responses
{
    public class BarcodeResponse
    {

        public int ItemId { get; set; }

        public string ItemBarcode { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }

        public string Name { get; set; }
        public decimal Percentage { get; set; }





    }
}
