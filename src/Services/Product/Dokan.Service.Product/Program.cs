using Dokan.Service.Product.Models;
using Dokan.Service.Product.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductService>();

var connectionString = builder.Configuration.GetConnectionString("DokanDB");

builder.Services.AddDbContextPool<ProductDbContext>(option =>
option.UseSqlServer(connectionString))  ;

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
