using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JWTManager;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

var routes = "Routes";
builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routes;
});

// Add services to the container.

builder.Services.AddControllers();

// Add CORS for ocelot
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
    builder => builder.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(builder.Configuration).AddPolly();
builder.Services.AddSwaggerForOcelot(builder.Configuration);


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();

// 1. JWT Extension method
builder.Services.AddCustomJwtAuthentication();
var app = builder.Build();
app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
   // app.UseSwaggerUI();
}

//app.UseOcelot();
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
    //opt.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;
},
uiOption => { 
// swagger UI options
uiOption.DocumentTitle = "Dokan Documentation";
}).UseOcelot().Wait();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();