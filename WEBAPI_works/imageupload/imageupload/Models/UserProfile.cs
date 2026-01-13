using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace imageupload.Models
{
    public class UserProfile
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        
        [Required, StringLength(300)]
        public string Address { get; set; }

        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }


        [StringLength(500)]
        public string Bio { get; set; }

    
        public string ProfileImagePath { get; set; }

    }
}
