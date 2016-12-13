namespace EquationSimplifier.Entities
{
	public struct Variable
	{
		private string _name;
		private int _power;

		public Variable(string name, int power)
		{
			_name = name;
			_power = power;
		}
	}
}
