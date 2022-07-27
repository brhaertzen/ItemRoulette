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
				if (temperatureResponse == "escape" && canGoToMainMenu)
				{
					returnToMainMenu = true;
					return TemperatureScale.Fahrenheit;
				}
				else if (temperatureResponse == "fahrenheit")
				{
					Console.WriteLine($"Temperature Scale Preference set to Fahrenheit.");
					temperatureScale = TemperatureScale.Fahrenheit;
					validTemperatureResponse = true;
				}
				else if (temperatureResponse == "celsius")
				{
					Console.WriteLine($"Temperature Scale Preference set to Celsius.");
					temperatureScale = TemperatureScale.Celsius;
					validTemperatureResponse = true;
				}
				else if (temperatureResponse == "kelvin")
				{
					Console.WriteLine($"Temperature Scale Preference set to Kelvin.");
					temperatureScale = TemperatureScale.Kelvin;
					validTemperatureResponse = true;
				}
				else
					Console.WriteLine($"{invalidResponse}");
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
				if (measurementResponse == "escape" && canGoToMainMenu)
				{
					returnToMainMenu = true;
					return MeasurementSystem.Imperial;
				}
				else if (measurementResponse == "imperial")
				{
					Console.WriteLine($"Measurement System Preference set to Imperial.");
					measurementSystem = MeasurementSystem.Imperial;
					validMeasurementResponse = true;
				}
				else if (measurementResponse == "metric")
				{
					Console.WriteLine($"Measurement System Preference set to Metric.");
					measurementSystem = MeasurementSystem.Metric;
					validMeasurementResponse = true;
				}
				else
					Console.WriteLine($"{invalidResponse}");
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
				if (colorResponse == "escape" && canGoToMainMenu)
				{
					returnToMainMenu = true;
					return ConsoleColor.White;
				}
				else if (colorResponse == "red")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Red}]Red[/].");
					textColor = ConsoleColor.Red;
					validColorResponse = true;
				}
				else if (colorResponse == "yellow")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Yellow}]Yellow[/].");
					textColor = ConsoleColor.Yellow;
					validColorResponse = true;
				}
				else if (colorResponse == "green")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Green}]Green[/].");
					textColor = ConsoleColor.Green;
					validColorResponse = true;
				}
				else if (colorResponse == "blue")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Blue}]Blue[/].");
					textColor = ConsoleColor.Blue;
					validColorResponse = true;
				}
				else if (colorResponse == "cyan")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Cyan}]Cyan[/].");
					textColor = ConsoleColor.Cyan;
					validColorResponse = true;
				}
				else if (colorResponse == "magenta")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Magenta}]Magenta[/].");
					textColor = ConsoleColor.Magenta;
					validColorResponse = true;
				}
				else if (colorResponse == "gray")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Gray}]Gray[/].");
					textColor = ConsoleColor.Gray;
					validColorResponse = true;
				}
				else if (colorResponse == "white")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.White}]White[/].");
					textColor = ConsoleColor.White;
					validColorResponse = true;
				}
				else
					Console.WriteLine($"{invalidResponse}");
			}
			return textColor;
		}
	}
}
