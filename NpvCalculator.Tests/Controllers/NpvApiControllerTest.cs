using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpvCalculator.Controllers;
using NpvCalculator.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace NpvCalculator.Tests.Controllers
{
    [TestClass]
    public class NpvApiControllerTest
    {
        [TestMethod]
        public async Task Get()
        {
            // Arrange
            var controller = new NpvApiController();

            // Act
            dynamic result = await controller.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IHttpActionResult);
        }

        [TestMethod]
        public async Task Calculate()
        {
            // Arrange
            var controller = new NpvApiController();            

            // Act
            dynamic result = await controller.Calculate(new NpvModel()
            {
                InitialInvestment = 10000,
                CommaDelimetedCashFlows = "1000, 1500, 2000",
                LowerBoundDiscountRate = 1,
                UpperBoundDiscountRate = 1.5,
                DiscountRateIncrement = 0.25
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IHttpActionResult);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(3, result.Content.Labels.Count);
            Assert.AreEqual(3, result.Content.Values.Count);
        }
    }
}
