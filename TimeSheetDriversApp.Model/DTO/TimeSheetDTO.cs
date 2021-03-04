using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Helper;

namespace TimeSheetDriversApp.Model.DTO
{
    public class TimeSheetDTO
    {
        public int TimeSheetId { get; set; }
        public string TimeSheetKey => User?.Username + "-" + EntryDate.ToString("MM") + "-" + EntryDate.Year;
        public Status Status { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateStr => EntryDate.ToShortDateString();
        public int DayNo => EntryDate.Day;
        public string DayName => EntryDate.DayOfWeek.ToString();
        public DayType DayType { get; set; }
        public string DayTypeStr => DayType.GetDescription();
        public DateTime? StartTime { get; set; }
        public string StartTimeStr => StartTime != null ? StartTime.Value.TimeOfDay.ToString(@"hh\:mm") : "";
        public DateTime? EndTime { get; set; }
        public string EndTimeStr => EndTime != null ? EndTime.Value.TimeOfDay.ToString(@"hh\:mm") : "";
        public DateTime? BreakTime { get; set; }
        public string BreakTimeStr => BreakTime != null ? BreakTime.Value.TimeOfDay.ToString(@"hh\:mm") : "00:00";
        public string WorkTime
        {
            get
            {
                if (DayType == DayType.Sick || DayType == DayType.Vacation)
                    return (new TimeSpan(8, 0, 0)).ToString(@"hh\:mm");

                if (!EndTime.HasValue || !StartTime.HasValue)
                    return "";

                TimeSpan totalTime = EndTime.Value - StartTime.Value;

                if (BreakTime.HasValue)
                    totalTime -= BreakTime.Value.TimeOfDay;

                return totalTime
                    .ToString(@"hh\:mm");
            }
        }

        public int? MetersSquared { get; set; }
        public int? KmStand { get; set; }
        public int? PrivatTanken { get; set; }
        public int? Fuel { get; set; }
        public int? AdBlue { get; set; }
        public string Notes { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; }

    }
    public enum Status
    {
        WorkInProgress, ApprovedByEmployee, Closed
    }
    public enum DayType
    {
        [Description("Work")]
        WorkDay,
        [Description("Time Consumption")]
        TimeConsumption,
        [Description("Vacation")]
        Vacation,
        [Description("Sick")]
        Sick,
        [Description("Other")]
        Other
    }
}
