
using JasperFx.CodeGeneration.Frames;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);
            if(cacheBasket == null) 
            {
                var basket = await basketRepository.GetBasket(userName, cancellationToken);
                if (basket == null) return new ShoppingCart();
                await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
                return basket;
            }
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
            
        }

        public async Task<string> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            var userName = await basketRepository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return userName;
        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasket(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }

    }
}
