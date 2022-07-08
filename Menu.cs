using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class Menu
	{
		protected static string lineBreak = $"========================================";
		public virtual void Enter()
		{
		}

		protected static void MenuStateEnterText(string enterStatement)
		{
			Console.WriteLine($"{lineBreak}");
			Console.WriteLine();
			Console.WriteLine($"{enterStatement}");
			Console.WriteLine();
		}
	}
}
