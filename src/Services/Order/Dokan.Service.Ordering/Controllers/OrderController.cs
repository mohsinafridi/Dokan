﻿using Dokan.Service.Ordering.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dokan.Service.Ordering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            await _publishEndpoint.Publish<OrderCreated>(new
            {
                Id = 1,
                orderDto.ProductName,
                orderDto.Quantity,
                orderDto.Price
            });
            return Ok();
        }
    }
}