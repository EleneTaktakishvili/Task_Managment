using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagment.Domain.Entities;
using Task = TaskManagment.Domain.Entities.Task;
using TaskStatus = TaskManagment.Domain.Entities.TaskStatus;

namespace TaskManagment.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }   
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserTask>()
                .HasKey(s => new { s.UserId, s.TaskId });

            builder.Entity<UserTask>()
                .HasOne(s => s.User)
                .WithMany(s => s.UserTasks)
                .HasForeignKey(s => s.UserId);

            builder.Entity<UserTask>()
                .HasOne(s => s.Task)
                .WithMany(s => s.UserTasks)
                .HasForeignKey(s => s.TaskId);

            builder.Entity<Task>()
                .Property(t => t.Status)
                .HasConversion(
                v => v.ToString(),
                v => (TaskStatus)Enum.Parse(typeof(TaskStatus), v));


            builder.Entity<Task>()
                .Property(b => b.Status)
                .HasColumnName("TaskStatus")
                .HasConversion<int>();

            #region Seeding data

            //Seed roles           
            Guid userRoleId = Guid.Parse("944FD298-3B51-4252-AFFA-989FC68B03CF");
            Guid supportRoleId = Guid.Parse("DD3B6B5D-00AC-4EA0-B80B-022DE8179A4C");

            builder.Entity<Role>().HasData(
                new Role { Id = userRoleId, Name = "User", NormalizedName = "USER" },
                new Role { Id = supportRoleId, Name = "Support", NormalizedName = "SUPPORT" }
            );

            // Seed users
            var userId = Guid.Parse("FD9119B8-3CC7-4DCE-A521-A132A0A9F206");
            var userPasswordHash = new PasswordHasher<User>().HashPassword(null, "UserPassword123!");

            var supportUserId = Guid.Parse("24B05C76-71F5-4154-AA4F-1ECB8F8934DF");
            var supportPasswordHash = new PasswordHasher<User>().HashPassword(null, "SupportPassword123!");

            builder.Entity<User>().HasData(
                new User
                {
                    Id = userId,
                    UserName = "etaktakishvili",
                    NormalizedUserName = "ETAKTAKISHVILI",
                    Email = "eltaktakishvili@gmail.com",
                    NormalizedEmail = "ELTAKTAKISHVILI@GMAIL.COM",
                    PasswordHash = userPasswordHash,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                 new User
                 {
                     Id = supportUserId,
                     UserName = "support",
                     NormalizedUserName = "SUPPORT",
                     Email = "support@gmail.com",
                     NormalizedEmail = "SUPPORT@GMAIL.COM",
                     PasswordHash = supportPasswordHash,
                     SecurityStamp = Guid.NewGuid().ToString()
                 }
            );

            // Seed user roles
            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid> { UserId = userId, RoleId = userRoleId },
                new IdentityUserRole<Guid> { UserId = supportUserId, RoleId = supportRoleId }
                );

            #endregion
        }

    }
}
