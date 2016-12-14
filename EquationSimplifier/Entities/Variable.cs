namespace EquationSimplifier.Entities
{
	public struct Variable
	{
		public string Name { get; set; }
		public int Power { get; set; }

		public Variable(string name, int power)
		{
			Name = name;
			Power = power;
		}
	}
}
