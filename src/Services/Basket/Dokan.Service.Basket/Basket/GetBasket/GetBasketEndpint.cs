
namespace Dokan.Service.Basket.Basket.GetBasket;

// public record GetBasketQuery(string UserName);


public record GetBasketResponse(ShoppingCart Cart);
public class GetBasketEndpint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        }).WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get the basket of a user")
        .WithDescription("Get the basket of a user by providing the user name");
    }
}
