
using EmployeeManage.Controllers;
using EmployeeManage.Model;
using EmployeeManage.Models;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
   public interface IItemGroupRepo
    {
        TblItem GetItem(int id);
        TblItemPrice GetItemprice(int id);
      

        Task AddItemGroup(Item_GroupRequest model,string usierid,string companyid, CancellationToken token = default);
        Task AddItem(ItemsRequest model, string usierid, string companyid, CancellationToken token = default);
        Task AddUnitOfMeasure(UnitOfMeasureRequest model, CancellationToken token = default);
        Task AddTax(AddTaxRequestVM model, string usierGUID, string companyGUID, string branchGUID, CancellationToken token = default);
        Task AddItemsPrice(AdditemsPriceRequestVM model, string UserGUID,string CompanyGUID,  CancellationToken token = default);
        Task <IEnumerable<ItemsDataListsResponseVM>> UpdateItemData(CancellationToken token = default);


    
       public Task  <IEnumerable<SelectListItem>> ItemsList(CancellationToken token = default);
       public Task <IEnumerable<SelectListItem>> GroupList(CancellationToken token = default);
       public Task <IEnumerable<SelectListItem>> TaxList(CancellationToken token = default);
       public Task <IEnumerable<SelectListItem>> UnitInCaseList(CancellationToken token = default);
    }
}
