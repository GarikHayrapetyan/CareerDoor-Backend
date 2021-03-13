using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<GetTogether> GetTogethers { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
