using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class ItemRouletteMenu : Menu
	{
		private MeasurementSystem MSP;
		private TemperatureScale TSP;

		public ItemRouletteMenu(Navigator navigator) : base(navigator)
		{
			MSP = nav.CurrentUser.MeasurementSystemPref;
			TSP = nav.CurrentUser.TemperatureScalePref;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in the Item Roulette.");
			Console.WriteLine(
				$"Use Evaluator Tokens to play the Roulette! Each attempt will consume one token and show you two items and compare them.\n" +
				$"You have {nav.CurrentUser.DisplayEvaluatorTokens()} to use.\n");				
			while (nav.CurrentUser.EvaluatorTokens > 0)
			{
				Console.WriteLine(
					$"Type {quote}Spin{quote} to Play!\n" +
					$"{returnToMainMenuOption}");
				string response = Console.ReadLine().ToLower();
				if (response == "escape")
					return MenuState.MainMenu;
				else if (response == "spin")
				{
					Spin();
					Console.WriteLine(
						$"\n" +
						$"\n" +
						$"You now have {nav.CurrentUser.DisplayEvaluatorTokens()} remaining.");						
				}					
				else
					Console.WriteLine($"{invalidResponse}");
			}
			Console.WriteLine($"You have no more Evaluator Tokens. Press any key to return to Main Menu.");
			return MenuState.MainMenu;
		}

		private void Spin()
		{
			Console.WriteLine(
				$"\n" +
				$"Choose a category:\n" +
				$"Type {quote}Random{quote} to find 2 items and compare them by a random category.\n" +
				$"Type {quote}Name{quote} to find 2 items that share a first letter and compare them by a random category.\n" +
				$"Type {quote}Weight{quote} to find 2 items and compare them by Weight.\n" +
				$"Type {quote}Height{quote} to find 2 items and compare them by Height.\n" +
				$"Type {quote}Temperature{quote} to find 2 items and compare them by Temperature\n" +
				$"Type {quote}Tag{quote} to find 2 items and compare them by Tags\n" +
				$"Type {quote}Color{quote} to find 2 items that share a color and compare them by a random category\n" +
				$"{returnToMainMenuOption}");
			bool hasValidResponse = false;
			while (!hasValidResponse)
			{
				string response = Console.ReadLine().ToLower();
				if (response == "escape")
					return;
				else if (response == "random")
				{
					RandomSpin();
					return;
				}					
				else if (response == "name")
				{
					NameSpin();
					return;
				}												
				else if (response == "weight")
				{
					WeightSpin();
					return;
				}										
				else if (response == "height")
				{
					HeightSpin();
					return;
				}										
				else if (response == "temperature")
				{
					TemperatureSpin();
					return;
				}										
				else if (response == "tag")
				{
					TagSpin();
					return;
				}											
				else if (response == "color")
				{
					ColorSpin();
					return;
				}											
				else
					Console.WriteLine($"{invalidResponse}");
			}			
		}

		private void RandomSpin()
		{
			(Item item1, Item item2) = GetTwoRandomItems();
			Console.WriteLine(
				$"\n" +
				$"Item1: {item1.Name}\n" +
				$"Item2: {item2.Name}\n" +
				$"Press any key to continue.");
			Console.ReadKey();
		}

		private void NameSpin()
		{
			(Item item1, Item item2) = GetTwoItemsWithFirstLetter(out string chosenLetter);
			WriteColor(
				$"\n" +
				$"Your two items are [={item1.Color}]{item1.Name}[/] & [={item2.Color}]{item2.Name}[/]\n" +
				$"They both start with the letter {chosenLetter}!\n" +
				$"Press any key to continue.");
			Console.ReadKey();
		}

		private void WeightSpin()
		{
			(Item item1, Item item2) = GetTwoRandomItems();
			Console.WriteLine(
				$"Item1: {item1.Name}\n" +
				$"Item2: {item2.Name}");
			Console.ReadKey();
		}

		private void HeightSpin()
		{
			(Item item1, Item item2) = GetTwoRandomItems();
			Console.WriteLine(
				$"Item1: {item1.Name}\n" +
				$"Item2: {item2.Name}");
			Console.ReadKey();
		}

		private void TemperatureSpin()
		{
			(Item item1, Item item2) = GetTwoItemsWithTemperature();
			Console.WriteLine(
				$"Item1: {item1.Name}\n" +
				$"Item2: {item2.Name}");
			Console.ReadKey();
		}

		private void TagSpin()
		{
			(Item item1, Item item2) = GetTwoItemsWithTags();
			Console.WriteLine(
				$"Item1: {item1.Name}\n" +
				$"Item2: {item2.Name}");
			Console.ReadKey();
		}

		private void ColorSpin()
		{
			(Item item1, Item item2) = GetTwoItemsWithColor();
			Console.WriteLine(
				$"Item1: {item1.Name}\n" +
				$"Item2: {item2.Name}");
			Console.ReadKey();
		}

		private (Item item1, Item item2) GetTwoRandomItems()
		{
			Random random = new Random();
			int index1 = random.Next(nav.ItemList.Count);
			int index2 = index1;
			while (index2 == index1)
				index2 = random.Next(nav.ItemList.Count);
			Item item1 = nav.ItemList[index1];
			Item item2 = nav.ItemList[index2];
			return (item1, item2);
		}

		private (Item item1, Item item2) GetTwoItemsWithFirstLetter(out string chosenLetter)
		{
			List<string> allLetters = new List<string>();
			List<string> availableLetters = new List<string>();
			foreach (var item in nav.ItemList)
			{
				string letter = item.Name.Substring(0, 1);
				if (allLetters.Contains(letter) && !availableLetters.Contains(letter))
					availableLetters.Add(letter);
				allLetters.Add(letter);
			}
			Random random = new Random();
			int letterIndex = random.Next(availableLetters.Count);
			chosenLetter = availableLetters[letterIndex];
			List<Item> possibleItems = new List<Item>();
			foreach (var item in nav.ItemList)
			{
				if (item.Name.Substring(0, 1) == chosenLetter)
					possibleItems.Add(item);
			}
			int index1 = random.Next(possibleItems.Count);
			int index2 = index1;
			while (index2 == index1)
				index2 = random.Next(possibleItems.Count);
			Item item1 = possibleItems[index1];
			Item item2 = possibleItems[index2];
			return (item1, item2);
		}

		private (Item item1, Item item2) GetTwoItemsWithTemperature()
		{
			List<Item> itemsWithTemperature = new List<Item>();
			foreach (var item in nav.ItemList)			
				if (item.HasTemperature)
					itemsWithTemperature.Add(item);
			Random random = new Random();
			int index1 = random.Next(itemsWithTemperature.Count);
			int index2 = index1;
			while (index2 == index1)
				index2 = random.Next(itemsWithTemperature.Count);
			Item item1 = itemsWithTemperature[index1];
			Item item2 = itemsWithTemperature[index2];
			return (item1, item2);			
		}

		private (Item item1, Item item2) GetTwoItemsWithTags()
		{
			List<ItemTags> allTags = new List<ItemTags>();
			List<ItemTags> availableTags = new List<ItemTags>();
			foreach (var item in nav.ItemList)			
				foreach (var tag in item.ItemTags)
				{
					if (allTags.Contains(tag) && !availableTags.Contains(tag))
						availableTags.Add(tag);
					allTags.Add(tag);
				}			
			Random random = new Random();
			int tagIndex = random.Next(availableTags.Count);
			ItemTags chosenTag = availableTags[tagIndex];
			List<Item> possibleItems = new List<Item>();
			foreach (var item in nav.ItemList)			
				if (item.ItemTags.Contains(chosenTag))
					possibleItems.Add(item);			
			int index1 = random.Next(possibleItems.Count);
			int index2 = index1;
			while (index2 == index1)
				index2 = random.Next(possibleItems.Count);
			Item item1 = possibleItems[index1];
			Item item2 = possibleItems[index2];
			return (item1, item2);
		}

		private (Item item1, Item item2) GetTwoItemsWithColor()
		{
			List<ConsoleColor> allColors = new List<ConsoleColor>();
			List<ConsoleColor> availableColors = new List<ConsoleColor>();
			foreach (var item in nav.ItemList)
			{
				ConsoleColor color = item.Color;
				if (allColors.Contains(color) && !availableColors.Contains(color))
					availableColors.Add(color);
				allColors.Add(color);
			}
			Random random = new Random();
			int colorIndex = random.Next(availableColors.Count);
			ConsoleColor chosenColor = availableColors[colorIndex];
			List<Item> possibleItems = new List<Item>();
			foreach (var item in nav.ItemList)			
				if (item.Color == chosenColor)
					possibleItems.Add(item);
			int index1 = random.Next(possibleItems.Count);
			int index2 = index1;
			while (index2 == index1)
				index2 = random.Next(possibleItems.Count);
			Item item1 = possibleItems[index1];
			Item item2 = possibleItems[index2];
			return (item1, item2);
			
		}
	}
}
