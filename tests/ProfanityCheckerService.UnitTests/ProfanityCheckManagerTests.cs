using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfanityCheckerService.Managers;

namespace ProfanityCheckerService.UnitTests
{
    [TestClass]
    public class ProfanityCheckManagerTests
    {
        [TestMethod]
        public void Validate_NoProfanitiesInInput_ReturnsTrue()
        {
            var input = System.IO.File.ReadAllText("TestInput/validText.txt");

            var sut = new ProfanityCheckManager();

            var isValid = sut.Validate(input);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Validate_ProfanitiesInInput_ReturnsFalse()
        {
            var input = System.IO.File.ReadAllText("TestInput/invalidText.txt");

            var sut = new ProfanityCheckManager();

            var isValid = sut.Validate(input);

            Assert.IsFalse(isValid);
        }
    }
}
