using System;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using Rage;

namespace YobbinCallouts
{
	
	internal class InvestigationHandler
	{
		
		public static void OnBeforeInvestigationStarted()
		{
			GameFiber.StartNew(delegate()
			{
				for (;;)
				{
					while (Functions.IsCalloutRunning())
					{
						GameFiber.Yield();
					}
					bool startInvestigation = InvestigationHandler.StartInvestigation;
					if (startInvestigation)
					{
						break;
					}
					while (!Functions.IsCalloutRunning())
					{
						GameFiber.Yield();
					}
				}
				Game.LogTrivial("==========YOBBINCALLOUTS: " + InvestigationHandler.InvestigationName + " Investigation Started==========");
				Game.LogTrivial("Player Should Go to Investigation Start Point While not on a Call to Start the Investigation.");
				Game.DisplayNotification("A New ~y~Investigation~w~ Has Been ~g~Started!~w~ Go to the ~b~Blip~w~ on the Map!");
				InvestigationHandler.InvestigationBlip = new Blip(InvestigationHandler.InvestigationLocation, 15f);
				InvestigationHandler.InvestigationBlip.Color = Color.LightBlue;
				InvestigationHandler.InvestigationBlip.Name = InvestigationHandler.InvestigationName;
				while (!Game.IsKeyDown(Config.InvestigationEndKey))
				{
					bool flag = InvestigationHandler.player.DistanceTo(InvestigationHandler.InvestigationLocation) <= 15f;
					if (flag)
					{
						Game.DisplayHelp("Press ~y~ " + Config.MainInteractionKey.ToString() + " ~w~to Start this ~b~Investigation.");
						bool flag2 = Game.IsKeyDown(Config.MainInteractionKey);
						if (flag2)
						{
							Game.LogTrivial("YOBBINCALLOUTS: Player Attempted to Start Investigation Within Radius.");
							bool flag3 = !Functions.IsCalloutRunning();
							if (flag3)
							{
								Game.LogTrivial("YOBBINCALLOUTS: Player Not on Call. Starting Investigation.");
								try
								{
									Functions.StartCallout(InvestigationHandler.InvestigationName);
								}
								catch
								{
								}
								Game.LogTrivial("YOBBINCALLOUTS: Player Started Investigation.");
								break;
							}
							Game.LogTrivial("YOBBINCALLOUTS: Player On Call, Should Come Back Once it is Finished..");
							Game.LogTrivial("~g~Finish~w~ your Current ~b~Callout~w~ to Start the ~y~Investigation.");
						}
					}
					GameFiber.Yield();
				}
				bool flag4 = Game.IsKeyDown(Config.InvestigationEndKey);
				if (flag4)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Player Did not Start the Investigation.");
					Game.DisplayHelp("Investigation ~r~Ended.");
					InvestigationHandler.InvestigationRunning = false;
					bool flag5 = EntityExtensions.Exists(InvestigationHandler.InvestigationBlip);
					if (flag5)
					{
						InvestigationHandler.InvestigationBlip.Delete();
					}
				}
				else
				{
					bool flag6 = EntityExtensions.Exists(InvestigationHandler.InvestigationBlip);
					if (flag6)
					{
						InvestigationHandler.InvestigationBlip.Delete();
					}
				}
			});
		}

		
		public static bool StartInvestigation = false;

		
		public static bool InvestigationRunning = false;

		
		public static Vector3 InvestigationLocation;

		
		public static Blip InvestigationBlip;

		
		public static string InvestigationName;

		
		public static Ped player = Game.LocalPlayer.Character;
	}
}
