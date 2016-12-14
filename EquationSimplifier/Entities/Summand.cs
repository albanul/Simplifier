using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationSimplifier.Entities
{
	public class Summand : IEquatable<Summand>
	{
		public double Coeficient { get; set; }
		private List<Variable> Variables { get; set; }

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
				Variables.Remove(new Variable(string.Empty, 0));

				var found = false;

				for (var i = 0; i < Variables.Count; i++)
				{
					var v = Variables[i];

					if (v.Name == variable.Name)
					{
						Variables[i] = new Variable(v.Name, v.Power + variable.Power);
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
			return Variables.All(v => other.Variables.Contains(v));
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
	}
}
