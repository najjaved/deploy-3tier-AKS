using Basic3Tier.Core;
using Basic3Tier.Infrastructure;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1) Pull the connection string from appsettings.json, 
//    OR from environment variable named "ConnectionStrings__Basic3Tier" if present.
var connString = builder.Configuration.GetValue<string>("ConnectionStrings:Basic3Tier");

// 2) Extension method from your Basic3Tier.Core to inject DbContext
builder.Services.AddDbContext(connString);

// 3) Repositories/Services
builder.Services.AddRepositories();
builder.Services.AddServices();

// 4) Enable CORS for UI to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

var app = builder.Build();

// 5) Actually apply the CORS policy
app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

// 6) Run DB Migrations on Startup (if needed)
app.Services.RunMigration();

// 7) Swagger/SwaggerUI
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

// 8) HTTPS redirection (optional; it can stay)
app.UseHttpsRedirection();

// 9) Authorization if needed
app.UseAuthorization();

// 10) Map controllers
app.MapControllers();

// 11) Run
app.Run();
