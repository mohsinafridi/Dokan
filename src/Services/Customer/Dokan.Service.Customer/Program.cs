using Dokan.Service.Customer.Interfaces;
using Dokan.Service.Customer.Models;
using Dokan.Service.Customer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//DI

// builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));


//builder.Services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);


var cs = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(cs));

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseAuthorization();

app.MapControllers();

app.Run();
