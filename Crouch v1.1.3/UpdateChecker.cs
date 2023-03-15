using System;
using System.Net;
using System.Reflection;
using Rage;

namespace Crouch.Essential
{
	
	internal class UpdateChecker
	{
		
		
		internal static string LocalVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);
			}
		}

		
		internal static bool Initialize()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			WebClient webClient = new WebClient();
			Uri address = new Uri("https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=40396&textOnly=1");
			try
			{
				Game.LogTrivial("Local version: " + UpdateChecker.LocalVersion);
				UpdateChecker.OnlineVersion = webClient.DownloadString(address).Trim();
			}
			catch (WebException ex)
			{
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", Helper.assemblyName, "~r~Warning", "Failed to check for updates, ~y~Possible network error.");
				Game.LogTrivial("Failed to check for updates");
				Game.LogTrivial(ex.Message);
				UpdateChecker.ExceptionOccured = true;
			}
			if (UpdateChecker.OnlineVersion != UpdateChecker.LocalVersion && !UpdateChecker.ExceptionOccured && !Settings.EarlyAccess)
			{
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", Helper.assemblyName, "~r~v" + UpdateChecker.LocalVersion + " ~c~by Faya", "Found update ~g~v" + UpdateChecker.OnlineVersion + " ~s~available for you!");
				Game.LogTrivial("There is an update available");
				return true;
			}
			if (Settings.EarlyAccess)
			{
				Game.DisplayNotification("commonmenu", "shop_box_tick", Helper.assemblyName, string.Concat(new string[]
				{
					"~g~v",
					UpdateChecker.LocalVersion,
					"-beta",
					Settings.EAVersion,
					" ~c~by Faya"
				}), "~y~Early access~s~ ready for use!");
				Game.LogTrivial(string.Concat(new string[]
				{
					"v",
					UpdateChecker.LocalVersion,
					"-beta",
					Settings.EAVersion,
					" is ready for use"
				}));
				return false;
			}
			Game.DisplayNotification("commonmenu", "shop_box_tick", Helper.assemblyName, "~g~v" + UpdateChecker.LocalVersion + " ~c~by Faya", "~y~I don't know what to put here...");
			Game.LogTrivial("There is no update available");
			return false;
		}

		
		internal static string OnlineVersion = string.Empty;

		
		private static bool ExceptionOccured;
	}
}
