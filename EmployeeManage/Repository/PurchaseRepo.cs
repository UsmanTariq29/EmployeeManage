using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class PurchaseRepo : IPurchaseRepo
    {
        private readonly EmployeesDBContext _db;

        public PurchaseRepo(EmployeesDBContext db)
        {
            _db = db;

        }

        public async Task<IEnumerable<TaxResponseVM>> GetItemsTaxList(int id, CancellationToken token)
        {
            {
                var result = await _db.TblItems
                    .AsNoTracking()
                    .Where(x => x.ItemId == id)
                    .Join(
                       _db.TblTaxes,
                      data => data.TaxId,
                      tax => tax.TaxId,
                      (data, tax) => new { data, tax }
                      )
                      .Select(r => new TaxResponseVM
                      {
                          TaxId = r.data.TaxId,
                          Name = r.tax.Name
                      })
                       .ToListAsync(token);

                return result;
            }
        }

        public async Task<TaxResponseVM> GetTax(int id, CancellationToken token)
        {
            var result = await _db.TblTaxes
                      .AsNoTracking()
                      .Where(x => x.TaxId == id)
                        .Select(r => new TaxResponseVM
                        {
                            Percentage = r.Percentage
                        }).FirstOrDefaultAsync(token);

            return result;
        }

        public async Task SavePurchase(purchaseRequest model, DataTable purchasedItems, string userGUID, string branchGUID, string companyGUID, CancellationToken token = default)
        {
            await using var dbTransaction = await _db.Database.BeginTransactionAsync(token);

            try
            {
                var PurchaseMaster = new TblPurchaseMaster
                {
                    SupplierId = model.SupplierId,
                    DateCreated = System.DateTime.Today,
                    CreatedByUserGuid = userGUID,
                    CompanyGuid = companyGUID,
                    BranchGuid = branchGUID,
                    TotalDiscount = model.TotalDiscount,
                    NetAmount = model.NetAmount,
                    TotalAmount = model.TotalAmount,
                    TotalTax = model.TotalTax,
                    InvoiceNo = model.InvoiceNo,
                    Description = model.Description
                };
                _db.TblPurchaseMasters.Add(PurchaseMaster);
                await _db.SaveChangesAsync(token);

               await SavepurchaseDetail(purchasedItems, PurchaseMaster.PurchaseMasterId, token);

                await dbTransaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(token);
                throw ex;
            }
        }
        private async Task SavepurchaseDetail(DataTable purchasedItems, int masterId, CancellationToken token = default)
        {
            foreach (DataRow dr in purchasedItems.Rows)
            {
                var Purchasedetail = new TblPurchaseDetail
                {
                    ItemId = Convert.ToInt32(dr["ItemId"]),
                    Quantity = Convert.ToInt32(dr["ItemQuantity"]),
                    Price = Convert.ToDecimal(dr["ItemPRICE"]),
                    PurchaseMasterId = masterId,
                    Amount = Convert.ToDecimal(dr["ItemAmount"]),
                    FixedDiscount = Convert.ToDecimal(dr["ItemFixedDiscount"]),
                    Percentage = Convert.ToDecimal(dr["ItemTax"]),
                    AmountAfterDiscount = Convert.ToDecimal(dr["DiscountedAmount"]),
                    DiscountPercentage = Convert.ToDecimal(dr["ItemsPercentageDiscount"]),
                    ItemGroupId = Convert.ToInt32(dr["ItemGroupId"]),
                    ItemTaxId = Convert.ToInt32(dr["TaxId"]),
                    Description = Convert.ToString(dr["Description"]),
                };


                _db.TblPurchaseDetails.Add(Purchasedetail);

                await _db.SaveChangesAsync(token);
            }
        }


        public async Task<IEnumerable<SelectListItem>> Taxlist(CancellationToken token = default)
        {
            var data = await _db.TblTaxes
                .AsNoTracking()
                .ToListAsync(token);

            List<SelectListItem> taxlist = data
                .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.TaxId.ToString(),
                        Text = n.Name
                    }).ToList();
            var Itemslist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Tax"
            };
            taxlist.Insert(0, Itemslist);
            return new SelectList(taxlist, "Value", "Text");
        }


    }
}
