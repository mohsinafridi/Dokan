using Dokan.Service.Book.Interfaces;
using Dokan.Service.Book.Services;
using JWTManager;
using Microsoft.Extensions.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        builder => {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});

builder.Services.AddTransient<IBookService, BookService>();
var myAppSettings = builder.Configuration.Get<DatabaseSettings>();

// builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MyKey"));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);


// JWT Extension.
builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();
app.UseCors("AllowOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
