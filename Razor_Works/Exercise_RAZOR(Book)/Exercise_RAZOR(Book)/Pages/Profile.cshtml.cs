using Exercise_RAZOR_Book_.Model;
using Exercise_RAZOR_Book_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_RAZOR_Book_.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IUserService _userService;
        public ProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        public User user { get; set; }
        public void OnGet()
        {
            var userid = HttpContext.Session.GetInt32("UserId");

            if(userid!=null)
            {
                user = _userService.GetUser(userid.Value);

            }
        }
    }
}
