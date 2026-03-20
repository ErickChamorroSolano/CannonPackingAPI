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
                .HasIndex(u => u.ItemCode)
                .IsUnique();

            modelBuilder.Entity<Box>()
                .HasIndex(b => b.BoxCode)
                .IsUnique();

            modelBuilder.Entity<BoxTowel>()
                .HasOne(bi => bi.Box)
                .WithMany(b => b.BoxTowels)
                .HasForeignKey(bi => bi.BoxId);

            modelBuilder.Entity<BoxTowel>()
                .HasOne(bi => bi.towel)
                .WithMany(u => u.BoxTowels)
                .HasForeignKey(bi => bi.TowelId);

            modelBuilder.Entity<BoxTowel>()
                .HasIndex(bi => bi.TowelId)
                .HasFilter("IsActive = 1")
                .IsUnique();
        }
    }
}
