using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpvCalculator.Helper;
using NpvCalculator.Models;

namespace NpvCalculatorApplication.Tests.Controllers
{
    [TestClass]
    public class NpvHelperTest
    {
        readonly NpvModel model = new NpvModel()
        {
            InitialInvestment = 10000,
            CommaDelimetedCashFlows = "1000, 1500, 2000",
            LowerBoundDiscountRate = 1,
            UpperBoundDiscountRate = 1.5,
            DiscountRateIncrement = 0.25
        };

        [TestMethod]
        public async Task GetNpvByIDAsync_Check_Result()
        {
            // Arrange
            var helper = new NpvHelper();

            // Act
            var result = await helper.GetNpvByIDAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Labels.Count);
            Assert.AreEqual(3, result.Values.Count);
        }

        [TestMethod]
        public async Task GetNpvByIDAsync_Check_ModelResult()
        {
            // Arrange
            var helper = new NpvHelper();

            // Act
            var result = await helper.GetNpvResultAsync(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Labels.Count);
            Assert.AreEqual(3, result.Values.Count);
        }
    }
}
