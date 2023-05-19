using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
    public class FileUploadControllerTests
    {
        private FileUploadController _controller;
        private IEntityService<CarSpeedEntry> _entityService;
        private Mock<IFormFile> _fileMock;

        [SetUp]
        public void Setup()
        {
            var entityServiceMock = new Mock<IEntityService<CarSpeedEntry>>();
            _entityService = entityServiceMock.Object;

            _fileMock = new Mock<IFormFile>();

            _controller = new FileUploadController(_entityService);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Test]
        public async Task UploadFile_NoFileProvided_ReturnsBadRequest()
        {
            var result = await _controller.UploadFile(null);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task UploadFile_EmptyFileProvided_ReturnsBadRequest()
        {
            _fileMock.Setup(_ => _.Length).Returns(0);

            var result = await _controller.UploadFile(_fileMock.Object);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task UploadFile_ValidFileProvided_ReturnsOkResult()
        {
            _fileMock.Setup(_ => _.Length).Returns(10);

            var result = await _controller.UploadFile(_fileMock.Object);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task UploadFile_ValidFileProvided_AddsEntriesAndSavesChanges()
        {
            _fileMock.Setup(_ => _.Length).Returns(10);

            var uniqueEntries = new HashSet<CarSpeedEntry>
            {
                new CarSpeedEntry { Date = DateTime.Now, Speed = 50, RegistrationNumber = "LV0000" },
                new CarSpeedEntry { Date = DateTime.Now, Speed = 60, RegistrationNumber = "LV0001" }
            };

            var mockParser = new Mock<IFileParser<CarSpeedEntry>>();
            mockParser.Setup(parser => parser.Parse(It.IsAny<IFormFile>())).ReturnsAsync(uniqueEntries);

            var mockDbContext = new Mock<IRoadStatDbContext>();
            mockDbContext.Setup(context => context.Set<CarSpeedEntry>()).Returns(Mock.Of<DbSet<CarSpeedEntry>>());

            var entityService = new EntityService<CarSpeedEntry>(mockDbContext.Object, mockParser.Object);

            await entityService.UploadFile(_fileMock.Object);

            mockDbContext.Verify(context => context.Set<CarSpeedEntry>().AddRange(uniqueEntries), Times.Once);
            mockDbContext.Verify(context => context.SaveChangesAsync(), Times.Once);
        }
    }
}
