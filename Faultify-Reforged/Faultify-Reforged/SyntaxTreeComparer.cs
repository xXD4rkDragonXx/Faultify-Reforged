using DiffMatchPatch;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Faultify_Reforged.Core
{
    internal class SyntaxTreeComparer
    {
        public static List<MethodDeclarationSyntax> FindDifferingFunctions(Compilation compilation1, Compilation compilation2)
        {
            var syntaxTrees1 = compilation1.SyntaxTrees;
            var syntaxTrees2 = compilation2.SyntaxTrees;

            // Compare the syntax trees within the compilations
            var differingFunctions = new List<MethodDeclarationSyntax>();
            foreach (var syntaxTree1 in syntaxTrees1)
            {
                var syntaxTree2 = syntaxTrees2.FirstOrDefault(tree => tree.FilePath == syntaxTree1.FilePath);
                if (syntaxTree2 != null)
                {
                    var x = GetMethodDiff(syntaxTree1.ToString(), syntaxTree2.ToString());
                    Console.WriteLine(x);
                }
            }

            return differingFunctions;
        }

        public static string GetMethodDiff(string originalCode, string mutatedCode)
        {
            var dmp = new diff_match_patch();
            var diffs = dmp.diff_main(originalCode, mutatedCode);
            dmp.diff_cleanupSemantic(diffs);
            var patches = dmp.patch_make(diffs);
            var patchText = dmp.patch_toText(patches);
            var methodDiff = patchText.Split(new[] { "@@" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
            return methodDiff;
        }
    }
}
