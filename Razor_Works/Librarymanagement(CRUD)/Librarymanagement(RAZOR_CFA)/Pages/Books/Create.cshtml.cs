using Librarymanagement_RAZOR_CFA_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Librarymanagement_RAZOR_CFA_.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly LibraryDbContext _context;

        public CreateModel(LibraryDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookModel Book { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BookTable.Add(Book);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
