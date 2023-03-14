using System;
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
	
	[CalloutInfo("Bait Car", 3)]
	public class BaitCar : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Bait Car Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 0);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is Value is " + this.MainScenario.ToString());
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).RealAreaName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			bool flag = this.MainScenario >= 0;
			if (flag)
			{
				Vector3 Spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(550f));
				try
				{
					if (BaitCar.<>o__28.<>p__0 == null)
					{
						BaitCar.<>o__28.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(BaitCar), new CSharpArgumentInfo[]
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
					BaitCar.<>o__28.<>p__0.Target(BaitCar.<>o__28.<>p__0, NativeFunction.Natives, Spawn, ref nodePosition, ref heading, 1, 3f, 0);
					if (BaitCar.<>o__28.<>p__2 == null)
					{
						BaitCar.<>o__28.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(BaitCar)));
					}
					Func<CallSite, object, bool> target = BaitCar.<>o__28.<>p__2.Target;
					CallSite <>p__ = BaitCar.<>o__28.<>p__2;
					if (BaitCar.<>o__28.<>p__1 == null)
					{
						BaitCar.<>o__28.<>p__1 = CallSite<<>F{00000010}<CallSite, object, Vector3, float, Vector3, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "xA0F8A7517A273C05", new Type[]
						{
							typeof(bool)
						}, typeof(BaitCar), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
						}));
					}
					Vector3 outPosition;
					bool success = target(<>p__, BaitCar.<>o__28.<>p__1.Target(BaitCar.<>o__28.<>p__1, NativeFunction.Natives, Spawn, heading, ref outPosition));
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
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 50f);
			base.AddMinimumDistanceCheck(50f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("WE_HAVE_01 CRIME_OFFICER_IN_NEED_OF_ASSISTANCE_02");
			base.CalloutMessage = "Bait Car";
			base.CalloutPosition = this.MainSpawnPoint;
			base.CalloutAdvisory = "Officer Requests Assistance Monitoring a Bait Car Operation.";
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Bait Car Callout Accepted by User.");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~b~Code 2.~w~");
			}
			bool flag = this.MainScenario >= 0;
			if (flag)
			{
				this.OfficerVehicle = new Vehicle(Config.PoliceVehicle, this.MainSpawnPoint, this.VehicleHeading);
				this.OfficerVehicle.IsPersistent = true;
				this.OfficerVehicle.LicensePlate = "COCONUT";
				this.Officer = this.OfficerVehicle.CreateRandomDriver();
				this.Officer.IsPersistent = true;
				this.Officer.BlockPermanentEvents = true;
			}
			this.AreaBlip = new Blip(this.MainSpawnPoint, 25f);
			this.AreaBlip.Color = Color.Yellow;
			this.AreaBlip.Alpha = 0.67f;
			this.AreaBlip.IsRouteEnabled = true;
			this.AreaBlip.Name = "Callout Location";
			bool flag2 = Config.BaitVehicle == "None" || Config.BaitVehicle == "none";
			if (flag2)
			{
				this.BaitVehicle = CallHandler.SpawnVehicle(this.OfficerVehicle.GetOffsetPositionFront(-7f), this.OfficerVehicle.Heading, true);
			}
			else
			{
				Game.LogTrivial("YOBBINCALLOUTS: Player Has Specified a Custom Bait Car. Attempting Spawn.");
				try
				{
					this.BaitVehicle = new Vehicle(Config.BaitVehicle, this.OfficerVehicle.GetOffsetPositionFront(-7f), this.OfficerVehicle.Heading);
				}
				catch (Exception)
				{
					Game.DisplayNotification("~r~Error~w~ Spawning ~b~Custom Bait Car.~w~ Please Ensure the ~y~Vehicle name~w~ in ~y~Yobbincallouts.ini~w~ is ~g~Valid.");
					Game.LogTrivial("YOBBINCALLOUTS: Error Spawning Custom Bait Car Model. Most Likely an Invalid Vehicle Model/Name Changed by the User in YobbinCallouts.ini.");
					this.BaitVehicle = CallHandler.SpawnVehicle(this.OfficerVehicle.GetOffsetPositionFront(-7f), this.OfficerVehicle.Heading, true);
				}
			}
			Game.LogTrivial("YOBBINCALLOUTS: Bait Car Successfully Spawned.");
			this.BaitVehicle.IsPersistent = true;
			bool flag3 = !this.CalloutRunning;
			if (flag3)
			{
				this.CalloutRunning = true;
			}
			this.Callout();
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Bait Car Callout Not Accepted by User.");
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
						this.Setup();
						this.Observe();
						this.Stolen();
					}
					Game.LogTrivial("YOBBINCALLOUTS: Callout Finished, Ending...");
					EndCalloutHandler.EndCallout();
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
			bool flag = EntityExtensions.Exists(this.Suspect) && this.Suspect != this.MainSuspect;
			if (flag)
			{
				this.Suspect.Delete();
			}
			bool flag2 = EntityExtensions.Exists(this.Rando1) && this.Rando1 != this.MainSuspect;
			if (flag2)
			{
				this.Rando1.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.Rando2) && this.Rando2 != this.MainSuspect;
			if (flag3)
			{
				this.Rando2.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.Officer);
			if (flag4)
			{
				this.Officer.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.AreaBlip);
			if (flag5)
			{
				this.AreaBlip.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag6)
			{
				this.SuspectBlip.Delete();
			}
			bool flag7 = EntityExtensions.Exists(this.OfficerBlip);
			if (flag7)
			{
				this.OfficerBlip.Delete();
			}
			bool flag8 = EntityExtensions.Exists(this.BaitVehicleBlip);
			if (flag8)
			{
				this.BaitVehicleBlip.Delete();
			}
			bool flag9 = EntityExtensions.Exists(this.Rando1);
			if (flag9)
			{
				this.Rando1.Dismiss();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Bait Car Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void Setup()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				while (this.player.DistanceTo(this.Officer) >= 25f && !Game.IsKeyDown(Config.CalloutEndKey))
				{
					GameFiber.Wait(0);
				}
				bool flag = Game.IsKeyDown(Config.CalloutEndKey);
				if (flag)
				{
					this.End();
				}
				Game.DisplayHelp("Talk to the ~b~Officer.");
				this.AreaBlip.Delete();
				this.OfficerBlip = this.Officer.AttachBlip();
				this.OfficerBlip.IsFriendly = true;
				this.OfficerBlip.Scale = 0.75f;
				this.OfficerBlip.Name = "Officer";
				while (this.player.IsInAnyVehicle(false))
				{
					GameFiber.Wait(0);
				}
				this.Officer.Tasks.LeaveVehicle(0).WaitForCompletion();
				this.Officer.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
				CallHandler.IdleAction(this.Officer, true);
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak With the ~b~Officer.");
				}
				if (BaitCar.<>o__34.<>p__0 == null)
				{
					BaitCar.<>o__34.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(BaitCar), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				BaitCar.<>o__34.<>p__0.Target(BaitCar.<>o__34.<>p__0, NativeFunction.Natives, this.Officer, this.player, -1);
				while (this.player.DistanceTo(this.Officer) >= 5f)
				{
					GameFiber.Wait(0);
				}
				this.Officer.Tasks.ClearImmediately();
				Random r = new Random();
				int Dialogue = r.Next(0, 1);
				bool flag2 = Dialogue == 0;
				if (flag2)
				{
					CallHandler.Dialogue(this.OpeningDialogue1, this.Officer, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				this.BaitVehicleBlip = CallHandler.AssignBlip(this.BaitVehicle, Color.Green, 1f, "Bait Car", false, 1f);
				GameFiber.Wait(1000);
				this.Officer.PlayAmbientSpeech("generic_thanks");
				this.OfficerBlip.Delete();
				this.Officer.Dismiss();
				GameFiber.Wait(2500);
				Game.DisplayHelp("Find a ~y~suitable location~w~ to monitor the ~b~bait car.~w~ when you're ~g~ready, ~w~press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to ~g~Start Observing.");
				GameFiber.Wait(4000);
				Game.DisplayHelp("You can also move the ~g~Bait Car~w~ to a better position before Observing.");
			}
		}

		
		private void Observe()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				while (!Game.IsKeyDown(Config.MainInteractionKey))
				{
					GameFiber.Wait(0);
				}
				Random r = new Random();
				this.Peeps = r.Next(0, 3);
				bool flag = this.Peeps == 0;
				if (flag)
				{
					this.SpawnSuspects();
				}
				bool flag2 = this.Peeps == 1;
				if (flag2)
				{
					this.SpawnSuspects();
				}
				bool flag3 = this.Peeps == 2;
				if (flag3)
				{
					this.SpawnSuspects();
				}
				Game.LogTrivial("YOBBINCALLOUTS: Finished Spawning Suspects.");
				Game.LogTrivial("YOBBINCALLOUTS: There will be " + this.Peeps.ToString() + " Suspects Before one Enters the Bait Car.");
				Game.LogTrivial("YOBBINCALLOUTS: Starting Bait Car Event.");
				Game.DisplayNotification("Dispatch, We are Starting the ~b~Bait Car.");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Bait Car Operation Started");
				}
				this.BaitVehicleBlip.Delete();
				this.BaitVehicleBlip = CallHandler.AssignBlip(this.BaitVehicle, Color.Green, 1f, "Bait Car", false, 1f);
				this.OfficerBlip = new Blip(this.BaitVehicle.Position, 50f);
				this.OfficerBlip.Alpha = 0.5f;
				this.OfficerBlip.Color = Color.Green;
				this.OfficerBlip.Name = "Detection Radius";
				GameFiber.Wait(4000);
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Stay Outside the ~g~Green Circle~w~ to Avoid ~y~Detection.");
				}
				bool flag4 = EntityExtensions.Exists(this.Rando1);
				if (flag4)
				{
					this.Rando1 = this.Suspect;
				}
				this.Suspect.Tasks.FollowNavigationMeshToPosition(this.BaitVehicle.GetOffsetPositionRight(2f), this.BaitVehicle.Heading, 1.25f, 2f, -1).WaitForCompletion();
				while (!this.BaitVehicle.HasDriver)
				{
					if (this.Peeps >= 0)
					{
						Game.LogTrivial("YOBBINCALLOUTS: First Suspect Event Started");
						this.Suspect.Tasks.ClearImmediately();
						this.Suspect.Tasks.AchieveHeading(this.BaitVehicle.Heading + 90f).WaitForCompletion(500);
						this.Suspect.Tasks.PlayAnimation("missarmenian2lamar_idles", "idle_look_behind_left", 1f, 0).WaitForCompletion(1500);
						this.Suspect.Tasks.PlayAnimation("missarmenian2lamar_idles", "idle_look_behind_right", 1f, 0).WaitForCompletion(1500);
						bool flag5 = this.player.DistanceTo(this.BaitVehicle) >= 50f;
						if (flag5)
						{
							bool flag6 = this.Peeps == 0;
							if (flag6)
							{
								this.Suspect.Tasks.EnterVehicle(this.BaitVehicle, -1).WaitForCompletion();
								Game.LogTrivial("YOBBINCALLOUTS: First Suspect Entered Vehicle");
							}
							else
							{
								Game.LogTrivial("YOBBINCALLOUTS: First Suspect Did Not Enter Vehicle");
								this.Suspect.Dismiss();
							}
						}
						else
						{
							bool flag7 = EntityExtensions.Exists(this.Suspect);
							if (flag7)
							{
								this.Suspect.Dismiss();
							}
							GameFiber.Wait(2000);
							Game.DisplayHelp("You're ~r~Too Close~w~ to the ~g~Bait Car.~w~ Find a ~b~Discrete Location~w~ Further Away.");
							Game.LogTrivial("YOBBINCALLOUTS: Player too Close to First Suspect.");
							GameFiber.Wait(2000);
						}
					}
					bool hasDriver = this.BaitVehicle.HasDriver;
					if (hasDriver)
					{
						break;
					}
					if (this.Peeps >= 1)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Second Suspect Event Started");
						this.Rando2.IsVisible = true;
						this.Rando2.Tasks.FollowNavigationMeshToPosition(this.BaitVehicle.GetOffsetPositionRight(2f), this.BaitVehicle.Heading, 1.25f, 2f, -1).WaitForCompletion();
						this.Rando2.Tasks.ClearImmediately();
						this.Rando2.Tasks.AchieveHeading(this.BaitVehicle.Heading + 90f).WaitForCompletion(500);
						this.Rando2.Tasks.PlayAnimation("missarmenian2lamar_idles", "idle_look_behind_left", 1f, 0).WaitForCompletion(1500);
						this.Rando2.Tasks.PlayAnimation("missarmenian2lamar_idles", "idle_look_behind_right", 1f, 0).WaitForCompletion(1500);
						bool flag8 = this.player.DistanceTo(this.BaitVehicle) >= 50f;
						if (flag8)
						{
							bool flag9 = this.Peeps == 1 || this.Peeps == 2;
							if (flag9)
							{
								this.Rando2.Tasks.EnterVehicle(this.BaitVehicle, -1).WaitForCompletion();
								Game.LogTrivial("YOBBINCALLOUTS: Second Suspect Entered Vehicle");
							}
							else
							{
								Game.LogTrivial("YOBBINCALLOUTS: Second Suspect Did not Enter Vehicle");
								this.Rando1.Dismiss();
							}
						}
						else
						{
							bool flag10 = EntityExtensions.Exists(this.Rando2);
							if (flag10)
							{
								this.Rando2.Dismiss();
							}
							GameFiber.Wait(2000);
							Game.DisplayHelp("You're ~r~Too Close~w~ to the ~g~Bait Car.~w~ Find a ~b~Discrete Location~w~ Further Away.");
							Game.LogTrivial("YOBBINCALLOUTS: Player too Close to Second Suspect");
							GameFiber.Wait(2000);
						}
					}
					bool hasDriver2 = this.BaitVehicle.HasDriver;
					if (hasDriver2)
					{
						break;
					}
					bool flag11 = this.Peeps >= 2;
					if (flag11)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Third and Final Suspect Event Started");
						this.Rando2.Tasks.ClearImmediately();
						this.Rando2.Tasks.AchieveHeading(this.BaitVehicle.Heading + 90f).WaitForCompletion(500);
						this.Rando2.Tasks.PlayAnimation("missarmenian2lamar_idles", "idle_look_behind_left", 1f, 0).WaitForCompletion(1500);
						this.Rando2.Tasks.PlayAnimation("missarmenian2lamar_idles", "idle_look_behind_right", 1f, 0).WaitForCompletion(1500);
						this.Rando2.Dismiss();
						GameFiber.Wait(1500);
						Game.DisplayHelp("Keep Monitoring the ~g~Bait Car.~w~ Parking the Car in ~o~High Crime~w~ Areas Will ~b~Increase~w~ the Chance of a ~o~Hit.");
						GameFiber.Wait(1500);
						this.Rando3.IsVisible = true;
						this.Rando3.Tasks.FollowNavigationMeshToPosition(this.BaitVehicle.GetOffsetPositionRight(2f), this.BaitVehicle.Heading, 1.25f, 2f, -1).WaitForCompletion();
						this.SuspectBlip = this.Rando3.AttachBlip();
						this.SuspectBlip.IsFriendly = false;
						this.SuspectBlip.Scale = 0.75f;
						this.Rando3.Tasks.ClearImmediately();
						this.Rando3.Tasks.AchieveHeading(this.BaitVehicle.Heading + 90f).WaitForCompletion(500);
						this.Rando3.Tasks.PlayAnimation("missarmenian2lamar_idles", "idle_look_behind_left", 1f, 0).WaitForCompletion(1500);
						this.Rando3.Tasks.PlayAnimation("missarmenian2lamar_idles", "idle_look_behind_right", 1f, 0).WaitForCompletion(1500);
						bool flag12 = this.player.DistanceTo(this.BaitVehicle) >= 50f;
						if (flag12)
						{
							this.Rando3.Tasks.EnterVehicle(this.BaitVehicle, -1).WaitForCompletion();
							Game.LogTrivial("YOBBINCALLOUTS: Third Suspect Entered Vehicle");
							break;
						}
						bool flag13 = EntityExtensions.Exists(this.Rando3);
						if (flag13)
						{
							this.Rando3.Dismiss();
						}
						GameFiber.Wait(2000);
						Game.DisplayHelp("You're Still ~r~Too Close~w~ to the ~g~Bait Car.~y~ Try Again~w~ Some Other Time!");
						Game.LogTrivial("YOBBINCALLOUTS: Third Suspect Did not Enter Vehicle, Player Still too close (noob lol)");
						bool flag14 = EntityExtensions.Exists(this.SuspectBlip);
						if (flag14)
						{
							this.SuspectBlip.Delete();
						}
						GameFiber.Wait(2000);
						break;
					}
				}
			}
		}

		
		private void Stolen()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.LogTrivial("YOBBINCALLOUTS: A Suspect Has Entered the Bait Car");
				GameFiber.Wait(1000);
				Game.DisplaySubtitle("Dispatch, Someone Just ~r~Entered~w~ the ~g~Bait Car!", 3000);
				this.player.Tasks.PlayAnimation("random@arrests", "generic_radio_chatter", -1f, 48).WaitForCompletion(3000);
				GameFiber.Wait(3000);
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Suspect has Entered Bait Car.");
				}
				Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
				GameFiber.Wait(2000);
				Game.DisplayHelp("Perform a ~r~Traffic Stop~w~ on the Suspect.");
				this.BaitVehicleBlip.Delete();
				this.OfficerBlip.Delete();
				bool flag = this.Peeps == 0;
				if (flag)
				{
					this.MainSuspect = this.Suspect;
				}
				else
				{
					bool flag2 = this.Peeps == 1;
					if (flag2)
					{
						this.MainSuspect = this.Rando1;
					}
					else
					{
						bool flag3 = this.Peeps == 2;
						if (flag3)
						{
							this.MainSuspect = this.Rando2;
						}
					}
				}
				this.SuspectBlip = this.MainSuspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				this.SuspectBlip.Name = "Suspect";
				bool flag4 = this.MainSuspect.IsAlive && !Functions.IsPedArrested(this.MainSuspect);
				if (flag4)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Suspect is Driving Away");
					try
					{
						this.MainSuspect.Tasks.CruiseWithVehicle(15f, 1);
					}
					catch (Exception)
					{
						Game.DisplayNotification("There was an ~r~Error~w~ in Getting the Driver to Move. ~w~Please Check Your ~g~Log File.~w~Sorry for the Inconvenience!");
						Game.LogTrivial("YOBBINCALLOUTS: Crash Making Bait Car Drivable/Task Invoker. Sorry for the Inconvenience.");
						this.End();
					}
				}
				Game.LogTrivial("YOBBINCALLOUTS: Suspect Started Driving Away, Waiting for a Pullover.");
				while (!Functions.IsPlayerPerformingPullover() && this.MainSuspect.IsAlive)
				{
					GameFiber.Wait(0);
				}
				bool flag5 = this.MainScenario == 0;
				if (flag5)
				{
					GameFiber.Wait(2000);
					Game.LogTrivial("YOBBINCALLOUTS: Suspect Pursuit Started");
					Functions.ForceEndCurrentPullover();
					this.MainPursuit = Functions.CreatePursuit();
					Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
					bool calloutInterface2 = Main.CalloutInterface;
					if (calloutInterface2)
					{
						CalloutInterfaceHandler.SendMessage(this, "Suspect is Fleeing in the Bait Car");
					}
					Functions.SetPursuitIsActiveForPlayer(this.MainPursuit, true);
					Functions.AddPedToPursuit(this.MainPursuit, this.MainSuspect);
					GameFiber.Wait(1500);
					GameFiber.Wait(3500);
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Activate the ~b~Kill Switch ~w~and ~o~Stop~w~ the ~r~Suspect's Vehicle.");
					while (Functions.IsPursuitStillRunning(this.MainPursuit) && !Game.IsKeyDown(Config.MainInteractionKey))
					{
						GameFiber.Yield();
						bool flag6 = Game.IsKeyDown(Config.MainInteractionKey);
						if (flag6)
						{
							this.MainSuspect.CurrentVehicle.FuelLevel = 0f;
							GameFiber.Wait(500);
							Game.DisplayNotification("Dispatch, We Have ~b~Disabled~w~ the ~r~Bait Vehicle!");
							Game.LogTrivial("YOBBINCALLOUTS: Player Has Disabled Suspects Vehicle.");
							break;
						}
					}
					while (Functions.IsPursuitStillRunning(this.MainPursuit))
					{
						GameFiber.Wait(0);
					}
					bool flag7 = EntityExtensions.Exists(this.Suspect);
					if (flag7)
					{
						bool flag8 = Functions.IsPedArrested(this.Suspect);
						if (flag8)
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Following the Pursuit.");
						}
						else
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Pursuit.");
						}
					}
					else
					{
						GameFiber.Wait(1000);
						Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Pursuit.");
					}
					GameFiber.Wait(2000);
					Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
				}
				bool flag9 = EntityExtensions.Exists(this.Officer);
				if (flag9)
				{
					this.Officer.Delete();
				}
				this.SuspectBlip.Alpha = 0f;
			}
		}

		
		private void SpawnSuspects()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Started Suspect " + this.PeepsSpawned.ToString() + " Spawn");
			string[] Peds = new string[]
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
			try
			{
				bool flag = this.PeepsSpawned == 1;
				Ped tempsuspect;
				if (flag)
				{
					if (BaitCar.<>o__37.<>p__0 == null)
					{
						BaitCar.<>o__37.<>p__0 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
						{
							typeof(Vector3)
						}, typeof(BaitCar), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
						}));
					}
					Vector3 suspectspawn;
					BaitCar.<>o__37.<>p__0.Target(BaitCar.<>o__37.<>p__0, NativeFunction.Natives, World.GetNextPositionOnStreet(this.player.Position.Around(45f)), 360, ref suspectspawn);
					tempsuspect = new Ped(Peds[r2.Next(0, Peds.Length)], suspectspawn, 69f);
				}
				else
				{
					bool flag2 = this.PeepsSpawned == 2;
					if (flag2)
					{
						tempsuspect = new Ped(Peds[r2.Next(0, Peds.Length)], this.Rando1.Position.Around(25f), 69f);
					}
					else
					{
						tempsuspect = new Ped(Peds[r2.Next(0, Peds.Length)], this.Rando2.Position.Around(15f), 69f);
					}
				}
				tempsuspect.IsPersistent = true;
				tempsuspect.BlockPermanentEvents = true;
				tempsuspect.IsVisible = false;
				tempsuspect.Tasks.Wander();
				bool flag3 = this.PeepsSpawned == 1;
				if (flag3)
				{
					this.Rando1 = tempsuspect;
				}
				else
				{
					bool flag4 = this.PeepsSpawned == 2;
					if (flag4)
					{
						this.Rando2 = tempsuspect;
					}
					else
					{
						this.Rando3 = tempsuspect;
					}
				}
			}
			catch (InvalidHandleableException)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Yobbincallouts Crash Prevented. InvalidHandleableException.");
				Game.DisplayNotification("~b~YobbinCallouts~r~ Crash~g~ Prevented.~w~ I Apologize for the ~y~Inconvenience.");
				this.End();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Finished Suspect " + this.PeepsSpawned.ToString() + " Spawn");
			this.PeepsSpawned++;
		}

		
		private Vector3 MainSpawnPoint;

		
		private Blip AreaBlip;

		
		private Blip SuspectBlip;

		
		private Blip OfficerBlip;

		
		private Blip BaitVehicleBlip;

		
		private Ped Suspect;

		
		private Ped Rando1;

		
		private Ped Rando2;

		
		private Ped Rando3;

		
		private Ped Officer;

		
		private Ped MainSuspect;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private int MainScenario;

		
		private int WaitTime;

		
		private int Peeps;

		
		private int PeepsSpawned = 1;

		
		private bool CalloutRunning = false;

		
		private bool HighCrime = false;

		
		private string Zone;

		
		private LHandle MainPursuit;

		
		private Vehicle BaitVehicle;

		
		private Vehicle OfficerVehicle;

		
		private Vehicle PlayerVehicle;

		
		private uint VehicleHash;

		
		private float VehicleHeading;

		
		private float BaitCarHeading;

		
		private int SuspectModel;

		
		private readonly List<string> OpeningDialogue1 = new List<string>
		{
			"~b~Officer: ~w~Hey, hope you're doing well Officer.",
			"~b~Officer: ~w~As part of a new effort to reduce vehicle theft, we've decided to run a bait car setup in a high crime area.",
			"~b~Officer: ~w~The ~g~bait car~w~ is parked down the street.",
			"~b~Officer: ~w~Park your cruiser in a nondescript location in view of the car.",
			"~g~You:~w~ Sounds good, hopefully we catch some people!",
			"~b~Officer:~w~ One more thing, if someone drives off, you can remotely switch the car off using ~y~" + Config.MainInteractionKey.ToString() + "~w~.",
			"~b~Officer:~w~ The car will have to move around 100 metres from the start point before you can shut the engine off."
		};
	}
}
