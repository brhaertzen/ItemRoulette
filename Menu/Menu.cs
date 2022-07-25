using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class Menu
	{
		protected const string lineBreak = "================================================================================";
		protected const string quote = "\"";
		protected string returnToMainMenuOption = $"Type {quote}Escape{quote} to return to Main Menu.";
		public virtual MenuState Enter()
		{
			return MenuState.Start;
		}

		protected void MenuStateEnterText(string enterStatement)
		{			
			Console.WriteLine(
				$"\n" +
				$"{lineBreak}\n" +
				$"\n" +
				$"{enterStatement}\n");
		}		
	}
}
