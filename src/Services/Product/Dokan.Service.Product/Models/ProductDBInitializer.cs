using System.Data.Entity;
using System.Diagnostics;

namespace Dokan.Service.Product.Models
{
    public class ProductDBInitializer : DropCreateDatabaseAlways<ProductDbContext>
    {
        protected override void Seed(ProductDbContext context)
        {
            IList<Product> products = new List<Product>();

            products.Add(new Product() 
            { 
                ProductId=1,ProductName = "IPhone", 
                ProductCode= "I-001",Price=5000 ,
                ProductDescription = "IPhone Mobile",ProductStock = 23
            });

            products.Add(new Product() {
                ProductId = 1, ProductName = "IPhone", ProductCode = "I-001", Price = 5000, ProductDescription = "IPhone Mobile", ProductStock = 23 });


            context.Products.AddRange(products);
            base.Seed(context);
        }
    }
}
