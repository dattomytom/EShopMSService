var builder = WebApplication.CreateBuilder(args);
//Add service to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();
var app = builder.Build();

//configuration the Http request pipeline
app.MapCarter();

app.Run();
