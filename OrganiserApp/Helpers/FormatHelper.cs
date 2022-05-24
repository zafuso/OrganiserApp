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

        public static decimal FormatPrice(decimal Price)
        {
            var roundedPrice = Math.Round(Price, 2, MidpointRounding.ToEven);
            return Convert.ToDecimal(String.Format("{0:0.00}", roundedPrice));
        }
    }
}
