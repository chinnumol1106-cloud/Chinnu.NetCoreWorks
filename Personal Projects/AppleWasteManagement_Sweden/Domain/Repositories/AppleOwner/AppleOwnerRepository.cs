using Domain.Data;
using Domain.Enum;
using Domain.Interfaces.Householder;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Householder
{
    public class AppleOwnerRepository:IAppleOwnerRepository
    {
        private readonly AppDbContext _context;

        public AppleOwnerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRequestAsync(CollectionRequest request)
        {
            await _context.CollectionRequests.AddAsync(request);
        }

        public async Task<List<CollectionRequest>> GetRequestsByOwnerAsync(Guid ownerId)
        {
            return await _context.CollectionRequests
                .Include(r => r.AppleVariety)
                .Where(r => r.AppleOwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<CollectionRequest?> GetRequestByIdAsync(Guid requestId)
        {
            return await _context.CollectionRequests.FindAsync(requestId);
        }

        public async Task<StudentAssignment?>GetAssignedStudentAsync(Guid ownerId, Guid requestId)
        {
            return await _context.StudentAssignments
                .Include(a => a.Student)
                    .ThenInclude(s => s.Profile)
                .Include(a => a.CollectionRequest)
                .FirstOrDefaultAsync(a =>
                    a.CollectionRequestId == requestId &&
                    a.CollectionRequest.AppleOwnerId == ownerId);
        }

        public async Task<StudentAssignment?> GetAssignedStudentForRequestAsync(Guid ownerId, Guid requestId)
        {
            return await _context.StudentAssignments
                .Include(a => a.Student)
                    .ThenInclude(s => s.Profile)
                .Include(a => a.CollectionRequest)
                .FirstOrDefaultAsync(a =>
                    a.CollectionRequestId == requestId &&
                    a.CollectionRequest.AppleOwnerId == ownerId);
        }

        public async Task<bool> HasOpenRequestForVarietyAsync(Guid ownerId,Guid appleVarietyId)
        {
            return await _context.CollectionRequests.AnyAsync(r =>
                r.AppleOwnerId == ownerId &&
                r.AppleVarietyId == appleVarietyId &&
                (r.Status == CollectionStatus.Requested ||
                 r.Status == CollectionStatus.Assigned ||
                 r.Status == CollectionStatus.Collected)
            );
        }

        public async Task<List<CollectionResult>> GetCollectionResultsAsync(Guid requestId,Guid ownerId)
        {
            return await _context.CollectionResults
                .Include(r => r.CollectionRequest)
                .Where(r =>
                    r.CollectionRequestId == requestId &&
                    r.CollectionRequest.AppleOwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByRequestIdAsync(Guid requestId, Guid ownerId)
        {
            return await _context.Payments
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p =>
                    p.CollectionRequestId == requestId &&
                    p.OwnerId == ownerId);
        }




        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }




        public async Task<List<CollectionResult>> GetResultsByRequestIdAsync(Guid requestId)
        {
            return await _context.CollectionResults
                .Include(x => x.AppleVariety)
                .Where(x => x.CollectionRequestId == requestId)
                .ToListAsync();
        }

        public async Task<Payment?> GetLatestPaymentByOwnerAsync(Guid ownerId)
        {
            return await _context.Payments
                .Where(p => p.OwnerId == ownerId)
                .OrderByDescending(p => p.PaidAt)
                .FirstOrDefaultAsync();
        }

    }
}
