using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Requests;

namespace TimeSheetDriversApp.Web.ViewModels
{
    public class TimeSheetUpdateVM
    {
        public List<TimeSheetDTO> TimeSheet { get; set; }

        public TimeSheetSearchRequest SearchRequest { get; set; }

    }
}
