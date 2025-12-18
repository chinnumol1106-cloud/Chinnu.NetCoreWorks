using Exercise_RAZOR_Book_.Model;
using Exercise_RAZOR_Book_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_RAZOR_Book_.Pages
{
    public class EditBookModel : PageModel
    {

        private readonly IBookService _bookservice;

        public EditBookModel(IBookService bookservice)
        {
            _bookservice = bookservice;
        }

        [BindProperty]
        public Book book { get; set; }
        public IActionResult OnGet(int id)
        {

            book=_bookservice.GetBookById(id);

            if(book!=null)
            {
                return Page();
            }
            return NotFound();
        }


        public IActionResult OnPost()
        {
            _bookservice.UpdateBook(book);
            return RedirectToPage("Dashboard");

        }
    }
}
