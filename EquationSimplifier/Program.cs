using System;

namespace EquationSimplifier
{
	public enum ModeType
	{
		Mannual,
		FromFile
	}

	partial class Program
	{
		static void Main(string[] args)
		{
			var options = new Options();

			if (CommandLine.Parser.Default.ParseArguments(args, options))
			{
				switch (options.Mode)
				{
					case "mannual":
						Console.WriteLine("Please enter the equation:");
						var equation = Console.ReadLine();
						break;
					case "file":
						var filepath = options.Path;
						break;
					default:
						Console.WriteLine(@"Sorry, unsupported --mode value. Use ""mannual"" or ""file"".");
						Environment.Exit(1);
						break;
				}

				//while (true)
				//{
					
				//		switch (choise)
				//		{
				//			case ChooseType.Manually:
								
				//				break;
				//			case ChooseType.FromAFile:
								
				//				break;
				//			default:
				//				choise = ChooseType.CantUnderstand;
								
				//				break;
				//		}
					
				//}
			}
		}
	}
}
