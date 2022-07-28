using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class ItemViewerMenu : Menu
	{
		public ItemViewerMenu(Navigator navigator) : base(navigator)
		{
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in your Item Viewer.");
			Console.WriteLine(
				$"You have {nav.CurrentUser.DisplayItemsCreated()}. Type {quote}List{quote} to see all your items.\n" +
				returnToMainMenuOption);
			bool keepLookingAtItems = true;
			while (keepLookingAtItems)
			{
				string viewerResponse = Console.ReadLine().ToLower();
				switch (viewerResponse)
				{
					case "escape": return MenuState.MainMenu;
					case "list":
						ShowAllItems();
						return MenuState.MainMenu;
					default:
						Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			return MenuState.MainMenu;
		}

		public void ShowAllItems()
		{
			Console.WriteLine();
			Dictionary<string, Item> itemStringDict = new Dictionary<string, Item>();
			foreach (var item in nav.ItemList.Where(item => item.UserWhoCreated == nav.CurrentUser.Name))
			{
				WriteColor($"[={item.Color}]{item.Name}[/]");
				itemStringDict.Add(item.Name.ToLower(), item);
			}
			Console.WriteLine(
				$"Type the name of your item if you would like to see its properties.\n" +
				$"{returnToMainMenuOption}");
			bool validListResponse = false;
			while (!validListResponse)
			{
				string listResponse = Console.ReadLine().ToLower();
				switch (listResponse)
				{
					case "escape": return;
					default:
						if (itemStringDict.ContainsKey(listResponse))
						{
							itemStringDict.TryGetValue(listResponse, out Item itemRequest);
							WriteColor(
								$"\n" +
								$"Here are the properties of Item: [={itemRequest.Color}]{itemRequest.Name}[/].\n" +
								$"Name: {itemRequest.Name}\n" +
								$"User who Created: {itemRequest.UserWhoCreated}\n" +
								$"Weight: {MeasurementConverter.DisplayValueWeight(nav.CurrentUser.MeasurementSystemPref, itemRequest.Weight)}\n" +
								$"Height: {MeasurementConverter.DisplayValueHeight(nav.CurrentUser.MeasurementSystemPref, itemRequest.Height)}\n" +
								$"{TemperatureResponse(itemRequest)}\n" +
								$"Item Tags: {DisplayItemTags(itemRequest.ItemTags)}\n" +
								$"Color: [={itemRequest.Color}]{itemRequest.Color}[/]\n" +
								$"Type the name of another item if you would like to see its properties.\n" +
								$"{returnToMainMenuOption}");
						}
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
		}

		private string TemperatureResponse(Item item)
		{
			string response = "";
			if (item.HasTemperature)			
				response = $"Temperature: {TemperatureConverter.DisplayValueTemperature(nav.CurrentUser.TemperatureScalePref, item.Temperature)}";			
			else
				response = $"Item has no temperature data";
			return response;
		}
	}
}
