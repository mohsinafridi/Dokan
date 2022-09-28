using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models;

namespace Dokan.Service.Ticketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
    //    private readonly IBus _bus;
        public TicketController()
        {
           
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("OK");
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateTicket(Ticket ticket)
        //{
        //    if (ticket != null)
        //    {
        //        ticket.BookedOn = DateTime.Now;
        //        Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
        //        var endPoint = await _bus.GetSendEndpoint(uri);
        //        await endPoint.Send(ticket);
        //        return Ok();
        //    }
        //    return BadRequest();
        //}
    }
}
