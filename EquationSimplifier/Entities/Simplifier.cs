using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using EquationSimplifier.Entities.Parsers;
using EquationSimplifier.Entities.Writers;

namespace EquationSimplifier.Entities
{
	public class Simplifier
	{
		private const string PowerSymbol = "^";
		private const string DotSymbol = ".";
		private const string PlusSymbol = "+";
		private const string MinusSymbol = "-";
		private const string EqualSymbol = "=";

		public enum SimplifierState
		{
			None,
			TheIntegerPartOfCoeficientOrZero,
			Dot,
			FractionalPartOfCorficient,
			Variable,
			Power,
			PowerCoeficient,
			OpenBracket,
			CloseBracket
		}

		private SimplifierState _state;
		private readonly IParser _parser;
		private readonly IWriter _writer;
		private List<Summand> _summands = new List<Summand>();

		private Variable _variable;
		private Summand _summand;

		private readonly StringBuilder _coeficientBuilder;

		private bool _finished;

		public Simplifier(IParser parser, IWriter writer, SimplifierState state = SimplifierState.None)
		{
			_parser = parser;
			_writer = writer;
			_state = state;

			_summand = new Summand(1, new List<Variable>());
			_coeficientBuilder = new StringBuilder();
		}

		public Summand GetNextSummand()
		{
			_summands.Add(_summand);
			var summandDone = false;

			while (true)
			{
				var character = _parser.GetNextCharacter();

				if (string.IsNullOrEmpty(character))
				{
					_finished = true;
					break;
				}

				switch (_state)
				{
					case SimplifierState.None:
						if (char.IsNumber(character, 0))
						{
							_coeficientBuilder.Append(character);
							_state = SimplifierState.TheIntegerPartOfCoeficientOrZero;
						}
						else if (char.IsLetter(character, 0))
						{
							_variable = new Variable(character, 1);
							//_summand.Variables.Add(_variable);
							_state = SimplifierState.Variable;
						}

						break;
					case SimplifierState.TheIntegerPartOfCoeficientOrZero:
						if (character == DotSymbol)
						{
							_coeficientBuilder.Append(character);
							_state = SimplifierState.Dot;
						}
						else if (char.IsNumber(character, 0))
						{
							_coeficientBuilder.Append(character);
							_state = SimplifierState.FractionalPartOfCorficient;
						}
						else if (char.IsLetter(character, 0))
						{
							_summand.Coeficient = double.Parse(_coeficientBuilder.ToString());
							_coeficientBuilder.Clear();

							_variable = new Variable(character, 1);

							_state = SimplifierState.Variable;
						}

						break;
					case SimplifierState.Variable:
						if (char.IsLetter(character, 0))
						{
							_summand.Variables.Add(_variable);
							_variable = new Variable(character, 1);
						} 
						else if (character == PowerSymbol)
						{
							_state = SimplifierState.Power;
						}
						else if (character == PlusSymbol || character == MinusSymbol || character == EqualSymbol)
						{
							_summand.Variables.Add(_variable);

							_state = SimplifierState.None;

							summandDone = true;
						}

						break;
					case SimplifierState.Power:
						if (char.IsNumber(character, 0))
						{
							_coeficientBuilder.Append(character);
							_state = SimplifierState.PowerCoeficient;
						}
						else
						{
							throw new Exception();
						}

						break;
					case SimplifierState.PowerCoeficient:
						if (char.IsNumber(character, 0))
						{
							_coeficientBuilder.Append(character);
						}
						else if (char.IsLetter(character, 0))
						{
							_variable.Power = int.Parse(_coeficientBuilder.ToString());
							_coeficientBuilder.Clear();

							_summand.Variables.Add(_variable);
							_variable = new Variable(character, 1);
							
							_state = SimplifierState.Variable;
						}
						else if (character == PlusSymbol || character == MinusSymbol || character == EqualSymbol)
						{
							_variable.Power = int.Parse(_coeficientBuilder.ToString());
							_coeficientBuilder.Clear();

							_summand.Variables.Add(_variable);

							_state = SimplifierState.None;

							summandDone = true;
						}
						else
						{
							throw new Exception();
						}

						break;
					case SimplifierState.Dot:
						if (char.IsNumber(character, 0))
						{
							_coeficientBuilder.Append(character);
							_state = SimplifierState.FractionalPartOfCorficient;
						}
						else
						{
							throw new Exception();
						}

						break;
					case SimplifierState.FractionalPartOfCorficient:
						if (char.IsNumber(character, 0))
						{
							_coeficientBuilder.Append(character);
						}
						else if (char.IsLetter(character, 0))
						{
							_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
							_coeficientBuilder.Clear();

							_variable = new Variable(character, 1);

							_state = SimplifierState.Variable;
						}
						else
						{
							throw new Exception();
						}

						break;
					default:
						throw new Exception("Unhandle simplifier state");
				}

				if (_finished || summandDone) break;
			}

			return _summand;
		}
	}
}
