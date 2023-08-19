using Dokan.Service.Notes.Models;
using Dokan.Service.Notes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<NoteDatabaseSetting>(builder.Configuration.GetSection("NoteDatabaseSetting"));

<<<<<<< HEAD
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
=======
>>>>>>> a295952fca6a62d543d22973b598880827e12064

builder.Services.AddSingleton<NotesService>();
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
