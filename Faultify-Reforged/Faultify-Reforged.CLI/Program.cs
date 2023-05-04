using CommandLine;
using Faultify_Reforged.Core;

namespace Faultify_Reforged.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleMessages.printMessage(ConsoleMessages.GetLogo());
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options => RunOptions(options))
                .WithNotParsed(err => HandleParseErrors(err));
        }

        private static void HandleParseErrors(IEnumerable<Error> err)
        {
            return;
        }

        static void RunOptions(Options options)
        {
            ConsoleMessages.printOptions(options);
            FaultifyCore faultify_Reforged = new FaultifyCore(options.InputProject);
        }
    }
}