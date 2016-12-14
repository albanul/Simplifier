using System;

namespace EquationSimplifier.Entities.Parsers
{
	public class ConsoleInputParser : IParser
	{
		private readonly string _inputString;
		private int _index = -1;

		public ConsoleInputParser(string inputString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				throw new ArgumentNullException(nameof(inputString), "Input string can not be null or empty");
			}

			_inputString = inputString;
		}

		public string GetNextCharacter()
		{
			char current = ' ';

			while (_index < _inputString.Length - 1 && char.IsWhiteSpace(current))
			{
				current = _inputString[++_index];
			}

			var character = char.IsWhiteSpace(current) ? null : current.ToString();

			return character;
		}
	}
}
