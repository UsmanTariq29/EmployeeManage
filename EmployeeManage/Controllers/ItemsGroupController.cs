using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class ItemsGroupController : Controller
    {

        private readonly IItemGroupRepo _itemGroupRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItemsGroupController(IItemGroupRepo itemGroupRepo, IHttpContextAccessor httpContextAccessor)
        {
            _itemGroupRepo = itemGroupRepo;
            _httpContextAccessor = httpContextAccessor;
        }



        [HttpGet]
        public IActionResult AddGroup()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGroup(Item_GroupRequest model, CancellationToken token = default)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyId = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            try
            {
                if (ModelState.IsValid)
                {
                    await _itemGroupRepo.AddItemGroup(model, userId, CompanyId,token);
                    TempData["success"] = "Added Successfylly";
                    return RedirectToAction("AddGroup", "ItemsGroup");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(model.ItemGroupName), Ex.Message);
                return View(model);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddItems(CancellationToken token = default)
        {
            var Grouplist = await _itemGroupRepo.GroupList(token);
            var Unitlist = await _itemGroupRepo.UnitInCaseList(token);
            var taxlist = await _itemGroupRepo.TaxList(token);
            ItemsRequest Items = new ItemsRequest()
            {
                GroupList = Grouplist,
                UnitInCaseList = Unitlist,
                TaxList = taxlist
            };
            return View(Items);
        }

        [HttpPost]
        public async Task<IActionResult> AddItems(ItemsRequest model, CancellationToken token = default)
            {
            var UserId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyId = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");

            if (ModelState.IsValid)
            {
                await _itemGroupRepo.AddItem(model, UserId, CompanyId,token);
                TempData["success"] = "Added Successfylly";
                return RedirectToAction("AddItems", "ItemsGroup");
            }
            var Grouplist = await _itemGroupRepo.GroupList(token);
            var Unitlist = await _itemGroupRepo.UnitInCaseList(token);
            var taxlist = await _itemGroupRepo.TaxList(token);
            ItemsRequest Items = new ItemsRequest()
            {
                GroupList = Grouplist,
                UnitInCaseList = Unitlist,
                TaxList = taxlist
            };
            return View();
        }

        [HttpGet]
        public IActionResult AddUnitOfMeasure()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUnitOfMeasure(UnitOfMeasureRequest model, CancellationToken token = default)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    await _itemGroupRepo.AddUnitOfMeasure(model, token);
                    TempData["success"] = "Added Successfylly";
                    return RedirectToAction("AddUnitOfMeasure", "ItemsGroup");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(model.unitOfMeasure), Ex.Message);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddItemTax()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItemTax(AddTaxRequestVM model, CancellationToken token= default)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
                    var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
                    var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

                    await _itemGroupRepo.AddTax(model,userId,CompanyGUID,branchGUID,token);
                    TempData["success"] = "Added Successfylly";
                    return RedirectToAction("AddItemTax", "ItemsGroup");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(model.taxId), Ex.Message);
                return View(model);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddItemsPrice(CancellationToken token = default)
        {
            var ItemsList = await _itemGroupRepo.ItemsList(token);
            AdditemsPriceRequestVM Items = new AdditemsPriceRequestVM()
            {
            ItemsList =ItemsList

            };
            return View(Items);

        }

        [HttpPost]
        public async Task<IActionResult> AddItemsPrice(AdditemsPriceRequestVM model, CancellationToken token = default)
        {
            var UserId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyId = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");

            if (ModelState.IsValid)
            {
                await _itemGroupRepo.AddItemsPrice(model, UserId, CompanyId, token);
                TempData["success"] = "Added Successfylly";
                return RedirectToAction("AddItemsPrice", "ItemsGroup");
            }
            var itemslist = await _itemGroupRepo.ItemsList(token);
            AdditemsPriceRequestVM Items = new AdditemsPriceRequestVM()
            {
                ItemsList = itemslist,
            };
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> AllItems(CancellationToken token = default)
        {
            var itemresult = await _itemGroupRepo.UpdateItemData(token);
           
            return View(itemresult);
        }
        [HttpGet]
        public async Task<ActionResult> EditItems(int id, CancellationToken token = default)
        {
            var result = _itemGroupRepo.GetItem(id);
            var price = _itemGroupRepo.GetItemprice(id);
            
           

            if (result == null)
            {
                return View("ItemNotFound", id);
            }
            var grouplist =await _itemGroupRepo.GroupList(token);
            var itemList = await _itemGroupRepo.ItemsList(token);
            var UomList = await _itemGroupRepo.UnitInCaseList(token);
            var taxlist = await _itemGroupRepo.TaxList(token);
            ItemsDataListsResponseVM itemUpdate = new ItemsDataListsResponseVM
            {
                ItemId = result.ItemId,
                ItemName = result.ItemName,
                ItemGroupId = result.ItemGroupId,
                ItemsPrice = price.ItemsPrice,
                unitOfMeasureId = result.UnitInCase,
                ItemsList = itemList,
                GroupList = grouplist,
                UOMList = UomList,
                taxList = taxlist
            };
            return View(itemUpdate);
        }



    }
}