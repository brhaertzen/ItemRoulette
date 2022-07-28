using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoulette
{
	public enum MenuState
	{
		Start,
		MainMenu,
		UserSelect,
		UserCreator,
		ItemCreator,
		ItemViewer,
		ItemRoulette,
		UserSettings,
		Exit
	}

	public enum TemperatureScale
	{
		Fahrenheit,
		Celsius,
		Kelvin
	}

	public enum MeasurementSystem
	{
		Metric,
		Imperial
	}

	public enum ItemTags
	{
		Soft,
		Hard,
		Furry,
		Slimy,
		Shiny,
		Dull,
		Loud,
		Quiet,
		Flat,
		Spherical,
		Triangular,
		Cylindrical,
		Rectagonal,
		Wet,
		Sticky,
		Dry,
		Poisonous,
		Edible,
		Inedible,
		Sweet,
		Spicy,
		Salty,
		Alive,
		Imaginary,
		Inanimate,
		Bipedal,
		Quadrupedal,
		Fruit,
		Plant,
		Fungus,
		Insect,
		Mammal,
		Aquatic,
		Terrestrial,
		Happy,
		Nice,
		Angry,
		Funny,
		Sad,
		Intelligent,
		Dumb,
		Fragile,
		Cheap,
		Expensive,
		Young,
		Old
	}	

	public enum SpinCategory
	{
		FirstLetter,
		Weight,
		Height,
		Temperature,
		ItemTag,
		Color
	}

	public class Enums
	{
	}
}
