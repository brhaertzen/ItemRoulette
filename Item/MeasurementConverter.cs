using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public static class MeasurementConverter
	{
		//Values are saved in database as imperial for all height and weight. Conversion needed to save and retrieve
		public static float SaveValueHeight(MeasurementSystem measurementSystem, float meterOrFeet, float centimeterOrInch)
		{
			float saveValue;
			if (measurementSystem == MeasurementSystem.Imperial)			
				saveValue = FeetAndInchToFeet(meterOrFeet, centimeterOrInch);			
			else
			{
				saveValue = MeterAndCentimeterToMeter(meterOrFeet, centimeterOrInch);
				saveValue = MeterToFeet(saveValue);
			}
			return saveValue;
		}

		public static float SaveValueWeight(MeasurementSystem measurementSystem, float kilogramOrPound, float gramOrOunce)
		{
			float saveValue;
			if (measurementSystem == MeasurementSystem.Imperial)			
				saveValue = PoundAndOunceToPound(kilogramOrPound, gramOrOunce);			
			else
			{
				saveValue = KilogramAndGramToKilogram(kilogramOrPound, gramOrOunce);
				saveValue = KilogramToPound(saveValue);
			}
			return saveValue;
		}

		public static string DisplayValueHeight(MeasurementSystem measurementSystem, float height)
		{
			float value;
			string displayValue;
			if (measurementSystem == MeasurementSystem.Imperial)
			{
				value = height;
				displayValue = DisplayImperialHeight(value);
			}
			else
			{
				value = FeetToMeter(height);
				displayValue = DisplayMetricHeight(value);
			}
			return displayValue;
		}

		public static string DisplayValueWeight(MeasurementSystem measurementSystem, float weight)
		{
			float value;
			string displayValue;
			if (measurementSystem == MeasurementSystem.Imperial)
			{
				value = weight;
				displayValue = DisplayImperialWeight(value);
			}
			else
			{
				value = PoundToKilogram(weight);
				displayValue = DisplayMetricWeight(value);
			}
			return displayValue;
		}

		private static float MeterToFeet(float meter)
		{
			float feet = meter * 3.281f;
			return feet;
		}

		private static float FeetToMeter(float feet)
		{
			float meter = feet / 3.281f;
			return meter;
		}

		private static float KilogramToPound(float kilogram)
		{
			float pound = kilogram * 2.205f;
			return pound;
		}

		private static float PoundToKilogram(float pound)
		{
			float kilogram = pound / 2.205f;
			return kilogram;
		}

		private static float FeetAndInchToFeet(float feet, float inch)
		{
			float newFeet = feet + (inch / 12f);
			return newFeet;
		}

		private static float MeterAndCentimeterToMeter(float meter, float centimeter)
		{
			float newMeter = meter + (centimeter / 100f);
			return newMeter;
		}

		private static float PoundAndOunceToPound(float pound, float ounce)
		{
			float newPound = pound + (ounce / 16f);
			return newPound;
		}

		private static float KilogramAndGramToKilogram(float kilogram, float gram)
		{
			float newKilogram = kilogram + (gram / 100f);
			return newKilogram;
		}

		private static string DisplayMetricHeight(float meter)
		{
			float newMeter = (float)Math.Floor(meter);
			float centimeter = meter % 100f;
			string returnString;
			if (newMeter == 0f)			
				returnString = $"{centimeter} cm";			
			else if (centimeter == 0f)			
				returnString = $"{newMeter} m";			
			else			
				returnString = $"{newMeter} m {centimeter} cm";			
			return returnString;
		}

		private static string DisplayImperialHeight(float feet)
		{
			float newFeet = (float)Math.Floor(feet);
			float inches = feet % 12f;
			string returnString;
			if (newFeet == 0f)
				returnString = $"{inches} in";
			else if (inches == 0f)
				returnString = $"{newFeet} ft";
			else
				returnString = $"{newFeet} ft {inches} in";
			return returnString;
		}

		private static string DisplayMetricWeight(float kilogram)
		{
			float newKilogram = (float)Math.Floor(kilogram);
			float gram = kilogram % 100f;
			string returnString;
			if (newKilogram == 0f)
				returnString = $"{gram} g";
			else if (gram == 0f)
				returnString = $"{newKilogram} kg";
			else
				returnString = $"{newKilogram} kg {gram} g";
			return returnString;
		}

		private static string DisplayImperialWeight(float pound)
		{
			float newPound = (float)Math.Floor(pound);
			float ounches = pound % 16f;
			string returnString;
			if (pound == 0f)
				returnString = $"{ounches} oz";
			else if (ounches == 0f)
				returnString = $"{newPound} lb";
			else
				returnString = $"{newPound} lb {ounches} oz";
			return returnString;
		}
	}
}
