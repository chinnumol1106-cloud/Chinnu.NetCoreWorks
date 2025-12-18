using AutoMapper;
using Machine_Test.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;

namespace Machine_Test.Pages
{
    public class LoginModel : PageModel
    {

        private readonly ApplicationDbContext _context;
    

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
           
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage {  get; set; }
        public IActionResult OnPost()
        {

            var existuser = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password==Password);

            if(existuser!=null)
            {
                HttpContext.Session.SetString("username", existuser.FName);

                return RedirectToPage("/Home");

              
            }
            else
            {

                ErrorMessage = "invalid Credentials";
                return Page();

            }


        }
    }
}
