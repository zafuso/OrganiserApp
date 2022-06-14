using OrganiserApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserAppTests
{
    public class HelperTests
    {
        string IsoString = "2022-06-14T10:00:00+00:00"; // Tuesday 14th of June, 10:00 AM UTC

        [Fact]
        public void FormatISO8601ToDateTimeString_Test()
        {
            var result = FormatHelper.FormatISO8601ToDateTimeString(IsoString);

            Assert.IsType<string>(result);
            Assert.Contains("dinsdag, 14 juni", result);
        }

        [Fact]
        public void FormatISO8601ToDate_Test()
        {
            var date = new DateTime(2022, 06, 14).Date;
            var result = FormatHelper.FormatISO8601ToDate(IsoString);

            Assert.IsType<DateTime>(result);
            Assert.Equal(date, result);
        }

        [Fact]
        public void RandomString_Test()
        {
            int length = 10;
            var result = FormatHelper.RandomString(length);

            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void FormatPrice_Test()
        {
            int price = 10;
            var result = FormatHelper.FormatPrice(price);

            Assert.Equal(10.00M, result);
            Assert.IsType<decimal>(result);
        }
    }
}
