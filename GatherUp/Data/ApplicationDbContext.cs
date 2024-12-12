using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GatherUp.Models;

namespace GatherUp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GatherUp.Models.Event> Event { get; set; } = default!;
        public DbSet<GatherUp.Models.EventFollow> EventFollow { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // store status as string, yet use enums in code
            modelBuilder.Entity<EventInvitationBase>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(), 
                    v => (InvitationStatus)Enum.Parse(typeof(InvitationStatus), v)  
                );
        }
        public DbSet<GatherUp.Models.EventJoinRequest> EventJoinRequest { get; set; } = default!;
    }
}
