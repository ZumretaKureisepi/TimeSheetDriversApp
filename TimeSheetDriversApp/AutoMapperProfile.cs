using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.WebAPI.Models;

namespace TimeSheetDriversApp.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.User, Model.DTO.UserDTO>().ReverseMap();
            CreateMap<Models.Role, Model.DTO.RoleDTO>().ReverseMap();
            CreateMap<Models.AuditFile, Model.DTO.AuditFileDTO>().ReverseMap();
            CreateMap<Models.Request, Model.DTO.RequestDTO>().ReverseMap();
            CreateMap<Models.TimeSheet, Model.DTO.TimeSheetDTO>().ReverseMap();

            CreateMap<UserInsertRequest, Models.User>();
            CreateMap<UserUpdateRequest, Models.User>();
            CreateMap<AuditFileInsertRequest, Models.AuditFile>();
            CreateMap<RequestInsertRequest, Models.Request>();
            CreateMap<TimeSheetInsertRequest, Models.TimeSheet>();

            CreateMap<Model.DTO.UserDTO, UserUpdateRequest > ();
            CreateMap<Model.DTO.TimeSheetDTO, TimeSheetInsertRequest > ();
            CreateMap<Model.DTO.RequestDTO, RequestInsertRequest > ();
        }
    }
}
