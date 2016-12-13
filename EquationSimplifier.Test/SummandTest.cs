using System.Collections.Generic;
using EquationSimplifier.Entities;
using Xunit;

namespace EquationSimplifier.Test
{
	public class SummandTest
	{
		[Fact]
		public void Equals_X1_X1_TrueReturned()
		{
			var x1 = new Variable("x", 1);

			var firstVariables = new List<Variable> {x1};
			var firstSummand = new Summand(2, firstVariables);

			var seconndVariables = new List<Variable> {x1};
			var secondSummand = new Summand(2, seconndVariables);

			Assert.True(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_X1Y2_X1Y2_TrueReturned()
		{
			var x1 = new Variable("x", 1);
			var y2 = new Variable("y", 2);

			var firstVariables = new List<Variable> { x1, y2 };
			var firstSummand = new Summand(2, firstVariables);

			var seconndVariables = new List<Variable> { x1, y2 };
			var secondSummand = new Summand(2, seconndVariables);

			Assert.True(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_X1_X1Y2_FalseReturned()
		{
			var x1 = new Variable("x", 1);
			var y2 = new Variable("y", 2);

			var firstVariables = new List<Variable> { x1 };
			var firstSummand = new Summand(2, firstVariables);

			var seconndVariables = new List<Variable> { x1, y2 };
			var secondSummand = new Summand(2, seconndVariables);

			Assert.False(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_X1Y2_Y2X1_TrueReturned()
		{
			var x1 = new Variable("x", 1);
			var y2 = new Variable("y", 2);

			var firstVariables = new List<Variable> { x1, y2 };
			var firstSummand = new Summand(2, firstVariables);

			var seconndVariables = new List<Variable> { y2, x1 };
			var secondSummand = new Summand(2, seconndVariables);

			Assert.True(firstSummand == secondSummand);
		}
	}
}
