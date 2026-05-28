using Domain.Enum;
using Domain.Exceptions;
using Domain.Helper;
using Domain.Interfaces.Admin;
using Domain.Interfaces.Buyer;
using Domain.Interfaces.Email;
using Domain.Interfaces.Pay;
using Domain.Models;

using Domain.Repositories.Buyer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Services.Buyer
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _repo;

        private readonly IAdminRepository _priceRepo;
        private readonly IPaymentRepository _paymentRepo;

        private readonly IEmailService _emailService;

        public StudentService(IStudentRepository repo, IAdminRepository priceRepo,IPaymentRepository paymentRepo, IEmailService emailService)
        {
            _repo = repo;
            _priceRepo = priceRepo;
            _paymentRepo = paymentRepo;
            _emailService = emailService;
        }

        public async Task<List<StudentAssignment>> GetAssignmentsAsync(Guid studentId)
        {
            return await _repo.GetAssignmentsAsync(studentId);
        }

        public async Task MarkCollectedAsync(Guid studentId, Guid assignmentId)
        {
            var assignment = await _repo.GetAssignmentWithRequestAsync(assignmentId);

            if (assignment == null)
                throw new BusinessException("Assignment not found");

            if (assignment.StudentId != studentId)
                throw new BusinessException("Unauthorized access");

            assignment.Status = AssignmentStatus.Collected;
            assignment.CollectedAt = DateTime.UtcNow;
            assignment.CollectionRequest.Status = CollectionStatus.Collected;

            await _repo.SaveAsync();
        }

        public async Task<decimal> GradeCollectionAsync(Guid studentId,Guid assignmentId,List<(Guid VarietyId, Guid GradeId, decimal QuantityKg)> grades)
        {
            var assignment = await _repo.GetAssignmentWithRequestAsync(assignmentId);

            if (assignment == null)
                throw new BusinessException("Assignment not found");

            if (assignment.StudentId != studentId)
                throw new BusinessException("Unauthorized");

            if (assignment.Status != AssignmentStatus.Collected)
                throw new BusinessException("Collect first");

            var request = assignment.CollectionRequest;
            var totalPostedKg = request.QuantityKg;

            var alreadyGradedKg = await _repo.GetTotalGradedKgAsync(request.Id);

            if (alreadyGradedKg >= totalPostedKg)
                throw new BusinessException("Already graded full quantity");

            foreach (var g in grades)
            {
                if (g.QuantityKg <= 0)
                    throw new BusinessException("Invalid quantity");

                if (alreadyGradedKg + g.QuantityKg > totalPostedKg)
                    throw new BusinessException("Quantity exceeds remaining limit");

                // SAVE RESULT
                await _repo.AddResultAsync(new CollectionResult
                {
                    CollectionRequestId = request.Id,
                    AppleVarietyId = g.VarietyId,
                    AppleGradeId = g.GradeId,
                    QuantityKg = g.QuantityKg
                });

                alreadyGradedKg += g.QuantityKg;

                //  METRICS 
                var metric = await _priceRepo.GetLatestMetricAsync();
                if (metric == null)
                {
                    metric = new AdminMetric
                    {
                        Id = Guid.NewGuid(),
                        TotalKgCollected = 0,
                        WasteReducedKg = 0,
                        CalculatedAt = DateTime.UtcNow
                    };
                    await _priceRepo.AddMetricAsync(metric);
                }

                metric.TotalKgCollected += g.QuantityKg;

                var gradeEntity = await _priceRepo.GetAppleGradeByIdAsync(g.GradeId);

               

                if(gradeEntity.Grade!="D")
                {
                    metric.WasteReducedKg += g.QuantityKg;
                }

                //if (!gradeEntity.Name.Trim().ToLower().Contains("d"))
                //    metric.WasteReducedKg += g.QuantityKg;

                metric.CalculatedAt = DateTime.UtcNow;

                //  PRICE 
                var price = await _priceRepo.GetPriceByVarietyAndGradeAsync(
                    g.VarietyId,
                    g.GradeId);

                decimal amount = 0;
                if (price != null)
                    amount = g.QuantityKg * price.PricePerKg;

                // PAYMENT EACH GRADE 
                var payment = new Payment
                {
                    OwnerId = request.AppleOwnerId,
                    CollectionRequestId = request.Id,
                    Amount = amount,
                    Status = PaymentStatus.Pending,
                    PaidAt = DateTime.UtcNow
                };

                await _paymentRepo.AddPaymentAsync(payment);
            }

            await _repo.SaveAsync();
            await _priceRepo.SaveAsync();
            await _paymentRepo.SaveAsync();

            var remaining = totalPostedKg - alreadyGradedKg;

            //  COMPLETED 
            if (remaining == 0)
            {
                assignment.Status = AssignmentStatus.Completed;
                assignment.CollectionRequest.Status = CollectionStatus.Completed;

                await _repo.SaveAsync();

                await _emailService.SendEmailAsync(new MailRequest
                {
                    ToEmail = request.User.Email,
                    Subject = "Grading Completed",
                    Body = "Your payment will be credited shortly."
                });
            }

            return remaining;
        }







          
                public async Task SetWeeklyAvailabilityAsync(Guid studentId,List<StudentAvailability> availabilities)
        {
            if (availabilities == null || !availabilities.Any())
                throw new BusinessException("Availability required");

            var weekNumber = availabilities.First().WeekNumber;

            // WEEK VALIDATION
            if (weekNumber < 1 || weekNumber > 53)
                throw new BusinessException("Invalid week number");

            // DUPLICATE CHECK
            var exists = await _repo
                .IsAvailabilityExistsAsync(studentId, weekNumber);

            if (exists)
                throw new BusinessException(
                    "Availability already saved for this week");

            foreach (var a in availabilities)
            {
                a.StudentId = studentId;
                a.WeekNumber = weekNumber;
            }

            await _repo.AddAvailabilitiesAsync(availabilities);
            await _repo.SaveAsync();
        }


        public async Task UpdateWeeklyAvailabilityAsync(Guid studentId,List<StudentAvailability> availabilities)
        {
            if (availabilities == null || !availabilities.Any())
                throw new BusinessException("Availability required");

            var weekNumber = availabilities.First().WeekNumber;

            if (weekNumber < 1 || weekNumber > 53)
                throw new BusinessException("Invalid week number");

            await _repo.DeleteAvailabilityForStudentAsync(
                studentId, weekNumber);

            foreach (var a in availabilities)
            {
                a.StudentId = studentId;
                a.WeekNumber = weekNumber;
            }

            await _repo.AddAvailabilitiesAsync(availabilities);
            await _repo.SaveAsync();
        }



        public async Task<List<StudentAssignment>> GetMyAssignmentHistoryAsync(Guid studentId)
        {
            return await _repo.GetAssignmentsByStudentAndStatusAsync(studentId,AssignmentStatus.Completed);
        }

    }
}
