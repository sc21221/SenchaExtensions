using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SenchaExtensions.Tests
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseInMemoryDatabase(databaseName: "SenchaExtensionsTest");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(MockData.Users());
            modelBuilder.Entity<Office>().HasData(MockData.Offices());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
    }
}
