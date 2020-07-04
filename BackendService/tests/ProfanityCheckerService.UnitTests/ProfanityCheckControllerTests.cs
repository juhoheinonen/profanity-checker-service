using System.Net;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfanityCheckerService.Controllers;

namespace ProfanityCheckerService.UnitTests
{
    [TestClass]
    public class ProfanityCheckControllerTests
    {
        [TestMethod]
        public void Validate_ManagerNull_ReturnsStatusCode500()
        {
            var sut = new ProfanityCheckController(null);

            var result = (IStatusCodeActionResult)sut.Validate("test").Result;

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
