using System.Text.RegularExpressions;

namespace Faultify_Reforged.TestRunner.MutationChecker
{
    internal class MutationChecker
    {
        public static Dictionary<string, bool> checkMutations(string testOutput)
        {
            var testResults = Regex.Matches(testOutput, "(Passed|Failed) (\\w+) \\[(\\d+ ms)\\]", RegexOptions.IgnoreCase);

            return null;
        }
    }
}
