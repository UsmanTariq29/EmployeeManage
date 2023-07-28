using EmployeeManage.Model;
using EmployeeManage.Models;
using EmployeeManage.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Model
{
   public interface IUser 
    {
        Task<TblUser> user(string userName, string Password, CancellationToken token = default);
          

    }
}
