using Dokan.Service.Ordering.Interfaces;
using Dokan.Service.Ordering.Models;
using MassTransit.Transports;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dokan.Service.Ordering.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<BsonDocument> _orderCollection;
        public OrderService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient();
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _orderCollection = database.GetCollection<BsonDocument>(_databaseSettings.CollectionName);
        }
        public async Task<TransactionResult> AddOrder(Order order)
        {
            var collection = GetCollection("dokan", "orders");
            var document = new BsonDocument
            {
                {"id", Guid.NewGuid().ToString("N") },
                {"customerid", order.CustomerId},
                {"orderon", order.OrderOn},
              //  {"orderdetails", order.OrderDetails},              
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

        public async Task<bool> DeleteOrder(string id)
        {
            var result = await _orderCollection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("orderid", id));

            return result.DeletedCount > 0;
        }

        public async Task<Order> GetOrderById(string Id)
        {
            var orderCursor = await _orderCollection.Find(Builders<BsonDocument>.Filter.Eq("orderid", Id)).FirstOrDefaultAsync();

            var order = ConvertBsonToBook(orderCursor);

            if (order == null)
            {
                return new Models.Order();
            }

            return order;
        }

        public async Task<IList<Order>> GetOrders()
        {
            var ordersResult = await _orderCollection.Find(_ => true).ToListAsync();
            var orderList = new List<Models.Order>();

            if (ordersResult == null) return orderList;

            foreach (var document in ordersResult)
            {
                orderList.Add(ConvertBsonToBook(document));
            }

            return orderList;
        }


        public async Task<TransactionResult> UpdateOrder(Order order)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("id", order.OrderId);

            var update = Builders<BsonDocument>.Update
        .Set("customerid", order.CustomerId)
        .Set("orderon", order.OrderOn);

            var result = await _orderCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                return TransactionResult.NotFound;
            }

            if (result.ModifiedCount > 0)
            {
                return TransactionResult.Success;
            }
            return TransactionResult.ServerError;
        }

        private IMongoCollection<BsonDocument> GetCollection(string databaseName, string collectionName)
        {
            var client = new MongoClient();
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);
            return collection;
        }
        private Models.Order ConvertBsonToBook(BsonDocument document)
        {
            if (document == null) return null;

            return new Models.Order
            {
                OrderId = document["id"].AsString,
                CustomerId = document["customerid"].AsString,
                OrderOn = document["orderon"].AsDateTime,
            };
        }
    }
}
