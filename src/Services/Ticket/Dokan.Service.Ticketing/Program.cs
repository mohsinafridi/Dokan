using MassTransit;
using JWTManager;

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



/*
 builder.Services.AddDbContext<DataContext>( options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectiion"));
});
 */


//builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Extension.
builder.Services.AddCustomJwtAuthentication();


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
