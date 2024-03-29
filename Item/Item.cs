﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItemRoulette
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
			Name = name;
			UserWhoCreated = userWhoCreated;
			Weight = weight;
			Height = height;
			HasTemperature = hasTemperature;
			Temperature = temperature;
			ItemTags = itemTags;
			Color = color;
		}	
		
		public void AdjustUserWhoCreated(string newUserName)
		{
			UserWhoCreated = newUserName;
		}
	}
}
