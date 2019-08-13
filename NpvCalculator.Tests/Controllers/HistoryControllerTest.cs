using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpvCalculator.Controllers;

namespace NpvCalculator.Tests.Controllers
{
    [TestClass]
    public class HistoryControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var npvDBEntities = new NpvDBEntities();
            var controller = new HistoryController(npvDBEntities);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
