using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	static class TemperatureConverter
	{
		//Values are saved in database as fahrenheit for all temperatures. Conversion needed to save and retrieve
		public static double SaveValueTemperature(TemperatureScale temperatureScale, float temperature)
		{
			double saveTemp;
			if (temperatureScale == TemperatureScale.Fahrenheit)
				saveTemp = temperature;
			else if (temperatureScale == TemperatureScale.Celsius)
				saveTemp = CelsiusToFahrenheit(temperature);
			else
				saveTemp = KelvinToFahrenheit(temperature);			
			return saveTemp;
		}
				
		public static string DisplayValueTemperature(TemperatureScale temperatureScale, float temperature)
		{
			double tempValue;
			string displayValue;
			if (temperatureScale == TemperatureScale.Fahrenheit)
			{
				tempValue = temperature;
				displayValue = DisplayFahrenheit(tempValue);
			}									
			else if (temperatureScale == TemperatureScale.Celsius)
			{
				tempValue = FahrenheitToCelsius(temperature);
				displayValue = DisplayCelsius(tempValue);
			}									
			else
			{
				tempValue = FahrenheitToKelvin(temperature);
				displayValue = DisplayKelvin(tempValue);
			}								
			return displayValue;
		}

		private static double FahrenheitToCelsius(double fahrenheitTemp)
		{
			double celciusTemp = (5 / 9) * (fahrenheitTemp - 32f);
			return celciusTemp;
		}

		private static double FahrenheitToKelvin(double fahrenheitTemp)
		{
			double kelvinTemp = ((5 / 9) * (fahrenheitTemp - 32f)) + 273f;
			return kelvinTemp;
		}

		private static double CelsiusToFahrenheit(double celsiusTemp)
		{
			double fahrenheitTemp = ((9 / 5) * celsiusTemp) + 32f;
			return fahrenheitTemp;
		}

		private static double CelsiusToKelvin(double celsiusTemp)
		{
			double kelvinTemp = celsiusTemp + 273f;
			return kelvinTemp;
		}

		private static double KelvinToFahrenheit(double kelvinTemp)
		{
			double fahrenheitTemp = ((9 / 5) * (kelvinTemp - 273f)) + 32f;
			return fahrenheitTemp;
		}

		private static double KelvinToCelsius(double kelvinTemp)
		{
			double celsiusTemp = kelvinTemp - 273f;
			return celsiusTemp;
		}

		private static string DisplayFahrenheit(double fahrenheitTemp)
		{
			string displayString = $"{fahrenheitTemp}°F";
			return displayString;
		}

		private static string DisplayCelsius(double celsiusTemp)
		{
			string displayString = $"{celsiusTemp}°C";
			return displayString;
		}

		private static string DisplayKelvin(double kelvinTemp)
		{
			string displayString = $"{kelvinTemp}K";
			return displayString;
		}
	}
}
