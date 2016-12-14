using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EquationSimplifier.Entities
{
	public static class SummandsToStringWriter
	{
		public static string GetString(List<Summand> list)
		{
			var sb = new StringBuilder();

			foreach (var summand in list)
			{
				if (sb.Length > 0 && summand.Coeficient > 0)
				{
					sb.Append(" + ");
				}

				if (summand.Coeficient < 0)
				{
					sb.Append(" - ");
				}

				if (Math.Abs(Math.Abs(summand.Coeficient) - 1) > 1e-10)
				{
					sb.Append(Math.Abs(summand.Coeficient).ToString(CultureInfo.InvariantCulture));
				}

				foreach (var variable in summand.Variables)
				{
					sb.Append(variable.Name);

					if (variable.Power > 1)
					{
						sb.Append("^").Append(variable.Power);
					}
				}
			}

			if (sb.Length > 0)
			{
				sb.Append(" = 0");
			}

			return sb.ToString();
		}
	}
}
