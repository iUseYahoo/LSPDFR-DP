using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Exceptions;
using Rage.Native;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Assault On Bus", 3)]
	public class AssaultOnBus : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Assault on Bus Callout Start==========");
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			CallHandler.locationChooser(this.list, 600f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			bool result;
			if (locationReturned)
			{
				this.SpawnPoint = CallHandler.SpawnPoint;
				base.ShowCalloutAreaBlipBeforeAccepting(this.SpawnPoint, 75f);
				base.AddMinimumDistanceCheck(30f, this.SpawnPoint);
				base.AddMaximumDistanceCheck(600f, this.SpawnPoint);
				bool city = this.City;
				if (city)
				{
					Functions.PlayScannerAudio("CITIZENS_REPORT YC_ASSAULT_ON_BUS");
				}
				else
				{
					Functions.PlayScannerAudio("CITIZENS_REPORT YC_ASSAULT_ON_COACH_BUS");
				}
				base.CalloutMessage = "Assault on Bus";
				base.CalloutPosition = this.SpawnPoint;
				base.CalloutAdvisory = "Suspect is Reported to have ~r~Violently Assaulted~w~ a Person on the Bus.";
				result = base.OnBeforeCalloutDisplayed();
			}
			else
			{
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Assault On Bus Callout Accepted by User");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 3", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~r~Code 3~w~");
			}
			bool city = this.City;
			if (city)
			{
				this.Bus = new Vehicle("BUS", this.SpawnPoint);
			}
			else
			{
				this.Bus = new Vehicle("coach", this.SpawnPoint);
			}
			try
			{
				if (AssaultOnBus.<>o__26.<>p__0 == null)
				{
					AssaultOnBus.<>o__26.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(AssaultOnBus), new CSharpArgumentInfo[]
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
				Vector3 vector;
				float heading;
				AssaultOnBus.<>o__26.<>p__0.Target(AssaultOnBus.<>o__26.<>p__0, NativeFunction.Natives, this.SpawnPoint, ref vector, ref heading, 1, 3f, 0);
				if (AssaultOnBus.<>o__26.<>p__2 == null)
				{
					AssaultOnBus.<>o__26.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(AssaultOnBus)));
				}
				Func<CallSite, object, bool> target = AssaultOnBus.<>o__26.<>p__2.Target;
				CallSite <>p__ = AssaultOnBus.<>o__26.<>p__2;
				if (AssaultOnBus.<>o__26.<>p__1 == null)
				{
					AssaultOnBus.<>o__26.<>p__1 = CallSite<<>F{00000010}<CallSite, object, Vector3, float, Vector3, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(AssaultOnBus), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				bool success = target(<>p__, AssaultOnBus.<>o__26.<>p__1.Target(AssaultOnBus.<>o__26.<>p__1, NativeFunction.Natives, this.SpawnPoint, heading, ref outPosition));
				bool flag = success;
				if (!flag)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
					return false;
				}
				this.SpawnPoint = outPosition;
				this.VehicleHeading = heading;
			}
			catch (TargetInvocationException)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
				return false;
			}
			this.Bus.Heading = this.VehicleHeading;
			Game.LogTrivial("YOBBINCALLOUTS: Bus Spawned");
			this.Bus.IsPersistent = true;
			this.Bus.IsEngineOn = true;
			this.Bus.IsDriveable = false;
			this.Driver = this.Bus.CreateRandomDriver();
			this.Driver.IsPersistent = true;
			this.Driver.BlockPermanentEvents = true;
			this.Driver.Tasks.CruiseWithVehicle(0f);
			this.Driver.IsInvincible = true;
			Game.LogTrivial("YOBBINCALLOUTS: Bus Driver Spawned");
			Random r = new Random();
			int Scenario = r.Next(0, 1);
			int num = Scenario;
			int num2 = num;
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					this.MainScenario = 1;
					Game.LogTrivial("YOBBINCALLOUTS: Assault on Bus Scenario 1 - Fare Evasion Chosen");
					this.DriverBlip = this.Driver.AttachBlip();
					this.DriverBlip.IsFriendly = true;
					this.DriverBlip.Scale = 0.65f;
					this.DriverBlip.IsRouteEnabled = true;
					this.DriverBlip.Name = "Driver";
					this.Witness = new Ped(this.Driver.GetOffsetPositionFront(2f));
					this.Witness.IsPersistent = true;
					this.Witness.BlockPermanentEvents = true;
					this.Witness2 = new Ped(this.Driver.GetOffsetPositionFront(5f));
					this.Witness2.IsPersistent = true;
					this.Witness2.BlockPermanentEvents = true;
				}
			}
			else
			{
				this.MainScenario = 0;
				Game.LogTrivial("YOBBINCALLOUTS: Assault on Bus Scenario 0 - Unprovoked Assault Chosen");
				this.VictimSpawnpoint = this.Driver.GetOffsetPositionRight(3f);
				this.Victim = new Ped(this.VictimSpawnpoint);
				this.Victim.IsPersistent = true;
				this.Victim.BlockPermanentEvents = true;
				this.Victim.IsInvincible = true;
				this.VictimBlip = this.Victim.AttachBlip();
				this.VictimBlip.IsFriendly = true;
				this.VictimBlip.IsRouteEnabled = true;
				this.VictimBlip.Scale = 0.65f;
				this.VictimBlip.Name = "Victim";
			}
			Random rYUY = new Random();
			this.SuspectAction = rYUY.Next(0, 3);
			Game.LogTrivial("YOBBINCALLOUTS: Suspect Action Value is " + this.SuspectAction.ToString());
			bool flag2 = !this.CalloutRunning;
			if (flag2)
			{
				this.Callout();
			}
			this.CalloutRunning = true;
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Assault on Bus Callout Not Accepted by User");
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
						while (this.player.DistanceTo(this.Driver) >= 15f && !Game.IsKeyDown(Config.CalloutEndKey))
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
							bool calloutInterface = Main.CalloutInterface;
							if (calloutInterface)
							{
								CalloutInterfaceHandler.SendMessage(this, "Unit Arrived on Scene. Talking to Witness");
							}
							bool flag2 = this.MainScenario == 0;
							if (flag2)
							{
								this.AssaultOpening();
							}
							bool flag3 = EntityExtensions.Exists(this.Witness) && this.Witness.IsInAnyVehicle(false);
							if (flag3)
							{
								this.Witness.Tasks.LeaveVehicle(this.Witness.CurrentVehicle, 0).WaitForCompletion();
								this.Witness.ClearLastVehicle();
								this.Witness.Tasks.Wander();
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

		
		private void AssaultOpening()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplayHelp("Speak with the ~b~Victim.");
				if (AssaultOnBus.<>o__29.<>p__0 == null)
				{
					AssaultOnBus.<>o__29.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(AssaultOnBus), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				AssaultOnBus.<>o__29.<>p__0.Target(AssaultOnBus.<>o__29.<>p__0, NativeFunction.Natives, this.Victim, this.player, -1);
				while (this.player.DistanceTo(this.Victim) >= 5f)
				{
					GameFiber.Wait(0);
				}
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~b~Victim.");
				}
				while (!Game.IsKeyDown(Config.MainInteractionKey))
				{
					GameFiber.Wait(0);
				}
				Random r = new Random();
				int OpeningDialogue = r.Next(0, 3);
				bool flag = OpeningDialogue == 0;
				if (flag)
				{
					CallHandler.Dialogue(this.OpeningDialogue1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				else
				{
					bool flag2 = OpeningDialogue == 1;
					if (flag2)
					{
						CallHandler.Dialogue(this.OpeningDialogue2, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					else
					{
						CallHandler.Dialogue(this.OpeningDialogue3, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
				}
				this.Victim.Tasks.ClearImmediately();
				Game.LogTrivial("YOBBINCALLOUTS: Started Suspect 1 Spawn");
				if (AssaultOnBus.<>o__29.<>p__1 == null)
				{
					AssaultOnBus.<>o__29.<>p__1 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(Vector3)
					}, typeof(AssaultOnBus), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 Suspect1Spawn;
				AssaultOnBus.<>o__29.<>p__1.Target(AssaultOnBus.<>o__29.<>p__1, NativeFunction.Natives, World.GetNextPositionOnStreet(this.player.Position.Around(69f)), 360, ref Suspect1Spawn);
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
				Random r2 = new Random();
				int SuspectModel = r2.Next(0, this.Suspects.Length);
				this.Suspect = new Ped(this.Suspects[SuspectModel], Suspect1Spawn, 69f);
				try
				{
					this.Suspect.IsPersistent = true;
					this.Suspect.BlockPermanentEvents = true;
					this.Suspect.IsVisible = false;
				}
				catch (InvalidHandleableException)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Yobbincallouts Crash Prevented. InvalidHandleableException.");
					Game.DisplayNotification("~b~YobbinCallouts~r~ Crash~g~ Prevented.~w~ I Apologize for the ~y~Inconvenience.");
					this.End();
				}
				Game.LogTrivial("YOBBINCALLOUTS: Finished Suspect 1 Spawn");
				Game.DisplayHelp(string.Concat(new string[]
				{
					"~y~",
					Config.Key1.ToString(),
					":~b~ Let the Victim Help You Search for the Suspect.~y~ ",
					Config.Key2.ToString(),
					":~b~ Search for the Suspect Yourself."
				}));
				CallHandler.IdleAction(this.Victim, false);
				while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
				{
					GameFiber.Wait(0);
				}
				this.Victim.Tasks.ClearImmediately();
				bool flag3 = Game.IsKeyDown(Config.Key1);
				if (flag3)
				{
					this.Follow();
				}
				else
				{
					this.Search();
				}
			}
		}

		
		private void Follow()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplaySubtitle("~g~You:~w~ Sure, It Would Help a lot if You Were There to Help Me. Hop In!", 3000);
				GameFiber.Wait(3500);
				Game.DisplayHelp("Get in your ~g~Vehicle.");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Victim was assaulted on the bus. Victim is assisting in finding the Suspect.");
				}
				while (!Game.LocalPlayer.Character.IsInAnyPoliceVehicle)
				{
					GameFiber.Wait(0);
				}
				Game.DisplayHelp(string.Concat(new string[]
				{
					"~y~",
					Config.Key1.ToString(),
					": ~b~Tell the Victim to Enter the Passenger Seat. ~y~",
					Config.Key2.ToString(),
					":~b~ Tell the Victim to Enter the Rear Seat."
				}));
				while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
				{
					GameFiber.Wait(0);
				}
				bool flag = Game.IsKeyDown(Config.Key1);
				if (flag)
				{
					int SeatIndex = Game.LocalPlayer.Character.CurrentVehicle.GetFreePassengerSeatIndex().Value;
					this.Victim.Tasks.EnterVehicle(Game.LocalPlayer.Character.CurrentVehicle, SeatIndex, 0).WaitForCompletion();
				}
				else
				{
					int SeatIndex = Game.LocalPlayer.Character.CurrentVehicle.GetFreeSeatIndex(1, 2).Value;
					this.Victim.Tasks.EnterVehicle(Game.LocalPlayer.Character.CurrentVehicle, SeatIndex, 0).WaitForCompletion();
				}
				bool flag2 = EntityExtensions.Exists(this.VictimBlip);
				if (flag2)
				{
					this.VictimBlip.Delete();
				}
				this.Suspect.IsVisible = false;
				Game.DisplayHelp("Start ~o~Searching~w~ for the ~r~Suspect.");
				this.SuspectArea = new Blip(this.Suspect.Position.Around(15f), 250f);
				this.SuspectArea.Color = Color.Orange;
				this.SuspectArea.Alpha = 0.5f;
				GameFiber.Wait(1500);
				bool flag3 = EntityExtensions.Exists(this.Driver);
				if (flag3)
				{
					this.Driver.Dismiss();
				}
				Random coco = new Random();
				int WaitTime = coco.Next(20000, 40000);
				Game.LogTrivial("YOBBINCALLOUTS: Waiting " + WaitTime.ToString() + " Seconds.");
				GameFiber.Wait(WaitTime);
				this.Suspect.Position = World.GetNextPositionOnStreet(this.Victim.Position.Around(30f));
				this.Suspect.IsVisible = true;
				this.Suspect.Tasks.Wander();
				Game.DisplaySubtitle("~b~Victim:~w~ Officer I See the ~r~Suspect~w~! He's Right Over There!", 2500);
				GameFiber.Wait(2000);
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				this.SuspectArea.Delete();
				Game.DisplayHelp("Arrest the ~r~Suspect.");
				while (this.player.DistanceTo(this.Suspect) >= 5f)
				{
					GameFiber.Wait(0);
				}
				Game.DisplaySubtitle("~g~You:~w~ Hey Sir, I Need to Speak With You!", 2500);
				GameFiber.Wait(1000);
				this.SuspectEnding();
			}
		}

		
		private void Search()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplaySubtitle("~g~You:~w~ No Sorry, I Can't Let You Come With Me. I'll Search for the Suspect Based on Your Information.", 3500);
				GameFiber.Wait(3500);
				this.Victim.Dismiss();
				bool flag = EntityExtensions.Exists(this.VictimBlip);
				if (flag)
				{
					this.VictimBlip.Delete();
				}
				Game.DisplayHelp("Start ~o~Searching~w~ for the ~r~Suspect.");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Victim was assaulted on the bus. Searching for the Suspect.");
				}
				this.SuspectArea = new Blip(this.Suspect.Position.Around(15f), 250f);
				this.SuspectArea.Color = Color.Orange;
				this.SuspectArea.Alpha = 0.5f;
				GameFiber.Wait(1500);
				this.Driver.Dismiss();
				Random coco = new Random();
				int WaitTime = coco.Next(35000, 69000);
				Game.LogTrivial("YOBBINCALLOUTS: Waiting " + WaitTime.ToString() + " Seconds.");
				GameFiber.Wait(WaitTime);
				this.Suspect.Position = World.GetNextPositionOnStreet(this.player.Position.Around(100f));
				this.Suspect.IsVisible = true;
				this.Suspect.Tasks.Wander();
				Game.DisplaySubtitle("~g~You:~w~ Hm, That Looks Like the Suspect Right There!", 2500);
				GameFiber.Wait(2000);
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				this.SuspectArea.Delete();
				Game.DisplayHelp("Arrest the ~r~Suspect.");
				while (this.player.DistanceTo(this.Suspect) >= 5f)
				{
					GameFiber.Wait(0);
				}
				Game.DisplaySubtitle("~g~You:~w~ Hey Sir, I Need to Speak With You!", 2500);
				GameFiber.Wait(1000);
				this.SuspectEnding();
			}
		}

		
		private void SuspectEnding()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = this.SuspectAction == 0;
				if (flag)
				{
					Game.DisplayHelp("Arrest the ~r~Suspect.");
					this.Suspect.Tasks.Clear();
					while (EntityExtensions.Exists(this.Suspect))
					{
						GameFiber.Yield();
						bool flag2 = this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect) || !EntityExtensions.Exists(this.Suspect);
						if (flag2)
						{
							break;
						}
					}
					bool flag3 = EntityExtensions.Exists(this.Suspect);
					if (flag3)
					{
						bool flag4 = Functions.IsPedArrested(this.Suspect) || this.Suspect.IsAlive;
						if (flag4)
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ for Assault.");
						}
						else
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~.");
						}
					}
					else
					{
						GameFiber.Wait(1000);
						Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~.");
					}
					GameFiber.Wait(2000);
					Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
					GameFiber.Wait(2000);
				}
				else
				{
					bool flag5 = this.SuspectAction == 1;
					if (flag5)
					{
						this.Fight();
					}
					else
					{
						this.Flee();
					}
				}
			}
		}

		
		private void Fight()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				this.Suspect.Tasks.ClearImmediately();
				this.Suspect.Inventory.Weapons.Clear();
				GameFiber.Wait(500);
				this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
				while (EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect) && this.Suspect.IsAlive)
				{
					GameFiber.Yield();
				}
				bool flag = EntityExtensions.Exists(this.Suspect);
				if (flag)
				{
					bool flag2 = Functions.IsPedArrested(this.Suspect) || this.Suspect.IsAlive;
					if (flag2)
					{
						GameFiber.Wait(1000);
						Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Attempting to ~r~Assault an Officer.");
					}
					else
					{
						GameFiber.Wait(1000);
						Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ for ~r~Assaulting an Officer.");
					}
				}
				else
				{
					GameFiber.Wait(1000);
					Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Attempting to ~r~Assault an Officer.");
				}
				GameFiber.Wait(2000);
				Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
				GameFiber.Wait(2000);
			}
		}

		
		private void Flee()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				GameFiber.Wait(500);
				this.Suspect.Tasks.ClearImmediately();
				CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
				{
					this.Suspect
				});
			}
		}

		
		public override void End()
		{
			base.End();
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplayNotification("Dispatch, Situation is ~b~Under Control.~w~ We Have Also Taken ~b~Witness Statements.");
				Game.DisplayNotification("~g~Code 4~w~, return to patrol.");
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS WE_ARE_CODE_4");
			}
			this.CalloutRunning = false;
			bool flag = EntityExtensions.Exists(this.SuspectBlip);
			if (flag)
			{
				this.SuspectBlip.Delete();
			}
			bool flag2 = EntityExtensions.Exists(this.VictimBlip);
			if (flag2)
			{
				this.VictimBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.DriverBlip);
			if (flag3)
			{
				this.DriverBlip.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.Driver);
			if (flag4)
			{
				this.Driver.Tasks.ClearImmediately();
			}
			bool flag5 = EntityExtensions.Exists(this.Driver);
			if (flag5)
			{
				this.Driver.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this.Suspect);
			if (flag6)
			{
				this.Suspect.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Witness2);
			if (flag7)
			{
				this.Witness2.Dismiss();
			}
			bool flag8 = EntityExtensions.Exists(this.Victim);
			if (flag8)
			{
				this.Victim.Tasks.ClearImmediately();
			}
			bool flag9 = EntityExtensions.Exists(this.Victim);
			if (flag9)
			{
				this.Victim.Dismiss();
			}
			bool flag10 = EntityExtensions.Exists(this.Bus);
			if (flag10)
			{
				this.Bus.IsPersistent = false;
			}
			bool flag11 = EntityExtensions.Exists(this.SuspectArea);
			if (flag11)
			{
				this.SuspectArea.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Assault on Bus Callout Cleaned Up");
		}

		
		private ArrayList list = new ArrayList
		{
			new Vector3(258.9062f, -377.681f, 44.58482f),
			new Vector3(-523.7678f, -267.3726f, 35.29238f),
			new Vector3(-107.0473f, -1685.895f, 29.20226f),
			new Vector3(-878.0088f, -1766.411f, 29.84155f),
			new Vector3(-138.7037f, -1982.754f, 22.83529f),
			new Vector3(279.4628f, -1459.935f, 29.1191f),
			new Vector3(-1521.592f, -463.6807f, 35.28944f),
			new Vector3(-1409.31f, -570.114f, 30.30899f),
			new Vector3(-1478.658f, -632.9943f, 30.46013f),
			new Vector3(-794.0625f, -3047.102f, 6.766771f),
			new Vector3(-806.9794f, -1354.459f, 26.29575f),
			new Vector3(788.3455f, -1368.106f, 26.49827f),
			new Vector3(770.7678f, -939.4482f, 25.593838f),
			new Vector3(785.4872f, -778.6043f, 26.33783f),
			new Vector3(-694.0866f, -667.7482f, 30.76516f),
			new Vector3(-506.7512f, -667.7038f, 33.02613f),
			new Vector3(-560.5147f, -845.7031f, 27.34039f),
			new Vector3(-1040.662f, -2725.775f, 20.0923f),
			new Vector3(-1045.258f, -2716.292f, 13.67731f),
			new Vector3(-146.6807f, 2041.945f, 22.94198f),
			new Vector3(1189.356f, -416.5154f, 67.46564f),
			new Vector3(-2102.175f, -295.1127f, 13.03409f),
			new Vector3(118.4144f, -785.7147f, 31.28741f),
			new Vector3(-172.1286f, -817.8408f, 31.10602f),
			new Vector3(-249.7246f, -883.182f, 30.56357f),
			new Vector3(-217.3994f, -1010.452f, 29.17915f),
			new Vector3(-692.3359f, -6.224844f, 38.15116f),
			new Vector3(-930.4749f, -126.0807f, 37.57829f),
			new Vector3(-1166.419f, -401.0388f, 35.45403f),
			new Vector3(-681.0934f, -375.9459f, 34.21792f),
			new Vector3(-258.2194f, -324.5125f, 29.88147f),
			new Vector3(257.9587f, -1187.642f, 29.45594f),
			new Vector3(70.83752f, -1474.274f, 29.20082f),
			new Vector3(49.93737f, -1537.299f, 29.19085f),
			new Vector3(307.6413f, -764.8298f, 29.2324f),
			new Vector3(-1168.946f, -1470.082f, 4.297196f),
			new Vector3(959.1135f, 173.7969f, 80.82661f),
			new Vector3(-501.973f, 20.67921f, 44.73386f),
			new Vector3(-1611.583f, 173.999f, 59.76557f),
			new Vector3(324.6387f, -88.70399f, 68.74538f),
			new Vector3(211.5656f, 250.5646f, 105.4593f),
			new Vector3(1122.201f, -252.3284f, 68.98948f),
			new Vector3(434.8558f, -2024.733f, 23.24066f),
			new Vector3(128.355f, -1715.873f, 29.06286f),
			new Vector3(351.9958f, -1064.253f, 29.39734f),
			new Vector3(461.3195f, -611.6256f, 28.48598f),
			new Vector3(307.9035f, -763.6847f, 29.22663f),
			new Vector3(-1212.478f, -1216.602f, 7.583918f),
			new Vector3(-862.2045f, -135.3484f, 37.80842f),
			new Vector3(-3031.738f, 593.2711f, 8.547567f),
			new Vector3(-3234.587f, 1005.579f, 13.0714f),
			new Vector3(-2563.994f, 2320.287f, 33.89155f),
			new Vector3(2565.242f, 392.0405f, 109.2949f),
			new Vector3(1733.514f, 6399.68f, 35.66004f),
			new Vector3(145.0622f, 6574.145f, 32.73963f),
			new Vector3(-215.3233f, 6173.316f, 32.05423f),
			new Vector3(-1145.751f, 2663.053f, 18.91434f),
			new Vector3(533.1015f, 2671.716f, 43.15641f),
			new Vector3(1955.849f, 3738.852f, 33.02981f),
			new Vector3(1680.88f, 4921.424f, 42.90202f),
			new Vector3(2700.959f, 3285.273f, 56.14145f)
		};

		
		private Vector3 SpawnPoint;

		
		private Vector3 VictimSpawnpoint;

		
		private string Zone;

		
		private bool City = false;

		
		private bool CalloutRunning = false;

		
		private Vehicle Bus;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private Ped Driver;

		
		private Ped Victim;

		
		private Ped Suspect;

		
		private Ped Witness;

		
		private Ped Witness2;

		
		private Blip VictimBlip;

		
		private Blip SuspectBlip;

		
		private Blip DriverBlip;

		
		private Blip SuspectArea;

		
		private int MainScenario;

		
		private int SuspectAction;

		
		private float VehicleHeading;

		
		private string[] Suspects;

		
		private LHandle MainPursuit;

		
		private readonly List<string> OpeningDialogue1 = new List<string>
		{
			"~b~Victim:~w~ Hi officer, over here.",
			"~g~You:~w~ Tell me what happened.",
			"~b~Victim:~w~ Well, I was just minding my own business when some random dude started yelling at me.",
			"~b~Victim:~w~ He got in my face and started swearing at me, and when I tried to leave, he punched me multiple times.",
			"~g~You:~w~ Do you need medical attention?",
			"~b~Victim:~w~ I think I'm Fine, but I'd like to catch him. He just ran off thinking he could get away with it.",
			"~g~You:~w~ Well, where did he go? What did he look like?",
			"~b~Victim:~w~ I can't remember exactly, but I'd definitely recognize Him if I saw Him. Could I come with you to help search for Him in the area?"
		};

		
		private readonly List<string> OpeningDialogue2 = new List<string>
		{
			"~b~Victim:~w~ Hi officer, over here!",
			"~g~You:~w~ What happened?.",
			"~b~Victim:~w~ Well, I was just minding my own business when some random dude started yelling at me.",
			"~b~Victim:~w~ He got in my face and started swearing at me, and when I tried to leave, he punched me multiple times.",
			"~g~You:~w~ Do you need medical attention?",
			"~b~Victim:~w~ I think I'm Fine, but I'd like to catch him. He just ran off thinking he could get away with it.",
			"~g~You:~w~ Well, where did he go? What did he look like?",
			"~b~Victim:~w~ I can't remember exactly, but I'd definitely recognize Him if I saw Him. Could I come with you to help search for Him in the area?"
		};

		
		private readonly List<string> OpeningDialogue3 = new List<string>
		{
			"~b~Victim:~w~ Hi officer, over here.",
			"~g~You:~w~ Tell me what happened.",
			"~b~Victim:~w~ Well, I was just minding my own business when some random dude started yelling at me.",
			"~b~Victim:~w~ He got in my face and started swearing at me, and when I tried to leave, he punched me multiple times.",
			"~g~You:~w~ Do you need medical attention?",
			"~b~Victim:~w~ I think I'm Fine, but I'd like to catch him. He just ran off thinking he could get away with it.",
			"~g~You:~w~ Well, where did he go? What did he look like?",
			"~b~Victim:~w~ I can't remember exactly, but I'd definitely recognize Him if I saw Him. Could I come with you to help search for Him in the area?"
		};
	}
}
