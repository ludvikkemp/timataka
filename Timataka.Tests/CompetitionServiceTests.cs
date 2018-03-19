using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Services;
using Timataka.Web;
using Xunit;

namespace Timataka.Tests
{
    public class CompetitionServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ICompetitionService _service;
        private readonly ICompetitionRepository _repo;


        public CompetitionServiceTests()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();
            var server = new TestServer(builder);
            _context = server.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            _repo = new CompetitionRepository(_context);
            _service = new CompetitionService(_repo);
            Setup();
        }

        public void Setup()
        {

            _context.SaveChanges();
        }

        [Fact]
        public async void TestAddCompetition()
        {
            //Arrange
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Keppnin", PhoneNumber = "5812345" });

            //Act
            IEnumerable<Competition> result = from x in _context.Competitions
                                              select x;

            //Assert
            Assert.Equal(expected: 1, actual: result.Count());
        }

        [Fact]
        public async void TestGetAllCompetitions()
        {
            //Arrange
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Keppnin", PhoneNumber = "5812345" });
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Önnur keppni", PhoneNumber = "5912345" });

            //Act
            var result = _service.GetAllCompetitions();

            //Assert
            Assert.Equal(expected: 2, actual: result.Count());
        }

        [Fact]
        public async void  TestGetCompetitionByID()
        {
            //Arrange
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Keppnin", PhoneNumber = "5812345" });
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Önnur keppni", PhoneNumber = "5912345" });
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Enn önnur keppni", PhoneNumber = "5012345" });

            //Act
            var result = await _service.GetCompetitionById(2);

            //Assert
            Assert.Equal(expected: "Önnur keppni", actual: result.Name);
        }

        [Fact]
        public async void TestCompetitionExists()
        {
            //Arrange
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Keppnin", PhoneNumber = "5812345" });
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Önnur keppni", PhoneNumber = "5912345" });
            await _service.Add(new CompetitionsViewModel { Description = "", Email = "email@email.com", Name = "Enn önnur keppni", PhoneNumber = "5012345" });

            //Act
            var result = await _service.CompetitionExists("Önnur keppni");
            var result2 = await _service.CompetitionExists("Keppni sem er ekki til");

            //Assert
            Assert.Equal(expected: true, actual: result);
            Assert.Equal(expected: false, actual: result2);
        }

        [Fact]
        public async void TestAddCompetitionInstance()
        {
            //Arrange
            await _service.AddInstance(new CompetitionsInstanceViewModel { CompetitionId = 1, Name = "Keppnin 2017" });

            //Act
            var result = (from x in _context.CompetitionInstances
                         select x).Count();

            //Assert
            Assert.Equal(expected: 1, actual: result);
        }

        [Fact]
        public async void TestGetAllCompetitionInstances()
        {
            //Arrange
            await _service.AddInstance(new CompetitionsInstanceViewModel { CompetitionId = 1, Name = "Keppnin 2017" });
            await _service.AddInstance(new CompetitionsInstanceViewModel { CompetitionId = 1, Name = "Keppnin 2018" });
            await _service.AddInstance(new CompetitionsInstanceViewModel { CompetitionId = 2, Name = "Keppnin 2017" });

            //Act
            var result = _service.GetAllCompetitionInstances().ToList();

            //Assert
            Assert.Equal(expected: 3, actual: result.Count());
            Assert.Equal(expected: "Keppnin 2017", actual: result.Last().Name);
        }

        [Fact]
        public async void TestGetAllInstancesOfCompetition()
        {
            //Arrange
            await _service.AddInstance(new CompetitionsInstanceViewModel { CompetitionId = 1, Name = "Keppnin 2017" });
            await _service.AddInstance(new CompetitionsInstanceViewModel { CompetitionId = 1, Name = "Keppnin 2018" });
            await _service.AddInstance(new CompetitionsInstanceViewModel { CompetitionId = 2, Name = "Keppnin 2017" });

            //Act
            var result = _service.GetAllInstancesOfCompetition(1);

            //Assert
            Assert.Equal(expected: 2, actual: result.Count());
            Assert.Equal(expected: "Keppnin 2018", actual: result.Last().Name);
        }

        //TODO test functionality for roles...

    }
}
