using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Buyer
{
    public interface IStudentService
    {
        Task<List<StudentAssignment>> GetAssignmentsAsync(Guid studentId);
        Task MarkCollectedAsync(Guid studentId, Guid assignmentId);

        
        Task<List<StudentAssignment>> GetMyAssignmentHistoryAsync(Guid studentId);

        Task SetWeeklyAvailabilityAsync(Guid studentId,List<StudentAvailability> availabilities);

        Task UpdateWeeklyAvailabilityAsync(Guid studentId,List<StudentAvailability> availabilities);


        Task<decimal> GradeCollectionAsync(Guid studentId,Guid assignmentId,List<(Guid VarietyId, Guid GradeId, decimal QuantityKg)> grades);



    }
}
