using ApiExample.ApplicationLayer.Commands.SaveProduct;
using ApiExample.DataAccessLayer;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddDotnetCqrs()
    .AddHandlersFromAssembly<SaveProductCommand>()
    .AddDbContext<ExampleDbContext>(options => options.UseSqlite("Data Source=CQRSExample.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();