using Dokan.Service.Order;
using Order.Application;
using Order.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();


// Add Services to Container
builder.Services
    .AddApplicationService(builder.Configuration)
    .AddInfrastructureService(builder.Configuration)
    .AddApiService(builder.Configuration);

// Configure Http request pipeline

app.Run();
