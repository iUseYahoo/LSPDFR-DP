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
using YobbinCallouts.Utilities;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Arson", 2)]
	public class Arson : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Arson Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(1, 5);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is Value is " + this.MainScenario.ToString());
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			bool flag = this.MainScenario > 0;
			if (flag)
			{
				Vector3 Spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(650f));
				try
				{
					if (Arson.<>o__19.<>p__0 == null)
					{
						Arson.<>o__19.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(Arson), new CSharpArgumentInfo[]
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
					Arson.<>o__19.<>p__0.Target(Arson.<>o__19.<>p__0, NativeFunction.Natives, Spawn, ref nodePosition, ref heading, 1, 3f, 0);
					if (Arson.<>o__19.<>p__2 == null)
					{
						Arson.<>o__19.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(Arson)));
					}
					Func<CallSite, object, bool> target = Arson.<>o__19.<>p__2.Target;
					CallSite <>p__ = Arson.<>o__19.<>p__2;
					if (Arson.<>o__19.<>p__1 == null)
					{
						Arson.<>o__19.<>p__1 = CallSite<<>F{00000010}<CallSite, object, Vector3, float, Vector3, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "xA0F8A7517A273C05", new Type[]
						{
							typeof(bool)
						}, typeof(Arson), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
						}));
					}
					Vector3 outPosition;
					bool success = target(<>p__, Arson.<>o__19.<>p__1.Target(Arson.<>o__19.<>p__1, NativeFunction.Natives, Spawn, heading, ref outPosition));
					bool flag2 = success;
					if (!flag2)
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
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 75f);
			base.AddMinimumDistanceCheck(50f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("CITIZENS_REPORT YC_ARSON");
			base.CalloutMessage = "Arson";
			base.CalloutPosition = this.MainSpawnPoint;
			base.CalloutAdvisory = "Reports of a ~r~Suspect ~w~Setting ~o~Fire~w~ to Parked Vehicles.";
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Arson Callout Accepted by User.");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 3", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~r~Code 3~w~");
			}
			bool flag = this.MainScenario > 0;
			if (flag)
			{
				this.VictimVehicle = CallHandler.SpawnVehicle(this.MainSpawnPoint, this.VehicleHeading, true);
				this.VictimVehicle.IsPersistent = true;
				this.VictimVehicle.IsDriveable = false;
				this.VictimVehicle.EngineHealth = 0f;
				this.MainSpawnPoint = this.VictimVehicle.GetOffsetPositionRight(1.5f);
				this.SuspectHeading = this.VictimVehicle.Heading + 90f;
				this.House = this.VictimVehicle.AttachBlip();
				this.House.IsFriendly = false;
				bool flag2 = EntityExtensions.Exists(this.House);
				if (flag2)
				{
					this.House.IsRouteEnabled = true;
				}
			}
			bool flag3 = this.MainScenario < 3;
			if (flag3)
			{
				this.Peds = new string[]
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
				int SuspectModel = r2.Next(0, this.Peds.Length);
				this.Suspect = new Ped(this.Peds[SuspectModel], this.MainSpawnPoint, this.SuspectHeading);
				Game.LogTrivial("YOBBINCALLOUTS: Suspect Spawned.");
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.Suspect.IsFireProof = true;
				Random r3 = new Random();
				int Weapon = r3.Next(0, 3);
				bool flag4 = Weapon == 0;
				if (flag4)
				{
					this.Suspect.Inventory.GiveNewWeapon("weapon_molotov", -1, true);
				}
				else
				{
					this.Suspect.Inventory.GiveNewWeapon("weapon_petrolcan", -1, true);
				}
				bool calloutInterface2 = Main.CalloutInterface;
				if (calloutInterface2)
				{
					CalloutInterfaceHandler.SendMessage(this, "Victim reports the Suspect is still on the scene.");
					CalloutInterfaceHandler.SendMessage(this, "Suspect is threatening victim with a further arson attack.");
				}
			}
			else
			{
				this.Victim = new Ped(this.VictimVehicle.GetOffsetPositionRight(6f));
				this.Victim.IsInvincible = true;
				this.Victim.IsPersistent = true;
				this.Victim.BlockPermanentEvents = true;
				bool calloutInterface3 = Main.CalloutInterface;
				if (calloutInterface3)
				{
					CalloutInterfaceHandler.SendMessage(this, "Victim reports Suspect fled the scene following the arson attack.");
					CalloutInterfaceHandler.SendMessage(this, "Multiple reports that the Victim's vehicle is still on fire.");
				}
			}
			bool flag5 = !this.CalloutRunning;
			if (flag5)
			{
				this.CalloutRunning = true;
			}
			this.Callout();
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Attempted Arson Callout Not Accepted by User.");
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
						Game.DisplayHelp("Locate the ~r~Suspect.");
						while (this.player.Character.DistanceTo(this.MainSpawnPoint) >= 35f && !Game.IsKeyDown(Config.CalloutEndKey))
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
							bool flag2 = this.MainScenario <= 2;
							if (flag2)
							{
								this.SuspectOnScene();
							}
							else
							{
								bool flag3 = this.MainScenario >= 3;
								if (flag3)
								{
									this.VictimOnFire();
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

		
		private void SuspectOnScene()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				this.SuspectBlip.Name = "Suspect";
				bool flag = EntityExtensions.Exists(this.House);
				if (flag)
				{
					this.House.IsRouteEnabled = false;
				}
				while (this.player.Character.DistanceTo(this.Suspect) >= 20f)
				{
					this.Suspect.Tasks.PlayAnimation("weapon@w_sp_jerrycan", "fire", -1f, 0);
					GameFiber.Wait(2000);
				}
				this.Suspect.Tasks.PlayAnimation("weapon@w_sp_jerrycan", "fire_outro", -1f, 1);
				GameFiber.Wait(1000);
				bool flag2 = EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect);
				if (flag2)
				{
					this.VictimVehicle.IsOnFire = true;
					if (Arson.<>o__23.<>p__0 == null)
					{
						Arson.<>o__23.<>p__0 = CallSite<Action<CallSite, object, Vector3, int, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "StartScriptFire", null, typeof(Arson), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Arson.<>o__23.<>p__0.Target(Arson.<>o__23.<>p__0, NativeFunction.Natives, this.VictimVehicle.Position, 5, true);
				}
				this.VictimVehicle.IsOnFire = true;
				Game.DisplaySubtitle("Dispatch, ~r~Suspect~w~ Just Lit a Vehicle on ~o~Fire!", 2500);
				GameFiber.Wait(1000);
				bool callFD = Config.CallFD;
				if (callFD)
				{
					try
					{
						Functions.RequestBackup(this.VictimVehicle.Position, 1, 7);
					}
					catch (NullReferenceException)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Error Spawning LSPDFR Fire Truck.");
					}
					Game.DisplayNotification("~r~Fire Department~w~ is En Route!");
					Game.LogTrivial("YOBBINCALLOUTS: Fire Department Has Been Called");
				}
				bool flag3 = this.MainScenario == 1;
				if (flag3)
				{
					this.SuspectFlees();
				}
				else
				{
					this.SuspectAttacks();
				}
			}
		}

		
		private void SuspectFlees()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = this.Suspect.IsAlive && EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect);
				if (flag)
				{
					this.Suspect.Tasks.ClearImmediately();
					Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
					bool calloutInterface = Main.CalloutInterface;
					if (calloutInterface)
					{
						CalloutInterfaceHandler.SendMessage(this, "Suspect is Fleeing, Requesting Backup.");
					}
					CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
					{
						this.Suspect
					});
				}
			}
		}

		
		private void SuspectAttacks()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = this.Suspect.IsAlive && EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect);
				if (flag)
				{
					this.Suspect.Tasks.ClearImmediately();
					this.Suspect.Inventory.Weapons.Clear();
					this.Suspect.Inventory.GiveNewWeapon("WEAPON_KNIFE", -1, true);
					GameFiber.Wait(500);
					this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
					while (EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect) && this.Suspect.IsAlive)
					{
						GameFiber.Yield();
					}
					bool flag2 = EntityExtensions.Exists(this.Suspect);
					if (flag2)
					{
						bool flag3 = Functions.IsPedArrested(this.Suspect);
						if (flag3)
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ for Attempting to ~r~Assault~w~ an Officer.");
						}
						else
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Attempting to ~r~Assault~w~ an Officer.");
						}
					}
					else
					{
						GameFiber.Wait(1000);
						Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Attempting to ~r~Assault~w~ an Officer.");
					}
					GameFiber.Wait(2000);
					Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
					GameFiber.Wait(2000);
				}
			}
		}

		
		private void VictimOnFire()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = EntityExtensions.Exists(this.House);
				if (flag)
				{
					this.House.IsRouteEnabled = false;
				}
				this.VictimBlip = this.Victim.AttachBlip();
				this.VictimBlip.IsFriendly = true;
				this.VictimBlip.Scale = 0.75f;
				this.Victim.Tasks.AchieveHeading(this.VictimVehicle.Heading + 90f).WaitForCompletion(500);
				this.Victim.Tasks.Cower(-1);
				this.VictimVehicle.IsOnFire = true;
				Vector3 FireBone = default(Vector3);
				FireBone = this.VictimVehicle.GetBonePosition("bodyshell");
				if (Arson.<>o__26.<>p__0 == null)
				{
					Arson.<>o__26.<>p__0 = CallSite<Action<CallSite, object, Vector3, int, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "StartScriptFire", null, typeof(Arson), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Arson.<>o__26.<>p__0.Target(Arson.<>o__26.<>p__0, NativeFunction.Natives, FireBone, 5, true);
				if (Arson.<>o__26.<>p__1 == null)
				{
					Arson.<>o__26.<>p__1 = CallSite<Action<CallSite, object, Vector3, int, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "StartScriptFire", null, typeof(Arson), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Arson.<>o__26.<>p__1.Target(Arson.<>o__26.<>p__1, NativeFunction.Natives, FireBone, 5, false);
				this.VictimVehicle.EngineHealth = 0f;
				this.VictimVehicle.IsOnFire = true;
				bool flag2 = this.player.Character.IsInAnyVehicle(false);
				if (flag2)
				{
					Game.DisplayHelp("Exit Your ~b~Vehicle~w~ and Investigate the ~r~Situation.");
				}
				while (this.player.Character.IsInAnyVehicle(false))
				{
					GameFiber.Wait(0);
				}
				Game.DisplaySubtitle("~g~You: ~w~Dispatch, We Have a Vehicle ~o~Fire!", 2500);
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Vehicle is on Fire Requesting Fire Department.");
				}
				GameFiber.Wait(2000);
				bool callFD = Config.CallFD;
				if (callFD)
				{
					try
					{
						Functions.RequestBackup(this.VictimVehicle.Position, 1, 7);
					}
					catch (NullReferenceException)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Error Spawning LSPDFR Fire Truck.");
					}
					Game.LogTrivial("YOBBINCALLOUTS: Fire Department Has Been Called");
					Game.DisplayNotification("~r~Fire Department~w~ is En Route!");
				}
				GameFiber.Wait(1000);
				Game.DisplayHelp("Either Talk to the ~b~Victim~w~, or Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Grab a ~o~Fire Extinguisher~w~ from the Back of Your ~b~Vehicle.");
				while (this.player.Character.IsInAnyVehicle(false))
				{
					GameFiber.Wait(0);
				}
				Vector3 BootBone = default(Vector3);
				bool flag3 = EntityExtensions.Exists(this.player.Character.LastVehicle);
				if (flag3)
				{
					bool flag4 = this.player.Character.LastVehicle.HasBone("boot");
					if (flag4)
					{
						BootBone = this.player.Character.LastVehicle.GetBonePosition("boot");
					}
					else
					{
						BootBone = this.player.Character.GetOffsetPositionFront(-4f);
					}
				}
				string str = "YOBBINCALLOUTS: Trunk Location is:";
				Vector3 vector = BootBone;
				Game.LogTrivial(str + vector.ToString());
				while (!Game.IsKeyDown(Config.MainInteractionKey) && this.player.Character.DistanceTo(this.Victim) >= 2f)
				{
					GameFiber.Wait(0);
				}
				bool flag5 = Game.IsKeyDown(Config.MainInteractionKey) && this.player.Character.DistanceTo(this.Victim) >= 2f;
				if (flag5)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Player Has Chosen to Fight Fire.");
					this.VictimVehicle.IsExplosionProof = false;
					this.player.Character.Tasks.FollowNavigationMeshToPosition(BootBone, 2f, this.player.Character.LastVehicle.Heading, 0.05f, -1).WaitForCompletion();
					this.player.Character.Heading = this.player.Character.LastVehicle.Heading;
					this.player.Character.Tasks.PlayAnimation("rcmepsilonism8", "bag_handler_grab_walk_left", 1f, 0);
					GameFiber.Wait(1000);
					this.player.Character.LastVehicle.OpenDoor(VehicleExtensions.Doors.Trunk, false);
					this.player.Character.Inventory.GiveNewWeapon("WEAPON_FIREEXTINGUISHER", -1, true);
					this.player.Character.IsFireProof = true;
					this.player.Character.IsExplosionProof = true;
					this.player.Character.CanRagdoll = false;
					GameFiber.Wait(500);
					this.player.Character.Tasks.ClearImmediately();
					GameFiber.Wait(5000);
					Game.DisplayHelp("Once the ~o~Fire~w~ is ~g~Extinguished~w~, Talk to the ~b~Victim.");
				}
				this.TalkToVictim();
			}
		}

		
		private void TalkToVictim()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				while (this.player.Character.DistanceTo(this.Victim) >= 3f)
				{
					GameFiber.Wait(0);
				}
				this.Victim.Tasks.ClearImmediately();
				this.Victim.Tasks.StandStill(500).WaitForCompletion(500);
				this.Victim.Tasks.AchieveHeading(this.player.Character.Heading - 180f).WaitForCompletion(500);
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to talk to the ~b~Victim.");
				}
				this.House.Delete();
				Random r = new Random();
				int OpeningDialogue = r.Next(0, 2);
				int num = OpeningDialogue;
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 == 1)
					{
						CallHandler.Dialogue(this.OpeningDialogue2, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
				}
				else
				{
					CallHandler.Dialogue(this.OpeningDialogue1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				Vector3 outPosition = World.GetNextPositionOnStreet(this.Victim.Position.Around(300f));
				try
				{
					this.Peds = new string[]
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
					int SuspectModel = r2.Next(0, this.Peds.Length);
					this.Suspect = new Ped(this.Peds[SuspectModel], outPosition, 69f);
					Game.LogTrivial("YOBBINCALLOUTS: Suspect Spawned.");
					this.Suspect.IsPersistent = true;
					this.Suspect.BlockPermanentEvents = true;
					this.Suspect.IsFireProof = true;
					Random r3 = new Random();
					int Weapon = r3.Next(0, 2);
					bool flag = Weapon == 0;
					if (flag)
					{
						this.Suspect.Inventory.GiveNewWeapon("weapon_molotov", -1, true);
					}
					else
					{
						this.Suspect.Inventory.GiveNewWeapon("weapon_petrolcan", -1, true);
					}
					Game.LogTrivial("YOBBINCALLOUTS: Suspect Weapon Given.");
					this.Victim.Tasks.ClearImmediately();
					Game.LogTrivial("YOBBINCALLOUTS: Victim Turning to Face Suspect Area.");
					if (Arson.<>o__27.<>p__0 == null)
					{
						Arson.<>o__27.<>p__0 = CallSite<Action<CallSite, object, Ped, Vector3, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_COORD", null, typeof(Arson), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Arson.<>o__27.<>p__0.Target(Arson.<>o__27.<>p__0, NativeFunction.Natives, this.Victim, outPosition, 1500);
				}
				catch (Exception)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Yobbincallouts Crash Prevented. InvalidHandleableException.");
					this.End();
				}
				GameFiber.Wait(2000);
				this.Victim.Tasks.ClearImmediately();
				this.Victim.Tasks.PlayAnimation("gestures@f@standing@casual", "gesture_point", 1f, 1).WaitForCompletion(1500);
				GameFiber.Wait(1500);
				this.Victim.Tasks.ClearImmediately();
				Game.DisplaySubtitle("Alright, I'll Start the Search. Thanks!", 3000);
				GameFiber.Wait(3500);
				this.Victim.Dismiss();
				this.VictimBlip.Delete();
				this.Search();
			}
		}

		
		private void Search()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = EntityExtensions.Exists(this.player.Character.LastVehicle);
				if (flag)
				{
					this.player.Character.LastVehicle.CloseDoor(VehicleExtensions.Doors.Trunk, false);
				}
				this.player.Character.IsFireProof = false;
				this.player.Character.IsExplosionProof = false;
				this.player.Character.CanRagdoll = true;
				this.SuspectArea = new Blip(this.Suspect.Position.Around(15f), 150f);
				this.SuspectArea.Color = Color.Orange;
				this.SuspectArea.Alpha = 0.5f;
				bool flag2 = EntityExtensions.Exists(this.SuspectArea);
				if (flag2)
				{
					this.SuspectArea.IsRouteEnabled = true;
				}
				this.Suspect.Tasks.Wander();
				GameFiber.Wait(1500);
				Game.DisplayHelp("Start ~o~Searching~w~ for the ~r~Suspect.");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Search Started for the Arson Suspect.");
				}
				this.Victim.ClearLastVehicle();
				this.Victim.Dismiss();
				while (this.player.Character.DistanceTo(this.Suspect) >= 60f)
				{
					GameFiber.Wait(0);
				}
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01");
				GameFiber.Wait(1000);
				Game.DisplayNotification("~b~Update:~w~ A Caller Has ~y~Spotted~w~ the ~r~Suspect. ~g~Updating Map.");
				GameFiber.Wait(1000);
				this.SuspectArea.Delete();
				this.SuspectArea = new Blip(this.Suspect.Position.Around(10f), 50f);
				this.SuspectArea.Color = Color.Orange;
				this.SuspectArea.Alpha = 0.5f;
				while (this.player.Character.DistanceTo(this.Suspect) >= 20f)
				{
					GameFiber.Wait(0);
				}
				this.SuspectArea.Delete();
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				GameFiber.Wait(1500);
				Game.DisplaySubtitle("Dispatch, We Have Located the ~r~Suspect!");
				GameFiber.Wait(1500);
				bool flag3 = this.MainScenario == 3;
				if (flag3)
				{
					this.SuspectFlees();
				}
				else
				{
					this.SuspectAttacks();
				}
			}
		}

		
		public override void End()
		{
			base.End();
			this.CalloutRunning = false;
			Game.DisplayNotification("~g~Code 4~w~, return to patrol.");
			bool flag = EntityExtensions.Exists(this.VictimBlip);
			if (flag)
			{
				this.VictimBlip.Delete();
			}
			bool flag2 = EntityExtensions.Exists(this.Suspect);
			if (flag2)
			{
				this.Suspect.Dismiss();
			}
			bool flag3 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag3)
			{
				this.SuspectBlip.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.House);
			if (flag4)
			{
				this.House.Delete();
			}
			Functions.PlayScannerAudio("ATTENTION_ALL_UNITS WE_ARE_CODE_4");
			Game.LogTrivial("YOBBINCALLOUTS: Arson Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private Vector3 MainSpawnPoint;

		
		private Blip House;

		
		private Blip SuspectBlip;

		
		private Blip VictimBlip;

		
		private Blip SuspectArea;

		
		private Ped Suspect;

		
		private Ped Victim;

		
		private int MainScenario;

		
		private float SuspectHeading;

		
		private bool CalloutRunning = false;

		
		private string Zone;

		
		private float VehicleHeading;

		
		private string[] Vehicles;

		
		private string[] Peds;

		
		private Player player = Game.LocalPlayer;

		
		private LHandle MainPursuit;

		
		private Vehicle VictimVehicle;

		
		private readonly List<string> OpeningDialogue1 = new List<string>
		{
			"~b~Victim:~w~ Oh thank god you're here Officer!",
			"~b~Victim:~w~ Some guy just ran up to my car and set it on fire while I was sitting in it!",
			"~g~You:~w~ Are you alright? Do you need Medical Attention?",
			"~b~Victim:~w~ I think I'm fine, But you've got to catch him! He's crazy and could be doing the same thing again!",
			"~g~You:~w~ Alright, do you know where he ran off too?",
			"~b~Victim:~w~ He ran further up the street somewhere that way!"
		};

		
		private readonly List<string> OpeningDialogue2 = new List<string>
		{
			"~b~Victim:~w~ Oh thank god you're here Officer!",
			"~b~Victim:~w~ Some dude ran up to my car and set it on fire while I was waiting at the traffic light!",
			"~g~You:~w~ Are you ok? Do you need Medical Attention?",
			"~b~Victim:~w~ I'll be fine, But you've got to catch him! He's insane and might do the same thing to someone else!",
			"~g~You:~w~ Alright, do you know where he ran off too?",
			"~b~Victim:~w~ He went further up the street somewhere in that direction!"
		};
	}
}
