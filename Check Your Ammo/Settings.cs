using System;
using System.Windows.Forms;
using Rage;

namespace CheckYourAmmo.Essential
{
	
	internal static class Settings
	{
		
		internal static void Initialize()
		{
			try
			{
				InitializationFile initializationFile = new InitializationFile("plugins\\Check Your Ammo.ini");
				initializationFile.Create();
				Settings.HoldToCheckKey = initializationFile.ReadEnum<Keys>("General", "HoldToCheckKey", Settings.HoldToCheckKey);
				Settings.VoiceLines = initializationFile.ReadBoolean("General", "VoiceLines", Settings.VoiceLines);
				Settings.WeaponWheelLoadedAmmoCensoring = initializationFile.ReadBoolean("General", "WeaponWheelLoadedAmmoCensoring", Settings.WeaponWheelLoadedAmmoCensoring);
				Game.LogTrivial("Loaded settings");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "Settings.Initialize()");
			}
		}

		
		internal static Keys HoldToCheckKey = Keys.R;

		
		internal static bool VoiceLines = true;

		
		internal static bool WeaponWheelLoadedAmmoCensoring = true;

		
		internal static bool EarlyAccess = false;

		
		internal static string EAVersion = "";
	}
}
