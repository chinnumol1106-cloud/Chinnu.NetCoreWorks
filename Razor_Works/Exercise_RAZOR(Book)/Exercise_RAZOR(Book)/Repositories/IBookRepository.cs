using Exercise_RAZOR_Book_.Migrations;
using Exercise_RAZOR_Book_.Model;

namespace Exercise_RAZOR_Book_.Repositories
{
    public interface IBookRepository
    {

        List<Book> GetAllBook();
        List<Book> GetAllBooks(string search);

        void AddNewBook(Book book);

        Book GetBookId(int id);

        void Update(Book book);
    }

}

