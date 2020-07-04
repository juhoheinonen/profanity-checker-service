using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProfanityCheckerService.Managers
{
    public class InputTextPreparer
    {
        public static ImmutableHashSet<string> Prepare(string input)
        {
            var textToCheck = (input ?? string.Empty).ToLowerInvariant().Trim();

            textToCheck = HttpUtility.HtmlDecode(textToCheck);

            var splitWords = Regex.Split(textToCheck, "([^a-zåäö0-9]|\\s)+");

            var onlyLettersAndDigits = splitWords.Select(s =>
                string.Join("", s.Where(char.IsLetterOrDigit)))
                .Where(s => s.Length > 0)
                .ToImmutableHashSet();

            return onlyLettersAndDigits;
        }
    }
}
