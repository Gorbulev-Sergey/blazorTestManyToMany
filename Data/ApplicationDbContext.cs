using blazorTestManyToMany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazorTestManyToMany.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<post> posts { get; set; }
        public DbSet<tag> tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
