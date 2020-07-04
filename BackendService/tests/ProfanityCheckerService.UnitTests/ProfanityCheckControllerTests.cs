using System.Net;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProfanityCheckerService.Controllers;

namespace ProfanityCheckerService.UnitTests
{
    [TestClass]
    public class ProfanityCheckControllerTests
    {
        [TestMethod]
        public void Validate_ManagerNull_ReturnsStatusCode500()
        {
            var mockLogger = new Mock<ILogger<ProfanityCheckController>>();

            var sut = new ProfanityCheckController(null, mockLogger.Object);

            var result = (IStatusCodeActionResult)sut.Validate("test").Result;

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
