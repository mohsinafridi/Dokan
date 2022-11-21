using Dokan.Service.Ordering.Consumers;
using MassTransit;
using JWTManager;
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Add services to the container.
//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<TicketConsumer>();
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
//    {        
//        cfg.Host(new Uri("rabbitmq://localhost"), h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });
//        cfg.ReceiveEndpoint("ticketQueue", ep =>
//        {
//            ep.PrefetchCount = 16;
//            ep.UseMessageRetry(r => r.Interval(2, 100));
//            ep.ConfigureConsumer<TicketConsumer>(provider);
//        });
//    }));
//});
//builder.Services.AddMassTransit(x =>
//{
//    x.UsingRabbitMq();
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomJwtAuthentication();

// builder.Services.AddMassTransitHostedService(); // ignore for newer version.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
