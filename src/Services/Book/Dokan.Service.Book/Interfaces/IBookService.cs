using Dokan.Service.Book.Models;

namespace Dokan.Service.Book.Interfaces
{
    public interface IBookService
    {
        Models.Book AddBook(Models.Book book);
        IList<Models.Book> GetBooks();
        Models.Book Find(string Id);
        void Update(Models.Book book);
        void Delete(string id);

    }
}
