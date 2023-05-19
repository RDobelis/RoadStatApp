using FluentAssertions;
using RoadStat.Core.Models;

namespace RoadStat.Tests
{
    [TestFixture]
    public class CarSpeedEntryComparerTests
    {
        private CarSpeedEntryComparer _comparer;
        private CarSpeedEntry _entry1;
        private CarSpeedEntry _entry2;

        [SetUp]
        public void SetUp()
        {
            _comparer = new CarSpeedEntryComparer();
            _entry1 = new CarSpeedEntry { Date = DateTime.Now, Speed = 50, RegistrationNumber = "AB123CD" };
            _entry2 = new CarSpeedEntry { Date = DateTime.Now, Speed = 60, RegistrationNumber = "EF456GH" };
        }

        [Test]
        public void Equals_SameObjects_ReturnsTrue()
        {
            _comparer.Equals(_entry1, _entry1).Should().BeTrue();
        }

        [Test]
        public void Equals_DifferentObjects_ReturnsFalse()
        {
            _comparer.Equals(_entry1, _entry2).Should().BeFalse();
        }

        [Test]
        public void Equals_OneNullObject_ReturnsFalse()
        {
            CarSpeedEntry entry2 = null;

            _comparer.Equals(_entry1, entry2).Should().BeFalse();
        }

        [Test]
        public void GetHashCode_SameObjects_ReturnsSameHashCode()
        {
            _comparer.GetHashCode(_entry1).Should().Be(_comparer.GetHashCode(_entry1));
        }

        [Test]
        public void GetHashCode_NullObject_ReturnsZero()
        {
            CarSpeedEntry entry = null;

            _comparer.GetHashCode(entry).Should().Be(0);
        }
    }
}