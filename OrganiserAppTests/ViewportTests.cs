using OrganiserApp.Models;
using OrganiserApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserAppTests
{
    public class ViewportTests
    {
        private readonly ShopService _sut = new();
        private readonly string EventUuid = "bc37c7da-1084-4caa-9426-f23156cb751f";

        [Fact]
        public async Task GetViewportsAsync_Test()
        {
            var result = await _sut.GetViewportsAsync(EventUuid);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PutViewportAsync_Test()
        {
            var newName = "goudennieuw";
            var viewports = await _sut.GetViewportsAsync(EventUuid);

            var testViewport = viewports.FirstOrDefault();
            Assert.NotNull(testViewport);

            testViewport!.Uri = newName;
            var result = await _sut.PutViewportAsync(testViewport, EventUuid);

            Assert.Equal(newName, result.Uri);
        }
    }
}
