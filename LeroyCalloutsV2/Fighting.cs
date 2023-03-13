using System;
using System.Drawing;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace LeroyCalloutsV2.Callouts
{
	
	[CalloutInfo("LC - Fighting", 2)]
	internal class Fighting : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Fighting";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPoint = Common.FindSpawnPoint(0, true, "CITIZENS_REPORT CRIME_DISTURBANCE IN_OR_ON_POSITION", 3, 7000);
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
						this.suspectOne = Common.CreatePed(true, this.spawnPoint, true, true);
						this.suspectTwo = Common.CreatePed(true, this.suspectOne.GetOffsetPositionFront(1f), true, true);
						this.suspectOne.RelationshipGroup = "suspectOne";
						this.suspectTwo.RelationshipGroup = "suspectTwo";
						Game.LocalPlayer.Character.RelationshipGroup = "player";
						this.suspectsBlip = Common.CreateSearchArea(this.suspectOne.Position, 100f);
						this.suspectsBlip.EnableRoute(Color.Yellow);
						Common.WriteToLog("All entities spawned");
					}
					catch (Exception ex2)
					{
						Common.WriteErrorToLog("Could not spawn all entities. Ending callout. This may be caused by one of GTA's \"Bermuda Triangles\".Move to another location and try again. Exception: " + ex2.ToString());
						this.End();
						return;
					}
					this.outcome = Common.PickOutcome(4, 4);
					Common.WriteToLog("Outcome: " + this.outcome.ToString());
					this.calloutRunning = true;
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspectOne) < 100f;
						if (flag)
						{
							this.suspectOne.BlockPermanentEvents = false;
							this.suspectTwo.BlockPermanentEvents = false;
							Game.SetRelationshipBetweenRelationshipGroups("suspectOne", "suspectTwo", 5);
							Game.SetRelationshipBetweenRelationshipGroups("suspectTwo", "suspectOne", 5);
						}
						bool flag2 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspectOne) < 30f;
						if (flag2)
						{
							Common.WriteToLog("Player arrived at suspects");
							Common.Dismiss(this.suspectsBlip);
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~to order the ~r~suspects ~s~to stop fighting");
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag3 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspectOne) < 10f;
						if (flag3)
						{
							bool flag4 = Game.IsKeyDown(this.talk);
							if (flag4)
							{
								Game.DisplaySubtitle("~b~ You: ~s~Police! Both of you stop and put your hands up!");
								bool flag5 = this.outcome == 1;
								if (flag5)
								{
									GameFiber.Wait(2000);
									this.suspectOne.BlockPermanentEvents = true;
									this.suspectTwo.BlockPermanentEvents = true;
									Game.SetRelationshipBetweenRelationshipGroups("suspectOne", "suspectTwo", 4);
									Game.SetRelationshipBetweenRelationshipGroups("suspectTwo", "suspectOne", 4);
									this.suspectOne.Tasks.Clear();
									this.suspectTwo.Tasks.Clear();
									this.suspectOne.Tasks.PutHandsUp(0, Game.LocalPlayer.Character);
									this.suspectTwo.Tasks.PutHandsUp(0, Game.LocalPlayer.Character);
								}
								else
								{
									bool flag6 = this.outcome == 2;
									if (flag6)
									{
										GameFiber.Wait(2000);
										this.suspectOne.BlockPermanentEvents = true;
										this.suspectTwo.BlockPermanentEvents = true;
										Game.SetRelationshipBetweenRelationshipGroups("suspectOne", "suspectTwo", 4);
										Game.SetRelationshipBetweenRelationshipGroups("suspectTwo", "suspectOne", 4);
										this.suspectOne.Tasks.Clear();
										this.suspectTwo.Tasks.Clear();
										this.suspectOne.Tasks.PutHandsUp(999999, Game.LocalPlayer.Character);
										Game.DisplayNotification("~b~You: ~s~Disptach, one suspect is cooperating the other is taking off on foot");
										this.pursuit = Functions.CreatePursuit();
										Functions.AddPedToPursuit(this.pursuit, this.suspectOne);
										Functions.SetPursuitIsActiveForPlayer(this.pursuit, true);
									}
									else
									{
										bool flag7 = this.outcome == 4;
										if (flag7)
										{
											GameFiber.Wait(2000);
											this.suspectOne.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, false);
											Game.SetRelationshipBetweenRelationshipGroups("suspectOne", "player", 5);
											Game.SetRelationshipBetweenRelationshipGroups("suspectTwo", "player", 5);
										}
									}
								}
								break;
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag8 = EntityExtensions.Exists(this.suspectOne) && EntityExtensions.Exists(this.suspectTwo) && ((this.suspectOne.IsDead && Functions.IsPedArrested(this.suspectTwo)) || (this.suspectTwo.IsDead && Functions.IsPedArrested(this.suspectOne)) || (this.suspectOne.IsDead && this.suspectTwo.IsDead) || (Functions.IsPedArrested(this.suspectOne) && Functions.IsPedArrested(this.suspectTwo)));
						if (flag8)
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
				bool flag3 = !EntityExtensions.Exists(this.suspectOne) || !EntityExtensions.Exists(this.suspectTwo);
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
			Common.Dismiss(this.suspectOne);
			Common.Dismiss(this.suspectTwo);
			Common.Dismiss(this.suspectsBlip);
			base.End();
		}

		
		private LHandle pursuit;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private int outcome;

		
		private Ped suspectOne;

		
		private Ped suspectTwo;

		
		private Blip suspectsBlip;

		
		private Random rand;
	}
}
