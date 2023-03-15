using System;
using System.Net;
using System.Reflection;
using Rage;

namespace VectorGrabber
{
	
	internal static class VersionChecker
	{
		
		
		internal static string CurrentVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
			}
		}

		
		internal static void CheckForUpdates()
		{
			WebClient webClient = new WebClient();
			bool pluginUpToDate = false;
			bool webSuccess = false;
			try
			{
				string receivedVersion = webClient.DownloadString("https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=43016&textOnly=1").Trim();
				Game.LogTrivial("Vector Grabber: Online Version: " + receivedVersion + " | Local VectorGrabber Version: " + VersionChecker.CurrentVersion);
				pluginUpToDate = (receivedVersion == VersionChecker.CurrentVersion);
				webSuccess = true;
			}
			catch (WebException)
			{
				Game.DisplayNotification("Vector Grabber By Roheat\nPlease make sure you are connected to proper WIFI Network.");
			}
			finally
			{
				Game.DisplayNotification("Vector Grabber By Roheat\nVersion is " + (webSuccess ? (pluginUpToDate ? "~g~Up To Date" : "~r~Out Of Date") : "~o~Version Check Failed"));
				bool flag = !pluginUpToDate;
				if (flag)
				{
					Game.LogTrivial("Vector Grabber: [VERSION OUTDATED] Please update to latest version here: https://www.lcpdfr.com/downloads/gta5mods/scripts/43016-vectorgrabber/");
				}
			}
		}
	}
}
