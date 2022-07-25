using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class ItemViewerMenu : Menu
	{
		public ItemViewerMenu()
		{
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in your Item Viewer.");
			return MenuState.MainMenu;
		}
	}
}
