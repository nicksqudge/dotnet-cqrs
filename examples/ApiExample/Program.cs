using ApiExample.CQRS.Commands.SaveProduct;
using ApiExample.Database;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddDotnetCQRS()
    .AddHandlersFromAssembly<SaveProductCommand>()
    .AddDbContext<ExampleDbContext>(options => options.UseSqlite("Data Source=CQRSExample.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
