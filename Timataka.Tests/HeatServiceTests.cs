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
            _service = new HeatService(_repo, null);
            Setup();
        }

        public void Setup()
        {

            _context.SaveChanges();
        }

        [Fact]
        public async void TestHeatNumberOfInitialHeat()
        {
            //Arrange
            

            //Act
            var result = await _service.AddAsync(1);

            //Assert
            Assert.Equal(expected: 0, actual: result.HeatNumber);
        }

        [Fact]
        public async void TestHeatNumberOfNextHeat()
        {
            //Arrange
            await _service.AddAsync(1);

            //Act
            var result = await _service.AddAsync(1);

            //Assert
            Assert.Equal(expected: 1, actual: result.HeatNumber);
        }

        [Fact]
        public async void TestHeatReorder()
        {
            //Arrange
            await _service.AddAsync(1);
            await _service.AddAsync(1);
            await _service.AddAsync(1);
            await _service.AddAsync(1);
            await _service.RemoveAsync(3);

            //Act
            await _service.ReorderHeatsAsync(1);
            IEnumerable<Heat> result = _service.GetHeatsForEvent(1);
            var r = (from x in result
                    select x).LastOrDefault();
            int i = r.HeatNumber;


            //Assert
            Assert.Equal(expected: 2, actual: i);
        }

        [Fact]
        public async void TestDeleteHeat()
        {
            //Arrange
            await _service.AddAsync(1);
            await _service.AddAsync(1);
            await _service.AddAsync(1);

            //Act
            await _service.RemoveAsync(2);
            Heat result = await _service.GetHeatByIdAsync(2);
            var result2 = _service.GetDeletedHeatsForEvent(1);

            //Assert
            Assert.Equal(expected: true, actual: result.Deleted);
            Assert.Equal(expected: 1, actual: result2.Count());
        }
    }
}
