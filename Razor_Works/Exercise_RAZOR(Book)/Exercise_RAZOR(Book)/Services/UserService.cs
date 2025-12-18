using Exercise_RAZOR_Book_.Model;
using Exercise_RAZOR_Book_.Repositories;

namespace Exercise_RAZOR_Book_.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool Register(User user)
        {
            var existuser=_userRepository.GetUserByEmail(user.Email);

            if (existuser != null)
            {
                return false;
            }
            _userRepository.Add(user);
            return true;
        }


        public User Login(string email, string password)
        {
            var user=_userRepository.GetUserByEmail(email);

            if (user != null && user.Password==password)
            {
                return user;
            }
            return null;
        }


        public User GetUser(int  id)
        {
            var newuser=_userRepository.GetUserById(id);
            return newuser;
        }
    }
}
