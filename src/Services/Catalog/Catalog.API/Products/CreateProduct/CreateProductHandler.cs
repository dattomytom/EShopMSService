using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile,decimal Price)
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is request");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is request");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is request");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageName is request");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }

    }

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create Product
            //save Db
            //return CreateProductResult result
            //var result = await validator.ValidateAsync(command, cancellationToken);
            //var error = result.Errors.Select(e => e.ErrorMessage).ToList();
            //if(error.Any())
            //{
            //    throw new ValidationException(error.FirstOrDefault());
            //}
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price

            };
            session.Store(product);
            await session.SaveChangesAsync();
           return new CreateProductResult(product.Id);  
        }
    }
}
