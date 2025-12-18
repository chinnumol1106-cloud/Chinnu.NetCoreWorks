using Employee_Blazor_machineTest.Dto;
using Employee_Blazor_machineTest.Model;

namespace Employee_Blazor_machineTest.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeebyIdAsync();
        Task AddEmployeeAsync(Employee newemployee);
        Task<Employee> GetEmployeebyIdAsync(int id);
        Task UpdateAsync(Employee existemployee);
        Task DeleteAsync(Employee existemployee);
    }
}
