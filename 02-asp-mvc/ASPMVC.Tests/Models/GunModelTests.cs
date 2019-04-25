using System;
using NUnit.Framework;
using ASPMVC.Models;

namespace ASPMVC.Tests.Models
{
    [TestFixture]
    public class GunModelTests
    {
        Gun mygun;
        [SetUp]
        public void Setup()
        {
            mygun = new Gun();
        }

        [Test]
        public void ShouldAdd()
        {
            //Arrange
            mygun.Name = "USP";

            //Act
            String gunName = mygun.Name;

            //Assert
            Assert.AreEqual("USP", gunName);
        }

        [TearDown]
        public void Teardown()
        {
            mygun = null;
        }
    }
}