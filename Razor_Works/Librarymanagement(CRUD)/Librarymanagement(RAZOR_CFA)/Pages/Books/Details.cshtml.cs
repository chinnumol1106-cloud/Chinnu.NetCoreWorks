using Librarymanagement_RAZOR_CFA_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Librarymanagement_RAZOR_CFA_.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryDbContext _context;

        public DetailsModel(LibraryDbContext context)
        {
            _context = context;
        }

        public BookModel Book { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.BookTable.FindAsync(id);
            // var book = await _context.BookTable.FirstOrDefault(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }
    }
}
