using EmployeeManage.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface IDashboardRepo
    {
        Task<List<EmployeeDataResponseVM>> GetBirthdayBuddies(CancellationToken token = default);
        Task<List<EmployeeHires>> GetNewHires(CancellationToken token = default);
        Task<List<ExpiredDocuments>> GetExpiredDocuments(CancellationToken token = default);
        Task<List<ActiveEmployee>> GetActiveEmployees(CancellationToken token = default);

    }
}
