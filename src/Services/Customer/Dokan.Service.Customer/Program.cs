using Dokan.Service.Customer.Interfaces;
using Dokan.Service.Customer.Models;
using Dokan.Service.Customer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));


// builder.Services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);


var cs = "Server=AEADLT19726;Database=CustomerDb;User ID=sa;Password=xxx";
 
builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(cs));

var app = builder.Build();

// Configure the HTTP request pipeline.

// app.UseAuthorization();

app.MapControllers();

app.Run();
