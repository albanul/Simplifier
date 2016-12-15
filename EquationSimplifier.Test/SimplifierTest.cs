using System;
using System.Collections.Generic;
using System.IO;
using EquationSimplifier.Entities;
using EquationSimplifier.Entities.Factories;
using Xunit;

namespace EquationSimplifier.Test
{
	public class SimplifierTest
	{
		#region GetNextSummand method tests

		[Fact]
		public void GetNextSummand_XEqualZero_XReturned()
		{
			var factory = new ConsoleInputOutputFactory("x = 0");
			var simplifier = new Simplifier(factory);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> {variable};
			var result = new Summand(1, x1);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X1EqualZero_XReturned()
		{
			var factory = new ConsoleInputOutputFactory("x^1 = 0");
			var simplifier = new Simplifier(factory);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> { variable };
			var result = new Summand(1, x1);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusXEqualZero_MinusXReturned()
		{
			var factory = new ConsoleInputOutputFactory("-x = 0");
			var simplifier = new Simplifier(factory);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> { variable };
			var result = new Summand(-1, x1);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusX1EqualZero_MinusX1Returned()
		{
			var factory = new ConsoleInputOutputFactory("-x^1 = 0");
			var simplifier = new Simplifier(factory);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> { variable };
			var result = new Summand(-1, x1);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_Minus2point3XEqualZero_Minus2point3XReturned()
		{
			var factory = new ConsoleInputOutputFactory("-2.3x = 0");
			var simplifier = new Simplifier(factory);
			var variable = new Variable("x", 1);
			var x1 = new List<Variable> { variable };
			var result = new Summand(-2.3, x1);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X2EqualZero_X2Returned()
		{
			var factory = new ConsoleInputOutputFactory("x^2 = 0");
			var simplifier = new Simplifier(factory);
			var x2 = new Variable("x", 2);
			var variables = new List<Variable> { x2 };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_2XEqualZero_2XReturned()
		{
			var factory = new ConsoleInputOutputFactory("2x = 0");
			var simplifier = new Simplifier(factory);
			var x1 = new Variable("x", 1);
			var variables = new List<Variable> { x1 };
			var result = new Summand(2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_Minus2XEqualZero_Minus2XReturned()
		{
			var factory = new ConsoleInputOutputFactory("-2x = 0");
			var simplifier = new Simplifier(factory);
			var variable = new Variable("x", 1);
			var x2 = new List<Variable> { variable };
			var result = new Summand(-2, x2);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_2X2EqualZero_2X2Returned()
		{
			var factory = new ConsoleInputOutputFactory("2x^2 = 0");
			var simplifier = new Simplifier(factory);
			var x1 = new Variable("x", 2);
			var variables = new List<Variable> { x1 };
			var result = new Summand(2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_Minus2X2EqualZero_Minus2X2Returned()
		{
			var factory = new ConsoleInputOutputFactory("-2x^2 = 0");
			var simplifier = new Simplifier(factory);
			var x1 = new Variable("x", 2);
			var variables = new List<Variable> { x1 };
			var result = new Summand(-2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_2point5X4EqualZero_2point5X4Returned()
		{
			var factory = new ConsoleInputOutputFactory("2.5x^4 = 0");
			var simplifier = new Simplifier(factory);
			var x4 = new Variable("x", 4);
			var variables = new List<Variable> { x4 };
			var result = new Summand(2.5, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_3point74Y5EqualZero_3point74Y5Returned()
		{
			var factory = new ConsoleInputOutputFactory("3.74y^5 = 0");
			var simplifier = new Simplifier(factory);
			var y5 = new Variable("y", 5);
			var variables = new List<Variable> { y5 };
			var result = new Summand(3.74, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_XYEqualZero_XYReturned()
		{
			var factory = new ConsoleInputOutputFactory("xy = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 1);
			var y = new Variable("y", 1);
			var variables = new List<Variable> { x, y };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_YXEqualZero_YXReturned()
		{
			var factory = new ConsoleInputOutputFactory("yx = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 1);
			var y = new Variable("y", 1);
			var variables = new List<Variable> { y, x };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusXYEqualZero_MinusXYReturned()
		{
			var factory = new ConsoleInputOutputFactory("-xy = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 1);
			var y = new Variable("y", 1);
			var variables = new List<Variable> { x, y };
			var result = new Summand(-1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X2Y3EqualZero_X2Y3Returned()
		{
			var factory = new ConsoleInputOutputFactory("x^2y^3 = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 2);
			var y = new Variable("y", 3);
			var variables = new List<Variable> { x, y };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusX2Y3EqualZero_MinusX2Y3Returned()
		{
			var factory = new ConsoleInputOutputFactory("-x^2y^3 = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 2);
			var y = new Variable("y", 3);
			var variables = new List<Variable> { x, y };
			var result = new Summand(-1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_4point2X2Y3EqualZero_4point2X2Y3Returned()
		{
			var factory = new ConsoleInputOutputFactory("4.2x^2y^3 = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 2);
			var y = new Variable("y", 3);
			var variables = new List<Variable> { x, y };
			var result = new Summand(4.2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_Minus4point2X2Y3EqualZero_Minus4point2X2Y3Returned()
		{
			var factory = new ConsoleInputOutputFactory("-4.2x^2y^3 = 0");
			var simplifier = new Simplifier(factory);
			var x2 = new Variable("x", 2);
			var y3 = new Variable("y", 3);
			var variables = new List<Variable> { x2, y3 };
			var result = new Summand(-4.2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusXInBracketsEqualZero_MinusXReturned()
		{
			var factory = new ConsoleInputOutputFactory("-(x) = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 1);
			var variables = new List<Variable> { x };
			var result = new Summand(-1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusMinusXInBracketsEqualZero_XReturned()
		{
			var factory = new ConsoleInputOutputFactory("-(-(x)) = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 1);
			var variables = new List<Variable> { x };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusMinusX3InBracketsEqualZero_X3Returned()
		{
			var factory = new ConsoleInputOutputFactory("-(-(x^3)) = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 3);
			var variables = new List<Variable> { x };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusX3InBracketsEqualZero_X3Returned()
		{
			var factory = new ConsoleInputOutputFactory("-(-x^3) = 0");
			var simplifier = new Simplifier(factory);
			var x = new Variable("x", 3);
			var variables = new List<Variable> { x };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusMinus4point2X2Y3InTwoBracketsEqualZero_Minus4point2X2Y3Returned()
		{
			var factory = new ConsoleInputOutputFactory("-(-(4.2x^2y^3)) = 0");
			var simplifier = new Simplifier(factory);
			var x2 = new Variable("x", 2);
			var y3 = new Variable("y", 3);
			var variables = new List<Variable> { x2, y3 };
			var result = new Summand(4.2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_MinusMinus4point2X2Y3InOneBracketsEqualZero_Minus4point2X2Y3Returned()
		{
			var factory = new ConsoleInputOutputFactory("-(-4.2x^2y^3) = 0");
			var simplifier = new Simplifier(factory);
			var x2 = new Variable("x", 2);
			var y3 = new Variable("y", 3);
			var variables = new List<Variable> { x2, y3 };
			var result = new Summand(4.2, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_3Minus3Brackets24point654X72_Minus24point654X72Returned()
		{
			var factory = new ConsoleInputOutputFactory("(-(-(-24.654x^72))) = 0");
			var simplifier = new Simplifier(factory);
			var x72 = new Variable("x", 72);
			var variables = new List<Variable> { x72 };
			var result = new Summand(-24.654, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSumman_ZeroEqualZero_ZeroReturned()
		{
			var factory = new ConsoleInputOutputFactory("0=0");
			var simplifier = new Simplifier(factory);
			var zero = new Variable(string.Empty, 0);
			var variables = new List<Variable> {zero};
			var result = new Summand(0, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSumman_0point0EqualZero_ZeroReturned()
		{
			var factory = new ConsoleInputOutputFactory("0.0=0");
			var simplifier = new Simplifier(factory);
			var zero = new Variable(string.Empty, 0);
			var variables = new List<Variable> { zero };
			var result = new Summand(0, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSumman_Zeropoint5EqualZero_Zeropoint5Returned()
		{
			var factory = new ConsoleInputOutputFactory("0.5=0.5");
			var simplifier = new Simplifier(factory);
			var half = new Variable(string.Empty, 0);
			var variables = new List<Variable> { half };
			var result = new Summand(0.5, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSumman_MinusZeropoint5EqualZero_MinusZeropoint5Returned()
		{
			var factory = new ConsoleInputOutputFactory("-0.5=0");
			var simplifier = new Simplifier(factory);
			var half = new Variable(string.Empty, 0);
			var variables = new List<Variable> { half };
			var result = new Summand(-0.5, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSumman_MinusMinusZeropoint5EqualZero_Zeropoint5Returned()
		{
			var factory = new ConsoleInputOutputFactory("-(-0.5)=0");
			var simplifier = new Simplifier(factory);
			var half = new Variable(string.Empty, 0);
			var variables = new List<Variable> { half };
			var result = new Summand(0.5, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X0EqualZero_1Returned()
		{
			var factory = new ConsoleInputOutputFactory("x^0=0");
			var simplifier = new Simplifier(factory);
			var one = new Variable(string.Empty, 0);
			var variables = new List<Variable> {one};
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X0Y0EqualZero_1Returned()
		{
			var factory = new ConsoleInputOutputFactory("x^0y^0=0");
			var simplifier = new Simplifier(factory);
			var one = new Variable(string.Empty, 0);
			var variables = new List<Variable> { one };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X0YEqualZero_YReturned()
		{
			var factory = new ConsoleInputOutputFactory("x^0y=0");
			var simplifier = new Simplifier(factory);
			var y = new Variable("y", 1);
			var variables = new List<Variable> { y };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X0Y1Z0EqualZero_YReturned()
		{
			var factory = new ConsoleInputOutputFactory("x^0y^1z^0=0");
			var simplifier = new Simplifier(factory);
			var y = new Variable("y", 1);
			var variables = new List<Variable> { y };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_0X3Y1EqualZero_0Returned()
		{
			var factory = new ConsoleInputOutputFactory("0x^3y^1=0");
			var simplifier = new Simplifier(factory);
			var zero = new Variable(string.Empty, 0);
			var variables = new List<Variable> { zero };
			var result = new Summand(0, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_0X3YEqualZero_0Returned()
		{
			var factory = new ConsoleInputOutputFactory("0x^3y=0");
			var simplifier = new Simplifier(factory);
			var zero = new Variable(string.Empty, 0);
			var variables = new List<Variable> { zero };
			var result = new Summand(0, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_0point0X3YEqualZero_0Returned()
		{
			var factory = new ConsoleInputOutputFactory("0.0x^3y=0");
			var simplifier = new Simplifier(factory);
			var zero = new Variable(string.Empty, 0);
			var variables = new List<Variable> { zero };
			var result = new Summand(0, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_XXX_X3Returned()
		{
			var factory = new ConsoleInputOutputFactory("xxx=0");
			var simplifier = new Simplifier(factory);
			var x3 = new Variable("x", 3);
			var variables = new List<Variable> { x3 };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X1X2X3_X6Returned()
		{
			var factory = new ConsoleInputOutputFactory("x^1x^2x^3=0");
			var simplifier = new Simplifier(factory);
			var x6 = new Variable("x", 6);
			var variables = new List<Variable> { x6 };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		[Fact]
		public void GetNextSummand_X1Y2X3Y5_X4Y7Returned()
		{
			var factory = new ConsoleInputOutputFactory("x^1y^2x^3y^5=0");
			var simplifier = new Simplifier(factory);
			var x3 = new Variable("x", 4);
			var y7 = new Variable("y", 7);
			var variables = new List<Variable> { x3, y7 };
			var result = new Summand(1, variables);

			var summand = simplifier.GetNextSummand();

			Assert.Equal(result, summand);
		}

		#endregion

		#region Simplify method tests

		[Fact]
		public void Simplify_XEqual0_ListWithXReturned()
		{
			var factory = new ConsoleInputOutputFactory("x=0");
			var simplifier = new Simplifier(factory);

			var variables = new List<Variable> {new Variable("x", 1)};
			var summand = new Summand(1, variables);
			var result = new List<Summand> {summand};

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_XPlusYEqual0_ListWithXAndYReturned()
		{
			var factory = new ConsoleInputOutputFactory("x+y=0");
			var simplifier = new Simplifier(factory);
			var x = new Summand(1, new List<Variable>
			{
				new Variable("x", 1)
			});
			var y = new Summand(1, new List<Variable>
			{
				new Variable("y", 1)
			});
			var result = new List<Summand> { x, y };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_X1PlusY2Equal0_ListWithY2AndX1Returned()
		{
			var factory = new ConsoleInputOutputFactory("x^1+y^2=0");
			var simplifier = new Simplifier(factory);
			var x = new Summand(1, new List<Variable>
			{
				new Variable("x", 1)
			});
			var y2 = new Summand(1, new List<Variable>
			{
				new Variable("y", 2)
			});
			var result = new List<Summand> { y2, x };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_X1PlusX2Y3PlusY2Equal0_ListWithX2Y3AndY2AndX1Returned()
		{
			var factory = new ConsoleInputOutputFactory("x^1+x^2y^3+y^2=0");
			var simplifier = new Simplifier(factory);
			var x = new Summand(1, new List<Variable>
			{
				new Variable("x", 1)
			});
			var x2y3 = new Summand(1, new List<Variable>
			{
				new Variable("x", 2),
				new Variable("y", 3)
			});
			var y2 = new Summand(1, new List<Variable>
			{
				new Variable("y", 2)
			});
			var result = new List<Summand> { x2y3, y2, x };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_XMinusYEqual0_ListWithXAndMinusYReturned()
		{
			var factory = new ConsoleInputOutputFactory("x-y=0");
			var simplifier = new Simplifier(factory);
			var x = new Summand(1, new List<Variable>
			{
				new Variable("x", 1)
			});
			var y = new Summand(-1, new List<Variable>
			{
				new Variable("y", 1)
			});
			var result = new List<Summand> { x, y };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_MinusXPlusYEqual0_ListWithXAndPlusYReturned()
		{
			var factory = new ConsoleInputOutputFactory("-(x+y)=0");
			var simplifier = new Simplifier(factory);
			var x = new Summand(1, new List<Variable>
			{
				new Variable("x", 1)
			});
			var y = new Summand(1, new List<Variable>
			{
				new Variable("y", 1)
			});
			var result = new List<Summand> { x, y };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_MinusXMinusYEqual0_ListWithXMinusYReturned()
		{
			var factory = new ConsoleInputOutputFactory("-(x-y)=0");
			var simplifier = new Simplifier(factory);
			var x = new Summand(1, new List<Variable>
			{
				new Variable("x", 1)
			});
			var y = new Summand(-1, new List<Variable>
			{
				new Variable("y", 1)
			});
			var result = new List<Summand> { x, y };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_MinusMinusXMinusYEqual0_ListWithXAndMinusYReturned()
		{
			var factory = new ConsoleInputOutputFactory("-(-x-y)=0");
			var simplifier = new Simplifier(factory);
			var x = new Summand(1, new List<Variable>
			{
				new Variable("x", 1)
			});
			var y = new Summand(1, new List<Variable>
			{
				new Variable("y", 1)
			});
			var result = new List<Summand> { x, y };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_XPlusXEqual0_List2XReturned()
		{
			var factory = new ConsoleInputOutputFactory("x+x=0");
			var simplifier = new Simplifier(factory);
			var x = new Summand(2, new List<Variable>
			{
				new Variable("x", 1)
			});
			var result = new List<Summand> { x };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_X2PlusX2Equal0_List2XReturned()
		{
			var factory = new ConsoleInputOutputFactory("x^2+x^2=0");
			var simplifier = new Simplifier(factory);
			var x2 = new Summand(2, new List<Variable>
			{
				new Variable("x", 2)
			});
			var result = new List<Summand> { x2 };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_X2PlusMinusX2Equal0_List2XReturned()
		{
			var factory = new ConsoleInputOutputFactory("x^2-x^2=0");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>();

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_XMinusX_EmptyListReturned()
		{
			var factory = new ConsoleInputOutputFactory("x-x=0");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>();

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_XMinusXPlusY_ListYReturned()
		{
			var factory = new ConsoleInputOutputFactory("x-(x+y)=0");
			var simplifier = new Simplifier(factory);
			var y = new Summand(1, new List<Variable>
			{
				new Variable("y", 1)
			});
			var result = new List<Summand> { y };

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_XMinusXPlusYPlusY_EmptyListReturned()
		{
			var factory = new ConsoleInputOutputFactory("x-(x+y)+y=0");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>();

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_SomeNumbersAndBrackets_ListWith1Returned()
		{
			var factory = new ConsoleInputOutputFactory("1-(((1-3)-2-(1-3))-(1-3))=0");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>
			{
				new Summand(1, new List<Variable>())
			};

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_SomeNumbersAndBrackets1_ListWith1Returned()
		{
			var factory = new ConsoleInputOutputFactory("1.5-(-(-(1.5-35.3)))=0");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>
			{
				new Summand(35.3, new List<Variable>())
			};

			var summands = simplifier.Simplify();

			Assert.Equal(result.Count, summands.Count);
			Assert.Equal(result[0].Coeficient, summands[0].Coeficient);
			Assert.True(result[0].EqualVariables(summands[0]));
		}

		[Fact]
		public void Simplify_SomeNumbersAndBrackets2_ListWith1Returned()
		{
			var factory = new ConsoleInputOutputFactory("1.55-(-(-(1.25-35.33)-2-(1.13-343.2))-(1.7-33.33))=0");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>
			{
				new Summand(344.07, new List<Variable>())
			};

			var summands = simplifier.Simplify();

			Assert.Equal(result.Count, summands.Count);
			Assert.Equal(result[0].Coeficient, summands[0].Coeficient);
			Assert.True(result[0].EqualVariables(summands[0]));
		}

		[Fact]
		public void Simplify_XEqualX_Returned()
		{
			var factory = new ConsoleInputOutputFactory("x=x");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>();

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_XMinusXPlusYEqual2X_YPlus2XReturned()
		{
			var factory = new ConsoleInputOutputFactory("x - (x + y) = 2x");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>
			{
				new Summand(2, new List<Variable>
				{
					new Variable("x", 1)
				}),
				new Summand(1, new List<Variable>
				{
					new Variable("y", 1)
				})
			};

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_X2Plus3point5XYPlusYEqualY2MinusXYPlusY_X2MinusY2Plus4point5XYReturned()
		{
			var factory = new ConsoleInputOutputFactory("x^2+3.5xy+y=y^2-xy+y");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>
			{
				new Summand(1, new List<Variable>
				{
					new Variable("x", 2)
				}),
				new Summand(-1, new List<Variable>
				{
					new Variable("y", 2)
				}),
				new Summand(4.5, new List<Variable>
				{
					new Variable("x", 1),
					new Variable("y", 1)
				})
			};

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_ZeroEqualMinusY2_ListWithY2Returned()
		{
			var factory = new ConsoleInputOutputFactory("0=-y^2");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>
			{
				new Summand(1, new List<Variable>
				{
					new Variable("y", 2)
				})
			};

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_ZeroEqualZero_EmptyListReturned()
		{
			var factory = new ConsoleInputOutputFactory("0=0");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>();

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void Simplify_1point2Equal1point2_EmptyListReturned()
		{
			var factory = new ConsoleInputOutputFactory("1.2=1.2");
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>();

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void File_Simplify_1E6OfX_ListWithX1E6Retruned()
		{
			var path = $"{Guid.NewGuid()}.txt";

			using (var sw = new StreamWriter(path))
			{
				for (var i = 0; i < 1000000; i++)
				{
					sw.Write("x");
				}

				sw.Write("=0");
			}

			var factory = new FileInputOutputFactory(path);
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>
			{
				new Summand(1, new List<Variable>
				{
					new Variable("x", 1000000)
				})
			};

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void File_Simplify_5E5OfX_ListWithX1E6Retruned()
		{
			var path = $"{Guid.NewGuid()}.txt";

			using (var sw = new StreamWriter(path))
			{
				for (var i = 0; i < 500000; i++)
				{
					sw.Write("x");
				}

				sw.Write("=");

				for (var i = 0; i < 500000; i++)
				{
					sw.Write("x");
				}
			}

			var factory = new FileInputOutputFactory(path);
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>();

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		[Fact]
		public void File_Simplify_AlotOfXSummedWithInceasingPowerInBackwardOrder_ListWithXInRightOrderRetruned()
		{
			var result = new List<Summand>();

			var path = $"{Guid.NewGuid()}.txt";

			using (var sw = new StreamWriter(path))
			{
				int i;

				Variable variable;

				for (i = 0; i < 1000; i++)
				{
					sw.Write($"x^{i+1} + ");
					variable = new Variable("x", i + 1);
					result.Insert(0, new Summand(1, new List<Variable> {variable}));
				}

				sw.Write($"x^{i + 1} = 0");
				variable = new Variable("x", i + 1);
				result.Insert(0, new Summand(1, new List<Variable> { variable }));
			}

			var factory = new FileInputOutputFactory(path);
			var simplifier = new Simplifier(factory);
			

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}


		[Fact]
		public void File_Simplify_AlotOfXMultipliedWithInceasingPowerInBackwardOrder_ListWithXInRightOrderRetruned()
		{
			var power = 0;
			var path = $"{Guid.NewGuid()}.txt";

			using (var sw = new StreamWriter(path))
			{
				for (var i = 0; i < 1000000; i++)
				{
					sw.Write($"x^{i + 1}");
					power += i + 1;
				}

				sw.Write("=0");
			}

			var factory = new FileInputOutputFactory(path);
			var simplifier = new Simplifier(factory);
			var result = new List<Summand>
			{
				new Summand(1, new List<Variable>
				{
					new Variable("x", power)
				})
			};

			var summands = simplifier.Simplify();

			Assert.Equal(result, summands);
		}

		#endregion
	}
}
