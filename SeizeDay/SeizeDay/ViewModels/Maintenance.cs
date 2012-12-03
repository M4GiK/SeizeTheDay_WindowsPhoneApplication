using System;
using System.Globalization;

namespace SeizeDay.ViewModels
{
    public static class Maintenance
    {
        /// <summary>
        /// This method changing date in string to object in DataTime
        /// </summary>
        /// <param name="dateString">string</param>
        /// <returns>Date in DateTime format</returns>
        public static DateTime toDate(this string dateString)
        {
            return DateTime.Parse(dateString);
        }
    }
}
