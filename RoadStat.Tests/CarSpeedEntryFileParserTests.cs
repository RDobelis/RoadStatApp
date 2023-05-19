using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using RoadStat.Core.Models;

namespace RoadStat.Tests
{
    [TestFixture]
    public class CarSpeedEntryFileParserTests
    {
        private CarSpeedEntryFileParser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new CarSpeedEntryFileParser();
        }

        [Test]
        public async Task Parse_ValidFileGiven_ReturnsUniqueEntries()
        {
            var fileContents = "2023-05-01\t60\tLV0000\n2023-05-02\t70\tLV0001\n2023-05-03\t80\tLV0002";
            var fileMock = CreateFormFile(fileContents);

            var result = await _parser.Parse(fileMock.Object);

            result.Should().HaveCount(3);
            result.Should().ContainEquivalentOf(new CarSpeedEntry { Date = new DateTime(2023, 05, 01), Speed = 60, RegistrationNumber = "LV0000" });
            result.Should().ContainEquivalentOf(new CarSpeedEntry { Date = new DateTime(2023, 05, 02), Speed = 70, RegistrationNumber = "LV0001" });
            result.Should().ContainEquivalentOf(new CarSpeedEntry { Date = new DateTime(2023, 05, 03), Speed = 80, RegistrationNumber = "LV0002" });
        }
        private Mock<IFormFile> CreateFormFile(string fileContents)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(fileContents);
            writer.Flush();
            stream.Position = 0;

            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.Length).Returns(stream.Length);

            return fileMock;
        }
    }
}
