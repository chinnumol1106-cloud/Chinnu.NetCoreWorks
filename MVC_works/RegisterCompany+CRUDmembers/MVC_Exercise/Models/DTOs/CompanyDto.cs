using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Exercise.Models.DTOs
{
    public class CompanyDto
    {
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }

        public string Email {  get; set; }
        public string Password {  get; set; }

        public string? Vision { get; set; }
        public string? Mission { get; set; }
        public string? About { get; set; }
        public string? Website { get; set; }
    }
}
