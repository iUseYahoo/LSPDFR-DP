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
	
	[CalloutInfo("LC - AggressivePanhandling", 2)]
	internal class AggressivePanhandling : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Aggressive Panhandling";
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
			try
			{
				GameFiber.StartNew(delegate()
				{
					try
					{
						this.suspect = Common.CreatePed(true, World.GetNextPositionOnStreet(this.spawnPoint.Around(50f, 200f)), true, true);
						this.witness = Common.CreatePed(false, this.spawnPoint, true, true);
						this.witnessBlip = Common.CreateBlip(this.witness, Color.Orange);
						this.witnessBlip.EnableRoute(Color.Yellow);
						Common.WriteToLog("All entities spawned");
					}
					catch (Exception ex2)
					{
						Common.WriteErrorToLog("Could not spawn all entities. Ending callout. This may be caused by one of GTA's \"Bermuda Triangles\".Move to another location and try again. Exception: " + ex2.ToString());
						this.End();
						return;
					}
					this.outcome = Common.PickOutcome(4, 3);
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
							bool flag3 = this.outcome == 3;
							if (flag3)
							{
								this.numOfDialog = this.dialogWithSuspectThree.Count;
								this.suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, false);
								this.suspect.RelationshipGroup = "suspect";
								Game.LocalPlayer.Character.RelationshipGroup = "cop";
							}
							else
							{
								this.numOfDialog = 1;
							}
						}
					}
					this.calloutRunning = true;
					this.suspect.Tasks.Wander();
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag4 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.witness) < 30f;
						if (flag4)
						{
							Common.WriteToLog("Player arrived at witness");
							this.witnessBlip.DisableRoute();
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~o~witness ~s~to advance the conversation");
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag5 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.witness) < 10f;
						if (flag5)
						{
							if (AggressivePanhandling.<>o__19.<>p__0 == null)
							{
								AggressivePanhandling.<>o__19.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(AggressivePanhandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							AggressivePanhandling.<>o__19.<>p__0.Target(AggressivePanhandling.<>o__19.<>p__0, NativeFunction.Natives, this.witness, Game.LocalPlayer.Character, -1);
							bool flag6 = Game.IsKeyDown(this.talk);
							if (flag6)
							{
								Common.WriteToLog("Witness dialog index: " + this.dialogWithWitnessIndex.ToString());
								Game.DisplaySubtitle(this.dialogWithWitness[this.dialogWithWitnessIndex]);
								this.dialogWithWitnessIndex++;
								bool flag7 = this.dialogWithWitnessIndex == this.dialogWithWitness.Count;
								if (flag7)
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
						bool flag8 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 10f;
						if (flag8)
						{
							bool flag9 = Game.IsKeyDown(this.talk);
							if (flag9)
							{
								this.suspect.Tasks.Clear();
								if (AggressivePanhandling.<>o__19.<>p__1 == null)
								{
									AggressivePanhandling.<>o__19.<>p__1 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(AggressivePanhandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								AggressivePanhandling.<>o__19.<>p__1.Target(AggressivePanhandling.<>o__19.<>p__1, NativeFunction.Natives, this.suspect, Game.LocalPlayer.Character, -1);
								Common.WriteToLog("Suspect dialog index: " + this.dialogIndex.ToString());
								bool flag10 = this.outcome == 1;
								if (flag10)
								{
									Game.DisplaySubtitle(this.dialogWithSuspectOne[this.dialogIndex]);
								}
								else
								{
									bool flag11 = this.outcome == 2;
									if (flag11)
									{
										Game.DisplaySubtitle(this.dialogWithSuspectTwo[this.dialogIndex]);
									}
									else
									{
										bool flag12 = this.outcome == 3;
										if (flag12)
										{
											Game.DisplaySubtitle(this.dialogWithSuspectThree[this.dialogIndex]);
										}
									}
								}
								this.dialogIndex++;
								bool flag13 = this.dialogIndex == this.numOfDialog;
								if (flag13)
								{
									Common.WriteToLog("Finished with dialog");
									bool flag14 = this.outcome == 3;
									if (flag14)
									{
										GameFiber.Wait(2000);
										this.suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
										break;
									}
									bool flag15 = this.outcome == 4;
									if (flag15)
									{
										Game.DisplaySubtitle("~b~You: ~s~Hey man, can I talk to you for a second?");
										GameFiber.Wait(2000);
										Game.DisplayNotification("~b~You: ~n~~s~Dispatch, the suspect is fleeing on foot!");
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
						bool flag16 = !EntityExtensions.Exists(this.suspect) || Functions.IsPedArrested(this.suspect) || this.suspect.IsDead;
						if (flag16)
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
				bool flag3 = !EntityExtensions.Exists(this.witness) || !EntityExtensions.Exists(this.suspect);
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
			Common.Dismiss(this.witness);
			Common.Dismiss(this.suspectBlip);
			Common.Dismiss(this.witnessBlip);
			base.End();
		}

		
		private LHandle pursuit;

		
		private int dialogIndex = 0;

		
		private int dialogWithWitnessIndex = 0;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private int outcome;

		
		private int numOfDialog;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private Ped witness;

		
		private Blip witnessBlip;

		
		private readonly List<string> dialogWithWitness = new List<string>
		{
			"~o~Witness: ~s~Hello officer. This guy has been forcing people to give him money.",
			"~b~You: ~s~Forcing? You mean robbing?",
			"~o~Witness: ~s~No, but he won't leave people alone until they give him money.",
			"~b~You: ~s~Has he threatened anyone?",
			"~o~Witness: ~s~Not that I've heard.",
			"~b~You: ~s~Ok. Do you know where he is now?",
			"~o~Victim: ~s~I think he went that way when you pulled up."
		};

		
		private readonly List<string> dialogWithSuspectOne = new List<string>
		{
			"~b~You: ~s~Hey man. What are you doing?",
			"~r~Suspect: ~s~Me? I'm just walking.",
			"~b~You: ~s~Have you been asking people for money?",
			"~r~Suspect: ~s~Even if I was, its not illegal.",
			"~b~You: ~s~Correct, but you've basiclly been forcing people to give you money.",
			"~r~Suspect: ~s~Well I need it more than they do!",
			"~b~You: ~s~Doesn't matter. Either way you're still being a public nuisance."
		};

		
		private readonly List<string> dialogWithSuspectTwo = new List<string>
		{
			"~b~You: ~s~Hey man. What are you doing?",
			"~r~Suspect: ~s~Me? I'm just walking.",
			"~b~You: ~s~Have you been asking people for money?",
			"~r~Suspect: ~s~Yes. I do not have a job and need money.",
			"~b~You: ~s~People are saying you're forcing them to give you money.",
			"~r~Suspect: ~s~Forcing? No. Maybe a little pushy.",
			"~b~You: ~s~Either way, if they say \"no\" you can't keep asking them.",
			"~r~Suspect: ~s~You're right. I won't do it again."
		};

		
		private readonly List<string> dialogWithSuspectThree = new List<string>
		{
			"~b~You: ~s~Hey man. What are you doing?",
			"~r~Suspect: ~s~None of your business what I'm doing.",
			"~b~You: ~s~Have you been asking people for money?",
			"~r~Suspect: ~s~Like I said, none of your business.",
			"~b~You: ~s~People are saying you're forcing them to give you money.",
			"~r~Suspect: ~s~Why don't you leave me alone before you piss me off.",
			"~b~You: ~s~Do you have any weapons on you sir?",
			"~r~Suspect: ~s~Yep. Right here."
		};
	}
}
