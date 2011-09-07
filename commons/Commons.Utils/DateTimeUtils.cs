using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Utils
{
    public class DateTimeUtils
    {
        public static Quarter GetQuarter(int Month)
        {
            if (Month <= 3)
                // 1st Quarter = January 1 to March 31
                return Quarter.First;
            if ((Month >= 4) && (Month <= 6))
                // 2nd Quarter = April 1 to June 30
                return Quarter.Second;
            if ((Month >= 7) && (Month <= 9))
                // 3rd Quarter = July 1 to September 30
                return Quarter.Third;
            // 4th Quarter = October 1 to December 31
                return Quarter.Fourth;
        }

        public static YearHalf GetYearHalf(DateTime dateTime)
        {
            if (dateTime.Month > 6) return YearHalf.Second;
            return YearHalf.First;
        }

        public static bool IsRealDateTime(DateTime? value)
        {
            return value == null || value == DateTime.MinValue || value == DateTime.MaxValue;
        }
    }
}
