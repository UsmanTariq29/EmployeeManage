
using EmployeeManage.Model;
using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUser _user;
        public LoginController(IUser iuser)
        {
            _user = iuser;
        }
        public ViewResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(User user)
        {
            var validate = await _user.user(user.UserName, user.Password);


            if (validate != null)
            {

                if (validate.Passwordexp <= System.DateTime.Today)
                {
                    //throw new Exception("Password Expires");                    
                }
                

                this.HttpContext.Session.SetString("Username", validate.UserName);
                this.HttpContext.Session.SetString("UserGUID", validate.UserGuid);
                this.HttpContext.Session.SetString("companyGUID", validate.CompanyGuid);
                this.HttpContext.Session.SetString("branchGUID", validate.BranchGuid);

                //return RedirectToAction("BarcodeSearch", "Barcode");
                return RedirectToAction("Purchase", "Purchase");
            }
            else
                return RedirectToAction("Index");
        }
    }
}