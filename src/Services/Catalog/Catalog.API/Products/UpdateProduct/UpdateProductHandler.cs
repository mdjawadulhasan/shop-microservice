namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductResult(bool IsSuccess);

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImagePath, decimal Price)
    : ICommand<UpdateProductResult>;


internal class UpdateProductHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Categories = command.Categories;
        product.Description = command.Description;
        product.ImagePath = command.ImagePath;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
