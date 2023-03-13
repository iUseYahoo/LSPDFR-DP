using System;
using System.Reflection;
using LSPD_First_Response.Mod.API;
using ManiacCallouts.Callouts;
using ManiacCallouts.Event;
using Rage;

namespace ManiacCallouts
{
	
	public class Main : Plugin
	{
		
		public override void Initialize()
		{
			Functions.OnOnDutyStateChanged += new Functions.OnDutyStateChangedEventHandler(Main.OnOnDutyStateChangedHandler);
			Game.LogTrivial("Plugin ManiacCallouts " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " by Sputtman has been initialised.");
			Game.LogTrivial("Go on duty to fully load ManiacCallouts");
			Settings.LoadSettings();
			AppDomain.CurrentDomain.AssemblyResolve += Main.LSPDFRResolveEventHandler;
		}

		
		public override void Finally()
		{
			Game.LogTrivial("ManiacCallouts has been cleaned up.");
		}

		
		private static void OnOnDutyStateChangedHandler(bool OnDuty)
		{
			if (OnDuty)
			{
				Main.RegisterCallouts();
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "ManiacCallouts", "~b~" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~r~by Sputtman", "~b~Is now successfully loaded!");
				PluginCheck.Updatecheck();
				bool trafficEventsEnable = Settings.TrafficEventsEnable;
				if (trafficEventsEnable)
				{
					EventPoolTraffic.EventsController();
				}
				bool pedEventsEnable = Settings.PedEventsEnable;
				if (pedEventsEnable)
				{
					EventPoolPed.EventsController();
				}
			}
		}

		
		private static void RegisterCallouts()
		{
			bool storeCity = Settings.StoreCity;
			if (storeCity)
			{
				Functions.RegisterCallout(typeof(StoreCity));
			}
			bool storeSandyPaleto = Settings.StoreSandyPaleto;
			if (storeSandyPaleto)
			{
				Functions.RegisterCallout(typeof(StoreSandyPaleto));
			}
			bool gangattack = Settings.Gangattack;
			if (gangattack)
			{
				Functions.RegisterCallout(typeof(Gangattack));
			}
			bool moneyTruck = Settings.MoneyTruck;
			if (moneyTruck)
			{
				Functions.RegisterCallout(typeof(MoneyTruck));
			}
			bool backupTrafficStop = Settings.BackupTrafficStop;
			if (backupTrafficStop)
			{
				Functions.RegisterCallout(typeof(BackupTrafficStop));
			}
			bool suspiciousActivity = Settings.SuspiciousActivity;
			if (suspiciousActivity)
			{
				Functions.RegisterCallout(typeof(SuspiciousActivity));
			}
			bool showingFirearm = Settings.ShowingFirearm;
			if (showingFirearm)
			{
				Functions.RegisterCallout(typeof(ShowingFirearm));
			}
			bool bankSilentAlarm = Settings.BankSilentAlarm;
			if (bankSilentAlarm)
			{
				Functions.RegisterCallout(typeof(Bank));
			}
			bool illegalStreetRace = Settings.IllegalStreetRace;
			if (illegalStreetRace)
			{
				Functions.RegisterCallout(typeof(StreetRace));
			}
			bool officerNoResponse = Settings.OfficerNoResponse;
			if (officerNoResponse)
			{
				Functions.RegisterCallout(typeof(OfficerNoResponse));
			}
		}

		
		public static Assembly LSPDFRResolveEventHandler(object sender, ResolveEventArgs args)
		{
			foreach (Assembly assembly in Functions.GetAllUserPlugins())
			{
				bool flag = args.Name.ToLower().Contains(assembly.GetName().Name.ToLower());
				if (flag)
				{
					return assembly;
				}
			}
			return null;
		}

		
		public static bool IsLSPDFRPluginRunning(string Plugin, Version minversion = null)
		{
			foreach (Assembly assembly in Functions.GetAllUserPlugins())
			{
				AssemblyName an = assembly.GetName();
				bool flag = an.Name.ToLower() == Plugin.ToLower();
				if (flag)
				{
					bool flag2 = minversion == null || an.Version.CompareTo(minversion) >= 0;
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
