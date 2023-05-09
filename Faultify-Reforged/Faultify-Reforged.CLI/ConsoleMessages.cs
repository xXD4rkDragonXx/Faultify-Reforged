namespace Faultify_Reforged.CLI
{
    /// <summary>
    /// The class for Console messages.
    /// </summary>
    internal class ConsoleMessages
    {
        /// <summary>
        /// Provides The commandline version of the logo of the application
        /// </summary>
        /// <returns>The commandline version of the logo</returns>
        public static string GetLogo()
        {
            var _logo = @"
      ███████╗ █████╗ ██╗   ██╗██╗  ████████╗██╗███████╗██╗   ██╗   
      ██╔════╝██╔══██╗██║   ██║██║  ╚══██╔══╝██║██╔════╝╚██╗ ██╔╝   
      █████╗  ███████║██║   ██║██║     ██║   ██║█████╗   ╚████╔╝    
      ██╔══╝  ██╔══██║██║   ██║██║     ██║   ██║██╔══╝    ╚██╔╝     
      ██║     ██║  ██║╚██████╔╝███████╗██║   ██║██║        ██║      
      ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝   ╚═╝╚═╝        ╚═╝      
  ██████╗ ███████╗███████╗ ██████╗ ██████╗  ██████╗ ███████╗██████╗ 
  ██╔══██╗██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝ ██╔════╝██╔══██╗
  ██████╔╝█████╗  █████╗  ██║   ██║██████╔╝██║  ███╗█████╗  ██║  ██║
  ██╔══██╗██╔══╝  ██╔══╝  ██║   ██║██╔══██╗██║   ██║██╔══╝  ██║  ██║
  ██║  ██║███████╗██║     ╚██████╔╝██║  ██║╚██████╔╝███████╗██████╔╝
  ╚═╝  ╚═╝╚══════╝╚═╝      ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═════╝
";
            return _logo;
        }

        /// <summary>
        /// Prints information the the command line in the given color
        /// </summary>
        /// <param name="message">Message to print</param>
        /// <param name="consoleColor">Color to print the message in</param>
        /// <param name="keepColor">Keep the color for future messages</param>
        public static void printMessage(string message, ConsoleColor consoleColor = ConsoleColor.Cyan, bool keepColor = false)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            if(!keepColor)
            {
                Console.ForegroundColor = currentColor;
            }
        }

        /// <summary>
        /// Print the options 
        /// </summary>
        /// <param name="options"></param>
        public static void printOptions(Options options)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Input Project: {options.InputProject}");
            Console.WriteLine($"Output Location: {(options.OutputLocation != null ? options.OutputLocation : "Default")}");
            Console.WriteLine($"Mutation Location: {(options.MutationLocation != null ? options.MutationLocation : "Default")}");
            Console.ForegroundColor = currentColor;
        }
    }
}
