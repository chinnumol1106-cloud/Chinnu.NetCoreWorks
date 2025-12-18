using Librarymanagement_RAZOR_CFA_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Librarymanagement_RAZOR_CFA_.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly LibraryDbContext _context;
        public int TotalCount { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public IndexModel(LibraryDbContext context)
        {
            _context = context;
        }

        public List<BookModel> Bookslist { get; set; }


        public async Task OnGetAsync()
        {
            // string searchTerm = Request.Query["SearchTerm"];
            if (string.IsNullOrEmpty(Search))
            {
                Bookslist = await _context.BookTable.ToListAsync();
            }
            else
            {

                Bookslist = await _context.BookTable
                    .Where(b => b.Title.Contains(Search)).ToListAsync();


            }

            TotalCount = Bookslist.Count();
        }
    }
}
