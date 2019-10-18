using System;

namespace AspCoreMicroservice.Core
{
    public static class DateTimeExtensions
    {
        private static readonly string[] Days = new string[] { "sun", "mon", "tue", "wed", "thu", "fri", "sat" };

        public static string ToShortDayOfWeek(this DateTime date)
        {
            return Days[(int) date.DayOfWeek];
        }
    }
}
