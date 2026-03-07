using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Householder
{
    public interface IAppleOwnerService
    {
        Task CreateCollectionRequestAsync(Guid ownerId, CollectionRequest request, IFormFile? image);
        Task<List<CollectionRequest>> GetMyRequestsAsync(Guid ownerId);
        Task CancelRequestAsync(Guid ownerId, Guid requestId);
        Task<StudentAssignment?> GetAssignedStudentAsync(Guid ownerId, Guid requestId);


        Task<StudentAssignment?> GetAssignedStudentForRequestAsync(Guid ownerId, Guid requestId);

        Task<(List<CollectionResult> results, Payment? payment)>GetTransparencyDataAsync(Guid ownerId, Guid requestId);


         Task<decimal> GetTotalPaymentAsync(Guid requestId);

        Task<object> GetPaymentDetailsAsync(Guid ownerId);


    }
}
