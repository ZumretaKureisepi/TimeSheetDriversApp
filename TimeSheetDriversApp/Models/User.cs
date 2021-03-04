using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetDriversApp.WebAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int PersonalNo { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FMNumber { get; set; }

        public Role Role { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }

        public DateTime EntryDate { get; set; }

    }
}
