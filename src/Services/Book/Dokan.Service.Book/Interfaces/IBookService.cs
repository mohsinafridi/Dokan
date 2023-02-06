using Dokan.Service.Book.Models;

namespace Dokan.Service.Book.Interfaces
{
    public interface IBookService
    {
        Task<TransactionResult> AddBook(Models.Book book);
        Task<IList<Models.Book>> GetBooks();
        Task<Models.Book> GetBookById(string Id);
        Task<TransactionResult> UpdateBook(Models.Book book);
        Task<bool> DeleteBook(string id);

    }
}
