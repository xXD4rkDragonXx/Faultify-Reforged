using CommandLine;

namespace Faultify_Reforged.CLI
{
    internal class Options
    {
        [Option('i', "inputproject", Required = true, HelpText = "Input Project .sln file location")]
        public string InputProject { get; set; }

        [Option('o', "outputlocation", Required = false, HelpText = "Location for output")]
        public string? OutputLocation { get; set; }
    }
}
