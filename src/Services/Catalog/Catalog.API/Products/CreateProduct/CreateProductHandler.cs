using MediatR;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string name, List<string> Categories, string Description, string ImagePath, decimal Price) : IRequest<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
