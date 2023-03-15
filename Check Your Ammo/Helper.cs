using System;
using System.Reflection;
using Rage;
using RAGENativeUI.Elements;

namespace CheckYourAmmo.Essential
{
	
	internal class Helper
	{
		
		
		internal static Ped MainPlayer
		{
			get
			{
				return Game.LocalPlayer.Character;
			}
		}

		
		
		internal static string assemblyName
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Name;
			}
		}

		
		internal static void LogException(Exception e, string location)
		{
			Game.LogTrivial(e.Message + " At " + location);
			Game.DisplayNotification("commonmenu", "mp_alerttriangle", Helper.assemblyName, "~r~Issue detected!", "Please fill in a ~g~bug report form~s~.\nThat can be found on the ~y~" + Helper.assemblyName + "~s~ page.");
		}

		
		internal static TimerBarPool tbPool = new TimerBarPool();

		
		internal static Object magazine;

		
		internal static float loadedAmmo = 0f;

		
		internal static float magazineSize = 0f;

		
		internal static float percentage = 0f;

		
		internal static int curViewCam = 0;

		
		internal static int timeout = 3600;

		
		internal static int voiceNumber = 0;

		
		internal static int keyDownCounter = 0;

		
		internal static int keyDownDuration = 40;

		
		internal static bool isChecking;

		
		internal static bool hasMagazine = true;

		
		internal static bool allowAnimation = true;

		
		internal static bool allowChecking = true;
	}
}
