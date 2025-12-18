using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LOGIN_DTO_CFA_.Data;
using RAZOR_LOGIN_DTO_CFA_.DTOs;

namespace RAZOR_LOGIN_DTO_CFA_.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public LoginDto Input { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == Input.Email && u.Password == Input.Password);
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
