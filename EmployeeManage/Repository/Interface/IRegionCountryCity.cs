using EmployeeManage.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface IRegionCountryCity
    {
        public IEnumerable<RegionCountryCityInfo> ListOfCityCountryRegion();
    }
}
