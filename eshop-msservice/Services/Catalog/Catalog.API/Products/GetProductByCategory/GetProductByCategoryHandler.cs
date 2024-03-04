
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category):IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQuery. Handler call query {0}", request);
            var result = await session.Query<Product>()
                .Where(p => p.Category.Contains(request.category))
                .ToListAsync();
            return new GetProductByCategoryResult(result);
        }
    }
}
