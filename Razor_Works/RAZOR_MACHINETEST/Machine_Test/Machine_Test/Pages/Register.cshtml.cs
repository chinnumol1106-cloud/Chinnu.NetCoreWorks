using AutoMapper;
using Machine_Test.Data;
using Machine_Test.Dto;
using Machine_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Machine_Test.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RegisterModel(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [BindProperty]
        public RegisterDto Registerdto {  get; set; }

        public string ErrorMessage {  get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            
                return Page();
            

            var existuser=_context.Users.FirstOrDefault(u=>u.Email==Registerdto.Email);
            
            if(existuser!=null)
            {
                ErrorMessage = "Email already Exist";
                return Page();
            }

            User newuser = _mapper.Map<User>(Registerdto);

            _context.Users.Add(newuser);
            _context.SaveChanges();

            return RedirectToPage("/Login");
        }
    }
}
