using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Xunit;

namespace Timataka.Tests
{
    public class HeatServiceTests
    {
        [Fact]
        public async void TestHeatNumberOfInitialHeat()
        {
            //Arrange
            var dbMock = new Mock<IHeatRepository>();
            dbMock.Setup(x => x.Get()).Returns(() => new List<Heat>()
            {
                new Heat {Deleted = false, EventId = 1, HeatNumber = 0, Id = 1 },
                new Heat {Deleted = false, EventId = 2, HeatNumber = 0, Id = 2 },
                new Heat {Deleted = false, EventId = 4, HeatNumber = 0, Id = 3 }
            });
            var service = new HeatService(dbMock.Object);

            //Act
            var result = await service.AddAsync(3);
            var result2 = await service.AddAsync(1);

            //Assert
            Assert.Equal(expected: 0, actual: result.HeatNumber);
            Assert.Equal(expected: 1, actual: result2.HeatNumber);
        }

        [Fact]
        public async void TestHeatReorder()
        {
            //Arrange
            Heat edited = new Heat { Deleted = false, EventId = 1, HeatNumber = 1, Id = 2 };
            var dbMock = new Mock<IHeatRepository>();
            dbMock.Setup(x => x.Get()).Returns(() => new List<Heat>()
            {
                new Heat {Deleted = false, EventId = 1, HeatNumber = 0, Id = 1 },
                new Heat {Deleted = false, EventId = 1, HeatNumber = 3, Id = 2 },
                new Heat {Deleted = false, EventId = 1, HeatNumber = 7, Id = 3 }
            });
            var service = new HeatService(dbMock.Object);

            //Act
            await service.ReorderHeatsAsync(1);
            IEnumerable<Heat> result = service.GetHeatsForEvent(1);
            var r = (from x in result
                    where x.Id.Equals(2)
                    select x).SingleOrDefault();
            int i = r.HeatNumber;


            //Assert
            //Assert.Equal(expected: 1, actual: i);
        }
    }
}
