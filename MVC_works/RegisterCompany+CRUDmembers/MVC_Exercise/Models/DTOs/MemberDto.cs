using MVC_Exercise.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Exercise.Models.DTOs
{
    public class MemberDto
    {
       
        public Guid MemberId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Designation { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedDate { get; set; }


        public Guid? CompanyId { get; set; }
    }
}
