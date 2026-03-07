using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

        public string County { get; set; }     // ✅ Län (e.g., Halland)
        public string ZipCode { get; set; }    // ✅ Postnummer

        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
