using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	static class TemperatureConverter
	{
		public static float FahrenheitToCelsius(float fahrenheitTemp)
		{
			float celciusTemp = (5 / 9) * (fahrenheitTemp - 32f);
			return celciusTemp;
		}

		public static float FahrenheitToKelvin(float fahrenheitTemp)
		{
			float kelvinTemp = ((5 / 9) * (fahrenheitTemp - 32f)) + 273f;
			return kelvinTemp;
		}

		public static float CelsiusToFahrenheit(float celsiusTemp)
		{
			float fahrenheitTemp = ((9 / 5) * celsiusTemp) + 32f;
			return fahrenheitTemp;
		}

		public static float CelsiusToKelvin(float celsiusTemp)
		{
			float kelvinTemp = celsiusTemp + 273f;
			return kelvinTemp;
		}

		public static float KelvinToFahrenheit(float kelvinTemp)
		{
			float fahrenheitTemp = ((9 / 5) * (kelvinTemp - 273f)) + 32f;
			return fahrenheitTemp;
		}

		public static float KelvinToCelsius(float kelvinTemp)
		{
			float celsiusTemp = kelvinTemp - 273f;
			return celsiusTemp;
		}
	}
}
