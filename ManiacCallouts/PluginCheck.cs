using System;
using System.Net;
using Rage;

namespace ManiacCallouts
{
	
	public class PluginCheck
	{
		
		public static bool Updatecheck()
		{
			string curVersion = Settings.CalloutVersion;
			Uri latestVersionUri = new Uri("https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=38222&textOnly=1");
			WebClient webClient = new WebClient();
			string receivedData = string.Empty;
			try
			{
				receivedData = webClient.DownloadString(latestVersionUri).Trim();
			}
			catch (WebException)
			{
				Game.DisplayNotification("~w~ManiacCallouts Notification: ~r~Failed To Do Update Check!");
				Game.Console.Print("[ManiacCallouts]: Failed to check for an update.");
				Game.Console.Print("[ManiacCallouts]: Please make sure you have connection to the internet!");
				return false;
			}
			bool flag = receivedData != Settings.CalloutVersion;
			bool result;
			if (flag)
			{
				Game.DisplayNotification(string.Concat(new string[]
				{
					"~w~ManiacCallouts Notification: ~w~A new Update is available! Current Version: ~y~",
					curVersion,
					"~w~<br>New Version: ~o~",
					receivedData,
					"<br>~w~Update to the latest build to get the best experience!"
				}));
				Game.Console.Print("[ManiacCallouts]: A new version of ManiacCallouts is available! Update to the latest build to get the best experience!");
				Game.Console.Print("[ManiacCallouts]: Current Version:  " + curVersion);
				Game.Console.Print("[ManiacCallouts]: New Version:  " + receivedData);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}
	}
}
