using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;

namespace Machine_Test.Pages
{
    public class HomeModel : PageModel
    {
       public string Welcome {  get; set; }

        public bool IsLoggedIn {  get; set; }

        public IActionResult OnGet()
        {
            var user=HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(user))
            {
                Welcome = $"Welcome{user}";
                IsLoggedIn = true;
            }
            else
            {
                Welcome = "Please Login Or register";
                IsLoggedIn = false;

            }
            return Page();
        }
     
    }


}
