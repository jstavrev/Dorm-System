using System;
using System.Linq;
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

        public virtual DbSet<SensorDataHistory> SensorsDataHistory { get; set; }

        public virtual DbSet<SensorTypes> SensorTypes { get; set; }

        public virtual DbSet<UserSensors> UserSensors { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserSensors>()
                .HasKey(us => new { us.UserId, us.SensorId });


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
