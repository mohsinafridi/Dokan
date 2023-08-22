using Dokan.Service.Product.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dokan.Service.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _dbContext;
        public ProductService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Models.Product> AddProduct(Models.Product product)
        {
            var result = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

       

        public async Task<Models.Product> GetProductById(int id)
        {
            var product = await _dbContext
                .Products
                .Where(x => x.ProductId == id)
                .FirstOrDefaultAsync();

            return product == null ? new Models.Product() : product;
        }

        public async Task<IEnumerable<Models.Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();           
        }

        public async Task<Models.Product> UpdateProduct(Models.Product product)
        {
             _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
        public bool DeleteProduct(int Id)
        {
            //var filteredData = _dbContext.Products.Where(x => x.ProductId == Id).FirstOrDefault();
            //var result = _dbContext.remove(filteredData);
            //_dbContext.SaveChanges();
            return true;
        }
    }
}
