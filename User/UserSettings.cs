using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class UserSettings
	{
		public TemperatureScale TemperatureScalePref { get; private set; }
		public MeasurementSystem MeasurementSystemPref { get; private set; }
		public ConsoleColor TextColorPref { get; private set; }

		public UserSettings(TemperatureScale temperatureScalePref, MeasurementSystem measurementSystemPref, ConsoleColor textColorPref)
		{
			this.TemperatureScalePref = temperatureScalePref;
			this.MeasurementSystemPref = measurementSystemPref;
			this.TextColorPref = textColorPref;
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
	}
}
