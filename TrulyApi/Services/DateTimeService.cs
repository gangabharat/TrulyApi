using TrulyApi.Services.Interfaces;

namespace TrulyApi.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
