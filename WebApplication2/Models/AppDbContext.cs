using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "48e92e17-e670-48b0-904b-b6bc8659c319",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = "AQAAAAIAAYagAAAAELe9TBNkxNVL+rw2vdoPh4Yht3yQqrZDEFZ0fT3ctJ3be6ks9vrxYXV8nBNHs2LxJQ==",
                    SecurityStamp = "2ZPZEYXZSQODKPPTAGMEYMVOOSF6NSQJ",
                    ConcurrencyStamp = "31af1032-fe0d-4fc4-8032-2fcb5491c31c",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "26696aab-db9e-43f8-9d98-a48718466d03",
                    Name = "boss",
                    NormalizedName = "BOSS"
                },
                new IdentityRole
                {
                    Id = "45533c1a-a06f-421f-981b-38b2ee1b1c0f",
                    Name = "employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "47bb83d2-add0-4690-94d5-8278c3f6df84",
                    Name = "admin",
                    NormalizedName = "ADMIN"
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "47bb83d2-add0-4690-94d5-8278c3f6df84",
                    UserId = "48e92e17-e670-48b0-904b-b6bc8659c319"
                });

            //modelBuilder.Entity<IdentityUser>().HasData(
            //    new IdentityUser { }
            //    );
            //modelBuilder.Entity<Employee>().HasData(
            //        new Employee("Vadim", "Podlipny", "Pavlovich", "OASU", "Operator EVM", "Kreerenko")
            //        {
            //            Id = 1,
            //            BossId = null
            //        }
            //);
        }
    }
}
