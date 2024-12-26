using RedisRateLimitingAPI.Services;

namespace RedisRateLimitingAPI.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RateLimiterService _rateLimiter;

        public RateLimitingMiddleware(RequestDelegate next, RateLimiterService rateLimiter)
        {
            _next = next;
            _rateLimiter = rateLimiter;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userID = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(userID)||!await _rateLimiter.IsReqAllowedAsync(userID))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too Many Requests. Try again later");
                return;
            }
            await _next(context);
        }
    }
}
