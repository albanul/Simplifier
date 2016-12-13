using System;

namespace EquationSimplifier.Entities.Writers
{
	public class ConsoleWriter : IWriter
	{
		public void Write(object o)
		{
			Console.WriteLine(o);
		}
	}
}
