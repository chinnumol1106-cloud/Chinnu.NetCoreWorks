using Exercise_RAZOR_Book_.Migrations;
using Exercise_RAZOR_Book_.Model;

namespace Exercise_RAZOR_Book_.Services
{
    public interface IBookService
    {
       List<Book> GetBook();
        List<Book> GetBooks(string search);

        void AddBook(Book book);

        Book GetBookById(int id);

        void UpdateBook(Book book);
    }
}
