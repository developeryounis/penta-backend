using Microsoft.EntityFrameworkCore;
using Penta.Service.Models;
using Penta.Service.Models.ModelConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service
{
    public class PentaEntaties : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UserPrivilege> UserPrivileges { get; set; }

        private readonly string connectionString;

        public PentaEntaties()
        {
            connectionString = "Data Source=.;Initial Catalog=Penta;Integrated Security=True";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PrivilegeConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new UserPrivilegeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
