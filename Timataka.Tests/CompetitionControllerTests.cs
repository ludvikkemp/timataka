using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
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

            /*
            //Act
            var result = controller.Index() as ViewResult;
            var data = (List<Competition>)result.ViewData.Model;

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.Equal(expected: "Hausthlaup", actual: data[1].Name);
            Assert.Equal(expected: 2, actual: data.Count);
            */
        }

        [Fact]
        public async void TestDetailsAsync()
        {
            //Arrange
            int competitionId = 1;
            var serviceMock = new Mock<ICompetitionService>();
            serviceMock.Setup(x => x.GetCompetitionById(1)).Returns(Task.FromResult(new Competition
            {
                Id = 1,
                Name = "Vorhlaup",
                Description = "Um vorhlaupid",
                Email = "vorhlaup@vorhlaup.is",
                Phone = "5551234",
                Sponsor = "",
                WebPage = "vorhlaup.is",
                Deleted = false
            }));
            var controller = new CompetitionController(serviceMock.Object);

            //Act
            var result = await controller.Details(competitionId) as ViewResult;
            var data = (Competition)result.ViewData.Model;

            //Assert
            Assert.Equal(expected: "Vorhlaup", actual: data.Name);
        }

        [Fact]
        public async void TestCreate()
        {
            //Arrange
            var c = new Competition
            {
                Id = 1,
                Name = "Vorhlaup",
                Description = "Um vorhlaupid",
                Email = "vorhlaup@vorhlaup.is",
                Phone = "5551234",
                Sponsor = "",
                WebPage = "vorhlaup.is"
            };
            var cMod = new CompetitionsViewModel
            {             
                Name = "Vorhlaup",
                Description = "Um vorhlaupid",
                Email = "vorhlaup@vorhlaup.is",
                PhoneNumber = "5551234",
                Sponsor = "",
                WebPage = "vorhlaup.is"
            };
            var serviceMock = new Mock<ICompetitionService>();
            serviceMock.Setup(x => x.Add(cMod)).Returns(Task.FromResult(c));
            var controller = new CompetitionController(serviceMock.Object);

            //Act
            var result = await controller.Create(cMod) as RedirectToActionResult;

            //Assert 
            Assert.Equal(expected: "Competitions", actual: result.ActionName);
        }

    }
}
