﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Requests;

namespace TimeSheetDriversApp.Web.ViewModels
{
    public class AuditFileExportVM
    {

        public List<AuditFileDTO> AuditFiles { get; set; }

        public List<SelectListItem> UsersList { get; set; }
        public List<SelectListItem> MonthsList { get; set; }
        public List<SelectListItem> YearsList { get; set; }

        public AuditFileSearchRequest SearchRequest { get; set; }
    }
}
