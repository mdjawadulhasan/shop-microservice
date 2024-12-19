namespace Catalog.API.Products.GetProductByCategory;

public record GetProductBycatalogResult(IEnumerable<Product> Products);

public record GetProductsByCategoryQuery(string categories) : IQuery<GetProductBycatalogResult>;

public class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductBycatalogResult>
{
    public async Task<GetProductBycatalogResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().Where(x => x.Categories.Contains(query.categories)).ToListAsync();

        return new GetProductBycatalogResult(products);
    }
}