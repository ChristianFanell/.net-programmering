using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LabbUppgift3.Models
{
    public class AppDbContext : DbContext
    {
        // constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //poco class references for db
        public DbSet<Errand> Errands { get; set; }

        public DbSet<Status> Status1 { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Sequence> Sequences { get; set; }

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Sample> Samples { get; set; }
            

    }
}
