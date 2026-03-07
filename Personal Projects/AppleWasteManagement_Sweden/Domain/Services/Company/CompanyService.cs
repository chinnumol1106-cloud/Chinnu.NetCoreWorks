using Domain.Enum;
using Domain.Exceptions;
using Domain.Helper;
using Domain.Interfaces.Company;
using Domain.Interfaces.Email;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Company
{
    public class CompanyService:ICompanyService
    {
        private readonly ICompanyRepository _repo;
        private readonly IEmailService _email;

        public CompanyService(ICompanyRepository repo,IEmailService email)
        {
            _repo = repo;
            _email = email;
        }


        public async Task<List<(AppleGrade Grade, decimal AvailableKg)>> GetAvailableGradesAsync()
         => await _repo.GetAvailableGradesWithQuantityAsync();

        public async Task<List<CompanyApplePrice>> GetPricesAsync()
            => await _repo.GetPricesAsync();

        public async Task RequestApplesAsync(Guid userId, Guid gradeId, Guid? varietyId, decimal quantity)
        {
            if (quantity <= 0)
                throw new BusinessException("Invalid quantity");

        
            var company = await _repo.GetCompanyByUserIdAsync(userId);
            if (company == null)
                throw new BusinessException("Company not found");

            var grade = await _repo.GetGradeByIdAsync(gradeId);
            if (grade == null)
                throw new BusinessException("Grade not found");


            if(grade.Grade=="D")
                throw new BusinessException("Trash apples cannot be requested");

            CompanyApplePrice? price;

          
           
                if (varietyId == null)
                    throw new BusinessException("Varietyis required");

                price = await _repo.GetPriceByVarietyAndGradeAsync(varietyId.Value, gradeId);
            
           



            if (price == null)
                throw new BusinessException("Price not found");

            var total = price.PricePerKg * quantity;

            var request = new CompanyAppleRequest
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id, 
                AppleGradeId = gradeId,
                AppleVarietyId = varietyId,
                QuantityKg = quantity,
                TotalAmount = total,
                Status = CompanyRequestStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddRequestAsync(request);
            await _repo.SaveAsync();
        }
        public async Task<List<CompanyAppleRequest>> GetMyRequestsAsync(Guid userId)
        {
            var company = await _repo.GetCompanyByUserIdAsync(userId);
            if (company == null)
                throw new BusinessException("Company not found");

            return await _repo.GetRequestsByCompanyAsync(company.Id);
        }

        public async Task PayAsync(Guid userId, Guid requestId)
        {
            var company = await _repo.GetCompanyByUserIdAsync(userId);
            if (company == null)
                throw new BusinessException("Company not found");

            var req = await _repo.GetRequestByIdAsync(requestId);
            if (req == null || req.CompanyId != company.Id)
                throw new BusinessException("Request not found");

            req.Status = CompanyRequestStatus.Paid;
            await _repo.SaveAsync();

            await _email.SendEmailAsync(new MailRequest
            {
                ToEmail = "admin@greenshare.com",
                Subject = "Company Payment",
                Body = $"Company paid {req.TotalAmount}"
            });
        }

        public async Task ReceiveAsync(Guid userId, Guid requestId)
        {
            var company = await _repo.GetCompanyByUserIdAsync(userId);
            if (company == null)
                throw new BusinessException("Company not found");

            var req = await _repo.GetRequestByIdAsync(requestId);
            if (req == null || req.CompanyId != company.Id)
                throw new BusinessException("Request not found");

            req.Status = CompanyRequestStatus.Completed;

            var stock = await _repo.GetStockItemAsync(company.Id, req.AppleGradeId);

            if (stock == null)
            {
                stock = new CompanyStock
                {
                    Id = Guid.NewGuid(),
                    CompanyId = company.Id,
                    AppleGradeId = req.AppleGradeId,
                    QuantityKg = req.QuantityKg
                };
                await _repo.AddStockAsync(stock);
            }
            else
            {
                stock.QuantityKg += req.QuantityKg;
            }

            await _repo.SaveAsync();
        }

        public async Task<List<CompanyStock>> GetStockAsync(Guid userId)
        {
            var company = await _repo.GetCompanyByUserIdAsync(userId);
            if (company == null)
                throw new BusinessException("Company not found");

            return await _repo.GetStockAsync(company.Id);
        }

        public async Task UsageAsync(Guid userId, Guid gradeId, decimal kg, string purpose)
        {
            var company = await _repo.GetCompanyByUserIdAsync(userId);
            if (company == null)
                throw new BusinessException("Company not found");

            var stock = await _repo.GetStockItemAsync(company.Id, gradeId);
            if (stock == null || stock.QuantityKg < kg)
                throw new BusinessException("Not enough stock");

            stock.QuantityKg -= kg;

            await _repo.SaveAsync();
        }



        public async Task<object> GetAvailableGradesForCompanyAsync(Guid userId)
        {
            var company = await _repo.GetCompanyByUserIdAsync(userId);

            if (company == null)
                throw new BusinessException("Company not found");

            var allResults = await _repo.GetAllCollectionResultsAsync();

            var result = new List<object>();

            //For supermarket
            if (company.CompanyType == CompanyType.SuperMarket)
            {
                var gradeA = allResults
                    .Where(x => x.AppleGrade.Grade == "A")
                    .GroupBy(x => x.AppleVariety.Name)
                    .Select(g => new
                    {
                        Grade = "A",
                        Variety = g.Key,
                        QuantityKg = g.Sum(x => x.QuantityKg)
                    });

                result.AddRange(gradeA);
            }

            //For factories
            else if (company.CompanyType == CompanyType.Factory)
            {
                var gradeB = allResults
                    .Where(x => x.AppleGrade.Grade == "B")
                    .GroupBy(x => x.AppleVariety.Name)
                    .Select(g => new
                    {
                        Grade = "B",
                        Variety = g.Key,
                        QuantityKg = g.Sum(x => x.QuantityKg)
                    });

                result.AddRange(gradeB);
            }
            //For Biogas
            else if (company.CompanyType == CompanyType.Biogas)
            {
                var gradeC = allResults
                    .Where(x => x.AppleGrade.Grade == "C")
                    .GroupBy(x => x.AppleVariety.Name)
                    .Select(g => new
                    {
                        Grade = "C",
                        Variety = g.Key,
                        QuantityKg = g.Sum(x => x.QuantityKg)
                    });

                result.AddRange(gradeC);
            }


            return result;
        }

    }
}
