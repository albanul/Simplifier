using System.Collections.Generic;
using EquationSimplifier.Entities.Factories;
using EquationSimplifier.Entities.Writers;

namespace EquationSimplifier.Entities
{
	public class SummandWriter
	{
		private readonly IWriter _writer;

		public SummandWriter(BaseInputOutputFactory factory)
		{
			_writer = factory.CreateWriter();
		}

		public void Write(ICollection<Summand> summands)
		{
			_writer?.Write(summands);
		}
	}
}
