using Dokan.Service.Book.Interfaces;
using MongoDB.Driver;

namespace Dokan.Service.Book.Services
{
    public class BookService : IBookService
    {
        private readonly IMongoCollection<Models.Book> _books;

        public BookService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _books = database.GetCollection<Models.Book>("Book");

        }

        public Models.Book AddBook(Models.Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public IList<Models.Book> GetBooks() => _books.Find(sub => true).ToList();

        public Models.Book Find(string id) => _books.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(Models.Book book) => _books.ReplaceOne(sub => sub.Id == book.Id, book);

        public void Delete(string id) => _books.DeleteOne(book => book.Id == id);        
    }
}
