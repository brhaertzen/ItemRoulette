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

		public void EnterNavigator()
		{			
			Navigate();
		}

		public void Navigate()
		{
			MenuState nextMenuState = MenuState.Start;
			while (true)
			{
				switch (nextMenuState)
				{
					case MenuState.Start: nextMenuState = Start(); break;
					case MenuState.MainMenu: nextMenuState = EnterMainMenu(); break;
					case MenuState.ItemCreator: nextMenuState = EnterItemCreator(); break;
					case MenuState.ItemViewer: nextMenuState = EnterItemViewer(); break;
					case MenuState.ItemRoulette: nextMenuState = EnterItemRoulette(); break;
					case MenuState.UserSelect: nextMenuState = EnterUserSelector(); break;
					case MenuState.UserCreator: nextMenuState = EnterUserCreator(); break;
					case MenuState.UserSettings: nextMenuState = EnterUserSettings(); break;
					case MenuState.Exit: Environment.Exit(0); break;
				}
			}					
		}

		public void UpdateUserAndUserList(List<User> userList, User user)
		{
			this.UserList = userList;
			this.CurrentUser = user;
		}

		public void UpdateItemList(List<Item> itemList)
		{
			this.ItemList = itemList;
		}

		private MenuState Start()
		{
			MenuStateEnterText(
				$"Welcome to the Item Evaluator Application!\n" +
				$"Create Items to earn Evaluator Tokens that allow you to use the Item Roulette!");
			if (UserList.Count == 0)
			{
				Console.WriteLine($"There are no users created. Please create a User to continue.");
				return MenuState.UserCreator;
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
					return MenuState.UserCreator;
				}
				else if (nameDict.ContainsKey(userResponse))
				{
					validUserOption = true;
					nameDict.TryGetValue(userResponse, out User nextSelectedUser);
					CurrentUser = nextSelectedUser;
					Console.WriteLine($"User set to {CurrentUser.Name}");
					return MenuState.MainMenu;
				}
				else
					Console.WriteLine($"Invalid Response. Please try again.");				
			}
			return MenuState.MainMenu;
		}

		private MenuState EnterMainMenu()
		{
			MainMenu mainMenu = new MainMenu(UserList, CurrentUser);
			MenuState nextMenuState = mainMenu.Enter();
			return nextMenuState;
		}

		private MenuState EnterItemCreator()
		{			
			ItemCreatorMenu itemCreator = new ItemCreatorMenu(this, ItemList, CurrentUser);
			MenuState nextMenuState =  itemCreator.Enter();
			return nextMenuState;
		}

		private MenuState EnterItemViewer()
		{
			ItemViewerMenu itemViewer = new ItemViewerMenu();
			MenuState nextMenuState = itemViewer.Enter();
			return nextMenuState;
		}

		private MenuState EnterItemRoulette()
		{
			ItemRouletteMenu itemRouletteMenu = new ItemRouletteMenu();
			MenuState nextMenuState = itemRouletteMenu.Enter();
			return nextMenuState;
		}

		private MenuState EnterUserSelector()
		{
			UserSelectMenu userSelectorMenu = new UserSelectMenu(this, UserList, CurrentUser);
			MenuState nextMenuState = userSelectorMenu.Enter();
			return nextMenuState;
		}

		private MenuState EnterUserCreator()
		{			
			UserCreatorMenu userCreator = new UserCreatorMenu(this, UserList);
			MenuState nextMenuState = userCreator.Enter();
			return nextMenuState;
		}

		private MenuState EnterUserSettings()
		{			
			UserSettingsMenu userSettingsMenu = new UserSettingsMenu(UserList, CurrentUser);
			MenuState nextMenuState = userSettingsMenu.Enter();
			return nextMenuState;
		}				
	}
}
