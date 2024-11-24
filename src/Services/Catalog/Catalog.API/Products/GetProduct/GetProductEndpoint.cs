
namespace Catalog.API.Products.GetProduct;

public record GetProductResponse(string name, List<string> Categories, string Description, string ImagePath, decimal Price);


//public class GetProductEndpoint : ICarterModule
//{
//    public void AddRoutes(IEndpointRouteBuilder app)
//    {
//        throw new NotImplementedException();
//    }
//}
