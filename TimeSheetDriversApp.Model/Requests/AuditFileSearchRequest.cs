using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeSheetDriversApp.Model.Requests
{
    public class AuditFileSearchRequest
    {
        [Display(Name = "Month")]
        public List<int> Months { get; set; }
        [Display(Name = "User")]
        public List<int> UserIDs { get; set; }
        public int? Year { get; set; }
    }
}
