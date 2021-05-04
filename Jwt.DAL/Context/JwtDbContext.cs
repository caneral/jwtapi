using AppCore.Entity.Identity;
using Microsoft.EntityFrameworkCore;
using Jwt.DAL.Entities;
using System;
using System.Reflection;

namespace Jwt.DAL.Context
{
    public class JwtDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = Environment.GetEnvironmentVariable("MYSQL_URI");
            //optionsBuilder.UseMySQL(connectionString);
            optionsBuilder.UseMySQL("Server=104.131.162.10; Port=3306;Database=jwt;Uid=root;Pwd=root;convert zero datetime=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppClaim> AppClaims { get; set; }
        public DbSet<AppUserClaim> AppUserClaims { get; set; }

    }
}
