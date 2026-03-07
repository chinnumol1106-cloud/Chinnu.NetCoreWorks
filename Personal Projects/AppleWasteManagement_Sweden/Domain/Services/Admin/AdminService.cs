using AutoMapper;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Helper;
using Domain.Interfaces.Admin;
using Domain.Interfaces.Email;
using Domain.Models;
using Domain.Repositories.Admin;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;
        private readonly IEmailService _emailService;

        public AdminService(IAdminRepository repo,IEmailService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }

        public async Task AddAppleTypeAsync(AppleType type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type.Name))
                    throw new BusinessException("Apple type name is required");

                if (await _repo.AppleTypeExistsAsync(type.Name))
                    throw new BusinessException("Apple type already exists");

                await _repo.AddAppleTypeAsync(type);
                await _repo.SaveAsync();
            }
            catch (BusinessException)
            {
                throw; 
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to add apple type");
            }
        }

        public async Task AddAppleVarietyAsync(AppleVariety variety)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(variety.Name))
                    throw new BusinessException("Apple variety name is required");

                if (variety.AppleTypeId == Guid.Empty)
                    throw new BusinessException("Apple type is required for variety");

                if (await _repo.AppleVarietyExistsAsync(variety.Name, variety.AppleTypeId))
                    throw new BusinessException("This variety already exists for this apple type");

                await _repo.AddAppleVarietyAsync(variety);
                await _repo.SaveAsync();
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to add apple variety");
            }
        }

        public async Task AddAppleGradeAsync(AppleGrade grade)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(grade.Grade))
                    throw new BusinessException("Apple grade  is required");

                if (string.IsNullOrWhiteSpace(grade.Description))
                    throw new BusinessException("Apple grade description is required");

                if (await _repo.AppleGradeExistsAsync(grade.Grade))
                    throw new BusinessException("Apple grade already exists");

                await _repo.AddAppleGradeAsync(grade);
                await _repo.SaveAsync();
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to add apple grade");
            }
        }

        public async Task AddApplePriceAsync(ApplePrice price)
        {
            try
            {
                if (price.AppleTypeId == Guid.Empty)
                    throw new BusinessException("Apple type is required for pricing");

                if (price.AppleVarietyId == Guid.Empty)
                    throw new BusinessException("Apple variety is required for pricing");

                if (price.AppleGradeId == Guid.Empty)
                    throw new BusinessException("Apple grade is required for pricing");


                var grade=await _repo.GetAppleGradeByIdAsync(price.AppleGradeId);
                if (grade == null)
                    throw new BusinessException("Grade not found");

                //Trash Rule

                if(grade.Grade=="D")
                {
                    if (price.PricePerKg != 0)
                        throw new BusinessException("Trash grade price must be 0");

                    
                }
                else
                {
                    if(price.PricePerKg<=0)
                        throw new BusinessException("Price must be Greater than 0");

                }


                    var existingPrice = await _repo.GetActivePriceAsync(
                        //price.AppleTypeId,
                        price.AppleVarietyId,
                        price.AppleGradeId);

                if (existingPrice != null)
                    throw new BusinessException(
                        "Price already exists for this variety and grade");

                await _repo.AddApplePriceAsync(price);
                await _repo.SaveAsync();
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to add apple price");
            }

        }



        public async Task UpdateApplePriceAsync(Guid priceId, decimal newPrice)
        {
            try
            {
                var price = await _repo.GetApplePriceByIdAsync(priceId);

                if (price == null)
                    throw new BusinessException("Price not found");

                //  Get Grade
                var grade = await _repo.GetAppleGradeByIdAsync(price.AppleGradeId);

                if (grade == null)
                    throw new BusinessException("Grade not found");


                if(grade.Grade=="D")
                {
                    if(newPrice!=0)
                        throw new BusinessException("Trash grade price must be 0");
                }
                else
                {
                    if (newPrice <= 0)
                        throw new BusinessException("Price must be greater than zero");
                }

              
               

                //  Update
                price.PricePerKg = newPrice;

                await _repo.SaveAsync();
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to update apple price");
            }
        }




        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                return await _repo.GetUsersAsync();
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to fetch users");
            }
        }

        public async Task AssignStudentAsync(Guid studentId, Guid requestId)
        {
            try
            {
                var request = await _repo.GetCollectionRequestByIdAsync(requestId);

                if (request == null)
                    throw new BusinessException("Collection request not found");

                if (request.Status != CollectionStatus.Requested)
                    throw new BusinessException("Only requested jobs can be assigned");

                // NEW WEEK VALIDATION
                var studentAvailable = await _repo.GetAvailableStudentsByDayAsync(
                    request.PreferredPickupAt.Value.DayOfWeek,
                    request.WeekNumber);

                var isAvailable = studentAvailable.Any(s => s.StudentId == studentId);

                if (!isAvailable)
                    throw new BusinessException("Student not available for this pickup week");


                //  PREVENT DOUBLE ASSIGNMENT
                var alreadyAssigned = await _repo.IsRequestAlreadyAssignedAsync(requestId);

                if (alreadyAssigned)
                    throw new BusinessException("This request is already assigned to a student");


                var assignment = new StudentAssignment
                {
                    StudentId = studentId,
                    CollectionRequestId = requestId,
                    Status = AssignmentStatus.Assigned,
                    AssignedAt = DateTime.UtcNow
                };

                await _repo.AddStudentAssignmentAsync(assignment);

                request.Status = CollectionStatus.Assigned;
                await _repo.UpdateCollectionRequestAsync(request);

                await _repo.SaveAsync();
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to assign student");
            }
        }
        public async Task<List<(AppleGrade Grade, decimal AvailableKg)>> GetGradedStockAsync()
        {
            return await _repo.GetGradedStockAsync();
        }
        public async Task BlockUserAsync(Guid userId)
        {
            try
            {
                var user = await _repo.GetUserByIdAsync(userId);

                if (user == null)
                    throw new BusinessException("User not found");

                user.IsActive = false;
                await _repo.SaveAsync();
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to block user");
            }
        }



        public async Task<List<StudentAvailability>> GetAvailableStudentsByDayAsync(DayOfWeek day,int weekNumber)
        
            {
                if (weekNumber <= 0)
                    throw new BusinessException("Invalid week number");

                return await _repo.GetAvailableStudentsByDayAsync(day, weekNumber);
            }
        




        public async Task<List<CollectionRequest>> GetCollectionRequestsAsync(CollectionStatus? status)
        {
            try
            {
                return await _repo.GetCollectionRequestsAsync(status);
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to load collection requests");
            }
        }


        public async Task<List<StudentAssignment>> GetStudentAssignmentsAsync(AssignmentStatus? status)
        {
            try
            {
                return await _repo.GetStudentAssignmentsAsync(status);
            }
            catch (Exception)
            {
                throw new BusinessException("Failed to load student assignments");
            }
        }
        public async Task<List<CompanyAppleRequest>> GetCompanyRequestsAsync()
        {
            return await _repo.GetCompanyRequestsAsync();
        }


        public async Task<AdminMetric> GetMetricsAsync()
        {
            try
            {
                var metric = await _repo.GetMetricsAsync();

                if (metric == null)
                {
                    metric = new AdminMetric
                    {
                        Id = Guid.NewGuid(),
                        TotalKgCollected = 0,
                        WasteReducedKg = 0,
                        CalculatedAt = DateTime.UtcNow
                    };

                    await _repo.AddMetricAsync(metric);
                    await _repo.SaveAsync();
                }

                return metric;
            }
            catch
            {
                throw new BusinessException("Failed to load sustainability metrics");
            }
        }
        public async Task ApproveCompanyRequestAsync(Guid id)
        {
            var req = await _repo.GetCompanyRequestByIdAsync(id);
            if (req == null) throw new BusinessException("Request not found");

            req.Status = CompanyRequestStatus.Approved;

            await _repo.ReduceStockAsync(
    req.AppleGradeId,
    req.AppleVarietyId,
    req.QuantityKg);

            await _repo.SaveAsync();

            await _emailService.SendEmailAsync(new MailRequest
            {
                ToEmail = req.Company.User.Email,
                Subject = "Request Approved",
                Body = $"Your request approved. Pay {req.TotalAmount}"
            });
        }

        public async Task RejectCompanyRequestAsync(Guid id)
        {
            var req = await _repo.GetCompanyRequestByIdAsync(id);
            if (req == null) throw new BusinessException("Request not found");

            req.Status = CompanyRequestStatus.Rejected;

            await _repo.SaveAsync();

            await _emailService.SendEmailAsync(new MailRequest
            {
                ToEmail = req.Company.User.Email,
                Subject = "Request Rejected",
                Body = "Your apple request was rejected"
            });
        }



        public async Task ConfirmCompanyPaymentAsync(Guid id)
        {
            var req = await _repo.GetCompanyRequestByIdAsync(id);
            if (req == null) throw new BusinessException("Request not found");

            req.Status = CompanyRequestStatus.PaymentConfirmed;

            await _repo.SaveAsync();

            await _emailService.SendEmailAsync(new MailRequest
            {
                ToEmail = req.Company.User.Email,
                Subject = "Payment Confirmed",
                Body = "Payment confirmed. You can collect apples."
            });
        }
        public async Task AddCompanyPriceAsync(CompanyApplePrice price)
        {
            var grade = await _repo.GetAppleGradeByIdAsync(price.AppleGradeId);

            if (grade == null)
                throw new BusinessException("Grade not found");

            // GRADE D NOT ALLOWED
            if (grade.Grade == "D")
                throw new BusinessException("Trash grade cannot be sold to companies");

            // GRADE A NEEDS VARIETY
            if (grade.Grade == "A" && price.AppleVarietyId == null)
                throw new BusinessException("Variety required for Grade A");

            if (price.PricePerKg <= 0)
                throw new BusinessException("Invalid price");

            var existingCompanyPrice = await _repo.GetCompanyPriceByVarietyAndGradeAsync(
                     //price.AppleTypeId,
                     price.AppleVarietyId.Value,
                     price.AppleGradeId);

            if (existingCompanyPrice != null)
                throw new BusinessException(
                    "Price already exists for this variety and grade");



            // 🔥 NEW RULE START
            ApplePrice? ownerPrice;

            if (price.AppleVarietyId != null)
            {
                ownerPrice = await _repo.GetPriceByVarietyAndGradeAsync(
                    price.AppleVarietyId.Value,
                    price.AppleGradeId);
            }
            else
            {
                ownerPrice = await _repo.GetPriceByGradeAsync(price.AppleGradeId);
            }

            if (ownerPrice == null)
                throw new BusinessException("Set owner price first");

            if (price.PricePerKg <= ownerPrice.PricePerKg)
                throw new BusinessException("Company price must be greater than owner price");
            // 🔥 NEW RULE END




            await _repo.AddCompanyPriceAsync(price);
            await _repo.SaveAsync();
        }

        public async Task UpdateCompanyPriceAsync(Guid gradeId, decimal newPrice)
        {
            var price = await _repo.GetCompanyPriceByGradeAsync(gradeId);

            if (price == null)
                throw new BusinessException("Price not found");

            price.PricePerKg = newPrice;
            await _repo.SaveAsync();
        }
        public async Task<List<ApplePrice>> GetOwnerPricesAsync()
        {
            return await _repo.GetOwnerPricesAsync();
        }
        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _repo.GetUserByIdAsync(id);

            if (user == null)
                throw new BusinessException("User not found");

            _repo.DeleteUser(user);
            await _repo.SaveAsync();
        }

        public async Task<List<CompanyApplePrice>> GetCompanyPricesAsync()
        {
            return await _repo.GetCompanyPricesAsync();
        }


        public async Task<object> GetAvailableApplesAsync()
        {
            var stock = await _repo.GetAllGradedStockAsync();

            var result = stock
                .Where(x => x.Kg > 0 && x.Grade!="D")
                .Select(x => new
                {
                    Grade = x.Grade,
                    Variety = x.Variety,
                    QuantityKg = x.Kg
                })
                .ToList();

            return result;
        }









    }
}
