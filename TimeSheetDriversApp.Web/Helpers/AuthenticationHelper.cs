using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.WebAPI.Models;
using TimeSheetDriversApp.WebAPI.Services;

namespace TimeSheetDriversApp.Web.Helpers
{
    public static class AuthenticationHelper
    {
        private const string LoggedInUserCookie = "loggedin_user";

        public static void SetCurrentUser(this HttpContext context, UserDTO user)
        {

            Context db = context.RequestServices.GetService(typeof(Context)) as Context;

            Guid existingToken = context.Request.GetCookieJson<Guid>(LoggedInUserCookie);
            if (existingToken != null)
            {
                WebAuthToken existingTokenEntry = db.WebAuthTokens.FirstOrDefault(x => x.Value == existingToken);
                if (existingTokenEntry != null)
                {
                    db.WebAuthTokens.Remove(existingTokenEntry);
                    db.SaveChanges();
                }
            }

            if (user != null)
            {

                Guid token = Guid.NewGuid();
                db.WebAuthTokens.Add(new WebAuthToken
                {
                    Value = token,
                    UserId = user.UserId,
                    DateCreated = DateTime.Now
                });
                db.SaveChanges();
                context.Response.SetCookieJson(LoggedInUserCookie, token);

                IUsersService usersService = context.RequestServices.GetService(typeof(IUsersService)) as IUsersService;

                usersService.SetCurrentUser(user);
            }
        }

        public static Guid GetToken(this HttpContext context)
        {
            return context.Request.GetCookieJson<Guid>(LoggedInUserCookie);
        }

        public static UserDTO GetCurrentUser(this HttpContext context)
        {
            Context db = context.RequestServices.GetService(typeof(Context)) as Context;

            Guid token = context.Request.GetCookieJson<Guid>(LoggedInUserCookie);
            if (token == null)
                return null;

            var user = db.WebAuthTokens
                .Where(x => x.Value == token)
                .Include(x => x.User.Role)
                .Select(s => s.User)
                .SingleOrDefault();
            UserDTO mappedUser = null;
            if (user != null)
            {
                IUsersService usersService = context.RequestServices.GetService(typeof(IUsersService)) as IUsersService;
                IMapper mapper = context.RequestServices.GetService(typeof(IMapper)) as IMapper;

                mappedUser = mapper.Map<UserDTO>(user);
                usersService.SetCurrentUser(mappedUser);
            }

            return mappedUser;
        }

    }
}