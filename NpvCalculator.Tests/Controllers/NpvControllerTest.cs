using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpvCalculator.Controllers;
using NpvCalculator.Models;

namespace NpvCalculator.Tests.Controllers
{
    [TestClass]
    public class NpvControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new NpvController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CalculateNpv()
        {
            // Arrange
            var controller = new NpvController();

            // Act
            var result = controller.CalculateNpv(new NpvModel()
            {
                InitialInvestment = 10000,
                CommaDelimetedCashFlows = "1000, 1500, 2000",
                LowerBoundDiscountRate = 1,
                UpperBoundDiscountRate = 1.5,
                DiscountRateIncrement = 0.25
            }) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
