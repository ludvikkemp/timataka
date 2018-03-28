using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Timataka.Web.Controllers;
using Xunit;

namespace Timataka.Tests
{
    public class DisciplineControllerTests
    {
        [Fact]
        public void GetDisciplinesTest()
        {
            //Arrange
            var serviceMock = new Mock<IDisciplineService>();
            var serviceMock2 = new Mock<ISportService>();
            serviceMock.Setup(x => x.GetAllDisciplines()).Returns(() => new List<Discipline>()
            {
                new Discipline {Id = 1, Name = "Road Running", SportId = 1},
                new Discipline {Id = 2, Name = "Race Walking", SportId = 1},
                new Discipline {Id = 3, Name = "Cross Country Running", SportId = 1}
            });
            var controller = new DisciplineController(serviceMock.Object, serviceMock2.Object);

            //Act
            //var result = controller.Index() as ViewResult;
            //var data = (List<Discipline>)result.ViewData.Model;

            //Assert
            //Assert.IsType<ViewResult>(result);
            //Assert.Equal(expected: "Race Walking", actual: data[1].Name);
            //Assert.Equal(expected: 3, actual: data.Count);


        }
    }
}
