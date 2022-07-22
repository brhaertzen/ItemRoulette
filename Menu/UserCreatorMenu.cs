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
		private string returnToMainMenuOption = $"Type {quote}Escape{quote} to return to Main Menu.";
		public UserCreatorMenu(Navigator navigator, List<User> userList)
		{
			this.navigator = navigator;
			this.userList = userList;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in the User Creator.");

			Console.WriteLine(
				$"Please enter a name for your new User.\n" +
				$"{returnToMainMenuOption}");
			bool validName = false;
			string nameResponse = "";
			while (!validName)
			{
				nameResponse = Console.ReadLine();
				string nameResponseLower = nameResponse.ToLower();
				if (nameResponseLower == "escape")
					return MenuState.MainMenu;
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

			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred Temperature Scale between Fahrenheit, Celsius, & Kelvin.\n" +
				$"{returnToMainMenuOption}");
			bool validTemperatureResponse = false;
			TemperatureScale temperatureScale = TemperatureScale.Fahrenheit;
			while (!validTemperatureResponse)
			{
				string temperatureResponse = Console.ReadLine().ToLower();
				if (temperatureResponse == "escape")
					return MenuState.MainMenu;
				else if (temperatureResponse == "fahrenheit")
				{
					Console.WriteLine($"Temperature Scale set to Fahrenheit.");
					temperatureScale = TemperatureScale.Fahrenheit;
					validTemperatureResponse = true;
				}
				else if (temperatureResponse == "celsius")
				{
					Console.WriteLine($"Temperature Scale set to Celsius.");
					temperatureScale = TemperatureScale.Celsius;
					validTemperatureResponse = true;
				}
				else if (temperatureResponse == "kelvin")
				{
					Console.WriteLine($"Temperature Scale set to Kelvin.");
					temperatureScale = TemperatureScale.Kelvin;
					validTemperatureResponse = true;
				}
				else				
					Console.WriteLine($"Invalid Temperature Scale. Please try again.");				
			}

			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred System of Measurement between Imperial and Metric.\n" +
				$"{returnToMainMenuOption}");
			bool validMeasurementResponse = false;
			MeasurementSystem measurementSystem = MeasurementSystem.Imperial;
			while (!validMeasurementResponse)
			{
				string measurementResponse = Console.ReadLine().ToLower();
				if (measurementResponse == "escape")
					return MenuState.MainMenu;
				else if (measurementResponse == "imperial")
				{
					Console.WriteLine($"Measurement System set to Imperial.");
					measurementSystem = MeasurementSystem.Imperial;
					validMeasurementResponse = true;
				}
				else if (measurementResponse == "metric")
				{
					Console.WriteLine($"Measurement System set to Metric.");
					measurementSystem = MeasurementSystem.Metric;
					validMeasurementResponse = true;
				}
				else
					Console.WriteLine($"Invalid Measurement System. Please try again.");
			}

			Console.WriteLine(
				$"\n" +
				$"Please enter your Text Color among these options:\n" +
				$"Red, Yellow, Green, Blue, Cyan, Magenta, Gray, & White\n" +
				$"{returnToMainMenuOption}");
			bool validColorResponse = false;
			ConsoleColor textColor = ConsoleColor.White;
			while (!validColorResponse)
			{
				string colorResponse = Console.ReadLine().ToLower();
				if (colorResponse == "escape")
					return MenuState.MainMenu;
				else if (colorResponse == "red")
				{
					Console.WriteLine("Text Color set to Red.");
					textColor = ConsoleColor.Red;
					validColorResponse = true;
				}
				else if (colorResponse == "yellow")
				{
					Console.WriteLine("Text Color set to Yellow.");
					textColor = ConsoleColor.Yellow;
					validColorResponse = true;
				}
				else if (colorResponse == "green")
				{
					Console.WriteLine("Text Color set to Green.");
					textColor = ConsoleColor.Green;
					validColorResponse = true;
				}
				else if (colorResponse == "blue")
				{
					Console.WriteLine("Text Color set to Blue.");
					textColor = ConsoleColor.Blue;
					validColorResponse = true;
				}
				else if (colorResponse == "cyan")
				{
					Console.WriteLine("Text Color set to Cyan.");
					textColor = ConsoleColor.Cyan;
					validColorResponse = true;
				}
				else if (colorResponse == "magenta")
				{
					Console.WriteLine("Text Color set to Magenta.");
					textColor = ConsoleColor.Magenta;
					validColorResponse = true;
				}
				else if (colorResponse == "gray" || colorResponse == "grey")
				{
					Console.WriteLine("Text Color set to Gray.");
					textColor = ConsoleColor.Gray;
					validColorResponse = true;
				}
				else if (colorResponse == "white")
				{
					Console.WriteLine("Text Color set to White.");
					textColor = ConsoleColor.White;
					validColorResponse = true;
				}
				else
					Console.WriteLine($"Invalid Color. Please try again.");
			}

			Console.WriteLine(
				$"\n" +
				$"New User {nameResponse} has been created!\n" +
				$"Temperature Scale: {temperatureScale}.\n" +
				$"Measurement System: {measurementSystem}.\n" +
				$"Text Color: {textColor}.\n" +
				$"Press any button to return to Main Menu.");
			Console.ReadKey();
			UserSettings newSettings = new UserSettings(temperatureScale, measurementSystem, textColor);
			User newUser = new User(nameResponse, newSettings);
			userList.Add(newUser);
			navigator.UpdateUserAndList(userList, newUser);
			return MenuState.MainMenu;
		}
	}
}
