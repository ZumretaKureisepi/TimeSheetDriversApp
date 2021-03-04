using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetDriversApp.WebAPI.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options)
            :base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TimeSheet> TimeSheets { get; set; }
        public virtual DbSet<AuditFile> AuditFiles { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<WebAuthToken> WebAuthTokens { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=TimeSheetDriversDB;Trusted_Connection=True;");
            }
        }





    }
}
