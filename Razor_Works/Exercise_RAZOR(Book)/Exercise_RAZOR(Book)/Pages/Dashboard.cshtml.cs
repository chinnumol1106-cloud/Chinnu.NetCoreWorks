using Exercise_RAZOR_Book_.Model;
using Exercise_RAZOR_Book_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_RAZOR_Book_.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IBookService _bookService;
        public DashboardModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        public string UserName {  get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search {  get; set; }

        public List<Book> Booklist {  get; set; }
        public int TotalCount {  get; set; }
      

        public void OnGet()
        {
            UserName = HttpContext.Session.GetString("UserName");


            if (string.IsNullOrEmpty(Search))
            {
                Booklist = _bookService.GetBook();
            }
            else
            {
                Booklist = _bookService.GetBooks(Search);
            }

            TotalCount = Booklist.Count();
        }
    }
}
