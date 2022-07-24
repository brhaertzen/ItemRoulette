using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class UserSelectMenu : Menu
	{
		private Navigator navigator;
		private List<User> userList;
		private User currentUser;

		public UserSelectMenu(Navigator navigator, List<User> userList, User currentUser)
		{
			this.navigator = navigator;
			this.userList = userList;
			this.currentUser = currentUser;
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in User Select.");

			Console.WriteLine($"Please select a User by typing their name from the following list:");
			Dictionary<string, User> nameDict = new Dictionary<string, User>();
			foreach (var user in userList)
			{
				nameDict.Add(user.Name, user);
				Console.WriteLine($"{quote}{user.Name}{quote}");
			}
			bool validUserOption = false;
			while (!validUserOption)
			{
				string userResponse = Console.ReadLine().ToLower();
				if (nameDict.ContainsKey(userResponse))
				{
					validUserOption = true;
					nameDict.TryGetValue(userResponse, out User nextSelectedUser);
					currentUser = nextSelectedUser;
					navigator.UpdateUserAndList(userList, currentUser);
					Console.WriteLine(
						$"\n" +
						$"User set to {currentUser.Name}\n" +
						$"Press any key to return to Main Menu.");
					Console.ReadKey();
				}
				else
					Console.WriteLine($"Invalid Response. Please try again.");
			}
			return MenuState.MainMenu;
		}
	}
}
