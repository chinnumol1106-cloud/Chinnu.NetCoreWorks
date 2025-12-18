namespace BlazorActivity_ServiceRepo.Data.Models.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        void AddBook(Book book);

        Book GetBookById(int id);

        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
