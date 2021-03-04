using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeSheetDriversApp.Model.Requests
{
    public class TimeSheetSearchRequest
    {
        [Display(Name = "User")]
        public int? UserId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Day { get; set; }

        public List<int> Months { get; set; }
        public List<int> UserIDs { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
