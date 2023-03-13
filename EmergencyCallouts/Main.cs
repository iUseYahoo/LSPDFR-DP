using System;
using EmergencyCallouts.Callouts;
using EmergencyCallouts.Essential;
using LSPD_First_Response.Mod.API;
using Rage;

namespace EmergencyCallouts
{
	
	public class Main : Plugin
	{
		
		public override void Initialize()
		{
			Functions.OnOnDutyStateChanged += new Functions.OnDutyStateChangedEventHandler(Main.OnOnDutyStateChangedHandler);
			Game.LogTrivial("[Emergency Callouts]: Successfully Loaded v" + Project.LocalVersion);
		}

		
		public override void Finally()
		{
			Game.LogTrivial("[Emergency Callouts]: Successfully Unloaded");
		}

		
		private static void OnOnDutyStateChangedHandler(bool OnDuty)
		{
			if (OnDuty)
			{
				Settings.Initialize();
				Main.RegisterCallouts();
				UpdateChecker.UpdateAvailable();
				if (Functions.GetPlayerRadioAction() == null)
				{
					Game.LogTrivial("[Emergency Callouts]: User didn't set a radio action");
					Functions.SetPlayerRadioAction(4);
					Game.LogTrivial("[Emergency Callouts]: Set a radio action for user");
				}
			}
		}

		
		private static void RegisterCallouts()
		{
			if (Settings.PublicIntoxication)
			{
				Functions.RegisterCallout(typeof(PublicIntoxication));
			}
			if (Settings.Trespassing)
			{
				Functions.RegisterCallout(typeof(Trespassing));
			}
			if (Settings.DomesticViolence)
			{
				Functions.RegisterCallout(typeof(DomesticViolence));
			}
			if (Settings.Burglary)
			{
				Functions.RegisterCallout(typeof(Burglary));
			}
			if (Settings.SuspiciousActivity)
			{
				Functions.RegisterCallout(typeof(SuspiciousActivity));
			}
			Game.LogTrivial("[Emergency Callouts]: Registered 4 callouts");
		}
	}
}
