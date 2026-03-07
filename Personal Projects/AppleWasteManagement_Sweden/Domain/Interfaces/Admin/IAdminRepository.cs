using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Admin
{
    public interface IAdminRepository
    {

        Task AddAppleTypeAsync(AppleType type);
        Task AddAppleVarietyAsync(AppleVariety variety);
        Task AddAppleGradeAsync(AppleGrade grade);
        Task AddApplePriceAsync(ApplePrice price);

        Task<List<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);

        Task AddStudentAssignmentAsync(StudentAssignment assignment);

        Task<List<StudentAssignment>> GetStudentAssignmentsAsync(AssignmentStatus? status);
        //Task<AdminMetric> GetMetricsAsync();

        Task<List<CollectionRequest>> GetCollectionRequestsAsync(CollectionStatus? status);

        Task<CollectionRequest?> GetCollectionRequestByIdAsync(Guid requestId);

        Task<bool> IsRequestAlreadyAssignedAsync(Guid requestId);

        Task<bool> AppleTypeExistsAsync(string name);
        Task<bool> AppleVarietyExistsAsync(string name, Guid appleTypeId);
        Task<bool> AppleGradeExistsAsync(string name);
        Task<ApplePrice?> GetApplePriceByIdAsync(Guid id);
        //Task<List<CompanyAppleRequest>> GetCompanyRequestsAsync();
        //Task<CompanyAppleRequest?> GetCompanyRequestByIdAsync(Guid id);
       
        Task<decimal> GetAvailableQuantityByGradeAsync(Guid gradeId);
        Task<List<StudentAvailability>> GetAvailableStudentsByDayAsync(DayOfWeek day, int weekNumber);
        Task AddCompanyPriceAsync(CompanyApplePrice price);
        Task<CompanyApplePrice?> GetCompanyPriceByGradeAsync(Guid gradeId);
        Task<List<CompanyApplePrice>> GetCompanyPricesAsync();
        Task UpdateCollectionRequestAsync(CollectionRequest request);
        Task<List<ApplePrice>> GetOwnerPricesAsync();
        Task<ApplePrice?> GetPriceByVarietyAndGradeAsync(Guid appleVarietyId, Guid appleGradeId);
       Task<CompanyApplePrice?> GetCompanyPriceByVarietyAndGradeAsync(Guid AppleVarietyId,Guid AppleGradeId);
        Task<AdminMetric?> GetMetricsAsync();
        Task<ApplePrice?> GetActivePriceAsync(
    //Guid appleTypeId,
    Guid appleVarietyId,
    Guid appleGradeId
);
        Task AddMetricAsync(AdminMetric metric); 
        Task<AdminMetric?> GetLatestMetricAsync();
        //Task AddMetricAsync(AdminMetric metric);
        void DeleteUser(User user);
        Task<AppleGrade?> GetAppleGradeByIdAsync(Guid id);
        Task<ApplePrice?> GetPriceByGradeAsync(Guid gradeId);


        Task<List<(AppleGrade Grade, decimal AvailableKg)>> GetGradedStockAsync();
        Task<List<(string Grade, string Variety, decimal Kg)>> GetAllGradedStockAsync();

        Task<CompanyAppleRequest?> GetCompanyRequestByIdAsync(Guid id);
        Task<List<CompanyAppleRequest>> GetCompanyRequestsAsync();

        Task<List<(Guid VarietyId, string VarietyName, Guid GradeId, decimal Kg)>>GetGradeAVarietiesAsync();
        Task SaveAsync();

        Task ReduceStockAsync(Guid gradeId, Guid? varietyId, decimal qty);

    }
}
