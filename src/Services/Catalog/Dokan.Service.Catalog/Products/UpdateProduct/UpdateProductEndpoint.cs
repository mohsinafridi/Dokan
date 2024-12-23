
namespace Dokan.Service.Catalog.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;


public record UpdateProductResponse(bool IsSuccess);
internal class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (UpdateProductRequest request, ISender sender,CancellationToken cancellationToken) =>
        {
            var command  = request.Adapt<UpdateProductCommand>();

            var result = await sender.Send(request, cancellationToken);

            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        }).
        WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Update a product")
        .WithSummary("Update a product");
    }
}
