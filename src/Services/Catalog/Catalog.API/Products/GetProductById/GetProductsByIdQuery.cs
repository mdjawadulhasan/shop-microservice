
namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Products);
public record GetProductsByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public class GetProductsByIdQueryHandler
    (IDocumentSession session, ILogger<GetProductsByIdQueryHandler> logger) : IQueryHandler<GetProductsByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsByIdQueryHandler.Handle called with {@Query}", query);

        var product = await session.Query<Product>().FirstOrDefaultAsync(x => x.Id == query.Id);

        if (product == null) throw new ProductNotFoundException(query.Id);

        return new GetProductByIdResult(product);
    }
}
