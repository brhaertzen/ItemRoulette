using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class Item
	{
		public string Name { get; private set; }
		public User UserWhoCreated { get; private set; }
		public Vector2 Weight { get; private set; }
		public Vector2 Height { get; private set; }
		public bool HasTemperature { get; private set; } = false;
		public double Temperature { get; private set; }
		public List<ItemTags> ItemTags { get; private set; }
		public List<ConsoleColor> ColorTags { get; private set; }

		//Item with temperature
		public Item(string name, User userWhoCreated, Vector2 weight, Vector2 height,
			double temperature, List<ItemTags> itemTags, List<ConsoleColor> colorTags)
		{
			this.Name = name;
			this.UserWhoCreated = userWhoCreated;
			this.Weight = weight;
			this.Height = height;
			this.HasTemperature = true;
			this.Temperature = temperature;
			this.ItemTags = itemTags;
			this.ColorTags = colorTags;
		}

		//Item without temperature
		public Item(string name, User userWhoCreated, Vector2 weight, Vector2 height,
			List<ItemTags> itemTags, List<ConsoleColor> colorTags)
		{
			this.Name = name;
			this.UserWhoCreated = userWhoCreated;
			this.Weight = weight;
			this.Height = height;
			this.HasTemperature = false;
			this.ItemTags = itemTags;
			this.ColorTags = colorTags;
		}
	}
}
