using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.WebAPI.Models;

namespace TimeSheetDriversApp.WebAPI.Services
{
    public class TimeSheetsService : ITimeSheetsService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IAuditFilesService auditFilesService;
        private readonly IUsersService usersService;

        public TimeSheetsService(Context context, IMapper mapper, IAuditFilesService auditFilesService, IUsersService usersService)
        {
            _context = context;
            _mapper = mapper;
            this.auditFilesService = auditFilesService;
            this.usersService = usersService;
        }

        public List<Model.DTO.TimeSheetDTO> GetTimeSheets(TimeSheetSearchRequest searchRequest)
        {
            var query = _context.TimeSheets
                .Include(x => x.User)
                .AsQueryable();

            var role = usersService.GetCurrentUser().Role.RoleName;
            if (searchRequest != null)
            {
                query = query
                .Where(x => searchRequest.Month == null || x.EntryDate.Month == searchRequest.Month)
                .Where(x => searchRequest.Year == null || x.EntryDate.Year == searchRequest.Year)
                .Where(x => searchRequest.Day == null || x.EntryDate.Day == searchRequest.Day)
                .Where(x => searchRequest.DateFrom == null || x.EntryDate >= searchRequest.DateFrom)
                .Where(x => searchRequest.DateTo == null || x.EntryDate <= searchRequest.DateTo);

                if (searchRequest.Months != null)
                {
                    query = query.Where(x => searchRequest.Months.Contains(x.EntryDate.Month));
                }

                if (role == "Super User" || role == "Admin")
                {
                    if (searchRequest.UserId != null)
                        query = query.Where(x => x.UserId == searchRequest.UserId);

                    if (searchRequest.UserIDs != null)
                        query = query.Where(x => searchRequest.UserIDs.Contains(x.UserId));
                }
            }

            if (role == "User")
                query = query.Where(x => x.UserId == usersService.GetCurrentUser().UserId);

            var list = query.OrderBy(x => x.EntryDate).ToList();
            return _mapper.Map<List<Model.DTO.TimeSheetDTO>>(list);
        }
        public Model.DTO.TimeSheetDTO GetTimeSheet(int id)
        {
            var TimeSheet = _context.TimeSheets
                .Include(x => x.User)
                .Where(x => x.TimeSheetId == id)
                .FirstOrDefault();

            if (TimeSheet == null)
                return null;

            return _mapper.Map<Model.DTO.TimeSheetDTO>(TimeSheet);
        }

        public Model.DTO.TimeSheetDTO PostTimeSheet(TimeSheetInsertRequest TimeSheet)
        {
            var newTimeSheet = _mapper.Map<TimeSheet>(TimeSheet);
            if (newTimeSheet.EntryDate == DateTime.MinValue)
                newTimeSheet.EntryDate = DateTime.Now;

            _context.TimeSheets.Add(newTimeSheet);
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.TimeSheetDTO>(newTimeSheet);
        }
        public Model.DTO.TimeSheetDTO PutTimeSheet(int id, TimeSheetInsertRequest TimeSheet)
        {
            var existingTimeSheet = _context.TimeSheets.Find(id);
            if (existingTimeSheet == null)
                return null;

            var currentUser = usersService.GetCurrentUser();
            int currentUserId = currentUser.UserId;

            if (currentUser.Role.RoleName == "User")
                TimeSheet.UserId = currentUserId;

            if ((TimeSheet.StartTime.HasValue != existingTimeSheet.StartTime.HasValue) ||
            (TimeSheet.StartTime.HasValue && existingTimeSheet.StartTime.HasValue &&
             TimeSheet.StartTime.Value.TimeOfDay != existingTimeSheet.StartTime.Value.TimeOfDay))
            {
                auditFilesService.PostAuditFile(new AuditFileInsertRequest
                {
                    ChangedByUserId = currentUserId,
                    Field = Model.DTO.TimeSheetField.StartTime,
                    TimeSheetId = id,
                    OldValue = existingTimeSheet.StartTime,
                    NewValue = TimeSheet.StartTime
                });
            }

            if ((TimeSheet.EndTime.HasValue != existingTimeSheet.EndTime.HasValue) ||
                (TimeSheet.EndTime.HasValue && existingTimeSheet.EndTime.HasValue &&
                 TimeSheet.EndTime.Value.TimeOfDay != existingTimeSheet.EndTime.Value.TimeOfDay))
            {
                auditFilesService.PostAuditFile(new AuditFileInsertRequest
                {
                    ChangedByUserId = currentUserId,
                    Field = Model.DTO.TimeSheetField.EndTime,
                    TimeSheetId = id,
                    OldValue = existingTimeSheet.EndTime,
                    NewValue = TimeSheet.EndTime
                });
            }
            if ((TimeSheet.BreakTime.HasValue != existingTimeSheet.BreakTime.HasValue) ||
                (TimeSheet.BreakTime.HasValue && existingTimeSheet.BreakTime.HasValue &&
                 TimeSheet.BreakTime.Value.TimeOfDay != existingTimeSheet.BreakTime.Value.TimeOfDay))
            {
                auditFilesService.PostAuditFile(new AuditFileInsertRequest
                {
                    ChangedByUserId = currentUserId,
                    Field = Model.DTO.TimeSheetField.BreakTime,
                    TimeSheetId = id,
                    OldValue = existingTimeSheet.BreakTime,
                    NewValue = TimeSheet.BreakTime
                });
            }

            _mapper.Map(TimeSheet, existingTimeSheet);
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.TimeSheetDTO>(existingTimeSheet);
        }

        public Model.DTO.TimeSheetDTO DeleteTimeSheet(int id)
        {
            var TimeSheet = _context.TimeSheets.Find(id);
            if (TimeSheet == null)
            {
                return null;
            }

            _context.TimeSheets.Remove(TimeSheet);
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.TimeSheetDTO>(TimeSheet);
        }

        public bool TimeSheetExists(int id)
        {
            return _context.TimeSheets.Any(e => e.TimeSheetId == id);
        }

    }
}
