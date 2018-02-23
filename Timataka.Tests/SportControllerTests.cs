using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Timataka.Web.Controllers;
using Xunit;

namespace Timataka.Tests
{
    public class SportControllerTests
    {
        //SportsService sportsService = new SportsService();

        [Fact]
        public void GetSportsIndex()
        {
            // Arrange
            var serviceMock = new Mock<ISportService>();
            serviceMock.Setup(x => x.GetAllSports()).Returns(() => new List<Sport>()
            {
                new Sport {Id = 1, Name = "Running"},
                new Sport {Id = 2, Name = "Swimming"},
                new Sport {Id = 3, Name = "Cycling"}
            });
            var controller = new SportsController(serviceMock.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var data = (List<Sport>)result.ViewData.Model;

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.Equal(expected: "Running", actual: data.First().Name);
            Assert.Equal(expected: 3, actual: data.Count);

        }

        [Fact]
        public void TestGetSportById()
        {
            // Arrange
            int SportID = 2;
            var mockService = new Mock<ISportService>();
            mockService.Setup(x => x.GetSportById(SportID))
                .Returns(((Sport)new Sport { Id = 2, Name = "Swimming" }));
            var controller = new SportsController(mockService.Object);

            // Act
            var result = controller.Details(SportID) as ViewResult;
            var model = (Sport)result.ViewData.Model;

            // Assert
            var contentResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Swimming", model.Name);
        }

        [Fact]
        public void TestAddSportWithValidModel()
        {
            //Arrange
            var serviceMock = new Mock<ISportService>();
            Sport newSport = new Sport { Id = 4, Name = "Rowing" };
            serviceMock.Setup(x => x.Add(newSport)).Returns(Task.FromResult((Sport)newSport));
            var controller = new SportsController(serviceMock.Object);

            //Act
            var result = controller.Add(newSport) as RedirectToActionResult;

            //Assert 
            Assert.Equal(expected: "Index", actual: result.ActionName);
        }

        [Fact]
        public void TestAddSportWithInValidModel()
        {
            //Arrange
            var serviceMock = new Mock<ISportService>();
            Sport newSport = new Sport { Id = 8 };
            serviceMock.Setup(x => x.Add(newSport)).Returns(Task.FromResult((Sport)newSport));
            var controller = new SportsController(serviceMock.Object);

            //Act
            var result = controller.Add(newSport) as ViewResult;
            var model = (Sport)result.ViewData.Model;

            //Assert 
            Assert.Equal(expected: 8, actual: model.Id);
        }
    }
}
