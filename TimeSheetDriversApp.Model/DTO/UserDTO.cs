using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetDriversApp.Model.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int PersonalNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FMNumber { get; set; }

        public int RoleId { get; set; }
        public RoleDTO Role { get; set; }

        public DateTime EntryDate { get; set; }
        public string Email { get; set; }

        public string Name => FirstName + " " + LastName;

    }
}
