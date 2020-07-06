using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfanityCheckerService.Managers;

namespace ProfanityCheckerService.UnitTests
{
    [TestClass]
    public class InputTextPreparerTests
    {
        [TestMethod]
        public void Prepare_InputWithMixedCases_WordsSplitAndTurnedToLowerCase()
        {
            // arrange
            const string input = "The quick BROWN fox   JUMped over the lazy dog. How    are you?";

            // act
            var output = InputTextPreparer.Prepare(input);

            // assert
            var expectedWords = new[] { "the", "quick", "brown", "fox", "jumped", "over", "lazy", "dog", "how", "are", "you" };

            Assert.AreEqual(expectedWords.Length, output.Count);

            foreach (var expectedWord in expectedWords)
            {
                Assert.IsTrue(output.Contains(expectedWord));
            }
        }

        [TestMethod]
        public void Prepare_InputNull_ReturnsZeroLengthValue()
        {
            var output = InputTextPreparer.Prepare(null);

            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.Count);
        }
    }
}
