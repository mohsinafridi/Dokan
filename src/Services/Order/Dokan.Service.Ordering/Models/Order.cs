using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Dokan.Service.Ordering.Models
{
    [Serializable,BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId,BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("orderid")]
        public string OrderId { get; set; }
        [JsonPropertyName("customerid")]
        public string CustomerId { get; set; }
        [JsonPropertyName("orderon")]
        public DateTime OrderOn { get; set; }
        [JsonPropertyName("orderdetails")] 
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
