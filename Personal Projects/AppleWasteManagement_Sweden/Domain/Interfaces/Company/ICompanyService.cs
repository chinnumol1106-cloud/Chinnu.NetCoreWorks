using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Company
{
    public interface ICompanyService
    {
        //Task<List<(AppleGrade Grade, decimal AvailableKg)>> GetAvailableGradesAsync();
      

       
        //Task MarkReceivedAsync(Guid companyId, Guid requestId);
     
       
        Task<List<CompanyAppleRequest>> GetMyRequestsAsync(Guid userId);

        Task PayAsync(Guid userId, Guid requestId);

        Task ReceiveAsync(Guid userId, Guid requestId);

        Task<List<CompanyStock>> GetStockAsync(Guid userId);

        Task UsageAsync(Guid userId, Guid gradeId, decimal kg, string purpose);
        Task<List<CompanyApplePrice>> GetPricesAsync();
        Task<object> GetAvailableGradesForCompanyAsync(Guid userid);
        Task RequestApplesAsync(Guid userId, Guid gradeId, Guid? varietyId, decimal quantity);
    }
}
