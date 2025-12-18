using Exercise_RAZOR_Book_.Data;
using Exercise_RAZOR_Book_.Model;

namespace Exercise_RAZOR_Book_.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u=>u.Email == email);
            return user;
        }


        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public User GetUserById(int  id)
        {
         var newuser= _context.Users.FirstOrDefault(u=>u.Id== id);
            return newuser;
        }
    }
}
