

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImagePath, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ImagePath).NotEmpty().WithMessage("ImagePath is required");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
    }
}

internal class CreateProductHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImagePath = command.ImagePath,
            Price = command.Price,
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
