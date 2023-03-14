using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Road Rage", 2)]
	public class RoadRage : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Road Rage Callout Start==========");
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			Random chez = new Random();
			int boom = chez.Next(0, 3);
			this.MainScenario = boom;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is " + this.MainScenario.ToString());
			Vector3 Spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(550f));
			try
			{
				if (RoadRage.<>o__21.<>p__0 == null)
				{
					RoadRage.<>o__21.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(RoadRage), new CSharpArgumentInfo[]
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
				RoadRage.<>o__21.<>p__0.Target(RoadRage.<>o__21.<>p__0, NativeFunction.Natives, Spawn, ref nodePosition, ref heading, 1, 3f, 0);
				if (RoadRage.<>o__21.<>p__2 == null)
				{
					RoadRage.<>o__21.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(RoadRage)));
				}
				Func<CallSite, object, bool> target = RoadRage.<>o__21.<>p__2.Target;
				CallSite <>p__ = RoadRage.<>o__21.<>p__2;
				if (RoadRage.<>o__21.<>p__1 == null)
				{
					RoadRage.<>o__21.<>p__1 = CallSite<<>F{00000010}<CallSite, object, Vector3, float, Vector3, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(RoadRage), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				bool success = target(<>p__, RoadRage.<>o__21.<>p__1.Target(RoadRage.<>o__21.<>p__1, NativeFunction.Natives, Spawn, heading, ref outPosition));
				bool flag = success;
				if (!flag)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
					return false;
				}
				this.MainSpawnPoint = outPosition;
				this.VehicleHeading = heading;
			}
			catch (TargetInvocationException)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
				return false;
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 25f);
			base.AddMinimumDistanceCheck(50f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("WE_HAVE_01 CRIME_MALICIOUS_VEHICLE_DAMAGE_01");
			base.CalloutMessage = "Road Rage";
			base.CalloutPosition = this.MainSpawnPoint;
			bool flag2 = this.MainScenario == 0;
			if (flag2)
			{
				base.CalloutAdvisory = "Caller Reports a ~r~Road Rage~w~ Incident. Suspect Then Fled the Scene.";
			}
			else
			{
				base.CalloutAdvisory = "Caller Reprots a ~r~Road Rage~w~ Incident. Suspect is Damaging their Vehicle.";
			}
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Road Rage Callout Accepted by User");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 3", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~r~Code 3~w~.");
			}
			this.VictimVehicle = CallHandler.SpawnVehicle(this.MainSpawnPoint, this.VehicleHeading, true);
			Game.LogTrivial("YOBBINCALLOUTS: Victim Vehicle Spawned");
			this.VictimVehicle.IsPersistent = true;
			this.VictimVehicle.IsEngineOn = true;
			this.VictimVehicle.IsDriveable = false;
			this.Victim = this.VictimVehicle.CreateRandomDriver();
			this.Victim.IsPersistent = true;
			this.Victim.BlockPermanentEvents = true;
			this.Victim.Tasks.CruiseWithVehicle(0f);
			this.Victim.IsInvincible = true;
			this.SuspectSpawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(650f));
			this.VictimVehicle.IsDeformationEnabled = true;
			this.VictimVehicle.Deform(this.VictimVehicle.GetPositionOffset(this.VictimVehicle.GetBonePosition("door_dside_f")), 100f, 600f);
			this.Area = new Blip(this.MainSpawnPoint, 25f);
			this.Area.Color = Color.Yellow;
			this.Area.Alpha = 0.67f;
			this.Area.IsRouteEnabled = true;
			this.Area.Name = "Scene";
			bool flag = this.MainScenario >= 1;
			if (flag)
			{
				this.SuspectVehicle = CallHandler.SpawnVehicle(this.VictimVehicle.GetOffsetPositionFront(5f), this.VictimVehicle.Heading - 180f, true);
				this.SuspectVehicle.IsDeformationEnabled = true;
				this.SuspectVehicle.IsPersistent = true;
				this.Suspect = this.SuspectVehicle.CreateRandomDriver();
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.SuspectVehicle.IsDriveable = false;
				this.Suspect.Tasks.CruiseWithVehicle(0f);
				Game.LogTrivial("YOBBINCALLOUTS: Finished Spawning Suspect.");
				this.Victim.IsInvincible = true;
				this.Victim.Tasks.LeaveVehicle(256);
				bool calloutInterface2 = Main.CalloutInterface;
				if (calloutInterface2)
				{
					CalloutInterfaceHandler.SendMessage(this, "Caller reports suspect is still on scene damaging their vehicle");
				}
			}
			else
			{
				bool calloutInterface3 = Main.CalloutInterface;
				if (calloutInterface3)
				{
					CalloutInterfaceHandler.SendMessage(this, "Caller reports suspect fled the scene after damaging their vehicle");
				}
			}
			Game.DisplayNotification("Go to the ~y~Scene~w~ to Speak with the ~b~Caller.");
			bool flag2 = !this.CalloutRunning;
			if (flag2)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Road Rage Callout Not Accepted by User.");
			Functions.PlayScannerAudio("OTHER_UNIT_TAKING_CALL_01");
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
						bool flag = this.MainScenario == 0;
						if (flag)
						{
							while (this.player.DistanceTo(this.MainSpawnPoint) >= 25f && !Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
						}
						else
						{
							while (this.player.DistanceTo(this.MainSpawnPoint) >= 50f && !Game.IsKeyDown(Config.CalloutEndKey))
							{
								this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256).WaitForCompletion();
							}
							GameFiber.Wait(0);
						}
						bool flag2 = Game.IsKeyDown(Config.CalloutEndKey);
						if (flag2)
						{
							EndCalloutHandler.CalloutForcedEnd = true;
						}
						else
						{
							bool flag3 = this.MainScenario == 0;
							if (flag3)
							{
								this.CallerFirst();
								this.Search();
								this.Discovered();
							}
							else
							{
								bool flag4 = this.MainScenario >= 1;
								if (flag4)
								{
									this.SuspectFirst();
								}
							}
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
			bool flag = EntityExtensions.Exists(this.Area);
			if (flag)
			{
				this.Area.Delete();
			}
			bool flag2 = EntityExtensions.Exists(this.VictimBlip);
			if (flag2)
			{
				this.VictimBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag3)
			{
				this.SuspectBlip.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.SuspectArea);
			if (flag4)
			{
				this.SuspectArea.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this.Victim);
			if (flag5)
			{
				this.Victim.Dismiss();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Road Rage Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void SuspectFirst()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				this.Area.Delete();
				this.VictimBlip = this.Victim.AttachBlip();
				this.VictimBlip.Scale = 0.75f;
				this.VictimBlip.IsFriendly = true;
				this.VictimBlip.Name = "Caller";
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.Scale = 0.75f;
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Name = "Suspect";
				Random sewil = new Random();
				int WeaponChooser = sewil.Next(0, 3);
				bool flag = WeaponChooser == 0;
				if (flag)
				{
					this.Suspect.Inventory.GiveNewWeapon("WEAPON_CROWBAR", -1, true);
				}
				bool flag2 = WeaponChooser == 1;
				if (flag2)
				{
					this.Suspect.Inventory.GiveNewWeapon("WEAPON_BAT", -1, true);
				}
				bool flag3 = WeaponChooser == 2;
				if (flag3)
				{
					this.Suspect.Inventory.GiveNewWeapon("WEAPON_POOLCUE", -1, true);
				}
				this.Suspect.Tasks.PlayAnimation("missheist_agency3aig_13", "wait_loops_player0", -1f, 1);
				Game.DisplaySubtitle("~b~Driver:~w~ Officer! That Person Right There Just Damaged my Vehicle!!", 5000);
				this.Victim.Tasks.Cower(-1);
				Game.DisplayHelp("Apprehend the ~r~Suspect.");
				bool flag4 = this.MainScenario == 2;
				if (flag4)
				{
					this.Suspect.Tasks.EnterVehicle(this.SuspectVehicle, -1, -1, 4.2f).WaitForCompletion();
					this.Discovered();
				}
				else
				{
					Game.DisplayHelp("Arrest the ~r~Suspect.");
					while (EntityExtensions.Exists(this.Suspect))
					{
						GameFiber.Yield();
						bool flag5 = this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect) || !EntityExtensions.Exists(this.Suspect);
						if (flag5)
						{
							break;
						}
					}
					bool flag6 = EntityExtensions.Exists(this.Suspect);
					if (flag6)
					{
						bool flag7 = Functions.IsPedArrested(this.Suspect);
						if (flag7)
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, Suspect is Under ~g~Arrest~w~ for ~b~Road Rage.");
						}
						else
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, Suspect Was ~r~Killed.");
						}
					}
					this.SuspectBlip.Delete();
					GameFiber.Wait(2000);
					Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
					GameFiber.Wait(2000);
					this.CallerEnd();
				}
			}
		}

		
		private void CallerFirst()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				this.Area.Delete();
				this.VictimBlip = this.Victim.AttachBlip();
				this.VictimBlip.Scale = 0.75f;
				this.VictimBlip.IsFriendly = true;
				this.VictimBlip.Name = "Caller";
				Game.DisplayHelp("Speak with the ~b~Caller.");
				this.Victim.Tasks.LeaveVehicle(256).WaitForCompletion();
				if (RoadRage.<>o__28.<>p__0 == null)
				{
					RoadRage.<>o__28.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(RoadRage), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				RoadRage.<>o__28.<>p__0.Target(RoadRage.<>o__28.<>p__0, NativeFunction.Natives, this.Victim, this.player, -1);
				while (this.player.DistanceTo(this.Victim) >= 5f)
				{
					GameFiber.Wait(0);
				}
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~b~Driver.");
				}
				CallHandler.Dialogue(this.OpeningDialogue1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				this.SuspectVehicle = CallHandler.SpawnVehicle(this.SuspectSpawnPoint, 69f, true);
				this.Colours = new Color[]
				{
					Color.White,
					Color.Black,
					Color.Gray,
					Color.Silver,
					Color.Red,
					Color.Blue,
					Color.Teal,
					Color.Beige
				};
				Random r3 = new Random();
				int Colour = r3.Next(0, this.Colours.Length);
				this.SuspectVehicle.PrimaryColor = this.Colours[Colour];
				Game.LogTrivial("YOBBINCALLOUTS: Suspect Vehicle Spawned");
				this.SuspectVehicle.IsPersistent = true;
				this.SuspectVehicle.IsEngineOn = true;
				this.SuspectVehicle.IsDriveable = false;
				this.SuspectVehicle.IsVisible = false;
				this.SuspectVehicle.IsDeformationEnabled = true;
				bool flag = this.SuspectVehicle.HasBone("door_pside_r");
				if (flag)
				{
					this.SuspectVehicle.Deform(this.SuspectVehicle.GetPositionOffset(this.SuspectVehicle.GetBonePosition("door_pside_r")), 100f, 500f);
				}
				this.Suspect = this.SuspectVehicle.CreateRandomDriver();
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.Suspect.Tasks.CruiseWithVehicle(0f);
				this.Suspect.IsInvincible = true;
				this.Victim.Tasks.PlayAnimation("missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				string VehicleColour = this.Colours[Colour].Name;
				Game.DisplaySubtitle(string.Concat(new string[]
				{
					"~b~Caller:~w~ The Vehicle was a ~b~",
					VehicleColour,
					"-Colored ~r~",
					this.SuspectVehicle.Model.Name,
					"."
				}), 4000);
				GameFiber.Wait(4000);
				if (RoadRage.<>o__28.<>p__1 == null)
				{
					RoadRage.<>o__28.<>p__1 = CallSite<Action<CallSite, object, Ped, Vector3, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_COORD", null, typeof(RoadRage), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				RoadRage.<>o__28.<>p__1.Target(RoadRage.<>o__28.<>p__1, NativeFunction.Natives, this.Victim, this.SuspectSpawnPoint, 1500);
				Game.DisplaySubtitle("~b~Caller:~w~ They Went That Way!", 2500);
				GameFiber.Wait(2000);
				this.Victim.Tasks.ClearImmediately();
				this.Victim.Tasks.PlayAnimation("gestures@f@standing@casual", "gesture_point", 1f, 1).WaitForCompletion(1500);
				GameFiber.Wait(1500);
				this.Victim.Tasks.ClearImmediately();
				Game.DisplaySubtitle("~g~You:~w~ Alright, I'll Start the Search. Thanks!", 3000);
				GameFiber.Wait(3500);
				this.Victim.Dismiss();
				this.VictimBlip.Delete();
			}
		}

		
		private void Search()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplayHelp("Start ~o~Searching~w~ for the ~r~Suspect.");
				this.SuspectArea = new Blip(this.Suspect.Position.Around(15f), 250f);
				this.SuspectArea.Color = Color.Orange;
				this.SuspectArea.Alpha = 0.5f;
				this.SuspectArea.IsRouteEnabled = true;
				GameFiber.Wait(1500);
				while (this.player.DistanceTo(this.Suspect) >= 150f)
				{
					GameFiber.Wait(0);
				}
				this.SuspectVehicle.IsDriveable = true;
				this.SuspectVehicle.IsVisible = true;
				this.Suspect.Tasks.CruiseWithVehicle(this.SuspectVehicle, 20f, 4);
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01");
				GameFiber.Wait(1000);
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Caller spotted the suspect driving recklessly, updating map.");
				}
				else
				{
					Game.DisplayNotification("~b~Update:~w~ A Caller Has ~y~Spotted~w~ the ~r~Suspect~w~ Driving Recklessly. ~g~Updating Map.");
				}
				GameFiber.Wait(1000);
				bool flag = EntityExtensions.Exists(this.SuspectArea);
				if (flag)
				{
					this.SuspectArea.Delete();
				}
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				GameFiber.Wait(1500);
				Game.DisplaySubtitle("Dispatch, We Have Located the ~r~Suspect!");
				GameFiber.Wait(1500);
				Game.DisplayHelp("Perform a ~o~Traffic Stop~w~ on the ~r~Suspect.");
				while (!Functions.IsPlayerPerformingPullover() && this.Suspect.IsAlive)
				{
					GameFiber.Wait(0);
				}
			}
		}

		
		private void Discovered()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Random r = new Random();
				int SuspectAction = r.Next(0, 0);
				bool flag = SuspectAction == 0;
				if (flag)
				{
					CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
					{
						this.Suspect
					});
				}
			}
		}

		
		private void CallerEnd()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplayHelp("Speak With the ~b~Victim ~w~Once You're Ready.");
				while (this.player.DistanceTo(this.Victim) >= 5f)
				{
					GameFiber.Wait(0);
				}
				Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~b~Driver.");
				CallHandler.Dialogue(this.OpeningDialogue2, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				this.Victim.Tasks.ClearImmediately();
				this.Victim.Dismiss();
			}
		}

		
		private Vector3 MainSpawnPoint;

		
		private Vector3 SuspectSpawnPoint;

		
		private Blip Area;

		
		private Blip VictimBlip;

		
		private Blip SuspectBlip;

		
		private Blip SuspectArea;

		
		private Vehicle VictimVehicle;

		
		private Vehicle SuspectVehicle;

		
		private Ped Victim;

		
		private Ped Suspect;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private float VehicleHeading;

		
		private int MainScenario;

		
		private string Zone;

		
		private string[] Vehicles;

		
		private Color[] Colours;

		
		private string SuspectVehicleModel;

		
		private bool CalloutRunning = false;

		
		private LHandle MainPursuit;

		
		private readonly List<string> OpeningDialogue1 = new List<string>
		{
			"~g~You:~w~ Hey, what happened here?",
			"~b~Caller:~w~ Some asshole cut me off when he changed lanes!",
			"~b~Caller:~w~ I honked at him; and he got really pissed off, officer.",
			"~b~Caller:~w~ Eventually, he cut me off again, but sideswiped my door, as you can see.",
			"~g~You:~w~ I see that. Are you okay?",
			"~b~Caller:~w~ I'm fine, but my car isn't! And you can't let that asshole get away with it either!",
			"~g~You:~w~ What did his car look like? Where did he go?"
		};

		
		private readonly List<string> OpeningDialogue2 = new List<string>
		{
			"~b~Caller:~w~ Thanks for getting here so quickly officer! I thought I was gonna die!",
			"~g~You:~w~ Of course! What happened to start all of this?",
			"~b~Caller:~w~ Some asshole cut me off when he changed lanes!",
			"~b~Caller:~w~ I honked at him; and he got really pissed off, officer.",
			"~b~Caller:~w~ They stopped in the middle of the lane and forced me to pull over, then started smashing my car!",
			"~g~You:~w~ Did they assault you? Or was it just the car?",
			"~b~Caller:~w~ I'm fine, but my car isn't! Anyways, I'm just glad I didn't get hurt.",
			"~g~You:~w~ Yeah that's the most important thing. Well, if your car still works, then you're free to go.",
			"~b~Caller:~w~ Alright, thanks officer! Guess I'm on my way to the repair shop!"
		};
	}
}
