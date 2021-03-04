using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetDriversApp.WebAPI.Models
{
    public class AuditFile
    {
        public int AuditFileId { get; set; }
        public TimeSheetField Field { get; set; }
        public DateTime? OldValue { get; set; }
        public DateTime? NewValue { get; set; }
        public DateTime DateChanged { get; set; }
        public User ChangedByUser { get; set; }             // da li se u bazu pohranjuje username usera koji je
        public int ChangedByUserId { get; set; }            // napravio promjenu ili nesto drugo

        public int TimeSheetId { get; set; }
        public TimeSheet TimeSheet { get; set; }
    }

    public enum TimeSheetField
    {
        StartTime, EndTime, BreakTime
    }
}
