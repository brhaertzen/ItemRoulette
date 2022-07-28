using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItemRoulette
{
	public class User
	{
		public string Name { get; private set; }
		public TemperatureScale TemperatureScalePref { get; private set; }
		public MeasurementSystem MeasurementSystemPref { get; private set; }
		public ConsoleColor ColorPref { get; private set; }
		public int ItemsCreated { get; private set; }
		public int RouletteCredit { get; private set; }

		[JsonConstructor]
		public User(String name, TemperatureScale temperatureScalePref, MeasurementSystem measurementSystemPref, ConsoleColor colorPref, int itemsCreated, int rouletteCredit)
		{
			Name = name;
			TemperatureScalePref = temperatureScalePref;
			MeasurementSystemPref = measurementSystemPref;
			ColorPref = colorPref;
			ItemsCreated = itemsCreated;
			RouletteCredit = rouletteCredit;
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

		public int GiveRouletteCredit(int creditIncrease)
		{
			RouletteCredit += creditIncrease;
			return RouletteCredit;
		}

		public int useRouletteCredit(int creditDecrease)
		{
			RouletteCredit -= creditDecrease;
			return RouletteCredit;
		}

		public string DisplayRouletteCredits()
		{
			switch (RouletteCredit)
			{
				case 0: return $"{RouletteCredit} Roulette Credits";
				case 1: return $"{RouletteCredit} Roulette Credit";
				default: return $"{RouletteCredit} Roulette Credits";
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
