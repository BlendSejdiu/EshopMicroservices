using Ordering.Application;
using Ordering.Infrastructure;
using OrderingAPI;

var builder = WebApplication.CreateBuilder(args);

//Add services before build.
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddAPIServices();

var app = builder.Build();

//Configure for HTTP pipeline.

app.Run();
