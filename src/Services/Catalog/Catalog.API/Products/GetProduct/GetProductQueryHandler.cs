
namespace Catalog.API.Products.GetProduct;

public record GetProductResult(IEnumerable<Product> Products);

public record GetProductsQuery() : IQuery<GetProductResult>;

internal class GetProductQueryHandler
    (IDocumentSession session, ILogger<GetProductQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var prodcuts = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductResult(prodcuts);
    }
}


