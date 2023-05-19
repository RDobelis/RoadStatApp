using System.Collections;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RoadStat.Controllers;
using RoadStat.Core.Models;
using RoadStat.Core.Services;
using RoadStat.Data;
using RoadStat.Services;

namespace RoadStat.Tests
{
    [TestFixture]
    public class RoadStatControllerTests
    {
        private RoadStatController _controller;
        private RoadStatDbContext _context;
        private IEntityService<CarSpeedEntry> _entityService;

        [SetUp]
        public void Setup()
        {
            SetupDatabase();

            _entityService = new EntityService<CarSpeedEntry>(_context, null);
            _controller = new RoadStatController(_entityService);
        }

        [Test]
        public void GetFilteredData_ValidParamsGiven_ReturnsFilteredData()
        {
            var request = new FilterRequestModel
            {
                MinSpeed = 60,
                DateFrom = new DateTime(2020, 1, 1),
                DateTo = new DateTime(2020, 1, 1),
                Page = 1
            };

            var result = _controller.GetFilteredData(request);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var model = okResult.Value.Should().BeAssignableTo<IEnumerable>().Subject;
            var filteredData = model.Should().BeAssignableTo<List<CarSpeedEntry>>().Subject;

            filteredData.Count.Should().Be(2);
        }

        [Test]
        public void GetAverageSpeed_ValidParamsGiven_ReturnsAverageSpeed()
        {
            var date = new DateTime(2020, 1, 1);
            var entries = CreateTestEntries();

            var mockDbContext = MockDbContext(entries);
            var entityService = new EntityService<CarSpeedEntry>(mockDbContext.Object, new CarSpeedEntryFileParser());

            var result = entityService.GetAverageSpeed(date);

            result.Should().HaveCount(3);
            result.Should().Contain(item => item.Hour == 10 && item.AverageSpeed == 50.0);
            result.Should().Contain(item => item.Hour == 11 && item.AverageSpeed == 60.0);
            result.Should().Contain(item => item.Hour == 12 && item.AverageSpeed == 70.0);
        }

        [Test]
        public void GetAverageSpeed_ReturnsExpectedAverageSpeedData()
        {
            var date = new DateTime(2020, 1, 1);
            var expectedData = CreateExpectedAverageSpeedResults();

            var mockEntityService = new Mock<IEntityService<CarSpeedEntry>>();
            mockEntityService.Setup(service => service.GetAverageSpeed(date)).Returns(expectedData);

            var controller = new RoadStatController(mockEntityService.Object);

            var result = controller.GetAverageSpeed(date);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedData);
        }

        private void SetupDatabase()
        {
            var options = new DbContextOptionsBuilder<RoadStatDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new RoadStatDbContext(options);

            SeedDb();
        }
        private void SeedDb()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var entries = new List<CarSpeedEntry>
            {
                new CarSpeedEntry()
                    { Date = new DateTime(2020, 1, 1, 10, 0, 0), Speed = 50, RegistrationNumber = "LV0000" },
                new CarSpeedEntry()
                    { Date = new DateTime(2020, 1, 1, 11, 30, 0), Speed = 60, RegistrationNumber = "LV0001" },
                new CarSpeedEntry()
                    { Date = new DateTime(2020, 1, 1, 12, 40, 0), Speed = 70, RegistrationNumber = "LV0002" },
            };

            _context.CarSpeedEntries.AddRange(entries);
            _context.SaveChanges();

            _context.CarSpeedEntries.Count().Should().Be(3);
        }

        private List<CarSpeedEntry> CreateTestEntries()
        {
            return new List<CarSpeedEntry>
            {
                new CarSpeedEntry { Date = new DateTime(2020, 1, 1, 10, 0, 0), Speed = 50 },
                new CarSpeedEntry { Date = new DateTime(2020, 1, 1, 11, 30, 0), Speed = 60 },
                new CarSpeedEntry { Date = new DateTime(2020, 1, 1, 12, 40, 0), Speed = 70 }
            };
        }

        private Mock<IRoadStatDbContext> MockDbContext(List<CarSpeedEntry> entries)
        {
            var mockDbSet = new Mock<DbSet<CarSpeedEntry>>();
            mockDbSet.As<IQueryable<CarSpeedEntry>>().Setup(m => m.Provider).Returns(entries.AsQueryable().Provider);
            mockDbSet.As<IQueryable<CarSpeedEntry>>().Setup(m => m.Expression)
                .Returns(entries.AsQueryable().Expression);
            mockDbSet.As<IQueryable<CarSpeedEntry>>().Setup(m => m.ElementType)
                .Returns(entries.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<CarSpeedEntry>>().Setup(m => m.GetEnumerator())
                .Returns(entries.AsQueryable().GetEnumerator());

            var mockDbContext = new Mock<IRoadStatDbContext>();
            mockDbContext.Setup(c => c.Set<CarSpeedEntry>()).Returns(mockDbSet.Object);

            return mockDbContext;
        }

        private List<AverageSpeedResult> CreateExpectedAverageSpeedResults()
        {
            return new List<AverageSpeedResult>
            {
                new AverageSpeedResult { Hour = 10, AverageSpeed = 50.0 },
                new AverageSpeedResult { Hour = 11, AverageSpeed = 60.0 },
                new AverageSpeedResult { Hour = 12, AverageSpeed = 70.0 }
            };
        }
    }
}
