using System;
using NUnit.Framework;
using ASPMVC.Models;
using ASPMVC.Controllers;
using ASPMVC.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace ASPMVC.Tests.Controllers
{
    [TestFixture]
    public class ManufacturersMoqTests
    {
        Mock<IGunRepository> gunService;
        Mock<IManufacturerRepository> manuService;
        Manufacturer manuf;

        [SetUp]
        public void Setup()
        {
            gunService = new Mock<IGunRepository>();
            manuService = new Mock<IManufacturerRepository>();

            manuf = new Manufacturer()
            {
                ManufacturerID = 1,
                Name = "Colt",
                Country = "USA",
                Headquarters = "USA",
                FoundDate = new DateTime(1992, 2, 4)
            };
        }

        [Test]
        public void Index_ShouldReturnRightNumberOfManufacturers_IfDBIsNotEmpty()
        {
            //Arrange
            var manufacturers = new List<Manufacturer>()
            {
                new Manufacturer(),
                new Manufacturer(),
                new Manufacturer(),
                new Manufacturer()
            };

            manuService.Setup(x => x.GetAll()).Returns(manufacturers);
            var controller = new ManufacturersController(gunService.Object, manuService.Object);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Manufacturer>;
            Assert.AreEqual(4, model.Count);
        }

        [Test]
        public void Index_ShouldReturnEmptyList_IfDBIsEmpty()
        {
            //Arrange
            manuService.Setup(x => x.GetAll()).Returns(new List<Manufacturer>());
            var controller = new ManufacturersController(gunService.Object, manuService.Object);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Manufacturer>;
            Assert.AreEqual(0, model.Count);
        }

        [Test]
        public void Details_ShouldReturnNotFoundResult_IfManufacturerDoesNotExist()
        {
            //Arrange
            var manu1 = new Manufacturer() { Name = "Colt", Country = "USA", Headquarters = "USA" };
            manuService.Setup(x => x.GetById(1)).Returns(manu1);
            var controller = new ManufacturersController(gunService.Object, manuService.Object);

            //Act
            var result = controller.Details(5);

            //Assert
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public void Create_ShouldReturnView_IfCalledWithoutArgument()
        {
            //Arrange
            var controller = new ManufacturersController(gunService.Object, manuService.Object);

            //Act
            var result = controller.Create();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Create_ShouldReturnSameModel_IfManufacturerIsInvalid()
        {
            //Arrange
            var expected = new Manufacturer();
            var controller = new ManufacturersController(gunService.Object, manuService.Object);
            controller.ModelState.AddModelError("", "empty");

            //Act
            var result = controller.Create(expected) as ViewResult;

            //Assert
            Assert.AreEqual(expected, result.Model);
        }

        [Test]
        public void Create_ShouldReturnCreatedGun_IfManufacturerIsValid()
        {
            //Arrange
            var controller = new ManufacturersController(gunService.Object, manuService.Object);
            controller.ModelState.AddModelError("", "valid");

            //Act
            var result = controller.Create(manuf) as ViewResult;

            //Assert
            Assert.AreEqual(manuf, result.Model);
        }

        [Test]
        public void Delete_ShouldReturnDeletedManufacturer_IfManufacturerIsValid()
        {
            //Arrange
            manuService.Setup(x => x.GetById(1)).Returns(manuf);
            var controller = new ManufacturersController(gunService.Object, manuService.Object);

            //Act
            var result = controller.Delete(1) as ViewResult;

            //Assert
            Assert.AreEqual(manuf, result.Model);
        }

        [Test]
        public void Delete_ShouldReturnNotFoundResult_IfManufacturerIsInvalid()
        {
            //Arrange
            var controller = new ManufacturersController(gunService.Object, manuService.Object);

            //Act
            var result = controller.Delete(8);

            //Assert
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public void Edit_ShouldReturnNotFoundResult_IfManufacturerIsInvalid()
        {
            //Arrange
            var controller = new ManufacturersController(gunService.Object, manuService.Object);

            //Act
            var result = controller.Edit(6);

            //Assert
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public void Edit_ShouldReturnEditedManufacturer_IfManufacturerIsValid()
        {
            //Arrange
            manuService.Setup(x => x.GetById(1)).Returns(manuf);
            var controller = new ManufacturersController(gunService.Object, manuService.Object);

            //Act
            var result = controller.Edit(1) as ViewResult;

            //Assert
            Assert.AreEqual(manuf, result.Model);
        }

        [TearDown]
        public void Teardown()
        {
            gunService = null;
            manuService = null;
            manuf = null;
        }
    }
}
