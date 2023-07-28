using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IItemGroupRepo _itemGroupRepo;
        private readonly ISupplierRepo _supplierRepo;
        private readonly IPurchaseRepo _purchaseRepo;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PurchaseController(IItemGroupRepo itemGroupRepo,ISupplierRepo supplierRepo,IPurchaseRepo purchaseRepo, IHttpContextAccessor httpContextAccessor)
        {
            _itemGroupRepo = itemGroupRepo;
            _httpContextAccessor = httpContextAccessor;
            _supplierRepo = supplierRepo;
            _purchaseRepo = purchaseRepo;
        }

        [HttpGet]
        public IActionResult Purchase(CancellationToken token = default)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(purchaseRequest model, string purchasedItems,  CancellationToken token = default)
        {
            var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            DataTable dt = (DataTable)JsonConvert.DeserializeObject(purchasedItems, typeof(DataTable));
            
                await _purchaseRepo.SavePurchase(model, dt, userGUID, branchGUID, CompanyGUID, token);
            return Json(true);
        }
        [HttpGet]
        public async Task<JsonResult> getitemsTaxData(int id, CancellationToken token = default)
        {
            var AllData = await _purchaseRepo.GetItemsTaxList(id, token);

            return Json(AllData);
        }
        [HttpGet]
        public async Task<JsonResult> getitemsGroupData( CancellationToken token = default)
        {
            var AllData = await _itemGroupRepo.GroupList(token);

            return Json(AllData);
        }
        [HttpGet]
        public async Task<JsonResult> getTaxData( CancellationToken token = default)
        {
            var AllData = await _purchaseRepo.Taxlist(token);

            return Json(AllData);
        }
        [HttpGet]
        public async Task<JsonResult> getSupplierData( CancellationToken token = default)
        {
            var AllData = await _supplierRepo.SuppliersList(token);

            return Json(AllData);
        }
        [HttpGet]
        public async Task<JsonResult> GetTax(int id, CancellationToken token = default)
        {
            var AllData = await _purchaseRepo.GetTax(id, token);

            return Json(AllData);
        }


    }
}
