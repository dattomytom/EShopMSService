
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id)
        :ICommand<DeleteProductResult>;
    public record DeleteProductResult(Product Product);
    public class DeleteProductHandler(IDocumentSession session,ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand query, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductHandler call query {0}", query);
            var product = await session.LoadAsync<Product>(query.id, cancellationToken);
            if(product==null)
            {
                throw new ProductNotFoundException();
            }
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(product);
        }
    }
}
