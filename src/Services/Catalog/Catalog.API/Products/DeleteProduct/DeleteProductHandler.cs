namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductHandler
    (IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle called with {@request}", command);

        var product = await session.Query<Product>().FirstOrDefaultAsync(x => x.Id == command.Id);

        if (product == null) throw new  ProductNotFoundException(command.Id);

        session.Delete(product);
        await session.SaveChangesAsync();

        return new DeleteProductResult(true);
    }
}
