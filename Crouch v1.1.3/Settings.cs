using System;
using System.Windows.Forms;
using Rage;

namespace Crouch.Essential
{
	
	internal static class Settings
	{
		
		internal static void Initialize()
		{
			try
			{
				InitializationFile initializationFile = new InitializationFile("plugins\\Crouch.ini");
				initializationFile.Create();
				Settings.CrouchKey = initializationFile.ReadEnum<Keys>("General", "CrouchKey", Settings.CrouchKey);
				Settings.ControllerCrouchButton = initializationFile.ReadEnum<ControllerButtons>("General", "ControllerCrouchButton", Settings.ControllerCrouchButton);
				Settings.AllowController = initializationFile.ReadBoolean("General", "AllowController", Settings.AllowController);
				Game.LogTrivial("Loaded settings");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "Settings.Initialize()");
			}
		}

		
		internal static Keys CrouchKey = Keys.LControlKey;

		
		internal static ControllerButtons ControllerCrouchButton = 64;

		
		internal static bool AllowController = true;

		
		internal static bool EarlyAccess = false;

		
		internal static string EAVersion = "";
	}
}
