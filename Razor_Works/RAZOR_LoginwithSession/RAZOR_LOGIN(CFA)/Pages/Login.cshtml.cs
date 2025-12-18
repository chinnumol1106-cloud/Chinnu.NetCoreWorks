using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LOGIN_CFA_.Data;

namespace RAZOR_LOGIN_CFA_.Pages
{
    public class LoginModel : PageModel
    {

        private readonly ApplicationDbContext _db;


        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
        }


        [BindProperty]
        public string Email { get; set; }


        [BindProperty]
        public string Password { get; set; }


        public string ErrorMessage { get; set; }



        public void OnGet()
        {
        }



        public IActionResult OnPost()
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.FirstName);
                return RedirectToPage("/Home");
            }


            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
}
