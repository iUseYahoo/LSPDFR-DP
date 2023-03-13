using System;
using System.Net;
using Rage;

namespace EmergencyCallouts.Essential
{
	// Token: 0x0200000D RID: 13
	internal class UpdateChecker
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002A00 File Offset: 0x00000C00
		internal static bool UpdateAvailable()
		{
			WebClient webClient = new WebClient();
			Uri address = new Uri("https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=37760&textOnly=1");
			try
			{
				Game.LogTrivial("[Emergency Callouts]: Checking for updates");
				UpdateChecker.OnlineVersion = webClient.DownloadString(address).Trim();
			}
			catch (WebException)
			{
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~r~Error", "Failed to check for updates, ~y~Possible network error.");
				Game.LogTrivial("[Emergency Callouts]: Checked for updates; Failed to check");
				UpdateChecker.ExceptionOccured = true;
			}
			if (UpdateChecker.OnlineVersion != Project.LocalVersion && !Settings.EarlyAccess && !UpdateChecker.ExceptionOccured)
			{
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~r~v" + Project.LocalVersion + " ~c~by Faya", "Found update ~g~v" + UpdateChecker.OnlineVersion + " ~s~available for you!");
				Game.LogTrivial("[Emergency Callouts]: Checked for updates; Found an update");
				return true;
			}
			if (Settings.EarlyAccess)
			{
				Game.DisplayNotification("dia_police", "dia_police", "Emergency Callouts", string.Concat(new string[]
				{
					"~g~v",
					Project.LocalVersion,
					"-beta",
					UpdateChecker.EarlyAccessExtension,
					" ~c~by Faya"
				}), "~y~Early Access~s~ ready for use!");
				Game.LogTrivial("[Emergency Callouts]: Checked for updates; Early Access Loaded");
				return false;
			}
			Game.DisplayNotification("dia_police", "dia_police", "Emergency Callouts", "~g~v" + Project.LocalVersion + " ~c~by Faya", "~y~Reporting for duty!");
			Game.LogTrivial("[Emergency Callouts]: Checked for updates; None available");
			return false;
		}

		// Token: 0x04000017 RID: 23
		internal static string OnlineVersion = string.Empty;

		// Token: 0x04000018 RID: 24
		private static bool ExceptionOccured;

		// Token: 0x04000019 RID: 25
		private static string EarlyAccessExtension = "";
	}
}
