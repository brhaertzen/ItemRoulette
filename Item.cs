using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public enum ItemTags
	{

	}

	public enum ColorTags
	{
		Red,
		Orange,
		Yellow,
		Green,
		Blue,
		Purple,
		Brown,
		White,
		Black
	}

	public class Item
	{
		public string name { get; private set; }
		public string userWhoCreated { get; private set; }
		public float weight { get; private set; }
		public List<ItemTags> itemTags { get; private set; }
		public List<ColorTags> colorTags { get; private set; }

		public Item(String name, string userWhoCreated, float weight, List<ItemTags> itemTags, List<ColorTags> colorTags)
		{
			this.name = name;
			this.userWhoCreated = userWhoCreated;
			this.weight = weight;
			this.itemTags = itemTags;
			this.colorTags = colorTags;
		}
	}
}
