using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class ItemCreatorMenu : Menu
	{
		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in the Item Creator.");
			return MenuState.MainMenu;
		}
	}
}
