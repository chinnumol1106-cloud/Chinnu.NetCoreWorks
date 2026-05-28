using Domain.Data;
using Domain.Enum;
using Domain.Interfaces.Buyer;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Repositories.Buyer
{
    public class StudentRepository:IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentAssignment>> GetAssignmentsAsync(Guid studentId)
        {
            return await _context.StudentAssignments
        .Include(a => a.CollectionRequest)
            .ThenInclude(r => r.User)
                .ThenInclude(u => u.Profile)
        .Include(a => a.CollectionRequest)
            .ThenInclude(r => r.AppleVariety)
        .Where(a => a.StudentId == studentId
                 && a.Status == AssignmentStatus.Assigned)
        .ToListAsync();
        }

        public async Task<StudentAssignment?> GetAssignmentWithRequestAsync(Guid assignmentId)
        {
            return await _context.StudentAssignments
                .Include(a => a.CollectionRequest)
                    .ThenInclude(r => r.User)
                .Include(a => a.CollectionRequest)
                    .ThenInclude(r => r.AppleVariety)
                     .ThenInclude(v => v.AppleType)   // ⭐ THIS LINE IS IMPORTANT
                .FirstOrDefaultAsync(a => a.Id == assignmentId);
        }

        public async Task AddResultAsync(CollectionResult result)
        {
            await _context.CollectionResults.AddAsync(result);
        }

        public async Task<List<StudentAssignment>> GetAssignmentsByStudentAndStatusAsync(Guid studentId, AssignmentStatus status)
        {
            return await _context.StudentAssignments
                .Include(a => a.CollectionRequest)
                    .ThenInclude(r => r.User)
                .Include(a => a.CollectionRequest)
                    .ThenInclude(r => r.AppleVariety)
                .Where(a => a.StudentId == studentId && a.Status == status)
                .OrderByDescending(a => a.CollectedAt)
                .ToListAsync();
        }


        public async Task<StudentAvailability?> GetByStudentAndDayAsync(Guid studentId, DayOfWeek day)
        {
            return await _context.StudentAvailabilities
                .FirstOrDefaultAsync(a =>
                    a.StudentId == studentId &&
                    a.DayOfWeek == day);
        }

        public async Task AddAsync(StudentAvailability availability)
        {
            await _context.StudentAvailabilities.AddAsync(availability);
        }

        public async Task<bool> IsAvailabilityExistsAsync(Guid studentId,int weekNumber)
        {
            return await _context.StudentAvailabilities
                .AnyAsync(a =>
                    a.StudentId == studentId &&
                    a.WeekNumber == weekNumber);
        }
        public async Task<decimal> GetTotalGradedQuantityAsync(Guid assignmentId)
        {
            return await _context.CollectionResults
                .Where(x => x.CollectionRequestId == assignmentId)
                .SumAsync(x => (decimal?)x.QuantityKg) ?? 0;
        }

        public async Task DeleteAvailabilityForStudentAsync(Guid studentId, int weekNumber)
        {
            var existing = await _context.StudentAvailabilities
                .Where(a => a.StudentId == studentId && a.WeekNumber == weekNumber)
                .ToListAsync();

            _context.StudentAvailabilities.RemoveRange(existing);
        }

        public async Task AddAvailabilitiesAsync(List<StudentAvailability> availabilities)
        {
            await _context.StudentAvailabilities.AddRangeAsync(availabilities);
        }


        public async Task<decimal> GetTotalGradedKgAsync(Guid requestId)
        {
            return await _context.CollectionResults
                .Where(x => x.CollectionRequestId == requestId)
                .SumAsync(x => (decimal?)x.QuantityKg) ?? 0;
        }

        public async Task<List<(string VarietyName, decimal Kg)>>GetVarietySummaryAsync(Guid requestId)
        {
            return await _context.CollectionResults
                .Where(x => x.CollectionRequestId == requestId)
                .Include(x => x.AppleVariety)
                .GroupBy(x => x.AppleVariety.Name)
                .Select(g => new ValueTuple<string, decimal>(
                    g.Key,
                    g.Sum(x => x.QuantityKg)))
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

      

    }
}
