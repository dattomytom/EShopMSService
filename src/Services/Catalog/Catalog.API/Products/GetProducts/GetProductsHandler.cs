
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) :IQuery<GetProductsReslut>;
    public record GetProductsReslut(IEnumerable<Product> Products);
    internal class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsReslut>
    {
        public async  Task<GetProductsReslut> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {    
            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1,query.PageSize ?? 10,cancellationToken);
            return new GetProductsReslut(products);
        }
    }
}
