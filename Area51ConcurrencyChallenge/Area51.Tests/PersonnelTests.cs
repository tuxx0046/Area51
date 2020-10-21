using NUnit.Framework;

namespace Area51.Tests
{
    [TestFixture]
    public class PersonnelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Personnel_OnCreation_AttributesAreCorrect()
        {
            var sut = new Personnel(1,3,1,1);
         
            string expectedId = "Staff1";
            int expectedTargetFloor = 3;
            int expectedSpawnFloor = 1;
            bool expectedDeadStatus = false;

            Assert.That(sut.Id, Is.EqualTo(expectedId));
            Assert.That(sut.SpawnFloor, Is.EqualTo(expectedSpawnFloor));
            Assert.That(sut.TargetFloor, Is.EqualTo(expectedTargetFloor));
            Assert.That(sut.IsDead, Is.EqualTo(expectedDeadStatus));
        }
    }
}