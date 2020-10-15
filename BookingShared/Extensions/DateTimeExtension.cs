using System;
using System.Collections.Generic;
using System.Text;

namespace BookingShared.Extensions
{
    public static class DateTimeExtension
    {
        public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }
    }
}
