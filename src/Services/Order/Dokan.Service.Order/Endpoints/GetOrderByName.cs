using Order.Application.Orders.Queries.GetOrderByName;

namespace Dokan.Service.Order.Endpoints;

// public record  GetOrderByNameRequest(string Name);
public record GetOrderByNameResponse (IEnumerable<OrderDto> Orders);
public class GetOrderByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet( "/orders/{name}", async (string orderName , ISender sender) =>
        {
            var query = new GetOrdersByNameQuery(orderName);

            var result = await sender.Send(query);
            
            var response = new GetOrderByNameResponse(result.Adapt<IEnumerable<OrderDto>>());
            
            return Results.Ok(response);
        }).WithName("GetOrderByName")
        .Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Get Order by Name");
    }
}
