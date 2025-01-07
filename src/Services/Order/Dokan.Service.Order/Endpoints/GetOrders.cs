using BuildingBlocks.Pagination;
using Order.Application.Orders.Queries.GetOrders;

namespace Dokan.Service.Order.Endpoints;

public record GetOrdersRequest(PaginationRequest PaginationRequest);
public record  GetOrdersResponse(PaginatedResult<OrderDto> Orders);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var query = new GetOrdersQuery(request);

            var result = await sender.Send(query);

            var response = new GetOrdersResponse(result.Adapt<PaginatedResult<OrderDto>>());

            return Results.Ok(response);

        }).WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders");
    }
}
