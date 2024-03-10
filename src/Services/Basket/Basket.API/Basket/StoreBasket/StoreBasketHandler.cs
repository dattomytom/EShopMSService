using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBaketResponse>;
    public record StoreBaketResponse(string UserName);
    public class StoreBasketCommandValidator:AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is Requested");
        }
    }

    public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBaketResponse>
    {
        public async Task<StoreBaketResponse> Handle(StoreBasketCommand comand, CancellationToken cancellationToken)
        {
            ShoppingCart cart = comand.Cart;
            await repository.StoreBasket(cart, cancellationToken);
            return new StoreBaketResponse(cart.UserName);
        }
    }
}
