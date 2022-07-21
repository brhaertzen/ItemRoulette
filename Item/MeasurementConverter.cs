using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public static class MeasurementConverter
	{
		public static float SaveValueHeight(MeasurementSystem measurementSystem, float meterOrFeet, float centimeterOrInch)
		{
			float saveValue;
			if (measurementSystem == MeasurementSystem.Imperial)
			{
				saveValue = FeetAndInchToFeet(meterOrFeet, centimeterOrInch);
			}
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
			{
				saveValue = PoundAndOunceToPound(kilogramOrPound, gramOrOunce);
			}
			else
			{
				saveValue = KilogramAndGramToKilogram(kilogramOrPound, gramOrOunce);
				saveValue = KilogramToPound(saveValue);
			}
			return saveValue;
		}

		public static float MeterToFeet(float meter)
		{
			float feet = meter * 3.281f;
			return feet;
		}

		public static float FeetToMeter(float feet)
		{
			float meter = feet / 3.281f;
			return meter;
		}

		public static float KilogramToPound(float kilogram)
		{
			float pound = kilogram * 2.205f;
			return pound;
		}

		public static float PoundToKilogram(float pound)
		{
			float kilogram = pound / 2.205f;
			return kilogram;
		}

		public static float FeetAndInchToFeet(float feet, float inch)
		{
			float newFeet = feet + (inch / 12f);
			return newFeet;
		}

		public static float MeterAndCentimeterToMeter(float meter, float centimeter)
		{
			float newMeter = meter + (centimeter / 100f);
			return newMeter;
		}

		public static float PoundAndOunceToPound(float pound, float ounce)
		{
			float newPound = pound + (ounce / 16f);
			return newPound;
		}

		public static float KilogramAndGramToKilogram(float kilogram, float gram)
		{
			float newKilogram = kilogram + (gram / 100f);
			return newKilogram;
		}

		public static string DisplayMetricLength(float meter)
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

		public static string DisplayImperialLength(float feet)
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

		public static string DisplayMetricWeight(float kilogram)
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

		public static string DisplayImperialWeight(float pound)
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
