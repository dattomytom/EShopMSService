namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var Basket = session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            return Basket is null ? throw new BasketNotFoundException(userName) : Basket;
        }

        public async Task<string> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
           session.Store(cart);
           await session.SaveChangesAsync(cancellationToken);
           return cart.UserName;
        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            if(cart is null)
            {
                throw new BasketNotFoundException(userName);
            }
            session.Delete(cart);
            await session.SaveChangesAsync();
            return true;
        }
    }
}
