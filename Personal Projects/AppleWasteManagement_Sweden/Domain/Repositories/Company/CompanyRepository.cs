using Domain.Data;
using Domain.Enum;
using Domain.Interfaces.Company;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Company
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyEntity?> GetCompanyByUserIdAsync(Guid userId)
        {
            return await _context.CompanyEntities
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

       
      



       

        //  AVAILABLE GRADES 
        public async Task<List<(AppleGrade Grade, decimal AvailableKg)>> GetAvailableGradesWithQuantityAsync()
        {
            var grades = await _context.AppleGrades.ToListAsync();
            var result = new List<(AppleGrade, decimal)>();

            foreach (var grade in grades)
            {
                var totalGraded = await _context.CollectionResults
                    .Where(x => x.AppleGradeId == grade.Id)
                    .SumAsync(x => (decimal?)x.QuantityKg) ?? 0;

                //var totalTaken = await _context.CompanyAppleRequests
                //    .Where(x => x.AppleGradeId == grade.Id &&
                //                x.Status == CompanyRequestStatus.Completed)
                //    .SumAsync(x => (decimal?)x.QuantityKg) ?? 0;

                result.Add((grade, totalGraded));
            }

            return result;
        }

        public async Task<List<CompanyApplePrice>> GetPricesAsync()
        {
            return await _context.CompanyApplePrices
                .Include(x => x.AppleGrade)
                .Include(x => x.AppleVariety)
                .ToListAsync();
        }

        public async Task<CompanyApplePrice?> GetPriceByGradeAsync(Guid gradeId)
        {
            return await _context.CompanyApplePrices
                .FirstOrDefaultAsync(x => x.AppleGradeId == gradeId && x.AppleVarietyId == null);
        }

        public async Task<CompanyApplePrice?> GetPriceByVarietyAndGradeAsync(Guid varietyId, Guid gradeId)
        {
            return await _context.CompanyApplePrices
                .FirstOrDefaultAsync(x =>
                    x.AppleVarietyId == varietyId &&
                    x.AppleGradeId == gradeId);
        }

       
        public async Task AddRequestAsync(CompanyAppleRequest request)
        {
            await _context.CompanyAppleRequests.AddAsync(request);
        }

        public async Task<List<CompanyAppleRequest>> GetRequestsByCompanyAsync(Guid companyId)
        {
            return await _context.CompanyAppleRequests
                .Include(x => x.AppleGrade)
                .Include(x => x.AppleVariety)
                .Where(x => x.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<CompanyAppleRequest?> GetRequestByIdAsync(Guid id)
        {
            return await _context.CompanyAppleRequests
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CompanyStock>> GetStockAsync(Guid companyId)
        {
            return await _context.CompanyStocks
                .Include(x => x.AppleGrade)
                .Where(x => x.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<CompanyStock?> GetStockItemAsync(Guid companyId, Guid gradeId)
        {
            return await _context.CompanyStocks
                .FirstOrDefaultAsync(x =>
                    x.CompanyId == companyId &&
                    x.AppleGradeId == gradeId);
        }

        public async Task AddStockAsync(CompanyStock stock)
        {
            await _context.CompanyStocks.AddAsync(stock);
        }

       
        public async Task<List<(Guid VarietyId, string VarietyName, Guid GradeId, decimal Kg)>> GetGradeAVarietiesAsync()
        {
            return await _context.CollectionResults
                .Include(x => x.AppleVariety)
                .Include(x => x.AppleGrade)
                .Where(x => x.AppleGrade.Grade == "A")
                .GroupBy(x => new { x.AppleVarietyId, x.AppleVariety.Name, x.AppleGradeId })
                .Select(g => new ValueTuple<Guid, string, Guid, decimal>(
                    g.Key.AppleVarietyId,
                    g.Key.Name,
                    g.Key.AppleGradeId,
                    g.Sum(x => x.QuantityKg)
                ))
                .ToListAsync();
        }

        public async Task<AppleGrade?> GetGradeByIdAsync(Guid gradeId)
        {
            return await _context.AppleGrades
                .FirstOrDefaultAsync(x => x.Id == gradeId);
        }

        public async Task<List<CollectionResult>> GetAllCollectionResultsAsync()
        {
            return await _context.CollectionResults
                .Include(x => x.AppleGrade)
                .Include(x => x.AppleVariety)
                .ToListAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
