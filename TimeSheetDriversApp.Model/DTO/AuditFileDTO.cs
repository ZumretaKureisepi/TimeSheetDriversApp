using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Helper;

namespace TimeSheetDriversApp.Model.DTO
{
    public class AuditFileDTO
    {
        public int AuditFileId { get; set; }
        public TimeSheetField Field { get; set; }
        public string FieldStr => Field.GetDescription();
        public DateTime? OldValue { get; set; }
        public string OldValueStr => OldValue != null ? OldValue.Value.TimeOfDay.ToString(@"hh\:mm") : "(no value)";

        public DateTime? NewValue { get; set; }
        public string NewValueStr => NewValue != null ? NewValue.Value.TimeOfDay.ToString(@"hh\:mm") : "(no value)";

        public DateTime DateChanged { get; set; }
        public string DateChangedStr => DateChanged != DateTime.MinValue ?
            DateChanged.ToShortDateString()+" " + DateChanged.TimeOfDay.ToString(@"hh\:mm") : "(no value)";


        public UserDTO ChangedByUser { get; set; }
        public int ChangedByUserId { get; set; }

        public int TimeSheetId { get; set; }
        public TimeSheetDTO TimeSheet { get; set; }

    }


    public enum TimeSheetField
    {
        [Description("Start Time")]
        StartTime,
        [Description("End Time")]
        EndTime,
        [Description("Break Time")]
        BreakTime
    }

}
