using BuildingBlocks.Exceptions.Handler;
using Marten;

var builder = WebApplication.CreateBuilder(args);


// Add Services here
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);   // make username as identity column
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();


// Configure the HTTP request pipeline.


app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
