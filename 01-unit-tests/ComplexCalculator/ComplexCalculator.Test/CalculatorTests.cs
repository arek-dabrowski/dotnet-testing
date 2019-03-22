using NUnit.Framework;
using ComplexCalculator;
using Microsoft.QualityTools.Testing.Fakes;
using System;
using System.Fakes;

namespace ComplexCalculator.Test
{
    [TestFixture]
    class CalculatorTests
    {
        Calculator sut;
        ComplexNumber cn1;
        ComplexNumber cn2;

        [SetUp]
        public void Setup()
        {
            sut = new Calculator();
            cn1 = new ComplexNumber(3.0, 5.0);
            cn2 = new ComplexNumber(2.0, -3.0);
        }

        [Test]
        public void ShouldAdd()
        {
            //Arrange
            double[] expected = { 5.0, 2.0 };
            double[] actual;

            //Act
            actual = sut.Add(cn1, cn2).GetParts();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldSubtract()
        {
            //Arrange
            double[] expected = { 1.0, 8.0 };
            double[] actual;

            //Act
            actual = sut.Sub(cn1, cn2).GetParts();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldMultiply()
        {
            //Arrange
            double[] expected = { 21.0, 1.0 };
            double[] actual;

            //Act
            actual = sut.Mul(cn1, cn2).GetParts();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldDivide()
        {
            //Arrange
            double[] expected = { -0.69231, 1.46154 };
            double[] actual;

            //Act
            actual = sut.Div(cn1, cn2).GetParts();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldRaiseDivisionException()
        {
            //Arrange
            double[] expected = { -198.0, 10.0 };
            ComplexNumber zero = new ComplexNumber();

            //Assert
            Assert.Throws<DivideByZeroException>(() => sut.Div(cn1, zero));
        }

        [Test]
        public void ShouldPower()
        {
            //Arrange
            double[] expected = { -198.0, 10.0 };
            double[] actual;

            //Act
            actual = sut.Pow(cn1, 3).GetParts();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShimShouldPower()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => { return new DateTime(2000, 1, 1); };

                var calc = new Calculator();
                var result = calc.Pow(new ComplexNumber(), 4);

                Assert.IsNotNull(result, "This object shouldn't be null");
            }
        }

        [Test]
        public void ShimShouldNotPower()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => { return new DateTime(1720, 1, 1); };

                var calc = new Calculator();
                var result = calc.Pow(new ComplexNumber(), 4);

                Assert.IsNull(result);
            }
        }

        [Test]
        public void StubLastPartsTest()
        {
            // Arrange:
            double[] expected = { 3.0, 5.0 };

            IComplexNumber complexNumber =
                 new ComplexCalculator.Fakes.StubIComplexNumber()
                 {
                 GetParts = () => { return new double[] { 3.0, 5.0 }; }
                 };

            var componentUnderTest = new Calculator(complexNumber);

            // Act:
            double[] actualValue = componentUnderTest.GetLastParts();

            // Assert:
            CollectionAssert.AreEqual(expected, actualValue);
        }

        [TearDown]
        public void Teardown()
        {
            sut = null;
            cn1 = null;
            cn2 = null;
        }
    }
}
