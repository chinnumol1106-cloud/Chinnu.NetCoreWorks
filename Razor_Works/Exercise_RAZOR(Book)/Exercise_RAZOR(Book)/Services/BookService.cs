using Exercise_RAZOR_Book_.Model;
using Exercise_RAZOR_Book_.Repositories;

namespace Exercise_RAZOR_Book_.Services
{
    public class BookService : IBookService


    {

        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public List<Book> GetBook()
        {
            var newbook = _bookRepository.GetAllBook();
            return newbook;
        }

        public List<Book> GetBooks(string search)
        {
         var newbooks = _bookRepository.GetAllBooks(search);
                return newbooks;
        }


        public void AddBook(Book book)
        {
            _bookRepository.AddNewBook(book);
        }

        public Book GetBookById(int id)
        {
            var getbook=_bookRepository.GetBookId(id);
            return getbook;
        }

       public void UpdateBook(Book book)
        {
            _bookRepository.Update(book);
        }
    }
}