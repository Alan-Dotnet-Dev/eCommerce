using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/myapp-.txt", rollingInterval: RollingInterval.Day) // Log to a text file
    .CreateLogger();

builder.Services.AddLogging(builder =>
{
    builder.AddSerilog(); // Integrate Serilog with ASP.NET Core logging
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>(); // Specify the class where logging is being used

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception error)
{
    logger.LogError(error, "An error occurred during migration");
}

app.Run();
