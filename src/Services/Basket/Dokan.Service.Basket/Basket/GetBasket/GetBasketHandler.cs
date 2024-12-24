namespace Dokan.Service.Basket.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);
public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {

        var basket = await basketRepository.GetBasket(query.UserName);

        return new GetBasketResult(basket);
    }
}
