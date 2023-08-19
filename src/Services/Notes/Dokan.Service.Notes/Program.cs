using Dokan.Service.Notes.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Services
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<INoteService, NoteService>();
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

var app = builder.Build();
app.UseCors("AllowOrigin");
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
