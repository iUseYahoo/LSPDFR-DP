using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Explosion", 2)]
	public class Explosion : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Explosion Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 0);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is " + this.MainScenario.ToString());
			bool flag = this.MainScenario >= 0;
			if (flag)
			{
				this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
				Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
				this.MainSpawnPoint = World.GetNextPositionOnStreet(this.player.Position.Around(600f));
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 75f);
			base.AddMinimumDistanceCheck(25f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("CITIZENS_REPORT YC_EXPLOSION");
			base.CalloutMessage = "Explosion";
			base.CalloutPosition = this.MainSpawnPoint;
			base.CalloutAdvisory = "Citizens Report an ~r~Explosion~w~. Several Reported ~o~Injured.";
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Explosion Callout Accepted by User.");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 3", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~r~Code 3.");
			}
			Random monke = new Random();
			int victims = monke.Next(1, 5);
			Ped[] Randos = World.GetAllPeds();
			for (int i = 0; i < 25; i++)
			{
				GameFiber.Yield();
				bool flag = EntityExtensions.Exists(Randos[i]);
				if (flag)
				{
					bool flag2 = Randos[i] != this.player && Randos[i] != this.Suspect;
					if (flag2)
					{
						Randos[i] = this.Victims[i];
						this.Victims[i].IsPersistent = true;
						this.Suspect.RelationshipGroup.SetRelationshipWith(this.Victims[i].RelationshipGroup, 5);
						bool flag3 = i <= victims;
						if (flag3)
						{
							this.Victims[i].Position = this.MainSpawnPoint.Around(5f);
							this.Victims[i].BlockPermanentEvents = true;
						}
						else
						{
							bool flag4 = i <= victims + 1;
							if (flag4)
							{
								this.Witness = this.Victims[i];
								this.Witness.IsPersistent = true;
								this.Witness.BlockPermanentEvents = true;
							}
							else
							{
								this.Victims[i].Tasks.ReactAndFlee(this.Suspect);
							}
						}
					}
				}
			}
			this.Suspects = new string[]
			{
				"A_M_Y_SouCent_01",
				"A_M_Y_StWhi_01",
				"A_M_Y_StBla_01",
				"A_M_Y_Downtown_01",
				"A_M_Y_BevHills_01",
				"G_M_Y_MexGang_01",
				"G_M_Y_MexGoon_01",
				"G_M_Y_StrPunk_01"
			};
			bool flag5 = this.MainScenario == 0;
			if (flag5)
			{
				Random r2 = new Random();
				int SuspectModel = r2.Next(0, this.Suspects.Length);
				if (Explosion.<>o__16.<>p__0 == null)
				{
					Explosion.<>o__16.<>p__0 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(Explosion), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				Explosion.<>o__16.<>p__0.Target(Explosion.<>o__16.<>p__0, NativeFunction.Natives, World.GetNextPositionOnStreet(this.MainSpawnPoint), 0, ref outPosition);
				this.Suspect = new Ped(this.Suspects[SuspectModel], outPosition, 69f);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
			}
			this.AreaBlip = new Blip(this.MainSpawnPoint, 25f);
			this.AreaBlip.Color = Color.Orange;
			this.AreaBlip.Alpha = 0.67f;
			this.AreaBlip.IsRouteEnabled = true;
			this.AreaBlip.Name = "Scene";
			bool flag6 = !this.CalloutRunning;
			if (flag6)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Bar Fight Callout Not Accepted by User.");
			base.OnCalloutNotAccepted();
		}

		
		private void Callout()
		{
			this.CalloutRunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.CalloutRunning)
					{
						while (this.player.DistanceTo(this.Suspect) >= 25f && !Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(Config.CalloutEndKey);
						if (flag)
						{
							EndCalloutHandler.CalloutForcedEnd = true;
						}
						else
						{
							Game.LogTrivial("YOBBINCALLOUTS: Player has arrived on scene.");
						}
					}
					Game.LogTrivial("YOBBINCALLOUTS: Callout Finished, Ending...");
					EndCalloutHandler.EndCallout();
					this.End();
				}
				catch (Exception e)
				{
					bool calloutRunning = this.CalloutRunning;
					if (calloutRunning)
					{
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
						Game.LogTrivial("IN: " + ((this != null) ? this.ToString() : null));
						string error = e.ToString();
						Game.LogTrivial("ERROR: " + error);
						Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Check Your ~g~Log File.~w~ Sorry for the Inconvenience!");
						Game.DisplayNotification("Error: ~r~" + error);
						Game.LogTrivial("If You Believe this is a Bug, Please Report it on my Discord Server. Thanks!");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
					}
					else
					{
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - CALLOUT NO LONGER RUNNING==========");
						string error2 = e.ToString();
						Game.LogTrivial("ERROR: " + error2);
						Game.LogTrivial("No Need to Report This Error if it Did not Result in an LSPDFR Crash.");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - CALLOUT NO LONGER RUNNING==========");
					}
					this.End();
				}
			});
		}

		
		public override void End()
		{
			base.End();
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplayNotification("~g~Code 4~w~, return to patrol.");
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS WE_ARE_CODE_4");
			}
			this.CalloutRunning = false;
			bool flag = EntityExtensions.Exists(this.SuspectBlip);
			if (flag)
			{
				this.SuspectBlip.Delete();
			}
			bool flag2 = EntityExtensions.Exists(this.Suspect2Blip);
			if (flag2)
			{
				this.Suspect2Blip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.VictimBlip);
			if (flag3)
			{
				this.VictimBlip.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.AreaBlip);
			if (flag4)
			{
				this.AreaBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Bar Fight Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private Vector3 MainSpawnPoint;

		
		private Ped Suspect;

		
		private Ped Suspect2;

		
		private Ped Witness;

		
		private Ped[] Victims;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private string[] Suspects;

		
		private Blip SuspectBlip;

		
		private Blip Suspect2Blip;

		
		private Blip VictimBlip;

		
		private Blip AreaBlip;

		
		private int MainScenario;

		
		private string Zone;

		
		private bool CalloutRunning;

		
		private LHandle SuspectPursuit;
	}
}
