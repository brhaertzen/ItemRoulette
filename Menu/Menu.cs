using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class Menu
	{
		protected Navigator nav;
		protected const string lineBreak = "================================================================================";
		protected const string quote = "\"";
		protected string returnToMainMenuOption = $"Type {quote}Escape{quote} to return to Main Menu.";
		protected string invalidResponse = $"Invalid Response. Please try again.";

		public Menu(Navigator navigator)
		{
			this.nav = navigator;
		}

		public Menu()
		{
		}

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

		protected void WriteColor(string message)
		{
			string[] msgArray = message.Split('[', ']');
			ConsoleColor color;
			foreach (var msg in msgArray)
			{
				if (msg.StartsWith("/"))
					Console.ResetColor();
				else if (msg.StartsWith("=") && Enum.TryParse(msg.Substring(1), out color))
				{
					Console.ForegroundColor = color;
				}					
				else
					Console.Write(msg);
			}
			Console.Write($"\n");
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
	}
}
