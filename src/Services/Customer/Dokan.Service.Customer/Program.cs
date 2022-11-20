using Dokan.Service.Customer.Interfaces;
using Dokan.Service.Customer.Models;
using Dokan.Service.Customer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//DI

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);

var DbHost = Environment.GetEnvironmentVariable("DB_HOST");
var DbName = Environment.GetEnvironmentVariable("DB_NAME");

//var connectionString = $"Data Source=MOHSIN\\SQLEXPRESS;Initial Catalog=CustomerDb;Integrated Security=True;"; // WITHOUT DOCKER COMPOSE
//var connectionString = $"Data Source={DbHost}; Initial Catalog={DbName}";
//builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("CustomerDbCS")));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseAuthorization();

app.MapControllers();

app.Run();
