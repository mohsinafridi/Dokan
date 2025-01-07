using Order.Application.Orders.Commands.CreateOrder;

namespace Dokan.Service.Order.Endpoints;



// Accept CreateOrderRequest
// Maps TO CreateOrderCommand
// Use Medaitor to send command to handler
// Return Repsonse.

public record CreateOrderRequest(OrderDto OrderDto);

public record CreateOrderResponse(Guid Id);
public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request,ISender sender) =>
        {
            
            var command = request.Adapt<CreateOrderCommand>();
            
            var result = await sender.Send(command);
            
            var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/orders/{response.Id}", response);
        }).WithDescription("Create a new order").WithName("CreateOrder")
        .Produces<CreateOrderResponse>(201).ProducesProblem(400).ProducesProblem(500);
    }
}
