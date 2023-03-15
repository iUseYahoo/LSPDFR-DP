using System;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using Rage;

namespace Crouch.Essential
{
	
	internal static class Helper
	{
		
		internal static void LogException(Exception e, string location)
		{
			Game.LogTrivial(e.Message + " At " + location);
			Game.DisplayNotification("commonmenu", "mp_alerttriangle", Helper.assemblyName, "~r~Issue detected!", "Please fill in a ~g~bug report form~s~.\nThat can be found on the ~y~" + Helper.assemblyName + "~s~ page.");
			Helper.LogToDiscord(e, location);
		}

		
		public static byte[] post(string uri, NameValueCollection pair)
		{
			byte[] result;
			using (WebClient webClient = new WebClient())
			{
				result = webClient.UploadValues(uri, pair);
			}
			return result;
		}

		
		internal static void LogToDiscord(Exception e, string location)
		{
			try
			{
				if (!UpdateChecker.Initialize())
				{
					if (Settings.EarlyAccess)
					{
						Helper.version = UpdateChecker.LocalVersion + "-beta" + Settings.EAVersion;
					}
					else
					{
						Helper.version = UpdateChecker.LocalVersion;
					}
					Helper.post(Helper.webhook, new NameValueCollection
					{
						{
							"username",
							Helper.assemblyName + " v" + Helper.version
						},
						{
							"content",
							string.Concat(new string[]
							{
								"```",
								e.Message,
								"```\n`Location: ",
								location,
								"`"
							})
						}
					});
					Game.LogTrivial("Sent exception message to Discord webhook");
				}
			}
			catch (WebException ex)
			{
				Game.LogTrivial(ex.Message);
			}
			catch (Exception ex2)
			{
				Game.LogTrivial(ex2.Message);
			}
		}

		
		internal static string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

		
		private static string webhook = "removed for their security";

		
		private static string version = string.Empty;
	}
}
