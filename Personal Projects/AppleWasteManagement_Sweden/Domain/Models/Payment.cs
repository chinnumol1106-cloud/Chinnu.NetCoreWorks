using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Payment
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OwnerId { get; set; } // Householder or Landlord

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; } 

        public Guid CollectionRequestId {  get; set; }


        [ForeignKey(nameof(CollectionRequestId))]
        public CollectionRequest CollectionRequest { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

    }
}
