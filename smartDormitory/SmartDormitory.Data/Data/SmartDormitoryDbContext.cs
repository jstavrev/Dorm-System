using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMovieStore.Models.Contracts;
using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Data.Data
{
    public class SmartDormitoryDbContext : IdentityDbContext<User>
    {


        public SmartDormitoryDbContext(DbContextOptions<SmartDormitoryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sensor> Sensors { get; set; }

        public virtual DbSet<SensorTypes> SensorTypes { get; set; }

        public virtual DbSet<UserSensors> UserSensors { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserSensors>()
                .HasKey(us => us.Id );

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "Admin", Id = 1.ToString(), NormalizedName = "Admin".ToUpper(), ConcurrencyStamp = "aaa" });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "User", Id = 2.ToString(), NormalizedName = "User".ToUpper(), ConcurrencyStamp = "bbb" });

            var adminUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "ICBAdmin",
                NormalizedUserName = "ICBAdmin".ToUpper(),
                Email = "ICBAdmin@mail.com",
                NormalizedEmail = "ICBAdmin@mail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+55555",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            var hashePass = new PasswordHasher<User>().HashPassword(adminUser, "!Password2018");
            adminUser.PasswordHash = hashePass;

            builder.Entity<User>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = 1.ToString(),
                UserId = adminUser.Id
            });

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
