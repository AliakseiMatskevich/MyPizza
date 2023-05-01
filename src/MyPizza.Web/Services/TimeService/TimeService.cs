using MyPizza.ApplicationCore.Interfaces;

namespace MyPizza.Web.Services.TimeService
{
    public class TimeService : ITimeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TimeService(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;

        }
        public int GetTimezoneOffset()
        {
            var timeZoneOffset = _httpContextAccessor.HttpContext!.Request.Cookies.ContainsKey("timezoneoffset") ?
                                 int.Parse(_httpContextAccessor.HttpContext!.Request.Cookies["timezoneoffset"]!) : 0;
            return timeZoneOffset;
        }
    }
}
