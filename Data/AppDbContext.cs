using Microsoft.EntityFrameworkCore;
using CommanderGQL.Models;

namespace CommanderGQL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        // DbContext = database
        // Dbset = the table in the database
        public DbSet<Platform> Platforms { get; set; }
    
        public DbSet<Command> Commands { get; set; }
    

        // config tables by ourselves (custom)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // one platform can have many commands
            modelBuilder
                .Entity<Platform>()
                .HasMany(p => p.Commands)
                .WithOne(p => p.Platform!)
                .HasForeignKey(p => p.PlatformId);

            modelBuilder 
                .Entity<Command>()
                // one command belongs to one platform
                .HasOne(p => p.Platform)
                // many commands can have the same platform
                .WithMany(p => p.Commands)
                .HasForeignKey(p => p.PlatformId);
        }
    }
}