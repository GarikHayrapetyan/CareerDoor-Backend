using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<GetTogether> GetTogethers { get; set; }

        public DbSet<GetTogetherAttendee> GetTogetherAttendees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GetTogetherAttendee>(x => x.HasKey(aa => new { aa.AppUserID, aa.GetTogetherId }));

            builder.Entity<GetTogetherAttendee>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.GetTogethers)
                .HasForeignKey(aa => aa.AppUserID);

            builder.Entity<GetTogetherAttendee>()
                .HasOne(u => u.GetTogether)
                .WithMany(a => a.Attendees)
                .HasForeignKey(aa => aa.GetTogetherId);
        }
    } 
}
