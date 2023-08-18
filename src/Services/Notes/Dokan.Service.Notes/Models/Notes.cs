using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Dokan.Service.Notes.Models
{
    public class Notes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreateadAt { get; set; }
        public bool isDeleted { get; set; }
    }
}
