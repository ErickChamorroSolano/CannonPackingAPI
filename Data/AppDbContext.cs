using CannonPackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CannonPackingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Towel> Towel => Set<Towel>();
        public DbSet<Box> Box => Set<Box>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ItemCode único
            modelBuilder.Entity<Towel>()
                .HasIndex(t => t.ItemCode)
                .IsUnique();

            // BoxCode único
            modelBuilder.Entity<Box>()
                .HasIndex(b => b.BoxCode)
                .IsUnique();

            // Relación Box - Towel (1 a muchos)
            modelBuilder.Entity<Towel>()
                .HasOne(t => t.Box)
                .WithMany(b => b.Towels)
                .HasForeignKey(t => t.BoxId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de campos
            modelBuilder.Entity<Towel>()
                .Property(t => t.ProductCode)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Box>()
                .Property(b => b.ProductCode)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Box>()
                .Property(b => b.Capacity)
                .HasColumnType("smallint");

            // guardar status como string
            modelBuilder.Entity<Towel>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Box>()
                .Property(b => b.Status)
                .HasConversion<string>();
        }
    }
}
