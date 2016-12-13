using System.Collections.Generic;
using EquationSimplifier.Entities.Parsers;
using EquationSimplifier.Entities.Writers;

namespace EquationSimplifier.Entities
{
	public class Simplifier
	{
		private IParser _parser;
		private IWriter _writer;
		private List<Summand> _summands = new List<Summand>();

		public Simplifier(IParser parser, IWriter writer)
		{
			_parser = parser;
			_writer = writer;
		}

		public Summand GetNextSummand()
		{
			return new Summand();
		}
	}
}
