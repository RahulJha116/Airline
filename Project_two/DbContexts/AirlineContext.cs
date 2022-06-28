using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project_two.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project_two.DbContexts
{
    public class AirlineContext : DbContext
    {
        public AirlineContext()
        {
        }

        public AirlineContext(DbContextOptions<AirlineContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("Project_twoDB");
                optionsBuilder.UseSqlServer(connectionString);

            }

        }

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    adminId = 1,
                    adminName = "Admin1",
                    adminEmailId = "Admin1",
                    adminPasskey = "Admin1"
                },
                new Admin
                {
                    adminId = 2,
                    adminName = "Admin2",
                    adminEmailId = "Admin2",
                    adminPasskey = "Admin2"
                });

        }
    }
}
