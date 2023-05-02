using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Web.Services.Stripe;

namespace MyPizza.Web.Services.TimeService
{
    public class TimeService : ITimeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<TimeService> _logger;
        public TimeService(IHttpContextAccessor httpContextAccessor,
            ILogger<TimeService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public int GetTimezoneOffset()
        {            
            var timeZoneOffset = _httpContextAccessor.HttpContext!.Request.Cookies.ContainsKey("timezoneoffset") ?
                                 int.Parse(_httpContextAccessor.HttpContext!.Request.Cookies["timezoneoffset"]!) : 0;
            _logger.LogInformation($"Timezoneoffset {timeZoneOffset} get successfully!");
            return timeZoneOffset;
        }
    }
}
