using Dokan.Service.Product.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dokan.Service.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }
        [HttpGet]


        [HttpGet]
        public ActionResult<IEnumerable<Models.Product>> Get()
        {
            return _context.Products;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Models.Product>> Get(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Models.Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Models.Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProduct(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
