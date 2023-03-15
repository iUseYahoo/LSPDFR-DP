using System;
using System.Windows.Forms;
using Rage;

namespace VectorGrabber
{
	
	internal static class Settings
	{
		
		internal static void Initialize()
		{
			try
			{
				Settings.iniFile = new InitializationFile("Plugins/VectorGrabber.ini");
				Settings.iniFile.Create();
				Settings.SaveKey = Settings.iniFile.ReadEnum<Keys>("Keybinds", "Savekey", Settings.SaveKey);
				Settings.TeleportNextKey = Settings.iniFile.ReadEnum<Keys>("Keybinds", "NextKey", Settings.TeleportNextKey);
				Settings.TeleportBackKey = Settings.iniFile.ReadEnum<Keys>("Keybinds", "BackKey", Settings.TeleportBackKey);
				Settings.TeleportKey = Settings.iniFile.ReadEnum<Keys>("Keybinds", "TeleportKey", Settings.TeleportKey);
				Settings.MenuKey = Settings.iniFile.ReadEnum<Keys>("Keybinds", "MenuKey", Settings.MenuKey);
				Settings.RereadFile = Settings.iniFile.ReadEnum<Keys>("Keybinds", "RereadFile", Settings.RereadFile);
				Settings.ClipboardKey = Settings.iniFile.ReadEnum<Keys>("Keybinds", "ClipboardKey", Settings.ClipboardKey);
				Settings.ModifierKey = Settings.iniFile.ReadEnum<Keys>("Keybinds", "ModifierKey", Settings.ModifierKey);
				Settings.EnableVectorBlips = Settings.iniFile.ReadBoolean("Customization", "EnableVectorBlips", Settings.EnableVectorBlips);
			}
			catch (Exception e)
			{
				string error = e.ToString();
				Game.LogTrivial("Vector Grabber: ERROR IN 'Settings.cs, Initialize()': " + error);
				Game.DisplayNotification("Vector Grabber: Error Occured");
			}
		}

		
		internal static void UpdateINI()
		{
			Settings.iniFile.Write("Customization", "EnableVectorBlips", Settings.EnableVectorBlips);
		}

		
		internal static Keys SaveKey = Keys.Y;

		
		internal static Keys TeleportNextKey = Keys.Right;

		
		internal static Keys TeleportBackKey = Keys.Left;

		
		internal static Keys TeleportKey = Keys.T;

		
		internal static Keys RereadFile = Keys.R;

		
		internal static Keys ClipboardKey = Keys.C;

		
		internal static Keys MenuKey = Keys.M;

		
		internal static Keys ModifierKey = Keys.LControlKey;

		
		internal static bool EnableVectorBlips = true;

		
		internal static InitializationFile iniFile;
	}
}
