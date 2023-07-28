using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Responses
{
    public class ItemsDataListsResponseVM
    {
        public int PriceId { get; set; }
        public int ItemId { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemName { get; set; }
        public int ItemGroupId { get; set; }
        public int? Taxid { get; set; }
        public string TaxName { get; set; }
        public decimal ItemsPrice { get; set; }
        public string unitOfMeasure { get; set; }
        public int unitOfMeasureId { get; set; }
        public string UnitOfMeasureDescription { get; set; }
        public  IEnumerable<SelectListItem> ItemsList { get; set; }
        public IEnumerable<SelectListItem> UOMList { get; set; }
        public IEnumerable<SelectListItem> taxList { get; set; }
        public IEnumerable<SelectListItem> GroupList { get; set; }


    }
}
