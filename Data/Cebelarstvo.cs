using BeeOrganizer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BeeOrganizer.Data
{
    public class Cebelarstvo : IdentityDbContext<ApplicationUser>
    {
        public Cebelarstvo(DbContextOptions<Cebelarstvo> options) : base(options)
        {
        }

        public DbSet<Cebeljnjak> Cebeljnjaki { get; set; }
        public DbSet<Evidenca> Evidenca { get; set; }
        public DbSet<Drustvo> Drustvo { get; set; }
        public DbSet<Dogodek> Dogodek { get; set; }
        public DbSet<Panj> Panji { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cebeljnjak>().ToTable("Cebeljnjak");
            modelBuilder.Entity<Evidenca>().ToTable("Evidenca");
            modelBuilder.Entity<Drustvo>().ToTable("Drustvo");
            modelBuilder.Entity<Dogodek>().ToTable("Dogodek");
            modelBuilder.Entity<Panj>().ToTable("Panj");
            base.OnModelCreating(modelBuilder);
        }
    }
}