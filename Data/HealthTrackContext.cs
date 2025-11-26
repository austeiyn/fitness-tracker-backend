using Microsoft.EntityFrameworkCore;
using HealthTrackAPI.Models;

namespace HealthTrackAPI.Data
{
    public class HealthTrackContext : DbContext
    {
        public HealthTrackContext(DbContextOptions<HealthTrackContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);
        }
    }
}