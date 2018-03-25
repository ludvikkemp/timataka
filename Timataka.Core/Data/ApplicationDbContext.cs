using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<UserInClub> UsersInClubs { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionInstance> CompetitionInstances { get; set; }
        public DbSet<ManagesCompetition> ManagesCompetitions { get; set; }
        public DbSet<Heat> Heats { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ContestantInHeat> ContestantsInHeats { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DevicesInEvent> DevicesInEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<UserInClub>()
                .HasKey(c => new { c.UserId, c.SportId });

            builder.Entity<ManagesCompetition>()
                .HasKey(m => new {m.UserId, m.CompetitionId});

            builder.Entity<ContestantInHeat>()
                .HasKey(c => new { c.UserId, c.HeatId });

            builder.Entity<Marker>()
                .HasKey(m => new { m.EventId, m.HeatId });

            builder.Entity<DevicesInEvent>()
                .HasKey(d => new { d.EventId, d.DeviceId });
        }
    }
}
