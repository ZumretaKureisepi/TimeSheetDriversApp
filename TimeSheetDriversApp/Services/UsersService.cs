using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.WebAPI.Models;

namespace TimeSheetDriversApp.WebAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private Model.DTO.UserDTO _currentUser;

        public UsersService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.DTO.UserDTO> GetUsers()
        {
            var list = _context.Users
                .Include(x => x.Role)
                .ToList();

            return _mapper.Map<List<Model.DTO.UserDTO>>(list);
        }
        public Model.DTO.UserDTO GetUser(int id)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .Where(x => x.UserId == id)
                .FirstOrDefault();

            if (user == null)
                return null;

            return _mapper.Map<Model.DTO.UserDTO>(user);
        }

        public Model.DTO.UserDTO MyProfile()
        {
            var user = _context.Users
                .Include(x => x.Role)
                .Where(x => x.UserId == GetCurrentUser().UserId)
                .FirstOrDefault();

            if (user == null)
                return null;

            return _mapper.Map<Model.DTO.UserDTO>(user);
        }

        public Model.DTO.UserDTO PostUser(UserInsertRequest user)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            newUser.EntryDate = DateTime.Now;
            newUser.Username =
                newUser.FirstName.Substring(0, 1).ToUpper() +
                newUser.FirstName.Substring(1, 1).ToLower() +
                newUser.LastName.Substring(0, 2).ToLower();

            _context.Users.Add(newUser);
            _context.SaveChanges();

            newUser.PersonalNo = user.RoleId * 1000 + newUser.UserId;
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.UserDTO>(newUser);
        }
        public Model.DTO.UserDTO PutUser(int id, UserUpdateRequest user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
                return null;

            _mapper.Map(user, existingUser);

            if (!string.IsNullOrEmpty(user.NewPassword))
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);

            existingUser.PersonalNo = user.RoleId * 1000 + existingUser.UserId;
            existingUser.Username =
                existingUser.FirstName.Substring(0, 1).ToUpper() +
                existingUser.FirstName.Substring(1, 1).ToLower() +
                existingUser.LastName.Substring(0, 2).ToLower();

            _context.SaveChanges();
 
            return _mapper.Map<Model.DTO.UserDTO>(existingUser);
        }

        public Model.DTO.UserDTO DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return _mapper.Map<Model.DTO.UserDTO>(user);
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
        public void SetCurrentUser(Model.DTO.UserDTO user)
        {
            _currentUser = user;
        }
        public Model.DTO.UserDTO GetCurrentUser()
        {
            return _currentUser;
        }

        public UserDTO Authenticate(string username, string password)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .Where(x => x.Username == username)
                .FirstOrDefault();

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return _mapper.Map<Model.DTO.UserDTO>(user);
        }
    }
}
