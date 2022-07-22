using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemEvaluator
{
	public class User
	{
		public string Name { get; private set; }
		public UserSettings UserSettings { get; private set; }
		public int ItemsCreated { get; private set; } = 0;

		public User(String name, UserSettings settings)
		{
			this.Name = name;
			this.UserSettings = settings;
		}

		public void AdjustUserSettings(UserSettings newSettings)
		{
			this.UserSettings = newSettings;
		}

		public void IncreaseItemsCreatedCount()
		{
			ItemsCreated++;
		}
	}
}
