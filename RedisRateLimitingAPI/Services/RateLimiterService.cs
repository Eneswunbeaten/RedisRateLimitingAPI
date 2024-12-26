using StackExchange.Redis;

namespace RedisRateLimitingAPI.Services
{
    public class RateLimiterService
    {
        private readonly IConnectionMultiplexer _redis;
        private int _maxReq;
        private TimeSpan _timeWindow;
        public RateLimiterService(IConnectionMultiplexer redis, int maxReq, TimeSpan timeWindow)
        {
            _redis = redis;
            _maxReq = maxReq;
            _timeWindow = timeWindow;
        }
        public async Task<bool> IsReqAllowedAsync(string userID)
        {
            var db = _redis.GetDatabase();
            var key = $"ratelimit:{userID}";
            var currentCount = await db.StringGetAsync(key);
            if (currentCount.IsNullOrEmpty)
            {
                await db.StringSetAsync(key, 1, _timeWindow);
                return true;
            }
            if (int.TryParse(currentCount, out int count) && count < _maxReq)
            {
                await db.StringIncrementAsync(key);
                return true;
            }

            return false;
        }
    }
}
