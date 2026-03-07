using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces.Householder;
using Domain.Interfaces.Pay;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Householder
{
    public class AppleOwnerService : IAppleOwnerService
    {


        private readonly IAppleOwnerRepository _repo;
        private readonly IPaymentRepository _paymentRepo;

        public AppleOwnerService(IAppleOwnerRepository repo, IPaymentRepository paymentRepo)
        {
            _repo = repo;
            _paymentRepo = paymentRepo;
        }

        public async Task CreateCollectionRequestAsync(Guid ownerId, CollectionRequest request, IFormFile? image)
        {

            //  BASIC VALIDATIONS
            if (request.AppleVarietyId == Guid.Empty)
                throw new BusinessException("Apple variety is required");

            if (request.QuantityKg <= 0)
                throw new BusinessException("Quantity must be greater than zero");

            if (request.PreferredPickupAt == null)
                throw new BusinessException("Preferred pickup date is required");

            if (request.PreferredPickupAt < DateTime.UtcNow)
                throw new BusinessException("Pickup date cannot be in the past");


            //  TIME VALIDATION
            var pickupTime = request.PreferredPickupAt.Value.TimeOfDay;
            var startTime = new TimeSpan(10, 0, 0);
            var endTime = new TimeSpan(15, 0, 0);

            if (pickupTime < startTime || pickupTime > endTime)
                throw new BusinessException("Pickup time allowed only between 10 AM and 3 PM");


            if (request.WeekNumber < 1 || request.WeekNumber > 53)
                throw new BusinessException("Invalid week number");




            // PREVENT DUPLICATE OPEN REQUEST
            var hasOpenRequest = await _repo.HasOpenRequestForVarietyAsync(
        ownerId, request.AppleVarietyId);

            if (hasOpenRequest)
                throw new BusinessException(
                    "You already have an active request for this apple variety");




            request.AppleOwnerId = ownerId;
            request.Status = CollectionStatus.Requested;

            //  HANDLE IMAGE
            if (image != null && image.Length > 0)
            {
                // Size limit (5MB)
                if (image.Length > 5 * 1024 * 1024)
                    throw new BusinessException("Image size must be less than 5MB");

                // Allowed extensions
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(image.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    throw new BusinessException("Only JPG and PNG images are allowed");

                var uploadsFolder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                request.ImageUrl = $"/uploads/{fileName}";
            }

            await _repo.AddRequestAsync(request);
            await _repo.SaveAsync();
        }

        public async Task<List<CollectionRequest>> GetMyRequestsAsync(Guid ownerId)
        {
            return await _repo.GetRequestsByOwnerAsync(ownerId);
        }

        public async Task CancelRequestAsync(Guid ownerId, Guid requestId)
        {
            var request = await _repo.GetRequestByIdAsync(requestId);

            if (request == null)
                throw new BusinessException("Request not found");

            if (request.AppleOwnerId != ownerId)
                throw new BusinessException("Unauthorized");

            if (request.Status != CollectionStatus.Requested)
                throw new BusinessException("Cannot cancel request now");

            request.Status = CollectionStatus.Cancelled;
            await _repo.SaveAsync();
        }

        public async Task<StudentAssignment?>GetAssignedStudentAsync(Guid ownerId, Guid requestId)
        {
            return await _repo.GetAssignedStudentAsync(ownerId, requestId);
        }





        public async Task<StudentAssignment?> GetAssignedStudentForRequestAsync(Guid ownerId, Guid requestId)
        {
            return await _repo.GetAssignedStudentForRequestAsync(ownerId, requestId);
        }




        public async Task<(List<CollectionResult> results, Payment? payment)>
        GetTransparencyDataAsync(Guid ownerId, Guid requestId)
        {
            var results = await _repo.GetCollectionResultsAsync(requestId, ownerId);

            if (!results.Any())
                return (new List<CollectionResult>(), null);

            var payment = await _repo.GetPaymentByRequestIdAsync(requestId, ownerId);

            return (results, payment);
        }





        public async Task<object> GetPaymentDetailsAsync(Guid ownerId)
        {
            var payment = await _repo.GetLatestPaymentByOwnerAsync(ownerId);

            if (payment == null)
                throw new BusinessException("Payment details not available yet");

            var results = await _repo.GetResultsByRequestIdAsync(payment.CollectionRequestId);

            var varieties = results
                .GroupBy(x => x.AppleVariety.Name)
                .Select(g => new
                {
                    Variety = g.Key,
                    QuantityKg = g.Sum(x => x.QuantityKg)
                })
                .ToList();

            return new
            {
                TotalAmount = payment.Amount,
                Varieties = varieties
            };
        }


        public async Task<decimal> GetTotalPaymentAsync(Guid requestId)
        {
            return await _paymentRepo.GetTotalPaymentByRequestAsync(requestId);
        }


    }
}
