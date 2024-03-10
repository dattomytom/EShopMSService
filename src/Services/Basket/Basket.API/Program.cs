using Basket.API.Data;
using Basket.API.Models;
using BuildingBLocks.Behaviors;
using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);
//Add service to the container

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LogingBehavior<,>));
});

builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("Database")!);//! in here
    option.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();

app.MapCarter();

//Congure the Http request pepiline

app.MapGet("/", () => "Hello World!");

app.Run();
