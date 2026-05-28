using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Buyer
{
    public interface IStudentRepository
    {
        Task<List<StudentAssignment>> GetAssignmentsAsync(Guid studentId);
        Task<StudentAssignment?> GetAssignmentWithRequestAsync(Guid assignmentId);
        Task AddResultAsync(CollectionResult result);

        Task<List<StudentAssignment>> GetAssignmentsByStudentAndStatusAsync(Guid studentId,AssignmentStatus status);

        Task<decimal> GetTotalGradedQuantityAsync(Guid assignmentId);

        Task<StudentAvailability?> GetByStudentAndDayAsync(Guid studentId, DayOfWeek day);

        Task DeleteAvailabilityForStudentAsync(Guid studentId, int weekNumber);
        Task AddAvailabilitiesAsync(List<StudentAvailability> availabilities);
        Task<bool> IsAvailabilityExistsAsync(Guid studentId,int weekNumber);
        Task AddAsync(StudentAvailability availability);


        Task<decimal> GetTotalGradedKgAsync(Guid requestId);
        Task<List<(string VarietyName, decimal Kg)>> GetVarietySummaryAsync(Guid requestId);


        Task SaveAsync();
    }
}
