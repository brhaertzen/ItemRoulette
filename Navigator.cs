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

		public string userListFilePath => ThisLocation() + @"\Data\UserList.json";
		public string itemListFilePath => ThisLocation() + @"\Data\ItemList.json";
		
		public User CurrentUser { get; private set; }
		public List<User> UserList { get; private set; } = new List<User>();
		public List<Item> ItemList { get; private set; } = new List<Item>();

		public void EnterNavigator()
		{
			LoadItemList();
			LoadUserList();
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
			SaveUserList();
		}		

		public void UpdateItemList(List<Item> itemList)
		{
			this.ItemList = itemList;
			SaveItemList();
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
			ItemCreatorMenu itemCreator = new ItemCreatorMenu(this, ItemList, UserList, CurrentUser);
			MenuState nextMenuState =  itemCreator.Enter();
			return nextMenuState;
		}

		private MenuState EnterItemViewer()
		{
			ItemViewerMenu itemViewer = new ItemViewerMenu(this, ItemList, UserList, CurrentUser);
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
			UserSettingsMenu userSettingsMenu = new UserSettingsMenu(this, UserList, CurrentUser);
			MenuState nextMenuState = userSettingsMenu.Enter();
			return nextMenuState;
		}		
		
		private void SaveItemList()
		{
			var itemJson = JsonConvert.SerializeObject(ItemList, Formatting.Indented);
			File.WriteAllText(itemListFilePath, itemJson);
			
		}

		private void SaveUserList()
		{
			var userJson = JsonConvert.SerializeObject(UserList, Formatting.Indented);
			File.WriteAllText(userListFilePath, userJson);
		}

		private void LoadItemList()
		{
			ItemList = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(itemListFilePath));			
		}

		private void LoadUserList()
		{
			UserList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(userListFilePath));
		}

		private string ThisLocation()
		{
			string location = Directory.GetCurrentDirectory();
			string location2 = Path.GetDirectoryName(location);
			string location3 = Path.GetDirectoryName(location2);
			string location4 = Path.GetDirectoryName(location3);
			//string location = System.AppDomain.CurrentDomain.BaseDirectory;
			//string location = System.Reflection.Assembly.GetEntryAssembly().Location;
			//string location = Path.GetDirectoryName(file);
			//string location = Environment.CurrentDirectory;
			//string location = AppDomain.CurrentDomain.BaseDirectory;
			return location4;
		}
	}
}
