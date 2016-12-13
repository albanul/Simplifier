using System.Collections.Generic;

namespace EquationSimplifier.Entities
{
	public struct Summand
	{
		public double Coeficient { get; set; }
		public List<Variable> Variables { get; set; }

		public Summand(double coeficient, List<Variable> variables)
		{
			Coeficient = coeficient;
			Variables = variables;
		}
	}
}
