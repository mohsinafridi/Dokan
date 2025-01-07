using Order.Application.Orders.Queries.GetOrdersByCustomer;

namespace Dokan.Service.Order.Endpoints;


public record class GetOrdersByCustomerResponse(IEnumerable<OrderDto> OrderDtos);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var query = new GetOrdersByCustomerQuery(customerId);

            var result = await sender.Send(query);

            var response = new GetOrdersByCustomerResponse(result.Adapt<IEnumerable<OrderDto>>());

            return Results.Ok(response);
        }).WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders by Customer");
    }
}
