using Dokan.Service.Notes.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register Services
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<INoteService, NoteService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
