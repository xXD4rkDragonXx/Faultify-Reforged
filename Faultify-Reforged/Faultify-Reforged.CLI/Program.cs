using CommandLine;
using Faultify_Reforged.Core;

namespace Faultify_Reforged.CLI
{
    /// <summary>
    /// The main CLI class.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Runs the program
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        static void Main(string[] args)
        {
            ConsoleMessages.printMessage(ConsoleMessages.GetLogo());
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options => RunOptions(options))
                .WithNotParsed(err => HandleParseErrors(err));

        }

        /// <summary>
        /// Handles Parse errors of the Commandline arguments
        /// </summary>
        /// <param name="err"></param>
        private static void HandleParseErrors(IEnumerable<Error> err)
        {
            foreach (var error in err)
            {
                Console.Error.WriteLine(error);
            }
            return;
        }

        /// <summary>
        /// Run the system with the given command line options
        /// </summary>
        /// <param name="options">The options given in the Commandline</param>
        static void RunOptions(Options options)
        {
            ConsoleMessages.printOptions(options);
            if (options.OutputLocation != null)
            {
                FaultifyCore faultify_Reforged = new FaultifyCore(options.InputProject, options.InputProjectTestLocation, options.MutationLocation, options.OutputLocation);
            }
            else
            {
                FaultifyCore faultify_Reforged = new FaultifyCore(options.InputProject, options.InputProjectTestLocation, options.MutationLocation);
            }
        }
    }
}