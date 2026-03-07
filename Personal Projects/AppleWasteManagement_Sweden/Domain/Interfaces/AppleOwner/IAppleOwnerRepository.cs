using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Householder
{
    public interface IAppleOwnerRepository
    {
        Task AddRequestAsync(CollectionRequest request);
        Task<List<CollectionRequest>> GetRequestsByOwnerAsync(Guid ownerId);
        Task<CollectionRequest?> GetRequestByIdAsync(Guid requestId);
        Task<StudentAssignment?> GetAssignedStudentAsync(Guid ownerId, Guid requestId);
        Task SaveAsync();

        Task<StudentAssignment?> GetAssignedStudentForRequestAsync(Guid ownerId, Guid requestId);
        Task<bool> HasOpenRequestForVarietyAsync(Guid ownerId, Guid appleVarietyId);

        Task<List<CollectionResult>> GetCollectionResultsAsync(Guid requestId,Guid ownerId);
        Task<Payment?> GetPaymentByRequestIdAsync(Guid requestId, Guid ownerId);




        Task<Payment?> GetLatestPaymentByOwnerAsync(Guid ownerId);
        Task<List<CollectionResult>> GetResultsByRequestIdAsync(Guid requestId);


    }
}
