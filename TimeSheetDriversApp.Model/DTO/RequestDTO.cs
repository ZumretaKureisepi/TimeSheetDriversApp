using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Helper;

namespace TimeSheetDriversApp.Model.DTO
{
    public class RequestDTO
    {
        public int RequestId { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string DateOfRequestStr => DateOfRequest != DateTime.MinValue ? DateOfRequest.ToShortDateString() : "";

        public RequestType RequestType { get; set; }
        public string RequestTypeStr => RequestType.GetDescription();
        public DateTime StartDateTime { get; set; }
        public string StartDateStr => StartDateTime != DateTime.MinValue ? StartDateTime.ToShortDateString() : "";
        public string StartTimeStr => StartDateTime != DateTime.MinValue ? StartDateTime.TimeOfDay.ToString(@"hh\:mm") : "";

        public DateTime EndDateTime { get; set; }
        public string EndDateStr => EndDateTime != DateTime.MinValue ? EndDateTime.ToShortDateString() : "";
        public string EndTimeStr => EndDateTime != DateTime.MinValue ? EndDateTime.TimeOfDay.ToString(@"hh\:mm") : "";
        public RequestStatus Status { get; set; }
        public string StatusStr => Status.GetDescription();

        public int UserId { get; set; }
        public UserDTO User { get; set; }

    }
    public enum RequestType
    {
        [Description("Time Consumption")]
        TimeConsumption,
        [Description("Time Sheet")]
        TimeSheet,
        [Description("Vacation")]
        Vacation
    }

    public enum RequestStatus
    {
        Pending, Approved, Declined
    }
}
