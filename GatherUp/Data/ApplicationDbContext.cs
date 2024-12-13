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
            // store status as string, yet use enums in code
            modelBuilder.Entity<EventInvitationBase>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (InvitationStatus)Enum.Parse(typeof(InvitationStatus), v)
                );

            modelBuilder.Entity<EventFollow>()
                 .HasOne(ef => ef.Event)      
                 .WithMany()                  
                 .HasForeignKey(ef => ef.EventId) 
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventInvitationBase>()
                 .HasOne(ef => ef.Event)
                 .WithMany()
                 .HasForeignKey(ef => ef.EventId)
                 .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<GatherUp.Models.EventJoinRequest> EventJoinRequest { get; set; } = default!;
    }
}
