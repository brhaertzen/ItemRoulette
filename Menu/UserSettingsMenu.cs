using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class UserSettingsMenu : Menu
	{		
		public UserSettingsMenu(Navigator navigator) : base(navigator)
		{
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in your User Settings.");
			WriteColor(
				$"Your current User Settings are:\n" +
				$"User Name: [={nav.CurrentUser.ColorPref}]{nav.CurrentUser.Name}[/]\n" +
				$"Temperature Scale: {nav.CurrentUser.TemperatureScalePref}\n" +
				$"Measurement System: {nav.CurrentUser.MeasurementSystemPref}\n" +
				$"Text Color: [={nav.CurrentUser.ColorPref}]{nav.CurrentUser.ColorPref}[/]");
			bool validResponse = true;
			while (validResponse)
			{
				Console.WriteLine(
				$"\n" +
				$"Type {quote}Name{quote} to edit User Name.\n" +
				$"Type {quote}Temperature{quote} to edit Temperature Scale Preference.\n" +
				$"Type {quote}Measurement{quote} to edit Measurement System Preference.\n" +
				$"Type {quote}Color{quote} to edit Text Color Preference.\n" +
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
				else if (optionResponse == "color")				
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
			WriteColor($"New User Name set to [={nav.CurrentUser.ColorPref}]{nameResponse}[/].");
			nav.CurrentUser.AdjustUserName(nameResponse);
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
			nav.CurrentUser.AdjustTemperatureScalePref(newTemperatureScale);
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
			nav.CurrentUser.AdjustMeasurementSystemPref(newMeasurementSystem);
		}

		private void AdjustTextColor()
		{
			WriteColor(
				$"\n" +
				$"Please enter your preferred Text Color among these options:\n" +
				$"{quote}[={ConsoleColor.Red}]Red[/]{quote}," +
				$"{quote}[={ConsoleColor.Yellow}]Yellow[/]{quote}," +
				$"{quote}[={ConsoleColor.Green}]Green[/]{quote}," +
				$"{quote}[={ConsoleColor.Blue}]Blue[/]{quote}," +
				$"{quote}[={ConsoleColor.Cyan}]Cyan[/]{quote}, " +
				$"{quote}[={ConsoleColor.Magenta}]Magenta[/]{quote}, " +
				$"{quote}[={ConsoleColor.Gray}]Gray[/]{quote}, & " +
				$"{quote}[={ConsoleColor.White}]White[/]{quote}.");
			bool validColorResponse = false;
			ConsoleColor newTextColor = ConsoleColor.White;
			while (!validColorResponse)
			{
				string colorResponse = Console.ReadLine().ToLower();
				if (colorResponse == "red")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Red}]Red[/].");
					newTextColor = ConsoleColor.Red;
					validColorResponse = true;
				}
				else if (colorResponse == "yellow")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Yellow}]Yellow[/].");
					newTextColor = ConsoleColor.Yellow;
					validColorResponse = true;
				}
				else if (colorResponse == "green")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Green}]Green[/].");
					newTextColor = ConsoleColor.Green;
					validColorResponse = true;
				}
				else if (colorResponse == "blue")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Blue}]Blue[/].");
					newTextColor = ConsoleColor.Blue;
					validColorResponse = true;
				}
				else if (colorResponse == "cyan")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Cyan}]Cyan[/].");
					newTextColor = ConsoleColor.Cyan;
					validColorResponse = true;
				}
				else if (colorResponse == "magenta")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Magenta}]Magenta[/].");
					newTextColor = ConsoleColor.Magenta;
					validColorResponse = true;
				}
				else if (colorResponse == "gray")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.Gray}]Gray[/].");
					newTextColor = ConsoleColor.Gray;
					validColorResponse = true;
				}
				else if (colorResponse == "white")
				{
					WriteColor($"Text Color Preference set to [={ConsoleColor.White}]White[/].");
					newTextColor = ConsoleColor.White;
					validColorResponse = true;
				}
				else
					Console.WriteLine($"Invalid Color. Please try again.");
			}
			nav.CurrentUser.AdjustTextColorPref(newTextColor);
		}
	}
}
