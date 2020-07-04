using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProfanityCheckerService.Managers;

namespace ProfanityCheckerService.UnitTests
{
    [TestClass]
    public class ProfanityCheckManagerTests
    {
        [TestMethod]
        public void Validate_NoProfanitiesInInput_ReturnsTrue()
        {
            var files = System.IO.Directory.GetFiles("ValidTestInput");

            Assert.IsTrue(files.Length > 0);

            foreach (var file in files)
            {
                var input = System.IO.File.ReadAllText(file);

                var logger = new Mock<ILogger<ProfanityCheckManager>>();

                var sut = new ProfanityCheckManager(logger.Object);

                var isValid = sut.Validate(input);

                Assert.IsTrue(isValid);
            }
        }

        [TestMethod]
        public void Validate_ProfanitiesInInput_ReturnsFalse()
        {
            var files = System.IO.Directory.GetFiles("InvalidTestInput");

            Assert.IsTrue(files.Length > 0);

            foreach (var file in files)
            {
                var input = System.IO.File.ReadAllText(file);

                var logger = new Mock<ILogger<ProfanityCheckManager>>();

                var sut = new ProfanityCheckManager(logger.Object);

                var isValid = sut.Validate(input);

                Assert.IsFalse(isValid);
            }
        }
    }
}
