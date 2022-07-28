using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoulette
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
				$"Type {quote}Item{quote} to go into the Item Creator and earn Roulette Credits.");
			if (nav.CurrentUser.ItemsCreated > 0)
				Console.WriteLine($"Type {quote}View{quote} to view all of your created Items. You have {nav.CurrentUser.DisplayItemsCreated()}.");
			if (nav.CurrentUser.RouletteCredit > 0 && nav.ItemList.Count > 10)
				Console.WriteLine($"Type {quote}Roulette{quote} to go into the Item Roulette. You have {nav.CurrentUser.DisplayRouletteCredits()} to use.");
			else if (nav.CurrentUser.RouletteCredit > 0)
				Console.WriteLine($"There aren't enough Items created to use the Item Roulette. Create more Items to access the Item Roulette.");
			else
				Console.WriteLine($"You have {nav.CurrentUser.DisplayRouletteCredits()}. Earn more so you can use the Item Roulette.");			
			Console.WriteLine(
				$"Type {quote}Settings{quote} to go into your User Settings.\n" +
				$"Type {quote}Exit{quote} to Exit the Application.");				
			bool validMenu = false;
			while (!validMenu)
			{
				string menuResponse = Console.ReadLine().ToLower();
				switch (menuResponse)
				{
					case "user" when nav.UserList.Count > 1: return MenuState.UserSelect;
					case "new":	return MenuState.UserCreator;
					case "item": return MenuState.ItemCreator;
					case "view" when nav.CurrentUser.ItemsCreated > 0: return MenuState.ItemViewer;
					case "roulette" when nav.CurrentUser.RouletteCredit > 0: return MenuState.ItemRoulette;
					case "settings": return MenuState.UserSettings;
					case "exit": return MenuState.Exit;
					default: Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			return MenuState.MainMenu;
		}
	}
}
