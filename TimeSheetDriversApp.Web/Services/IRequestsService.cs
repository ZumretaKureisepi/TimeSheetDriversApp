using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Web.ViewModels;

namespace TimeSheetDriversApp.Web.Services
{
   public interface IRequestsService
    {
        void UpsertTimeSheetEntry(Model.DTO.RequestDTO updatedRequest, int UserId, DateTime startDate, DateTime? endDate = null);
        void ProcessUpdate(RequestsUpdateVM vM);
        List<RequestDTO> GetRequests();
    }
}
