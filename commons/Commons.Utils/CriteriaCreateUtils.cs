using System;

namespace Commons.Utils
{
    /// <summary>
    /// функции для создания критериев в запросах
    /// </summary>
    public class CriteriaCreateUtils
    {
        /// <summary>
        /// создаем дату "ОТ" без времени
        /// </summary>
        /// <param name="from">дата "ОТ"</param>
        /// <returns></returns>
        public static DateTime CorrectFromDate(DateTime from)
        {
            if (from == DateTime.MinValue) return DateTime.MinValue;
            return new DateTime(from.Year, from.Month, from.Day);
        }

        /// <summary>
        /// создаем дату "ДО" за 1мс до 24часов
        /// </summary>
        /// <param name="to">дата "ДО"</param>
        /// <returns></returns>
        public static DateTime CorrectToDate(DateTime to)
        {
            if (to == DateTime.MinValue) return DateTime.MinValue;
            return (new DateTime(to.Year, to.Month, to.Day).AddDays(1)).AddMilliseconds(-1);
        }
    }
}
