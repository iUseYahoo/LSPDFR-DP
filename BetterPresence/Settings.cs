using System;
using System.Net;
using Rage;

namespace BetterPresence
{
	
	public class Settings
	{
		
		public static void UpdateSettings()
		{
			InitializationFile initializationFile = new InitializationFile("Plugins/LSPDFR/BetterPresence.ini");
			initializationFile.Create();
			Settings.DetailsLine = initializationFile.ReadString("Settings", "DetailsLine", "%STATUS% in %STREET_NAME%, %REGION% as %DEPARTMENT%");
		}

		
		public static void CheckVersion()
		{
			Uri address = new Uri("https:
			WebClient webClient = new WebClient();
			string text = string.Empty;
			try
			{
				text = webClient.DownloadString(address);
			}
			catch (WebException)
			{
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", "~r~BetterPresence", "~y~Failed to check for updates!", "~w~BetterPresence could not check for updates! You are running version " + Settings.version);
			}
			if (text != Settings.version)
			{
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", "~y~BetterPresence", "~w~A new update is available for BetterPresence!", "~w~You are running ~r~" + Settings.version + "~w~, and BetterPresence is on " + text);
			}
		}

		
		public static string DetailsLine = "%STATUS% in %STREET_NAME%, %REGION% as %DEPARTMENT%";

		
		public static readonly string version = "2.0.1";
	}
}
