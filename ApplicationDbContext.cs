
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UfinetPrueba.Domain.Entities;

namespace UfinetPrueba
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
