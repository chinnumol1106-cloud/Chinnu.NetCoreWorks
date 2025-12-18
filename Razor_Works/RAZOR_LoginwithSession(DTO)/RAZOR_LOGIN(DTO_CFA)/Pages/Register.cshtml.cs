using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LOGIN_DTO_CFA_.Data;
using RAZOR_LOGIN_DTO_CFA_.DTOs;
using RAZOR_LOGIN_DTO_CFA_.Models;

namespace RAZOR_LOGIN_DTO_CFA_.Pages
{
    public class RegisterModel : PageModel
    {


        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RegisterModel(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [BindProperty]
        public RegisterDto Input { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exists = _db.Users.FirstOrDefault(u => u.Email == Input.Email);
            if (exists != null)
            {
                Message = "Email already registered.";
                return Page();
            }

            User user = _mapper.Map<User>(Input);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
    }
}
