using System;
using NUnit.Framework;
using ASPMVC.Models;

namespace ASPMVC.Tests.Models
{
    [TestFixture]
    public class GunModelTests
    {
        Gun gun;

        [SetUp]
        public void Setup()
        {
            gun = new Gun()
            {
                Name = "MP5",
                ProductionDate = new DateTime(1992, 2, 4),
                Type = GunType.Assault,
                Caliber = "9mm",
                Price = 2600.99M
            };
        }

        [Test]
        public void Gun_ShouldValidate_IfDataIsCorrect()
        {
            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void Gunr_ShouldNotValidate_IfNameStartsWithLowercase()
        {
            //Arrange
            gun.Name = "mp5";

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Name first letter have to be capital.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Gun_ShouldNotValidate_IfNameIsShorterThan3()
        {
            //Arrange
            gun.Name = "M5";

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Name must be a string with a minimum length of 3 and a maximum length of 20.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Gun_ShouldNotValidate_IfNameIsLongerThan20()
        {
            //Arrange
            gun.Name = "MP5 Semi Automatic Carabin Rifle";

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Name must be a string with a minimum length of 3 and a maximum length of 20.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Gun_ShouldNotValidate_IfNameIsEmptyString()
        {
            //Arrange
            gun.Name = String.Empty;

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Name field is required.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Gun_ShouldNotValidate_IfGunTypeIsNone()
        {
            //Arrange
            gun.Type = GunType.None;

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Select a Gun Type",
                results[0].ErrorMessage);
        }

        [Test]
        public void Gun_ShouldNotValidate_IfGunTypeIsWrongNumber()
        {
            //Arrange
            gun.Type = (GunType)58;

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Select a Gun Type",
                results[0].ErrorMessage);
        }

        [Test]
        public void Gun_ShouldNotValidate_IfGunTypeIsNegativeNumber()
        {
            //Arrange
            gun.Type = (GunType)(-33);

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Select a Gun Type",
                results[0].ErrorMessage);
        }

        [Test]
        public void Gun_ShouldNotValidate_IfPriceIsNegative()
        {
            //Arrange
            gun.Price = -2835.55M;

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Price must be between 0 and 9999.99.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Gun_ShouldNotValidate_IfPriceIsTooHigh()
        {
            //Arrange
            gun.Price = 1234532.65M;

            //Act
            var results = TestModelHelper.Validate(gun);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Price must be between 0 and 9999.99.",
                results[0].ErrorMessage);
        }

        [TearDown]
        public void Teardown()
        {
            gun = null;
        }
    }
}