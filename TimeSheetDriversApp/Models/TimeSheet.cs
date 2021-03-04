using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetDriversApp.WebAPI.Models
{
    public class TimeSheet
    {
        public int TimeSheetId { get; set; } 
        public string TimeSheetKey => User?.Username + "-" + EntryDate.ToString("MM") + "-" + EntryDate.Year;
        public Status Status { get; set; }
        public DateTime EntryDate { get; set; }
        public int DayNo => EntryDate.Day;
        public string DayName => EntryDate.DayOfWeek.ToString();
        public DayType DayType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? BreakTime { get; set; }
        public TimeSpan WorkTimeTS
        {
            get
            {
                if (DayType != DayType.WorkDay && DayType != DayType.TimeConsumption)
                    return new TimeSpan(8, 0, 0);

                if (!EndTime.HasValue || !StartTime.HasValue)
                    return TimeSpan.Zero;

                TimeSpan totalTime = EndTime.Value - StartTime.Value;

                return totalTime - BreakTime.Value.TimeOfDay;
            }
        }
      
        public int? MetersSquared { get; set; }
        public int? KmStand { get; set; }
        public int? PrivatTanken { get; set; }
        public int? Fuel { get; set; }
        public int? AdBlue { get; set; }
        public string Notes { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
    public enum Status
    {
        WorkInProgress, ApprovedByEmployee, Closed
    }
    public enum DayType
    {
        WorkDay, TimeConsumption, Vacation, Sick, Other
    }
}
