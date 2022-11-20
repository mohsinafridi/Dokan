using Dokan.Service.Product.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DI
var DbHost = Environment.GetEnvironmentVariable("DB_HOST");
var DbName = Environment.GetEnvironmentVariable("DB_NAME");


//var connectionString = builder.Configuration.GetConnectionString("ProductConnection");
//builder.Services.AddDbContext<ProductDbContext>(options => options.UseMySQL(connectionString));
builder.Services.AddDbContext<ProductDbContext>(
options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("ProductConnection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});


// var connectionString = $"Data Source=MOHSIN\\SQLEXPRESS;Initial Catalog=ProductDb;Integrated Security=True;"; // WITHOUT DOCKER COMPOSE
//var connectionString = $"Data Source={DbHost}; Initial Catalog={DbName};Integrated Security=True;";


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
