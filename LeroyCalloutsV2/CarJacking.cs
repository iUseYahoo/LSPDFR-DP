using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace LeroyCalloutsV2.Callouts
{
	
	[CalloutInfo("LC - Carjacking", 2)]
	internal class Carjacking : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Carjacking";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPoint = Common.FindSpawnPoint(0, false, "", 2, 0);
				bool flag = this.spawnPoint != Vector3.Zero;
				if (flag)
				{
					Common.WriteToLog("Spawn Point: " + this.spawnPoint.ToString());
					base.ShowCalloutAreaBlipBeforeAccepting(this.spawnPoint, 30f);
					base.AddMinimumDistanceCheck(100f, this.spawnPoint);
					base.CalloutMessage = "Carjacking~n~~r~Code 3 Response";
					base.CalloutPosition = this.spawnPoint;
					Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT CRIME_CAR_JACKING IN_OR_ON_POSITION", this.spawnPoint);
					GameFiber.StartNew(delegate()
					{
						GameFiber.Wait(7000);
						Functions.PlayScannerAudio("UNITS_RESPOND_CODE_03");
						GameFiber.Hibernate();
					});
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
						this.victim = Common.CreatePed(false, this.spawnPoint, true, true);
						this.victimBlip = Common.CreateBlip(this.victim, Color.Yellow);
						this.victimBlip.EnableRoute(Color.Yellow);
						this.vehicle = Common.CreateVehicle(World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(250f, 400f)), 0f, true);
						this.suspect = Common.CreatePed(true, Vector3.Zero, true, true);
						this.suspect.WarpIntoVehicle(this.vehicle, -1);
						this.suspect.Tasks.CruiseWithVehicle(15f, 183);
						this.color = ((this.vehicle.PrimaryColor.ToKnownColor().ToString() == "0") ? "" : this.vehicle.PrimaryColor.ToKnownColor().ToString());
						this.model = this.vehicle.Model.Name.ToString();
						this.plate = this.vehicle.LicensePlate;
					}
					catch (Exception ex2)
					{
						Common.WriteErrorToLog("Could not spawn all entities. Ending callout. This may be caused by one of GTA's \"Bermuda Triangles\".Move to another location and try again. Exception: " + ex2.ToString());
						this.End();
						return;
					}
					this.calloutRunning = true;
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag = Vector3.Distance(Game.LocalPlayer.Character.Position, this.victim) < 30f;
						if (flag)
						{
							Common.WriteToLog("Player arrived at victim");
							this.victimBlip.DisableRoute();
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~y~victim ~s~to advance the conversation");
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag2 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.victim) < 10f;
						if (flag2)
						{
							if (Carjacking.<>o__19.<>p__0 == null)
							{
								Carjacking.<>o__19.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(Carjacking), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Carjacking.<>o__19.<>p__0.Target(Carjacking.<>o__19.<>p__0, NativeFunction.Natives, this.victim, Game.LocalPlayer.Character, -1);
							bool flag3 = Game.IsKeyDown(this.talk);
							if (flag3)
							{
								bool flag4 = this.dialogIndex == 6;
								if (flag4)
								{
									Game.DisplaySubtitle(string.Concat(new string[]
									{
										"~o~Victim: ~s~It's a ~r~",
										this.color,
										" ",
										this.model,
										" ~s~license plate is ~r~",
										this.plate
									}));
								}
								else
								{
									Game.DisplaySubtitle(this.dialogWithPed[this.dialogIndex]);
								}
								this.dialogIndex++;
								bool flag5 = this.dialogIndex == this.dialogWithPed.Count;
								if (flag5)
								{
									Common.WriteToLog("Finished with dialog");
									break;
								}
							}
						}
					}
					this.searchArea = Common.CreateSearchArea(this.suspect.Position, 300f);
					this.searchArea.EnableRoute(Color.Yellow);
					Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", this.suspect.Position);
					Game.DisplayHelp("Locate the ~r~stolen vehicle");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag6 = this.vehicle.DistanceTo(this.suspectPosition) > 200f;
						if (flag6)
						{
							Common.WriteToLog("Refreshing search area");
							this.searchArea.Delete();
							this.searchArea = Common.CreateSearchArea(this.suspect.Position, 100f);
							this.searchArea.EnableRoute(Color.Yellow);
							this.suspectPosition = this.suspect.Position;
							Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", this.suspectPosition);
						}
						else
						{
							bool flag7 = Game.LocalPlayer.Character.DistanceTo(this.suspect) < 20f;
							if (flag7)
							{
								Game.DisplayNotification("~b~You: ~s~Dispatch, suspect located.");
								this.searchArea.Delete();
								this.suspectBlip = Common.CreateBlip(this.suspect, Color.Red);
								break;
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag8 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 10f;
						if (flag8)
						{
							Game.DisplayNotification("~b~You: ~s~Dispatch, suspect is fleeing!");
							Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CRIME_RESIST_ARREST");
							this.pursuit = Functions.CreatePursuit();
							Functions.AddPedToPursuit(this.pursuit, this.suspect);
							Functions.SetPursuitIsActiveForPlayer(this.pursuit, true);
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag9 = !EntityExtensions.Exists(this.suspect) || Functions.IsPedArrested(this.suspect) || this.suspect.IsDead;
						if (flag9)
						{
							this.calloutRunning = false;
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
					this.End();
				}
				bool flag3 = !EntityExtensions.Exists(this.victim) || !EntityExtensions.Exists(this.suspect) || !EntityExtensions.Exists(this.vehicle);
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
			Common.Dismiss(this.victim);
			Common.Dismiss(this.suspect);
			Common.Dismiss(this.vehicle);
			Common.Dismiss(this.victimBlip);
			Common.Dismiss(this.suspectBlip);
			base.End();
		}

		
		private Vector3 suspectPosition;

		
		private Blip searchArea;

		
		private string color;

		
		private string model;

		
		private string plate;

		
		private LHandle pursuit;

		
		private int dialogIndex = 0;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private Ped victim;

		
		private Blip victimBlip;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private Vehicle vehicle;

		
		private List<string> dialogWithPed = new List<string>
		{
			"~o~Victim: ~s~Someone stole my car officer! Please, you have to get it back!",
			"~b~You: ~s~Okay, just calm down. Tell me what happened.",
			"~o~Victim: ~s~I was sitting at the intersection and someone just came up and jerked me out of my car.",
			"~b~You: ~s~Did you see what they looked like?",
			"~o~Victim: ~s~No. By the time I knew what happened, they were gone.",
			"~b~You: ~s~What kind of car is it? And what is the license plate number?",
			"",
			"~b~You: ~s~Alright. Did you see which way they went?",
			"~o~Victim: ~s~That way, I think."
		};
	}
}
