using System;
using System;
using System.Collections.Generic;
using System.Text;
using TimeSheetDriversApp.Model.DTO;

namespace TimeSheetDriversApp.Model.Requests
{
    public class RequestInsertRequest
    {
        public int RequestId { get; set; }
        public RequestType RequestType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public RequestStatus Status { get; set; }
    }
}
