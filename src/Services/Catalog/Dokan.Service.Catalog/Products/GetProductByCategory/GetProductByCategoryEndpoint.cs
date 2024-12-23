
namespace Dokan.Service.Catalog.Products.GetProductByCategory;

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category,ISender sender, CancellationToken cancellationToken) =>
        {

            var query = new GetProductByCategoryQuery(category);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetProductsByCategoryResponse>();

            return Results.Ok(result.Products);
        })
            .WithName(nameof(GetProductsByCategoryResponse))
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products by category")
            .WithDescription("Get products by category");

    }
}
