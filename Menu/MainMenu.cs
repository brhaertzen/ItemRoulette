using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	class MainMenu : Menu
	{
		public override MenuState Enter()
		{
			MenuStateEnterText($"Welcome to the Main Menu!");
			Console.WriteLine(
				$"Where would you like to go?\n" +
				$"Type 1 to go into the Item Creator.\n" +
				$"Type 2 to go into the User Creator.\n" +
				$"Type 3 to go into User Settings.\n" +
				$"Type 4 to to Exit the Application.");
			int intInput;
			bool isInt = int.TryParse(Console.ReadLine(), out intInput);
			while (!isInt)
			{
				Console.WriteLine($"Invalid Input, please try again.");
				isInt = int.TryParse(Console.ReadLine(), out intInput);
			}
			switch (intInput)
			{
				case 1: return MenuState.ItemCreator;
				case 2: return MenuState.UserCreator;
				case 3: return MenuState.Settings;
				case 4: return MenuState.Exit;
			}
			return MenuState.MainMenu;
		}
	}
}
