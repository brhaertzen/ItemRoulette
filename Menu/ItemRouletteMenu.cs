using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class ItemRouletteMenu : Menu
	{
		public ItemRouletteMenu(Navigator navigator) : base(navigator)
		{
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in the Item Roulette.");
			return MenuState.MainMenu;
		}
	}
}
