using Exercise_RAZOR_Book_.Model;

namespace Exercise_RAZOR_Book_.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void Add(User user);
        User GetUserById(int id);
    }
}
