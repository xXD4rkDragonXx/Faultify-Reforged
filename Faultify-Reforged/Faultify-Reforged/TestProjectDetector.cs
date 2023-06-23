using Microsoft.CodeAnalysis;

namespace Faultify_Reforged.Core
{
    /// <summary>
    /// For detecting test projects
    /// </summary>
    internal class TestProjectDetector
    {
        /// <summary>
        /// Tests if a compilation is a test project by checking the references
        /// </summary>
        /// <param name="compilation">Compilation to check</param>
        /// <returns>True if the compilation is a test framework</returns>
        public static bool IsTestProject(Compilation compilation)
        {
            var testFrameworks = new[]
            {
                "Microsoft.VisualStudio.TestPlatform.TestFramework",
                "NUnit.Framework",
                "Xunit",
                "Microsoft.NET.Test.Sdk",
                // Add other test framework assembly names here when needed
            };

            if (compilation.AssemblyName.EndsWith("Tests"))
            {
                return true;
            }

            Console.WriteLine(compilation.References);

            foreach (var reference in compilation.References)
            {
                if (testFrameworks.Any(testFramework => reference.Display.EndsWith(testFramework, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
