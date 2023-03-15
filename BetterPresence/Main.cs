using System;
using LSPD_First_Response.Mod.API;
using Rage;

namespace BetterPresence
{
	
	public class Main : Plugin
	{
		
		public override void Initialize()
		{
			Functions.OnOnDutyStateChanged += new Functions.OnDutyStateChangedEventHandler(Main.OnOnDutyStateChangedHandler);
			Game.LogVerbose("BetterPresence v2.0.0 loaded. Go on duty to fully load.");
			Settings.UpdateSettings();
		}

		
		public override void Finally()
		{
			Presence.StopLoop();
		}

		
		public static void OnOnDutyStateChangedHandler(bool onDuty)
		{
			if (onDuty)
			{
				GameFiber.StartNew(delegate()
				{
					Presence.StartLoop();
					Game.LogVerbose("BetterPresence 2.0.0 enabled.");
				});
				return;
			}
			Presence.StopLoop();
			Game.LogVerbose("BetterPresence 2.0.0 disabled.");
		}
	}
}
