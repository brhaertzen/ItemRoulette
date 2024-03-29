﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoulette
{
	public class ItemCreatorMenu : Menu
	{
		private MeasurementSystem MSP;
		private TemperatureScale TSP;
		private string displayItemTags =
			$"{(ItemTags)1} {(ItemTags)2} {(ItemTags)3} {(ItemTags)4} {(ItemTags)5}\n" +
			$"{(ItemTags)6} {(ItemTags)7} {(ItemTags)8} {(ItemTags)9} {(ItemTags)10}\n" +
			$"{(ItemTags)11} {(ItemTags)12} {(ItemTags)13} {(ItemTags)14} {(ItemTags)15}\n" +
			$"{(ItemTags)16} {(ItemTags)17} {(ItemTags)18} {(ItemTags)19} {(ItemTags)20}\n" +
			$"{(ItemTags)21} {(ItemTags)22} {(ItemTags)23} {(ItemTags)24} {(ItemTags)25}\n" +
			$"{(ItemTags)26} {(ItemTags)27} {(ItemTags)28} {(ItemTags)29} {(ItemTags)30}\n" +
			$"{(ItemTags)31} {(ItemTags)32} {(ItemTags)33} {(ItemTags)34} {(ItemTags)35}\n" +
			$"{(ItemTags)36} {(ItemTags)37} {(ItemTags)38} {(ItemTags)39} {(ItemTags)40}\n" +
			$"{(ItemTags)41} {(ItemTags)42} {(ItemTags)43} {(ItemTags)44} {(ItemTags)45}";
		private string displayColors =
			$"[={ConsoleColor.Red}]{ConsoleColor.Red}[/] [={ConsoleColor.Yellow}]{ConsoleColor.Yellow}[/] [={ConsoleColor.Green}]{ConsoleColor.Green}[/] [={ConsoleColor.Blue}]{ConsoleColor.Blue}[/]\n" +
			$"[={ConsoleColor.Cyan}]{ConsoleColor.Cyan}[/] [={ConsoleColor.Magenta}]{ConsoleColor.Magenta}[/] [={ConsoleColor.Gray}]{ConsoleColor.Gray}[/] [={ConsoleColor.White}]{ConsoleColor.White}[/]";

		public ItemCreatorMenu(Navigator navigator) : base(navigator)
		{
			MSP = nav.CurrentUser.MeasurementSystemPref;
			TSP = nav.CurrentUser.TemperatureScalePref;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText(
				$"You are now in the Item Creator.\n" +
				$"Each Item created will give you 1 Roulette Credit.");
			bool keepCreatingItems = true;
			bool goToRoulette = false;
			while (keepCreatingItems && !goToRoulette)			
				CreateItem(out keepCreatingItems, out goToRoulette);											
			nav.SaveItemList();
			nav.SaveUserList();
			if (goToRoulette)
				return MenuState.ItemRoulette;
			return MenuState.MainMenu;
		}

		private void CreateItem(out bool keepCreatingItems, out bool goToRoulette)
		{
			keepCreatingItems = false;
			goToRoulette = false;
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
			ConsoleColor newColor = GetColor(out returnToMainMenu);
			if (returnToMainMenu)
				return;
			Item newItem = new Item(newItemName, nav.CurrentUser.Name, newItemWeight, newItemHeight, newItemHasTemperature, newItemTemperature, newItemTags, newColor);			
			nav.ItemList.Add(newItem);
			nav.CurrentUser.IncreaseItemsCreatedCount();
			nav.CurrentUser.GiveRouletteCredit(1);
			WriteColor(
				$"\n" +
				$"New Item [={newItem.Color}]{newItem.Name}[/] added to Item Evaluator.\n" +
				$"You have earned 1 Roulette Credit and now have {nav.CurrentUser.DisplayRouletteCredits()}.\n" +
				$"Type {quote}Item{quote} to create another Item.");
			if (nav.ItemList.Count > 10)
				Console.WriteLine($"Type {quote}Roulette{quote} to go to the Item Roulette!");
			Console.WriteLine($"{returnToMainMenuOption}");
			bool validResponse = false;
			while (!validResponse)
			{
				string keepCreatingResponse = Console.ReadLine().ToLower();
				switch (keepCreatingResponse)
				{
					case "escape": keepCreatingItems = false; return;
					case "item": Console.WriteLine(); keepCreatingItems = true; return;
					case "roulette" when nav.ItemList.Count > 10: keepCreatingItems = false; goToRoulette = true; return;
					default: Console.WriteLine($"{invalidResponse}"); break;
				}					
			}
		}

		private string GetName(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			List<string> itemNameList = (nav.ItemList.Select(item => item.Name.ToLower())).ToList();
			Console.WriteLine(
				$"What is the name of your Item?\n" +
				returnToMainMenuOption);
			bool validItemName = false;
			string itemNameResponse = "";
			while (!validItemName)
			{
				itemNameResponse = Console.ReadLine();
				string itemNameResponseLower = itemNameResponse.ToLower();
				switch (itemNameResponseLower)
				{
					case "escape":
						returnToMainMenu = true; return "";
					default:
						if (nav.ItemList.Count == 0)
							validItemName = true;
						else
						{
							foreach (var item in nav.ItemList)
							{
								validItemName = true;
								if (item.Name.ToLower() == itemNameResponseLower)
								{
									validItemName = false;
									Console.WriteLine($"Item Name has already been taken. Please try again."); break;
								}
							}
						} break;
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
				switch (largeWeightResponse)
				{
					case "escape":
						returnToMainMenu = true; return new Vector2(0, 0);
					default:
						if (int.TryParse(largeWeightResponse, out int result))						
							validLargeWeight = true;						
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			double largeWeight = Convert.ToDouble(largeWeightResponse);
			Console.WriteLine(
				$"Enter any remaining Weight in {MeasurementConverter.GetSmallWeightName(MSP)}. It will be rounded to 2 decimal places.\n" +
				$"Maximum value allowed: {MeasurementConverter.MaximumSmallWeight(MSP)}\n" +
				returnToMainMenuOption);
			bool validSmallWeight = false;
			string smallWeightResponse = "";
			double smallWeight = 0;
			while (!validSmallWeight)
			{
				smallWeightResponse = Console.ReadLine().ToLower();
				switch (smallWeightResponse)
				{
					case "escape":
						returnToMainMenu = true; return new Vector2(0, 0);
					default:
						if (DetermineIfValidSmallValue(smallWeightResponse, out smallWeight) && MeasurementConverter.MaximumSmallWeight(MSP) >= smallWeight)
						{
							if (largeWeight == 0 && smallWeight == 0)
								Console.WriteLine($"Invalid Reponse. Total Item Weight Cannot be 0. Please try again.");
							else
								validSmallWeight = true;
						}
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
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
				switch (largeHeightResponse)
				{
					case "escape":
						returnToMainMenu = true; return new Vector2(0, 0);
					default:
						if (int.TryParse(largeHeightResponse, out int result))
							validLargeHeight = true;
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			double largeHeight = Convert.ToDouble(largeHeightResponse);
			Console.WriteLine(
				$"Enter any remaining Height in {MeasurementConverter.GetSmallHeightName(MSP)}. It will be rounded to 2 decimal places.\n" +
				$"Maximum value allowed: {MeasurementConverter.MaximumSmallHeight(MSP)}\n" +
				returnToMainMenuOption);
			bool validSmallHeight = false;
			string smallHeightResponse = "";
			double smallHeight = 0;
			while (!validSmallHeight)
			{
				smallHeightResponse = Console.ReadLine().ToLower();
				switch (smallHeightResponse)
				{
					case "escape":
						returnToMainMenu = true; return new Vector2(0, 0);
					default:
						if (DetermineIfValidSmallValue(smallHeightResponse, out smallHeight) && MeasurementConverter.MaximumSmallHeight(MSP) >= smallHeight)
						{
							if (largeHeight == 0 && smallHeight == 0)
								Console.WriteLine($"Invalid Response. Total Item height Cannot be 0. Please try again.");
							else
								validSmallHeight = true;
						}
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
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
				switch (hasTemperatureResponse)
				{
					case "escape":
						returnToMainMenu = true; return 0;
					case "yes":
						hasValidTemperatureResponse = true;	break;
					case "no":
						Console.WriteLine();
						newItemHasTemperature = false; return 0;
					default:
						Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			Console.WriteLine(
				$"What Temperature is your item in {TemperatureConverter.GetTemperatureName(nav.CurrentUser.TemperatureScalePref)}? It will be rounded to 2 decimal places.\n" +
				returnToMainMenuOption);
			bool hasValidTemperature = false;
			string temperatureResponse = "";
			double newTemperature = 0;
			while (!hasValidTemperature)
			{
				temperatureResponse = Console.ReadLine().ToLower();
				switch (temperatureResponse)
				{
					case "escape":
						returnToMainMenu = true; return 0;
					default:
						if (DetermineIfValidSmallValue(temperatureResponse, out newTemperature))						
							hasValidTemperature = true;						
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			double saveValueTemperature = TemperatureConverter.SaveValueTemperature(TSP, newTemperature);
			Console.WriteLine($"Temperature set to {TemperatureConverter.DisplayValueTemperature(TSP, saveValueTemperature)}.\n");
			return saveValueTemperature;
		}

		private List<ItemTags> GetItemTags(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"Add some tags to your item! Here is the list of all available tags. Add as many as you can.\n" +
				$"{displayItemTags}\n" +
				returnToMainMenuOption);
			bool hasValidFirstItemTagResponse = false;
			string firstItemResponse = "";
			List<ItemTags> itemTagsList = new List<ItemTags>();
			Dictionary<string, ItemTags> itemTagStringsDict = new Dictionary<string, ItemTags>();
			for (int i = 1; i <= 45; i++)
			{
				ItemTags tempItemTag = (ItemTags)i;
				string stringItemTag = tempItemTag.ToString().ToLower();
				itemTagStringsDict.Add(stringItemTag, tempItemTag);
			}
			while (!hasValidFirstItemTagResponse)
			{
				firstItemResponse = Console.ReadLine().ToLower();
				switch (firstItemResponse)
				{
					case "escape":
						returnToMainMenu = true; return new List<ItemTags>();
					default:
						if (itemTagStringsDict.ContainsKey(firstItemResponse))
						{
							hasValidFirstItemTagResponse = true;
							itemTagStringsDict.TryGetValue(firstItemResponse, out ItemTags firstItemTag);
							itemTagsList.Add(firstItemTag);
							Console.WriteLine($"{firstItemTag} added to Item Tags.\n");
						}
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			Console.WriteLine(
					$"Your current Item Tags are: {StateItemTags(itemTagsList)}.\n" +
					$"Would you like to add more?\n" +
					$"Type {quote}Done{quote} anytime if you are done adding Item Tags.\n" +
					returnToMainMenuOption);
			bool hasValidMoreItemTagsResponse = false;
			string moreItemTagsResponse = "";
			while (!hasValidMoreItemTagsResponse)
			{				
				moreItemTagsResponse = Console.ReadLine().ToLower();
				switch (moreItemTagsResponse)
				{
					case "escape":
						returnToMainMenu = true; return new List<ItemTags>();
					case "done":
						hasValidMoreItemTagsResponse = true; break;
					default:
						if (itemTagStringsDict.ContainsKey(moreItemTagsResponse))
						{
							itemTagStringsDict.TryGetValue(moreItemTagsResponse, out ItemTags moreItemTag);
							if (itemTagsList.Contains(moreItemTag))
								Console.WriteLine($"{moreItemTag} already added to Item Tags. Please try again.");
							else
							{
								itemTagsList.Add(moreItemTag);
								Console.WriteLine($"{moreItemTag} added to Item Tags. Your current Item Tags are: {StateItemTags(itemTagsList)}.");
							}
						}
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			Console.WriteLine();
			return itemTagsList;
		}

		private ConsoleColor GetColor(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			WriteColor(
				$"What Color is your item? Here is the list of available colors: \n" +
				$"{displayColors}\n" +
				returnToMainMenuOption);
			bool hasValidColorResponse = false;
			ConsoleColor color = ConsoleColor.White;
			Dictionary<string, ConsoleColor> colorStringsDict = new Dictionary<string, ConsoleColor>();
			colorStringsDict.Add("red", ConsoleColor.Red);
			colorStringsDict.Add("yellow", ConsoleColor.Yellow);
			colorStringsDict.Add("green", ConsoleColor.Green);
			colorStringsDict.Add("blue", ConsoleColor.Blue);
			colorStringsDict.Add("cyan", ConsoleColor.Cyan);
			colorStringsDict.Add("magenta", ConsoleColor.Magenta);
			colorStringsDict.Add("gray", ConsoleColor.Gray);
			colorStringsDict.Add("white", ConsoleColor.White);
			while (!hasValidColorResponse)
			{
				string colorResponse = Console.ReadLine().ToLower();
				switch (colorResponse)
				{
					case "escape":
						returnToMainMenu = true; return color;
					default:
						if (colorStringsDict.ContainsKey(colorResponse))
						{
							hasValidColorResponse = true;
							colorStringsDict.TryGetValue(colorResponse, out color);
							WriteColor($"Item Color set to [={color}]{color}[/].");
						}
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}			
			return color;
		}		

		private bool DetermineIfValidSmallValue(string smallValue, out double newValue)
		{
			if (double.TryParse(smallValue, out newValue))
			{
				newValue = Math.Round(newValue, 2, MidpointRounding.AwayFromZero);		
				return true;
			}			
			return false;			
		}
	}
}
