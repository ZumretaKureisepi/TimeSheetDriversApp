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
    public class AuditFilesService : IAuditFilesService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public AuditFilesService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.DTO.AuditFileDTO> GetAuditFiles(AuditFileSearchRequest request)
        {
            var query = _context.AuditFiles
                .Include(x => x.TimeSheet.User)
                .Include(x => x.ChangedByUser)
                .AsQueryable();

            if(request?.Months != null)
            {
                query = query.Where(x => request.Months.Contains(x.DateChanged.Month));
            }
            if(request?.UserIDs != null)
            {
                query = query.Where(x => request.UserIDs.Contains(x.ChangedByUserId));
            }
            if(request?.Year != null)
            {
                query = query.Where(x => x.DateChanged.Year == request.Year);
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.DTO.AuditFileDTO>>(list);
        }
        public Model.DTO.AuditFileDTO GetAuditFile(int id)
        {
            var AuditFile = _context.AuditFiles
                .Include(x => x.TimeSheet.User)
                .Include(x => x.ChangedByUser)
                .Where(x => x.AuditFileId == id)
                .FirstOrDefault();

            if (AuditFile == null)
                return null;

            return _mapper.Map<Model.DTO.AuditFileDTO>(AuditFile);
        }

        public Model.DTO.AuditFileDTO PostAuditFile(AuditFileInsertRequest AuditFile)
        {
            var newAuditFile = _mapper.Map<AuditFile>(AuditFile);

            if (newAuditFile.DateChanged == DateTime.MinValue)
                newAuditFile.DateChanged = DateTime.Now;

            _context.AuditFiles.Add(newAuditFile);
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.AuditFileDTO>(newAuditFile);
        }
        public Model.DTO.AuditFileDTO PutAuditFile(int id, AuditFileInsertRequest AuditFile)
        {
            var existingAuditFile = _context.AuditFiles.Find(id);
            if (existingAuditFile == null)
                return null;

            _mapper.Map(AuditFile, existingAuditFile);
            _context.SaveChanges();
 
            return _mapper.Map<Model.DTO.AuditFileDTO>(existingAuditFile);
        }

        public Model.DTO.AuditFileDTO DeleteAuditFile(int id)
        {
            var AuditFile = _context.AuditFiles.Find(id);
            if (AuditFile == null)
            {
                return null;
            }

            _context.AuditFiles.Remove(AuditFile);
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.AuditFileDTO>(AuditFile);
        }

        public bool AuditFileExists(int id)
        {
            return _context.AuditFiles.Any(e => e.AuditFileId == id);
        }

    }
}
