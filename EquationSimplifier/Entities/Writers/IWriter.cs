using System.Collections.Generic;

namespace EquationSimplifier.Entities.Writers
{
	public interface IWriter
	{
		void Write(List<Summand> list);
	}
}
