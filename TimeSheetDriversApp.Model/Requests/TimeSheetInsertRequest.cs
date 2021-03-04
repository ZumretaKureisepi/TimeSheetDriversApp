using System;
using System.Collections.Generic;
using System.Text;
using TimeSheetDriversApp.Model.DTO;

namespace TimeSheetDriversApp.Model.Requests
{
    public class TimeSheetInsertRequest
    {
        public int TimeSheetId { get; set; }
        public Status Status { get; set; }
        public DateTime EntryDate { get; set; }
        public DayType DayType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? BreakTime { get; set; }

        public int? MetersSquared { get; set; }
        public int? KmStand { get; set; }
        public int? PrivatTanken { get; set; }
        public int? Fuel { get; set; }
        public int? AdBlue { get; set; }
        public string Notes { get; set; }

        public int UserId { get; set; }
    }
}
