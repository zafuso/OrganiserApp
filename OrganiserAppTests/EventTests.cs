using OrganiserApp.Enums;
using OrganiserApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserAppTests
{
    public class EventTests
    {
        private readonly EventService _sut = new();

        [Fact]
        public async Task GetEventsAsync_Test()
        {
            var result = await _sut.GetEventListAsync(FilterDateRangeType.all, 0, 5);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEventAsync_Test()
        {
            var result = await _sut.GetEventAsync("852ac0ee-ad49-42cb-9641-07631993b5bd");
            Assert.NotNull(result);
            Assert.Equal("Back to the '90s", result.Name);
        }
    }
}
