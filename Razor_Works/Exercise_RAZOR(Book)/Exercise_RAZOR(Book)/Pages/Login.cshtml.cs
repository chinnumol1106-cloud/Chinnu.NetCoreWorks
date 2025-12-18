using Exercise_RAZOR_Book_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_RAZOR_Book_.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public string Email {  get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage {  get; set; }
       

        public IActionResult OnPost()
        {
            var user=_userService.Login(Email, Password);

            if(user != null)
            {
                HttpContext.Session.SetInt32("UserId",user.Id);
                HttpContext.Session.SetString("UserName",user.FirstName);
                return RedirectToPage("/Dashboard");

            }
            ErrorMessage = "Invalid Credentials";
            return Page();

          
        }
    }
}
