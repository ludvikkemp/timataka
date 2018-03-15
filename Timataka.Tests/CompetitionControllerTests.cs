using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Timataka.Web.Controllers;
using Xunit;

namespace Timataka.Tests
{
    public class CompetitionControllerTests
    {

        [Fact]
        public void TestGetAllCompetitions()
        {
            //Arrange
            var serviceMock = new Mock<ICompetitionService>();
            serviceMock.Setup(x => x.GetAllCompetitions()).Returns(() => new List<Competition>()
            {
                new Competition {Id = 1,
                    Name = "Vorhlaup",
                    Description = "Um vorhlaupid",
                    Email = "vorhlaup@vorhlaup.is",
                    Phone = "5551234",
                    Sponsor = "",
                    WebPage = "vorhlaup.is",
                    Deleted = false
                },
                new Competition {Id = 2,
                    Name = "Hausthlaup",
                    Description = "Um hausthlaupid",
                    Email = "hausthlaup@hausthlaup.is",
                    Phone = "5551234",
                    Sponsor = "",
                    WebPage = "hausthlaup.is",
                    Deleted = false
                }
            });
            var controller = new CompetitionController(serviceMock.Object);

            //Act
            var result = controller.Index() as ViewResult;
            var data = (List<Competition>)result.ViewData.Model;

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.Equal(expected: "Hausthlaup", actual: data[1].Name);
            Assert.Equal(expected: 2, actual: data.Count);
        }

    }
}
