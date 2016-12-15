using System;
using EquationSimplifier.Entities;
using EquationSimplifier.Entities.Factories;

namespace EquationSimplifier
{ 
	public class Program
	{
		static void Main(string[] args)
		{
			var options = new Options();

			if (CommandLine.Parser.Default.ParseArguments(args, options))
			{
				switch (options.Mode)
				{
					case "mannual":
						while (true)
						{
							Console.WriteLine("Please enter the equation:");
							var equation = Console.ReadLine();
							var factory = new ConsoleInputFactory(equation);
							var simplifier = new Simplifier(factory);

							try
							{
								simplifier.Simplify();
								simplifier.Write();
							}
							catch (Exception)
							{
								Console.WriteLine("Sorry, incorrect equation. Try again.");
							}
						}
					case "file":
						var filepath = options.Path;
						break;
					default:
						Console.WriteLine(@"Sorry, unsupported --mode value. Use ""mannual"" or ""file"".");
						Environment.Exit(1);
						break;
				}
			}
		}
	}
}
