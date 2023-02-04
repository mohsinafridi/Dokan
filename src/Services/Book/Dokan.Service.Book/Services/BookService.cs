using Dokan.Service.Book.Interfaces;
using Dokan.Service.Book.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dokan.Service.Book.Services
{
    public class BookService : IBookService
    {
        private readonly IMongoCollection<BsonDocument> _bookCollection;

        public BookService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient();
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _bookCollection = database.GetCollection<BsonDocument>(_databaseSettings.CollectionName);

        }

        public async Task<TransactionResult> AddBook(Models.Book book)
        {
            var collection = GetCollection("dokan", "books");
            var document = new BsonDocument
            {
                {"id", Guid.NewGuid().ToString("N") },
                {"name", book.Name},
                {"price", book.Price},
                {"category", book.Category},
                {"author", book.Author}
            };

            try
            {
                await collection.InsertOneAsync(document);
                if (document["_id"].IsObjectId)
                {
                    return TransactionResult.Success;
                }

                return TransactionResult.BadRequest;
            }
            catch
            {
                return TransactionResult.ServerError;
            }
        }

        public async Task<IList<Models.Book>> GetBooks() 
        {           
            var booksResult = _bookCollection.Find(_ => true).ToListAsync();
            var bookList= new List<Models.Book>();

            if (booksResult == null) return bookList;

            foreach (var document in await booksResult)
            {
                bookList.Add(ConvertBsonToBook(document));
            }

            return bookList;
        }

        public async Task<Models.Book> GetBookById(string id)
        {
            var bookCursor = await _bookCollection.Find(Builders<BsonDocument>.Filter.Eq("id", id)).FirstOrDefaultAsync();
            
            var book= ConvertBsonToBook(bookCursor);

            if (book == null)
            {
                return new Models.Book();
            }

            return book;
        }

        public async Task<TransactionResult> UpdateBook(Models.Book book) 
        {
          
            var filter = Builders<BsonDocument>.Filter.Eq("id", book.Id);
            var update = Builders<BsonDocument>.Update
                .Set("name", book.Name)
                .Set("category", book.Category)
                .Set("author", book.Author)
                .Set("price", book.Price);       
            
            var result = await _bookCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0) 
            {
            return TransactionResult.NotFound;
            }

            if (result.ModifiedCount> 0)
            {
                return TransactionResult.Success;
            }
            return TransactionResult.ServerError;

        }

        public async Task<bool> DeleteBook(string id)
        {            
            var result = await _bookCollection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("id", id));

            return result.DeletedCount > 0;
        }

        private IMongoCollection<BsonDocument> GetCollection(string databaseName, string collectionName)
        {
            var client = new MongoClient();
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);
            return collection;
        }
        private Models.Book ConvertBsonToBook(BsonDocument document)
        {
            if (document == null) return null;

            return new Models.Book
            {
                Id = document["id"].AsString,
                Name = document["name"].AsString,
                Category= document["category"].AsString,
                Author= document["author"].AsString,
                Price= document["price"].AsDouble
            };
        }
    }
}
