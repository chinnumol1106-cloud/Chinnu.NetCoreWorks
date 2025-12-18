using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Machine_Test.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
    }
}
