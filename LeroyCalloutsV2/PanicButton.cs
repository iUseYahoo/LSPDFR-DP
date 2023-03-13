using System;
using System.Drawing;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using Rage.Native;

namespace LeroyCalloutsV2.Callouts
{
	
	[CalloutInfo("LC - PanicButton", 2)]
	internal class PanicButton : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Panic Button";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPoint = Common.FindSpawnPoint(1, true, "INTRO_01 REQUEST_BACKUP DISPATCH_INTRO ATTENTION_ALL_UNITS CRIME_SHOTS_FIRED_AT_AN_OFFICER UNITS_RESPOND_CODE_99", 3, 10000);
				base.ShowCalloutAreaBlipBeforeAccepting(this.spawnPoint, 30f);
				base.AddMinimumDistanceCheck(100f, this.spawnPoint);
				base.CalloutMessage = "Officer Pressed Panic Button~n~~r~Shots Fired Code 99";
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
						Common.WriteToLog("Remove other vehicle");
						Vehicle[] allVehicles = World.GetAllVehicles();
						foreach (Vehicle vehicle in allVehicles)
						{
							bool flag = !vehicle;
							if (!flag)
							{
								bool flag2 = vehicle.DistanceTo2D(this.spawnPoint) > 20f;
								if (!flag2)
								{
									bool flag3 = vehicle != Game.LocalPlayer.Character.LastVehicle;
									if (flag3)
									{
										vehicle.Delete();
									}
								}
							}
						}
						Common.WriteToLog("suspect vehicle");
						this.SuspectVehicle = Common.CreateVehicle(this.spawnPoint, Common.GetHeading(this.spawnPoint, 1), true);
						Common.WriteToLog("suspect one");
						this.SuspectOne = Common.CreatePed(true, this.SuspectVehicle.GetOffsetPositionRight(-1f), true, true);
						this.SuspectOne.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, true);
						this.SuspectOne.WarpIntoVehicle(this.SuspectVehicle, -1);
						this.SuspectOne.RelationshipGroup = "suspects";
						this.SuspectOneBlip = Common.CreateBlip(this.SuspectOne, Color.Red);
						Common.WriteToLog("suspect two");
						this.SuspectTwo = Common.CreatePed(true, this.SuspectVehicle.GetOffsetPositionRight(1f), true, true);
						this.SuspectTwoBlip = Common.CreateBlip(this.SuspectTwo, Color.Red);
						this.SuspectTwo.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, true);
						this.SuspectTwo.WarpIntoVehicle(this.SuspectVehicle, 0);
						this.SuspectTwo.RelationshipGroup = "suspects";
						this.SuspectOne.Tasks.LeaveVehicle(256);
						this.SuspectTwo.Tasks.LeaveVehicle(256);
						Common.WriteToLog("officer vehicle");
						bool flag4 = NativeFunction.CallByName<uint>("GET_HASH_OF_MAP_AREA_AT_COORDS", new NativeArgument[]
						{
							this.spawnPoint.X,
							this.spawnPoint.Y,
							this.spawnPoint.Z
						}) == Game.GetHashKey("city");
						if (flag4)
						{
							Common.WriteToLog("In city");
							this.OfficerVehicle = new Vehicle(Main.cityOfficerCar, this.SuspectVehicle.GetOffsetPositionFront(-10f), this.SuspectVehicle.Heading);
						}
						else
						{
							Common.WriteToLog("In county");
							this.OfficerVehicle = new Vehicle(Main.countyOfficerCar, this.SuspectVehicle.GetOffsetPositionFront(-10f), this.SuspectVehicle.Heading);
						}
						this.OfficerVehicle.IsPersistent = true;
						Common.WriteToLog("Spawning officer");
						bool flag5 = NativeFunction.CallByName<uint>("GET_HASH_OF_MAP_AREA_AT_COORDS", new NativeArgument[]
						{
							this.spawnPoint.X,
							this.spawnPoint.Y,
							this.spawnPoint.Z
						}) == Game.GetHashKey("city");
						if (flag5)
						{
							this.Officer = new Ped("s_m_y_cop_01", this.OfficerVehicle.GetOffsetPositionRight(-1f), this.SuspectVehicle.Heading);
						}
						else
						{
							this.Officer = new Ped("s_m_y_sheriff_01", this.OfficerVehicle.GetOffsetPositionRight(-1f), this.SuspectVehicle.Heading);
						}
						Functions.SetPedAsCop(this.Officer);
						this.Officer.WarpIntoVehicle(this.OfficerVehicle, -1);
						this.Officer.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, true);
						this.Officer.IsPersistent = true;
						this.OfficerBlip = this.Officer.AttachBlip();
						this.OfficerBlip.IsFriendly = true;
						this.OfficerBlip.EnableRoute(Color.Yellow);
						this.Officer.Tasks.LeaveVehicle(256);
						this.OfficerVehicle.IsSirenOn = true;
						Common.WriteToLog("All entities spawned");
					}
					catch (Exception ex2)
					{
						Common.WriteErrorToLog("Could not spawn all entities. Ending callout. This may be caused by one of GTA's \"Bermuda Triangles\".Move to another location and try again. Exception: " + ex2.ToString());
						this.End();
						return;
					}
					this.calloutRunning = true;
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag6 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.Officer) < 200f;
						if (flag6)
						{
							Common.WriteToLog("Player arrived at scene");
							this.Officer.BlockPermanentEvents = false;
							this.SuspectOne.BlockPermanentEvents = false;
							this.SuspectTwo.BlockPermanentEvents = false;
							Game.SetRelationshipBetweenRelationshipGroups("suspects", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("suspects", "PLAYER", 5);
							this.SuspectOne.Tasks.FightAgainstClosestHatedTarget(10f);
							this.SuspectTwo.Tasks.FightAgainstClosestHatedTarget(10f);
							this.OfficerBlip.DisableRoute();
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag7 = (!EntityExtensions.Exists(this.SuspectOne) && !EntityExtensions.Exists(this.SuspectTwo)) || (Functions.IsPedArrested(this.SuspectOne) && Functions.IsPedArrested(this.SuspectTwo)) || (this.SuspectOne.IsDead && this.SuspectTwo.IsDead);
						if (flag7)
						{
							this.calloutRunning = false;
							Common.WriteToLog("Suspects arrested or dead.");
							Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
							Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
							this.End();
							break;
						}
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
				bool flag3 = !EntityExtensions.Exists(this.Officer) || !EntityExtensions.Exists(this.SuspectOne) || !EntityExtensions.Exists(this.SuspectTwo);
				if (flag3)
				{
					this.calloutRunning = false;
					Common.WriteErrorToLog("Entity does not exist");
					Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
					Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
					this.End();
				}
			}
		}

		
		public override void End()
		{
			Common.WriteToLog("Ending Callout");
			Common.Dismiss(this.SuspectOne);
			Common.Dismiss(this.SuspectTwo);
			Common.Dismiss(this.Officer);
			Common.Dismiss(this.SuspectOneBlip);
			Common.Dismiss(this.SuspectTwoBlip);
			Common.Dismiss(this.OfficerBlip);
			base.End();
		}

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private Vehicle OfficerVehicle;

		
		private Vehicle SuspectVehicle;

		
		private Blip OfficerBlip;

		
		private Blip SuspectOneBlip;

		
		private Blip SuspectTwoBlip;

		
		private Ped SuspectOne;

		
		private Ped SuspectTwo;

		
		private Ped Officer;
	}
}
