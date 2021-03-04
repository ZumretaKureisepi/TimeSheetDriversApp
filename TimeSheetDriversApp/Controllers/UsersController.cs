using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.WebAPI.Models;
using TimeSheetDriversApp.WebAPI.Services;

namespace TimeSheetDriversApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<List<Model.DTO.UserDTO>> GetUsers()
        {
            return _usersService.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<Model.DTO.UserDTO> GetUser(int id)
        {
            if (!_usersService.UserExists(id))
                return NotFound();

            return _usersService.GetUser(id);
        }

        // GET: api/Users/MyProfile
        [HttpGet("MyProfile")]
        [Authorize]
        public ActionResult<Model.DTO.UserDTO> MyProfile()
        {
            return _usersService.MyProfile();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult<Model.DTO.UserDTO> PutUser(int id, UserUpdateRequest user)
        {
            if (id != user.UserId)
                return BadRequest();

            if (!_usersService.UserExists(id))
                return NotFound();

            return _usersService.PutUser(id, user);
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<Model.DTO.UserDTO> PostUser(UserInsertRequest user)
        {
            var newUser = _usersService.PostUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.UserId }, newUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ActionResult<Model.DTO.UserDTO> DeleteUser(int id)
        {
            if (!_usersService.UserExists(id))
                return NotFound();

            return _usersService.DeleteUser(id);
        }


    }
}
