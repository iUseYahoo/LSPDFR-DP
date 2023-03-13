using System;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using LeroyCalloutsV2.Callouts;
using LSPD_First_Response.Mod.API;
using Rage;

namespace LeroyCalloutsV2
{
	
	public class Main : Plugin
	{
		
		public override void Initialize()
		{
			Game.LogTrivial("Leroy Callouts | To report a bug or request support, use the Discord invite link on the \"Leroy Callouts\" page of lspdfr.com");
			Functions.OnOnDutyStateChanged += new Functions.OnDutyStateChangedEventHandler(Main.OnOnDutyStateChangedHandler);
			GameFiber.StartNew(new ThreadStart(menu.Start));
			try
			{
				string path = "Plugins/LSPDFR/LeroyCallouts.ini";
				InitializationFile ini = new InitializationFile(path);
				bool flag = !ini.Exists();
				if (flag)
				{
					ini.Create();
				}
				Main.TalkKeyString = ini.ReadString("Keybindings", "TalkKey", "Y");
				Main.EndCalloutKeyString = ini.ReadString("Keybindings", "EndCalloutKey", "End");
				Main.MenuKeyString = ini.ReadString("Keybindings", "MenuKey", "F10");
				KeysConverter kc = new KeysConverter();
				Main.TalkKey = (Keys)kc.ConvertFromString(Main.TalkKeyString);
				Main.EndCalloutKey = (Keys)kc.ConvertFromString(Main.EndCalloutKeyString);
				Main.MenuKey = (Keys)kc.ConvertFromString(Main.MenuKeyString);
				Main.PanicButton = ini.Read<bool>("Callouts", "PanicButton", true);
				Main.cityOfficerCar = ini.Read<string>("Other", "PanicButtonCityCar", "POLICE3");
				Main.countyOfficerCar = ini.Read<string>("Other", "PanicButtonCountyCar", "SHERIFF");
				Main.Carjacking = ini.Read<bool>("Callouts", "Carjacking", true);
				Main.Stalking = ini.Read<bool>("Callouts", "Stalking", true);
				Main.PublicUrination = ini.Read<bool>("Callouts", "PublicUrination", true);
				Main.AggressivePanhandling = ini.Read<bool>("Callouts", "AggressivePanhandling", true);
				Main.CustomerRefusingToLeave = ini.Read<bool>("Callouts", "CustomerRefusingToLeave", true);
				Main.PersonVandalizingVehicle = ini.Read<bool>("Callouts", "PersonVandalizingVehicle", true);
				Main.ElderlyCivilianLost = ini.Read<bool>("Callouts", "ElderlyCivilianLost", true);
				Main.UnderageWithFakeID = ini.Read<bool>("Callouts", "UnderageWithFakeID", true);
				Main.HomelessPersonOnPrivateProperty = ini.Read<bool>("Callouts", "HomelessPersonOnPrivateProperty", true);
				Main.SecurityGuardAttacked = ini.Read<bool>("Callouts", "SecurityGuardAttacked", true);
				Main.Disturbance = ini.Read<bool>("Callouts", "Disturbance", true);
				Main.Fighting = ini.Read<bool>("Callouts", "Fighting", true);
				Main.checkForUpdatesOnStart = ini.Read<bool>("Other", "CheckForUpdatesOnStart", true);
				Main.probViolent = ini.Read<int>("Other", "ViolentOutcomeProbability", 20);
				Main.minSpawnDistance = ini.Read<int>("Other", "MinimumCalloutDistance", 300);
				Main.maxSpawnDistance = ini.Read<int>("Other", "MaximumCalloutDistance", 1650);
				bool flag2 = Main.checkForUpdatesOnStart;
				if (flag2)
				{
					GameFiber.StartNew(new ThreadStart(this.CheckForUpdate));
				}
			}
			catch (Exception ex)
			{
				Game.LogTrivial("Leroy Callouts | Failed to load INI settings. Reverting to defaults. Message: " + ex.ToString());
				Main.TalkKey = Keys.Y;
				Main.EndCalloutKey = Keys.End;
				Main.MenuKey = Keys.F10;
				Main.PanicButton = true;
				Main.cityOfficerCar = "POLICE3";
				Main.countyOfficerCar = "SHERIFF";
				Main.Carjacking = true;
				Main.Stalking = true;
				Main.PublicUrination = true;
				Main.AggressivePanhandling = true;
				Main.CustomerRefusingToLeave = true;
				Main.PersonVandalizingVehicle = true;
				Main.ElderlyCivilianLost = true;
				Main.UnderageWithFakeID = true;
				Main.SecurityGuardAttacked = true;
				Main.HomelessPersonOnPrivateProperty = true;
				Main.Disturbance = true;
				Main.Fighting = true;
				Main.checkForUpdatesOnStart = true;
				Main.probViolent = 20;
				Main.minSpawnDistance = 300;
				Main.maxSpawnDistance = 1650;
				Game.DisplayNotification("~r~Error loading LeroyCallouts.ini~n~~y~Reverting to defaults.");
			}
			Game.LogTrivial("Leroy Callouts  " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " has been initialized.");
		}

		
		public override void Finally()
		{
			Game.LogTrivial("Leroy Callouts | Cleaned");
		}

		
		private static void OnOnDutyStateChangedHandler(bool OnDuty)
		{
			if (OnDuty)
			{
				try
				{
					Main.RegisterCallouts();
					Game.LogTrivial("Leroy Callouts | Check for updates: " + Main.checkForUpdatesOnStart.ToString());
					Game.LogTrivial("Leroy Callouts | End key: " + Main.EndCalloutKeyString);
					Game.LogTrivial("Leroy Callouts | Talk key: " + Main.TalkKeyString);
					Game.LogTrivial("Leroy Callouts | Menu key: " + Main.MenuKeyString);
					Game.LogTrivial("Leroy Callouts | Violence probability: " + Main.probViolent.ToString());
					Game.LogTrivial("Leroy Callouts | City car: " + Main.cityOfficerCar);
					Game.LogTrivial("Leroy Callouts | County car: " + Main.countyOfficerCar);
					Game.LogTrivial("Leroy Callouts | Max: " + Main.maxSpawnDistance.ToString());
					Game.LogTrivial("Leroy Callouts | Min: " + Main.minSpawnDistance.ToString());
					Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~b~Leroy Callouts " + Assembly.GetExecutingAssembly().GetName().Version.ToString(), "~y~By Leroy100", "Successfully loaded.");
				}
				catch (Exception ex)
				{
					Game.LogTrivial("Leroy Callouts | Error going on duty: " + ex.ToString());
					Game.DisplayNotification("~r~There was a problem loading ~y~Leroy Callouts. ~n~~s~ Check to make sure you have installed all files correctly, then send a message in the \"support\" channel of my Discord. Be sure to include your RAGE log.~n~The Discord invite link can be found on the Leroy Callouts page on lspdfr.com.");
				}
			}
		}

		
		private static void RegisterCallouts()
		{
			bool aggressivePanhandling = Main.AggressivePanhandling;
			if (aggressivePanhandling)
			{
				Functions.RegisterCallout(typeof(AggressivePanhandling));
			}
			bool carjacking = Main.Carjacking;
			if (carjacking)
			{
				Functions.RegisterCallout(typeof(Carjacking));
			}
			bool customerRefusingToLeave = Main.CustomerRefusingToLeave;
			if (customerRefusingToLeave)
			{
				Functions.RegisterCallout(typeof(CustomerRefusingToLeave));
			}
			bool panicButton = Main.PanicButton;
			if (panicButton)
			{
				Functions.RegisterCallout(typeof(PanicButton));
			}
			bool publicUrination = Main.PublicUrination;
			if (publicUrination)
			{
				Functions.RegisterCallout(typeof(PublicUrination));
			}
			bool stalking = Main.Stalking;
			if (stalking)
			{
				Functions.RegisterCallout(typeof(Stalking));
			}
			bool personVandalizingVehicle = Main.PersonVandalizingVehicle;
			if (personVandalizingVehicle)
			{
				Functions.RegisterCallout(typeof(PersonVandalizingVehicle));
			}
			bool elderlyCivilianLost = Main.ElderlyCivilianLost;
			if (elderlyCivilianLost)
			{
				Functions.RegisterCallout(typeof(ElderlyCivilianLost));
			}
			bool underageWithFakeID = Main.UnderageWithFakeID;
			if (underageWithFakeID)
			{
				Functions.RegisterCallout(typeof(UnderageWithFakeID));
			}
			bool securityGuardAttacked = Main.SecurityGuardAttacked;
			if (securityGuardAttacked)
			{
				Functions.RegisterCallout(typeof(SecurityGuardAttacked));
			}
			bool homelessPersonOnPrivateProperty = Main.HomelessPersonOnPrivateProperty;
			if (homelessPersonOnPrivateProperty)
			{
				Functions.RegisterCallout(typeof(HomelessPersonOnPrivateProperty));
			}
			bool disturbance = Main.Disturbance;
			if (disturbance)
			{
				Functions.RegisterCallout(typeof(Disturbance));
			}
			bool fighting = Main.Fighting;
			if (fighting)
			{
				Functions.RegisterCallout(typeof(Fighting));
			}
		}

		
		private void CheckForUpdate()
		{
			GameFiber.Yield();
			try
			{
				WebClient updateChecker = new WebClient();
				Version currentVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());
				string updateReply = updateChecker.DownloadString("http:
				bool flag = updateReply.Length > 0;
				if (flag)
				{
					Version updateVersion = new Version(updateReply);
					bool flag2 = updateVersion > currentVersion;
					if (flag2)
					{
						Game.DisplayNotification("An update is available for ~y~Leroy Callouts~n~~s~Current Version: ~r~" + currentVersion.ToString() + "~n~~s~Latest Version: ~g~" + updateVersion.ToString());
					}
					else
					{
						Game.DisplayNotification("~y~Leroy Callouts ~g~is up to date.");
					}
				}
			}
			catch (Exception ex)
			{
				Game.DisplayNotification("Unable to check for updates to ~y~Leroy Callouts. ~s~Make sure you are connected to the internet.");
				Game.LogTrivial("Leroy Callouts | Unable to check for updates: " + ex.ToString());
			}
			GameFiber.Hibernate();
		}

		
		internal static Keys TalkKey = Keys.Y;

		
		internal static Keys EndCalloutKey = Keys.End;

		
		internal static Keys MenuKey = Keys.F10;

		
		private static string TalkKeyString;

		
		private static string EndCalloutKeyString;

		
		private static string MenuKeyString;

		
		internal static bool PanicButton = true;

		
		internal static bool Carjacking = true;

		
		internal static bool Stalking = true;

		
		internal static bool PublicUrination = true;

		
		internal static bool AggressivePanhandling = true;

		
		internal static bool CustomerRefusingToLeave = true;

		
		internal static bool PersonVandalizingVehicle = true;

		
		internal static bool ElderlyCivilianLost = true;

		
		internal static bool UnderageWithFakeID = true;

		
		internal static bool HomelessPersonOnPrivateProperty = true;

		
		internal static bool SecurityGuardAttacked = true;

		
		internal static bool Disturbance = true;

		
		internal static bool Fighting = true;

		
		internal static bool checkForUpdatesOnStart = true;

		
		internal static string countyOfficerCar = "SHERIFF";

		
		internal static string cityOfficerCar = "POLICE3";

		
		internal static int probViolent = 20;

		
		internal static int maxSpawnDistance = 1650;

		
		internal static int minSpawnDistance = 300;

		
		internal static string activeCall = "";
	}
}
