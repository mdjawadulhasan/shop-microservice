namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public Guid ProductId { get; }

    public ProductNotFoundException(Guid productId)
        : base($"Product with Id {productId} not found.")
    {
        ProductId = productId;
    }
}
