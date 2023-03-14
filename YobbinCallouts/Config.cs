using System;
using System.Windows.Forms;
using Rage;

namespace YobbinCallouts
{
	
	internal static class Config
	{
		
		public static readonly InitializationFile INIFile = new InitializationFile("Plugins\\LSPDFR\\YobbinCallouts.ini");

		
		public static readonly bool BrokenDownVehicle = Config.INIFile.ReadBoolean("Callouts", "Broken Down Vehicle", true);

		
		public static readonly bool AssaultOnBus = Config.INIFile.ReadBoolean("Callouts", "Assault On Bus", true);

		
		public static readonly bool TrafficBreak = Config.INIFile.ReadBoolean("Callouts", "Traffic Break", true);

		
		public static readonly bool PhotographyOfPrivateProperty = Config.INIFile.ReadBoolean("Callouts", "Photography of Private Property", true);

		
		public static readonly bool PropertyCheck = Config.INIFile.ReadBoolean("Callouts", "Property Checkup", true);

		
		public static readonly bool StolenPoliceHardware = Config.INIFile.ReadBoolean("Callouts", "Stolen Police Hardware", true);

		
		public static readonly bool Arson = Config.INIFile.ReadBoolean("Callouts", "Arson", true);

		
		public static readonly bool BarFight = Config.INIFile.ReadBoolean("Callouts", "Bar Fight", true);

		
		public static readonly bool BaitCar = Config.INIFile.ReadBoolean("Callouts", "Bait Car", true);

		
		public static readonly bool RoadRage = Config.INIFile.ReadBoolean("Callouts", "Road Rage", true);

		
		public static readonly bool StolenCellPhone = Config.INIFile.ReadBoolean("Callouts", "Stolen Cell Phone", true);

		
		public static readonly bool SovereignCitizen = Config.INIFile.ReadBoolean("Callouts", "Sovereign Citizen", true);

		
		public static readonly bool ActiveShooter = Config.INIFile.ReadBoolean("Callouts", "Active Shooter", true);

		
		public static readonly bool CitizenArrest = Config.INIFile.ReadBoolean("Callouts", "Citizen's Arrest", true);

		
		public static readonly bool HumanTrafficking = Config.INIFile.ReadBoolean("Callouts", "Human Trafficking", true);

		
		public static readonly bool WeaponFound = Config.INIFile.ReadBoolean("Callouts", "Weapon Found", true);

		
		public static readonly bool HospitalEmergency = Config.INIFile.ReadBoolean("Callouts", "Hospital Emergency", true);

		
		public static readonly bool DUIReported = Config.INIFile.ReadBoolean("Callouts", "DUI Reported", true);

		
		public static readonly bool LandlordTenantDispute = Config.INIFile.ReadBoolean("Callouts", "Landlord-Tenant Dispute", true);

		
		public static readonly bool StolenMail = Config.INIFile.ReadBoolean("Callouts", "Stolen Mail", true);

		
		public static readonly Keys MainInteractionKey = Config.INIFile.ReadEnum<Keys>("Keys", "Main Key", Keys.Y);

		
		public static readonly Keys CalloutEndKey = Config.INIFile.ReadEnum<Keys>("Keys", "Callout End Key", Keys.End);

		
		public static readonly Keys InvestigationEndKey = Config.INIFile.ReadEnum<Keys>("Keys", "Investigation End Key", Keys.Prior);

		
		public static readonly Keys Key1 = Config.INIFile.ReadEnum<Keys>("Keys", "First Option", Keys.Z);

		
		public static readonly Keys Key2 = Config.INIFile.ReadEnum<Keys>("Keys", "Second Option", Keys.X);

		
		public static readonly bool DisplayHelp = Config.INIFile.ReadBoolean("Miscellaneous", "Display Help Messages", true);

		
		public static readonly bool RunInvestigations = Config.INIFile.ReadBoolean("Miscellaneous", "Run Investigations", true);

		
		public static readonly bool LeaveCalloutsRunning = Config.INIFile.ReadBoolean("Miscellaneous", "Leave Callouts Running", false);

		
		public static readonly string PoliceVehicle = Config.INIFile.ReadString("Vehicle", "Police Vehicle", "POLICE");

		
		public static readonly bool CallFD = Config.INIFile.ReadBoolean("Arson", "Automatically Call Fire Department", true);

		
		public static readonly string BaitVehicle = Config.INIFile.ReadString("Bait Car", "Bait Vehicle", "None");
	}
}
