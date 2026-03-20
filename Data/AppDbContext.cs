using CannonPackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CannonPackingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Towel> Towels => Set<Towel>();
        public DbSet<Box> Boxes => Set<Box>();
        public DbSet<BoxTowel> BoxTowels => Set<BoxTowel>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Towel>()
                .HasIndex(t => t.ItemCode)
                .IsUnique();

            modelBuilder.Entity<Box>()
                .HasIndex(b => b.BoxCode)
                .IsUnique();

            modelBuilder.Entity<Towel>()
                .HasOne(t => t.Box)
                .WithMany(b => b.Towels)
                .HasForeignKey(t => t.BoxId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
