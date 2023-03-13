using System;
using System.Windows.Forms;
using Rage;

namespace EmergencyCallouts.Essential
{
	
	internal static class Settings
	{
		
		internal static void Initialize()
		{
			InitializationFile initializationFile = new InitializationFile(Project.SettingsPath);
			initializationFile.Create();
			Settings.PublicIntoxication = initializationFile.ReadBoolean("Callouts", "PublicIntoxication", Settings.PublicIntoxication);
			Settings.Trespassing = initializationFile.ReadBoolean("Callouts", "Trespassing", Settings.Trespassing);
			Settings.DomesticViolence = initializationFile.ReadBoolean("Callouts", "DomesticViolence", Settings.DomesticViolence);
			Settings.Burglary = initializationFile.ReadBoolean("Callouts", "Burglary", Settings.Burglary);
			Settings.SuspiciousActivity = initializationFile.ReadBoolean("Callouts", "SuspiciousActivity", Settings.SuspiciousActivity);
			Settings.SearchAreaSize = initializationFile.ReadInt32("Measurements", "SearchAreaSize", Settings.SearchAreaSize);
			Settings.MaxCalloutDistance = initializationFile.ReadInt32("Measurements", "MaxCalloutDistance", Settings.MaxCalloutDistance);
			Settings.PedBlipScale = initializationFile.ReadDouble("Measurements", "PedBlipScale", Settings.PedBlipScale);
			Settings.InteractKey = initializationFile.ReadEnum<Keys>("Keybindings", "InteractKey", Settings.InteractKey);
			Settings.EndCalloutKey = initializationFile.ReadEnum<Keys>("Keybindings", "EndCalloutKey", Settings.EndCalloutKey);
			Settings.ChanceOfPropertyDamage = initializationFile.ReadInt32("Chances", "ChanceOfPropertyDamage", Settings.ChanceOfPropertyDamage);
			Settings.ChanceOfPressingCharges = initializationFile.ReadInt32("Chances", "ChanceOfPressingCharges", Settings.ChanceOfPressingCharges);
			Settings.ChanceOfCallingOwner = initializationFile.ReadInt32("Chances", "ChanceOfCallingOwner", Settings.ChanceOfCallingOwner);
			Game.LogTrivial("[Emergency Callouts]: Loaded settings");
		}

		
		internal static bool PublicIntoxication = true;

		
		internal static bool Trespassing = true;

		
		internal static bool DomesticViolence = true;

		
		internal static bool Burglary = true;

		
		internal static bool SuspiciousActivity = true;

		
		internal static int SearchAreaSize = 60;

		
		internal static int MaxCalloutDistance = 1000;

		
		internal static double PedBlipScale = 0.75;

		
		internal static Keys InteractKey = Keys.Y;

		
		internal static ControllerButtons ControllerInteractKey = 4;

		
		internal static Keys EndCalloutKey = Keys.End;

		
		internal static bool AllowController = true;

		
		internal static int ChanceOfPropertyDamage = 75;

		
		internal static int ChanceOfPressingCharges = 50;

		
		internal static int ChanceOfCallingOwner = 50;

		
		internal static bool EarlyAccess = false;
	}
}
