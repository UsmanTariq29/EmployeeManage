using EmployeeManage.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
   public interface IDocumentCount
    {
         Task<IEnumerable<TotalDocumentsInfo>> DocumentsInfoList(CancellationToken token = default);
    }
}
