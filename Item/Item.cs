using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItemEvaluator
{
	public class Item
	{
		public string Name { get; private set; }
		public string UserWhoCreated { get; private set; }
		public Vector2 Weight { get; private set; }
		public Vector2 Height { get; private set; }
		public bool HasTemperature { get; private set; } = false;
		public double Temperature { get; private set; }
		public List<ItemTags> ItemTags { get; private set; }
		public ConsoleColor Color { get; private set; }
				
		[JsonConstructor]
		public Item(string name, string userWhoCreated, Vector2 weight, Vector2 height, bool hasTemperature,
			double temperature, List<ItemTags> itemTags, ConsoleColor color)
		{
			this.Name = name;
			this.UserWhoCreated = userWhoCreated;
			this.Weight = weight;
			this.Height = height;
			this.HasTemperature = hasTemperature;
			this.Temperature = temperature;
			this.ItemTags = itemTags;
			this.Color = color;
		}		
	}
}
