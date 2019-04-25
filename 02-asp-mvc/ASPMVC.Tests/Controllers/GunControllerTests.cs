using System;
using NUnit.Framework;
using ASPMVC.Controllers;
using ASPMVC.Tests.Doubles;
using Microsoft.AspNetCore.Mvc;

namespace ASPMVC.Tests.Controllers
{
    [TestFixture]
    public class GunControllerTests
    {
        [Test]
        public void Testowy_Test()
        {
            //Arrange
            var manufacturerRepo = new FakeManufacturerRepository();
            var gunRepo = new FakeGunRepository();
            var controller = new GunsController(gunRepo, manufacturerRepo);

            //Act
            var result = controller.Create();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

    }
}
