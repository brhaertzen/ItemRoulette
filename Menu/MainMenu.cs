using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class MainMenu : Menu
	{
		private User currentUser;
		public MainMenu(User currentUser)
		{
			this.currentUser = currentUser;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"Welcome to the Main Menu!");
			Console.WriteLine(
				$"You are currently set to User {currentUser.Name}.\n" +
				$"\n" +
				$"Where would you like to go?\n" +
				$"Type {quote}User{quote} to go to User Select.\n" +
				$"Type {quote}New{quote} to create a New User.\n" +
				$"Type {quote}Item{quote} to go into the Item Creator and earn Evaluator Tokens.");
			if (currentUser.EvaluatorTokens == 1)			
				Console.WriteLine($"Type {quote}Roulette{quote} to go into the Item Roulette. You have {currentUser.EvaluatorTokens} Evaluator Token to use.");			
			else if (currentUser.EvaluatorTokens > 1)			
				Console.WriteLine($"Type {quote}Roulette{quote} to go into the Item Roulette. You have {currentUser.EvaluatorTokens} Evaluator Tokens to use.");			
			else			
				Console.WriteLine($"You have 0 Evaluator Tokens. Earn more so you can use the Item Roulette.");			
			Console.WriteLine(
				$"Type {quote}Settings{quote} to go into your User Settings.\n" +
				$"Type {quote}Exit{quote} to Exit the Application.");
				
			bool validMenu = false;
			while (!validMenu)
			{
				string menuResponse = Console.ReadLine().ToLower();
				if (menuResponse == "user")
					return MenuState.UserSelect;
				else if (menuResponse == "new")
					return MenuState.UserCreator;
				else if (menuResponse == "item")
					return MenuState.ItemCreator;
				else if (menuResponse == "roulette" && currentUser.EvaluatorTokens > 0)
					return MenuState.ItemRoulette;
				else if (menuResponse == "settings")
					return MenuState.UserSettings;
				else if (menuResponse == "exit")
					return MenuState.Exit;
				else
					Console.WriteLine("Invalid response. Please try again.");
			}
			return MenuState.MainMenu;
		}
	}
}
