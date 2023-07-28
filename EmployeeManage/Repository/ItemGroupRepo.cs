using EmployeeManage.Controllers;
using EmployeeManage.Model;
using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class ItemGroupRepo : IItemGroupRepo
    {
        private readonly EmployeesDBContext _db;

        public ItemGroupRepo(EmployeesDBContext db)
        {
            _db = db;
            
        }


        public async Task AddItemGroup(Item_GroupRequest model, string userId, string CompanyId, CancellationToken  token= default)
        {
            await ValidateGroup(model.ItemGroupName,token);
            var itemGroup = new TblItemGroup
            {
                ItemGroupName = model.ItemGroupName,
                ItemCreatedDateTime = DateTime.Today.Date,
                UserGuId = userId,
                CompanyGuid = CompanyId
            };
           
            _db.TblItemGroups.Add(itemGroup);
            await _db.SaveChangesAsync(token);
        }

        public  async Task AddItem(ItemsRequest model, string userGUID, string CompanyId, CancellationToken token = default)
        {

            var item = new TblItem
            {
                ItemCode = model.ItemCode,
                ItemName = model.ItemName,
                IsExempted = model.IsExempted,
                IsActive = model.IsActive,
                ItemDateTime = DateTime.Today.Date,
                ItemReOrder = model.ItemReorder,
                ItemGroupId = model.ItemGroupId,
                IsBatchItem = model.IsBatchItem,
                IsRepalaceable = model.IsReplaceable,
                IsExpiryAllowed = model.IsExpiryAllowed,
                UnitInCase = model.UnitInCase,
                UserGuId = userGUID,
                TaxId = model.TaxId,
                CompanyGuid = CompanyId
            };

            _db.TblItems.Add(item);
          await  _db.SaveChangesAsync(token);
        }
        public async Task<List <TblTax>> getTax(CancellationToken token= default)
        {
            return await _db.TblTaxes.ToListAsync(token);
        }
       
        public async Task<List <TblUnitOfMeasure>> getUOM(CancellationToken token = default)
        {
            return await _db.TblUnitOfMeasures.ToListAsync(token);
        }
        public async Task<List <TblItem>> getItems(CancellationToken token = default)
        {
            return await _db.TblItems.ToListAsync(token);
        }
        public async Task<IEnumerable<SelectListItem>> ItemsList(CancellationToken token = default)
        {
            var data = await getItems(token);

            List<SelectListItem> itemslist = data
                .OrderBy(n => n.ItemName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ItemId.ToString(),
                        Text = n.ItemName
                    }).ToList();
            var Itemslist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Items"
            };
            itemslist.Insert(0, Itemslist);
            return new SelectList(itemslist, "Value", "Text");

        }

        public async Task<IEnumerable<SelectListItem>> GroupList(CancellationToken token = default)
        {
            var data = await _db.TblItemGroups.ToListAsync(token);

            List<SelectListItem> group = data
                .OrderBy(n => n.ItemGroupName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ItemGroupId.ToString(),
                        Text = n.ItemGroupName
                    }).ToList();

            var grplist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Group"
            };
            group.Insert(0, grplist);

            return new SelectList(group, "Value", "Text");

        } 
        public async Task<IEnumerable<SelectListItem>> TaxList(CancellationToken token = default)
        {
            var data = await getTax(token);
            
            List<SelectListItem> group = data
                .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.TaxId.ToString(),
                        Text = n.Name
                    }).ToList();
            var taxlist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Tax"
            };
            group.Insert(0, taxlist);

            return  new SelectList(group, "Value", "Text");

        } 

        public async Task<IEnumerable<SelectListItem>> UnitInCaseList(CancellationToken token = default)
        {
            var data = await getUOM(token); 

            List<SelectListItem> unit = data
                .OrderBy(n => n.UnitOfMeasure)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.UnitOfMeasureId.ToString(),
                        Text = n.UnitOfMeasure
                    }).ToList();
            var UOM = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Unit"
            };
            unit.Insert(0, UOM);
            return new SelectList(unit, "Value", "Text");

        }
        public async Task AddUnitOfMeasure(UnitOfMeasureRequest model, CancellationToken token = default)
        {
            await ValidateUnitMeasureRecord(model.unitOfMeasure, token);
            var measure = new TblUnitOfMeasure
            {
                UnitOfMeasure = model.unitOfMeasure,
                UnitOfMeasureDescription = model.UnitOfMeasureDescription
            };
            _db.TblUnitOfMeasures.Add(measure);
                await _db.SaveChangesAsync(token);
        }
        public async Task<TblUnitOfMeasure> ValidateUnitMeasureRecord(string UnitOfMeasureName, CancellationToken token = default)
        {
            var record = await _db.TblUnitOfMeasures
                .AsNoTracking()
                .Where(x => x.UnitOfMeasure.Equals(UnitOfMeasureName))
                .FirstOrDefaultAsync(token);
            if (record != null)
            {
                throw new Exception("Unit Of Measure Already Exists");
            }
            return  record;
        }
        public async Task<TblItemGroup> ValidateGroup(string GroupName, CancellationToken token = default)
        {
            var record = await _db.TblItemGroups
                .AsNoTracking()
                .Where(x => x.ItemGroupName.Equals(GroupName))
                .FirstOrDefaultAsync(token);
           
            if ( record != null)
            {
                throw new Exception("Group Name Already Exists");
            }
            return  record;
        }

        public async Task AddItemsPrice(AdditemsPriceRequestVM model,string  UserGUID,string CompanyGUID, CancellationToken token = default)
        {
            var data = new TblItemPrice
            {
                ItemId = model.ItemId,
                ItemsPrice =model.ItemsPrice,
                UserGuid = UserGUID,
                CompanyGuid =CompanyGUID
            };
            Serilog.Log.Information(LoggerMessageClass.ItemPrice.ItemPriceInserted, data);
            _db.TblItemPrices.Add(data);
            await _db.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<ItemsDataListsResponseVM>> UpdateItemData(CancellationToken token = default)
        {
            var query = await  _db.TblItems
                .Join(_db.TblItemGroups,

                item => item.ItemGroupId,
                group => group.ItemGroupId,
                (item, group) => new { item, group }
                )
                .Join(_db.TblUnitOfMeasures,
                data => data.item.UnitInCase,
                uom => uom.UnitOfMeasureId,
                (data, uom) => new
                {
                    data,
                    data.item,
                    data.group,
                    uom
                }
                )
                .Join(_db.TblItemPrices,
                data => data.item.ItemId,
                price => price.ItemId,
                (data, price) => new
                {
                    data,
                    data.item,
                    data.uom,
                    data.group,
                    price
                }
                )
                .Select(r=>new  ItemsDataListsResponseVM
                {
                    ItemId = r.item.ItemId,
                    unitOfMeasure =r.uom.UnitOfMeasure,
                    ItemGroupName =r.group.ItemGroupName,
                    ItemsPrice =  r.price.ItemsPrice,
                    UnitOfMeasureDescription =r.uom.UnitOfMeasureDescription,
                    ItemName =r.item.ItemName
                }).ToListAsync(token);

            return query;
        }


        public TblItem GetItem(int Id)
        {
            return ValidateRecord(Id);
        }
        private TblItem ValidateRecord(int itemid)
        {
            var record = _db.TblItems
                .AsNoTracking()
                .Where(x => x.ItemId == itemid)
                .FirstOrDefault();

            if (record == null)
            {
                throw new Exception("Record not found");

            }

            return record;
        }

        public TblItemPrice GetItemprice(int id)
        {
            return validteItemPrice(id);
        }
        
        private TblItemPrice validteItemPrice(int itemid)
        {
            var record = _db.TblItemPrices
                .AsNoTracking()
                .Where(x => x.ItemId == itemid)
                .FirstOrDefault();

            if (record == null)
            {
                throw new Exception("Price not found");
            }

            return record;
        }
        private async Task<TblTax> validteTax(int Taxid,CancellationToken token = default)
        {
            var record = _db.TblTaxes
                .AsNoTracking()
                .Where(x => x.TaxId == Taxid)
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Tax not found");
            }

            return await record;
        }

        public async Task AddTax(AddTaxRequestVM model, string userGUID, string companyGUID, string branchGUID, CancellationToken token = default)
        {
            var tax = new TblTax
            {
               TaxId  = model.taxId,
                Name= model.Name,
                Percentage = model.Percentage,
                Date = DateTime.Today.Date,
                CreatedByUserGuid = userGUID,
                CompanyGuid =companyGUID,
                BranchGuid = branchGUID
            };
            _db.TblTaxes.Add(tax);
            await _db.SaveChangesAsync(token);


        }
    }
}