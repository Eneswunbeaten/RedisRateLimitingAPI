using RedisRateLimitingAPI.Middlewares;
using RedisRateLimitingAPI.Services;
using StackExchange.Redis;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration["Redis:ConnectionString"]);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddSingleton<RateLimiterService>(serviceProvider =>
{
    var redis = serviceProvider.GetRequiredService<IConnectionMultiplexer>();
    int maxReq = 3;
    TimeSpan timeWindow = TimeSpan.FromSeconds(10);
    return new RateLimiterService(redis, maxReq, timeWindow);
}); 
// builder.Services.AddSingleton<RateLimitingMiddleware>();

var app = builder.Build();
app.UseMiddleware<RateLimitingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
