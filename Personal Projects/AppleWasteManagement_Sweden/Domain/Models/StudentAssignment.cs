using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentAssignment
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public User Student { get; set; }

        public Guid CollectionRequestId { get; set; }

        [ForeignKey(nameof(CollectionRequestId))]
        public CollectionRequest CollectionRequest { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CollectedAt { get; set; }

        public AssignmentStatus Status { get; set; }
    }
}
