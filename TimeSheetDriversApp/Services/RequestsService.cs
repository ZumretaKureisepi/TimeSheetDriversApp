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
    public class RequestsService : IRequestsService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IUsersService usersService;

        public RequestsService(Context context, IMapper mapper, IUsersService usersService)
        {
            _context = context;
            _mapper = mapper;
            this.usersService = usersService;
        }

        public List<Model.DTO.RequestDTO> GetRequests(RequestSearchRequest req)
        {
           var query = _context.Requests
                    .Include(x => x.User)
                    .AsQueryable();

            var role = usersService.GetCurrentUser().Role.RoleName;
            if(role == "User")
            {
                query = query.Where(x => x.UserId == usersService.GetCurrentUser().UserId);
            }
            else if(req != null && req.UserId != 0) {
                query = query.Where(x => x.UserId == req.UserId);
            }

            var list = query.ToList();
            return _mapper.Map<List<Model.DTO.RequestDTO>>(list);
        }
        public Model.DTO.RequestDTO GetRequest(int id)
        {
            var Request = _context.Requests
                .Include(x => x.User)
                .Where(x => x.RequestId == id)
                .FirstOrDefault();

            if (Request == null)
                return null;

            return _mapper.Map<Model.DTO.RequestDTO>(Request);
        }

        public Model.DTO.RequestDTO PostRequest(RequestInsertRequest Request)
        {
            var newRequest = _mapper.Map<Request>(Request);

            newRequest.DateOfRequest = DateTime.Now;
            newRequest.UserId = usersService.GetCurrentUser().UserId;
            _context.Requests.Add(newRequest);
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.RequestDTO>(newRequest);
        }
        public Model.DTO.RequestDTO PutRequest(int id, RequestInsertRequest Request)
        {
            var existingRequest = _context.Requests.Find(id);
            if (existingRequest == null)
                return null;

            _mapper.Map(Request, existingRequest);
            _context.SaveChanges();
 
            return _mapper.Map<Model.DTO.RequestDTO>(existingRequest);
        }

        public Model.DTO.RequestDTO DeleteRequest(int id)
        {
            var Request = _context.Requests.Find(id);
            if (Request == null)
            {
                return null;
            }

            _context.Requests.Remove(Request);
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.RequestDTO>(Request);
        }

        public bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }

    }
}
