﻿using System;
using System.IO;
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
							var factory = new ConsoleInputOutputFactory(equation);
							var simplifier = new Simplifier(factory);
							var writer = new SummandWriter(factory);
							try
							{
								var summands = simplifier.Simplify();
								writer.Write(summands);
							}
							catch (SyntaxException)
							{
								Console.WriteLine("Sorry, incorrect equation. Try again.");
							}
							catch (Exception)
							{
								Console.WriteLine("Sorry, something went wrong. Try again.");
							}
						}
					case "file":
						try
						{
							var filepath = options.Path;
							var factory = new FileInputOutputFactory(filepath);
							var simplifier = new Simplifier(factory);
							var writer = new SummandWriter(factory);

							var summands = simplifier.Simplify();
							writer.Write(summands);
						}
						catch (SyntaxException)
						{
							Console.WriteLine("Sorry, incorrect equation.");
						}
						catch (FileNotFoundException)
						{
							Console.WriteLine("Sorry, can't find file to open.");
						}
						catch (Exception)
						{
							Console.WriteLine("Sorry, something went wrong.");
						}

						break;
					default:
						Console.WriteLine(@"Sorry, unsupported --mode value. Use ""mannual"" or ""file"".");
						break;
				}
			}
		}
	}
}
