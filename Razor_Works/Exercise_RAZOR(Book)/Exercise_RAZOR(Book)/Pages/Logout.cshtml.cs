using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_RAZOR_Book_.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
    }
}
