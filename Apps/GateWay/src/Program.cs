using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//ocelot
builder.Configuration
    .AddJsonFile("configuration/ocelot.global.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

// Add services to the container.
builder.Services.AddOpenApi();



var app = builder.Build();


app.UseAuthentication();

app.UseOcelot().Wait();

app.Run();