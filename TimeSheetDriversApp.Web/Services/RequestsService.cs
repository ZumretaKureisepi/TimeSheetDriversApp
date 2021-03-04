using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.Web.ViewModels;
using TimeSheetDriversApp.WebAPI.Services;

namespace TimeSheetDriversApp.Web.Services
{
    public class RequestsService: IRequestsService
    {
        private readonly WebAPI.Services.IRequestsService requestsAPIService;
        private readonly WebAPI.Services.ITimeSheetsService timeSheetsAPIService;
        private readonly IMapper mapper;

        public RequestsService(WebAPI.Services.IRequestsService requestsAPIService, WebAPI.Services.ITimeSheetsService timeSheetsAPIService, IMapper mapper)
        {
            this.requestsAPIService = requestsAPIService;
            this.timeSheetsAPIService = timeSheetsAPIService;
            this.mapper = mapper;
        }

        public void ProcessUpdate(RequestsUpdateVM VM)
        {
            foreach (var requestId in VM.selectedRequests)
            {
                var updatedRequest = requestsAPIService.GetRequest(requestId);

                if (updatedRequest.Status != Model.DTO.RequestStatus.Pending)
                    continue;

                int UserId = updatedRequest.UserId;

                if (VM.Action == "Approve")
                {
                    if (updatedRequest.RequestType == Model.DTO.RequestType.TimeSheet)
                    {
                        ApproveTimeSheetRequest(updatedRequest, UserId);
                    }
                    else if (updatedRequest.RequestType == Model.DTO.RequestType.Vacation)
                    {
                        ApproveVacationRequest(updatedRequest, UserId);

                    }
                    else if (updatedRequest.RequestType == Model.DTO.RequestType.TimeConsumption)
                    {
                        ApproveTimeConsumptionRequest(updatedRequest, UserId);

                    }

                    updatedRequest.Status = Model.DTO.RequestStatus.Approved;
                }
                else if (VM.Action == "Decline")
                {
                    updatedRequest.Status = Model.DTO.RequestStatus.Declined;
                }
                var mappedEntry = mapper.Map<RequestInsertRequest>(updatedRequest);
                requestsAPIService.PutRequest(requestId, mappedEntry);
            }
        }

        private void ApproveTimeConsumptionRequest(RequestDTO updatedRequest, int UserId)
        {
            var startDate = updatedRequest.StartDateTime;
            var endDate = updatedRequest.EndDateTime;
            int timeConsumptionDays = (updatedRequest.EndDateTime.Date - updatedRequest.StartDateTime.Date).Days + 1;
            for (int i = 0; i < timeConsumptionDays; i++)
            {
                this.UpsertTimeSheetEntry(updatedRequest, UserId, startDate, endDate);

                startDate = startDate.AddDays(1);
                endDate = endDate.AddDays(1);
            }
        }

        private void ApproveVacationRequest(RequestDTO updatedRequest, int UserId)
        {
            var entryDate = updatedRequest.StartDateTime.Date;
            int vacationDays = (updatedRequest.EndDateTime.Date - updatedRequest.StartDateTime.Date).Days + 1;
            for (int i = 0; i < vacationDays; i++)
            {
                this.UpsertTimeSheetEntry(updatedRequest, UserId, entryDate);

                entryDate = entryDate.AddDays(1);
            }
        }

        private void ApproveTimeSheetRequest(RequestDTO updatedRequest, int UserId)
        {
            var entries = timeSheetsAPIService.GetTimeSheets(new TimeSheetSearchRequest
            {
                UserId = UserId,
                DateFrom = updatedRequest.StartDateTime,
                DateTo = updatedRequest.EndDateTime
            });
            foreach (var timeSheetEntry in entries)
            {
                if (timeSheetEntry.Status == Model.DTO.Status.ApprovedByEmployee)
                {
                    timeSheetEntry.Status = Model.DTO.Status.Closed;

                    var mappedTSEntry = mapper.Map<TimeSheetInsertRequest>(timeSheetEntry);
                    timeSheetsAPIService.PutTimeSheet(timeSheetEntry.TimeSheetId, mappedTSEntry);
                }
            }

            var month = updatedRequest.EndDateTime.Month;
            var year = updatedRequest.EndDateTime.Year;
            if (month == 12)
            {
                year++;
                month = 1;
            }
            else
            {
                month++;
            }

            var entryDate = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= daysInMonth; i++)
            {
                var existingTimeSheets = timeSheetsAPIService.GetTimeSheets(new TimeSheetSearchRequest
                {
                    UserId = updatedRequest.UserId,
                    Month = entryDate.Month,
                    Year = entryDate.Year,
                    Day = entryDate.Day
                });

                if (existingTimeSheets.Count == 0)
                {
                    timeSheetsAPIService.PostTimeSheet(new TimeSheetInsertRequest
                    {
                        EntryDate = entryDate,
                        UserId = UserId
                    });

                }
                entryDate = entryDate.AddDays(1);
            }
        }

        public void UpsertTimeSheetEntry(Model.DTO.RequestDTO updatedRequest, int UserId, DateTime startDate, DateTime? endDate = null)
        {
            DayType dayType = updatedRequest.RequestType == Model.DTO.RequestType.Vacation
                ? DayType.Vacation
                : DayType.TimeConsumption;

            var existingTimeSheets = timeSheetsAPIService.GetTimeSheets(new TimeSheetSearchRequest
            {
                UserId = updatedRequest.UserId,
                Month = startDate.Month,
                Year = startDate.Year,
                Day = startDate.Day
            });

            TimeSheetInsertRequest request;

            if (existingTimeSheets.Count > 0)
            {
                var existingTimeSheet = existingTimeSheets[0];
                request = mapper.Map<TimeSheetInsertRequest>(existingTimeSheet);
            }
            else
            {
                request = new TimeSheetInsertRequest
                {
                    UserId = UserId,
                };
            }

            request.DayType = dayType;
            request.Status = Model.DTO.Status.Closed;

            if (dayType == DayType.TimeConsumption)
            {
                request.EntryDate = startDate.Date;
                request.StartTime = startDate;
                request.EndTime = endDate;
            }
            else if (dayType == DayType.Vacation)
            {
                request.EntryDate = startDate;
            }

            if (existingTimeSheets.Count > 0)
            {
                timeSheetsAPIService.PutTimeSheet(request.TimeSheetId, request);
            }
            else
            {
                timeSheetsAPIService.PostTimeSheet(request);
            }
        }

        public List<RequestDTO> GetRequests()
        {
            return requestsAPIService.GetRequests(null);
        }
    }
}
