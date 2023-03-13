using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace LeroyCalloutsV2.Callouts
{
	
	[CalloutInfo("LC - ElderlyCivilianLost", 2)]
	internal class ElderlyCivilianLost : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Elderly Civilian Lost";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPoint = Common.FindSpawnPoint(0, true, "ATTENTION_ALL_UNITS_GENERIC CRIME_CIVILIAN_NEEDING_ASSISTANCE IN_OR_ON_POSITION", 2, 8000);
				IEnumerable<Vector3> orderedHomes = from x in this.possibleHomes
				orderby x.DistanceTo(Game.LocalPlayer.Character.Position)
				select x;
				this.civHome = orderedHomes.ToArray<Vector3>()[1];
				base.ShowCalloutAreaBlipBeforeAccepting(this.spawnPoint, 30f);
				base.AddMinimumDistanceCheck(100f, this.spawnPoint);
				base.CalloutMessage = Common.activeCall + "~n~~y~Code 2 Response";
				base.CalloutPosition = this.spawnPoint;
				bool flag = this.spawnPoint != Vector3.Zero;
				if (flag)
				{
					result = base.OnBeforeCalloutDisplayed();
				}
				else
				{
					Common.WriteToLog("Unable to find valid spawn point.");
					result = false;
				}
			}
			catch (Exception ex)
			{
				Common.WriteErrorToLog("Before callout displayed: " + ex.ToString());
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			this.CalloutHandler();
			return base.OnCalloutAccepted();
		}

		
		private void CalloutHandler()
		{
			try
			{
				GameFiber.StartNew(delegate()
				{
					try
					{
						int civIndex = Common.PickOutcome(5, -1);
						civIndex--;
						this.elderlyCiv = new Ped(this.civModel[civIndex], this.spawnPoint, 90f)
						{
							IsPersistent = true,
							BlockPermanentEvents = true
						};
						this.searchArea = Common.CreateSearchArea(this.elderlyCiv.Position, 100f);
						this.searchArea.EnableRoute(Color.Yellow);
						Common.WriteToLog("All entities spawned");
					}
					catch (Exception ex2)
					{
						Common.WriteErrorToLog("Could not spawn all entities. Ending callout. This may be caused by one of GTA's \"Bermuda Triangles\".Move to another location and try again. Exception: " + ex2.ToString());
						this.End();
						return;
					}
					this.outcome = Common.PickOutcome(2, -1);
					Common.WriteToLog("Outcome: " + this.outcome.ToString());
					bool flag = this.outcome == 1;
					if (flag)
					{
						this.numOfDialog = this.dialogWithCivOne.Count;
					}
					else
					{
						this.numOfDialog = this.dialogWithCivTwo.Count;
					}
					this.calloutRunning = true;
					this.elderlyCiv.Tasks.Wander();
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						try
						{
							bool flag2 = this.elderlyCiv.DistanceTo2D(this.searchArea) > 100f;
							if (flag2)
							{
								this.searchArea.Position = this.elderlyCiv.Position;
							}
							bool flag3 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.elderlyCiv) < 30f;
							if (flag3)
							{
								Common.WriteToLog("Player arrived at civ");
								this.searchArea.DisableRoute();
								this.searchArea.Delete();
								this.civBlip = Common.CreateBlip(this.elderlyCiv, Color.Orange);
								this.elderlyCiv.Tasks.Clear();
								this.elderlyCiv.Tasks.StandStill(-1);
								if (ElderlyCivilianLost.<>o__33.<>p__0 == null)
								{
									ElderlyCivilianLost.<>o__33.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(ElderlyCivilianLost), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								ElderlyCivilianLost.<>o__33.<>p__0.Target(ElderlyCivilianLost.<>o__33.<>p__0, NativeFunction.Natives, this.elderlyCiv, Game.LocalPlayer.Character, -1);
								this.playerVehicle = Game.LocalPlayer.Character.LastVehicle;
								Game.DisplaySubtitle("~o~Civilan: ~s~Officer! Over here!");
								Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~o~elderly civilian ~s~to advance the conversation");
								break;
							}
						}
						catch (Exception ex3)
						{
							Common.WriteErrorToLog(ex3.ToString());
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag4 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.elderlyCiv) < 10f;
						if (flag4)
						{
							if (ElderlyCivilianLost.<>o__33.<>p__1 == null)
							{
								ElderlyCivilianLost.<>o__33.<>p__1 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(ElderlyCivilianLost), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							ElderlyCivilianLost.<>o__33.<>p__1.Target(ElderlyCivilianLost.<>o__33.<>p__1, NativeFunction.Natives, this.elderlyCiv, Game.LocalPlayer.Character, -1);
							bool flag5 = Game.IsKeyDown(this.talk);
							if (flag5)
							{
								Common.WriteToLog("Witness dialog index: " + this.dialogWithCivIndex.ToString());
								bool flag6 = this.outcome == 1;
								if (flag6)
								{
									Game.DisplaySubtitle(this.dialogWithCivOne[this.dialogWithCivIndex]);
								}
								else
								{
									Game.DisplaySubtitle(this.dialogWithCivTwo[this.dialogWithCivIndex]);
								}
								this.dialogWithCivIndex++;
								bool flag7 = this.dialogWithCivIndex == this.numOfDialog;
								if (flag7)
								{
									Common.WriteToLog("Dialog done");
									break;
								}
							}
						}
					}
					bool flag8 = this.outcome == 2;
					if (flag8)
					{
						while (this.calloutRunning)
						{
							GameFiber.Yield();
							bool flag9 = !this.ambulanceSpawned;
							if (flag9)
							{
								Common.WriteToLog("Spawning ambulance");
								this.ambulance = new Vehicle("ambulance", World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(100f, 200f)));
								this.ambulance.IsPersistent = true;
								this.ambulanceBlip = this.ambulance.AttachBlip();
								this.ambulanceBlip.IsFriendly = true;
								Common.WriteToLog("Spawning medicOne");
								this.medicOne = new Ped("s_m_m_paramedic_01", Vector3.Zero, 0f);
								this.medicOne.IsPersistent = true;
								this.medicOne.BlockPermanentEvents = true;
								this.medicOne.WarpIntoVehicle(this.ambulance, -1);
								Common.WriteToLog("Spawning medicTwo");
								this.medicTwo = new Ped("s_m_m_paramedic_01", Vector3.Zero, 0f);
								this.medicTwo.IsPersistent = true;
								this.medicTwo.BlockPermanentEvents = true;
								this.medicTwo.WarpIntoVehicle(this.ambulance, 0);
								Common.WriteToLog("Tasking medicOne drive to elderlyCiv");
								this.stopPos = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(5f, 20f));
								this.medicOne.Tasks.DriveToPosition(this.stopPos, 15f, -2147220938);
								this.ambulance.IsSirenOn = true;
								this.ambulanceSpawned = true;
								GameFiber.Wait(2000);
								Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~ to warp the ambulance.");
							}
							bool flag10 = this.ambulanceSpawned && Game.IsKeyDown(this.talk);
							if (flag10)
							{
								this.ambulance.Position = this.stopPos;
							}
							bool flag11 = this.ambulance.DistanceTo(this.stopPos) <= 3f && !this.ambulanceArrived;
							if (flag11)
							{
								Common.WriteToLog("Ambulance arrived at elderlyCiv");
								this.ambulanceArrived = true;
								this.medicOne.Tasks.Clear();
								GameFiber.Wait(1000);
								this.medicOne.Tasks.LeaveVehicle(256);
								this.medicTwo.Tasks.LeaveVehicle(256).WaitForCompletion(4000);
								bool flag12 = this.medicTwo.IsInAnyVehicle(false);
								if (flag12)
								{
									this.medicTwo.Position = this.ambulance.GetOffsetPositionRight(1f);
								}
								Common.WriteToLog("medicTwo left ambulance");
								this.medicTwo.Tasks.FollowNavigationMeshToPosition(this.elderlyCiv.GetOffsetPositionFront(1f), this.elderlyCiv.Heading - 180f, 4f).WaitForCompletion(12000);
								bool flag13 = this.medicTwo.DistanceTo(this.elderlyCiv) > 1.2f;
								if (flag13)
								{
									this.medicTwo.Position = this.elderlyCiv.GetOffsetPositionFront(1f);
								}
								Common.WriteToLog("medicTwo arrived at elderlyCiv");
								GameFiber.Wait(3000);
								Common.WriteToLog("Tasking medicTwo to enter ambulance");
								this.medicTwo.Tasks.EnterVehicle(this.ambulance, 15000, 0, 1f, 0);
								Common.WriteToLog("Tasking medicOne to enter ambulance");
								this.medicOne.Tasks.EnterVehicle(this.ambulance, -1);
								Common.WriteToLog("Tasking elderlyCiv to enter ambulance");
								this.elderlyCiv.Tasks.FollowNavigationMeshToPosition(this.ambulance.GetOffsetPositionFront(-1f), this.ambulance.Heading, 1f).WaitForCompletion(15000);
								bool flag14 = !this.medicTwo.IsInAnyVehicle(false);
								if (flag14)
								{
									this.medicTwo.WarpIntoVehicle(this.ambulance, 0);
								}
								bool flag15 = !this.medicOne.IsInAnyVehicle(false);
								if (flag15)
								{
									this.medicOne.WarpIntoVehicle(this.ambulance, -1);
								}
								Common.WriteToLog("medicTwo in ambulance. Deleting elderlyCiv");
								this.elderlyCiv.Delete();
								GameFiber.Wait(2000);
								Common.WriteToLog("Tasking medicOne to cruise");
								this.medicOne.Tasks.CruiseWithVehicle(20f, 262710);
								this.medicTwo.Tasks.Pause(-1);
								Game.DisplayNotification("~b~You: ~s~Dispatch, civilian is being transported to the hospital.");
								Functions.PlayScannerAudio("REPORT_RESPONSE_COPY");
								this.End();
							}
						}
					}
					else
					{
						while (this.calloutRunning)
						{
							GameFiber.Yield();
							bool flag16 = !this.displayOnce;
							if (flag16)
							{
								Game.DisplayHelp("Get in your vehicle and the ~o~civilian ~s~will follow. Or you can call a taxi using another script and press ~y~" + this.end.ToString() + " ~s~to end the callout.");
								this.displayOnce = true;
							}
							bool flag17 = EntityExtensions.Exists(this.elderlyCiv) && Game.LocalPlayer.Character.IsInAnyVehicle(false);
							if (flag17)
							{
								this.playerVehicle = Game.LocalPlayer.Character.CurrentVehicle;
								int? freeSeat = this.playerVehicle.GetFreePassengerSeatIndex();
								bool flag18 = freeSeat != null;
								if (flag18)
								{
									Common.WriteToLog("Civilian entering player vehicle");
									this.elderlyCiv.Tasks.EnterVehicle(this.playerVehicle, freeSeat.Value).WaitForCompletion(10000);
									bool flag19 = !this.elderlyCiv.IsInAnyVehicle(false);
									if (flag19)
									{
										this.elderlyCiv.WarpIntoVehicle(this.playerVehicle, freeSeat.Value);
									}
									break;
								}
								bool flag20 = !this.displayedSeatWarning;
								if (flag20)
								{
									Common.WriteToLog("Civilian was unable to find a valid seat");
									Game.DisplayNotification("The ~o~civilian ~s~is unable to find an open seat. Make sure your current vehicle model has at least one valid open seat. In some vehicle models, not all seats are recognizable.");
									this.displayedSeatWarning = true;
								}
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag21 = EntityExtensions.Exists(this.elderlyCiv) && this.elderlyCiv.IsInVehicle(this.playerVehicle, false);
						if (flag21)
						{
							Common.WriteToLog("Civilian in player vehicle. Marking civHome.");
							this.homeBlip = new Blip(this.civHome)
							{
								IsFriendly = true,
								Color = Color.Purple
							};
							this.homeBlip.EnableRoute(Color.Yellow);
							Game.DisplayHelp("Follow the ~y~route ~s~to drive the ~o~elderly civilian ~s~home.");
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag22 = Game.LocalPlayer.Character.DistanceTo2D(this.civHome) < 50f;
						if (flag22)
						{
							Common.WriteToLog("Deleting vehicles in drive");
							Vehicle[] allVehicles = World.GetAllVehicles();
							foreach (Vehicle vehicle in allVehicles)
							{
								bool flag23 = !vehicle;
								if (!flag23)
								{
									bool flag24 = vehicle.DistanceTo2D(this.civHome) > 5f;
									if (!flag24)
									{
										bool flag25 = vehicle != Game.LocalPlayer.Character.LastVehicle;
										if (flag25)
										{
											vehicle.Delete();
										}
									}
								}
							}
							Game.DisplaySubtitle("~o~Civilian: ~s~That's my place right up there.");
							Game.DisplayHelp("Stop your vehicle on the ~b~vehicle marker. ~s~The ~o~civilian ~s~will get out automatically.");
							this.homeBlip.DisableRoute();
							break;
						}
						if (ElderlyCivilianLost.<>o__33.<>p__2 == null)
						{
							ElderlyCivilianLost.<>o__33.<>p__2 = CallSite<<>A<CallSite, object, int, float, float, float, float, float, float, float, float, float, float, float, float, int, int, int, int, bool, bool, int, bool, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DRAW_MARKER", null, typeof(ElderlyCivilianLost), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						ElderlyCivilianLost.<>o__33.<>p__2.Target(ElderlyCivilianLost.<>o__33.<>p__2, NativeFunction.Natives, 36, this.civHome.X, this.civHome.Y, this.civHome.Z + 1f, 0f, 0f, 0f, 0f, 0f, 0f, 2f, 2f, 2f, 30, 144, 255, 90, true, true, 2, true, 0, 0, false);
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag26 = EntityExtensions.Exists(this.elderlyCiv) && Game.LocalPlayer.Character.DistanceTo2D(this.civHome) < 5f && this.playerVehicle.Speed == 0f;
						if (flag26)
						{
							Common.WriteToLog("Arrived at civHome. Civilian leaving player vehicle.");
							this.elderlyCiv.Tasks.LeaveVehicle(0).WaitForCompletion(4000);
							this.elderlyCiv.Face(Game.LocalPlayer.Character.Position);
							this.elderlyCiv.Tasks.PlayAnimation("anim@amb@waving@male", "ground_wave", 1f, 0);
							this.elderlyCiv.BlockPermanentEvents = false;
							Game.DisplaySubtitle("~o~Civilian: ~s~Thank you officer!");
							Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
							Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
							this.End();
						}
						if (ElderlyCivilianLost.<>o__33.<>p__3 == null)
						{
							ElderlyCivilianLost.<>o__33.<>p__3 = CallSite<<>A<CallSite, object, int, float, float, float, float, float, float, float, float, float, float, float, float, int, int, int, int, bool, bool, int, bool, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DRAW_MARKER", null, typeof(ElderlyCivilianLost), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						ElderlyCivilianLost.<>o__33.<>p__3.Target(ElderlyCivilianLost.<>o__33.<>p__3, NativeFunction.Natives, 36, this.civHome.X, this.civHome.Y, this.civHome.Z + 1f, 0f, 0f, 0f, 0f, 0f, 0f, 2f, 2f, 2f, 30, 144, 255, 90, true, true, 2, true, 0, 0, false);
					}
				});
			}
			catch (Exception ex)
			{
				Common.WriteErrorToLog(ex.ToString());
			}
		}

		
		public override void Process()
		{
			base.Process();
			bool flag = this.calloutRunning;
			if (flag)
			{
				bool flag2 = Game.IsKeyDown(this.end);
				if (flag2)
				{
					this.calloutRunning = false;
					Common.WriteToLog("Player requested end callout.");
					Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
					Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
					this.End();
				}
				bool flag3 = !EntityExtensions.Exists(this.elderlyCiv) || this.elderlyCiv.IsDead;
				if (flag3)
				{
					this.calloutRunning = false;
					Common.WriteErrorToLog("Entity does not exist");
					Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
					Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
					this.End();
				}
				bool flag4 = EntityExtensions.Exists(this.elderlyCiv) && this.elderlyCiv.IsInAnyVehicle(false) && this.elderlyCiv.CurrentVehicle != this.playerVehicle && !this.elderlyCiv.IsInAnyPoliceVehicle;
				if (flag4)
				{
					Common.WriteToLog("Civilian is in non-player vehicle");
					this.civInTaxi = true;
					Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
					Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
					this.End();
				}
			}
		}

		
		public override void End()
		{
			this.calloutRunning = false;
			Common.WriteToLog("Ending Callout");
			Common.Dismiss(this.elderlyCiv);
			Common.Dismiss(this.civBlip);
			Common.Dismiss(this.homeBlip);
			Common.Dismiss(this.searchArea);
			Common.Dismiss(this.ambulance);
			Common.Dismiss(this.medicTwo);
			Common.Dismiss(this.medicOne);
			Common.Dismiss(this.ambulanceBlip);
			base.End();
		}

		
		private Keys end = Main.EndCalloutKey;

		
		private Keys talk = Main.TalkKey;

		
		private Ped elderlyCiv;

		
		private Vehicle playerVehicle;

		
		private Vehicle ambulance;

		
		private Ped medicOne;

		
		private Ped medicTwo;

		
		private Blip civBlip;

		
		private Blip homeBlip;

		
		private Blip searchArea;

		
		private Blip ambulanceBlip;

		
		private Vector3 spawnPoint;

		
		private Vector3 civHome;

		
		private Vector3 stopPos;

		
		private int Index;

		
		private int numOfDialog;

		
		private int dialogWithCivIndex = 0;

		
		private Random num = new Random();

		
		private bool civReached = false;

		
		private bool homeFound = false;

		
		private bool civInTaxi = false;

		
		private bool displayedSeatWarning = false;

		
		private bool ambulanceArrived = false;

		
		private bool ambulanceSpawned = false;

		
		private bool calloutRunning;

		
		private bool displayOnce = false;

		
		private int outcome;

		
		private readonly List<string> dialogWithCivOne = new List<string>
		{
			"~b~You: ~s~Hello, did you call?",
			"~o~Civilian: ~s~Oh hello officer. Yes, that was me. I can't seem to remember where I am.",
			"~b~You: ~s~Ok. Where are you trying to go?",
			"~o~Civilian: ~s~I'm trying to get back home.",
			"~b~You: ~s~Tell me where you live and I'll drive you home. Or I can call you a taxi.",
			"~o~Civilian: ~s~Thank you! You're so kind.",
			"~b~You: ~s~No problem."
		};

		
		private readonly List<string> dialogWithCivTwo = new List<string>
		{
			"~b~You: ~s~Hello, did you call?",
			"~o~Civilian: ~s~Yes officer. I think I may have fell. I'm not sure who I am or where I am.",
			"~b~You: ~s~Ok. Do you remeber where you were heading?",
			"~o~Civilian: ~s~Home I think. But I'm not sure where I live. I can't find my ID or license either.",
			"~b~You: ~s~Ok, everything is going to be fine. I'm going to call you an ambulance so they can check you out at the hospital.",
			"~o~Civilian: ~s~Thank you officer.",
			"~b~You: ~s~No problem."
		};

		
		private readonly List<string> civModel = new List<string>
		{
			"a_f_o_genstreet_01",
			"a_m_o_genstreet_01",
			"cs_mrs_thornhill",
			"u_f_o_eileen"
		};

		
		private readonly List<Vector3> possibleHomes = new List<Vector3>
		{
			new Vector3(-38.31347f, -1446.813f, 31.00985f),
			new Vector3(-157.8855f, -1545.259f, 34.49948f),
			new Vector3(284.9991f, -1990.54f, 19.99873f),
			new Vector3(-985.6766f, -1108.47f, 1.559799f),
			new Vector3(-1171.929f, -1116.324f, 1.982988f),
			new Vector3(52.99039f, -52.33957f, 68.83619f),
			new Vector3(-273.0607f, 105.6128f, 68.38969f),
			new Vector3(-445.6445f, 107.0503f, 63.38292f),
			new Vector3(-631.3771f, 206.9325f, 73.46074f),
			new Vector3(-773.5261f, 306.0356f, 85.21845f),
			new Vector3(-845.1215f, 460.1257f, 87.31214f),
			new Vector3(-1106.37f, 555.7727f, 102.1323f),
			new Vector3(-1452.405f, 533.5774f, 118.7007f),
			new Vector3(-1951.001f, 401.4068f, 95.79191f),
			new Vector3(-1950.467f, 548.5787f, 113.562f),
			new Vector3(955.2603f, -599.7228f, 58.89166f),
			new Vector3(1101.434f, -418.6891f, 66.66598f),
			new Vector3(1360.345f, -602.9456f, 73.85144f),
			new Vector3(463.3181f, 2606.712f, 42.78747f),
			new Vector3(355.5217f, 2576.085f, 43.03484f),
			new Vector3(2170.366f, 3368.062f, 44.88914f),
			new Vector3(1684.019f, 4649.771f, 42.88552f),
			new Vector3(1724.585f, 4630.387f, 42.74358f),
			new Vector3(36.25834f, 6604.872f, 31.97209f),
			new Vector3(-396.0145f, 6311.505f, 28.4819f),
			new Vector3(-294.7897f, 6339.134f, 31.75342f),
			new Vector3(-225.0338f, 6436.215f, 30.71082f),
			new Vector3(205.5047f, 3039.071f, 42.46117f),
			new Vector3(1771.002f, 3751.336f, 33.36716f),
			new Vector3(1667.565f, 3827.206f, 34.41676f),
			new Vector3(1519.001f, 3714.442f, 33.84896f),
			new Vector3(1432.934f, 3662.182f, 33.72443f),
			new Vector3(-3181.312f, 1290.041f, 13.81223f),
			new Vector3(-3226.133f, 1079.23f, 10.37136f),
			new Vector3(-3098.148f, 307.6872f, 7.885136f),
			new Vector3(-1811.934f, -636.8777f, 10.45337f),
			new Vector3(1226.976f, -1604.859f, 51.26062f),
			new Vector3(1336.392f, -1529.775f, 52.96046f)
		};
	}
}
