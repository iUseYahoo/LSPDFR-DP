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
	
	[CalloutInfo("LC - Disturbance", 2)]
	internal class Disturbance : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Disturbance";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPoint = Common.FindSpawnPoint(0, true, "CITIZENS_REPORT CRIME_DISTURBANCE IN_OR_ON_POSITION", 2, 7000);
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
			GameFiber.StartNew(delegate()
			{
				this.calloutSelection = Common.PickOutcome(5, -1);
				this.calloutSelection--;
				bool flag = this.calloutSelection == 0;
				if (flag)
				{
					Game.DisplayNotification("Speak to the caller");
					Common.WriteToLog("Picked StolenCocaine");
					this.StolenCocaine();
				}
				else
				{
					bool flag2 = this.calloutSelection == 1;
					if (flag2)
					{
						Game.DisplayNotification("Get to the scene and investigate the situation");
						Common.WriteToLog("Picked PersonInStreet");
						this.PersonInStreet();
					}
					else
					{
						bool flag3 = this.calloutSelection == 2;
						if (flag3)
						{
							Game.DisplayNotification("Get to the scene and investigate the situation");
							Common.WriteToLog("Picked AbandonedCar");
							this.AbandonedCar();
						}
						else
						{
							bool flag4 = this.calloutSelection == 3;
							if (flag4)
							{
								Game.DisplayNotification("Speak to the caller");
								Common.WriteToLog("Picked SpottedAliens");
								this.SpottedAliens();
							}
						}
					}
				}
			});
		}

		
		private void StolenCocaine()
		{
			try
			{
				this.caller = Common.CreatePed(true, this.spawnPoint, true, true);
				this.callerBlip = Common.CreateBlip(this.caller, Color.Yellow);
				this.callerBlip.EnableRoute(Color.Yellow);
				Common.WriteToLog("Spawned all entities");
				this.calloutRunning = true;
				int dialogIndex = 0;
				List<string> dialogWithSuspectOne = new List<string>
				{
					"~b~You: ~s~Hello sir. Did you call?",
					"~r~Suspect: ~s~You're damn right I called! Someone stole my cocaine!",
					"~b~You: ~s~....excuse me?",
					"~r~Suspect: ~s~I had my cocaine stash hidden right here and someone stole it all!",
					"~b~You: ~s~You called the police becuase someone stole your cocaine?",
					"~r~Suspect: ~s~Yes! How many times do I have to say it!?",
					"~b~You: ~s~Sir, are you high right now?",
					"~r~Suspect: ~s~How in the hell could I be high when someone stole all my cocaine!?"
				};
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag = Vector3.Distance(Game.LocalPlayer.Character.Position, this.caller) < 30f;
					if (flag)
					{
						Common.WriteToLog("Player arrived at caller");
						this.callerBlip.DisableRoute();
						Game.DisplayHelp("Press ~y~ " + this.talk.ToString() + " ~s~when near the ~y~caller ~s~to advance conversation.");
						break;
					}
				}
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag2 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.caller) < 10f;
					if (flag2)
					{
						if (Disturbance.<>o__12.<>p__0 == null)
						{
							Disturbance.<>o__12.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(Disturbance), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						Disturbance.<>o__12.<>p__0.Target(Disturbance.<>o__12.<>p__0, NativeFunction.Natives, this.caller, Game.LocalPlayer.Character, -1);
						bool flag3 = Game.IsKeyDown(this.talk);
						if (flag3)
						{
							Game.DisplaySubtitle(dialogWithSuspectOne[dialogIndex]);
							dialogIndex++;
							bool flag4 = dialogIndex == dialogWithSuspectOne.Count;
							if (flag4)
							{
								Common.WriteToLog("Finished with dialog");
								Game.DisplayHelp("Deal with the ~r~suspect ~s~as you see fit. Press ~y~" + this.end.ToString() + " ~s~to end the callout.");
								break;
							}
						}
					}
				}
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag5 = !EntityExtensions.Exists(this.caller) || Functions.IsPedArrested(this.caller) || this.caller.IsDead;
					if (flag5)
					{
						this.End();
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Common.WriteErrorToLog(ex.ToString());
			}
		}

		
		private void PersonInStreet()
		{
			try
			{
				this.spawnPoint = World.GetNextPositionOnStreet(this.spawnPoint);
				this.caller = Common.CreatePed(true, this.spawnPoint, true, true);
				this.callerBlip = Common.CreateBlip(this.caller, Color.Yellow);
				this.callerBlip.EnableRoute(Color.Yellow);
				Common.WriteToLog("Spawned all entities");
				this.calloutRunning = true;
				int dialogIndex = 0;
				List<string> dialogWithSuspectOne = new List<string>
				{
					"~b~You: ~s~Excuse me. Mind telling me why your dancing in the middle of the street?",
					"~r~Suspect: ~s~Dancing is how I express myself.",
					"~b~You: ~s~Well the street is for driving, not expressing yourself.",
					"~r~Suspect: ~s~But there is so many people on the street. I want everyone to see.",
					"~b~You: ~s~I doubt anyone wants to see though. I need you to get out of the middle of the street.",
					"~r~Suspect: ~s~Sure, no problem......once I'm done"
				};
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag = Vector3.Distance(Game.LocalPlayer.Character.Position, this.caller) < 30f;
					if (flag)
					{
						Common.WriteToLog("Player arrived at caller");
						this.callerBlip.Color = Color.Red;
						Game.DisplayHelp("Press ~y~ " + this.talk.ToString() + " ~s~when near the ~r~suspect ~s~to advance conversation.");
						this.caller.Tasks.PlayAnimation("mini@strip_club@private_dance@part1", "priv_dance_p1", 1f, 1);
						this.callerBlip.DisableRoute();
						break;
					}
				}
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag2 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.caller) < 10f;
					if (flag2)
					{
						bool flag3 = Game.IsKeyDown(this.talk);
						if (flag3)
						{
							Game.DisplaySubtitle(dialogWithSuspectOne[dialogIndex]);
							dialogIndex++;
							bool flag4 = dialogIndex == dialogWithSuspectOne.Count;
							if (flag4)
							{
								break;
							}
						}
					}
				}
				Common.WriteToLog("Finished with dialog");
				Game.DisplayHelp("Deal with the ~r~suspect ~s~as you see fit. Press ~y~" + this.end.ToString() + " ~s~to end the callout.");
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag5 = !EntityExtensions.Exists(this.caller) || Functions.IsPedArrested(this.caller) || this.caller.IsDead;
					if (flag5)
					{
						this.End();
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Common.WriteErrorToLog(ex.ToString());
			}
		}

		
		private void AbandonedCar()
		{
			try
			{
				int carBomb = Common.PickOutcome(2, 1);
				Common.WriteToLog("carBomb is " + carBomb.ToString());
				this.vehicle = Common.CreateVehicle(World.GetNextPositionOnStreet(this.spawnPoint), 0f, true);
				this.vehicleBlip = Common.CreateBlip(this.vehicle, Color.Yellow);
				this.vehicleBlip.EnableRoute(Color.Yellow);
				Common.WriteToLog("Spawned all entities");
				this.calloutRunning = true;
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag = Vector3.Distance(Game.LocalPlayer.Character.Position, this.vehicle) < 30f;
					if (flag)
					{
						Common.WriteToLog("Player arrived at vehicle");
						this.vehicleBlip.DisableRoute();
						Game.DisplayHelp("Investigate the ~y~vehicle. ~s~Press ~y~" + this.end.ToString() + "~s~ to end the callout.");
						break;
					}
				}
				bool flag2 = carBomb == 1;
				if (flag2)
				{
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag3 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.vehicle) < 3f;
						if (flag3)
						{
							Game.DisplayNotification("char_lester_deathwish", "char_lester_deathwish", "Danger", "", "~r~You hear ticking. Like the ticking a bomb makes!");
							GameFiber.Wait(5000);
							World.SpawnExplosion(this.vehicle.Position, 7, 3f, true, false, 0.5f);
							Common.WriteToLog("vehicle exploded");
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Common.WriteErrorToLog(ex.ToString());
			}
		}

		
		private void SpottedAliens()
		{
			try
			{
				this.caller = Common.CreatePed(true, this.spawnPoint, true, true);
				this.callerBlip = Common.CreateBlip(this.caller, Color.Yellow);
				this.callerBlip.EnableRoute(Color.Yellow);
				Common.WriteToLog("Spawned all entities");
				this.calloutRunning = true;
				int dialogIndex = 0;
				List<string> dialogWithCaller = new List<string>
				{
					"~b~You: ~s~See who?",
					"~y~Caller: ~s~The aliens! They're everywhere! They're starting the invasion!",
					"~b~You: ~s~Where do you see them?",
					"~y~Caller: ~s~There....and there....and there.....and over there....and in the sky. Everywhere!",
					"~b~You: ~s~There are no aliens around here.",
					"~y~Caller: ~s~Yes there are! Everyone run! Run for your life!"
				};
				this.caller.Tasks.PlayAnimation("amb@code_human_cower@male@base", "base", 1f, 1);
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag = Vector3.Distance(Game.LocalPlayer.Character.Position, this.caller) < 30f;
					if (flag)
					{
						Common.WriteToLog("Player arrived at caller");
						this.callerBlip.DisableRoute();
						Game.DisplayHelp("Press ~y~ " + this.talk.ToString() + " ~s~when near the ~y~caller ~s~to advance conversation.");
						break;
					}
				}
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag2 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.caller) < 10f;
					if (flag2)
					{
						Game.DisplaySubtitle("~y~Caller: ~s~Look! Can't you see them!?");
						break;
					}
				}
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag3 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.caller) < 10f;
					if (flag3)
					{
						bool flag4 = Game.IsKeyDown(this.talk);
						if (flag4)
						{
							Game.DisplaySubtitle(dialogWithCaller[dialogIndex]);
							dialogIndex++;
							bool flag5 = dialogIndex == dialogWithCaller.Count;
							if (flag5)
							{
								break;
							}
						}
					}
				}
				bool flag6 = this.calloutRunning;
				if (flag6)
				{
					Common.WriteToLog("Finshed with dialog");
					this.caller.Tasks.Clear();
					this.caller.Tasks.ReactAndFlee(Game.LocalPlayer.Character);
					Game.DisplayHelp(string.Concat(new string[]
					{
						"Handle the situation as you see fit. Press ~y~",
						this.end.ToString(),
						"~s~ to end the callout.~n~You may press ~y~",
						this.talk.ToString(),
						"~s~ to make the ~y~caller ~s~a pursuit target."
					}));
				}
				while (this.calloutRunning)
				{
					GameFiber.Yield();
					bool flag7 = Game.IsKeyDown(this.talk);
					if (flag7)
					{
						Common.WriteToLog("Pursuit created");
						bool flag8 = EntityExtensions.Exists(this.callerBlip);
						if (flag8)
						{
							this.callerBlip.Delete();
						}
						LHandle pursuit = Functions.CreatePursuit();
						Functions.AddPedToPursuit(pursuit, this.caller);
						Functions.SetPursuitIsActiveForPlayer(pursuit, true);
						break;
					}
				}
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
				bool flag3 = (this.calloutSelection != 2 && !EntityExtensions.Exists(this.caller)) || (this.calloutSelection == 2 && !EntityExtensions.Exists(this.vehicle));
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
			Common.Dismiss(this.caller);
			Common.Dismiss(this.vehicle);
			Common.Dismiss(this.callerBlip);
			Common.Dismiss(this.vehicleBlip);
			base.End();
		}

		
		private bool calloutRunning = false;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private Ped caller;

		
		private Blip callerBlip;

		
		private Vehicle vehicle;

		
		private Blip vehicleBlip;

		
		public Vector3 spawnPoint;

		
		private int calloutSelection;
	}
}
