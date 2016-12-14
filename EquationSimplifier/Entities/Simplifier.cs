using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using EquationSimplifier.Entities.Factories;
using EquationSimplifier.Entities.Parsers;
using EquationSimplifier.Entities.Writers;

namespace EquationSimplifier.Entities
{
	public class Simplifier
	{
		const double Eps = 0.00000001;
		private const string PowerSymbol = "^";
		private const string DotSymbol = ".";
		private const string PlusSymbol = "+";
		private const string MinusSymbol = "-";
		private const string EqualSymbol = "=";
		private const string OpenBracketSymbol = "(";
		private const string CloseBracketSymbol = ")";

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
			CloseBracket,
			Minus
		}

		private SimplifierState _state;
		private readonly IParser _parser;
		private readonly IWriter _writer;
		private List<Summand> _summands = new List<Summand>();

		private Variable _variable;
		private short _variableSign = 1;
		private Summand _summand;

		private readonly StringBuilder _coeficientBuilder;

		private bool _finished;

		public Simplifier(BaseInputOutputFactory factory, SimplifierState state = SimplifierState.None)
		{
			_parser = factory.CreateParser();
			_writer = factory.CreateWriter();
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
						NoneStateHandle(character);

						break;
					case SimplifierState.Minus:
						MinusStateHandle(character);

						break;
					case SimplifierState.OpenBracket:
						OpenBrackerStateHandle(character);

						break;
					case SimplifierState.TheIntegerPartOfCoeficientOrZero:
						summandDone = TheIntegerPartOfCoeficientOrZeroStateHandle(character);

						break;
					case SimplifierState.Variable:
						summandDone = VariableStateHandle(character);

						break;
					case SimplifierState.Power:
						PowerStateHandle(character);

						break;
					case SimplifierState.PowerCoeficient:
						summandDone = PowerCoeficientStateHandle(character);

						break;
					case SimplifierState.Dot:
						DotStateHandle(character);

						break;
					case SimplifierState.FractionalPartOfCorficient:
						summandDone = FractionalPartOfCorficientStateHandle(character);

						break;
					case SimplifierState.CloseBracket:
						CloseBracketStateHandle(character);

						break;
					default:
						throw new Exception("Unhandle simplifier state");
				}

				if (_finished || summandDone) break;
			}

			_summand.Coeficient *= _variableSign;

			// check if coeficient == 0 then make it a zero constant
			if (Math.Abs(_summand.Coeficient) < Eps)
			{
				_summand.MakeConstant(0);
			}
			return _summand;
		}

		private void CloseBracketStateHandle(string character)
		{
			if (character == CloseBracketSymbol)
			{
				_state = SimplifierState.CloseBracket;
			}
			else if (character == PlusSymbol || character == MinusSymbol || character == EqualSymbol)
			{
				_state = SimplifierState.None;
			}
			else
			{
				throw new Exception();
			}
		}

		private bool FractionalPartOfCorficientStateHandle(string character)
		{
			var summandDone = false;

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
			else if (character == PlusSymbol || character == MinusSymbol || character == EqualSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
				_coeficientBuilder.Clear();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == CloseBracketSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
				_coeficientBuilder.Clear();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.CloseBracket;
			}
			else
			{
				throw new Exception();
			}

			return summandDone;
		}

		private void DotStateHandle(string character)
		{
			if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.FractionalPartOfCorficient;
			}
			else
			{
				throw new Exception();
			}
		}

		private bool PowerCoeficientStateHandle(string character)
		{
			var summandDone = false;
			if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
			}
			else if (char.IsLetter(character, 0))
			{
				_variable.Power = int.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_summand.AddVariable(ref _variable);
				_variable = new Variable(character, 1);

				_state = SimplifierState.Variable;
			}
			else if (character == PlusSymbol || character == MinusSymbol || character == EqualSymbol)
			{
				_variable.Power = int.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_summand.AddVariable(ref _variable);

				_state = SimplifierState.None;

				summandDone = true;
			}
			else if (character == CloseBracketSymbol)
			{
				_variable.Power = int.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_summand.AddVariable(ref _variable);

				_state = SimplifierState.CloseBracket;
			}
			else
			{
				throw new Exception();
			}
			return summandDone;
		}

		private void PowerStateHandle(string character)
		{
			if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.PowerCoeficient;
			}
			else
			{
				throw new Exception();
			}
		}

		private bool VariableStateHandle(string character)
		{
			var summandDone = false;
			if (char.IsLetter(character, 0))
			{
				_summand.AddVariable(ref _variable);
				_variable = new Variable(character, 1);
			}
			else if (character == PowerSymbol)
			{
				_state = SimplifierState.Power;
			}
			else if (character == PlusSymbol || character == MinusSymbol || character == EqualSymbol)
			{
				_summand.AddVariable(ref _variable);

				_state = SimplifierState.None;

				summandDone = true;
			}
			else if (character == CloseBracketSymbol)
			{
				_summand.AddVariable(ref _variable);

				_state = SimplifierState.CloseBracket;
			}
			else
			{
				throw new Exception();
			}
			return summandDone;
		}

		private bool TheIntegerPartOfCoeficientOrZeroStateHandle(string character)
		{
			var summandDone = false;

			if (character == DotSymbol)
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.Dot;
			}
			else if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.TheIntegerPartOfCoeficientOrZero;
			}
			else if (char.IsLetter(character, 0))
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_variable = new Variable(character, 1);

				_state = SimplifierState.Variable;
			}
			else if (character == PlusSymbol || character == MinusSymbol || character == EqualSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.None;
			}
			else
			{
				throw new Exception();
			}

			return summandDone;
		}

		private void OpenBrackerStateHandle(string character)
		{
			if (character == OpenBracketSymbol)
			{
				_state = SimplifierState.OpenBracket;
			}
			else if (char.IsLetter(character, 0))
			{
				_variable = new Variable(character, 1);
				_state = SimplifierState.Variable;
			}
			else if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.TheIntegerPartOfCoeficientOrZero;
			}
			else if (character == MinusSymbol)
			{
				_variableSign *= -1;
				_state = SimplifierState.Minus;
			}
			else
			{
				throw new Exception();
			}
		}

		private void MinusStateHandle(string character)
		{
			if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.TheIntegerPartOfCoeficientOrZero;
			}
			else if (char.IsLetter(character, 0))
			{
				_variable = new Variable(character, 1);
				_state = SimplifierState.Variable;
			}
			else if (character == OpenBracketSymbol)
			{
				_state = SimplifierState.OpenBracket;
			}
			else
			{
				throw new Exception();
			}
		}

		private void NoneStateHandle(string character)
		{
			if (character == MinusSymbol)
			{
				_variableSign *= -1;
				_state = SimplifierState.Minus;
			}
			else if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.TheIntegerPartOfCoeficientOrZero;
			}
			else if (char.IsLetter(character, 0))
			{
				_variable = new Variable(character, 1);
				_state = SimplifierState.Variable;
			}
			else if (character == OpenBracketSymbol)
			{
				_state = SimplifierState.OpenBracket;
			}
			else
			{
				throw new Exception();
			}
		}
	}
}
