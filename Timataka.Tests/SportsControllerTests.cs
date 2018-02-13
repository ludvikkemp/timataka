using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Web.Controllers;
using Xunit;

namespace Timataka.Tests
{
    public class SportsControllerTests
    {
        [Fact]
        public void GetSportsIndex()
        {
            // Arrange
            var serviceMock = new Mock<ISportsRepository>();
            serviceMock.Setup(x => x.Get()).Returns(() => new List<Sport>()
            {
                new Sport {Id = 1, Name = "Running"},
                new Sport {Id = 2, Name = "Swimming"},
                new Sport {Id = 3, Name = "Cycling"}
            });
            var controller = new SportsController(serviceMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);

        }
    }
}
