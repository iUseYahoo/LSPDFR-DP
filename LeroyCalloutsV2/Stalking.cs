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
	
	[CalloutInfo("LC - Stalking", 2)]
	internal class Stalking : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Stalking";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPoint = Common.FindSpawnPoint(0, true, "ATTENTION_ALL_UNITS_GENERIC CRIME_CIVILIAN_NEEDING_ASSISTANCE IN_OR_ON_POSITION", 2, 8000);
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
						this.suspect = Common.CreatePed(true, World.GetNextPositionOnStreet(this.spawnPoint.Around(100f, 200f)), true, true);
						this.victim = new Ped("a_f_y_bevhills_01", this.spawnPoint, 0f);
						this.victimBlip = Common.CreateBlip(this.victim, Color.Orange);
						this.victimBlip.EnableRoute(Color.Yellow);
						Common.WriteToLog("All entities spawned");
					}
					catch (Exception ex2)
					{
						Common.WriteErrorToLog("Could not spawn all entities. Ending callout. This may be caused by one of GTA's \"Bermuda Triangles\".Move to another location and try again. Exception: " + ex2.ToString());
						this.End();
						return;
					}
					this.outcome = Common.PickOutcome(3, 3);
					Common.WriteToLog("Outcome: " + this.outcome.ToString());
					bool flag = this.outcome == 1;
					if (flag)
					{
						this.numOfDialog = this.dialogWithSuspectOne.Count;
					}
					else
					{
						bool flag2 = this.outcome == 2;
						if (flag2)
						{
							this.numOfDialog = this.dialogWithSuspectTwo.Count;
						}
						else
						{
							this.numOfDialog = this.dialogWithSuspectThree.Count;
							int weapon = this.num.Next(0, 3);
							bool flag3 = weapon == 0;
							if (flag3)
							{
								this.suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, false);
							}
							else
							{
								bool flag4 = weapon == 1;
								if (flag4)
								{
									this.suspect.Inventory.GiveNewWeapon("WEAPON_SWITCHBLADE", -1, false);
								}
								else
								{
									this.suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", -1, false);
								}
							}
							this.suspect.RelationshipGroup = "suspect";
							Game.LocalPlayer.Character.RelationshipGroup = "cop";
						}
					}
					this.calloutRunning = true;
					this.suspect.Tasks.Wander();
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag5 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.victim) < 20f;
						if (flag5)
						{
							Common.WriteToLog("Player arrived at witness");
							Game.DisplaySubtitle("~o~Victim: ~s~Officer! Over here!");
							this.victimBlip.DisableRoute();
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~o~victim ~s~to advance the conversation");
							this.victim.Tasks.StandStill(-1);
							if (Stalking.<>o__20.<>p__0 == null)
							{
								Stalking.<>o__20.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(Stalking), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Stalking.<>o__20.<>p__0.Target(Stalking.<>o__20.<>p__0, NativeFunction.Natives, this.victim, Game.LocalPlayer.Character, -1);
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag6 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.victimBlip) < 10f;
						if (flag6)
						{
							if (Stalking.<>o__20.<>p__1 == null)
							{
								Stalking.<>o__20.<>p__1 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(Stalking), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Stalking.<>o__20.<>p__1.Target(Stalking.<>o__20.<>p__1, NativeFunction.Natives, this.victim, Game.LocalPlayer.Character, -1);
							bool flag7 = Game.IsKeyDown(this.talk);
							if (flag7)
							{
								Common.WriteToLog("Witness dialog index: " + this.dialogWithVictimIndex.ToString());
								Game.DisplaySubtitle(this.dialogWithPed[this.dialogWithVictimIndex]);
								this.dialogWithVictimIndex++;
								bool flag8 = this.dialogWithVictimIndex == this.dialogWithPed.Count;
								if (flag8)
								{
									Common.WriteToLog("Routing to suspect");
									this.suspectBlip = Common.CreateBlip(this.suspect, Color.Red);
									this.suspectBlip.EnableRoute(Color.Yellow);
									Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", this.suspect.Position);
									Game.DisplayHelp("Talk to the ~r~suspect");
									break;
								}
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag9 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 10f;
						if (flag9)
						{
							bool flag10 = Game.IsKeyDown(this.talk);
							if (flag10)
							{
								this.suspect.Tasks.Clear();
								if (Stalking.<>o__20.<>p__2 == null)
								{
									Stalking.<>o__20.<>p__2 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(Stalking), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								Stalking.<>o__20.<>p__2.Target(Stalking.<>o__20.<>p__2, NativeFunction.Natives, this.suspect, Game.LocalPlayer.Character, -1);
								Common.WriteToLog("Suspect dialog index: " + this.dialogIndex.ToString());
								bool flag11 = this.outcome == 1;
								if (flag11)
								{
									Game.DisplaySubtitle(this.dialogWithSuspectOne[this.dialogIndex]);
								}
								else
								{
									bool flag12 = this.outcome == 2;
									if (flag12)
									{
										Game.DisplaySubtitle(this.dialogWithSuspectTwo[this.dialogIndex]);
									}
									else
									{
										bool flag13 = this.outcome == 3;
										if (flag13)
										{
											Game.DisplaySubtitle(this.dialogWithSuspectThree[this.dialogIndex]);
										}
									}
								}
								this.dialogIndex++;
								bool flag14 = this.dialogIndex == this.numOfDialog;
								if (flag14)
								{
									Common.WriteToLog("Finished with dialog");
									bool flag15 = this.outcome == 3;
									if (flag15)
									{
										GameFiber.Wait(2000);
										this.suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
										break;
									}
									bool flag16 = this.outcome == 4;
									if (flag16)
									{
										Game.DisplaySubtitle("~b~You: ~s~Hey man, can I talk to you for a second?");
										GameFiber.Wait(2000);
										Game.DisplayNotification("~b~You: ~n~Dispatch, the suspect is fleeing on foot!");
										Common.WriteToLog("Starting pursuit");
										this.pursuit = Functions.CreatePursuit();
										Functions.AddPedToPursuit(this.pursuit, this.suspect);
										Functions.SetPursuitIsActiveForPlayer(this.pursuit, true);
										break;
									}
									Game.DisplayHelp("Deal with the ~r~suspect ~s~as you see fit. Press ~y~" + this.end.ToString() + " ~s~to end the callout.");
									break;
								}
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag17 = !EntityExtensions.Exists(this.suspect) || Functions.IsPedArrested(this.suspect) || this.suspect.IsDead;
						if (flag17)
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
				bool flag3 = !EntityExtensions.Exists(this.victim) || !EntityExtensions.Exists(this.suspect);
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
			Common.Dismiss(this.victim);
			Common.Dismiss(this.suspectBlip);
			Common.Dismiss(this.victimBlip);
			base.End();
		}

		
		private LHandle pursuit;

		
		private int dialogIndex = 0;

		
		private int dialogWithVictimIndex = 0;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private Random num = new Random();

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private int outcome;

		
		private int numOfDialog;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private Ped victim;

		
		private Blip victimBlip;

		
		private readonly List<string> dialogWithPed = new List<string>
		{
			"~o~Victim: ~s~Thank God you're here officer. I think somone is stalking me. ",
			"~b~You: ~s~Why do you think that?",
			"~o~Victim: ~s~Iv'e been walking home from work and this guy has been following me.",
			"~b~You: ~s~Have you seen him before?",
			"~o~Victim: ~s~Yes. He's been wandering around my workplace for several days.",
			"~b~You: ~s~Alright. Do you know where he is now?",
			"~o~Victim: ~s~I think he went that way when you pulled up."
		};

		
		private readonly List<string> dialogWithSuspectOne = new List<string>
		{
			"~b~You: ~s~Excuse me sir! Can I talk to you for a minute?",
			"~r~Suspect: ~s~I guess. What for?",
			"~b~You: ~s~A lady said you have been following and stalking her.",
			"~r~Suspect: ~s~No! She's just happens to be at the exact same place that I'm always at....",
			"~b~You: ~s~That's the story you're going with?",
			"~r~Suspect: ~s~Yup."
		};

		
		private readonly List<string> dialogWithSuspectTwo = new List<string>
		{
			"~b~You: ~s~Excuse me sir! Can I talk to you for a minute?",
			"~r~Suspect: ~s~I guess. What for?",
			"~b~You: ~s~A lady said you have been following and stalking her.",
			"~r~Suspect: ~s~What? I don't know what you're talking about.",
			"~b~You: ~s~She described you excatly. Said you have been wandering around her work too.",
			"~r~Suspect: ~s~I swear I don't know what you're talking about."
		};

		
		private readonly List<string> dialogWithSuspectThree = new List<string>
		{
			"~b~You: ~s~Excuse me sir! Can I talk to you for a minute?",
			"~r~Suspect: ~s~I'm in a hurry what?",
			"~b~You: ~s~A lady said you have been following and stalking her.",
			"~r~Suspect: ~s~Yeah. What's your point?",
			"~b~You: ~s~....that's illegal.",
			"~r~Suspect: ~s~So? What are you going to do about it?",
			"~b~You: ~s~I'll have to arrest you.",
			"~r~Suspect: ~s~You'll have to kill me!"
		};
	}
}
