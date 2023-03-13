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
	
	[CalloutInfo("LC - HomeLessPersonOnPrivateProperty", 2)]
	internal class HomelessPersonOnPrivateProperty : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Homeless Person On Property";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPointIndex = Common.FindSpawnPointIndex(this.possibleSpawns, true, "ATTENTION_ALL_UNITS_GENERIC CRIME_CIVILIAN_NEEDING_ASSISTANCE IN_OR_ON_POSITION", 2, 8000);
				this.homeownerSpawn = this.possibleSpawns[this.spawnPointIndex];
				base.ShowCalloutAreaBlipBeforeAccepting(this.homeownerSpawn, 30f);
				base.AddMinimumDistanceCheck(100f, this.homeownerSpawn);
				base.CalloutMessage = Common.activeCall + "~n~~y~Code 2 Response";
				base.CalloutPosition = this.homeownerSpawn;
				bool flag = this.spawnPointIndex != -1;
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
						this.suspect = Common.CreatePed(this.suspectModel, this.suspectSpawns[this.spawnPointIndex], true, true);
						this.suspectBlip = this.suspect.AttachBlip();
						this.suspectBlip.IsFriendly = false;
						this.suspect.Tasks.PlayAnimation("amb@world_human_sunbathe@male@back@idle_a", "idle_a", 1f, 1);
						this.homeowner = Common.CreatePed(false, this.homeownerSpawn, true, true);
						this.homeowner.Heading = this.homeownerHeadings[this.spawnPointIndex];
						this.homeownerBlip = Common.CreateBlip(this.homeowner, Color.Orange);
						this.homeownerBlip.EnableRoute(Color.Yellow);
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
							this.suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, false);
							this.suspect.RelationshipGroup = "suspect";
							Game.LocalPlayer.Character.RelationshipGroup = "cop";
						}
					}
					this.calloutRunning = true;
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag3 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.homeowner) < 30f;
						if (flag3)
						{
							Common.WriteToLog("Player arrived at witness");
							this.homeownerBlip.DisableRoute();
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~o~homeowner ~s~to advance the conversation");
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag4 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.homeowner) < 10f;
						if (flag4)
						{
							if (HomelessPersonOnPrivateProperty.<>o__27.<>p__0 == null)
							{
								HomelessPersonOnPrivateProperty.<>o__27.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(HomelessPersonOnPrivateProperty), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							HomelessPersonOnPrivateProperty.<>o__27.<>p__0.Target(HomelessPersonOnPrivateProperty.<>o__27.<>p__0, NativeFunction.Natives, this.homeowner, Game.LocalPlayer.Character, -1);
							bool flag5 = Game.IsKeyDown(this.talk);
							if (flag5)
							{
								Common.WriteToLog("Homeonwer dialog index: " + this.dialogWithHomeownerIndex.ToString());
								Game.DisplaySubtitle(this.dialogWithHomeowner[this.dialogWithHomeownerIndex]);
								this.dialogWithHomeownerIndex++;
								bool flag6 = this.dialogWithHomeownerIndex == this.dialogWithHomeowner.Count;
								if (flag6)
								{
									Game.DisplayHelp("Talk to the ~r~suspect");
									break;
								}
							}
						}
					}
					bool isUp = false;
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag7 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 10f;
						if (flag7)
						{
							bool flag8 = Game.IsKeyDown(this.talk);
							if (flag8)
							{
								bool flag9 = !isUp;
								if (flag9)
								{
									this.suspect.Tasks.PlayAnimation("get_up@standard", "back", 1f, 0).WaitForCompletion(3000);
									this.suspect.Tasks.Clear();
									isUp = true;
								}
								if (HomelessPersonOnPrivateProperty.<>o__27.<>p__1 == null)
								{
									HomelessPersonOnPrivateProperty.<>o__27.<>p__1 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(HomelessPersonOnPrivateProperty), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								HomelessPersonOnPrivateProperty.<>o__27.<>p__1.Target(HomelessPersonOnPrivateProperty.<>o__27.<>p__1, NativeFunction.Natives, this.suspect, Game.LocalPlayer.Character, -1);
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
									Game.DisplayHelp("Deal with the ~r~suspect ~s~as you see fit. Press ~y~" + this.end.ToString() + " ~s~to end the callout.");
									break;
								}
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag15 = !EntityExtensions.Exists(this.suspect) || Functions.IsPedArrested(this.suspect) || this.suspect.IsDead;
						if (flag15)
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
				bool flag3 = !EntityExtensions.Exists(this.homeowner) || !EntityExtensions.Exists(this.suspect);
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
			Common.Dismiss(this.homeowner);
			Common.Dismiss(this.suspectBlip);
			Common.Dismiss(this.homeownerBlip);
			base.End();
		}

		
		private LHandle pursuit;

		
		private int dialogIndex = 0;

		
		private int spawnPointIndex;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private bool spawnFound = false;

		
		private Vector3 homeownerSpawn;

		
		private int outcome;

		
		private int numOfDialog;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private Ped homeowner;

		
		private Blip homeownerBlip;

		
		private int Index;

		
		private Random num = new Random();

		
		private readonly List<string> dialogWithHomeowner = new List<string>
		{
			"~o~Homeowner: ~s~Hello officer. That homeless person is sleeping in my yard.",
			"~b~You: ~s~How long have they been there?",
			"~o~Homeowner: ~s~About 30 minutes.",
			"~b~You: ~s~Alright, have you asked them to leave?",
			"~o~Homeowner: ~s~No, I haven't talked to them. ",
			"~b~You: ~s~Okay, I'll see what I can do.",
			"~o~Homeowner: ~s~Thank you."
		};

		
		private int dialogWithHomeownerIndex = 0;

		
		private readonly List<string> dialogWithSuspectOne = new List<string>
		{
			"~r~Suspect: ~s~You better back away from me!",
			"~b~You: ~s~You're on private property. The homeowner wants you to leave.",
			"~r~Suspect: ~s~I'll leave when I'm done sleeping.",
			"~b~You: ~s~No, you're going to leave now.",
			"~r~Suspect: ~s~I'm not going anywhere."
		};

		
		private readonly List<string> dialogWithSuspectTwo = new List<string>
		{
			"~b~You: ~s~Hey wake up. I need to talk to you for a minute?",
			"~r~Suspect: ~s~Dammit. What?",
			"~b~You: ~s~You know you're on private property?",
			"~r~Suspect: ~s~Yes. But I was tired and this spot looked really comfortable.",
			"~b~You: ~s~Well I'm afraid you're going to have to leave.",
			"~r~Suspect: ~s~Please let me stay. I'm so tired.",
			"~b~You: ~s~Sorry, the homeowner does not want you sleeping on their property.",
			"~r~Suspect: ~s~Fine. I'll go."
		};

		
		private readonly List<string> dialogWithSuspectThree = new List<string>
		{
			"~b~You: ~s~Hey wake up. I need to talk to you for a minute?",
			"~r~Suspect: ~s~Dammit. What?",
			"~b~You: ~s~You know you're on private property?",
			"~r~Suspect: ~s~Yeah, what's your point?",
			"~b~You: ~s~The homeowner wants you to leave.",
			"~r~Suspect: ~s~Yeah I'll leave...after I'm done sleeping",
			"~b~You: ~s~No, you're going to leave now.",
			"~r~Suspect: ~s~I'm going to give you five seconds to get the hell away from me!",
			"~r~Suspect: ~s~1....2....5!"
		};

		
		private readonly List<Vector3> possibleSpawns = new List<Vector3>
		{
			new Vector3(1029.261f, -408.9471f, 65.9493f),
			new Vector3(-1118.347f, 762.426f, 164.2888f),
			new Vector3(1213.345f, -1644.128f, 48.64601f),
			new Vector3(1270.89f, -683.327f, 66.03162f),
			new Vector3(1315.143f, -1732.302f, 54.70009f),
			new Vector3(-1338.198f, 606.1179f, 134.3799f),
			new Vector3(1674.704f, 4657.8f, 43.37118f),
			new Vector3(1724.575f, 4642.209f, 43.87546f),
			new Vector3(1733.278f, 3809.439f, 34.80133f),
			new Vector3(191.7844f, 3082.284f, 43.47281f),
			new Vector3(1937.251f, 3891.153f, 32.47469f),
			new Vector3(-214.0658f, 6396.432f, 33.08509f),
			new Vector3(-26.7758f, 6597.502f, 31.86073f),
			new Vector3(-3077.508f, 658.9644f, 11.63682f),
			new Vector3(-3093.037f, 349.2007f, 7.533775f),
			new Vector3(-32.54408f, -1846.585f, 26.19353f),
			new Vector3(-355.6098f, 459.8973f, 116.4674f),
			new Vector3(366.2049f, 2569.463f, 43.51953f),
			new Vector3(-406.8687f, 6313.629f, 28.9427f),
			new Vector3(471.209f, 2607.885f, 44.47724f),
			new Vector3(495.9004f, -1822.104f, 28.86971f),
			new Vector3(54.23644f, -1873.378f, 22.80583f),
			new Vector3(-678.6112f, 511.1859f, 113.526f),
			new Vector3(861.6411f, -582.8782f, 58.15649f),
			new Vector3(268.9718f, -1712.772f, 29.66879f)
		};

		
		private readonly List<float> homeownerHeadings = new List<float>
		{
			216.3635f,
			28.89284f,
			53.14288f,
			299.9053f,
			337.0093f,
			90.49399f,
			250.5917f,
			109.3158f,
			23.33549f,
			299.2918f,
			198.32f,
			55.28459f,
			43.38983f,
			313.1714f,
			265.4525f,
			264.5157f,
			342.7601f,
			75.85258f,
			255.3645f,
			10.0158f,
			12.94105f,
			165.6396f,
			180.2261f,
			346.136f,
			39.17722f
		};

		
		private readonly List<Vector3> suspectSpawns = new List<Vector3>
		{
			new Vector3(1028.102f, -416.6275f, 65.93145f),
			new Vector3(-1131.757f, 760.4294f, 162.9266f),
			new Vector3(1220.113f, -1637.928f, 47.78682f),
			new Vector3(1278.504f, -677.9854f, 65.9818f),
			new Vector3(1307.867f, -1729.752f, 54.36715f),
			new Vector3(-1343.352f, 593.2214f, 133.7417f),
			new Vector3(1683.046f, 4660.783f, 43.37194f),
			new Vector3(1719.323f, 4634.501f, 43.34169f),
			new Vector3(1740.628f, 3818.62f, 34.73468f),
			new Vector3(197.5572f, 3097.748f, 42.63744f),
			new Vector3(1929.862f, 3883.192f, 32.52633f),
			new Vector3(-215.9129f, 6404.375f, 31.53634f),
			new Vector3(-25.64019f, 6606.626f, 31.4098f),
			new Vector3(-3082.116f, 667.4514f, 11.64609f),
			new Vector3(-3084.853f, 353.7074f, 7.4422f),
			new Vector3(-34.44476f, -1841.752f, 25.805f),
			new Vector3(-341.2585f, 455.3304f, 112.4472f),
			new Vector3(360.2989f, 2578.503f, 43.51953f),
			new Vector3(-400.7046f, 6302.115f, 29.28213f),
			new Vector3(475.5251f, 2613.595f, 43.19867f),
			new Vector3(491.2805f, -1823.211f, 28.48951f),
			new Vector3(58.61945f, -1882.645f, 22.36857f),
			new Vector3(-657.3087f, 506.6458f, 109.656f),
			new Vector3(854.3335f, -573.3047f, 57.7639f),
			new Vector3(267.3581f, -1705.546f, 29.44113f)
		};

		
		private readonly List<string> suspectModel = new List<string>
		{
			"a_m_o_tramp_01",
			"a_m_m_tramp_01",
			"a_f_m_tramp_01",
			"u_m_o_tramp_01",
			"a_f_m_trampbeac_01",
			"a_m_m_trampbeac_01"
		};
	}
}
