using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class UserCreatorMenu : Menu
	{
		private List<User> userList = new List<User>();
		private Navigator navigator;
		private bool canGoToMainMenu;

		public UserCreatorMenu(Navigator navigator, List<User> userList)
		{
			this.navigator = navigator;
			this.userList = userList;
			this.canGoToMainMenu = userList.Count > 0;
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
			User newUser = new User(newName, newTemperatureScale, newMeasurementSystem, newTextColor);
			userList.Add(newUser);
			navigator.UpdateUserAndUserList(userList, newUser);
			Console.WriteLine(
				$"\n" +
				$"New User {newName} has been created!\n" +
				$"Temperature Scale Preference: {newTemperatureScale}.\n" +
				$"Measurement System Preference: {newMeasurementSystem}.\n" +
				$"Text Color Preference: {newTextColor}.\n" +
				$"Press any button to return to Main Menu.");
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
				if (userList.Count == 0)
					break;
				foreach (var user in userList)
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
					Console.WriteLine($"Invalid Temperature Scale. Please try again.");
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
					Console.WriteLine($"Invalid Measurement System. Please try again.");
			}
			return measurementSystem;
		}

		private ConsoleColor GetTextColor(out bool returnToMainMenu)
		{
			returnToMainMenu = false;
			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred Text Color among these options:\n" +
				$"{quote}Red{quote}, {quote}Yellow{quote}, {quote}Green{quote}, {quote}Blue{quote}, {quote}Cyan{quote}, {quote}Magenta{quote}, {quote}Gray{quote}, & {quote}White{quote}.");
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
					Console.WriteLine("Text Color Preference set to Red.");
					textColor = ConsoleColor.Red;
					validColorResponse = true;
				}
				else if (colorResponse == "yellow")
				{
					Console.WriteLine("Text Color Preference set to Yellow.");
					textColor = ConsoleColor.Yellow;
					validColorResponse = true;
				}
				else if (colorResponse == "green")
				{
					Console.WriteLine("Text Color Preference set to Green.");
					textColor = ConsoleColor.Green;
					validColorResponse = true;
				}
				else if (colorResponse == "blue")
				{
					Console.WriteLine("Text Color Preference set to Blue.");
					textColor = ConsoleColor.Blue;
					validColorResponse = true;
				}
				else if (colorResponse == "cyan")
				{
					Console.WriteLine("Text Color Preference set to Cyan.");
					textColor = ConsoleColor.Cyan;
					validColorResponse = true;
				}
				else if (colorResponse == "magenta")
				{
					Console.WriteLine("Text Color Preference set to Magenta.");
					textColor = ConsoleColor.Magenta;
					validColorResponse = true;
				}
				else if (colorResponse == "gray" || colorResponse == "grey")
				{
					Console.WriteLine("Text Color Preference set to Gray.");
					textColor = ConsoleColor.Gray;
					validColorResponse = true;
				}
				else if (colorResponse == "white")
				{
					Console.WriteLine("Text Color Preference set to White.");
					textColor = ConsoleColor.White;
					validColorResponse = true;
				}
				else
					Console.WriteLine($"Invalid Color. Please try again.");
			}
			return textColor;
		}
	}
}
