using Employee_Blazor_machineTest.Dto;
using Employee_Blazor_machineTest.Model;

namespace Employee_Blazor_machineTest.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetEmployeebyIdAsync();
        Task AddEmployeeAsync(EmployeeDto employee);
        Task UpdateEmployeeAsync(EmployeeDto employee);
        Task DeleteEmployeeAsync(int id);
    }
}
