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
    public class RolesService : IRolesService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public RolesService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.DTO.RoleDTO> GetRoles()
        {
            var list = _context.Roles
                .ToList();

            return _mapper.Map<List<Model.DTO.RoleDTO>>(list);
        }
        public Model.DTO.RoleDTO GetRole(int id)
        {
            var Role = _context.Roles
                .Where(x => x.RoleId == id)
                .FirstOrDefault();

            if (Role == null)
                return null;

            return _mapper.Map<Model.DTO.RoleDTO>(Role);
        }

    }
}
