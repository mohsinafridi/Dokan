using Order.Application.Orders.Commands.UpdateOrder;

namespace Dokan.Service.Order.Endpoints;

public record UpdateOrderRequest(OrderDto OrderDto);

public record UpdateOrderResponse(bool ISuccess);
public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {

            var command = request.Adapt<UpdateOrderCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateOrderResponse>();

            return Results.Ok(response);

        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update an order")
        .WithDescription("Update an order");
    }
}
