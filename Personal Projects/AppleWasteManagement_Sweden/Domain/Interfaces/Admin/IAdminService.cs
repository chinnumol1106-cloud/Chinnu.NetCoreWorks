using Domain.Enum;
using Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Admin
{
    public interface IAdminService
    {
        Task AddAppleTypeAsync(AppleType type);
        Task AddAppleVarietyAsync(AppleVariety variety);
        Task AddAppleGradeAsync(AppleGrade grade);
        Task AddApplePriceAsync(ApplePrice price);
        Task AddCompanyPriceAsync(CompanyApplePrice price);
        Task<List<ApplePrice>> GetOwnerPricesAsync();
        Task<List<CompanyApplePrice>> GetCompanyPricesAsync();
        Task<List<CollectionRequest>> GetCollectionRequestsAsync(CollectionStatus? status);
        Task<object> GetAvailableApplesAsync();

        Task<List<CompanyAppleRequest>> GetCompanyRequestsAsync();
        Task<List<StudentAvailability>> GetAvailableStudentsByDayAsync(DayOfWeek day, int weekNumber);
        Task<List<(AppleGrade Grade, decimal AvailableKg)>> GetGradedStockAsync();

        Task<List<StudentAssignment>> GetStudentAssignmentsAsync(AssignmentStatus? status);

        Task<List<User>> GetUsersAsync();
        Task<AdminMetric> GetMetricsAsync();
        Task UpdateCompanyPriceAsync(Guid gradeId, decimal newPrice);
        Task UpdateApplePriceAsync(Guid priceId, decimal newPrice);
        Task AssignStudentAsync(Guid studentId, Guid requestId);

        Task ApproveCompanyRequestAsync(Guid requestId);
        Task RejectCompanyRequestAsync(Guid requestId);
        Task ConfirmCompanyPaymentAsync(Guid requestId);
        Task BlockUserAsync(Guid userId);
        Task DeleteUserAsync(Guid id);


     
        //Task<List<CompanyAppleRequest>> GetCompanyRequestsAsync();
        //Task ApproveRequestAsync(Guid requestId);
        //Task ConfirmPaymentAsync(Guid requestId);



     

      

      



    }
}
