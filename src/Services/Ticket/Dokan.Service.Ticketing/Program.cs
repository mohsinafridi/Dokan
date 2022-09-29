using MassTransit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
      x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
      {
         // config.UseHealthCheck(provider);
          config.Host(new Uri("rabbitmq://localhost"), h =>
          {
              h.Username("guest");
              h.Password("guest");
          });
      }))
      
      );


//builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
