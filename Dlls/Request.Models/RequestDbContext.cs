using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Request.Models
{
    public class RequestDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OVQ9BFI;Database=QLC;Trusted_Connection=True;");
        }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}
