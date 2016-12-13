using EquationSimplifier.Parsers;
using Xunit;

namespace EquationSimplifier.Test
{
	public class UserInputParserTest
	{
		private static void GetNextLexem(string sourceValue, string result)
		{
			var parser = new UserInputParser(sourceValue);

			var lexem = parser.GetNextLexem();

			Assert.Equal(result, lexem);
		}

		[InlineData("(")]
		[InlineData(")")]
		[InlineData("+")]
		[InlineData("=")]
		[InlineData("x")]
		[InlineData("y")]
		[InlineData("z")]
		[InlineData("0")]
		[Theory]
		public void GetNextLexem_SingleCharacter_SingleCharacterReturn(string value)
		{
			GetNextLexem(value, value);
		}

		[InlineData("     (")]
		[InlineData("  )")]
		[InlineData("       +")]
		[InlineData("    =")]
		[InlineData("   x")]
		[InlineData("    y")]
		[InlineData("        z")]
		[InlineData(" 0")]
		[Theory]
		public void GetNextLexem_SingleCharacterLexemWithAlotWhitespace_SingleCharachterLexemReturn(string value)
		{
			GetNextLexem(value, value.Trim());
		}

		[Fact]
		public void GetNextLexem_WhitespaceString_NullReturn()
		{
			GetNextLexem(" ", null);
		}
	}
}
