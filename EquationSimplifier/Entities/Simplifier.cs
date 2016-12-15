using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EquationSimplifier.Entities.Factories;
using EquationSimplifier.Entities.Parsers;

namespace EquationSimplifier.Entities
{
	public class Simplifier
	{
		private const double Eps = 1E-10;
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
			Minus,
			MinusPower
		}

		private SimplifierState _state;
		private readonly IParser _parser;
		private readonly List<Summand> _summands = new List<Summand>();

		private Variable _variable;
		private int _currentSummandSign = 1;
		private int _nextSummandSign = 1;
		
		private readonly Stack<int> _stackOfSigns = new Stack<int>();

		private Summand _summand;

		private readonly StringBuilder _coeficientBuilder;

		private bool _finished;
		private bool _equalSymbolPassed;

		public Simplifier(BaseInputOutputFactory factory, SimplifierState state = SimplifierState.None)
		{
			_parser = factory.CreateParser();
			_state = state;

			_summand = new Summand(1, new List<Variable>());
			_coeficientBuilder = new StringBuilder();
			_stackOfSigns.Push(1);
		}

		public List<Summand> Simplify()
		{
			while (!_finished)
			{
				var summand = GetNextSummand();

				// if coeficient != 0
				if (Math.Abs(summand.Coeficient) > Eps)
				{
					var summandsWithSameVariables = _summands.Where(s => s.EqualVariables(summand)).ToArray();

					if (summandsWithSameVariables.Length == 0)
					{
						// add new summand into colletion
						_summands.Add(summand);
					}
					else
					{
						var sameSummand = summandsWithSameVariables[0];
						var newCoeficient = sameSummand.Coeficient += summand.Coeficient;

						if (Math.Abs(newCoeficient) < Eps)
						{
							_summands.Remove(sameSummand);
						}
						else
						{
							sameSummand.Coeficient = newCoeficient;
						}
					}
				}
			}

			_summands.Sort();

			if (_summands.Count > 0 && _summands[0].Coeficient < 0)
			{
				foreach (var summand in _summands)
				{
					summand.Coeficient *= -1;
				}
			}

			return _summands;
		}

		public Summand GetNextSummand()
		{
			var summandDone = false;

			while (true)
			{
				var character = _parser.GetNextCharacter();

				switch (_state)
				{
					case SimplifierState.None:
						summandDone = NoneStateHandle(character);

						break;
					case SimplifierState.Minus:
						MinusStateHandle(character);

						break;
					case SimplifierState.OpenBracket:
						OpenBrackerStateHandle(character);

						break;
					case SimplifierState.TheIntegerPartOfCoeficientOrZero:
						summandDone = IntegerPartOfCoeficientStateHandle(character);

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
						summandDone = CloseBracketStateHandle(character);

						break;
					case SimplifierState.MinusPower:
						MinusPowerStateHanlder(character);
						break;
					default:
						throw new Exception("Unhandle simplifier state");
				}

				if (_finished || summandDone) break;
			}

			// set sign for the next summand
			_currentSummandSign = _nextSummandSign;
			_nextSummandSign = 1;

			// check if coeficient == 0 then make it a zero constant
			if (Math.Abs(_summand.Coeficient) < Eps)
			{
				_summand.MakeConstant(0);
			}

			// remember link to the summand
			var summand = _summand;

			// create new summand
			_summand = new Summand(1, new List<Variable>());

			return summand;
		}

		private void MinusPowerStateHanlder(string character)
		{
			if (string.IsNullOrEmpty(character))
			{
				throw new SyntaxException();
			}
			else if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.PowerCoeficient;
			}
			else
			{
				throw new SyntaxException();
			}
		}

		private bool NoneStateHandle(string character)
		{
			var summandDone = false;

			if (string.IsNullOrEmpty(character))
			{
				//_summand.Coeficient = 0;
				//summandDone = true;
				//_finished = true;
				throw new SyntaxException();
			}
			else if (character == MinusSymbol)
			{
				_currentSummandSign = -1;
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
				FixCoeficient();
				_state = SimplifierState.Variable;
			}
			else if (character == OpenBracketSymbol)
			{
				_stackOfSigns.Push(_currentSummandSign * _stackOfSigns.Peek());
				_currentSummandSign = 1;
				_state = SimplifierState.OpenBracket;
			}
			else
			{
				throw new SyntaxException();
			}

			return summandDone;
		}

		private bool FractionalPartOfCorficientStateHandle(string character)
		{
			var summandDone = false;

			if (string.IsNullOrEmpty(character))
			{
				if (!_equalSymbolPassed)
				{
					throw new SyntaxException();
				}

				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
				_coeficientBuilder.Clear();

				FixCoeficient();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;
				_finished = true;

				_state = SimplifierState.None;
			}
			else if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
			}
			else if (char.IsLetter(character, 0))
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
				_coeficientBuilder.Clear();

				_variable = new Variable(character, 1);

				FixCoeficient();

				_state = SimplifierState.Variable;
			}
			else if (character == PlusSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
				_coeficientBuilder.Clear();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				FixCoeficient();

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == EqualSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
				_coeficientBuilder.Clear();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				FixCoeficient();
				ReverseSignInStack();

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == MinusSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
				_coeficientBuilder.Clear();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				FixCoeficient();

				_nextSummandSign = -1;

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == CloseBracketSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString(), CultureInfo.InvariantCulture);
				_coeficientBuilder.Clear();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				// calculate correct sign of summand
				FixCoeficient();

				summandDone = true;

				_stackOfSigns.Pop();

				_state = SimplifierState.CloseBracket;
			}
			else
			{
				throw new SyntaxException();
			}

			return summandDone;
		}

		private void ReverseSignInStack()
		{
			if (_stackOfSigns.Count > 1)
			{
				throw new SyntaxException();
			}
			_stackOfSigns.Pop();
			_stackOfSigns.Push(-1);

			_equalSymbolPassed = true;
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
				throw new SyntaxException();
			}
		}

		private bool PowerCoeficientStateHandle(string character)
		{
			var summandDone = false;

			if (string.IsNullOrEmpty(character))
			{
				_variable.Power = int.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_summand.AddVariable(ref _variable);

				summandDone = true;
				_finished = true;

				_state = SimplifierState.None;
			}
			else if (char.IsNumber(character, 0))
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
			else if (character == PlusSymbol)
			{
				_variable.Power = int.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_summand.AddVariable(ref _variable);

				_state = SimplifierState.None;

				summandDone = true;
			}
			else if (character == EqualSymbol)
			{
				ReverseSignInStack();

				_variable.Power = int.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_summand.AddVariable(ref _variable);

				_state = SimplifierState.None;

				summandDone = true;
			}
			else if (character == MinusSymbol)
			{
				_variable.Power = int.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_summand.AddVariable(ref _variable);

				_nextSummandSign = -1;

				_state = SimplifierState.None;

				summandDone = true;
			}
			else if (character == CloseBracketSymbol)
			{
				_variable.Power = int.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.CloseBracket;
			}
			else
			{
				throw new SyntaxException();
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
			else if (character == MinusSymbol)
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.MinusPower;
			}
			else
			{
				throw new SyntaxException();
			}
		}

		private bool VariableStateHandle(string character)
		{
			var summandDone = false;

			if (string.IsNullOrEmpty(character))
			{
				_summand.AddVariable(ref _variable);

				summandDone = true;
				_finished = true;

				_state = SimplifierState.None;
			}
			else if (char.IsLetter(character, 0))
			{
				_summand.AddVariable(ref _variable);
				_variable = new Variable(character, 1);
			}
			else if (character == PowerSymbol)
			{
				_state = SimplifierState.Power;
			}
			else if (character == PlusSymbol)
			{
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == EqualSymbol)
			{
				ReverseSignInStack();

				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == MinusSymbol)
			{
				_summand.AddVariable(ref _variable);

				_nextSummandSign = -1;

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == CloseBracketSymbol)
			{
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_stackOfSigns.Pop();

				_state = SimplifierState.CloseBracket;
			}
			else
			{
				throw new SyntaxException();
			}
			return summandDone;
		}

		private bool IntegerPartOfCoeficientStateHandle(string character)
		{
			var summandDone = false;

			if (string.IsNullOrEmpty(character))
			{
				if (!_equalSymbolPassed)
				{
					throw new SyntaxException();
				}

				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				FixCoeficient();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;
				_finished = true;

				_state = SimplifierState.None;
			}
			else if (character == DotSymbol)
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

				FixCoeficient();

				_variable = new Variable(character, 1);

				_state = SimplifierState.Variable;
			}
			else if (character == PlusSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				FixCoeficient();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == EqualSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				FixCoeficient();

				ReverseSignInStack();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == MinusSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				FixCoeficient();

				_nextSummandSign = -1;

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.None;
			}
			else if (character == CloseBracketSymbol)
			{
				_summand.Coeficient = double.Parse(_coeficientBuilder.ToString());
				_coeficientBuilder.Clear();

				FixCoeficient();

				_stackOfSigns.Pop();

				_variable = new Variable(string.Empty, 0);
				_summand.AddVariable(ref _variable);

				summandDone = true;

				_state = SimplifierState.CloseBracket;
			}
			else
			{
				throw new SyntaxException();
			}

			return summandDone;
		}

		private void OpenBrackerStateHandle(string character)
		{
			if (character == OpenBracketSymbol)
			{
				_stackOfSigns.Push(_currentSummandSign * _stackOfSigns.Peek());
				_currentSummandSign = 1;
				_state = SimplifierState.OpenBracket;
			}
			else if (char.IsLetter(character, 0))
			{
				_variable = new Variable(character, 1);
				FixCoeficient();
				_state = SimplifierState.Variable;
			}
			else if (char.IsNumber(character, 0))
			{
				_coeficientBuilder.Append(character);
				_state = SimplifierState.TheIntegerPartOfCoeficientOrZero;
			}
			else if (character == MinusSymbol)
			{
				_currentSummandSign = -1;
				_state = SimplifierState.Minus;
			}
			else
			{
				throw new SyntaxException();
			}
		}

		private bool CloseBracketStateHandle(string character)
		{
			var summandDone = false;

			if (string.IsNullOrEmpty(character))
			{
				if (!_equalSymbolPassed || _stackOfSigns.Count > 1)
				{
					throw new SyntaxException();
				}

				_summand.Coeficient = 0;

				summandDone = true;

				_finished = true;

				_state = SimplifierState.None;
			}
			else if (character == CloseBracketSymbol)
			{
				if (_stackOfSigns.Count == 1)
				{
					throw new SyntaxException();
				}

				_stackOfSigns.Pop();
				_state = SimplifierState.CloseBracket;
			}
			else if (character == PlusSymbol)
			{
				_state = SimplifierState.None;
			}
			else if (character == EqualSymbol)
			{
				ReverseSignInStack();

				_state = SimplifierState.None;
			}
			else
			if (character == MinusSymbol)
			{
				_currentSummandSign = -1;
				_state = SimplifierState.None;
			}
			else
			{
				throw new SyntaxException();
			}

			return summandDone;
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
				FixCoeficient();
				_state = SimplifierState.Variable;
			}
			else if (character == OpenBracketSymbol)
			{
				_stackOfSigns.Push(_currentSummandSign * _stackOfSigns.Peek());
				_currentSummandSign = 1;
				_state = SimplifierState.OpenBracket;
			}
			else
			{
				throw new SyntaxException();
			}
		}

		private void FixCoeficient()
		{
			_summand.Coeficient *= _currentSummandSign * _stackOfSigns.Peek();
			_currentSummandSign = 1;
		}
	}
}
