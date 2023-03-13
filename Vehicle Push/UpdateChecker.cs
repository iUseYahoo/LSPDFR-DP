using System;
using System.Net;
using System.Reflection;
using Rage;

namespace VehiclePush.Essential
{
	
	internal static class UpdateChecker
	{
		
		internal static bool Initialize()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			WebClient webClient = new WebClient();
			Uri address = new Uri("https:
			try
			{
				UpdateChecker.OnlineVersion = webClient.DownloadString(address).Trim();
			}
			catch (WebException ex)
			{
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Vehicle Push", "~r~Error", "Failed to check for updates, ~y~Possible network error.");
				Game.LogTrivial("Failed to check for updates");
				Game.LogTrivial(ex.Message);
				UpdateChecker.ExceptionOccured = true;
			}
			if (UpdateChecker.OnlineVersion != UpdateChecker.LocalVersion && !UpdateChecker.ExceptionOccured)
			{
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Vehicle Push", "~r~v" + UpdateChecker.LocalVersion + " ~c~by Faya", "Found update ~g~v" + UpdateChecker.OnlineVersion + " ~s~available for you!");
				Game.LogTrivial("There is an update available");
				return true;
			}
			Game.DisplayNotification("commonmenu", "shop_garage_icon_a", "Vehicle Push", "~g~v" + UpdateChecker.LocalVersion + " ~c~by Faya", string.Format("~y~Ready for some pushing...\n~c~{0} + {1} to designate a vehicle", Settings.PushVehicleModifierKey, Settings.PushVehicleKey));
			Game.LogTrivial("There are no updates available");
			return false;
		}

		
		internal static string OnlineVersion = string.Empty;

		
		internal static string LocalVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);

		
		private static bool ExceptionOccured;
	}
}
