using Dokan.Service.Notes.Models;
using Dokan.Service.Notes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<NoteDatabaseSetting>(builder.Configuration.GetSection("NoteDatabaseSetting"));


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

app.UseAuthorization();

app.MapControllers();

app.Run();
