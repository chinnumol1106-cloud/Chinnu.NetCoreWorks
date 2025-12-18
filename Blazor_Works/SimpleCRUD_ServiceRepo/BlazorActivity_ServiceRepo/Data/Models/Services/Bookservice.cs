using BlazorActivity_ServiceRepo.Data.Models.Interfaces;
using Microsoft.Identity.Client;

namespace BlazorActivity_ServiceRepo.Data.Models.Services
{
    public class Bookservice : IBookServicecs
    {
        private readonly IBookRepository _bookRepository;

        public Bookservice(IBookRepository bookRepository) 
            {
                _bookRepository = bookRepository;
            }



            public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();

        }


        public void AddBook(Book book)
        {
            _bookRepository.AddBook(book);
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }


        public void UpdateBook(Book book)
        {
            _bookRepository.UpdateBook(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.DeleteBook(id);
        }
    }
}
