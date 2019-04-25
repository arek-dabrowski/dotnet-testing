using System;
using NUnit.Framework;
using ASPMVC.Models;
using ASPMVC.Controllers;
using ASPMVC.Tests.Doubles;
using Microsoft.AspNetCore.Mvc;

namespace ASPMVC.Tests.Controllers
{
    [TestFixture]
    public class GunsControllerTests
    {
        FakeGunRepository gunRepo;
        FakeManufacturerRepository manuRepo;
        GunsController gc;
        Gun gun;

        [SetUp]
        public void Setup()
        {
            gunRepo = new FakeGunRepository();
            manuRepo = new FakeManufacturerRepository();
            gc = new GunsController(gunRepo, manuRepo);

            gun = new Gun()
            {
                ID = 1,
                Name = "MP5",
                ProductionDate = new DateTime(1992, 2, 4),
                Type = GunType.Assault,
                Caliber = "9mm",
                Price = 2600.99M
            };
        }

        [Test]
        public void Create_ShouldReturnView_IfCalledWithoutArgument()
        {
            //Act
            var result = gc.Create();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Create_ShouldReturnSameModel_IfGunIsInvalid()
        {
            //Arrange
            var expected = new Gun();
            gc.ModelState.AddModelError("", "empty");

            //Act
            var result = gc.Create(expected) as ViewResult;

            //Assert
            Assert.AreEqual(expected, result.Model);
        }

        [Test]
        public void Create_ShouldReturnCreatedGun_IfGunIsValid()
        {
            //Arrange
            gc.ModelState.AddModelError("", "valid");

            //Act
            var result = gc.Create(gun) as ViewResult;

            //Assert
            Assert.AreEqual(gun, result.Model);
        }

        [Test]
        public void Delete_ShouldReturnDeletedGun_IfGunIDIsValid()
        {
            //Arrange
            gunRepo.Add(gun);

            //Act
            var result = gc.Delete(1) as ViewResult;

            //Assert
            Assert.AreEqual(gun, result.Model);
        }

        [Test]
        public void Delete_ShouldReturnNotFoundResult_IfGunIDIsInvalid()
        {
            //Act
            var result = gc.Delete(8);

            //Assert
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public void Edit_ShouldReturnNotFoundResult_IfGunIDIsInvalid()
        {
            //Act
            var result = gc.Edit(6);

            //Assert
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public void Edit_ShouldReturnEditedGun_IfGunIDIsValid()
        {
            //Arrange
            gunRepo.Add(gun);

            //Act
            var result = gc.Edit(1) as ViewResult;

            //Assert
            Assert.AreEqual(gun, result.Model);
        }

        [Test]
        public void Details_ShouldReturnNotFoundResult_IfGunIDIsInvalid()
        {
            //Act
            var result = gc.Details(6);

            //Assert
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public void Details_ShouldReturnDetailedGun_IfGunIDIsValid()
        {
            //Arrange
            gunRepo.Add(gun);

            //Act
            var result = gc.Details(1) as ViewResult;

            //Assert
            Assert.AreEqual(gun, result.Model);
        }

        [TearDown]
        public void Teardown()
        {
            gunRepo = null;
            manuRepo = null;
            gun = null;
            gc = null;
        }
    }
}
