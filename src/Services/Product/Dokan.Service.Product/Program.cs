using Dokan.Service.Product.Models;
using Dokan.Service.Product.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductService>();

var connectionString = builder.Configuration.GetConnectionString("DokanDB");

builder.Services.AddDbContextPool<ProductDbContext>(option =>
option.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Version
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;

    config.ApiVersionReader = ApiVersionReader
    .Combine(new UrlSegmentApiVersionReader(),
      new HeaderApiVersionReader("x-api-version"),
      new MediaTypeApiVersionReader("x-api-version"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
