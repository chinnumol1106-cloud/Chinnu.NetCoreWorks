using Product_Exercise_Blazor.Model;
using Product_Exercise_Blazor.Pages;

namespace Product_Exercise_Blazor.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
