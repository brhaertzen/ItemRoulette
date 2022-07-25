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
		public static Vector2 SaveValueHeight(MeasurementSystem measurementSystem, double largeValue, double smallValue)
		{
			Vector2 saveValue;
			if (measurementSystem == MeasurementSystem.Imperial)			
				saveValue = new Vector2(largeValue, smallValue);			
			else							
				saveValue = MetricToImperialHeight(largeValue, smallValue);			
			return saveValue;
		}

		public static Vector2 SaveValueWeight(MeasurementSystem measurementSystem, double largeValue, double smallValue)
		{
			Vector2 saveValue;
			if (measurementSystem == MeasurementSystem.Imperial)			
				saveValue = new Vector2(largeValue, smallValue);			
			else			
				saveValue = MetricToImperialWeight(largeValue, smallValue);			
			return saveValue;
		}

		public static string DisplayValueWeight(MeasurementSystem measurementSystem, Vector2 weight)
		{
			Vector2 value;
			string displayValue;
			if (measurementSystem == MeasurementSystem.Imperial)
			{
				value = weight;
				displayValue = DisplayImperialWeight(value);
			}
			else
			{
				value = ImperialToMetricWeight(weight.x, weight.y);
				displayValue = DisplayMetricWeight(value);
			}
			return displayValue;
		}

		public static string DisplayValueHeight(MeasurementSystem measurementSystem, Vector2 height)
		{
			Vector2 value;
			string displayValue;
			if (measurementSystem == MeasurementSystem.Imperial)
			{
				value = height;
				displayValue = DisplayImperialHeight(value);
			}
			else
			{
				value = ImperialToMetricHeight(height.x, height.y);
				displayValue = DisplayMetricHeight(value);
			}
			return displayValue;
		}

		public static string GetLargeHeightName(MeasurementSystem measurementSystem)
		{
			string name;
			if (measurementSystem == MeasurementSystem.Imperial)
				name = $"Feet";
			else
				name = $"Meters";
			return name;
		}

		public static string GetSmallHeightName(MeasurementSystem measurementSystem)
		{
			string name;
			if (measurementSystem == MeasurementSystem.Imperial)
				name = $"Inches";
			else
				name = $"Centimeters";
			return name;
		}

		public static string GetLargeWeightName(MeasurementSystem measurementSystem)
		{
			string name;
			if (measurementSystem == MeasurementSystem.Imperial)
				name = $"Pounds";
			else
				name = $"Kilograms";
			return name;
		}

		public static string GetSmallWeightName(MeasurementSystem measurementSystem)
		{
			string name;
			if (measurementSystem == MeasurementSystem.Imperial)
				name = $"Ounces";
			else
				name = $"Grams";
			return name;
		}				

		public static double MaximumSmallWeight(MeasurementSystem measurementSystem)
		{
			double weight;
			if (measurementSystem == MeasurementSystem.Imperial)
				weight = 15f;
			else
				weight = 999f;
			return weight;
		}

		public static double MaximumSmallHeight(MeasurementSystem measurementSystem)
		{
			double height;
			if (measurementSystem == MeasurementSystem.Imperial)
				height = 11f;
			else
				height = 99f;
			return height;
		}

		private static Vector2 MetricToImperialWeight(double kilogram, double gram)
		{
			double pound = KilogramToPound(kilogram);
			double ounce = GramToOunce(gram);
			return new Vector2(pound, ounce);
		}

		public static Vector2 ImperialToMetricWeight(double pound, double ounce)
		{
			double kilogram = PoundToKilogram(pound);
			double gram = OunceToGram(ounce);
			return new Vector2(kilogram, gram);
		}

		public static Vector2 MetricToImperialHeight(double meter, double centimeter)
		{
			double feet = MeterToFeet(meter);
			double inch = CentimeterToInch(centimeter);
			return new Vector2(feet, inch);
		}		

		public static Vector2 ImperialToMetricHeight(double feet, double inch)
		{
			double meter = FeetToMeter(feet);
			double centimeter = InchToCentimeter(inch);
			return new Vector2(meter, centimeter);
		}

		private static double MeterToFeet(double meter)
		{
			double feet = meter * 3.281f;
			return feet;
		}

		private static double FeetToMeter(double feet)
		{
			double meter = feet / 3.281f;
			return meter;
		}

		private static double CentimeterToInch(double centimeter)
		{
			double inch = centimeter * 2.54f;
			return inch;
		}

		private static double InchToCentimeter(double inch)
		{
			double centimeter = inch / 2.54f;
			return centimeter;
		}

		private static double KilogramToPound(double kilogram)
		{
			double pound = kilogram * 2.205f;
			return pound;
		}

		private static double PoundToKilogram(double pound)
		{
			double kilogram = pound / 2.205f;
			return kilogram;
		}

		private static double GramToOunce(double gram)
		{
			double ounce = gram / 28.3495;
			return ounce;
		}

		private static double OunceToGram(double ounce)
		{
			double gram = ounce * 28.3495;
			return gram;
		}	

		private static string DisplayMetricHeight(Vector2 height)
		{
			string returnString;
			if (height.x == 0f)			
				returnString = $"{height.y}cm";			
			else if (height.y == 0f)			
				returnString = $"{height.x}m";			
			else			
				returnString = $"{height.x}m {height.y}cm";			
			return returnString;
		}

		private static string DisplayImperialHeight(Vector2 height)
		{
			string returnString;
			if (height.x == 0f)
				returnString = $"{height.y}in";
			else if (height.y == 0f)
				returnString = $"{height.x}ft";
			else
				returnString = $"{height.x}ft {height.y}in";
			return returnString;
		}

		private static string DisplayMetricWeight(Vector2 weight)
		{
			string returnString;
			if (weight.x == 0f)
				returnString = $"{weight.y}g";
			else if (weight.y == 0f)
				returnString = $"{weight.x}kg";
			else
				returnString = $"{weight.x}kg {weight.y}g";
			return returnString;
		}

		private static string DisplayImperialWeight(Vector2 weight)
		{
			string returnString;
			if (weight.x == 0f)
				returnString = $"{weight.y} oz";
			else if (weight.y == 0f)
				returnString = $"{weight.x} lb";
			else
				returnString = $"{weight.x} lb {weight.y} oz";
			return returnString;
		}
	}
}
