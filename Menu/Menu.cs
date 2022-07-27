using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class Menu
	{
		protected const string lineBreak = "================================================================================";
		protected const string quote = "\"";
		protected string returnToMainMenuOption = $"Type {quote}Escape{quote} to return to Main Menu.";
		public virtual MenuState Enter()
		{
			return MenuState.Start;
		}

		protected void MenuStateEnterText(string enterStatement)
		{			
			Console.WriteLine(
				$"\n" +
				$"{lineBreak}\n" +
				$"\n" +
				$"{enterStatement}\n");
		}

		protected void WriteColor(ConsoleColor color, string text)
		{
			Console.ForegroundColor = color;
			Console.Write(text);
			Console.ForegroundColor = ConsoleColor.White;
		}

		protected string DisplayItemTags(List<ItemTags> itemTagsList)
		{
			string displayTags = "";
			if (itemTagsList.Count == 1)
				displayTags = $"{itemTagsList[0]}";
			else if (itemTagsList.Count == 2)
			{
				displayTags = $"{itemTagsList[0]}";
				displayTags += $" & {itemTagsList[1]}";
			}
			else if (itemTagsList.Count > 2)
			{
				displayTags = $"{itemTagsList[0]}";
				for (int i = 1; i < itemTagsList.Count - 1; i++)
				{
					displayTags += $", {itemTagsList[i]}";
				}
				displayTags += $", & {itemTagsList[itemTagsList.Count - 1]}";
			}
			return displayTags;
		}

		protected string DisplayColorTags(List<ConsoleColor> colorTagsList)
		{
			string displayTags = "";
			if (colorTagsList.Count == 1)
				displayTags = $"{colorTagsList[0]}";
			else if (colorTagsList.Count == 2)
			{
				displayTags = $"{colorTagsList[0]}";
				displayTags += $" & {colorTagsList[1]}";
			}
			else if (colorTagsList.Count > 2)
			{
				displayTags = $"{colorTagsList[0]}";
				for (int i = 1; i < colorTagsList.Count - 1; i++)
				{
					displayTags += $", {colorTagsList[i]}";
				}
				displayTags += $", & {colorTagsList[colorTagsList.Count - 1]}";
			}
			return displayTags;
		}
	}
}
