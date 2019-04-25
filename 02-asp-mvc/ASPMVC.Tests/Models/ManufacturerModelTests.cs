using System;
using NUnit.Framework;
using ASPMVC.Models;

namespace ASPMVC.Tests.Models
{
    [TestFixture]
    public class ManufacturerModelTests
    {
        Manufacturer manufacturer;

        [SetUp]
        public void Setup()
        {
            //Arrange
            manufacturer = new Manufacturer()
            {
                Name = "Colt",
                Country = "USA",
                Headquarters = "USA",
                FoundDate = new DateTime(1997, 5, 15),
            };
        }

        [Test]
        public void Manufacturer_ShouldValidate_IfDataIsCorrect()
        {
            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfNameStartsWithLowercase()
        {
            //Arrange
            manufacturer.Name = "colt";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Name first letter have to be capital.", 
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfNameIsShorterThan3()
        {
            //Arrange
            manufacturer.Name = "Co";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Name must be a string with a minimum length of 3 and a maximum length of 20.", 
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfNameIsLongerThan20()
        {
            //Arrange
            manufacturer.Name = "Colt Company Industry Inc";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Name must be a string with a minimum length of 3 and a maximum length of 20.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfNameIsEmptyString()
        {
            //Arrange
            manufacturer.Name = String.Empty;

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Name field is required.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfCountryStartsWithLowercase()
        {
            //Arrange
            manufacturer.Country = "usa";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Country first letter have to be capital.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfCountryIsShorterThan3()
        {
            //Arrange
            manufacturer.Country = "US";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Country must be a string with a minimum length of 3 and a maximum length of 20.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfCountryIsLongerThan20()
        {
            //Arrange
            manufacturer.Country = "United Stated of America";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Country must be a string with a minimum length of 3 and a maximum length of 20.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfCountryIsEmptyString()
        {
            //Arrange
            manufacturer.Country = String.Empty;

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Country field is required.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfHQStartsWithLowercase()
        {
            //Arrange
            manufacturer.Headquarters = "usa";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Headquarters first letter have to be capital.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfHQIsShorterThan3()
        {
            //Arrange
            manufacturer.Headquarters = "US";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Headquarters must be a string with a minimum length of 3 and a maximum length of 30.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfHQIsLongerThan30()
        {
            //Arrange
            manufacturer.Headquarters = "United Stated of America and Canada";

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The field Headquarters must be a string with a minimum length of 3 and a maximum length of 30.",
                results[0].ErrorMessage);
        }

        [Test]
        public void Manufacturer_ShouldNotValidate_IfHQIsEmptyString()
        {
            //Arrange
            manufacturer.Headquarters = String.Empty;

            //Act
            var results = TestModelHelper.Validate(manufacturer);

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Headquarters field is required.",
                results[0].ErrorMessage);
        }

        [TearDown]
        public void Teardown()
        {
            manufacturer = null;
        }
    }
}
