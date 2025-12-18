using Librarymanagement_RAZOR_CFA_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Librarymanagement_RAZOR_CFA_.Pages.Books
{
    public class EditModel : PageModel
    {
        public readonly LibraryDbContext _context;

        public EditModel(LibraryDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookModel Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _context.BookTable.FindAsync(id);

            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bookToUpdate = await _context.BookTable.FindAsync(id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            bookToUpdate.Title = Book.Title;
            bookToUpdate.Genre = Book.Genre;
            bookToUpdate.Author = Book.Author;
            bookToUpdate.PublishDate = Book.PublishDate;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
