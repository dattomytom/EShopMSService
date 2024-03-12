using Basket.API.Data;
using Discount.Grpc;

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

    public class StoreBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
        : ICommandHandler<StoreBasketCommand, StoreBaketResponse>
    {
        public async Task<StoreBaketResponse> Handle(StoreBasketCommand comand, CancellationToken cancellationToken)
        {
            //Grpc Client
            ShoppingCart cart = comand.Cart;
            //discountProto.GetDiscountAsync()
            await DeductDiscount(cart, cancellationToken);
            await repository.StoreBasket(cart, cancellationToken);
            return new StoreBaketResponse(cart.UserName);
        }
        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
    
}
