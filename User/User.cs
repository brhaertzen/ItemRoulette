using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItemEvaluator
{
	public class User
	{
		public string Name { get; private set; }
		public TemperatureScale TemperatureScalePref { get; private set; }
		public MeasurementSystem MeasurementSystemPref { get; private set; }
		public ConsoleColor ColorPref { get; private set; }
		public int ItemsCreated { get; private set; }
		public int EvaluatorTokens { get; private set; }

		[JsonConstructor]
		public User(String name, TemperatureScale temperatureScalePref, MeasurementSystem measurementSystemPref, ConsoleColor colorPref, int itemsCreated, int evaluatorTokens)
		{
			Name = name;
			TemperatureScalePref = temperatureScalePref;
			MeasurementSystemPref = measurementSystemPref;
			ColorPref = colorPref;
			ItemsCreated = itemsCreated;
			EvaluatorTokens = evaluatorTokens;
		}

		public void AdjustUserName(string newName)
		{
			Name = newName;
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
			ColorPref = newTextColor;
		}

		public int IncreaseItemsCreatedCount()
		{
			ItemsCreated++;
			return ItemsCreated;
		}

		public int DecreaseItemsCreatedCount()
		{
			ItemsCreated--;
			return ItemsCreated;
		}

		public int GiveEvaluatorToken(int tokenIncrease)
		{
			EvaluatorTokens += tokenIncrease;
			return EvaluatorTokens;
		}

		public int UseEvaluatorTokens(int tokenDecrease)
		{
			EvaluatorTokens -= tokenDecrease;
			return EvaluatorTokens;
		}

		public string DisplayEvaluatorTokens()
		{
			switch (EvaluatorTokens)
			{
				case 0: return $"{EvaluatorTokens} Evaluator Tokens";
				case 1: return $"{EvaluatorTokens} Evaluator Token";
				default: return $"{EvaluatorTokens} Evaluator Tokens";
			}
		}

		public string DisplayItemsCreated()
		{
			switch (ItemsCreated)
			{
				case 0: return $"{ItemsCreated} Items created";
				case 1: return $"{ItemsCreated} Item created";
				default: return $"{ItemsCreated} Items created";
			}
		}
	}
}
