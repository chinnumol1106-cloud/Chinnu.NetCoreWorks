using Microsoft.EntityFrameworkCore;
using Product_Exercise_Blazor.Model;

namespace Product_Exercise_Blazor.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductAppDbContext _context;

        public UserRepository(ProductAppDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
