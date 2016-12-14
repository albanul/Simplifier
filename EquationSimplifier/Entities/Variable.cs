namespace EquationSimplifier.Entities
{
	public struct Variable
	{
		private string _name;
		public int Power { get; set; }

		public Variable(string name, int power)
		{
			_name = name;
			Power = power;
		}
	}
}
