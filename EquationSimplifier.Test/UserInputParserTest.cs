using EquationSimplifier.Parsers;
using Xunit;

namespace EquationSimplifier.Test
{
	public class UserInputParserTest
	{
		[Fact]
		public void GetNextLexem_OpenBracet_OpenBracedReturn()
		{
			var parser = new UserInputParser("(");

			var lexem = parser.GetNextLexem();

			Assert.Equal("(", lexem);
		}

		[Fact]
		public void GetNextLexem_ClosedBracet_ClosedBracedReturn()
		{
			var parser = new UserInputParser(")");

			var lexem = parser.GetNextLexem();

			Assert.Equal(")", lexem);
		}
	}
}
