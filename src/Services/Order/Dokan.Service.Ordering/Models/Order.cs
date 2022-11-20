using MongoDB.Bson.Serialization.Attributes;

namespace Dokan.Service.Ordering.Models
{
    [Serializable,BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId,BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderOn { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
