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

				// add variable only if we haven't it already
				if (!Variables.Contains(variable))
				{
					Variables.Add(variable);
				}
			}
			else
			{
				Variables.Add(variable);
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
