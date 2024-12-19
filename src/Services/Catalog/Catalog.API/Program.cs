using BuildingBlocks.Behaviours;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});


var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler((exceptionHandler) =>
{
    exceptionHandler.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null)
        {
            return;
        }

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace
        };

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, exception.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    });

});

app.Run();
