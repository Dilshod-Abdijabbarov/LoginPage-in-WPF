using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LoginTask.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=Logindb; Username=postgres; Password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Maruf",
                    Password = "Marufjon07"
                },
                new User
                {
                    Id = 2,
                    Name = "Dilshod",
                    Password = "Dilshodjon07"
                },
                new User
                {
                    Id = 3,
                    Name = "Murod",
                    Password = "Murodjon07"
                },
                 new User
                 {
                     Id = 4,
                     Name = "Aziz",
                     Password = "Azizjon07"
                 });
        }
    }
}
