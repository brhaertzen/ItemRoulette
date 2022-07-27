using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItemEvaluator
{	
	public class Navigator : Menu
	{				
		public User CurrentUser { get; set; }
		public List<User> UserList { get; set; } = new List<User>();
		public List<Item> ItemList { get; set; } = new List<Item>();
		private string userListFilePath => Directory.GetCurrentDirectory() + @"\Data\UserList.json";
		private string itemListFilePath => Directory.GetCurrentDirectory() + @"\Data\ItemList.json";

		public Navigator()
		{
		}

		public void EnterNavigator()
		{
			LoadItemList();
			LoadUserList();
			Navigate();
		}

		public void SaveItemList()
		{
			var itemJson = JsonConvert.SerializeObject(ItemList, Formatting.Indented);
			File.WriteAllText(itemListFilePath, itemJson);
		}

		public void SaveUserList()
		{
			var userJson = JsonConvert.SerializeObject(UserList, Formatting.Indented);
			File.WriteAllText(userListFilePath, userJson);
		}

		private void Navigate()
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
				nameDict.Add(user.Name.ToLower(), user);
				WriteColor($"{quote}[={user.ColorPref}]{user.Name}[/]{quote}");
			}				
			Console.WriteLine($"Or type {quote}New{quote} to create a New User.");
			bool validUserOption = false;
			while (!validUserOption)
			{
				string userResponse = Console.ReadLine().ToLower();
				if (userResponse == "new")
				{
					validUserOption = true;
					return MenuState.UserCreator;
				}
				else if (nameDict.ContainsKey(userResponse))
				{
					validUserOption = true;
					nameDict.TryGetValue(userResponse, out User nextSelectedUser);
					CurrentUser = nextSelectedUser;
					WriteColor($"User set to [={CurrentUser.ColorPref}]{CurrentUser.Name}[/].");
					return MenuState.MainMenu;
				}
				else
					Console.WriteLine($"Invalid Response. Please try again.");				
			}
			return MenuState.MainMenu;
		}

		private MenuState EnterMainMenu()
		{
			MainMenu mainMenu = new MainMenu(this);
			MenuState nextMenuState = mainMenu.Enter();
			return nextMenuState;
		}

		private MenuState EnterItemCreator()
		{			
			ItemCreatorMenu itemCreator = new ItemCreatorMenu(this);
			MenuState nextMenuState =  itemCreator.Enter();
			return nextMenuState;
		}

		private MenuState EnterItemViewer()
		{
			ItemViewerMenu itemViewer = new ItemViewerMenu(this);
			MenuState nextMenuState = itemViewer.Enter();
			return nextMenuState;
		}

		private MenuState EnterItemRoulette()
		{
			ItemRouletteMenu itemRouletteMenu = new ItemRouletteMenu(this);
			MenuState nextMenuState = itemRouletteMenu.Enter();
			return nextMenuState;
		}

		private MenuState EnterUserSelector()
		{
			UserSelectMenu userSelectorMenu = new UserSelectMenu(this);
			MenuState nextMenuState = userSelectorMenu.Enter();
			return nextMenuState;
		}

		private MenuState EnterUserCreator()
		{			
			UserCreatorMenu userCreator = new UserCreatorMenu(this);
			MenuState nextMenuState = userCreator.Enter();
			return nextMenuState;
		}

		private MenuState EnterUserSettings()
		{			
			UserSettingsMenu userSettingsMenu = new UserSettingsMenu(this);
			MenuState nextMenuState = userSettingsMenu.Enter();
			return nextMenuState;
		}			

		private void LoadItemList()
		{
			ItemList = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(itemListFilePath));			
		}

		private void LoadUserList()
		{
			UserList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(userListFilePath));
		}		
	}
}
