using Autopal.BrainBay.RickandMorty.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Autopal.BrainBay.RickandMorty.Domain
{
    public sealed class RickandMortyContext : DbContext
    {
        public RickandMortyContext(DbContextOptions<RickandMortyContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Location> Locations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Map entity to table
            modelBuilder.Entity<Character>().ToTable("CharacterInfo");
            modelBuilder.Entity<Location>().ToTable("LocationInfo");

            //Configure primary key
            modelBuilder.Entity<Character>().HasKey(s => s.Id);
            modelBuilder.Entity<Location>().HasKey(s => s.Id);

            // configures one-to-many relationship - 
            modelBuilder.Entity<Character>()
                .HasOne(s => s.Location)
                .WithMany(g => g.Residents);

            // configures one-to-many relationship
            modelBuilder.Entity<Character>()
                .HasOne(s => s.Origin);
        }
    }
}