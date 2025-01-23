using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
        builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

        //builder.Services.AddScoped<IBasketRepository>(provider =>
        //{
        //    var basketRepository = provider.GetRequiredService<BasketRepository>();
        //    var cache = provider.GetRequiredService<IDistributedCache>();
        //    return new CachedBasketRepository(basketRepository, cache);
        //});

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
        });


        builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
        {
            o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);

        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            return handler;
        });

        builder.Services.AddMessageBroker(builder.Configuration);

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddHealthChecks();

        builder.Services.AddHealthChecks()
            .AddRedis(builder.Configuration.GetConnectionString("Redis")!)
            .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


        var app = builder.Build();

        app.UseExceptionHandler(options => { });
        app.UseStatusCodePages();

        app.MapCarter();

        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        app.Run();
    }
}
