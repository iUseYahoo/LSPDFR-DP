using System;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using Rage;

namespace VehiclePush.Essential
{
	
	internal static class Helper
	{
		
		
		internal static Ped MainPlayer
		{
			get
			{
				return Game.LocalPlayer.Character;
			}
		}

		
		internal static void LogException(Exception e, string location)
		{
			Game.LogTrivial(e.Message + " At " + location);
			Game.DisplayNotification("commonmenu", "mp_alerttriangle", Helper.assemblyName, "~r~Issue detected!", "Please fill in a ~g~bug report form~s~.\nThat can be found on the ~y~" + Helper.assemblyName + " Page~s~.");
			Helper.LogToDiscord(e, location);
		}

		
		public static byte[] POST(string uri, NameValueCollection pair)
		{
			byte[] result;
			using (WebClient webClient = new WebClient())
			{
				result = webClient.UploadValues(uri, pair);
			}
			return result;
		}

		
		internal static void LogToDiscord(Exception ex, string location)
		{
			try
			{
				Helper.POST(Helper.token, new NameValueCollection
				{
					{
						"username",
						Helper.assemblyName + " v" + UpdateChecker.LocalVersion
					},
					{
						"content",
						string.Concat(new string[]
						{
							string.Format("**Exception Type**```fix\n{0}```\n", ex.GetType()),
							string.Format("**Stack Trace**```\n{0}\n```\n", ex),
							"**Message**```\n",
							ex.Message,
							"\n```\n**Location**\n```prolog\n",
							location,
							"```"
						})
					}
				});
				Game.LogTrivial("Sent exception message to Discord webhook");
			}
			catch (WebException ex2)
			{
				Game.LogTrivial(ex2.Message);
			}
			catch (Exception ex3)
			{
				Game.LogTrivial(ex3.Message);
			}
		}

		
		internal static readonly string[] coolLines = new string[]
		{
			"Hold up... wait a minute...",
			"Gimme a sec...",
			"One sec...",
			"Need a doula... get it?",
			"Enabling super strength...",
			"Getting ready...",
			"You better push hard..."
		};

		
		internal static Vehicle ClosestVehicle;

		
		internal static bool playingAnimation;

		
		internal static bool isInFront;

		
		internal static bool vehicleSet;

		
		internal static bool isPushing;

		
		internal static bool hidHelpMessage;

		
		internal static bool holsteredWeapon;

		
		internal static string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

		
		internal static string token = "https:
	}
}
