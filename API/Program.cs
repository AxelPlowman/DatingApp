using API.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().WithOrigins("https://localhost:4200"));

app.UseAuthentication(); // "Do you have a valid token?"
app.UseAuthorization(); // "Does this token authorize you to get access?"

app.MapControllers();

app.Run();
