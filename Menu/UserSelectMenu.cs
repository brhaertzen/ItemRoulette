using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class UserSelectMenu : Menu
	{
		private List<User> userList;
		private User currentUser;

		public UserSelectMenu(List<User> userList, User currentUser)
		{
			this.userList = userList;
			this.currentUser = currentUser;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in User Select.");
			return MenuState.MainMenu;
		}
	}
}
