﻿using Domain;
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

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }

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

            builder.Entity<Comment>()
                .HasOne(g => g.GetTogether)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);
        }
    } 
}
