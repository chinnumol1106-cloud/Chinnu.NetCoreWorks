using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LOGIN_CFA_.Data;
using RAZOR_LOGIN_CFA_.Models;

namespace RAZOR_LOGIN_CFA_.Pages
{
    public class RegisterModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public User Input { get; set; }

        public string Message {  get; set; }
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
                return Page();

            var exists=_db.Users.FirstOrDefault(u=> u.Email == Input.Email);
             
            if(exists != null)
            {
                Message = "Email Already Registered";
                return Page();
            }

            _db.Users.Add(Input);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
    }
}
