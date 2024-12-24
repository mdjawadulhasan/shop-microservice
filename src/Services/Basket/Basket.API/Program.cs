namespace Basket.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddCarter();
      
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        builder.Services.AddMarten(opt =>
        {
            opt.Connection(builder.Configuration.GetConnectionString("Database")!);
            opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);

        }).UseLightweightSessions();

        builder.Services.AddScoped<IBasketRepository, BasketRepository>();

        var app = builder.Build();

        app.UseExceptionHandler(options => { });
        app.UseStatusCodePages();

        app.MapCarter();

        app.Run();
    }
}
