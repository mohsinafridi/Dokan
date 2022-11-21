using Dokan.Service.Ordering.DTOs;
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
        private readonly IMongoCollection<Models.Order> _orderCollection;
        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            var dbHost = "";
            var dbName = "";
            var cs = "mongodb+srv://mohsinafridi91:Prompt7788@prompt-cluster.ho9semy.mongodb.net/?retryWrites=true&w=majority";

            var mongoUrl = MongoUrl.Create(cs);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase("PromptDb");
            _orderCollection = database.GetCollection<Models.Order>("order");
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
