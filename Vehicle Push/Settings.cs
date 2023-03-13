using System;
using System.Windows.Forms;
using Rage;

namespace VehiclePush.Essential
{
	
	internal static class Settings
	{
		
		internal static void Initialize()
		{
			try
			{
				InitializationFile initializationFile = new InitializationFile("plugins\\Vehicle Push.ini");
				initializationFile.Create();
				Settings.ClearDesignatedVehicleKey = initializationFile.ReadEnum<Keys>("Keybindings", "ClearDesignatedVehicleKey", Settings.ClearDesignatedVehicleKey);
				Settings.PushVehicleModifierKey = initializationFile.ReadEnum<Keys>("Keybindings", "PushVehicleModifierKey", Settings.PushVehicleModifierKey);
				Settings.PushVehicleKey = initializationFile.ReadEnum<Keys>("Keybindings", "PushVehicleKey", Settings.PushVehicleKey);
				Game.LogTrivial("Loaded settings");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "Settings.Initialize()");
			}
		}

		
		internal static Keys ClearDesignatedVehicleKey = Keys.H;

		
		internal static Keys PushVehicleModifierKey = Keys.LShiftKey;

		
		internal static Keys PushVehicleKey = Keys.E;
	}
}
