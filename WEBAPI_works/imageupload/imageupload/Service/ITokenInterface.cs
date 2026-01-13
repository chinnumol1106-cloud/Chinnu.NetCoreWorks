using imageupload.Models;

namespace imageupload.Service
{
    public interface ITokenInterface
    {
        public string? CreateToken(User user);

        string GetuserName();
    }
}
