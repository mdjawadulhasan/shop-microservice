namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string name, List<string> Categories, string Description, string ImagePath, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
       

        var product = new Product
        {
            Name = command.name,
            Categories = command.Categories,
            Description = command.Description,
            ImagePath = command.ImagePath,
            Price = command.Price,
        };


        return new CreateProductResult(Guid.NewGuid());
    }
}
