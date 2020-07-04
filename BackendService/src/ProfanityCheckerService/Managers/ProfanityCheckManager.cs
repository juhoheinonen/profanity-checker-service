using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ProfanityCheckerService.Managers
{
    public class ProfanityCheckManager: IProfanityCheckManager
    {
        private readonly ILogger<ProfanityCheckManager> _logger;

        // todo: think of better solution for word list.
        private List<string> _profanities;

        public ProfanityCheckManager(ILogger<ProfanityCheckManager> logger)
        {
            _logger = logger;

            var wordListPath = "Content/list.txt";
            _profanities = System.IO.File.ReadAllLines(wordListPath).Select(s => s.Trim()).ToList();
        }

        public bool Validate(string input)
        {
            _logger.LogDebug("Before prepare: " + input);

            var inputWords = InputTextPreparer.Prepare(input);

            _logger.LogDebug("After prepare.");

            var profanitiesFound = _profanities.Any(p => inputWords.Contains(p));

            _logger.LogDebug("Result: " + !profanitiesFound);

            if (profanitiesFound)
            {
                return false;
            }

            return true;
        }
    }
}
