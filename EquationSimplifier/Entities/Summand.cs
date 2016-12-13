using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationSimplifier.Entities
{
	public struct Summand : IEquatable<Summand>
	{
		public double Coeficient { get; set; }
		public List<Variable> Variables { get; set; }

		public Summand(double coeficient, List<Variable> variables)
		{
			Coeficient = coeficient;
			Variables = variables;
		}

		public static bool operator ==(Summand leftSummand, Summand rightSummand)
		{
			return leftSummand.Equals(rightSummand);
		}

		public static bool operator !=(Summand leftSummand, Summand rightSummand)
		{
			return !leftSummand.Equals(rightSummand);
		}

		public bool Equals(Summand other)
		{
			return Coeficient.Equals(other.Coeficient) &&
					Variables.Count == other.Variables.Count &&
					Variables.All(v => other.Variables.Contains(v));
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Summand && Equals((Summand) obj);
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
