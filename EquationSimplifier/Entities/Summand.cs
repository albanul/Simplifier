using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace EquationSimplifier.Entities
{
	public class Summand : IEquatable<Summand>, IComparable<Summand>
	{
		public double Coeficient { get; set; }
		public List<Variable> Variables { get; private set; }

		public bool IsConstant => Variables.Count == 1 && string.IsNullOrEmpty(Variables[0].Name);

		public Summand(double coeficient, List<Variable> variables)
		{
			Coeficient = coeficient;
			Variables = variables;
		}

		public void AddVariable(ref Variable variable)
		{
			if (variable.Power == 0)
			{
				variable.Name = string.Empty;

				// if there are no variables
				if (Variables.Count == 0)
				{
					Variables.Add(variable);
				}
			}
			else
			{
				// remove constant from variables
				Variables.Remove(new Variable(string.Empty, 0));

				var found = false;

				// try to find variable with the same name to sum the powers
				for (var i = 0; i < Variables.Count; i++)
				{
					var v = Variables[i];

					if (v.Name == variable.Name)
					{
						var power = v.Power + variable.Power;
						// if power == 0 then make variable a constant
						Variables[i] = new Variable(power == 0 ? string.Empty : v.Name, power);
						found = true;
						break;
					}
				}

				if (!found)
				{
					Variables.Add(variable);
				}
			}
		}

		public void MakeConstant(double coeficient)
		{
			Coeficient = coeficient;

			Variables = new List<Variable>
			{
				new Variable(string.Empty, 0)
			};
		}

		public bool EqualVariables(Summand other)
		{
			return Variables.Count == other.Variables.Count &&
					Variables.All(v => other.Variables.Contains(v));
		}

		public static bool operator ==(Summand leftSummand, Summand rightSummand)
		{
			return leftSummand?.Equals(rightSummand) ?? false;
		}

		public static bool operator !=(Summand leftSummand, Summand rightSummand)
		{
			return !(leftSummand?.Equals(rightSummand) ?? false);
		}

		public bool Equals(Summand other)
		{
			return !ReferenceEquals(null, other) &&
					Coeficient.Equals(other.Coeficient) &&
					Variables.Count == other.Variables.Count &&
					Variables.All(v => other.Variables.Contains(v));
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			var a = obj as Summand;
			return a != null && Equals(a);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Coeficient.GetHashCode() * 397) ^ (Variables?.GetHashCode() ?? 0);
			}
		}

		public int CompareTo(Summand other)
		{
			var thisMaxPower = Variables.Max(v => v.Power);
			var otherMaxPower = other.Variables.Max(v => v.Power);

			var compare = otherMaxPower.CompareTo(thisMaxPower);

			if (compare == 0)
			{
				compare = Variables.Count.CompareTo(Variables.Count);
			}

			if (compare == 0)
			{
				compare = Math.Abs(other.Coeficient).CompareTo(Math.Abs(Coeficient));
			}

			return compare;
		}
	}
}
