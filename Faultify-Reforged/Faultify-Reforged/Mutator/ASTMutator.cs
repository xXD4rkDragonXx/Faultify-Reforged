using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace Faultify_Reforged.Core.Mutator
{
    internal class ASTMutator
    {

        public static Compilation Mutate(Compilation compilation, Mutation mutation, MutationReporter mutationReporter)
        {
            Compilation newCompilation = compilation;
            foreach (SyntaxTree syntaxTree in compilation.SyntaxTrees)
            {
                SyntaxNode rootNode = syntaxTree.GetRoot();
                
                RegexSyntaxRewriter regexSyntaxRewriter = new RegexSyntaxRewriter(mutation.Identifier, mutation.Mutations[0], mutationReporter);
                SyntaxNode newRootNode = regexSyntaxRewriter.Visit(rootNode);

                SyntaxTree newSyntaxTree = syntaxTree.WithRootAndOptions(newRootNode, syntaxTree.Options);

                newCompilation = newCompilation.ReplaceSyntaxTree(syntaxTree, newSyntaxTree);
            }
            return newCompilation;
        }

        public static bool compileCodeToLocation(Compilation compilation, string outputPath)
        {
            EmitResult result = compilation.Emit(outputPath);

            if (!result.Success)
            {
                foreach (var diagnostic in result.Diagnostics)
                {
                    Console.WriteLine(diagnostic);
                }

                return false;
            }

            return true;
        }
    }
}
