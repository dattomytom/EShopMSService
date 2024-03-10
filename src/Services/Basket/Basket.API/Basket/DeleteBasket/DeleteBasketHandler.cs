using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string userName):ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBaseketCommandValidator:AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBaseketCommandValidator()
        {
            RuleFor(x => x.userName).NotEmpty().WithMessage("DeleteProductCommandHandle Need Product Id");
        }
    }
    public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(command.userName, cancellationToken);
            return new DeleteBasketResult(true);
        }
    }
}
