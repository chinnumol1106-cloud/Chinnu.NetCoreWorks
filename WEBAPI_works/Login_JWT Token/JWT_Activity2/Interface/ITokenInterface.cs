using JWT_Activity2.Models;

namespace JWT_Activity2.Interface
{
    public interface ITokenInterface
    {
        public string? CreateToken(User user);
        string GetuserName();
    }
}
