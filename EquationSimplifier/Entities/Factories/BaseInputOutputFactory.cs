using EquationSimplifier.Entities.Parsers;
using EquationSimplifier.Entities.Writers;

namespace EquationSimplifier.Entities.Factories
{
	public abstract class BaseInputOutputFactory
	{
		public abstract IParser CreateParser();

		public abstract IWriter CreateWriter();
	}
}
