using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class MainMenu : Menu
	{
		public MainMenu(Navigator navigator) : base(navigator)
		{
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in the Main Menu.");
			WriteColor(
				$"You are currently set to User [={nav.CurrentUser.ColorPref}]{nav.CurrentUser.Name}[/].\n" +
				$"\n" +
				$"Where would you like to go?");
			if (nav.UserList.Count > 1)
				Console.WriteLine($"Type {quote}User{quote} to go to User Select.");
			Console.WriteLine(
				$"Type {quote}New{quote} to create a New User.\n" +
				$"Type {quote}Item{quote} to go into the Item Creator and earn Evaluator Tokens.");
			if (nav.CurrentUser.ItemsCreated > 0)
				Console.WriteLine($"Type {quote}View{quote} to view all of your created Items. You have {nav.CurrentUser.DisplayItemsCreated()}.");
			if (nav.CurrentUser.EvaluatorTokens > 0)		
				Console.WriteLine($"Type {quote}Roulette{quote} to go into the Item Roulette. You have {nav.CurrentUser.DisplayEvaluatorTokens()} to use.");			
			else			
				Console.WriteLine($"You have {nav.CurrentUser.DisplayEvaluatorTokens()}. Earn more so you can use the Item Roulette.");			
			Console.WriteLine(
				$"Type {quote}Settings{quote} to go into your User Settings.\n" +
				$"Type {quote}Exit{quote} to Exit the Application.");				
			bool validMenu = false;
			while (!validMenu)
			{
				string menuResponse = Console.ReadLine().ToLower();
				if (menuResponse == "user" && nav.UserList.Count > 1)
					return MenuState.UserSelect;
				else if (menuResponse == "new")
					return MenuState.UserCreator;
				else if (menuResponse == "item")
					return MenuState.ItemCreator;
				else if (menuResponse == "view" && nav.CurrentUser.ItemsCreated > 0)
					return MenuState.ItemViewer;
				else if (menuResponse == "roulette" && nav.CurrentUser.EvaluatorTokens > 0)
					return MenuState.ItemRoulette;
				else if (menuResponse == "settings")
					return MenuState.UserSettings;
				else if (menuResponse == "exit")
					return MenuState.Exit;
				else
					Console.WriteLine($"{invalidResponse}");
			}
			return MenuState.MainMenu;
		}
	}
}
