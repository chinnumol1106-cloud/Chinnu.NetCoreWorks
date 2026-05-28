using Domain.Data;
using Domain.Enum;
using Domain.Interfaces.Admin;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Admin
{
    public class AdminRepository:IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAppleTypeAsync(AppleType type)
        {
            await _context.AppleTypes.AddAsync(type);
        }

        public async Task AddAppleVarietyAsync(AppleVariety variety)
        {
            await _context.AppleVarieties.AddAsync(variety);
        }

        public async Task AddAppleGradeAsync(AppleGrade grade)
        {
            await _context.AppleGrades.AddAsync(grade);
        }
        public async Task<bool> AppleGradeExistsAsync(string name)
        {
            return await _context.AppleGrades
                .AnyAsync(x => x.Grade.ToLower() == name.ToLower());
        }

        public async Task AddApplePriceAsync(ApplePrice price)
        {
            await _context.ApplePrices.AddAsync(price);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddStudentAssignmentAsync(StudentAssignment assignment)
        {
            await _context.StudentAssignments.AddAsync(assignment);
        }

        //public async Task<AdminMetric> GetMetricsAsync()
        //{
        //    return await _context.AdminMetrics
        //        .OrderByDescending(x => x.CalculatedAt)
        //        .FirstAsync();
        //}



        public async Task<ApplePrice?> GetActivePriceAsync(Guid appleVarietyId,Guid appleGradeId)
        {
            return await _context.ApplePrices
         .FirstOrDefaultAsync(p =>
             //p.AppleTypeId == appleTypeId &&
             p.AppleVarietyId == appleVarietyId &&
             p.AppleGradeId == appleGradeId);
        }



        public async Task<List<CollectionRequest>> GetCollectionRequestsAsync(CollectionStatus? status)
        {
            var query = _context.CollectionRequests
                .Include(r => r.User)
                    .ThenInclude(u => u.Profile)
                .Include(r => r.AppleVariety)
                .AsQueryable();

            // ✅ THIS IS THE FILTER
            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }

            return await query
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }


        public async Task<List<StudentAssignment>> GetStudentAssignmentsAsync(AssignmentStatus? status)
        {
            var query = _context.StudentAssignments
                .Include(a => a.Student)
                .Include(a => a.CollectionRequest)
                    .ThenInclude(r => r.User)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }

            return await query
                .OrderByDescending(a => a.AssignedAt)
                .ToListAsync();
        }


        public async Task<CollectionRequest?> GetCollectionRequestByIdAsync(Guid requestId)
        {
            return await _context.CollectionRequests
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }


        public Task UpdateCollectionRequestAsync(CollectionRequest request)
        {
            _context.CollectionRequests.Update(request);
            return Task.CompletedTask;
        }


        public async Task<bool> IsRequestAlreadyAssignedAsync(Guid requestId)
        {
            return await _context.StudentAssignments
                .AnyAsync(a => a.CollectionRequestId == requestId);
        }


        public async Task<bool> AppleTypeExistsAsync(string name)
        {
            return await _context.AppleTypes
                .AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> AppleVarietyExistsAsync(string name, Guid appleTypeId)
        {
            return await _context.AppleVarieties
                .AnyAsync(x =>
                    x.Name.ToLower() == name.ToLower() &&
                    x.AppleTypeId == appleTypeId);
        }

        

        public async Task<ApplePrice?> GetApplePriceByIdAsync(Guid id)
        {
            return await _context.ApplePrices
                .FirstOrDefaultAsync(p => p.Id == id);
        }



        public async Task<List<StudentAvailability>> GetAvailableStudentsByDayAsync(DayOfWeek day, int weekNumber)
        {
            return await _context.StudentAvailabilities
        .Include(a => a.Student)
            .ThenInclude(s => s.Profile)
        .Where(a =>
            a.DayOfWeek == day &&
            a.WeekNumber == weekNumber &&
            a.IsAvailable)
        .ToListAsync();
        }
        public async Task<AdminMetric?> GetMetricsAsync()
        {
            return await _context.AdminMetrics
         .OrderByDescending(x => x.CalculatedAt)
         .FirstOrDefaultAsync();
        }

        public async Task AddMetricAsync(AdminMetric metric)
        {
            await _context.AdminMetrics.AddAsync(metric);
        }
        public async Task AddCompanyPriceAsync(CompanyApplePrice price)
        {
            await _context.CompanyApplePrices.AddAsync(price);
        }
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }
        public async Task<CompanyApplePrice?> GetCompanyPriceByGradeAsync(Guid gradeId)
        {
            return await _context.CompanyApplePrices
                .FirstOrDefaultAsync(x => x.AppleGradeId == gradeId);
        }
    //    public async Task<ApplePrice?> GetActivePriceAsync(
    //Guid appleTypeId,
    //Guid appleVarietyId,
    //Guid appleGradeId)
    //    {
    //        return await _context.ApplePrices
    //            .FirstOrDefaultAsync(p =>
    //                p.AppleTypeId == appleTypeId &&
    //                p.AppleVarietyId == appleVarietyId &&
    //                p.AppleGradeId == appleGradeId
    //            );
    //    }

        public async Task<List<CompanyApplePrice>> GetCompanyPricesAsync()
        {
            return await _context.CompanyApplePrices
     .Include(x => x.AppleGrade)
     .Include(x => x.AppleVariety) // NEW
     .ToListAsync();
        }
        public async Task<List<ApplePrice>> GetOwnerPricesAsync()
        {
            return await _context.ApplePrices
                .Include(p => p.AppleType)
                .Include(p => p.AppleVariety)
                .Include(p => p.AppleGrade)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }


        public async Task<AdminMetric?> GetLatestMetricAsync()
        {
            return await _context.AdminMetrics
                .OrderByDescending(x => x.CalculatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<AppleGrade?> GetAppleGradeByIdAsync(Guid id)
        {
            return await _context.AppleGrades
                .FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<List<CompanyAppleRequest>> GetCompanyRequestsAsync()
        {
            return await _context.CompanyAppleRequests
          .Include(r => r.Company)
              .ThenInclude(c => c.User)
          .Include(r => r.AppleGrade)
          .Include(r => r.AppleVariety)   
          .OrderByDescending(r => r.CreatedAt)
          .ToListAsync();
        }

        public async Task<CompanyAppleRequest?> GetCompanyRequestByIdAsync(Guid id)
        {
            return await _context.CompanyAppleRequests
                .Include(x => x.Company)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task<List<(AppleGrade Grade, decimal AvailableKg)>> GetGradedStockAsync()
        {
            var grades = await _context.AppleGrades.ToListAsync();
            var result = new List<(AppleGrade, decimal)>();

            foreach (var grade in grades)
            {
                var graded = await _context.CollectionResults
                    .Where(x => x.AppleGradeId == grade.Id)
                    .SumAsync(x => (decimal?)x.QuantityKg) ?? 0;

                var taken = await _context.CompanyAppleRequests
                    .Where(x => x.AppleGradeId == grade.Id &&
                                x.Status == CompanyRequestStatus.Completed)
                    .SumAsync(x => (decimal?)x.QuantityKg) ?? 0;

                result.Add((grade, graded - taken));
            }

            return result;
        }


        public async Task<List<(string Grade, string Variety, decimal Kg)>> GetAllGradedStockAsync()
        {
            return await _context.CollectionResults
                .Include(x => x.AppleGrade)
                .Include(x => x.AppleVariety)
                .GroupBy(x => new
                {
                    x.AppleGrade.Grade,
                    x.AppleVariety.Name
                })
                .Select(g => new ValueTuple<string, string, decimal>(
                    g.Key.Grade,
                    g.Key.Name,
                    g.Sum(x => x.QuantityKg)
                ))
                .ToListAsync();
        }

        public async Task<decimal> GetAvailableQuantityByGradeAsync(Guid gradeId)
        {
            var graded = await _context.CollectionResults
                .Where(x => x.AppleGradeId == gradeId)
                .SumAsync(x => (decimal?)x.QuantityKg) ?? 0;

            var taken = await _context.CompanyAppleRequests
                .Where(x => x.AppleGradeId == gradeId && x.Status != CompanyRequestStatus.Rejected)
                .SumAsync(x => (decimal?)x.QuantityKg) ?? 0;

            return graded - taken;
        }


        public async Task<ApplePrice?> GetPriceByGradeAsync(Guid gradeId)
        {
            return await _context.ApplePrices
                .FirstOrDefaultAsync(x => x.AppleGradeId == gradeId);
        }

       public async Task<ApplePrice?> GetPriceByVarietyAndGradeAsync(Guid appleVarietyId, Guid appleGradeId)
        {
            return await _context.ApplePrices
                .FirstOrDefaultAsync(p =>
                    p.AppleVarietyId == appleVarietyId &&
                    p.AppleGradeId == appleGradeId);
        }

       public async Task<CompanyApplePrice?> GetCompanyPriceByVarietyAndGradeAsync(Guid AppleVarietyId, Guid AppleGradeId)
        {
            return await _context.CompanyApplePrices
              .FirstOrDefaultAsync(p =>
                  p.AppleVarietyId == AppleVarietyId &&
                  p.AppleGradeId == AppleGradeId);
        }





        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }



        public async Task<List<(Guid VarietyId, string VarietyName, Guid GradeId, decimal Kg)>>
GetGradeAVarietiesAsync()
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




        public async Task ReduceStockAsync(Guid gradeId, Guid? varietyId, decimal qty)
        {
            var records = _context.CollectionResults
                .Where(x => x.AppleGradeId == gradeId);

            if (varietyId != null)
                records = records.Where(x => x.AppleVarietyId == varietyId);

            var list = await records.ToListAsync();

            foreach (var r in list)
            {
                if (qty <= 0) break;

                if (r.QuantityKg >= qty)
                {
                    r.QuantityKg -= qty;
                    qty = 0;
                }
                else
                {
                    qty -= r.QuantityKg;
                    r.QuantityKg = 0;
                }
            }
        }


    }
}
