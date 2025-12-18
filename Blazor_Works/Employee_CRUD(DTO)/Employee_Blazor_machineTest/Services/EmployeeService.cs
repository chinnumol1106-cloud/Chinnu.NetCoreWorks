using AutoMapper;
using Employee_Blazor_machineTest.Dto;
using Employee_Blazor_machineTest.Model;
using Employee_Blazor_machineTest.Repository;
using System.Collections.Generic;

namespace Employee_Blazor_machineTest.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<EmployeeDto>> GetEmployeebyIdAsync()
        {
            var employeelist=await _repository.GetEmployeebyIdAsync();

            if(employeelist != null)
            {
                var newlist=_mapper.Map<List<EmployeeDto>>(employeelist);
                return newlist;
            }
            return null;
        }

        public async Task AddEmployeeAsync(EmployeeDto employee)
        {
            var newemployee=_mapper.Map<Employee>(employee);
            await _repository.AddEmployeeAsync(newemployee);

        }

        public async Task UpdateEmployeeAsync(EmployeeDto employee)
        {
            var existemployee=await _repository.GetEmployeebyIdAsync(employee.Id);

            if(existemployee != null)
            {
                _mapper.Map(employee, existemployee);
                await _repository.UpdateAsync(existemployee);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var existemployee = await _repository.GetEmployeebyIdAsync(id);
            if( existemployee != null )
            {
                await _repository.DeleteAsync(existemployee);
            }


        }
    }
}

