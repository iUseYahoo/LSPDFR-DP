using System;
using System.Reflection;
using DangerouseCallouts.Callouts;
using LSPD_First_Response.Mod.API;
using Rage;

namespace DangerouseCallouts
{
	
	public class Main : Plugin
	{
		
		public override void Initialize()
		{
			Functions.OnOnDutyStateChanged += new Functions.OnDutyStateChangedEventHandler(Main.OnOnDutyStatechangedHandler);
			Game.LogTrivial("Plugin Dangerouse Callouts has loaded SuccessFully Made By Leo™ " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "by Author has been initialised");
			Game.LogTrivial("Go on duty to fully load Dangerouse Callouts!");
			AppDomain.CurrentDomain.AssemblyResolve += Main.LSPDFRResolveEventHandler;
		}

		
		public override void Finally()
		{
			Game.LogTrivial("Dangerouse Callouts has been cleaned up!.");
		}

		
		private static void OnOnDutyStatechangedHandler(bool OnDuty)
		{
			if (OnDuty)
			{
				Main.RegisterCallouts();
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "DangerousCallouts", "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~o~by Leo™", "~b~successfully loaded!");
			}
		}

		
		private static void RegisterCallouts()
		{
			Functions.RegisterCallout(typeof(HighSpeedPursuit));
			Functions.RegisterCallout(typeof(Shooting));
			Functions.RegisterCallout(typeof(Stabbing));
			Functions.RegisterCallout(typeof(Juggernaut));
			Functions.RegisterCallout(typeof(EMSUnderFire));
			Functions.RegisterCallout(typeof(IntoxicatedPerson));
			Functions.RegisterCallout(typeof(OfficerDown));
			Functions.RegisterCallout(typeof(StoreRobbery));
			Functions.RegisterCallout(typeof(Fraud));
			Functions.RegisterCallout(typeof(Homicide));
			Functions.RegisterCallout(typeof(FistFight));
			Functions.RegisterCallout(typeof(DrunkDriver));
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

		
		public static bool ISLSPDFRunning(string Plugin, Version minversion = null)
		{
			foreach (Assembly assembly in Functions.GetAllUserPlugins())
			{
				AssemblyName name = assembly.GetName();
				bool flag = name.Name.ToLower() == Plugin.ToLower();
				if (flag)
				{
					bool flag2 = minversion == null || name.Version.CompareTo(minversion) >= 0;
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
