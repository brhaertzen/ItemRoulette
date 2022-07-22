using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class Menu
	{
		protected static string lineBreak = "========================================";
		protected static string quote = "\"";
		public virtual MenuState Enter()
		{
			return MenuState.Start;
		}

		protected static void MenuStateEnterText(string enterStatement)
		{			
			Console.WriteLine(
				$"\n" +
				$"{lineBreak}\n" +
				$"\n" +
				$"{enterStatement}\n");
		}
	}
}
