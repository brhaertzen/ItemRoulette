using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class UserSettingsMenu : Menu
	{
		private List<User> userList;
		private User currentUser;
		
		public UserSettingsMenu(List<User> userList, User currentUser)
		{
			this.userList = userList;
			this.currentUser = currentUser;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in User Settings.");

			Console.WriteLine(
				$"Your current User Settings are:\n" +
				$"User Name: {currentUser.Name}\n" +
				$"Temperature Scale: {currentUser.UserSettings.TemperatureScalePref}\n" +
				$"Measurement System: {currentUser.UserSettings.MeasurementSystemPref}\n" +
				$"Text Color: {currentUser.UserSettings.TextColorPref}");
			bool validResponse = true;
			while (validResponse)
			{
				Console.WriteLine(
				$"\n" +
				$"Type {quote}Name{quote} to edit User Name.\n" +
				$"Type {quote}Temperature{quote} to edit Temperature Scale Preference.\n" +
				$"Type {quote}Measurement{quote} to edit Measurement System Preference.\n" +
				$"Type {quote}Text{quote} to edit Text Color Preference.\n" +
				$"Type {quote}Escape{quote} to return to Main Menu.");
				string optionResponse = Console.ReadLine().ToLower();
				if (optionResponse == "escape")				
					return MenuState.MainMenu;									
				else if (optionResponse == "name")				
					AdjustName();				
				else if (optionResponse == "temperature")				
					AdjustTemperatureScale();				
				else if (optionResponse == "measurement")				
					AdjustMeasurementSystem();				
				else if (optionResponse == "text")				
					AdjustTextColor();				
				else
					Console.WriteLine($"Invalid response. Please try again.\n");
			}
			

			return MenuState.MainMenu;
		}		

		private void AdjustName()
		{
			Console.WriteLine(
				$"\n" +
				$"Please enter a new User Name.");
			bool validName = false;
			string nameResponse = "";
			while (!validName)
			{
				nameResponse = Console.ReadLine();
				string nameResponseLower = nameResponse.ToLower();
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
			Console.WriteLine($"New User Name set to {nameResponse}.");
			currentUser.AdjustUserName(nameResponse);
		}

		private void AdjustTemperatureScale()
		{
			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred Temperature Scale between {quote}Fahrenheit{quote}, {quote}Celsius{quote}, & {quote}Kelvin{quote}.");
			bool validTemperatureResponse = false;
			TemperatureScale newTemperatureScale = TemperatureScale.Fahrenheit;
			while (!validTemperatureResponse)
			{
				string temperatureResponse = Console.ReadLine().ToLower();
				if (temperatureResponse == "fahrenheit")
				{
					Console.WriteLine($"Temperature Scale Preference set to Fahrenheit.");
					newTemperatureScale = TemperatureScale.Fahrenheit;
					validTemperatureResponse = true;
				}
				else if (temperatureResponse == "celsius")
				{
					Console.WriteLine($"Temperature Scale Preference set to Celsius.");
					newTemperatureScale = TemperatureScale.Celsius;
					validTemperatureResponse = true;
				}
				else if (temperatureResponse == "kelvin")
				{
					Console.WriteLine($"Temperature Scale Preference set to Kelvin.");
					newTemperatureScale = TemperatureScale.Kelvin;
					validTemperatureResponse = true;
				}
				else
					Console.WriteLine($"Invalid Temperature Scale. Please try again.");
			}
			currentUser.UserSettings.AdjustTemperatureScalePref(newTemperatureScale);
		}

		private void AdjustMeasurementSystem()
		{
			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred System of Measurement between {quote}Imperial{quote} & {quote}Metric{quote}.");
			bool validMeasurementResponse = false;
			MeasurementSystem newMeasurementSystem = MeasurementSystem.Imperial;
			while (!validMeasurementResponse)
			{
				string measurementResponse = Console.ReadLine().ToLower();
				if (measurementResponse == "imperial")
				{
					Console.WriteLine($"Measurement System Preference set to Imperial.");
					newMeasurementSystem = MeasurementSystem.Imperial;
					validMeasurementResponse = true;
				}
				else if (measurementResponse == "metric")
				{
					Console.WriteLine($"Measurement System Preference set to Metric.");
					newMeasurementSystem = MeasurementSystem.Metric;
					validMeasurementResponse = true;
				}
				else
					Console.WriteLine($"Invalid Measurement System. Please try again.");
			}
			currentUser.UserSettings.AdjustMeasurementSystemPref(newMeasurementSystem);
		}

		private void AdjustTextColor()
		{
			Console.WriteLine(
				$"\n" +
				$"Please enter your preferred Text Color among these options:\n" +
				$"{quote}Red{quote}, {quote}Yellow{quote}, {quote}Green{quote}, {quote}Blue{quote}, {quote}Cyan{quote}, {quote}Magenta{quote}, {quote}Gray{quote}, & {quote}White{quote}.");
			bool validColorResponse = false;
			ConsoleColor newTextColor = ConsoleColor.White;
			while (!validColorResponse)
			{
				string colorResponse = Console.ReadLine().ToLower();
				if (colorResponse == "red")
				{
					Console.WriteLine("Text Color Preference set to Red.");
					newTextColor = ConsoleColor.Red;
					validColorResponse = true;
				}
				else if (colorResponse == "yellow")
				{
					Console.WriteLine("Text Color Preference set to Yellow.");
					newTextColor = ConsoleColor.Yellow;
					validColorResponse = true;
				}
				else if (colorResponse == "green")
				{
					Console.WriteLine("Text Color Preference set to Green.");
					newTextColor = ConsoleColor.Green;
					validColorResponse = true;
				}
				else if (colorResponse == "blue")
				{
					Console.WriteLine("Text Color Preference set to Blue.");
					newTextColor = ConsoleColor.Blue;
					validColorResponse = true;
				}
				else if (colorResponse == "cyan")
				{
					Console.WriteLine("Text Color Preference set to Cyan.");
					newTextColor = ConsoleColor.Cyan;
					validColorResponse = true;
				}
				else if (colorResponse == "magenta")
				{
					Console.WriteLine("Text Color Preference set to Magenta.");
					newTextColor = ConsoleColor.Magenta;
					validColorResponse = true;
				}
				else if (colorResponse == "gray" || colorResponse == "grey")
				{
					Console.WriteLine("Text Color Preference set to Gray.");
					newTextColor = ConsoleColor.Gray;
					validColorResponse = true;
				}
				else if (colorResponse == "white")
				{
					Console.WriteLine("Text Color Preference set to White.");
					newTextColor = ConsoleColor.White;
					validColorResponse = true;
				}
				else
					Console.WriteLine($"Invalid Color. Please try again.");
			}
			currentUser.UserSettings.AdjustTextColorPref(newTextColor);
		}
	}
}
