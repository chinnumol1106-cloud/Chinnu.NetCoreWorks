using MVC_Exercise.Models.DTOs;

namespace MVC_Exercise.Services
{
    public interface ICompanyService
    {
        bool CompanyRegister(CompanyDto newCompany);
       CompanyDto LoginCompany(string email,string password);
        CompanyDto GetCompanyById(Guid companyId);

    }
}
