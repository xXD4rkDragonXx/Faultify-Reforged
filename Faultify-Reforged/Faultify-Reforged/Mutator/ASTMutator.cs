using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Faultify_Reforged.Core.Mutator
{
    internal class ASTMutator
    {
        public static void Mutate(Compilation compilation, Mutation mutation)
        {
            foreach (SyntaxTree syntaxTree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree);

                var rewriter = new TypeInferenceRewriter(semanticModel);

                var rootNode = syntaxTree.GetRoot();
                var classDeclarations = rootNode.DescendantNodes().OfType<ClassDeclarationSyntax>();
                var methodDeclarations = rootNode.DescendantNodes().OfType<MethodDeclarationSyntax>();
                foreach (var classDeclaration in classDeclarations)
                {
                    var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration);
                    Console.WriteLine(classSymbol);
                }

                foreach (var methodDeclaration in methodDeclarations)
                {
                    var statementSyntaxes = methodDeclaration.Body.Statements;
                    foreach (var statement in statementSyntaxes)
                    {
                        Console.WriteLine(statement);

                    }
                }

                SyntaxTree mutationSyntaxTree = CodeStringToSyntaxTree(mutation.Mutations[0]);
            }
            
        }

        public static SyntaxTree CodeStringToSyntaxTree(string codeString)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeString);
            return syntaxTree;
        }
    }
}
