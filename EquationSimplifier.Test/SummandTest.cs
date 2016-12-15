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
			var firstSummand = new Summand(1, firstVariables);

			var seconndVariables = new List<Variable> {x1};
			var secondSummand = new Summand(1, seconndVariables);

			Assert.True(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_X1Y2_X1Y2_TrueReturned()
		{
			var x1 = new Variable("x", 1);
			var y2 = new Variable("y", 2);

			var firstVariables = new List<Variable> {x1, y2};
			var firstSummand = new Summand(1, firstVariables);

			var seconndVariables = new List<Variable> {x1, y2};
			var secondSummand = new Summand(1, seconndVariables);

			Assert.True(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_X1_X1Y2_FalseReturned()
		{
			var x1 = new Variable("x", 1);
			var y2 = new Variable("y", 2);

			var firstVariables = new List<Variable> {x1};
			var firstSummand = new Summand(1, firstVariables);

			var seconndVariables = new List<Variable> {x1, y2};
			var secondSummand = new Summand(1, seconndVariables);

			Assert.False(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_X1Y2_Y2X1_TrueReturned()
		{
			var x1 = new Variable("x", 1);
			var y2 = new Variable("y", 2);

			var firstVariables = new List<Variable> {x1, y2};
			var firstSummand = new Summand(1, firstVariables);

			var seconndVariables = new List<Variable> {y2, x1};
			var secondSummand = new Summand(1, seconndVariables);

			Assert.True(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_2X1_X1_FalseReturned()
		{
			var x1 = new Variable("x", 1);

			var firstVariables = new List<Variable> { x1 };
			var firstSummand = new Summand(1, firstVariables);

			var seconndVariables = new List<Variable> { x1 };
			var secondSummand = new Summand(2, seconndVariables);

			Assert.False(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_1point2_1point2_TrueReturned()
		{
			var constant = new Variable(string.Empty, 0);

			var first = new Summand(1.2, new List<Variable> {constant});
			var second = new Summand(1.2, new List<Variable> { constant });

			Assert.Equal(first, second);
		}

		[Fact]
		public void Equals_X1_Null_FalseReturned()
		{
			var x1 = new Variable("x", 1);

			var firstVariables = new List<Variable> {x1};
			var firstSummand = new Summand(1, firstVariables);

			Summand secondSummand = null;

			Assert.False(firstSummand == secondSummand);
		}

		[Fact]
		public void Equals_Null_X1_FalseReturned()
		{
			var x1 = new Variable("x", 1);

			var secondVariable = new List<Variable> {x1};
			var secondSummand = new Summand(1, secondVariable);

			Summand firstSummand = null;

			Assert.False(firstSummand == secondSummand);
		}
	}
}
