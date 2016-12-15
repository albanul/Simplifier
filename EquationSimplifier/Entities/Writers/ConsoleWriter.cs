using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EquationSimplifier.Entities.Writers
{
	public class ConsoleWriter : IWriter
	{
		public void Write(ICollection<Summand> list)
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
					// if coeficient != 1
					Console.Write(Math.Abs(summand.Coeficient).ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					// if it is a constant
					if (summand.IsConstant)
					{
						Console.Write(Math.Abs(summand.Coeficient).ToString(CultureInfo.InvariantCulture));
					}
				}

				// sort variables by power
				summand.Variables.Sort();

				foreach (var variable in summand.Variables)
				{
					Console.Write(variable.Name);

					if (variable.Power != 1 && variable.Power != 0)
					{
						Console.Write($"^{variable.Power}");
					}
				}

				first = false;
			}

			if (list.Count == 0)
			{
				Console.Write("0");
			}
			
			Console.WriteLine(" = 0");
		}
	}
}
