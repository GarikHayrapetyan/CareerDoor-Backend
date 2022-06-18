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
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCandidate> JobCandidate { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }
        public DbSet<JobType> JobType { get; set; }

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

            builder.Entity<UserFollowing>(b =>
            {
                b.HasKey(k => new { k.ObserverId, k.TargetId });

                b.HasOne(o => o.Observer)
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(o => o.Target)
                    .WithMany(f => f.Followers)
                    .HasForeignKey(o => o.TargetId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Job>(j=> {
                j.Property(p => p.Title).HasMaxLength(30);
                j.Property(p => p.Company).HasMaxLength(40);
                j.Property(p => p.Functionality).HasMaxLength(20);
                j.Property(p => p.Industry).HasMaxLength(30);
            });

            builder.Entity<JobCandidate>(x => x.HasKey(aa => new { aa.AppUserId, aa.JobId }));

            builder.Entity<JobCandidate>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.Candidates)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<JobCandidate>()
                .HasOne(u => u.Job)
                .WithMany(a => a.Candidates)
                .HasForeignKey(aa => aa.JobId);
        }
    } 
}
