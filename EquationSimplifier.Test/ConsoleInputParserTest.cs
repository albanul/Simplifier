using EquationSimplifier.Entities.Parsers;
using Xunit;

namespace EquationSimplifier.Test
{
	public class ConsoleInputParserTest
	{
		private static void GetNextCharacter(string sourceValue, string result)
		{
			var parser = new ConsoleInputParser(sourceValue);

			var character = parser.GetNextCharacter();

			Assert.Equal(result, character);
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
		public void GetNextCharacter_SingleCharacter_SingleCharacterReturn(string value)
		{
			GetNextCharacter(value, value);
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
		public void GetNextCharacter_SingleCharacterWithAlotWhitespace_SingleCharachterReturn(string value)
		{
			GetNextCharacter(value, value.Trim());
		}

		[Fact]
		public void GetNextCharacter_WhitespaceString_NullReturn()
		{
			GetNextCharacter(" ", null);
		}

		[Fact]
		public void GetNextCharacter_TwoBracketsDividedByWhitespaces_TwoBracketsWithoutWhitespacesReturn()
		{
			var parser = new ConsoleInputParser("     (             )    ");

			var character1 = parser.GetNextCharacter();
			var character2 = parser.GetNextCharacter();

			Assert.Equal("(", character1);
			Assert.Equal(")", character2);
		}
	}
}
