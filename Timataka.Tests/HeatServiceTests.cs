using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Timataka.Web;
using Xunit;

namespace Timataka.Tests
{
    public class HeatServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IHeatService _service;
        private readonly IHeatRepository _repo;


        public HeatServiceTests()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();
            var server = new TestServer(builder);
            _context = server.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            _repo = new HeatRepository(_context);
            _service = new HeatService(_repo);
            Setup();
        }

        public void Setup()
        {

            _context.Heats.Add(new Heat { Deleted = false, EventId = 1, HeatNumber = 0});
            _context.Heats.Add(new Heat { Deleted = false, EventId = 2, HeatNumber = 0});
            _context.Heats.Add(new Heat { Deleted = false, EventId = 4, HeatNumber = 0});
            _context.SaveChanges();
        }

        [Fact]
        public async void TestHeatNumberOfInitialHeat()
        {
            //Arrange

            //Act
            var result = await _service.AddAsync(1);

            //Assert
            Assert.Equal(expected: 1, actual: result.HeatNumber);
        }

        [Fact]
        public async void TestHeatReorder()
        {
            //Arrange
            _context.Heats.Add(new Heat { Deleted = false, EventId = 1, HeatNumber = 3 });
            _context.Heats.Add(new Heat { Deleted = false, EventId = 1, HeatNumber = 7 });
            _context.SaveChanges();

            //Act
            await _service.ReorderHeatsAsync(1);
            IEnumerable<Heat> result = _service.GetHeatsForEvent(1);
            var r = (from x in result
                    select x).LastOrDefault();
            int i = r.HeatNumber;


            //Assert
            Assert.Equal(expected: 3, actual: i);
        }
    }
}
