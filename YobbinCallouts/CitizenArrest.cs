using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Citizen Arrest", 2)]
	public class CitizenArrest : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Citizen Arrest Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 0);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is " + this.MainScenario.ToString());
			bool flag = this.MainScenario == 0;
			if (flag)
			{
				this.Crime = "Assault.";
			}
			else
			{
				bool flag2 = this.MainScenario == 1;
				if (flag2)
				{
					this.Crime = "Theft.";
				}
				else
				{
					this.Crime = "Discharging a Firearm.";
				}
			}
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).RealAreaName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			this.MainSpawnPoint = World.GetNextPositionOnStreet(this.player.Position.Around(569f));
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 75f);
			base.AddMinimumDistanceCheck(25f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("CITIZENS_REPORT CRIME_DISTURBING_THE_PEACE_01");
			base.CalloutMessage = "Citizen's Arrest";
			base.CalloutPosition = this.MainSpawnPoint;
			base.CalloutAdvisory = "Caller is a Citizen Who Has Arrested a Suspect for " + this.Crime;
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			try
			{
				Game.LogTrivial("YOBBINCALLOUTS: Citizen Arrest Callout Accepted by User.");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 03", "");
				}
				else
				{
					Game.DisplayNotification("Respond ~r~Code 3.");
				}
				if (CitizenArrest.<>o__22.<>p__0 == null)
				{
					CitizenArrest.<>o__22.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(CitizenArrest), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Vector3 nodePosition;
				float heading;
				CitizenArrest.<>o__22.<>p__0.Target(CitizenArrest.<>o__22.<>p__0, NativeFunction.Natives, this.MainSpawnPoint, ref nodePosition, ref heading, 1, 3f, 0);
				this.SuspectModels = new string[]
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
				Random r2 = new Random();
				int SuspectModel = r2.Next(0, this.SuspectModels.Length);
				this.Suspect = new Ped(nodePosition, heading);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				Functions.SetPedAsArrested(this.Suspect, true, false);
				Game.LogTrivial("YOBBINCALLOUTS: Suspect Spawned");
				this.Citizen = new Citizen(this.Suspect.GetOffsetPositionFront(2f));
				this.Citizen.IsPersistent = true;
				this.Citizen.BlockPermanentEvents = true;
				this.Citizen.Heading = this.Suspect.Heading - 180f;
				Vector3 victimspawnpoint = World.GetNextPositionOnStreet(this.Suspect.Position.Around(10f));
				if (CitizenArrest.<>o__22.<>p__1 == null)
				{
					CitizenArrest.<>o__22.<>p__1 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(CitizenArrest), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				CitizenArrest.<>o__22.<>p__1.Target(CitizenArrest.<>o__22.<>p__1, NativeFunction.Natives, victimspawnpoint, 0, ref outPosition);
				this.Victim = new Ped(outPosition);
				this.Victim.IsPersistent = true;
				this.Victim.BlockPermanentEvents = true;
				this.Victim.Tasks.Cower(-1);
				this.AreaBlip = new Blip(this.Suspect.Position, 25f);
				this.AreaBlip.Color = Color.Yellow;
				this.AreaBlip.Alpha = 0.67f;
				this.AreaBlip.IsRouteEnabled = true;
				this.AreaBlip.Name = "Callout Location";
				bool flag = !EntityExtensions.Exists(this.Suspect);
				if (flag)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Suspect no longer valid, aborting...");
					return false;
				}
			}
			catch (Exception e)
			{
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT ON CALLOUT INITIALIZATION==========");
				Game.LogTrivial("IN: " + ((this != null) ? this.ToString() : null));
				string error = e.ToString();
				Game.LogTrivial("ERROR: " + error);
				Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Check Your ~g~Log File.~w~ Sorry for the Inconvenience!");
				Game.DisplayNotification("Error: ~r~" + error);
				Game.LogTrivial("If You Believe this is a Bug, Please Report it on my Discord Server. Thanks!");
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT ON CALLOUT INITIALIZATION==========");
			}
			bool flag2 = !this.CalloutRunning;
			if (flag2)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Citizen Arrest Callout Not Accepted by User.");
			base.OnCalloutNotAccepted();
		}

		
		private void Callout()
		{
			this.CalloutRunning = true;
			GameFiber.StartNew(delegate()
			{
				if (this.CalloutRunning)
				{
					while (this.player.DistanceTo(this.Citizen) >= 25f && !Game.IsKeyDown(Config.CalloutEndKey))
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
						Game.LogTrivial("YOBBINCALLOUTS: Player Arrived on Scene.");
						this.AreaBlip.Delete();
						Game.DisplayHelp("Speak With the ~p~Arresting Citizen.");
						this.CitizenBlip = this.Citizen.AttachBlip();
						this.CitizenBlip.Scale = 0.7f;
						this.CitizenBlip.Color = Color.Purple;
						if (CitizenArrest.<>o__24.<>p__0 == null)
						{
							CitizenArrest.<>o__24.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(CitizenArrest), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						CitizenArrest.<>o__24.<>p__0.Target(CitizenArrest.<>o__24.<>p__0, NativeFunction.Natives, this.Citizen, this.player, -1);
						CallHandler.AssignBlip(this.Suspect, Color.Red, 1f, "", false, 1f);
						bool flag2 = this.MainScenario == 0;
						if (flag2)
						{
							this.Assault();
						}
					}
				}
				Game.LogTrivial("YOBBINCALLOUTS: Callout Finished, Ending...");
				EndCalloutHandler.EndCallout();
				this.End();
			});
		}

		
		public override void End()
		{
			base.End();
			this.CalloutRunning = false;
			Game.DisplayNotification("~g~Code 4~w~, return to patrol.");
			bool flag = EntityExtensions.Exists(this.Victim);
			if (flag)
			{
				this.Victim.Dismiss();
			}
			bool flag2 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag2)
			{
				this.SuspectBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.VictimBlip);
			if (flag3)
			{
				this.VictimBlip.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.CitizenBlip);
			if (flag4)
			{
				this.CitizenBlip.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this.Citizen);
			if (flag5)
			{
				this.Citizen.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag6)
			{
				this.SuspectBlip.Delete();
			}
			bool flag7 = EntityExtensions.Exists(this.AreaBlip);
			if (flag7)
			{
				this.AreaBlip.Delete();
			}
			Functions.PlayScannerAudio("ATTENTION_ALL_UNITS WE_ARE_CODE_4");
			Game.LogTrivial("YOBBINCALLOUTS: Citizen Arrest Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void GunPoint()
		{
		}

		
		private void Assault()
		{
			CallHandler.IdleAction(this.Citizen, true);
			while (this.player.DistanceTo(this.Citizen) >= 5f)
			{
				GameFiber.Wait(0);
			}
			bool displayHelp = Config.DisplayHelp;
			if (displayHelp)
			{
				Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~p~Arresting Citizen.");
			}
			CallHandler.Dialogue(this.AssaultOpening, this.Citizen, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
			this.Citizen.Tasks.Clear();
			CallHandler.IdleAction(this.Citizen, true);
			this.VictimBlip = this.Victim.AttachBlip();
			this.VictimBlip.Scale = 0.69f;
			this.VictimBlip.IsFriendly = true;
			Game.DisplayHelp("Speak With the ~b~Victim.");
			if (CitizenArrest.<>o__28.<>p__0 == null)
			{
				CitizenArrest.<>o__28.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(CitizenArrest), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			CitizenArrest.<>o__28.<>p__0.Target(CitizenArrest.<>o__28.<>p__0, NativeFunction.Natives, this.Victim, this.player, -1);
			while (this.player.DistanceTo(this.Victim) >= 5f)
			{
				GameFiber.Wait(0);
			}
			bool displayHelp2 = Config.DisplayHelp;
			if (displayHelp2)
			{
				Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~b~Victim.");
			}
			CallHandler.Dialogue(this.AssaultInvestigation, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
			this.Victim.Dismiss();
			bool flag = EntityExtensions.Exists(this.VictimBlip);
			if (flag)
			{
				this.VictimBlip.Delete();
			}
			this.Citizen.Dismiss();
			bool flag2 = EntityExtensions.Exists(this.CitizenBlip);
			if (flag2)
			{
				this.CitizenBlip.Delete();
			}
			GameFiber.Wait(2000);
			Game.DisplayHelp("Deal With the ~r~Suspect. ~w~Press ~y~" + Config.CalloutEndKey.ToString() + " ~w~When Finished.");
			Functions.SetPedAsArrested(this.Suspect, true, true);
			while (!Game.IsKeyDown(Config.CalloutEndKey))
			{
				GameFiber.Wait(0);
			}
			bool flag3 = EntityExtensions.Exists(this.Suspect);
			if (flag3)
			{
				bool isAlive = this.Suspect.IsAlive;
				if (isAlive)
				{
					Game.DisplayNotification("Dispatch, We Have ~b~Arrested~w~ the Suspect.");
				}
			}
		}

		
		private Vector3 MainSpawnPoint;

		
		private Ped Suspect;

		
		private Ped Citizen;

		
		private Ped Victim;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private Blip SuspectBlip;

		
		private Blip CitizenBlip;

		
		private Blip VictimBlip;

		
		private Blip AreaBlip;

		
		private int MainScenario;

		
		private string Zone;

		
		private string Crime;

		
		private string[] SuspectModels;

		
		private string[] CitizenModels;

		
		private string[] VictimModels;

		
		private bool CalloutRunning;

		
		private LHandle SuspectPursuit;

		
		private readonly List<string> AssaultOpening = new List<string>
		{
			"~p~Citizen:~w~ Hey Officer, I Just Performed a Citizen's Arrest on This Guy Right Here.",
			"~g~You:~w~ Alright, Can You Tell Me What Happened?",
			"~p~Citizen:~w~ Yeah, I was Just Walking Down the Street When I Noticed These Two Getting into an Argument.",
			"~p~Citizen:~w~ Just as I was Walking Over There, He Started Punching Them. I Seperated the Two and Arrested This Guy.",
			"~g~You:~w~ Alright, Let Me Go Speak With the Victim."
		};

		
		private readonly List<string> TheftOpening = new List<string>
		{
			"~p~Citizen:~w~ Hello Officer, I Just Performed a Citizen's Arrest on This Guy Right Here.",
			"~g~You:~w~ Can You Tell Me What Happened?",
			"~p~Citizen:~w~ I was Just Walking Along When I Saw This Guy Running Down the Street.",
			"~p~Citizen:~w~ The Victim Was Yelling That He Stole Thir Wallet so I Stopped and Arrested Him.",
			"~g~You:~w~ Alright, Let Me Go Speak With the Victim."
		};

		
		private readonly List<string> AssaultInvestigation = new List<string>
		{
			"~g~You:~w~ Hello, Can You Tell Me What Happened Here?",
			"~b~Victim:~w~ This Crazy Guy Bumped into me While We Were Waling Down the Street.",
			"~b~Victim:~w~ I Told Him to Watch it, and He Immediately Took Offence and Started Shoving Me.",
			"~b~Victim:~w~ I Tried to Walk Away to Diffuse the Situation, and then He Started Punching Me.",
			"~b~Victim:~w~ Then This Person Stepped in and Tackled the Guy to the Ground and Tied Him Up Before You Guys Got Here.",
			"~g~You:~w~ Do You Need Medical Attention?",
			"~b~Victim:~w~ I'm Fine, Just Glad This Guy Got Subdued.",
			"~g~You:~w~ Alright, I'll Process Him Then. You're Free to Go if You Don't Need Anything Else."
		};

		
		private readonly List<string> TheftInvestigation = new List<string>
		{
			"~p~Citizen:~w~ Hello Officer, I Just Performed a Citizen's Arrest on This Guy Right Here.",
			"~g~You:~w~ Can You Tell Me What Happened?",
			"~p~Citizen:~w~ I was Just Walking Along When I Saw This Guy Running Down the Street.",
			"~p~Citizen:~w~ The Victim Was Yelling That He Stole Thir Wallet so I Stopped and Arrested Him.",
			"~g~You:~w~ Alright, Let Me Go Speak With the Victim."
		};
	}
}
