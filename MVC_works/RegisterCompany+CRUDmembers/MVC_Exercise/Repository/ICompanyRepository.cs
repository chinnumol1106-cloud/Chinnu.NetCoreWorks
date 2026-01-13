using AutoMapper.Execution;
using MVC_Exercise.Models.Entities;

namespace MVC_Exercise.Repository
{
    public interface ICompanyRepository
    {
        Company GetCompanyByEmail(string email);
        void AddCompany(Company newcompany);
        Company GetCompanyById(Guid companyId);
    }
}
