using EmployeeManage.Model;
using EmployeeManage.Models;
using EmployeeManage.ViewModels;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static EmployeeManage.Model.EmployeeRepo;

namespace EmployeeManage.Model
{
    public interface IEmployeeRepo
    {
       Task <TblEmployee> ValidateRecord(int Id, CancellationToken token = default);
       Task <TblEmployee> GetEmployee(int Id);
        EmployeeInfo GetEmployeeDetails(int Id);
       Task<IEnumerable<EmployeeInfo>> SelectEmployeeInfo();
        IEnumerable<VwEmployeeDatum> GetEmployeeData();
        IEnumerable<EmployeesDocumentInfo> GetRemarkPath(int id);
        void Create(EmployeeCreateVM model);
          IEnumerable<SelectListItem> GetEmployeeList();
        IEnumerable<SelectListItem> GetEmployeeListAct(int id);
        public Task updateAsync(EmployeeUpdateVM employeeChanges);
        public IEnumerable<CompanyInfo> GetEmployeeCompnyInfo();
        public Task DeleteAsync(int id,CancellationToken token=default);
    }
}
