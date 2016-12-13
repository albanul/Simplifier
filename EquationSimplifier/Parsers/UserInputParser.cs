using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSimplifier.Parsers
{
	public class UserInputParser : IParser
	{
		private readonly string _inputString;

		public UserInputParser(string inputString)
		{
			_inputString = inputString;
		}

		public string GetNextLexem()
		{
			return "(";
		}
	}
}
