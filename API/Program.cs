using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
// NOTE: the WithMethods method has been added by myself in order to put my browser to rest.
// If the allowed methods were not explicitly mentioned, I would get CORS errors in both Firefox and Chrome.
app.UseCors(builder => builder
    .AllowAnyHeader()
    .WithMethods("PUT", "DELETE", "GET", "OPTIONS")
    .WithOrigins("https://localhost:4200"));

app.UseAuthentication(); // "Do you have a valid token?"
app.UseAuthorization(); // "Does this token authorize you to get access?"

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration.");
}

app.Run();
