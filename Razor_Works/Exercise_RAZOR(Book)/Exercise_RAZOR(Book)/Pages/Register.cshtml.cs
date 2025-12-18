using Exercise_RAZOR_Book_.Model;
using Exercise_RAZOR_Book_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_RAZOR_Book_.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _service;
        public RegisterModel(IUserService service)
        {
            _service = service;
        }

        [BindProperty]
        public User newuser { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var success = _service.Register(newuser);

            if (!success)
            {
                ErrorMessage = "Email already Registerd!";
                return Page();

            }

            return RedirectToPage("/Login");



        }
    }
}

