using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;
using OrderingAPI;

var builder = WebApplication.CreateBuilder(args);

//Add services before build.
builder.Services.AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddAPIServices(builder.Configuration);

var app = builder.Build();

//Configure for HTTP pipeline.

app.UseAPIServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDbAsync();
}

app.Run();
