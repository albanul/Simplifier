using System;
using System.Collections.Generic;
using System.IO;
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
			if (string.IsNullOrEmpty(inputString))
			{
				throw new ArgumentNullException(nameof(inputString), "input string can not be null or empty");
			}

			_inputString = inputString;
		}

		public string GetNextLexem()
		{
			string lexem;

			using (var enumerator = _inputString.GetEnumerator())
			{
				bool readSuccess;

				do
				{
					readSuccess = enumerator.MoveNext();
				} while (readSuccess && char.IsWhiteSpace(enumerator.Current));

				lexem = readSuccess ? enumerator.Current.ToString() : null;
			}

			return lexem;
		}
	}
}
