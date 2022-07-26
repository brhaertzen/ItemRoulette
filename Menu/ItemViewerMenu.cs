using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class ItemViewerMenu : Menu
	{
		private Navigator navigator;
		private List<Item> itemList;
		private List<User> userList;
		private User currentUser;

		public ItemViewerMenu(Navigator navigator, List<Item> itemList, List<User> userList, User currentUser)
		{
			this.navigator = navigator;
			this.itemList = itemList;
			this.userList = userList;
			this.currentUser = currentUser;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in your Item Viewer.\n");
			LookOption();


			return MenuState.MainMenu;
		}

		public void LookOption()
		{
			Console.WriteLine(
				$"You have {currentUser.StateItemsCreated()}. Type {quote}List{quote} to see all your items." +
				returnToMainMenuOption);
			bool keepLookingAtItems = true;
			while (keepLookingAtItems)
			{
				string viewerResponse = Console.ReadLine().ToLower();
				if (viewerResponse == "escape")
					return;
				else if (viewerResponse == "list")				
					ShowAllItems();				
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
		}

		public void ShowAllItems()
		{
			Dictionary<string, Item> itemStringDict = new Dictionary<string, Item>();
			foreach (var item in itemList)			
				if (item.UserWhoCreated == currentUser)
				{
					Console.WriteLine($"{item.Name}");
					itemStringDict.Add(item.Name.ToString().ToLower(), item);
				}					
		}
	}
}
