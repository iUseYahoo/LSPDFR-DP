using System;
using System.Drawing;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace LeroyCalloutsV2.Callouts
{
	
	[CalloutInfo("LC - PersonVandalizingVehicle", 2)]
	internal class PersonVandalizingVehicle : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Person Vandalizng Vehicle";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPoint = Common.FindSpawnPoint(1, true, "ATTENTION_ALL_UNITS_GENERIC CITIZENS_REPORT CRIME_MALICIOUS_VEHICLE_DAMAGE_01 IN_OR_ON_POSITION", 3, 9000);
				base.ShowCalloutAreaBlipBeforeAccepting(this.spawnPoint, 30f);
				base.AddMinimumDistanceCheck(100f, this.spawnPoint);
				base.CalloutMessage = Common.activeCall + "~n~~r~Code 3 Response";
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
						this.vehicle = Common.CreateVehicle(this.spawnPoint, Common.GetHeading(this.spawnPoint, 1), true);
						this.vehicleBlip = Common.CreateBlip(this.vehicle, Color.Purple);
						this.vehicleBlip.EnableRoute(Color.Yellow);
						Vector3 vector3_;
						Vector3 vector3_2;
						this.vehicle.Model.GetDimensions(ref vector3_, ref vector3_2);
						int damage = new Random().Next(10, 45);
						for (int index = 0; index < damage; index++)
						{
							float randomInt = MathHelper.GetRandomSingle(vector3_.X, vector3_2.X);
							float randomInt2 = MathHelper.GetRandomSingle(vector3_.Y, vector3_2.Y);
							float randomInt3 = MathHelper.GetRandomSingle(vector3_.Z, vector3_2.Z);
							this.vehicle.Deform(new Vector3(randomInt, randomInt2, randomInt3), 1000f, 10f);
						}
						this.suspect = Common.CreatePed(true, this.vehicle.GetOffsetPositionRight(-2f), true, true);
						this.suspect.Inventory.GiveNewWeapon("WEAPON_BAT", -1, true);
						this.suspect.Face(this.vehicle);
						this.outcome = Common.PickOutcome(2, -1);
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
						bool flag4 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.vehicle) < 20f;
						if (flag4)
						{
							Common.WriteToLog("Player arrived at vehicle");
							this.vehicleBlip.DisableRoute();
							this.suspect.Tasks.Clear();
							bool flag5 = this.outcome == 1;
							if (flag5)
							{
								this.pursuit = Functions.CreatePursuit();
								Functions.AddPedToPursuit(this.pursuit, this.suspect);
								Functions.SetPursuitIsActiveForPlayer(this.pursuit, true);
								Game.DisplayNotification("~b~You: ~s~Dispatch, suspect located. He's running!");
							}
							else
							{
								this.suspectBlip = this.suspect.AttachBlip();
								this.suspectBlip.Color = Color.Red;
								Game.DisplayNotification("~b~You: ~s~Dispatch, suspect located.");
								Game.DisplaySubtitle("~r~Suspect: ~s~Don't shoot! I won't run!");
								this.suspect.Tasks.PutHandsUp(-1, Game.LocalPlayer.Character);
								Game.DisplayHelp("Deal with the ~r~suspect ~s~as you see fit. Press ~y~" + this.end.ToString() + " ~s~to end the callout.");
							}
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag6 = !EntityExtensions.Exists(this.suspect) || Functions.IsPedArrested(this.suspect) || this.suspect.IsDead;
						if (flag6)
						{
							this.calloutRunning = false;
							Common.WriteToLog("Suspect arrested or dead.");
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
				bool flag3 = !EntityExtensions.Exists(this.vehicle) || !EntityExtensions.Exists(this.suspect);
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
			Common.Dismiss(this.suspect);
			Common.Dismiss(this.vehicle);
			Common.Dismiss(this.suspectBlip);
			Common.Dismiss(this.vehicleBlip);
			base.End();
		}

		
		private LHandle pursuit;

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private int outcome;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private Vehicle vehicle;

		
		private Blip vehicleBlip;
	}
}
