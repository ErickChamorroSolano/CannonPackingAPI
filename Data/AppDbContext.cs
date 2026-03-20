using CannonPackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CannonPackingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Towel> Towel => Set<Towel>();
        public DbSet<Box> Box => Set<Box>();
        public DbSet<BoxTowel> BoxTowel => Set<BoxTowel>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Towel>()
                .HasIndex(t => t.ItemCode)
                .IsUnique();

            modelBuilder.Entity<Box>()
                .HasIndex(b => b.BoxCode)
                .IsUnique();

            // relacion BoxTowel - Box
            modelBuilder.Entity<BoxTowel>()
                .HasOne(bt => bt.Box)
                .WithMany(b => b.BoxTowels)
                .HasForeignKey(bt => bt.BoxId)
                .OnDelete(DeleteBehavior.Restrict);

            // relacion BoxTowel - Towel
            modelBuilder.Entity<BoxTowel>()
                .HasOne(bt => bt.Towel)
                .WithMany(t => t.BoxTowels)
                .HasForeignKey(bt => bt.TowelId)
                .OnDelete(DeleteBehavior.Restrict);

            // evitar duplicados
            modelBuilder.Entity<BoxTowel>()
                .HasIndex(bt => new { bt.BoxId, bt.TowelId })
                .HasFilter("IsActive = 1")
                .IsUnique();
        }
    }
}
