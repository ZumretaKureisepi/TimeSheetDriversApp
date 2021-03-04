using System;
using System.Collections.Generic;
using System.Text;
using TimeSheetDriversApp.Model.DTO;

namespace TimeSheetDriversApp.Model.Requests
{
    public class AuditFileInsertRequest
    {
        public int AuditFileId { get; set; }
        public TimeSheetField Field { get; set; }
        public DateTime? OldValue { get; set; }
        public DateTime? NewValue { get; set; }
        public DateTime DateChanged { get; set; }
        public int ChangedByUserId { get; set; }

        public int TimeSheetId { get; set; }
    }
}
