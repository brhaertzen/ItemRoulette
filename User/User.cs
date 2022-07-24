using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class User
	{
		public string Name { get; private set; }
		public TemperatureScale TemperatureScalePref { get; private set; }
		public MeasurementSystem MeasurementSystemPref { get; private set; }
		public ConsoleColor TextColorPref { get; private set; }
		public int ItemsCreated { get; private set; } = 0;
		public int EvaluatorTokens { get; private set; } = 0;

		public User(String name, TemperatureScale temperatureScalePref, MeasurementSystem measurementSystemPref, ConsoleColor textColorPref)
		{
			this.Name = name;
			this.TemperatureScalePref = temperatureScalePref;
			this.MeasurementSystemPref = measurementSystemPref;
			this.TextColorPref = textColorPref;
		}

		public void AdjustUserName(string newName)
		{
			this.Name = newName;
		}

		public void AdjustTemperatureScalePref(TemperatureScale newTemperatureScale)
		{
			TemperatureScalePref = newTemperatureScale;
		}

		public void AdjustMeasurementSystemPref(MeasurementSystem newMeasurementSystem)
		{
			MeasurementSystemPref = newMeasurementSystem;
		}

		public void AdjustTextColorPref(ConsoleColor newTextColor)
		{
			TextColorPref = newTextColor;
		}

		public int IncreaseItemsCreatedCount()
		{
			ItemsCreated++;
			return ItemsCreated;
		}

		public int GiveEvaluatorToken()
		{
			EvaluatorTokens++;
			return EvaluatorTokens;
		}

		public int UseEvaluatorTokens()
		{
			EvaluatorTokens--;
			return EvaluatorTokens;
		}
	}
}
