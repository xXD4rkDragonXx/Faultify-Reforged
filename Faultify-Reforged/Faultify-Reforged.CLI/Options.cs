using CommandLine;

namespace Faultify_Reforged.CLI
{
    /// <summary>
    /// Contains all the Commandline options.
    /// </summary>
    internal class Options
    {
        [Option('i', "inputproject", Required = true, HelpText = "Input Project .sln file location")]
        public string InputProject { get; set; }

        [Option('t', "testlocation", Required = true, HelpText = "The .csproj file of the test project")]
        public string InputProjectTestLocation { get; set; }

        [Option('o', "outputlocation", Required = false, HelpText = "Location for output")]
        public string? OutputLocation { get; set; }

        [Option('m', "mutationlocation", Required = false, HelpText = "Folder containing the mutations")]
        public string MutationLocation { get; set; }

        [Option('r', "runners", Required = false, HelpText = "Amount of testrunners")]
        public int RunnerAmount { get; set;}
    }
}
