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
		public static float SaveValueTemperature(TemperatureScale temperatureScale, float temperature)
		{
			float saveTemp;
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
			float tempValue;
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

		private static float FahrenheitToCelsius(float fahrenheitTemp)
		{
			float celciusTemp = (5 / 9) * (fahrenheitTemp - 32f);
			return celciusTemp;
		}

		private static float FahrenheitToKelvin(float fahrenheitTemp)
		{
			float kelvinTemp = ((5 / 9) * (fahrenheitTemp - 32f)) + 273f;
			return kelvinTemp;
		}

		private static float CelsiusToFahrenheit(float celsiusTemp)
		{
			float fahrenheitTemp = ((9 / 5) * celsiusTemp) + 32f;
			return fahrenheitTemp;
		}

		private static float CelsiusToKelvin(float celsiusTemp)
		{
			float kelvinTemp = celsiusTemp + 273f;
			return kelvinTemp;
		}

		private static float KelvinToFahrenheit(float kelvinTemp)
		{
			float fahrenheitTemp = ((9 / 5) * (kelvinTemp - 273f)) + 32f;
			return fahrenheitTemp;
		}

		private static float KelvinToCelsius(float kelvinTemp)
		{
			float celsiusTemp = kelvinTemp - 273f;
			return celsiusTemp;
		}

		private static string DisplayFahrenheit(float fahrenheitTemp)
		{
			string displayString = $"{fahrenheitTemp}°F";
			return displayString;
		}

		private static string DisplayCelsius(float celsiusTemp)
		{
			string displayString = $"{celsiusTemp}°C";
			return displayString;
		}

		private static string DisplayKelvin(float kelvinTemp)
		{
			string displayString = $"{kelvinTemp}K";
			return displayString;
		}
	}
}
