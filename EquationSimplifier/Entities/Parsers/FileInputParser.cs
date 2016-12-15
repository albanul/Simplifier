using System.IO;

namespace EquationSimplifier.Entities.Parsers
{
	public class FileInputParser: IParser
	{
		private StreamReader _streamReader;

		public FileInputParser(string filepath)
		{
			_streamReader = File.OpenText(filepath);
		}

		public string GetNextCharacter()
		{
			if (_streamReader == null)
			{
				return null;
			}

			char current = ' ';

			while (!_streamReader.EndOfStream && char.IsWhiteSpace(current))
			{
				current = (char) _streamReader.Read();
			}

			if (_streamReader.EndOfStream)
			{
				_streamReader.Dispose();
				_streamReader = null;
			}

			var character = char.IsWhiteSpace(current) ? null : current.ToString();

			return character;
		}
	}
}
