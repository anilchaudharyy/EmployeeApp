using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrudNoAuthentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrudNoAuthentication.Data
{
    public class EmployeeDbContext : IdentityDbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
       : base(options)
        {
        }

        public DbSet<Employee> Employee {get; set;}
    }
}
