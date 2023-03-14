using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;
using StopThePed.API;
using YobbinCallouts.Utilities;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Broken Down Vehicle", 2)]
	public class BrokenDownVehicle : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Broken Down Vehicle Callout Start==========");
			Vector3 Spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(650f));
			try
			{
				if (BrokenDownVehicle.<>o__29.<>p__0 == null)
				{
					BrokenDownVehicle.<>o__29.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(BrokenDownVehicle), new CSharpArgumentInfo[]
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
				BrokenDownVehicle.<>o__29.<>p__0.Target(BrokenDownVehicle.<>o__29.<>p__0, NativeFunction.Natives, Spawn, ref nodePosition, ref heading, 1, 3f, 0);
				if (BrokenDownVehicle.<>o__29.<>p__2 == null)
				{
					BrokenDownVehicle.<>o__29.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(BrokenDownVehicle)));
				}
				Func<CallSite, object, bool> target = BrokenDownVehicle.<>o__29.<>p__2.Target;
				CallSite <>p__ = BrokenDownVehicle.<>o__29.<>p__2;
				if (BrokenDownVehicle.<>o__29.<>p__1 == null)
				{
					BrokenDownVehicle.<>o__29.<>p__1 = CallSite<<>F{00000010}<CallSite, object, Vector3, float, Vector3, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(BrokenDownVehicle), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				bool success = target(<>p__, BrokenDownVehicle.<>o__29.<>p__1.Target(BrokenDownVehicle.<>o__29.<>p__1, NativeFunction.Natives, Spawn, heading, ref outPosition));
				bool flag = success;
				if (!flag)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
					return false;
				}
				this.SpawnPoint = outPosition;
				this.VehicleHeading = heading;
			}
			catch (Exception)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
				return false;
			}
			Game.LogTrivial("YOBBINCALLOUTS: Successfully Found Spawn Point");
			base.ShowCalloutAreaBlipBeforeAccepting(this.SpawnPoint, 75f);
			base.AddMinimumDistanceCheck(50f, this.SpawnPoint);
			Functions.PlayScannerAudio("CITIZENS_REPORT CRIME_MOTOR_VEHICLE_ACCIDENT_01");
			base.CalloutMessage = "Broken Down Vehicle";
			base.CalloutPosition = this.SpawnPoint;
			base.CalloutAdvisory = "A Car Has Reportedly Stalled in the Middle of Traffic.";
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~b~Code 2~w~");
			}
			this.DriverVehicle = CallHandler.SpawnVehicle(this.SpawnPoint, this.VehicleHeading, true);
			this.DriverVehicle.IsPersistent = true;
			this.DriverVehicle.IsEngineOn = false;
			this.DriverVehicle.EngineHealth = 0f;
			this.DriverVehicle.IsDriveable = false;
			this.DriverVehicle.IsInvincible = true;
			this.DriverVehicle.IndicatorLightsStatus = 3;
			Game.LogTrivial("YOBBINCALLOUTS: Driver Vehicle Spawned");
			this.Driver = this.DriverVehicle.CreateRandomDriver();
			RandomCharacter.RandomizeCharacter(this.Driver);
			this.Driver.IsPersistent = true;
			this.Driver.BlockPermanentEvents = true;
			this.Driver.Tasks.CruiseWithVehicle(0f);
			Game.LogTrivial("YOBBINCALLOUTS: Driver Spawned");
			this.DriverVehicleBlip = this.DriverVehicle.AttachBlip();
			this.DriverVehicleBlip.Color = Color.Yellow;
			this.DriverVehicleBlip.IsRouteEnabled = true;
			this.DriverVehicleBlip.Name = "Broken Down Vehicle";
			bool flag = !this.CalloutRunning;
			if (flag)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Broken Down Vehicle Callout Not Accepted by User");
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
						GameFiber.Yield();
						while (this.player.DistanceTo(this.Driver) >= 30f)
						{
							GameFiber.Wait(0);
						}
						bool displayHelp = Config.DisplayHelp;
						if (displayHelp)
						{
							Game.DisplayHelp("Park Up Beside the ~y~Vehicle~w~, Then Approach When Ready.");
						}
						bool calloutInterface = Main.CalloutInterface;
						if (calloutInterface)
						{
							CalloutInterfaceHandler.SendMessage(this, "UNIT ON SCENE. Reporting one stalled vehicle, model " + this.DriverVehicle.Model.Name);
						}
						this.DriverBlip = new Blip(this.Driver);
						this.DriverBlip.Scale = 0.8f;
						this.DriverBlip.Color = Color.Blue;
						this.DriverBlip.Name = "Driver";
						Random r2 = new Random();
						this.Scenario = r2.Next(0, 2);
						while (this.player.DistanceTo(this.Driver) >= 6f)
						{
							GameFiber.Wait(0);
						}
						this.Driver.Tasks.LeaveVehicle(this.DriverVehicle, 0).WaitForCompletion();
						if (BrokenDownVehicle.<>o__32.<>p__0 == null)
						{
							BrokenDownVehicle.<>o__32.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(BrokenDownVehicle), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						BrokenDownVehicle.<>o__32.<>p__0.Target(BrokenDownVehicle.<>o__32.<>p__0, NativeFunction.Natives, this.Driver, this.player, -1);
						bool displayHelp2 = Config.DisplayHelp;
						if (displayHelp2)
						{
							Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Talk to the ~b~Driver.");
						}
						Random ODC = new Random();
						int OpeningDialogue = ODC.Next(0, 3);
						bool flag = OpeningDialogue == 0;
						if (flag)
						{
							CallHandler.Dialogue(this.OpeningDialogue1, this.Driver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
						else
						{
							bool flag2 = OpeningDialogue == 1;
							if (flag2)
							{
								CallHandler.Dialogue(this.OpeningDialogue2, this.Driver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
							else
							{
								CallHandler.Dialogue(this.OpeningDialogue3, this.Driver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
						}
						GameFiber.Wait(1500);
						bool flag3 = EntityExtensions.Exists(this.player.LastVehicle);
						if (flag3)
						{
							this.Driver.Tasks.FollowNavigationMeshToPosition(this.player.LastVehicle.GetOffsetPositionRight(1f), Game.LocalPlayer.Character.Heading - 180f, 1.25f).WaitForCompletion();
						}
						CallHandler.IdleAction(this.Driver, false);
						Game.DisplayHelp(string.Concat(new string[]
						{
							"~y~",
							Config.Key1.ToString(),
							":~b~ Call a Tow Truck.~y~ ",
							Config.Key2.ToString(),
							":~b~ Try to Fix the Car Yourself."
						}));
						while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
						{
							GameFiber.Wait(0);
						}
						bool flag4 = Game.IsKeyDown(Config.Key1);
						if (flag4)
						{
							this.TowTruckLogic();
						}
						else
						{
							this.FixCar();
						}
					}
					Game.LogTrivial("YOBBINCALLOUTS: Callout Finished, Ending...");
					bool calloutRunning = this.CalloutRunning;
					if (calloutRunning)
					{
						EndCalloutHandler.EndCallout();
					}
					this.End();
				}
				catch (Exception e)
				{
					bool calloutRunning2 = this.CalloutRunning;
					if (calloutRunning2)
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
			bool flag = EntityExtensions.Exists(this.Driver);
			if (flag)
			{
				bool flag2 = this.Driver.IsInAnyVehicle(false);
				if (flag2)
				{
					this.Driver.Tasks.LeaveVehicle(this.Driver.CurrentVehicle, 0).WaitForCompletion(4000);
				}
				this.Driver.Dismiss();
			}
			bool flag3 = EntityExtensions.Exists(this.DriverVehicle);
			if (flag3)
			{
				this.DriverVehicle.Dismiss();
			}
			bool flag4 = EntityExtensions.Exists(this.DriverBlip);
			if (flag4)
			{
				this.DriverBlip.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this.DriverVehicleBlip);
			if (flag5)
			{
				this.DriverVehicleBlip.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.TowTruckDriver);
			if (flag6)
			{
				this.TowTruckDriver.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.TowTruckBlip);
			if (flag7)
			{
				this.TowTruckBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Broken Down Vehicle Callout Cleaned Up");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void TowTruckLogic()
		{
			bool flag = !Main.STP;
			if (flag)
			{
				Game.DisplayHelp("Press " + Config.MainInteractionKey.ToString() + " to Call a ~b~Tow Truck.");
				while (!Game.IsKeyDown(Config.MainInteractionKey) && this.DriverVehicle.Speed < 1f)
				{
					GameFiber.Wait(0);
				}
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Requesting Tow Truck to Clear Disabled Vehicle.");
				}
				Game.DisplaySubtitle("Hey Dispatch, We Need a Tow Truck ASAP, Vehicle Blocking the Road.", 3500);
				Game.LocalPlayer.Character.Tasks.PlayAnimation("random@arrests", "generic_radio_chatter", -1f, 48).WaitForCompletion(4000);
				GameFiber.Wait(3000);
				Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
				GameFiber.Wait(1000);
				bool calloutInterface2 = Main.CalloutInterface;
				if (calloutInterface2)
				{
					CalloutInterfaceHandler.SendMessage(this, "Tow Truck is en Route.");
				}
				else
				{
					Game.DisplayHelp("Tow Truck is ~g~En Route!", 3500);
				}
				this.SpawnPointTruck = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(200f));
				this.TowTruck = new Vehicle("TOWTRUCK", this.SpawnPointTruck, 180f);
				this.TowTruck.IsPersistent = true;
				Game.LogTrivial("YOBBINCALLOUTS: Tow Truck Has Been Spawned");
				this.TowTruckDriver = this.TowTruck.CreateRandomDriver();
				this.TowTruckDriver.IsPersistent = true;
				this.TowTruckDriver.BlockPermanentEvents = true;
				this.TowTruckBlip = this.TowTruck.AttachBlip();
				this.TowTruckBlip.IsFriendly = true;
				this.TowTruckBlip.Name = "Tow Truck";
				this.TowTruckDriver.Tasks.DriveToPosition(this.DriverVehicle.GetOffsetPositionFront(7f), 25f, 262710, 15f);
				bool flag2 = EntityExtensions.Exists(this.TowTruck);
				if (flag2)
				{
					GameFiber.Wait(6000);
					Game.DisplayHelp("~b~Tow Truck~w~ Taking Too Long? Press " + Config.MainInteractionKey.ToString() + " To ~g~Warp~w~ it Closer.");
					while (this.TowTruck.DistanceTo(this.DriverVehicle) >= 30f && !Game.IsKeyDown(Config.MainInteractionKey))
					{
						GameFiber.Wait(0);
					}
					bool flag3 = this.TowTruck.DistanceTo(this.DriverVehicle) >= 30f && Game.IsKeyDown(Config.MainInteractionKey);
					if (flag3)
					{
						Game.DisplayNotification("~y~Warping~w~ Tow Truck Now, Please Wait.");
						GameFiber.Wait(2000);
						this.TowTruck.Position = this.DriverVehicle.GetOffsetPositionFront(7f);
						this.TowTruck.Heading = this.DriverVehicle.Heading;
						this.TowTruckWarped = true;
					}
					while (!this.TowTruckWarped && this.TowTruck.DistanceTo(this.DriverVehicle) >= 10f && this.TowTruck.DistanceTo(this.DriverVehicle) <= 30f)
					{
						GameFiber.Wait(0);
					}
					bool flag4 = !this.TowTruckWarped && this.TowTruck.DistanceTo(this.DriverVehicle) <= 10f;
					if (flag4)
					{
						this.TowTruck.Position = this.DriverVehicle.GetOffsetPositionFront(10f);
						this.TowTruck.Heading = this.DriverVehicle.Heading;
						this.TowTruckWarped = true;
					}
					GameFiber.Wait(2000);
					this.TowTruckDriver.Tasks.LeaveVehicle(this.TowTruck, 256).WaitForCompletion();
					this.TowTruckDriver.Tasks.ClearImmediately();
					GameFiber.Wait(2000);
					Game.DisplayHelp("Talk to the ~b~Tow Truck Driver.");
					while (Game.LocalPlayer.Character.DistanceTo(this.TowTruckDriver) >= 2.5f)
					{
						GameFiber.Wait(0);
					}
					this.TowTruckDriver.Tasks.AchieveHeading(Game.LocalPlayer.Character.Heading - 180f).WaitForCompletion(750);
					bool displayHelp = Config.DisplayHelp;
					if (displayHelp)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Advance the ~b~Tow Truck Driver.");
					}
					CallHandler.Dialogue(this.TowTruckDialogue1, this.TowTruckDriver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					GameFiber.Wait(2500);
					Game.DisplaySubtitle("~b~I'm Attaching the Vehicle in Three Seconds, Please Stand Clear!");
					GameFiber.Wait(3000);
					this.TowTruck.TowVehicle(this.DriverVehicle, true);
					GameFiber.Wait(1000);
					this.TowTruckDriver.Dismiss();
					bool flag5 = EntityExtensions.Exists(this.TowTruckBlip);
					if (flag5)
					{
						this.TowTruckBlip.Delete();
					}
					bool flag6 = EntityExtensions.Exists(this.DriverVehicleBlip);
					if (flag6)
					{
						this.DriverVehicleBlip.Delete();
					}
				}
			}
			else
			{
				try
				{
					Functions.callTowService();
				}
				catch (Exception e)
				{
					Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT IN CALLING STOPTHEPED TOW SERVICE==========");
					Game.LogTrivial("IN: " + ((this != null) ? this.ToString() : null));
					string str = "EXCEPTION: ";
					Exception ex = e;
					Game.LogTrivial(str + ((ex != null) ? ex.ToString() : null));
					Game.DisplayHelp("Use ~y~StopThePed~w~ to Call a ~b~Tow Truck.");
				}
				while (this.DriverVehicle.DistanceTo(this.SpawnPoint) <= 5f)
				{
					GameFiber.Wait(0);
				}
				bool flag7 = EntityExtensions.Exists(this.DriverVehicleBlip);
				if (flag7)
				{
					this.DriverVehicleBlip.Delete();
				}
			}
			GameFiber.Wait(1500);
			bool calloutInterface3 = Main.CalloutInterface;
			if (calloutInterface3)
			{
				CalloutInterfaceHandler.SendMessage(this, "Tow Truck has Cleared the Disabled Vehicle.");
			}
			else
			{
				Game.DisplayNotification("Tow Truck has Collected ~g~Vehicle,~w~ Towing to Repair Lot.");
			}
			this.Ending();
		}

		
		private void FixCar()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Player Has Started Attempting to Repair Vehicle.");
				this.DriverVehicleBlip.Delete();
				Game.DisplayHelp("Go to the ~b~Hood~w~ of the Vehicle to Try and ~y~Repair~w~ it.");
				bool flag = this.DriverVehicle.HasBone("bonnet");
				if (flag)
				{
					this.DriverHood = this.DriverVehicle.GetBonePosition("bonnet");
					string str = "YOBBINCALLOUTS: Driver Vehicle Hood Position at ";
					Vector3 driverHood = this.DriverHood;
					Game.LogTrivial(str + driverHood.ToString() + ".");
				}
				else
				{
					Game.DisplayNotification("Sorry, there was an ~r~Error~w~. Please Call a ~b~Tow Truck~w~ instead.");
					Game.LogTrivial("YOBBINCALLOUTS: Could not find bone for driver's vehicle. Player Must Call Tow Truck.");
					this.TowTruckLogic();
				}
				this.DriverVehicleBlip = new Blip(this.DriverHood);
				this.DriverVehicleBlip.Scale = 0.5f;
				this.DriverVehicleBlip.IsFriendly = true;
				while (Game.LocalPlayer.Character.DistanceTo2D(this.DriverVehicle.FrontPosition) >= 1.5f)
				{
					GameFiber.Wait(0);
				}
				Game.LocalPlayer.Character.Tasks.FollowNavigationMeshToPosition(this.DriverVehicle.FrontPosition, 2f, this.DriverVehicle.Heading - 180f, 0.05f, -1).WaitForCompletion(1500);
				Game.LocalPlayer.Character.Heading = this.DriverVehicle.Heading - 180f;
				Game.LogTrivial("YOBBINCALLOUTS: Player Has Started Looking at Vehicle's Hood.");
				this.DriverVehicle.OpenDoor(VehicleExtensions.Doors.Hood, false);
				Game.LogTrivial("YOBBINCALLOUTS: Opened the Hood");
				GameFiber.Wait(1000);
				Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to Look at the ~b~Engine.");
				while (!Game.IsKeyDown(Config.MainInteractionKey))
				{
					GameFiber.Wait(0);
				}
				Game.LocalPlayer.Character.Tasks.PlayAnimation("mini@repair", "fixing_a_ped", -1f, 1).WaitForCompletion(5000);
				GameFiber.Wait(2000);
				Game.LocalPlayer.Character.Tasks.ClearImmediately();
				GameFiber.Wait(1500);
				bool flag2 = this.Scenario == 0;
				if (flag2)
				{
					this.DriverVehicle.Repair();
					GameFiber.Wait(1000);
					Game.DisplaySubtitle("~g~You:~w~ Looks Like That Worked!", 3500);
					Game.LogTrivial("YOBBINCALLOUTS: Player fixed le car");
					this.DriverVehicle.CloseDoor(VehicleExtensions.Doors.Hood, false);
					Game.LogTrivial("YOBBINCALLOUTS: Closed the Hood");
					Game.DisplayHelp("Inform the ~b~Driver.");
					GameFiber.Wait(4500);
					if (BrokenDownVehicle.<>o__36.<>p__0 == null)
					{
						BrokenDownVehicle.<>o__36.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(BrokenDownVehicle), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					BrokenDownVehicle.<>o__36.<>p__0.Target(BrokenDownVehicle.<>o__36.<>p__0, NativeFunction.Natives, this.Driver, Game.LocalPlayer.Character, -1);
					while (Game.LocalPlayer.Character.DistanceTo(this.Driver) >= 5f)
					{
						GameFiber.Wait(0);
					}
					Game.LogTrivial("YOBBINCALLOUTS: Player Started Talking with Driver After Fixing the Car.");
					bool displayHelp = Config.DisplayHelp;
					if (displayHelp)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Inform the ~b~Driver.");
					}
					CallHandler.Dialogue(this.FixCarDialogue, this.Driver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					GameFiber.Wait(500);
					bool flag3 = EntityExtensions.Exists(this.DriverBlip);
					if (flag3)
					{
						this.DriverBlip.Delete();
					}
					bool flag4 = EntityExtensions.Exists(this.Driver);
					if (flag4)
					{
						this.Driver.Dismiss();
					}
				}
				else
				{
					GameFiber.Wait(1000);
					Game.DisplaySubtitle("~g~You:~w~ Damn, Looks Like I Couldn't Get it Working.", 3500);
					Game.LogTrivial("YOBBINCALLOUTS: Player did not fix le car");
					this.DriverVehicle.CloseDoor(VehicleExtensions.Doors.Hood, false);
					Game.LogTrivial("YOBBINCALLOUTS: Closed the Hood");
					bool flag5 = EntityExtensions.Exists(this.DriverVehicleBlip);
					if (flag5)
					{
						this.DriverVehicleBlip.Scale = 0.8f;
					}
					this.TowTruckLogic();
				}
			}
		}

		
		private void Ending()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				GameFiber.Wait(1500);
				bool flag = EntityExtensions.Exists(this.DriverVehicleBlip);
				if (flag)
				{
					this.DriverVehicleBlip.Delete();
				}
				while (Game.LocalPlayer.Character.DistanceTo(this.Driver) >= 2f)
				{
					Game.DisplayHelp("Talk to the ~b~Driver~w~ to Finish the Callout.");
					GameFiber.Wait(0);
				}
				this.Driver.Tasks.AchieveHeading(Game.LocalPlayer.Character.Heading - 180f).WaitForCompletion(1250);
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press~y~ " + Config.MainInteractionKey.ToString() + " ~w~to Talk to the ~b~Driver.");
				}
				Random r = new Random();
				int DriverEndingDialogue = r.Next(0, 4);
				bool flag2 = DriverEndingDialogue == 0;
				if (flag2)
				{
					CallHandler.Dialogue(this.DriverEndingDialogue1, this.Driver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				else
				{
					bool flag3 = DriverEndingDialogue == 1;
					if (flag3)
					{
						CallHandler.Dialogue(this.DriverEndingDialogue2, this.Driver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					else
					{
						bool flag4 = DriverEndingDialogue == 2;
						if (flag4)
						{
							CallHandler.Dialogue(this.DriverEndingDialogue3, this.Driver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
						else
						{
							CallHandler.Dialogue(this.DriverEndingDialogue4, this.Driver, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
					}
				}
				GameFiber.Wait(2000);
				this.Driver.Tasks.ClearImmediately();
				bool flag5 = DriverEndingDialogue >= 2;
				if (flag5)
				{
					this.DrivePeep();
				}
			}
		}

		
		private void DrivePeep()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				this.Driver.Tasks.AchieveHeading(Game.LocalPlayer.Character.Heading - 180f).WaitForCompletion(750);
				Game.DisplayHelp("Enter Your Vehicle Give the Driver a ~g~Ride~w~. To ~r~Decline~w~ the Ride, Press ~y~" + Config.CalloutEndKey.ToString());
				CallHandler.IdleAction(this.Driver, false);
				while (!Game.LocalPlayer.Character.IsInAnyPoliceVehicle && !Game.IsKeyDown(Config.CalloutEndKey))
				{
					GameFiber.Wait(0);
				}
				bool flag = Game.IsKeyDown(Config.CalloutEndKey);
				if (flag)
				{
					bool flag2 = EntityExtensions.Exists(this.Driver);
					if (flag2)
					{
						this.Driver.Tasks.ClearImmediately();
						this.Driver.ClearLastVehicle();
						this.Driver.Dismiss();
					}
					else
					{
						Game.DisplayNotification("Dispatch, ~b~Vehicle is Cleared.");
					}
					GameFiber.Wait(2000);
					bool calloutInterface = Main.CalloutInterface;
					if (calloutInterface)
					{
						CalloutInterfaceHandler.SendMessage(this, "Vehicle Has been Cleared, Code 4.");
					}
				}
				else
				{
					GameFiber.Wait(1000);
					Game.LogTrivial("YOBBINCALLOUTS: Player Has Accepted the Ride.");
					Game.DisplaySubtitle("~g~You:~w~ I Can Give You a Ride, Hop In!", 3000);
					bool calloutInterface2 = Main.CalloutInterface;
					if (calloutInterface2)
					{
						CalloutInterfaceHandler.SendMessage(this, "Unit is Giving the Citizen a Ride Home.");
					}
					GameFiber.Wait(2000);
					Game.DisplayHelp(string.Concat(new string[]
					{
						"~y~",
						Config.Key1.ToString(),
						": ~b~Tell the Driver to Enter the Passenger Seat. ~y~",
						Config.Key2.ToString(),
						":~b~ Tell the Driver to Enter the Rear Seat."
					}));
					while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
					{
						GameFiber.Wait(0);
					}
					bool flag3 = Game.IsKeyDown(Config.Key1);
					if (flag3)
					{
						this.SeatIndex = Game.LocalPlayer.Character.CurrentVehicle.GetFreePassengerSeatIndex().Value;
						this.Driver.Tasks.EnterVehicle(Game.LocalPlayer.Character.CurrentVehicle, this.SeatIndex, 0).WaitForCompletion();
					}
					else
					{
						this.SeatIndex = Game.LocalPlayer.Character.CurrentVehicle.GetFreeSeatIndex(1, 2).Value;
						this.Driver.Tasks.EnterVehicle(Game.LocalPlayer.Character.CurrentVehicle, this.SeatIndex, 0).WaitForCompletion();
					}
					bool flag4 = EntityExtensions.Exists(this.DriverBlip);
					if (flag4)
					{
						this.DriverBlip.Delete();
					}
					CallHandler.locationChooser(CallHandler.HouseList, 600f, 25f);
					bool locationReturned = CallHandler.locationReturned;
					if (locationReturned)
					{
						this.DriverDestination = CallHandler.SpawnPoint;
					}
					else
					{
						this.DriverDestination = World.GetNextPositionOnStreet(this.player.Position.Around(420f));
					}
					this.DriverBlip = new Blip(this.DriverDestination);
					this.DriverBlip.Color = Color.Green;
					this.DriverBlip.IsRouteEnabled = true;
					this.DriverBlip.Name = "Destination";
					GameFiber.Wait(1000);
					Game.DisplayHelp("Drive to the ~g~Location~w~ Marked on the Map.");
					while (this.Driver.DistanceTo(this.DriverDestination) >= 35f && !Game.IsKeyDown(Config.CalloutEndKey))
					{
						GameFiber.Wait(0);
					}
					bool flag5 = Game.IsKeyDown(Config.CalloutEndKey);
					if (flag5)
					{
						this.End();
					}
					Game.DisplayHelp("Stop Your Vehicle to Let the ~b~Driver ~w~Out.");
					while (Game.LocalPlayer.Character.CurrentVehicle.Speed > 0f)
					{
						GameFiber.Wait(0);
					}
					this.Driver.PlayAmbientSpeech("generic_thanks");
					this.Driver.Tasks.LeaveVehicle(Game.LocalPlayer.Character.CurrentVehicle, 0).WaitForCompletion();
					bool flag6 = EntityExtensions.Exists(this.DriverBlip);
					if (flag6)
					{
						this.DriverBlip.Delete();
					}
					GameFiber.StartNew(delegate()
					{
						this.Driver.Tasks.FollowNavigationMeshToPosition(this.DriverDestination, 69f, 1.25f, -1).WaitForCompletion();
					});
					GameFiber.Wait(1000);
					bool calloutInterface3 = Main.CalloutInterface;
					if (calloutInterface3)
					{
						CalloutInterfaceHandler.SendMessage(this, "Vehicle Has been Cleared, Code 4.");
					}
					else
					{
						Game.DisplayNotification("Dispatch, ~b~Vehicle is Cleared.~w~ We Have ~b~Given the Driver~w~ a Ride From the Scene.");
					}
				}
			}
		}

		
		private void CarFire()
		{
			this.Driver.Tasks.LeaveVehicle(this.DriverVehicle, 0).WaitForCompletion();
			this.Driver.Tasks.AchieveHeading(Game.LocalPlayer.Character.Heading - 180f).WaitForCompletion(750);
			bool displayHelp = Config.DisplayHelp;
			if (displayHelp)
			{
				Game.DisplayHelp("Press " + Config.MainInteractionKey.ToString() + " to Advance the Conversation.");
			}
			Random FODC = new Random();
			int OpeningDialogue = FODC.Next(0, 0);
			int num = OpeningDialogue;
			int num2 = num;
			if (num2 == 0)
			{
				while (this.OpeningDialogueCount < this.OpeningDialogue4.Count)
				{
					GameFiber.Yield();
					bool flag = Game.IsKeyDown(Config.MainInteractionKey);
					if (flag)
					{
						this.Driver.Tasks.PlayAnimation("missfbi3_party_d", "stand_talk_loop_a_male2", -1f, 1);
						Game.DisplaySubtitle(this.OpeningDialogue4[this.OpeningDialogueCount]);
						this.OpeningDialogueCount++;
					}
				}
			}
			this.DriverVehicle.EngineHealth = -1f;
			this.DriverVehicle.IsOnFire = true;
			Game.DisplaySubtitle("Shit, Your Car's on Fire! Take Cover, Now!", 2500);
			GameFiber.Wait(2000);
			this.Driver.Tasks.FollowNavigationMeshToPosition(Game.LocalPlayer.Character.LastVehicle.GetOffsetPositionFront(1f), Game.LocalPlayer.Character.Heading - 180f, 2.25f).WaitForCompletion(6000);
			GameFiber.Wait(500);
			this.DriverVehicle.Explode();
			Game.LogTrivial("YOBBINCALLOUTS: Vehicle Has Exploded");
			GameFiber.Wait(750);
			Game.DisplaySubtitle("Holy Crap!", 1500);
			this.DriverVehicleBlip.Delete();
			this.DriverBlip = new Blip(this.Driver)
			{
				Scale = 0.8f,
				Color = Color.Blue
			};
			GameFiber.Wait(1000);
			Game.DisplayNotification("~r~Fire Department~w~ is En Route!");
			Functions.RequestBackup(this.DriverVehicle.Position, 1, 7);
			Game.LogTrivial("YOBBINCALLOUTS: Fire Department Has Been Called");
			GameFiber.Wait(4500);
			Game.DisplayHelp("Once You are Done, Go to the ~b~Driver ~w~to Speak With Them.");
			while (Game.LocalPlayer.Character.DistanceTo(this.Driver) >= 2.5f)
			{
				GameFiber.Wait(0);
			}
		}

		
		private Ped Driver;

		
		private Ped TowTruckDriver;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private Vector3 SpawnPoint;

		
		private Vector3 SpawnPointTruck;

		
		private Vector3 DriverDestination;

		
		private Vector3 DriverHood;

		
		private Blip DriverBlip;

		
		private Blip DriverVehicleBlip;

		
		private Blip TowTruckBlip;

		
		private Vehicle DriverVehicle;

		
		private Vehicle TowTruck;

		
		private bool TowTruckWarped = false;

		
		private string Zone;

		
		private int SeatIndex;

		
		private float VehicleHeading;

		
		private int Scenario;

		
		private bool CalloutRunning = false;

		
		private readonly List<string> OpeningDialogue1 = new List<string>
		{
			"~b~Driver:~w~ Thanks for getting here so quickly, officer!",
			"~g~You:~w~ No problem, what seems to be the issue?",
			"~b~Driver:~w~ My car just stopped in the middle of the road! No warning, it just died!",
			"~g~You:~w~ Wow, that's quite bad luck! you're in a very dangerous spot, we have to move you're vehicle as soon as possible.",
			"~b~Driver:~w~ I know! I have no idea what's wrong with it, i've tried restarting the engine, but it never works!",
			"~g~You:~w~ Okay, let me sort this out. how about you stand by my car for the time being?",
			"~b~Driver:~w~ Sounds good, officer."
		};

		
		private readonly List<string> OpeningDialogue2 = new List<string>
		{
			"~b~Driver:~w~ Thank god you're here officer! My car died on me!",
			"~g~You:~w~ Were there any signs of damage before it stopped working?",
			"~b~Driver:~w~ No officer, one minute it was working fine, the next it stopped! I didn't have time to react!",
			"~g~You:~w~ Okay, calm down. we have to move your car as soon as possible.",
			"~g~You:~w~ I'll see what I can do. can you stand over there by my car, to stay out of traffic?",
			"~b~Driver:~w~ Of course officer. Thanks so much!"
		};

		
		private readonly List<string> OpeningDialogue3 = new List<string>
		{
			"~b~Driver:~w~ Hey Officer, sorry about the inconvience my vehicle has caused here.",
			"~g~You:~w~ What happened to it?",
			"~b~Driver:~w~ It just died on me! It's been giving me problems for a while and today it boiled over.",
			"~g~You:~w~ Well, you're vehicle is quite old. you're probably going to have to get a new one after this!",
			"~b~Driver:~w~ , I know! anyways, my phone died, so I didn't call anyone to fix it yet. could you sort that out for me?",
			"~g~You:~w~ Of course. could you do me a favour and go over to my car, so you can get off the road?",
			"~b~Driver:~w~ Yeah sure. thanks!"
		};

		
		private readonly List<string> OpeningDialogue4 = new List<string>
		{
			"~b~Driver:~w~ Thank God You're Here Officer! My Car Overheated!",
			"~g~You:~w~ Is it Possible to Move it Out of the Way, or is it Dead?",
			"~b~Driver:~w~ It Doesn't Move at All. I Was Really Scared to Leave My Vehicle in the Middle of Traffic Like This!"
		};

		
		private int OpeningDialogueCount;

		
		private readonly List<string> TowTruckDialogue1 = new List<string>
		{
			"~g~You:~w~ Thanks for getting here so quickly!",
			"~b~Tow Truck Driver:~w~ Not a problem. Quite a bad place to break down, Eh?",
			"~g~You:~w~ Yes, the Driver said the vehicle just died for no apparent reason.",
			"~b~Tow Truck Driver:~w~ Well, I'll get this out of the road now, Officer!"
		};

		
		private readonly List<string> DriverEndingDialogue1 = new List<string>
		{
			"~b~Driver:~w~ ~Wow, that was really stressful! Thanks again for your help, Officer!",
			"~g~You:~w~ No worries! You'll get a phone call later today with all the information you need regarding your vehicle.",
			"~b~Driver:~w~ Alright, sounds good. Take care!"
		};

		
		private readonly List<string> DriverEndingDialogue2 = new List<string>
		{
			"~b~Driver:~w~ Appreciate the help Officer!",
			"~g~You:~w~ Of course! You should get a phone call soon with all the details regarding your car.",
			"~b~Driver:~w~ Sounds good officer. have a great day!"
		};

		
		private readonly List<string> DriverEndingDialogue3 = new List<string>
		{
			"~b~Driver:~w~ Wow, that was really stressful! Thanks again for your help, officer!",
			"~g~You:~w~ No worries! You'll get a phone call later today with all the information you need regarding your vehicle.",
			"~b~Driver:~w~ Alright, sounds good. Mind if I ask you for another favor, officer?",
			"~g~You:~w~ Go ahead.",
			"~b~Driver:~w~ Could I get a lift to my place? It isn't very far away. I would really appreciate it!",
			"~b~Driver:~w~ Don't worry if you can't, I know you guys are pretty busy these days."
		};

		
		private readonly List<string> DriverEndingDialogue4 = new List<string>
		{
			"~b~Driver:~w~ Appreciate the help officer!",
			"~g~You:~w~ Of course! You should get a phone call soon with all the details regarding your car.",
			"~b~Driver:~w~ Sounds good officer. Could I ask one more thing of you, officer?",
			"~g~You:~w~ Go for it.",
			"~b~Driver:~w~ I'm running late to get back home, do you think you could give me a ride there?",
			"~b~Driver:~w~ It's not too far away. No worries if you can't, I know you're busy and all."
		};

		
		private readonly List<string> FixCarDialogue = new List<string>
		{
			"~g~You:~w~ I managed to get your car working again. Turned out your battery died.",
			"~b~Driver:~w~ Oh no! Do I have to get a new one?",
			"~g~You:~w~ I jumped your car, so you should be good to go.",
			"~b~Driver:~w~ Oh thank god, you're a lifesaver. Thanks so much officer.",
			"~g~You:~w~ No worries, drive safe!"
		};
	}
}
