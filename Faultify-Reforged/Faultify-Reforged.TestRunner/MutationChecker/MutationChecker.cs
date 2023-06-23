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
            Console.Out.WriteLine(testOutput);
            var x = Regex.Match(testOutput, "failed", RegexOptions.IgnoreCase);
            
            return null;
        }
    }
}
