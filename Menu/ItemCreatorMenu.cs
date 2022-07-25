using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class ItemCreatorMenu : Menu
	{
		private Navigator navigator;
		private List<Item> itemList;
		private User currentUser;
		private MeasurementSystem MSP;
		private TemperatureScale TSP;

		public ItemCreatorMenu(Navigator navigator, List<Item> itemList, User currentUser)
		{
			this.navigator = navigator;
			this.itemList = itemList;
			this.currentUser = currentUser;
			MSP = currentUser.MeasurementSystemPref;
			TSP = currentUser.TemperatureScalePref;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText(
				$"You are now in the Item Creator.\n" +
				$"Each Item created will give you 1 Evaluator Token.");
			bool keepCreatingItems = true;
			while (keepCreatingItems)
			{
				CreateItem(out keepCreatingItems);
			}			
			navigator.UpdateItemList(itemList);
			return MenuState.MainMenu;
		}

		private void CreateItem(out bool keepCreatingItems)
		{
			keepCreatingItems = false;
			string newItemName = GetName(out bool returnToMainMenu);
			if (returnToMainMenu)
				return;
			Vector2 newItemWeight = GetWeight(out returnToMainMenu);
			if (returnToMainMenu)
				return;
			Vector2 newItemHeight = GetHeight(out returnToMainMenu);
			if (returnToMainMenu)
				return;
			double newItemTemperature = GetTemperature(out returnToMainMenu, out bool newItemHasTemperature);
			if (returnToMainMenu)
				return;
			List<ItemTags> newItemTags = GetItemTags(out returnToMainMenu);
			if (returnToMainMenu)
				return;
			List<ConsoleColor> newColorTags = GetColorTags(out returnToMainMenu);
			if (returnToMainMenu)
				return;
			Item newItem;
			if (newItemHasTemperature)
				newItem = new Item(newItemName, currentUser, newItemWeight, newItemHeight, newItemTemperature, newItemTags, newColorTags);
			else
				newItem = new Item(newItemName, currentUser, newItemWeight, newItemHeight, newItemTags, newColorTags);
			itemList.Add(newItem);
			currentUser.IncreaseItemsCreatedCount();
			currentUser.GiveEvaluatorToken(1);
			Console.WriteLine(
				$"New Item {newItem.Name} added to Item Evaluator.\n" +
				$"You have earned 1 Evaluator Token and now have {currentUser.StateEvaluatorTokens()}.\n" +
				$"Type {quote}Item{quote} to create another Item.\n" +
				returnToMainMenuOption);
			bool validResponse = false;
			while (!validResponse)
			{
				string keepCreatingResponse = Console.ReadLine().ToLower();
				if (keepCreatingResponse == "escape")
				{
					validResponse = true;
					keepCreatingItems = false;
					return;
				}
				else if (keepCreatingResponse == "item")
				{
					validResponse = true;
					keepCreatingItems = true;
					return;
				}
				else
					Console.WriteLine("Invalid Response. Please try again.");					
			}
		}

		private string GetName(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			List<string> itemNameList = new List<string>();
			foreach (var item in this.itemList)
			{
				itemNameList.Add(item.Name.ToLower());
			}
			Console.WriteLine(
				$"What is the name of your Item?\n" +
				returnToMainMenuOption);
			bool validItemName = false;
			string itemNameResponse = "";
			while (!validItemName)
			{
				itemNameResponse = Console.ReadLine();
				string itemNameResponseLower = itemNameResponse.ToLower();
				if (itemNameResponseLower == "escape")
				{
					returnToMainMenu = true;
					return "";
				}
				else if (itemList.Count == 0)				
					validItemName = true;				
				else
				{
					foreach (var item in this.itemList)
					{
						validItemName = true;
						if (item.Name.ToLower() == itemNameResponseLower)
						{
							validItemName = false;
							Console.WriteLine($"Item Name has already been taken. Please try again.");
							break;
						}
					}
				}				
			}
			Console.WriteLine($"Item Name set to {itemNameResponse}.\n");
			return itemNameResponse;
		}

		private Vector2 GetWeight(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"What is the Weight of your Item?\n" +
				$"Enter the Weight in {MeasurementConverter.GetLargeWeightName(MSP)} of your item. Use only whole numbers.\n" +
				$"(We will ask for Weight in {MeasurementConverter.GetSmallWeightName(MSP)} next.)\n" +
				returnToMainMenuOption);
			bool validLargeWeight = false;
			string largeWeightResponse = "";
			while (!validLargeWeight)
			{
				largeWeightResponse = Console.ReadLine().ToLower();
				if (largeWeightResponse == "escape")
				{
					returnToMainMenu = true;
					return new Vector2(0, 0);
				}
				else if (int.TryParse(largeWeightResponse, out int result))
				{
					validLargeWeight = true;
				}
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
			double largeWeight = Convert.ToDouble(largeWeightResponse);
			Console.WriteLine(
				$"Enter any remaining Weight in {MeasurementConverter.GetSmallWeightName(MSP)}. Use only whole numbers.\n" +
				$"Maximum value allowed: {MeasurementConverter.MaximumSmallWeight(MSP)}\n" +
				returnToMainMenuOption);
			bool validSmallWeight = false;
			string smallWeightResponse = "";
			while (!validSmallWeight)
			{
				smallWeightResponse = Console.ReadLine().ToLower();
				if (smallWeightResponse == "escape")
				{
					returnToMainMenu = true;
					return new Vector2(0, 0);
				}
				else if (int.TryParse(smallWeightResponse, out int result) && MeasurementConverter.MaximumSmallWeight(MSP) > result)				
					validSmallWeight = true;				
				else if (largeWeight == 0 && result == 0)				
					Console.WriteLine($"Invalid Reponse. Item Weight Cannot be 0. Please try again.");				
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
			double smallWeight = Convert.ToDouble(smallWeightResponse);
			Vector2 saveValueWeight = MeasurementConverter.SaveValueWeight(MSP, largeWeight, smallWeight);
			Console.WriteLine($"Weight set to {MeasurementConverter.DisplayValueWeight(MSP, saveValueWeight)}.\n");
			return saveValueWeight;
		}

		private Vector2 GetHeight(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"What is the Height of your Item?\n" +
				$"Enter the Height in {MeasurementConverter.GetLargeHeightName(MSP)} of your item. Use only whole numbers.\n" +
				$"(We will ask for Height in {MeasurementConverter.GetSmallHeightName(MSP)} next.)\n" +
				returnToMainMenuOption);
			bool validLargeHeight = false;
			string largeHeightResponse = "";
			while (!validLargeHeight)
			{
				largeHeightResponse = Console.ReadLine().ToLower();
				if (largeHeightResponse == "escape")
				{
					returnToMainMenu = true;
					return new Vector2(0, 0);
				}
				else if (int.TryParse(largeHeightResponse, out int result))				
					validLargeHeight = true;				
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
			double largeHeight = Convert.ToDouble(largeHeightResponse);
			Console.WriteLine(
				$"Enter any remaining Height in {MeasurementConverter.GetSmallHeightName(MSP)}. Use only whole numbers.\n" +
				$"Maximum value allowed: {MeasurementConverter.MaximumSmallHeight(MSP)}\n" +
				returnToMainMenuOption);
			bool validSmallHeight = false;
			string smallHeightResponse = "";
			while (!validSmallHeight)
			{
				smallHeightResponse = Console.ReadLine().ToLower();
				if (smallHeightResponse == "escape")
				{
					returnToMainMenu = true;
					return new Vector2(0, 0);
				}
				else if (int.TryParse(smallHeightResponse, out int result) && MeasurementConverter.MaximumSmallHeight(MSP) > result)
				{
					validSmallHeight = true;
				}
				else if (largeHeight == 0 && result == 0)				
					Console.WriteLine($"Invalid Response. Item Height cannot be 0. Please try again.");				
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
			double smallHeight = Convert.ToDouble(smallHeightResponse);
			Vector2 saveValueHeight = MeasurementConverter.SaveValueHeight(MSP, largeHeight, smallHeight);
			Console.WriteLine($"Height set to {MeasurementConverter.DisplayValueHeight(MSP, saveValueHeight)}.\n");
			return saveValueHeight;
		}

		private double GetTemperature(out bool returnToMainMenu, out bool newItemHasTemperature)
		{
			returnToMainMenu = false;
			newItemHasTemperature = true;
			Console.WriteLine(
				$"Does your item have a temperature?\n" +
				$"Type {quote}Yes{quote} or {quote}No{quote}.\n" +
				returnToMainMenuOption);
			bool hasValidTemperatureResponse = false;
			while (!hasValidTemperatureResponse)
			{
				string hasTemperatureResponse = Console.ReadLine().ToLower();
				if (hasTemperatureResponse == "escape")
				{
					returnToMainMenu = true;
					return 0;
				}
				else if (hasTemperatureResponse == "yes")
				{
					hasValidTemperatureResponse = true;
				}
				else if (hasTemperatureResponse == "no")
				{
					newItemHasTemperature = false;
					return 0;
				}
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
			Console.WriteLine(
				$"What Temperature is your item? Use only whole numbers.\n" +
				returnToMainMenuOption);
			bool hasValidTemperature = false;
			string temperatureResponse = "";
			while (!hasValidTemperature)
			{
				temperatureResponse = Console.ReadLine().ToLower();
				if (temperatureResponse == "escape")
				{
					returnToMainMenu = true;
					return 0;
				}
				else if (int.TryParse(temperatureResponse, out int result))
				{
					hasValidTemperature = true;
				}
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
			double newTemperature = Convert.ToDouble(temperatureResponse);
			double saveValueTemperature = TemperatureConverter.SaveValueTemperature(TSP, newTemperature);
			Console.WriteLine($"Temperature set to {TemperatureConverter.DisplayValueTemperature(TSP, saveValueTemperature)}.\n");
			return saveValueTemperature;
		}

		private List<ItemTags> GetItemTags(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			return new List<ItemTags>();
		}

		private List<ConsoleColor> GetColorTags(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			return new List<ConsoleColor>();
		}
	}
}
