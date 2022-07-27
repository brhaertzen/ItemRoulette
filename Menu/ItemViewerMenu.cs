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
			MenuStateEnterText($"You are now in your Item Viewer.");
			Console.WriteLine(
				$"You have {currentUser.StateItemsCreated()}. Type {quote}List{quote} to see all your items.\n" +
				returnToMainMenuOption);
			bool keepLookingAtItems = true;
			while (keepLookingAtItems)
			{
				string viewerResponse = Console.ReadLine().ToLower();
				if (viewerResponse == "escape")
					return MenuState.MainMenu;
				else if (viewerResponse == "list")
				{
					ShowAllItems();
					return MenuState.MainMenu;
				}
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
			return MenuState.MainMenu;
		}

		public void ShowAllItems()
		{
			Console.WriteLine();
			Dictionary<string, Item> itemStringDict = new Dictionary<string, Item>();
			foreach (var item in itemList)			
				if (item.UserWhoCreated.Name == currentUser.Name)
				{
					Console.WriteLine($"{item.Name}");
					itemStringDict.Add(item.Name.ToLower(), item);
				}
			Console.WriteLine(
				$"Type the name of your item if you would like to see its properties.\n" +
				$"{returnToMainMenuOption}");
			bool validListResponse = false;
			while (!validListResponse)
			{
				string listResponse = Console.ReadLine().ToLower();
				if (listResponse == "escape")
					return;
				else if (itemStringDict.ContainsKey(listResponse))
				{
					itemStringDict.TryGetValue(listResponse, out Item itemRequest);
					Console.WriteLine(
						$"\n" +
						$"Here are the properties of Item: {itemRequest.Name}.\n" +
						$"Name: {itemRequest.Name}\n" +
						$"User who Created: {itemRequest.UserWhoCreated.Name}\n" +
						$"Weight: {MeasurementConverter.DisplayValueWeight(currentUser.MeasurementSystemPref, itemRequest.Weight)}\n" +
						$"Height: {MeasurementConverter.DisplayValueHeight(currentUser.MeasurementSystemPref, itemRequest.Height)}\n" +
						$"{TemperatureResponse(itemRequest)}\n" +
						$"Item Tags: {DisplayItemTags(itemRequest.ItemTags)}\n" +
						$"Color Tags: {DisplayColorTags(itemRequest.ColorTags)}\n" +
						$"Type the name of another item if you would like to see its properties.\n" +
						$"{returnToMainMenuOption}");
				}
				else
					Console.WriteLine("$Invalid Response. Please try again.");				
			}
		}

		private string TemperatureResponse(Item item)
		{
			string response = "";
			if (item.HasTemperature)			
				response = $"Temperature: {TemperatureConverter.DisplayValueTemperature(currentUser.TemperatureScalePref, item.Temperature)}";			
			else
				response = $"Item has no temperature data";
			return response;
		}
	}
}
