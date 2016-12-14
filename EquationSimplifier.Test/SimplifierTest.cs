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
		public void GetNextSummand_X1EqualZero_XReturned()
		{
			var parser = new UserInputParser("x^1 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> { variable };
			var result = new Summand(1, x1);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusXEqualZero_MinusXReturned()
		{
			var parser = new UserInputParser("-x = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> { variable };
			var result = new Summand(-1, x1);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusX1EqualZero_MinusX1Returned()
		{
			var parser = new UserInputParser("-x^1 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> { variable };
			var result = new Summand(-1, x1);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_Minus2point3XEqualZero_Minus2point3XReturned()
		{
			var parser = new UserInputParser("-2.3x = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> { variable };
			var result = new Summand(-2.3, x1);

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

		[Fact]
		public void GetNextSummand_2XEqualZero_2XReturned()
		{
			var parser = new UserInputParser("2x = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x1 = new Variable("x", 1);
			var variables = new List<Variable> { x1 };
			var result = new Summand(2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_Minus2XEqualZero_Minus2XReturned()
		{
			var parser = new UserInputParser("-2x = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var variable = new Variable("x", 1);
			var x2 = new List<Variable> { variable };
			var result = new Summand(-2, x2);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_2X2EqualZero_2X2Returned()
		{
			var parser = new UserInputParser("2x^2 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x1 = new Variable("x", 2);
			var variables = new List<Variable> { x1 };
			var result = new Summand(2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_Minus2X2EqualZero_Minus2X2Returned()
		{
			var parser = new UserInputParser("-2x^2 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x1 = new Variable("x", 2);
			var variables = new List<Variable> { x1 };
			var result = new Summand(-2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_2point5X4EqualZero_2point5X4Returned()
		{
			var parser = new UserInputParser("2.5x^4 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x4 = new Variable("x", 4);
			var variables = new List<Variable> { x4 };
			var result = new Summand(2.5, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_3point74Y5EqualZero_3point74Y5Returned()
		{
			var parser = new UserInputParser("3.74y^5 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var y5 = new Variable("y", 5);
			var variables = new List<Variable> { y5 };
			var result = new Summand(3.74, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_XYEqualZero_XYReturned()
		{
			var parser = new UserInputParser("xy = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 1);
			var y = new Variable("y", 1);
			var variables = new List<Variable> { x, y };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_YXEqualZero_YXReturned()
		{
			var parser = new UserInputParser("yx = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 1);
			var y = new Variable("y", 1);
			var variables = new List<Variable> { y, x };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusXYEqualZero_MinusXYReturned()
		{
			var parser = new UserInputParser("-xy = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 1);
			var y = new Variable("y", 1);
			var variables = new List<Variable> { x, y };
			var result = new Summand(-1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_X2Y3EqualZero_X2Y3Returned()
		{
			var parser = new UserInputParser("x^2y^3 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 2);
			var y = new Variable("y", 3);
			var variables = new List<Variable> { x, y };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusX2Y3EqualZero_MinusX2Y3Returned()
		{
			var parser = new UserInputParser("-x^2y^3 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 2);
			var y = new Variable("y", 3);
			var variables = new List<Variable> { x, y };
			var result = new Summand(-1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_4point2X2Y3EqualZero_4point2X2Y3Returned()
		{
			var parser = new UserInputParser("4.2x^2y^3 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 2);
			var y = new Variable("y", 3);
			var variables = new List<Variable> { x, y };
			var result = new Summand(4.2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_Minus4point2X2Y3EqualZero_Minus4point2X2Y3Returned()
		{
			var parser = new UserInputParser("-4.2x^2y^3 = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x2 = new Variable("x", 2);
			var y3 = new Variable("y", 3);
			var variables = new List<Variable> { x2, y3 };
			var result = new Summand(-4.2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusXInBracketsEqualZero_MinusXReturned()
		{
			var parser = new UserInputParser("-(x) = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 1);
			var variables = new List<Variable> { x };
			var result = new Summand(-1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusMinusXInBracketsEqualZero_XReturned()
		{
			var parser = new UserInputParser("-(-(x)) = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 1);
			var variables = new List<Variable> { x };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusMinusX3InBracketsEqualZero_X3Returned()
		{
			var parser = new UserInputParser("-(-(x^3)) = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 3);
			var variables = new List<Variable> { x };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusX3InBracketsEqualZero_X3Returned()
		{
			var parser = new UserInputParser("-(-x^3) = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x = new Variable("x", 3);
			var variables = new List<Variable> { x };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusMinus4point2X2Y3InTwoBracketsEqualZero_Minus4point2X2Y3Returned()
		{
			var parser = new UserInputParser("-(-(4.2x^2y^3)) = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x2 = new Variable("x", 2);
			var y3 = new Variable("y", 3);
			var variables = new List<Variable> { x2, y3 };
			var result = new Summand(4.2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_MinusMinus4point2X2Y3InOneBracketsEqualZero_Minus4point2X2Y3Returned()
		{
			var parser = new UserInputParser("-(-4.2x^2y^3) = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x2 = new Variable("x", 2);
			var y3 = new Variable("y", 3);
			var variables = new List<Variable> { x2, y3 };
			var result = new Summand(4.2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}

		[Fact]
		public void GetNextSummand_3Minus3Brackets24point654X72_Minus24point654X72Returned()
		{
			var parser = new UserInputParser("(-(-(-24.654x^72))) = 0");
			var writer = new ConsoleWriter();
			var simplifier = new Simplifier(parser, writer);
			var x72 = new Variable("x", 72);
			var variables = new List<Variable> { x72 };
			var result = new Summand(-24.654, variables);

			var summand = simplifier.GetNextSummand();

			Assert.True(summand == result);
		}
	}
}
