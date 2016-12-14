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
		public void GetNextSummand_XEqualZero_XReturned()
		{
			var parser = new UserInputParser("x = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> {variable};
			var result = new Summand(1, x1);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_X2EqualZero_X2Returned()
		{
			var parser = new UserInputParser("x^2 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x2 = new Variable("x", 2);
			var variables = new List<Variable> { x2 };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}
	}
}
