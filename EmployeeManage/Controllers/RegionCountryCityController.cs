using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class RegionCountryCityController : Controller
    {
        private readonly IRegionCountryCity _regionCountryCity;

        public RegionCountryCityController(IRegionCountryCity regionCountryCity)
        {
            _regionCountryCity = regionCountryCity;
        }
        public IActionResult CountryRegionCity()
        {
            var regioncitycountry = _regionCountryCity.ListOfCityCountryRegion();
            return View(regioncitycountry);
        }
    }
}