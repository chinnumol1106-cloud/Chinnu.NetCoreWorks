using AutoMapper;
using MVC_Exercise.Models.DTOs;
using MVC_Exercise.Models.Entities;
using MVC_Exercise.Repository;

namespace MVC_Exercise.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;
        public CompanyService(ICompanyRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

       public  bool CompanyRegister(CompanyDto newCompany)
        {
            var existcompany=_repository.GetCompanyByEmail(newCompany.Email);
            if(existcompany != null)
            {
                return false;

            }
            var newcompany=_mapper.Map<Company>(newCompany);

            _repository.AddCompany(newcompany);
            return true;

        }

        public CompanyDto LoginCompany(string email, string password)
        {
            var loggedcompany=_repository.GetCompanyByEmail(email);

            if(loggedcompany != null && loggedcompany.Password==password)
            {
                var logincompany=_mapper.Map<CompanyDto>(loggedcompany);
                return logincompany;
            }
            return null;
        }

        public CompanyDto GetCompanyById(Guid companyId)
        {
            var company=_repository.GetCompanyById(companyId);
           if(company != null)
            {
             var companyDetails=_mapper.Map<CompanyDto>(company);
                return companyDetails;
            }
            return null;
        }
    }
}
