using Employee_Blazor_machineTest.Model;
using Microsoft.EntityFrameworkCore;

namespace Employee_Blazor_machineTest.Repository
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeebyIdAsync()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task AddEmployeeAsync(Employee newemployee)
        {
            _context.Employees.Add(newemployee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeebyIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task UpdateAsync(Employee existemployee)
        {
            _context.Employees.Update(existemployee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee existemployee)
        {
            _context.Employees.Remove(existemployee);
            await _context.SaveChangesAsync();
        }
    }
}
