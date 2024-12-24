using Marten.Schema;

namespace Dokan.Service.Catalog.Data;
public class CatalogInitialData : IInitialData
{   
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        // Marten UPSERT will cater for existing records
        session.Store<Product>(GetProductArray());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetProductArray() => new List<Product>()
    {
        new Product()
        {
            Id = new Guid("f5b3f9c4-3b0d-4b5d-8b0d-3b0d4b5d8b0d"),
            Name = "IPhone 15 Prox Max",
            Description = "Mobile Products",
            ImageFile = "ipone15.png",
            Price = 1000,
            Category = new List<string>{"Smart Phone"}
        },
        new Product()
        {
            Id = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
            Name = "Lamp",
            Description = "Lamp Products",
            ImageFile = "lamp.png",
            Price = 1000,
            Category = new List<string>{"Home"}
        },
         new Product()
                {
                    Id = new Guid("c4bbc4a2-4555-45d8-97cc-2a99b2167bff"),
                    Name = "LG G7 ThinQ",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-6.png",
                    Price = 240.00M,
                    Category = new List<string> { "Home Kitchen" }
                },
         new Product()
                {
                    Id = new Guid("93170c85-7795-489c-8e8f-7dcf3b4f4188"),
                    Name = "Panasonic Lumix",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-6.png",
                    Price = 240.00M,
                    Category = new List<string> { "Camera" }
                }
    };
}


