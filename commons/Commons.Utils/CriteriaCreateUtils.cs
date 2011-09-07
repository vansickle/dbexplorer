using System;

namespace Commons.Utils
{
    /// <summary>
    /// ������� ��� �������� ��������� � ��������
    /// </summary>
    public class CriteriaCreateUtils
    {
        /// <summary>
        /// ������� ���� "��" ��� �������
        /// </summary>
        /// <param name="from">���� "��"</param>
        /// <returns></returns>
        public static DateTime CorrectFromDate(DateTime from)
        {
            if (from == DateTime.MinValue) return DateTime.MinValue;
            return new DateTime(from.Year, from.Month, from.Day);
        }

        /// <summary>
        /// ������� ���� "��" �� 1�� �� 24�����
        /// </summary>
        /// <param name="to">���� "��"</param>
        /// <returns></returns>
        public static DateTime CorrectToDate(DateTime to)
        {
            if (to == DateTime.MinValue) return DateTime.MinValue;
            return (new DateTime(to.Year, to.Month, to.Day).AddDays(1)).AddMilliseconds(-1);
        }
    }
}
