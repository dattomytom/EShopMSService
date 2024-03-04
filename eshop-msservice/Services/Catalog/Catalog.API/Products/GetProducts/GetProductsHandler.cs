
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery():IQuery<GetProductsReslut>;
    public record GetProductsReslut(IEnumerable<Product> Products);
    internal class GetProductsHandler(IDocumentSession session, ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsReslut>
    {
        public async  Task<GetProductsReslut> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsHandler.Handle call with query :  {@Query}", query);    
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductsReslut(products);
        }
    }
}
