using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace Faultify_Reforged.Core.Mutator
{
    internal class RegexSyntaxRewriter : CSharpSyntaxRewriter
    {

        string findPattern;
        string replacementString;

        public RegexSyntaxRewriter(string findPattern, string replacementString) 
        {
            this.findPattern = findPattern;
            this.replacementString = replacementString;
        }

        public override SyntaxNode? VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            string modifiedTreeString = node.ToString();

            MatchCollection regexMatches = Regex.Matches(modifiedTreeString, findPattern);
            
            foreach (Match regexMatch in regexMatches)
            {
                modifiedTreeString = modifiedTreeString.Remove(regexMatch.Index, regexMatch.Length);
                modifiedTreeString = modifiedTreeString.Insert(regexMatch.Index, replacementString);
            }

            SyntaxNode modfiedNode = SyntaxFactory.ParseStatement(modifiedTreeString);

            return base.VisitExpressionStatement((ExpressionStatementSyntax)modfiedNode);
        }

        public void SetFindPattern(string findPattern)
        {
            this.findPattern = findPattern;
        }

        public void SetReplacementString (string replacementString)
        {
            this.replacementString = replacementString;
        }

        public void SetValues (string findPattern, string replacementString)
        {
            this.findPattern = findPattern;
            this.replacementString = replacementString;
        }
    }
}
