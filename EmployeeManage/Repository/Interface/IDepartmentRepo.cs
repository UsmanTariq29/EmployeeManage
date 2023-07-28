using EmployeeManage.Model;
using EmployeeManage.Models;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
   public interface IDepartmentRepo 
    {
      Task<TblDepartment> GetDeptartment(int id, CancellationToken token = default);
      public Task <IEnumerable<SelectListItem>> GetDeptartmentList(CancellationToken token = default);
      public Task<IEnumerable<SelectListItem>> GetBranchList(CancellationToken token = default);
       public Task <IEnumerable<SelectListItem>> GetNationalityList(CancellationToken token = default);
       Task<IEnumerable<EmployeeNetSalaryInfo>> GetNetSalaryOfEmoployee(CancellationToken token = default);
        Task<IEnumerable<BranchEmployeeInfo>> GETBranchNameEmployee(int id, CancellationToken token = default);
    }
}
