using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public enum MenuState
	{
		Start,
		UserCreator,
		ItemCreator,
		Settings
	}
	
	public class Navigator : Menu
	{
		public override void Enter()
		{
			NavigateTo(MenuState.Start);
		}
		public void NavigateTo(MenuState currentMenuState)
		{
			switch (currentMenuState)
			{
				case MenuState.Start: Start(); break;
				case MenuState.ItemCreator: EnterItemCreator(); break;
				case MenuState.UserCreator: EnterUserCreator(); break;
				case MenuState.Settings: EnterSettings(); break;
			}			
		}

		private void Start()
		{
			MenuStateEnterText($"Welcome to the Item Evaluator!");
			Console.WriteLine($"Where would you like to go?");
			Console.WriteLine($"Type 1 to go into the Item Creator.");
			Console.WriteLine($"Type 2 to go into the User Creator.");
			Console.WriteLine($"Type 3 to go into Settings.");

			var input = Console.ReadLine();
			int intInput;
			bool isInt = int.TryParse(input, out intInput);

			if (!isInt)
			{
				Console.WriteLine();
				Console.WriteLine($"Invalid Input.");
				NavigateTo(MenuState.Start);
			}				
			else
			{
				switch (intInput)
				{
					case 1: EnterItemCreator(); break;
					case 2: EnterUserCreator(); break;
					case 3: EnterSettings(); break;
				}
			}			
		}

		private void EnterItemCreator()
		{			
			ItemCreator itemCreator = new ItemCreator();
			itemCreator.Enter();
		}

		private void EnterUserCreator()
		{			
			UserCreator userCreator = new UserCreator();
			userCreator.Enter();
		}

		private static void EnterSettings()
		{			
			Settings settingsMenu = new Settings();
			settingsMenu.Enter();
		}		
	}
}
