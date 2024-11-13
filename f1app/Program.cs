using F1App.Api.Services;

namespace F1App.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDriverRepository, DriverRepository>(x =>
    new DriverRepository(
        builder.Configuration.GetConnectionString("CosmosDb",
        builder.Configuration["CosmosConfig:primaryKey"],
        builder.Configuration["CosmosConfig:databaseName"],
        builder.Configuration["CosmosConfig:containerName"] )
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
