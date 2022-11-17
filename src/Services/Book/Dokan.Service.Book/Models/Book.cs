using MongoDB.Bson.Serialization.Attributes;

namespace Dokan.Service.Book.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        public string Category { get; set; } = string.Empty;

        public string Author {get; set; } = string.Empty;
}
}
