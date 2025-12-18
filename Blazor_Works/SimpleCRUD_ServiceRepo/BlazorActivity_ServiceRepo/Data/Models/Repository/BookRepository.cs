using BlazorActivity_ServiceRepo.Data.Models.Interfaces;

namespace BlazorActivity_ServiceRepo.Data.Models.Repository
{
    public class BookRepository:IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            var book = _context.Books.ToList();
            return book;
        }


        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public Book GetBookById(int id)
        {
           var book= _context.Books.FirstOrDefault(b=>b.BookId==id);
            return book;
        }

        public void UpdateBook(Book book)
        {
            var bookid=GetBookById(book.BookId);
            if(bookid!=null)
            {
                _context.Books.Update(bookid);
                _context.SaveChanges();
            }
        }



        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);

            if(id!=null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
