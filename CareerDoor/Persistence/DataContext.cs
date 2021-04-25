using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DbSet<GetTogether> GetTogethers { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
