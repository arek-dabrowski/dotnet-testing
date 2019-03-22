using NUnit.Framework;
using ComplexCalculator;
using System;
using System.Collections.Generic;
using System.IO;

namespace ComplexCalculator.Test
{
    [TestFixture]
    public class ComplexNumberTests
    {
        ComplexNumber cn1;
        ComplexNumber cn2;

        [SetUp]
        public void Setup()
        {
            cn1 = new ComplexNumber(3.0, 5.0);
            cn2 = new ComplexNumber(3.0, -5.0);
        }

        [Test]
        public void ShouldComputeArgZ()
        {
            //Arrange
            double expected = 1.0303768265243125;
            double actual;

            //Act
            actual = cn1.Arg();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldGetParts()
        {
            //Arrange
            double[] expected = { 3.0, 5.0 };
            double[] actual;

            //Act
            actual = cn1.GetParts();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldPrintNumber()
        {
            //Arrange
            string expected = "3+5i";
            string actual;

            //Act
            actual = cn1.Print();

            //Assert
            StringAssert.AreEqualIgnoringCase(expected, actual);
        }

        [Test]
        public void ShouldConjugate()
        {
            //Arrange
            double[] expected = { 3.0, -5.0 };
            double[] actual;

            //Act
            actual = cn1.Conjugate().GetParts();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldCompareNegatively()
        {
            //Act
            var result = cn1.Compare(cn2);

            //Assert
            Assert.False(result, "These numbers should not be equal");
        }

        [Test]
        public void ShouldComparePositively()
        {
            //Act
            var result = cn1.Compare(cn2.Conjugate());

            //Assert
            Assert.True(result, "These numbers should be equal");
        }

        [Test]
        public void ShouldntBeZero()
        {
            //Act
            var result = ComplexNumber.IsZero(cn1);

            //Assert
            Assert.False(result, "This number shouldn't be equal 0");
        }

        [Test]
        public void ShouldBeZero()
        {
            //Act
            var result = ComplexNumber.IsZero(new ComplexNumber());

            //Assert
            Assert.True(result, "This number should be equal 0");
        }

        [TestCaseSource("TestCases")]
        public double ShouldComputeModulus(double re, double im)
        {
            //Arrange
            var complexNumber = new ComplexNumber(re, im);

            //Act
            double actualModulus = complexNumber.Modulus();

            //Assert
            return actualModulus;
        }

        private static List<TestCaseData> TestCases
        {
            get
            {
                var testCases = new List<TestCaseData>();

                using (var fs = File.OpenRead(@"C:\test.csv"))
                using (var sr = new StreamReader(fs))
                {
                    string line = string.Empty;
                    while (line != null)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            string[] split = line.Split(new char[] { ',' },
                                StringSplitOptions.None);

                            double re = Convert.ToDouble(split[0]);
                            double im = Convert.ToDouble(split[1]);
                            double expectedModulus = Convert.ToDouble(split[2]);

                            var testCase = new TestCaseData(re, im).Returns(expectedModulus);
                            testCases.Add(testCase);
                        }
                    }
                }

                return testCases;
            }
        }

        [TearDown]
        public void Teardown()
        {
            cn1 = null;
            cn2 = null;
        }
    }
}