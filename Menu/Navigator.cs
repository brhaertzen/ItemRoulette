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
		public List<User> userList { get; private set; } = new List<User>();
		public List<Item> itemList { get; private set; } = new List<Item>();

		public void EnterNavigator()
		{			
			NavigateTo(MenuState.Start);
		}

		public void NavigateTo(MenuState nextMenuState)
		{
			switch (nextMenuState)
			{
				case MenuState.Start: Start(); break;
				case MenuState.MainMenu: EnterMainMenu(); break;
				case MenuState.ItemCreator: EnterItemCreator(); break;
				case MenuState.UserCreator: EnterUserCreator(); break;
				case MenuState.Settings: EnterUserSettings(); break;
				case MenuState.Exit: Exit(); break;
			}			
		}

		public void UpdateUserAndList(List<User> userList, User user)
		{
			this.userList = userList;
			this.user = user;
		}

		private void Start()
		{
			MenuStateEnterText($"Welcome to the Item Evaluator Application!");
			if (userList.Count == 0)
			{
				Console.WriteLine($"There are no users created. Please create a User to continue.");
				NavigateTo(MenuState.UserCreator);
				return;
			}
			Console.WriteLine($"Please select a User by typing their name from the following list:");
			foreach (var user in userList)
				Console.WriteLine($"{user.Name}");
			Console.WriteLine($"Or type {quote}New User{quote} to create a new user.");
			bool validUserOption = false;
			while (!validUserOption)
			{
				string userOption = Console.ReadLine().ToLower();
			}
		}

		private void EnterMainMenu()
		{
			MainMenu mainMenu = new MainMenu();
			MenuState nextMenuState = mainMenu.Enter();
			NavigateTo(nextMenuState);
		}

		private void EnterItemCreator()
		{			
			ItemCreatorMenu itemCreator = new ItemCreatorMenu();
			MenuState nextMenuState =  itemCreator.Enter();
			NavigateTo(nextMenuState);
		}

		private void EnterUserCreator()
		{			
			UserCreatorMenu userCreator = new UserCreatorMenu(this, userList);
			MenuState nextMenuState = userCreator.Enter();
			NavigateTo(nextMenuState);
		}

		private void EnterUserSettings()
		{			
			SettingsMenu settingsMenu = new SettingsMenu();
			MenuState nextMenuState = settingsMenu.Enter();
			NavigateTo(nextMenuState);
		}		

		private void Exit()
		{

		}
	}
}
