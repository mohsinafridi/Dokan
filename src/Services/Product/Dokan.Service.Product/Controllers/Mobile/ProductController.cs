using Dokan.Service.Product.Models;
using Dokan.Service.Product.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dokan.Service.Product.Controllers.Mobile
{
    // [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]    
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]        

        public async Task<ActionResult<IEnumerable<Models.Product>>> Get()
        {
            var products = await _productService.GetProducts();
            return new OkObjectResult(products);
        }

        [HttpGet("{Id}")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<Models.Product>> Get(int Id)
        {
            var product = await _productService.GetProductByIdV2(Id);
            return new OkObjectResult(product);
        }
        

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Models.Product product)
        {
            Models.Product productObj = await _productService.AddProduct(product);
            return Ok(productObj);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Models.Product product)
        {
            var updatedProduct = await _productService.UpdateProduct(product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteProduct(int Id)
        {
            var product = _productService.DeleteProduct(Id);
            if (product)
            {
                return Ok();
            }
            return Ok();
        }
    }
}
