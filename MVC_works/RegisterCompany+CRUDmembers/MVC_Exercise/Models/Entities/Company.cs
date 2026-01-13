using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Exercise.Models.Entities
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? Phone {  get; set; }
        [Required]
        [EmailAddress]
        public string Email {  get; set; }
        public string? Password {  get; set; }

        public string? Vision { get; set; }
        public string? Mission { get; set; }
        public string? About { get; set; }
        public string? Website { get; set; }

        public virtual ICollection<Member> Members { get; set; }


    }
}
