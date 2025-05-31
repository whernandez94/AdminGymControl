using AdminGymControl.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminGymControl.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipPlan> MembershipPlans { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<ClassSession> ClassSessions { get; set; }
        public DbSet<MemberClass> MemberClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MemberClass>()
                .HasKey(mc => new { mc.MemberId, mc.ClassSessionId });

            modelBuilder.Entity<MemberClass>()
                .HasOne(mc => mc.ClassSession)
                .WithMany(c => c.MemberClasses)
                .HasForeignKey(mc => mc.ClassSessionId);

            modelBuilder.Entity<Member>()
        .HasOne(m => m.MembershipPlan)
        .WithMany()
        .HasForeignKey(m => m.MembershipPlanId)
        .OnDelete(DeleteBehavior.SetNull);
        }


    }

}
