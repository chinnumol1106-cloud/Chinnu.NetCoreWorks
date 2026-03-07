using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Company
{
    public interface ICompanyRepository
    {


        Task AddRequestAsync(CompanyAppleRequest request);

        Task<CompanyApplePrice?> GetPriceByGradeAsync(Guid gradeId);

        Task<CompanyApplePrice?> GetPriceByVarietyAndGradeAsync(Guid varietyId, Guid gradeId);
        Task<AppleGrade?> GetGradeByIdAsync(Guid gradeId);
        Task<List<(Guid VarietyId, string VarietyName, Guid GradeId, decimal Kg)>> GetGradeAVarietiesAsync();

        Task<List<CompanyApplePrice>> GetPricesAsync();
        Task<List<(AppleGrade Grade, decimal AvailableKg)>> GetAvailableGradesWithQuantityAsync();
        Task<List<CompanyStock>> GetStockAsync(Guid companyId);
        Task AddStockAsync(CompanyStock stock);
        Task<CompanyStock?> GetStockItemAsync(Guid companyId, Guid gradeId);
        Task<CompanyAppleRequest?> GetRequestByIdAsync(Guid id);
        Task<List<CompanyAppleRequest>> GetRequestsByCompanyAsync(Guid companyId);

        Task<CompanyEntity?> GetCompanyByUserIdAsync(Guid userId);
        Task<List<CollectionResult>> GetAllCollectionResultsAsync();
        Task SaveAsync();

    }
}
