using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Responses
{
    public class PromotionItemsResponse
    {
        public int ItemId { get; set; }
        public int? ItemTaxid { get; set; }
        public string ItemName { get; set; }
        public decimal? Discount { get; set; }
        public decimal ItemsPrice { get; set; }

    }
}
