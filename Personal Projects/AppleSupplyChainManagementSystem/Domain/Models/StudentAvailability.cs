using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentAvailability
    {

        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
        public int WeekNumber { get; set; }   // 🔥 ADD THIS
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(StudentId))]
        public User Student { get; set; }
    }
}
