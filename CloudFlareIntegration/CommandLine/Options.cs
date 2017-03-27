using CommandLine;
using CommandLine.Text;

namespace CloudFlareIntegration.CommandLine
{
    public class CommandArgumentOptions
    {
        
        // Omitting long name, default --verbose
        [Option("v", HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        //--d
        [Option('d', "debug", Required = false, HelpText = "Turns debugging mode on.")]
        public bool IsDebug { get; set; }

        [Option('o', "OutputFile", Required = false, HelpText = "output file")]
        public string OutFilePath { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [HelpOption(HelpText = "Display this help screen.")]
        public string GetUsage()
        {
            
            var help = new HelpText
            {
                Heading = new HeadingInfo(
                    $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} Version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}"),
                Copyright = new CopyrightInfo(" ", System.DateTime.Now.Year),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine(
                $"Usage: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} site action");
            help.AddOptions(this);
            return help;
          
        }
    }
}
