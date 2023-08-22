namespace Dokan.Service.Product.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Models.Product>> GetProducts();
        public Task<Models.Product> GetProductById(int id);
        public Task<Models.Product> AddProduct(Models.Product product);
        public Task<Models.Product> UpdateProduct(Models.Product product);
        public bool DeleteProduct(int Id);
    }
}
