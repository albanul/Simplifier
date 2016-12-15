using EquationSimplifier.Entities.Parsers;
using EquationSimplifier.Entities.Writers;

namespace EquationSimplifier.Entities.Factories
{
	public class ConsoleInputOutputFactory : BaseInputOutputFactory
	{
		private readonly string _inputString;

		public ConsoleInputOutputFactory(string inputString)
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
