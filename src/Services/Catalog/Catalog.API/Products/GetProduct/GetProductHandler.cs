
namespace Catalog.API.Products.GetProduct;

public record GetProductResult(IEnumerable<Product> Products);

public record GetProductsQuery() : IQuery<GetProductResult>;

internal class GetProductHandler : IQueryHandler<GetProductsQuery, GetProductResult>
{
    public Task<GetProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


