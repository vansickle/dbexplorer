using System;

namespace Commons.Utils
{
    public class EnumUtils
    {
        public static T NumToEnum<T>(int number)
        {
            return (T) Enum.ToObject(typeof (T), number);
        }

        /// <summary>
        /// Checks variable is contained in values array
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsContainedIn(Enum variable, params Enum[] values)
        {
            foreach (Enum value in values)
            {
                if(variable.Equals(value))
                    return true;
            }
            return false;
        }
    }
}
