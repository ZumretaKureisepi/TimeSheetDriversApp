using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeSheetDriversApp.Model.Requests
{
    public class UserInsertRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "FM number")]
        public int FMNumber { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }
    }
}
