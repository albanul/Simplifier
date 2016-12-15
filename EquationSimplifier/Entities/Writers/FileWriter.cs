using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace EquationSimplifier.Entities.Writers
{
	class FileWriter : IWriter
	{
		private const string OutputPath = "a.out";

		public void Write(ICollection<Summand> list)
		{
			using (var sw = new StreamWriter(OutputPath))
			{
				var first = true;

				foreach (var summand in list)
				{
					if (!first && summand.Coeficient > 0)
					{
						sw.Write(" + ");
					}

					if (summand.Coeficient < 0)
					{
						sw.Write(" - ");
					}

					if (Math.Abs(Math.Abs(summand.Coeficient) - 1) > 1e-10)
					{
						// if coeficient != 1
						sw.Write(Math.Abs(summand.Coeficient).ToString(CultureInfo.InvariantCulture));
					}
					else
					{
						// if it is a constant
						if (summand.IsConstant)
						{
							sw.Write(Math.Abs(summand.Coeficient).ToString(CultureInfo.InvariantCulture));
						}
					}

					// sort variables by power
					summand.Variables.Sort();

					foreach (var variable in summand.Variables)
					{
						sw.Write(variable.Name);

						if (variable.Power > 1)
						{
							sw.Write($"^{variable.Power}");
						}
					}

					first = false;
				}

				if (list.Count == 0)
				{
					sw.Write("0");
				}

				sw.WriteLine(" = 0");
			}
			
		}
	}
}
