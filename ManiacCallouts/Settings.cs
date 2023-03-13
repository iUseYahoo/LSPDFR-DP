using System;
using System.Windows.Forms;
using Rage;

namespace ManiacCallouts
{
	
	internal static class Settings
	{
		
		internal static void LoadSettings()
		{
			string path = "Plugins/LSPDFR/ManiacCallouts.ini";
			InitializationFile ini = new InitializationFile(path);
			ini.Create();
			Settings.StoreCity = ini.ReadBoolean("ManiacCallouts", "StoreCity", true);
			Settings.StoreSandyPaleto = ini.ReadBoolean("ManiacCallouts", "StoreSandyPaleto", true);
			Settings.Gangattack = ini.ReadBoolean("ManiacCallouts", "Gangattack", true);
			Settings.MoneyTruck = ini.ReadBoolean("ManiacCallouts", "MoneyTruck", true);
			Settings.BackupTrafficStop = ini.ReadBoolean("ManiacCallouts", "BackupTrafficStop", true);
			Settings.SuspiciousActivity = ini.ReadBoolean("ManiacCallouts", "SuspiciousActivity", true);
			Settings.ShowingFirearm = ini.ReadBoolean("ManiacCallouts", "ShowingFirearm", true);
			Settings.BankSilentAlarm = ini.ReadBoolean("ManiacCallouts", "BankSilentAlarm", true);
			Settings.IllegalStreetRace = ini.ReadBoolean("ManiacCallouts", "IllegalStreetRace", true);
			Settings.OfficerNoResponse = ini.ReadBoolean("ManiacCallouts", "OfficerNoResponse", true);
			Settings.TrafficEventsEnable = ini.ReadBoolean("ManiacEvents", "Enable Traffic Events", true);
			Settings.TrafficShowMarker = ini.ReadBoolean("ManiacEvents", "Show Blip Traffic Events", true);
			Settings.PedEventsEnable = ini.ReadBoolean("ManiacEvents", "Enable Pedestrian Events", true);
			Settings.PedShowMarker = ini.ReadBoolean("ManiacEvents", "Show Blip Pedestrian Events", true);
			Settings.MaxCalloutDistance = ini.ReadInt32("Values", "MaxCalloutDistance", Settings.MaxCalloutDistance);
			Settings.MaxTimeAmbientEvent = ini.ReadInt32("ManiacEvents", "Maximun Time Before Create Event", Settings.MaxTimeAmbientEvent);
			Settings.MinTimeAmbientEvent = ini.ReadInt32("ManiacEvents", "Minimun Time Before Create Event", Settings.MinTimeAmbientEvent);
			Settings.EndCall = ini.ReadEnum<Keys>("Keybindings", "EndCall", Keys.End);
			Settings.Interact = ini.ReadEnum<Keys>("Keybindings", "Interact", Keys.Y);
		}

		
		internal static bool StoreCity = true;

		
		internal static bool StoreSandyPaleto = true;

		
		internal static bool Gangattack = true;

		
		internal static bool MoneyTruck = true;

		
		internal static bool BackupTrafficStop = true;

		
		internal static bool SuspiciousActivity = true;

		
		internal static bool ShowingFirearm = true;

		
		internal static bool BankSilentAlarm = true;

		
		internal static bool IllegalStreetRace = true;

		
		internal static bool OfficerNoResponse = true;

		
		internal static bool TrafficEventsEnable = true;

		
		internal static bool TrafficShowMarker = true;

		
		internal static bool PedEventsEnable = true;

		
		internal static bool PedShowMarker = true;

		
		internal static int MaxTimeAmbientEvent = 120;

		
		internal static int MinTimeAmbientEvent = 20;

		
		internal static int MaxCalloutDistance = 1500;

		
		internal static Keys EndCall = Keys.End;

		
		internal static Keys Interact = Keys.Y;

		
		public static readonly string CalloutVersion = "1.3.0.0";
	}
}
