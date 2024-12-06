namespace Catalog.API.Products.DeleteProduct;
public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{Id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
         .WithName("DeleteProductById")
         .Produces<DeleteProductCommand>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Delete Product By Id")
         .WithDescription("Delete Product By Id");
    }
}

