var builder = WebApplication.CreateBuilder(args);
//Add service to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

//configuration the Http request pipeline
app.MapCarter();

app.Run();
