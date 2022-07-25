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

		public ItemCreatorMenu(Navigator navigator, List<Item> itemList, User currentUser)
		{
			this.navigator = navigator;
			this.itemList = itemList;
			this.currentUser = currentUser;
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
			double newItemWeight = GetWeight(out returnToMainMenu);
			if (returnToMainMenu)
				return;
			double newItemHeight = GetHeight(out returnToMainMenu);
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
			Console.WriteLine($"Item Name set to {itemNameResponse}.");
			return itemNameResponse;
		}

		private double GetWeight(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			return 0;
		}

		private double GetHeight(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			return 0;
		}

		private double GetTemperature(out bool returnToMainMenu, out bool newItemHasTemperature)
		{
			returnToMainMenu = false;
			newItemHasTemperature = true;
			return 0;
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
