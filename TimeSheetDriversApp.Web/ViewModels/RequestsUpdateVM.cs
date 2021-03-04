using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;

namespace TimeSheetDriversApp.Web.ViewModels
{
    public class RequestsUpdateVM
    {
        public List<RequestDTO> requests { get; set; }
        public List<int> selectedRequests { get; set; }

        public string Action { get; set; }
    }
}
