using EquationSimplifier.Entities.Parsers;
using EquationSimplifier.Entities.Writers;

namespace EquationSimplifier.Entities.Factories
{
	public class FileInputOutputFactory : BaseInputOutputFactory
	{
		private readonly string _filepath;

		public FileInputOutputFactory(string filepath)
		{
			_filepath = filepath;
		}

		public override IParser CreateParser()
		{
			return new FileInputParser(_filepath);
		}

		public override IWriter CreateWriter()
		{
			return new FileWriter();
		}
	}
}
