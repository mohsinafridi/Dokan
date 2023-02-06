using Dokan.Service.Ordering.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Dokan.Service.Ordering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMongoCollection<Order> _orderCollection;
        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;    
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("dokan");
            _orderCollection = database.GetCollection<Order>("orders");
        }

        //[HttpGet]
        //[Authorize]
        //public IActionResult Get()
        //{
        //    return Ok("OK");
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(string id)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, id);
            return await _orderCollection.Find(filterDefinition).SingleOrDefaultAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            // Rabbir MQ Logic
            //await _publishEndpoint.Publish<OrderCreated>(new
            //{
            //    Id = 1,
            //    orderDto.ProductName,
            //    orderDto.Quantity,
            //    orderDto.Price
            //});
            //return Ok();
            await _orderCollection.InsertOneAsync(order);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filterDefinition, order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(string id)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, id);
            await _orderCollection.DeleteOneAsync(filterDefinition);
            return Ok();
        }
    }
}
