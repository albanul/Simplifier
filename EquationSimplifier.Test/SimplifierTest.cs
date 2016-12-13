using System.Collections.Generic;
using EquationSimplifier.Entities;
using EquationSimplifier.Entities.Parsers;
using EquationSimplifier.Entities.Writers;
using Xunit;

namespace EquationSimplifier.Test
{
	public class SimplifierTest
	{
		[Fact]
		public void GetNextSummand_X_XReturned()
		{
			var parser = new UserInputParser("x");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var result = new Summand(1, new List<Variable> {new Variable("x", 1)});

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}
}
}
