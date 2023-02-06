using Dokan.Service.Customer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dokan.Service.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;

        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }
        [HttpGet]


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Customer>>> Get()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers.Count == 0)
                return NotFound();

            return Ok(customers);
        }
   
        [HttpGet("{Id}")]
        public async Task<ActionResult<Models.Customer>> Get(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            if (customer?.CustomerId != Id)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(Models.Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer(Models.Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCustomer(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

    }
}
