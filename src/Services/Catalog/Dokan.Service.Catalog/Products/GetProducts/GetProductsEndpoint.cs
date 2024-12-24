namespace Dokan.Service.Catalog.Products.GetProducts;

public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> products);


public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, [AsParameters] GetProductRequest request) =>
        {
            var query = request.Adapt<GetProductsQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductsResponse>();
            
            return Results.Ok(response);
        }).WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Get all products")
        .WithDescription("Get all products in the catalog");
    }
}
