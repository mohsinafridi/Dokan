namespace Dokan.Service.Basket.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository , IDistributedCache cache) 
        : IBasketRepository
    {        
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
             var chachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if(!string.IsNullOrEmpty(chachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(chachedBasket)!;
            }
            
            var basket = await basketRepository.GetBasket(userName,cancellationToken);
            
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket),cancellationToken);

            return basket;
        }

        public async  Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasket(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }
        public async  Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }
    }
}
