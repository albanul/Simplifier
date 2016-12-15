using System;
using EquationSimplifier.Entities.Parsers;
using EquationSimplifier.Entities.Writers;

namespace EquationSimplifier.Entities.Factories
{
	public class ConsoleInputFactory : BaseInputFactory
	{
		private readonly string _inputString;

		public ConsoleInputFactory(string inputString)
		{
			_inputString = inputString;
		}

		public override IParser CreateParser()
		{
			return new ConsoleInputParser(_inputString);
		}

		public override IWriter CreateWriter()
		{
			return new ConsoleWriter();
		}
	}
}
