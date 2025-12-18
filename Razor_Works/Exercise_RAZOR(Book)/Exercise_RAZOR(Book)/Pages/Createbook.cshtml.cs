using Exercise_RAZOR_Book_.Model;
using Exercise_RAZOR_Book_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_RAZOR_Book_.Pages
{
    public class CreatebookModel : PageModel
    {
        private readonly IBookService _bookservice;

        public CreatebookModel(IBookService bookservice)
        {
            _bookservice = bookservice;
        }

        [BindProperty]
        public Book Book { get; set; }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _bookservice.AddBook(Book);
            return RedirectToPage("Dashboard");
        }


    }
}
