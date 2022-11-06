using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wpfintegral;
using Wpfintegral.Clasess;

namespace WpfintegralTest
{
    [TestClass]
    public class IntegralTestTrap
    {
        [TestMethod]
        public void IntagralXXCorrect()
        {
            //Arrange
            double actual = 333333333.333333333;
            double answer;

            //Act
            IntegralCalculateRectangle probe = new IntegralCalculateRectangle();
            answer = probe.Calculate(0, 1000, 100000, (x) => x * x);

            //Assert
            Assert.AreEqual(answer, actual, 0.01);
        }

        [TestMethod]
        public void IntagralXXCorrectSQR()
        {
            //Arrange
            double expected = 333333333.333333333;

            //Act
            IntegralCalculateTrapecia ProbeIntegral = new IntegralCalculateTrapecia();
            double answerTest = ProbeIntegral.Calculate(0, 1000, 100000, (x) => x * x);

            //Assert
            Assert.AreEqual(expected, answerTest, 0.02);
        }

        [TestMethod]
        public void IntagralXXCorrectSimpson()
        {
            //Arrange
            IntegralCalculateSimpson integral = new IntegralCalculateSimpson();
            double expected = 333333333.333333;

            //Act
            double actual = integral.Calculate(0, 1000, 1, (x) => x * x);

            //Assert
            Assert.AreEqual(expected, actual, 0.001);
        }
    }
}
