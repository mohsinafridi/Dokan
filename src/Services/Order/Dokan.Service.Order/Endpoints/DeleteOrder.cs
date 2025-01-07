
using Order.Application.Orders.Commands.DeleteOrder;

namespace Dokan.Service.Order.Endpoints;

public record class DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
        {
            var command = new DeleteOrderCommand(Id);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        }).WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest).
        WithDescription("Delete an order");
    }
}
