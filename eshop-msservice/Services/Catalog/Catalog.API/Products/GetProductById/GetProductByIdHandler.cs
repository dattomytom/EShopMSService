
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdHandler. Handle call is query @{query}", query);
            var result = await session.LoadAsync<Product>(query.id, cancellationToken);
            if( result == null )
            {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(result);
        }
    }
}
