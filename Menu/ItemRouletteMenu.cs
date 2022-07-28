using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoulette
{
	public class ItemRouletteMenu : Menu
	{
		private MeasurementSystem MSP;
		private TemperatureScale TSP;
		private List<SpinCategory> allCategories = new List<SpinCategory>() { SpinCategory.FirstLetter, SpinCategory.Weight, SpinCategory.Height, SpinCategory.Temperature, SpinCategory.ItemTag, SpinCategory.Color };

		public ItemRouletteMenu(Navigator navigator) : base(navigator)
		{
			MSP = nav.CurrentUser.MeasurementSystemPref;
			TSP = nav.CurrentUser.TemperatureScalePref;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in the Item Roulette.");
			Console.WriteLine(
				$"Use Roulette Credits to play the Roulette!\n" +
				$"Each attempt will consume one credit and show you two items and compare them.\n" +
				$"You have {nav.CurrentUser.DisplayRouletteCredits()} to use.\n" +
				$"Type {quote}Spin{quote} to Play!\n" +
				$"{returnToMainMenuOption}");
			while (nav.CurrentUser.RouletteCredit > 0)
			{
				string response = Console.ReadLine().ToLower();
				switch (response)
				{
					case "escape": return MenuState.MainMenu;
					case "spin":
						{
							Spin(out bool returnToMainMenu);
							if (returnToMainMenu)
								return MenuState.MainMenu;
							Console.WriteLine(
								$"\n" +
								$"\n" +
								$"You now have {nav.CurrentUser.DisplayRouletteCredits()} remaining.\n" +
								$"Type {quote}Spin{quote} to Play!\n" +
								$"{returnToMainMenuOption}");
						} break;
					default: Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			Console.WriteLine($"You have no more Roulette Credits. Press any key to return to Main Menu.");
			return MenuState.MainMenu;
		}

		private void Spin(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"\n" +
				$"Choose a category:\n" +
				$"Type {quote}List{quote} to choose one of your created Items and Spin the Roulette.\n" +
				$"Type {quote}Random{quote} to find 2 random items and Spin the Roulette.\n" +
				$"{returnToMainMenuOption}");
			bool hasValidResponse = false;
			while (!hasValidResponse)
			{
				string response = Console.ReadLine().ToLower();
				switch (response)
				{
					case "escape": returnToMainMenu = true; return;
					case "list": SpinByChosenFirstItem(out returnToMainMenu); return;
					case "random": SpinByRandomItems(); return;
					default: Console.WriteLine($"{invalidResponse}"); break;
				}
			}
		}

		private void SpinByChosenFirstItem(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"\n" +
				$"Here are all of your created Items:");
			Dictionary<string, Item> itemStringDict = new Dictionary<string, Item>();
			foreach (var item in nav.ItemList.Where(item => item.UserWhoCreated == nav.CurrentUser.Name))
			{
				WriteColor($"[={item.Color}]{item.Name}[/]");
				itemStringDict.Add(item.Name.ToLower(), item);
			}
			Console.WriteLine(
				$"Type the name of your item you would like to Spin the Roulette with.\n" +
				$"{returnToMainMenuOption}");
			bool validListResponse = false;
			Item item1 = nav.ItemList[0];
			while (!validListResponse)
			{
				string listResponse = Console.ReadLine().ToLower();
				switch (listResponse)
				{
					case "escape": returnToMainMenu = true; return;
					default:
						if (itemStringDict.ContainsKey(listResponse))
						{
							validListResponse = true;
							itemStringDict.TryGetValue(listResponse, out item1);
						}
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			(Item item2, SpinCategory chosenCategory) = GetSecondItemAndSpinCategory(item1);
			DisplayItems(item1, item2, chosenCategory);			
		}		

		private void SpinByRandomItems()
		{
			Item item1 = GetRandomItem();
			(Item item2, SpinCategory chosenCategory) = GetSecondItemAndSpinCategory(item1);
			DisplayItems(item1, item2, chosenCategory);
		}

		private void DisplayItems(Item item1, Item item2, SpinCategory chosenCategory)
		{
			Console.WriteLine();
			switch (chosenCategory)
			{
				case SpinCategory.FirstLetter: DisplayFirstLetter(item1, item2); break;
				case SpinCategory.Weight: DisplayWeight(item1, item2); break;
				case SpinCategory.Height: DisplayHeight(item1, item2); break;
				case SpinCategory.Temperature: DisplayTemperature(item1, item2); break;
				case SpinCategory.ItemTag: DisplayItemTags(item1, item2); break;
				case SpinCategory.Color: DisplayColor(item1, item2); break;

			}			
			nav.CurrentUser.useRouletteCredit(1);
			nav.SaveUserList();
			Console.WriteLine($"Press any key to continue");
			Console.ReadKey();
		}

		private void DisplayFirstLetter(Item item1, Item item2)
		{
			string firstLetter = item1.Name.Substring(0, 1);
			WriteColor(
				$"Your two items are [={item1.Color}]{item1.Name}[/] and [={item2.Color}]{item2.Name}[/].\n" +
				$"The Category is First Letters!\n" +
				$"They both start with the letter {firstLetter}!\n" +
				$"{StateUsers(item1, item2)}");
			

		}

		private void DisplayWeight(Item item1, Item item2)
		{
			string compareStatement = $"";
			if (item1.Weight.x == 0 && item2.Weight.x == 0)			
				if (item1.Weight.y > item2.Weight.y)
					compareStatement = $"[={item1.Color}]{item1.Name}[/] weighs more than [={item2.Color}]{item2.Name}[/]!";
				else
					compareStatement = $"[={item2.Color}]{item2.Name}[/] weighs more than [={item1.Color}]{item1.Name}[/]!";			
			else
				if (item1.Weight.x > item2.Weight.x)
					compareStatement = $"[={item1.Color}]{item1.Name}[/] weighs more than [={item2.Color}]{item2.Name}[/]!";
				else
					compareStatement = $"[={item2.Color}]{item2.Name}[/] weighs more than [={item1.Color}]{item1.Name}[/]!";
			WriteColor(
				$"Your two items are [={item1.Color}]{item1.Name}[/] and [={item2.Color}]{item2.Name}[/].\n" +
				$"The Category is Weight!\n" +
				$"[={item1.Color}]{item1.Name}[/] weighs {MeasurementConverter.DisplayValueWeight(MSP, item1.Weight)} " +
				$"and [={item2.Color}]{item2.Name}[/] weighs {MeasurementConverter.DisplayValueWeight(MSP, item2.Weight)}.\n" +
				$"{compareStatement}\n" +
				$"{StateUsers(item1, item2)}");
		}

		private void DisplayHeight(Item item1, Item item2)
		{
			string compareStatement = $"";
			if (item1.Height.x == 0 && item2.Height.x == 0)
				if (item1.Weight.y > item2.Height.y)
					compareStatement = $"[={item1.Color}]{item1.Name}[/] has more height than [={item2.Color}]{item2.Name}[/]!";
				else
					compareStatement = $"[={item2.Color}]{item2.Name}[/] has more height than [={item1.Color}]{item1.Name}[/]!";
			else
				if (item1.Height.x > item2.Height.x)
				compareStatement = $"[={item1.Color}]{item1.Name}[/] has more height than [={item2.Color}]{item2.Name}[/]!";
			else
				compareStatement = $"[={item2.Color}]{item2.Name}[/] has more height than [={item1.Color}]{item1.Name}[/]!";
			WriteColor(
				$"Your two items are [={item1.Color}]{item1.Name}[/] and [={item2.Color}]{item2.Name}[/].\n" +
				$"The Category is Height!!\n" +
				$"[={item1.Color}]{item1.Name}[/] has a height of {MeasurementConverter.DisplayValueHeight(MSP, item1.Height)} " +
				$"and [={item2.Color}]{item2.Name}[/] has a height of {MeasurementConverter.DisplayValueHeight(MSP, item2.Height)}.\n" +
				$"{compareStatement}\n" +
				$"{StateUsers(item1, item2)}");
		}

		private void DisplayTemperature(Item item1, Item item2)
		{
			string compareStatement = $"";
			if (item1.Temperature > item2.Temperature)
				compareStatement = $"[={item1.Color}]{item1.Name}[/] has a higher Temperature than [={item2.Color}]{item2.Name}[/]!";
			else
				compareStatement = $"[={item2.Color}]{item2.Name}[/] has a higher Temperature than [={item1.Color}]{item1.Name}[/]!";
			WriteColor(
				$"Your two items are [={item1.Color}]{item1.Name}[/] and [={item2.Color}]{item2.Name}[/].\n" +
				$"The Category is Temperature!\n" +
				$"[={item1.Color}]{item1.Name}[/] is {TemperatureConverter.DisplayValueTemperature(TSP, item1.Temperature)} " +
				$"and [={item2.Color}]{item2.Name}[/] is {TemperatureConverter.DisplayValueTemperature(TSP, item2.Temperature)}.\n" +
				$"{compareStatement}\n" +
				$"{StateUsers(item1, item2)}");
		}

		private void DisplayItemTags(Item item1, Item item2)
		{
			List<ItemTags> sharedTags = (item1.ItemTags.Where(tag => item2.ItemTags.Contains(tag))).ToList();
			WriteColor(
				$"Your two items are [={item1.Color}]{item1.Name}[/] and [={item2.Color}]{item2.Name}[/].\n" +
				$"The Category is Item Tags!\n" +
				$"Both Items share the tags {StateItemTags(sharedTags)}\n" +
				$"{StateUsers(item1, item2)}");
		}

		private void DisplayColor(Item item1, Item item2)
		{
			WriteColor(
				$"Your two items are [={item1.Color}]{item1.Name}[/] and [={item2.Color}]{item2.Name}[/].\n" +
				$"The Category is Color!\n" +
				$"Both Items share the Color [={item1.Color}]{item1.Color}[/]!\n" +
				$"{StateUsers(item1, item2)}");
		}

		private Item GetRandomItem()
		{
			Random random = new Random();
			Item chosenItem = nav.ItemList[random.Next(nav.ItemList.Count)];
			return chosenItem;
		}

		private string StateUsers(Item item1, Item item2)
		{
			User user1 = nav.GetUserByString(item1.UserWhoCreated);
			User user2 = nav.GetUserByString(item2.UserWhoCreated);
			if (user1 == user2)
				return $"Both Items were made by [={user1.ColorPref}]{user1.Name}[/].";
			else
				return $"[={item1.Color}]{item1.Name}[/] was made by [={user1.ColorPref}]{user1.Name}[/] and [={item2.Color}]{item2.Name}[/] was made by [={user2.ColorPref}]{user2.Name}[/].";
		}

		private (Item, SpinCategory) GetSecondItemAndSpinCategory(Item item1)
		{
			List<SpinCategory> validCategories = new List<SpinCategory>(allCategories);
			foreach (var category in allCategories)
			{
				switch (category)
				{
					case SpinCategory.FirstLetter:
						if (!FirstLetterItemAvailable(item1))
							validCategories.Remove(SpinCategory.FirstLetter); break;
					case SpinCategory.Temperature:
						if (!TemperatureAvailable(item1))
							validCategories.Remove(SpinCategory.Temperature); break;
					case SpinCategory.ItemTag:
						if (!ItemTagsAvailable(item1))
							validCategories.Remove(SpinCategory.ItemTag); break;
					case SpinCategory.Color:
						if (!ColorAvailable(item1))
							validCategories.Remove(SpinCategory.Color); break;
				}
			}
			Random random = new Random();
			SpinCategory selectedCategory = validCategories[random.Next(validCategories.Count)];
			Item item2 = item1;
			switch (selectedCategory)
			{
				case SpinCategory.FirstLetter: item2 = GetItemByFirstLetter(item1); break;
				case SpinCategory.Weight: item2 = GetItemByWeight(item1); break;
				case SpinCategory.Height: item2 = GetItemByHeight(item1); break;
				case SpinCategory.Temperature: item2 = GetItemByTemperature(item1); break;
				case SpinCategory.ItemTag: item2 = GetItemByItemTags(item1); break;
				case SpinCategory.Color: item2 = GetItemByColor(item1); break;
			}
			return (item2, selectedCategory);
		}

		private bool FirstLetterItemAvailable(Item checkItem)
		{
			string firstLetter = checkItem.Name.Substring(0, 1);
			foreach (Item item in nav.ItemList)
				if (item != checkItem && item.Name.Substring(0, 1) == firstLetter)
					return true;
			return false;
		}

		private bool TemperatureAvailable(Item checkItem)
		{
			if (!checkItem.HasTemperature)
				return false;
			foreach (var item in nav.ItemList)			
				if (item != checkItem && item.HasTemperature)
					return true;			
			return false;
		}

		private bool ItemTagsAvailable(Item checkItem)
		{
			foreach (var item in nav.ItemList)			
				if (item != checkItem)				
					foreach (var tag in checkItem.ItemTags)					
						if (item.ItemTags.Contains(tag))
							return true;		
			return false;
		}

		private bool ColorAvailable(Item checkItem)
		{
			foreach (var item in nav.ItemList)			
				if (item != checkItem && item.Color == checkItem.Color)
					return true;			
			return false;
		}

		private Item GetItemByFirstLetter(Item checkItem)
		{
			string firstLetter = checkItem.Name.Substring(0, 1);
			List<Item> possibleItems = (nav.ItemList.Where(item => item != checkItem && item.Name.Substring(0, 1) == firstLetter)).ToList();
			Random random = new Random();
			Item chosenItem = possibleItems[random.Next(possibleItems.Count)];
			return chosenItem;			
		}

		private Item GetItemByWeight(Item checkItem)
		{
			Item chosenItem = checkItem;
			Random random = new Random();
			while (chosenItem == checkItem)
				chosenItem = nav.ItemList[random.Next(nav.ItemList.Count)];
			return chosenItem;
		}

		private Item GetItemByHeight(Item checkItem)
		{
			Item chosenItem = checkItem;
			Random random = new Random();
			while (chosenItem == checkItem)
				chosenItem = nav.ItemList[random.Next(nav.ItemList.Count)];
			return chosenItem;
		}

		private Item GetItemByTemperature(Item checkItem)
		{
			List<Item> possibleItems = new List<Item>();
			foreach (var item in nav.ItemList)			
				if (item != checkItem && item.HasTemperature)
					possibleItems.Add(item);
			Random random = new Random();
			Item chosenItem = possibleItems[random.Next(possibleItems.Count)];
			return chosenItem;			
		}

		private Item GetItemByItemTags(Item checkItem)
		{
			List<Item> possibleItems = new List<Item>();
			foreach (var item in nav.ItemList)
				if (item != checkItem)
					foreach (var tag in checkItem.ItemTags)
						if (item.ItemTags.Contains(tag) && !possibleItems.Contains(item))
							possibleItems.Add(item);
			Random random = new Random();
			Item chosenItem = possibleItems[random.Next(possibleItems.Count)];
			return chosenItem;
		}

		private Item GetItemByColor(Item checkItem)
		{
			List<Item> possibleItems = new List<Item>();
			foreach (var item in nav.ItemList)			
				if (item != checkItem && item.Color == checkItem.Color)
					possibleItems.Add(item);
			Random random = new Random();
			Item chosenItem = possibleItems[random.Next(possibleItems.Count)];
			return chosenItem;
		}				
	}
}
