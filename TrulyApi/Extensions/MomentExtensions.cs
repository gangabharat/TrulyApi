using moment;
using moment.net;

namespace TrulyApi.Extensions
{
    public static class MomentExtensions
    {
        public static string InHuman(this DateTime date)
        {
            return date.FromNow();
        }
    }
}
