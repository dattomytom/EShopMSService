using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Add Service
//-------
//Infrastrucrure - EF Core
//Application - MediatR
//API -- Cater, HealthChecks

//Builder.Services.
//      .AddApplicationServices()
//      .AddInfrastructureServices(builder.Configuration)
//      .AddWebService();
//-------
#endregion

#region AddDependency Injection
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();
#endregion
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
