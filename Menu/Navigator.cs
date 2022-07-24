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
		public User CurrentUser { get; private set; }
		public List<User> UserList { get; private set; } = new List<User>();
		public List<Item> ItemList { get; private set; } = new List<Item>();

		static bool canReturnToMainMenu = true;
		static bool canNotReturnToMainMenu = false;

		public void EnterNavigator()
		{			
			NavigateTo(MenuState.Start, true);
		}

		public void NavigateTo(MenuState nextMenuState, bool canGoToMainMenu)
		{
			switch (nextMenuState)
			{
				case MenuState.Start: Start(); break;
				case MenuState.MainMenu: EnterMainMenu(); break;
				case MenuState.ItemCreator: EnterItemCreator(); break;
				case MenuState.ItemRoulette: EnterItemRoulette(); break;
				case MenuState.UserSelect: EnterUserSelector(); break;
				case MenuState.UserCreator: EnterUserCreator(canGoToMainMenu); break;
				case MenuState.UserSettings: EnterUserSettings(); break;
				case MenuState.Exit: Environment.Exit(0); break;
			}			
		}

		public void UpdateUserAndList(List<User> userList, User user)
		{
			this.UserList = userList;
			this.CurrentUser = user;
		}

		private void Start()
		{
			MenuStateEnterText(
				$"Welcome to the Item Evaluator Application!\n" +
				$"Create Items to earn Evaluator Tokens that allow you to use the Item Roulette!");
			if (UserList.Count == 0)
			{
				Console.WriteLine($"There are no users created. Please create a User to continue.");
				NavigateTo(MenuState.UserCreator, canNotReturnToMainMenu);
				return;
			}
			Console.WriteLine($"Please select a User by typing their name from the following list:");
			Dictionary<string, User> nameDict = new Dictionary<string, User>();
			foreach (var user in UserList)
			{
				nameDict.Add(user.Name, user);
				Console.WriteLine($"{quote}{user.Name}{quote}");
			}				
			Console.WriteLine($"Or type {quote}New User{quote} to create a new user.");
			bool validUserOption = false;
			while (!validUserOption)
			{
				string userResponse = Console.ReadLine().ToLower();
				if (userResponse == "new user")
				{
					validUserOption = true;
					NavigateTo(MenuState.UserCreator, canReturnToMainMenu);
				}
				else if (nameDict.ContainsKey(userResponse))
				{
					nameDict.TryGetValue(userResponse, out User nextSelectedUser);
					CurrentUser = nextSelectedUser;
					Console.WriteLine($"User set to {CurrentUser.Name}");
					NavigateTo(MenuState.MainMenu, canReturnToMainMenu);
				}
				else
					Console.WriteLine($"Invalid Response. Please try again.");				
			}
		}

		private void EnterMainMenu()
		{
			MainMenu mainMenu = new MainMenu(CurrentUser);
			MenuState nextMenuState = mainMenu.Enter();
			NavigateTo(nextMenuState, canReturnToMainMenu);
		}

		private void EnterItemCreator()
		{			
			ItemCreatorMenu itemCreator = new ItemCreatorMenu();
			MenuState nextMenuState =  itemCreator.Enter();
			NavigateTo(nextMenuState, canReturnToMainMenu);
		}

		private void EnterUserCreator(bool canGoToMainMenu)
		{			
			UserCreatorMenu userCreator = new UserCreatorMenu(this, UserList, canGoToMainMenu);
			MenuState nextMenuState = userCreator.Enter();
			NavigateTo(nextMenuState, canReturnToMainMenu);
		}

		private void EnterUserSettings()
		{			
			UserSettingsMenu userSettingsMenu = new UserSettingsMenu(UserList, CurrentUser);
			MenuState nextMenuState = userSettingsMenu.Enter();
			NavigateTo(nextMenuState, canReturnToMainMenu);
		}		

		private void EnterUserSelector()
		{
			UserSelectMenu userSelectorMenu = new UserSelectMenu(this, UserList, CurrentUser);
			MenuState nextMenuState = userSelectorMenu.Enter();
			NavigateTo(nextMenuState, canReturnToMainMenu);
		}

		private void EnterItemRoulette()
		{
			ItemRouletteMenu itemRouletteMenu = new ItemRouletteMenu();
			MenuState nextMenuState = itemRouletteMenu.Enter();
			NavigateTo(nextMenuState, canReturnToMainMenu);
		}
	}
}
