using System;

namespace EquationSimplifier.Entities
{
	public struct Variable : IComparable<Variable>
	{
		public string Name { get; set; }
		public int Power { get; set; }

		public Variable(string name, int power)
		{
			Name = name;
			Power = power;
		}

		public int CompareTo(Variable other)
		{
			return other.Power.CompareTo(Power);
		}
	}
}
