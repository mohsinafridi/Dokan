
using Dokan.Service.Customer.Models;
using Dokan.Service.Customer.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<CustomerDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(DatabaseSettings))));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));


var cs = "Server=MOHSIN\\SQLEXPRESS;Database=CustomerDb;User ID=sa;Password=Prompt@7788";
 
builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(cs));

var app = builder.Build();

// Configure the HTTP request pipeline.

// app.UseAuthorization();

app.MapControllers();

app.Run();
