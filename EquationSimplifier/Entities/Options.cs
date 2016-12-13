using CommandLine;
using CommandLine.Text;

namespace EquationSimplifier.Entities
{
	public class Options
	{
		[Option('m', "mode", Required = true, HelpText = @"Mode of application. Supported modes are: ""mannual"" and ""file"".")]
		public string Mode { get; set; }

		[Option('p', "path", HelpText = @"Path to the file which contains equation. Used in ""file"" mode otherwise ignored.")]
		public string Path { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
		}
	}
}