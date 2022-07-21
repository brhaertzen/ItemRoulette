using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItemEvaluator
{	
	public class Navigator : Menu
	{
		public User user { get; private set; }

		public override void Enter()
		{
			MenuStateEnterText($"Welcome to the Item Evaluator!");
			NavigateTo(MenuState.Start);
		}

		public void NavigateTo(MenuState currentMenuState)
		{

			switch (currentMenuState)
			{
				case MenuState.Start: Start(); break;
				case MenuState.ItemCreator: EnterItemCreator(); break;
				case MenuState.UserCreator: EnterUserCreator(); break;
				case MenuState.Settings: EnterUserSettings(); break;
			}			
		}

		private void Start()
		{			
			Console.WriteLine(
				$"Where would you like to go?\n" +
				$"Type 1 to go into the Item Creator.\n" +
				$"Type 2 to go into the User Creator.\n" +
				$"Type 3 to go into User Settings.\n");
			bool isInt = int.TryParse(Console.ReadLine(), out int intInput);
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
					case 3: EnterUserSettings(); break;
				}
			}			
		}

		private void EnterItemCreator()
		{			
			ItemCreatorMenu itemCreator = new ItemCreatorMenu();
			itemCreator.Enter();
		}

		private void EnterUserCreator()
		{			
			UserCreatorMenu userCreator = new UserCreatorMenu();
			userCreator.Enter();
		}

		private void EnterUserSettings()
		{			
			SettingsMenu settingsMenu = new SettingsMenu();
			settingsMenu.Enter();
		}		
	}
}
