using MVC_Exercise.Data;
using MVC_Exercise.Models.Entities;

namespace MVC_Exercise.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }
        public Company GetCompanyByEmail(string email)
        {
            return _context.Companies.FirstOrDefault(m => m.Email == email);
        }

        public void AddCompany(Company newcompany)
        {
            _context.Companies.Add(newcompany);
            _context.SaveChanges();
        }

        public Company GetCompanyById(Guid companyId)
        {
            return _context.Companies.FirstOrDefault(c => c.CompanyId == companyId);
        }
    }
}
