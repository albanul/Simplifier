﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EquationSimplifier.Entities.Writers
{
	public class ConsoleWriter : IWriter
	{
		public void Write(List<Summand> list)
		{
			var first = true;

			foreach (var summand in list)
			{
				if (!first && summand.Coeficient > 0)
				{
					Console.Write(" + ");
				}

				if (summand.Coeficient < 0)
				{
					Console.Write(" - ");
				}

				if (Math.Abs(Math.Abs(summand.Coeficient) - 1) > 1e-10)
				{
					Console.Write(Math.Abs(summand.Coeficient).ToString(CultureInfo.InvariantCulture));
				}

				foreach (var variable in summand.Variables)
				{
					Console.Write(variable.Name);

					if (variable.Power > 1)
					{
						Console.Write($"^{variable.Power}");
					}
				}

				first = false;
			}
			
			Console.Write(" = 0");
		}
	}
}
