namespace Dokan.Service.Catalog.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Product not found!")
        {
                
        }
    }
}
