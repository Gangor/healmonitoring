using System;
using System.Collections.Generic;
using System.Text;

namespace HealMonitoring.DB.Extends
{
    public static class DateTimeExtends
    {
        public static double ToUnixTime(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
