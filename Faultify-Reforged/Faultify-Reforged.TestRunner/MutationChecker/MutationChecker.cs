using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
