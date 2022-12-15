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
        public DbSet<Odhodek> Odhodki { get; set; }
        public DbSet<Prihodek> Prihodki { get; set; }
        public DbSet<Panj> Panji { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cebeljnjak>().ToTable("Cebeljnjak");
            modelBuilder.Entity<Odhodek>().ToTable("Odhodek");
            modelBuilder.Entity<Prihodek>().ToTable("Prihodek");
            modelBuilder.Entity<Panj>().ToTable("Panj");
            base.OnModelCreating(modelBuilder);
        }
    }
}