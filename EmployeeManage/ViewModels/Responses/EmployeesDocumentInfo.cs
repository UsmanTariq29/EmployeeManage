﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Responses
{
    public class EmployeesDocumentInfo
    {
        public int EmployeeId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentPath { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<SelectListItem> EmployeeActiveList { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> DocumentList { get; set; }
    }
}