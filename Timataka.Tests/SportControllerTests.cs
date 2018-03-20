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

        [Fact]
        public async void TestGetSportByID()
        {
            // Arrange
            int SportId = 2;
            var mockService = new Mock<ISportService>();
            mockService.Setup(x => x.GetSportById(SportId))
                .Returns(Task.FromResult(new Sport { Id = 2, Name = "Swimming" }));

            var controller = new SportsController(mockService.Object);

            // Act
            var result = await controller.Details(SportId) as ViewResult;
            var model = (Sport)result.ViewData.Model;

            // Assert
            var contentResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Swimming", model.Name);
        }

        [Fact]
        public async void TestDeleteSport()
        {
            //Arrange
            var serviceMock = new Mock<ISportService>();
            var sport2Delete = new Sport { Id = 1, Name = "Sport" };
            serviceMock.Setup(x => x.Remove(sport2Delete.Id)).Returns(Task.FromResult(result: sport2Delete.Id));
            serviceMock.Setup(y => y.GetSportById(sport2Delete.Id)).
                Returns(Task.FromResult(sport2Delete));
            var controller = new SportsController(serviceMock.Object);

            //Act
            var result = await controller.Delete(1) as ViewResult;
            var model = (Sport)result.ViewData.Model;

            //Assert
            Assert.Equal(expected: 1, actual: model.Id);
        }
    }
}
