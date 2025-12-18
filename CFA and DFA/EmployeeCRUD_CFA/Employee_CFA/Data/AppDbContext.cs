using Employee_CFA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_CFA.Data
{
    internal class AppDbContext:DbContext
    {
       public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-4BTVKFHO;Initial Catalog=EMPLOYEE_CFA;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
