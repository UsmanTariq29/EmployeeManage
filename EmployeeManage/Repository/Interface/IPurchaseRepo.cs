using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
   public interface IPurchaseRepo
    {

        Task SavePurchase( purchaseRequest model, DataTable purchasedItems,  string userGUID, string branchGUID, string companyGUID,
           CancellationToken token = default);
        Task<IEnumerable<TaxResponseVM>> GetItemsTaxList(int id, CancellationToken token);
        Task<TaxResponseVM> GetTax(int id, CancellationToken token);

        Task<IEnumerable<SelectListItem>> Taxlist(CancellationToken token = default);


    }
}
