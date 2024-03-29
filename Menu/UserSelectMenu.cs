﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoulette
{
	public class UserSelectMenu : Menu
	{
		public UserSelectMenu(Navigator navigator) : base(navigator)
		{
		}

		public override MenuState Enter()
		{
			MenuStateEnterText($"You are now in User Select.");
			Console.WriteLine(
				$"Please select a User by typing their name from the following list:" +
				$"{returnToMainMenuOption}");
			Dictionary<string, User> nameDict = new Dictionary<string, User>();
			foreach (var user in nav.UserList)
			{
				nameDict.Add(user.Name.ToLower(), user);
				WriteColor($"{quote}[={user.ColorPref}]{user.Name}[/]{quote}");
			}
			bool validUserOption = false;
			while (!validUserOption)
			{
				string userResponse = Console.ReadLine().ToLower();
				switch (userResponse)
				{
					case "escape": return MenuState.MainMenu;
					default:
						if (nameDict.ContainsKey(userResponse))
						{
							validUserOption = true;
							nameDict.TryGetValue(userResponse, out User nextSelectedUser);
							nav.CurrentUser = nextSelectedUser;
							WriteColor(
								$"\n" +
								$"User set to [={nav.CurrentUser.ColorPref}]{nav.CurrentUser.Name}[/].\n" +
								$"Press any key to return to Main Menu.");
							Console.ReadKey();
						}
						else
							Console.WriteLine($"{invalidResponse}"); break;
				}
			}
			return MenuState.MainMenu;
		}
	}
}
