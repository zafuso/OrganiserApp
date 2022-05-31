using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Helpers
{
    public static class FormatHelper
    {
        public static string FormatISO8601ToDateTimeString(string IsoDate)
        {
            return DateTime.Parse(IsoDate).ToString("dddd, dd MMMM yyyy HH:mm tt");
        }

        public static DateTime FormatISO8601ToDateTime(string IsoDate)
        {
            return DateTime.Parse(IsoDate);
        }

        public static DateTime FormatISO8601ToDate(string IsoDate)
        {
            return DateTime.Parse(IsoDate).Date;
        }

        public static string FormatISO8601ToDateString(string IsoDate)
        {
            return DateTime.Parse(IsoDate).Date.ToString("dd-MM-yyyy HH:mm");
        }

        public static TimeSpan FormatISO8601ToTime(string IsoDate)
        {
            return DateTime.Parse(IsoDate).TimeOfDay;
        }

        public static string FormatDateTimeToISO8601String(DateTime Date, TimeSpan Time)
        {
            var DateTime = Date.Date.Add(Time);
            return DateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        public static decimal FormatPrice(decimal Price)
        {
            var roundedPrice = Math.Round(Price, 2, MidpointRounding.ToEven);
            return Convert.ToDecimal(String.Format("{0:0.00}", roundedPrice));
        }
    }
}
