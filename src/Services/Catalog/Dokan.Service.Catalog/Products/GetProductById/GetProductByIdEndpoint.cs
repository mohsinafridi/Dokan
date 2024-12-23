

namespace Dokan.Service.Catalog.Products.GetProductById;

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (ISender sender, Guid id) =>
        {
            var query = new GetProductByIdQuery(id);

            var result = await sender.Send(query);
            
            var response = result.Adapt<GetProductByIdResponse>();
            
            return Results.Ok(response);

        }).WithName("GetProductById")
          .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .WithSummary("Get a Product by Id")
          .WithDescription("Get a Product by Id from the catalog");
    }
}
