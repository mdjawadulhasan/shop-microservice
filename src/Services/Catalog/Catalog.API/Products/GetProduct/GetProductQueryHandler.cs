namespace Catalog.API.Products.GetProduct;

public record GetProductResult(IEnumerable<Product> Products);

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;

internal class GetProductQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var prodcuts = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductResult(prodcuts);
    }
}
 

