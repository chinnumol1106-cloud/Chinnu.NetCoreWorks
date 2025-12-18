using Exercise_RAZOR_Book_.Data;
using Exercise_RAZOR_Book_.Model;
using Microsoft.EntityFrameworkCore;

namespace Exercise_RAZOR_Book_.Repositories
{
    public class BookRepository : IBookRepository

    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBook()
        {
            var allbook = _context.Books.ToList();
            return allbook;
        }

        public List<Book> GetAllBooks(string search)
        {
            var allbooks = _context.Books.Where(b => b.Title.Contains(search)).ToList();
            return allbooks;
        }


        public void AddNewBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public Book GetBookId(int id)
        {
            var getbook= _context.Books.Find(id);
            return getbook;
        }

       public void Update(Book book)
        {
            var existbook=GetBookId(book.Id);

            if (existbook!=null)
            {
              _context.Entry(existbook).State=EntityState.Detached;
                var updatedbook = book;
                _context.Books.Attach(updatedbook);
                _context.Entry(updatedbook).State=EntityState.Modified;
                _context.SaveChanges();

            }

   
        }
    }
}
