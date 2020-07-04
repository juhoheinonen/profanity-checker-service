using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ProfanityCheckerService.Managers
{
    public class ProfanityCheckManager: IProfanityCheckManager
    {
        // todo: think of better solution for word list.
        private List<string> _profanities;

        public ProfanityCheckManager(string wordListPath = "")
        {
            if (string.IsNullOrWhiteSpace(wordListPath))
            {
                wordListPath = "Content/list.txt";
            }

            _profanities = System.IO.File.ReadAllLines(wordListPath).Select(s => s.Trim()).ToList();
        }

        public bool Validate([FromBody]string input)
        {
            System.Diagnostics.Trace.WriteLine("Before prepare: " + input);

            var inputWords = InputTextPreparer.Prepare(input);

            System.Diagnostics.Trace.WriteLine("After prepare.");

            var profanitiesFound = _profanities.Any(p => inputWords.Contains(p));

            System.Diagnostics.Trace.WriteLine("Result: " + !profanitiesFound);

            if (profanitiesFound)
            {
                return false;
            }

            return true;
        }
    }
}
