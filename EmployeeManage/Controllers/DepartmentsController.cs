using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class DepartmentsController : Controller
    {
        public string List()
        {
            return "List of Departments";
        }
        public string Detaiils()
        {
            return "Details of Departments";
        }
    }
}
