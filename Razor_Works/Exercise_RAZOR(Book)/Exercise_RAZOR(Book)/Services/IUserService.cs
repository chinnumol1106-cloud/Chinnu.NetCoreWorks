using Exercise_RAZOR_Book_.Model;

namespace Exercise_RAZOR_Book_.Services
{
    public interface IUserService
    {
        bool Register(User user);
        User Login(string email, string password);
        User GetUser(int id);
    }
}
