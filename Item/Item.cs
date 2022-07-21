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
		public string UserWhoCreated { get; private set; }
		public float Weight { get; private set; }
		public float Height { get; private set; }
		public List<ItemTags> ItemTags { get; private set; }
		public List<ColorTag> ColorTags { get; private set; }

		public Item(String name, string userWhoCreated, float weight, float height, List<ItemTags> itemTags, List<ColorTag> colorTags)
		{
			this.Name = name;
			this.UserWhoCreated = userWhoCreated;
			this.Weight = weight;
			this.Height = height;
			this.ItemTags = itemTags;
			this.ColorTags = colorTags;
		}
	}
}
