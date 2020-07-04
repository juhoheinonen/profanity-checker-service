using System.Collections.Generic;
using System.Linq;

namespace ProfanityCheckerService.Managers
{
    public class ProfanityCheckManager: IProfanityCheckManager
    {
        // todo: think of better solution for word  list.
        // todo: implement caching
        private List<string> _profanities;

        public ProfanityCheckManager(string wordListPath = "")
        {
            // todo: language guesser for English and Finnish?

            // todo: think if better error handling would be good.
            if (string.IsNullOrWhiteSpace(wordListPath))
            {
                wordListPath = "Content/list.txt";
            }

            _profanities = System.IO.File.ReadAllLines(wordListPath).Select(s => s.Trim()).ToList();
        }

        public bool Validate(string input)
        {
            var inputWords = InputTextPreparer.Prepare(input);

            var profanitiesFound = _profanities.Any(p => inputWords.Contains(p));

            if (profanitiesFound)
            {
                return false;
            }

            return true;
        }
    }
}
