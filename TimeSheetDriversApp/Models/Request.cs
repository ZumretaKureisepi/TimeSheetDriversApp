using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetDriversApp.WebAPI.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public DateTime DateOfRequest { get; set; }
        public RequestType RequestType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public RequestStatus Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
    public enum RequestType
    {
        TimeConsumption, TimeSheet, Vacation
    }

    public enum RequestStatus
    {
        Pending, Approved, Declined
    }
}
