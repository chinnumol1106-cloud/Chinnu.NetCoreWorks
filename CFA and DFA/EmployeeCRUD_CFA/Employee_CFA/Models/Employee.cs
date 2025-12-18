using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_CFA.Models
{
    internal class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Salary { get; set; }

        public Employee( string name, int salary)
        {
            
            Name = name;
            Salary = salary;
        }
    }
}
