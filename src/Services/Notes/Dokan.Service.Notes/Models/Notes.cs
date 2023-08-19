using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Dokan.Service.Notes.Models
{
    public class Notes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
        [JsonPropertyName("createadat")]
        public DateTime CreateadAt { get; set; }
        [JsonPropertyName("isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
