using System.Text.RegularExpressions;

namespace Faultify_Reforged.TestRunner
{
    public class TestResultParser
    {
        public static MatchCollection ParseResults(string testResults)
        {
            MatchCollection parsedResults = Regex.Matches(testResults, "(Passed|Failed) (\\w+) \\[(\\d+ ms)\\]", RegexOptions.IgnoreCase);
            return parsedResults;
        }
    }
}
