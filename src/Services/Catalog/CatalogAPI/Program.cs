using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// add services to the container 
builder.Services.AddMediatR(config => 
    { 
        config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        config.AddOpenBehavior(typeof(ValidationBehaviour<,>)); 
    });
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddCarter();
builder.Services.AddMarten(options => { 
    options.Connection(builder.Configuration.GetConnectionString("Database")!); 

}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

// Configure HTTP REQUEST pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();