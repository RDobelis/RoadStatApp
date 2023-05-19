using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadStat.Controllers;
using RoadStat.Core.Models;
using RoadStat.Core.Services;
using RoadStat.Data;
using RoadStat.Services;

namespace RoadStat.Tests
{
    [TestFixture]
    public class CleanUpControllerTests
    {
        private CleanUpController _controller;
        private IRoadStatDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RoadStatDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new RoadStatDbContext(options);

            _context.CarSpeedEntries.RemoveRange(_context.CarSpeedEntries);
            _context.SaveChangesAsync();

            IEntityService<CarSpeedEntry> entityService = new EntityService<CarSpeedEntry>(_context, null);
            _controller = new CleanUpController(entityService);
        }

        [Test]
        public async Task DeleteAll_MethodGetsCalled_DeletesDataReturnsOkResult()
        {
            await _context.CarSpeedEntries.AddAsync(new CarSpeedEntry { Speed = 10, Date = DateTime.Now, RegistrationNumber = "LV0000" });
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteAll();

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be("All data deleted.");

            var remainingData = await _context.CarSpeedEntries.ToListAsync();
            remainingData.Should().BeEmpty();
        }
    }

}