using Product_Exercise_Blazor.Dto;

namespace Product_Exercise_Blazor.Service
{
    public interface IUserService
    {
        Task<bool> Registeruser(RegisterDto register, string password);
        Task<bool> Login(string email, string password);
    }
}
