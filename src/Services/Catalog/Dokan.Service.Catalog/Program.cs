var builder = WebApplication.CreateBuilder(args);

// Add Services to the Container
builder.Services.AddCarter();
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    // options.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

var app = builder.Build();


// Configure the HTTP request pipeline

app.MapCarter();

app.Run();
