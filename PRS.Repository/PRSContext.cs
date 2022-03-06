using Microsoft.EntityFrameworkCore;
using PRS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Repository
{ 
    public class PRSContext : DbContext
    {
        public PRSContext(DbContextOptions options) : base(options) { }


        public DbSet<User> user { get; set; }
        public DbSet<Contact> contact { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(p => new { p.id });
            modelBuilder.Entity<Contact>().HasKey(p => new { p.id });
        }
    }
}
