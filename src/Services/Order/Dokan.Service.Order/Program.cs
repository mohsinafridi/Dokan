using Dokan.Service.Order;
using Order.Application;
using Order.Infrastructure;
using Order.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Services to Container
builder.Services
    .AddApplicationService(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiService(builder.Configuration);

var app = builder.Build();

// Configure Http request pipeline
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
