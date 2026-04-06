using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using TravelDestinationApi.Models;

namespace TravelDestinationApi.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Destination> Destinations => Set<Destination>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.CityName)
                      .IsRequired();

                entity.Property(d => d.Country)
                      .IsRequired();

                entity.Property(d => d.Description)
                      .HasMaxLength(200);

                entity.Property(d => d.Rating)
                      .HasDefaultValue(3);

                entity.Property(d => d.LastVisited)
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
