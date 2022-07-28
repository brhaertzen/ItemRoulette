using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class UserCreatorMenu : Menu
	{
		private bool canGoToMainMenu;

		public UserCreatorMenu(Navigator navigator) : base(navigator)
		{
			this.canGoToMainMenu = nav.UserList.Count > 0;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in the User Creator.");
			string newName = GetUserName(out bool returnToMainMenu);
			if (returnToMainMenu)
				return MenuState.MainMenu;
			TemperatureScale newTemperatureScale = GetTemperatureScale(out returnToMainMenu);
			if (returnToMainMenu)
				return MenuState.MainMenu;
			MeasurementSystem newMeasurementSystem = GetMeasurementSystem(out returnToMainMenu);
			if (returnToMainMenu)
				return MenuState.MainMenu;
			ConsoleColor newTextColor = GetTextColor(out returnToMainMenu);
			if (returnToMainMenu)
				return MenuState.MainMenu;
			User newUser = new User(newName, newTemperatureScale, newMeasurementSystem, newTextColor, 0, 0);
			nav.UserList.Add(newUser);
			nav.SaveUserList();
			nav.CurrentUser = newUser;
			WriteColor(
				$"\n" +
				$"New User [={newTextColor}]{newName}[/] has been created!\n" +
				$"Temperature Scale Preference: {newTemperatureScale}.\n" +
				$"Measurement System Preference: {newMeasurementSystem}.\n" +
				$"Text Color Preference: [={newTextColor}]{newTextColor}[/].\n" +
				$"Press any key to return to Main Menu.");
			Console.ReadKey();			
			return MenuState.MainMenu;
		}

		private string GetUserName(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine($"Please enter a name for your new User.");
			if (canGoToMainMenu)
				Console.WriteLine(returnToMainMenuOption);
			bool validName = false;
			string nameResponse = "";
			while (!validName)
			{
				nameResponse = Console.ReadLine();
				string nameResponseLower = nameResponse.ToLower();
				if (nameResponseLower == "escape" && canGoToMainMenu)
				{
					returnToMainMenu = true;
					return null;
				}
				if (nav.UserList.Count == 0)
					break;
				foreach (var user in nav.UserList)
				{
					validName = true;
					if (user.Name.ToLower() == nameResponseLower)
					{
						validName = false;
						Console.WriteLine($"User Name has already been taken. Please try again.");
						break;
					}
				}
			}
			Console.WriteLine($"Username set to {nameResponse}.");
			return nameResponse;
		}

		private TemperatureScale GetTemperatureScale(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred Temperature Scale between {quote}Fahrenheit{quote}, {quote}Celsius{quote}, & {quote}Kelvin{quote}.");
			if (canGoToMainMenu)
				Console.WriteLine(returnToMainMenuOption);
			bool validTemperatureResponse = false;
			TemperatureScale temperatureScale = TemperatureScale.Fahrenheit;
			while (!validTemperatureResponse)
			{
				string temperatureResponse = Console.ReadLine().ToLower();
				switch (temperatureResponse)
				{
					case "escape" when canGoToMainMenu:
						returnToMainMenu = true;
						return TemperatureScale.Fahrenheit;
					case "fahrenheit":
						Console.WriteLine($"Temperature Scale Preference set to Fahrenheit.");
						temperatureScale = TemperatureScale.Fahrenheit;
						validTemperatureResponse = true; break;
					case "celsius":
						Console.WriteLine($"Temperature Scale Preference set to Celsius.");
						temperatureScale = TemperatureScale.Celsius;
						validTemperatureResponse = true; break;
					case "kelvin":
						Console.WriteLine($"Temperature Scale Preference set to Kelvin.");
						temperatureScale = TemperatureScale.Kelvin;
						validTemperatureResponse = true; break;
					default:
						Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			return temperatureScale;
		}

		private MeasurementSystem GetMeasurementSystem(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred System of Measurement between {quote}Imperial{quote} & {quote}Metric{quote}.");
			if (canGoToMainMenu)
				Console.WriteLine(returnToMainMenuOption);
			bool validMeasurementResponse = false;
			MeasurementSystem measurementSystem = MeasurementSystem.Imperial;
			while (!validMeasurementResponse)
			{
				string measurementResponse = Console.ReadLine().ToLower();
				switch (measurementResponse)
				{
					case "escape" when canGoToMainMenu:
						returnToMainMenu = true;
						return MeasurementSystem.Imperial;
					case "imperial":
						Console.WriteLine($"Measurement System Preference set to Imperial.");
						measurementSystem = MeasurementSystem.Imperial;
						validMeasurementResponse = true; break;
					case "metric":
						Console.WriteLine($"Measurement System Preference set to Metric.");
						measurementSystem = MeasurementSystem.Metric;
						validMeasurementResponse = true; break;
					default:
						Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			return measurementSystem;
		}

		private ConsoleColor GetTextColor(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred Text Color among these options:");
			WriteColor(
				$"{quote}[={ConsoleColor.Red}]Red[/]{quote}, " +
				$"{quote}[={ConsoleColor.Yellow}]Yellow[/]{quote}, " +
				$"{quote}[={ConsoleColor.Green}]Green[/]{quote}, " +
				$"{quote}[={ConsoleColor.Blue}]Blue[/]{quote}, " +
				$"{quote}[={ConsoleColor.Cyan}]Cyan[/]{quote}, " +
				$"{quote}[={ConsoleColor.Magenta}]Magenta[/]{quote}, " +
				$"{quote}[={ConsoleColor.Gray}]Gray[/]{quote}, " +
				$"& {quote}[={ConsoleColor.White}]White[/]{quote}.");
			if (canGoToMainMenu)
				Console.WriteLine(returnToMainMenuOption);
			bool validColorResponse = false;
			ConsoleColor textColor = ConsoleColor.White;
			while (!validColorResponse)
			{
				string colorResponse = Console.ReadLine().ToLower();
				switch (colorResponse)
				{
					case "escape" when canGoToMainMenu:	returnToMainMenu = true; return ConsoleColor.White;
					case "red":
						WriteColor($"Text Color Preference set to [={ConsoleColor.Red}]Red[/].");
						textColor = ConsoleColor.Red;
						validColorResponse = true; break;
					case "yellow":
						WriteColor($"Text Color Preference set to [={ConsoleColor.Yellow}]Yellow[/].");
						textColor = ConsoleColor.Yellow;
						validColorResponse = true; break;
					case "green":
						WriteColor($"Text Color Preference set to [={ConsoleColor.Green}]Green[/].");
						textColor = ConsoleColor.Green;
						validColorResponse = true; break;
					case "blue":
						WriteColor($"Text Color Preference set to [={ConsoleColor.Blue}]Blue[/].");
						textColor = ConsoleColor.Blue;
						validColorResponse = true; break;
					case "cyan":
						WriteColor($"Text Color Preference set to [={ConsoleColor.Cyan}]Cyan[/].");
						textColor = ConsoleColor.Cyan;
						validColorResponse = true; break;
					case "magenta":
						WriteColor($"Text Color Preference set to [={ConsoleColor.Magenta}]Magenta[/].");
						textColor = ConsoleColor.Magenta;
						validColorResponse = true; break;
					case "gray":
						WriteColor($"Text Color Preference set to [={ConsoleColor.Gray}]Gray[/].");
						textColor = ConsoleColor.Gray;
						validColorResponse = true; break;
					case "white":
						WriteColor($"Text Color Preference set to [={ConsoleColor.White}]White[/].");
						textColor = ConsoleColor.White;
						validColorResponse = true; break;
					default: Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			return textColor;
		}
	}
}
