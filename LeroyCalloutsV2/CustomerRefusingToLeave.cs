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
	
	[CalloutInfo("LC - CustomerRefusingToLeave", 2)]
	internal class CustomerRefusingToLeave : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Customer Refusing To Leave";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.clerkSpawnIndex = Common.FindSpawnPointIndex(this.possibleSpawns, true, "CITIZENS_REPORT IN_OR_ON_POSITION", 2, 7000);
				this.clerkSpawn = this.possibleSpawns[this.clerkSpawnIndex];
				base.ShowCalloutAreaBlipBeforeAccepting(this.clerkSpawn, 30f);
				base.AddMinimumDistanceCheck(100f, this.clerkSpawn);
				base.CalloutMessage = Common.activeCall + "~n~~y~Code 2 Response";
				base.CalloutPosition = this.clerkSpawn;
				bool flag = this.clerkSpawn != Vector3.Zero;
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
						this.clerk = new Ped("mp_m_shopkeep_01", this.clerkSpawn, this.clerkHeadings[this.clerkSpawnIndex])
						{
							IsPersistent = true,
							BlockPermanentEvents = true
						};
						this.suspect = Common.CreatePed(true, this.suspectSpawns[this.clerkSpawnIndex], true, true);
						this.suspect.Heading = this.suspectHeadings[this.clerkSpawnIndex];
						this.clerkBlip = Common.CreateBlip(this.clerk, Color.Orange);
						this.clerkBlip.EnableRoute(Color.Yellow);
						this.suspectBlip = Common.CreateBlip(this.suspect, Color.Red);
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
							bool flag3 = this.outcome == 3;
							if (flag3)
							{
								this.numOfDialog = this.dialogWithSuspectThree.Count;
								int weapon = this.num.Next(0, 3);
								bool flag4 = weapon == 0;
								if (flag4)
								{
									this.suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, false);
								}
								else
								{
									bool flag5 = weapon == 1;
									if (flag5)
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
							else
							{
								this.numOfDialog = 1;
							}
						}
					}
					this.calloutRunning = true;
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag6 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.clerk) < 50f;
						if (flag6)
						{
							Common.WriteToLog("Removing near peds");
							Ped[] allPeds = World.GetAllPeds();
							foreach (Ped ped in allPeds)
							{
								bool flag7 = !ped;
								if (!flag7)
								{
									bool flag8 = ped.DistanceTo2D(this.clerk) > 20f;
									if (!flag8)
									{
										bool flag9 = ped == this.clerk;
										if (!flag9)
										{
											bool flag10 = ped == this.suspect;
											if (!flag10)
											{
												bool flag11 = ped != Game.LocalPlayer.Character;
												if (flag11)
												{
													ped.Delete();
												}
											}
										}
									}
								}
							}
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag12 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.clerk) < 20f;
						if (flag12)
						{
							Common.WriteToLog("Player arrived at witness");
							this.clerkBlip.DisableRoute();
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~o~clerk ~s~to advance the conversation");
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag13 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.clerk) < 10f;
						if (flag13)
						{
							if (CustomerRefusingToLeave.<>o__29.<>p__0 == null)
							{
								CustomerRefusingToLeave.<>o__29.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(CustomerRefusingToLeave), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							CustomerRefusingToLeave.<>o__29.<>p__0.Target(CustomerRefusingToLeave.<>o__29.<>p__0, NativeFunction.Natives, this.clerk, Game.LocalPlayer.Character, -1);
							bool flag14 = Game.IsKeyDown(this.talk);
							if (flag14)
							{
								Common.WriteToLog("Clerk dialog index: " + this.dialogWithClerkIndex.ToString());
								Game.DisplaySubtitle(this.dialogWithClerk[this.dialogWithClerkIndex]);
								this.dialogWithClerkIndex++;
								bool flag15 = this.dialogWithClerkIndex == this.dialogWithClerk.Count;
								if (flag15)
								{
									Game.DisplayHelp("Talk to the ~r~suspect");
									break;
								}
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag16 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 10f;
						if (flag16)
						{
							bool flag17 = Game.IsKeyDown(this.talk);
							if (flag17)
							{
								this.suspect.Tasks.Clear();
								if (CustomerRefusingToLeave.<>o__29.<>p__1 == null)
								{
									CustomerRefusingToLeave.<>o__29.<>p__1 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(CustomerRefusingToLeave), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								CustomerRefusingToLeave.<>o__29.<>p__1.Target(CustomerRefusingToLeave.<>o__29.<>p__1, NativeFunction.Natives, this.suspect, Game.LocalPlayer.Character, -1);
								Common.WriteToLog("Suspect dialog index: " + this.dialogWithSuspectIndex.ToString());
								bool flag18 = this.outcome == 1;
								if (flag18)
								{
									Game.DisplaySubtitle(this.dialogWithSuspectOne[this.dialogWithSuspectIndex]);
								}
								else
								{
									bool flag19 = this.outcome == 2;
									if (flag19)
									{
										Game.DisplaySubtitle(this.dialogWithSuspectTwo[this.dialogWithSuspectIndex]);
									}
									else
									{
										bool flag20 = this.outcome == 3;
										if (flag20)
										{
											Game.DisplaySubtitle(this.dialogWithSuspectThree[this.dialogWithSuspectIndex]);
										}
									}
								}
								this.dialogWithSuspectIndex++;
								bool flag21 = this.dialogWithSuspectIndex == this.numOfDialog;
								if (flag21)
								{
									Common.WriteToLog("Finished with dialog");
									bool flag22 = this.outcome == 3;
									if (flag22)
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
						bool flag23 = !EntityExtensions.Exists(this.suspect) || Functions.IsPedArrested(this.suspect) || this.suspect.IsDead;
						if (flag23)
						{
							this.calloutRunning = false;
							Common.WriteToLog("Suspect arrested or dead.");
							Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
							Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
							this.clerk.Tasks.Pause(-1);
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
				bool flag3 = !EntityExtensions.Exists(this.clerk) || !EntityExtensions.Exists(this.suspect);
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
			Common.Dismiss(this.clerk);
			Common.Dismiss(this.suspectBlip);
			Common.Dismiss(this.clerkBlip);
			base.End();
		}

		
		private Ped clerk;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private Blip clerkBlip;

		
		private Vector3 clerkSpawn;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private int Index = 0;

		
		private int clerkSpawnIndex;

		
		private bool spawnFound = false;

		
		private bool clerkReached = false;

		
		private bool suspectReached = false;

		
		private bool suspectStopped = false;

		
		private bool calloutRunning;

		
		private int outcome;

		
		private Random num = new Random();

		
		private int numOfDialog;

		
		private readonly List<string> dialogWithClerk = new List<string>
		{
			"~o~Clerk: ~s~Hello officer. That guy over there will not leave.",
			"~b~You: ~s~Okay, has he done something wrong?",
			"~o~Clerk: ~s~He has stolen from us before and is banned from coming back",
			"~b~You: ~s~Alright, you've already asked him to leave?",
			"~o~Clerk: ~s~Several times.",
			"~b~You: ~s~Okay, I'll get him out.",
			"~o~Clerk: ~s~Thank you."
		};

		
		private int dialogWithClerkIndex = 0;

		
		private readonly List<string> dialogWithSuspectOne = new List<string>
		{
			"~r~Suspect: ~s~Don't bother telling me to leave! I have a right to be here!",
			"~b~You: ~s~No you don't. The clerk has asked you to leave multiple times.",
			"~r~Suspect: ~s~He's not the boss of me. I'm still shopping",
			"~b~You: ~s~Sir, if you do not leave I will have to arrest you.",
			"~r~Suspect: ~s~Arrest me and I'll sue you!"
		};

		
		private readonly List<string> dialogWithSuspectTwo = new List<string>
		{
			"~b~You: ~s~Excuse me sir. Can I talk to you for a minute?",
			"~r~Suspect: ~s~Sure. Have I done something wrong?",
			"~b~You: ~s~The clerk tells me you're banned from coming in this store.",
			"~r~Suspect: ~s~I know. I've stolen from here before. But I'm a different person now.",
			"~b~You: ~s~That may be so, but you're still banned from here.",
			"~r~Suspect: ~s~But I won't steal anymore!",
			"~b~You: ~s~The store still does not want you here. You'll have to leave.",
			"~r~Suspect: ~s~Fine. I'll go."
		};

		
		private readonly List<string> dialogWithSuspectThree = new List<string>
		{
			"~b~You: ~s~Excuse me sir. Can I talk to you for a minute?",
			"~r~Suspect: ~s~Now what?",
			"~b~You: ~s~The clerk tells me you're banned from coming in this store.",
			"~r~Suspect: ~s~Yeah, what's your point?",
			"~b~You: ~s~You'll have to leave.",
			"~r~Suspect: ~s~Nah. I'm not done shopping yet.",
			"~b~You: ~s~Either leave, or I'll have to arrest you for trespassing.",
			"~r~Suspect: ~s~You'll have to take me down first."
		};

		
		private int dialogWithSuspectIndex = 0;

		
		private readonly List<Vector3> possibleSpawns = new List<Vector3>
		{
			new Vector3(1133.787f, -981.9125f, 46.41585f),
			new Vector3(1165.31f, -323.576f, 69.20511f),
			new Vector3(1166.602f, 2711.259f, 38.15771f),
			new Vector3(-1222.435f, -908.8213f, 12.32636f),
			new Vector3(-1485.795f, -378.3975f, 40.16344f),
			new Vector3(1696.683f, 4923.199f, 42.06364f),
			new Vector3(1727.728f, 6415.88f, 35.03722f),
			new Vector3(-1819.074f, 793.7687f, 138.0759f),
			new Vector3(1959.418f, 3740.303f, 32.34375f),
			new Vector3(24.12959f, -1346.609f, 29.49702f),
			new Vector3(2556.623f, 380.5275f, 108.6229f),
			new Vector3(2677.665f, 3279.295f, 55.24113f),
			new Vector3(-3039.334f, 584.024f, 7.908929f),
			new Vector3(-3242.933f, 999.6537f, 12.83071f),
			new Vector3(372.3212f, 327.2576f, 103.5664f),
			new Vector3(-46.91849f, -1758.936f, 29.421f),
			new Vector3(549.3503f, 2671.28f, 42.15654f),
			new Vector3(-705.698f, -914.5412f, 19.21559f),
			new Vector3(-2966.444f, 390.2466f, 15.04331f)
		};

		
		private readonly List<float> clerkHeadings = new List<float>
		{
			273.0878f,
			97.56994f,
			175.639f,
			30.56647f,
			137.5399f,
			317.7079f,
			248.5619f,
			131.8244f,
			298.8092f,
			271.519f,
			354.4906f,
			335.7393f,
			16.87262f,
			355.0541f,
			252.5417f,
			41.83687f,
			91.77502f,
			88.78531f,
			84.8942f
		};

		
		private readonly List<Vector3> suspectSpawns = new List<Vector3>
		{
			new Vector3(1139.348f, -983.5212f, 46.41582f),
			new Vector3(1153.23f, -325.7385f, 69.20511f),
			new Vector3(1168.425f, 2706.349f, 38.15771f),
			new Vector3(-1222.369f, -904.2727f, 12.32635f),
			new Vector3(-1489.261f, -378.3443f, 40.16343f),
			new Vector3(1706.066f, 4929.756f, 42.06364f),
			new Vector3(1733.681f, 6412.224f, 35.03722f),
			new Vector3(-1828.666f, 791.9163f, 138.2538f),
			new Vector3(1967.293f, 3744.811f, 32.34377f),
			new Vector3(32.4838f, -1343.418f, 29.49702f),
			new Vector3(2553.058f, 385.8712f, 108.6229f),
			new Vector3(2680.204f, 3287.169f, 55.24113f),
			new Vector3(-3044.768f, 590.7498f, 7.908933f),
			new Vector3(-3246.068f, 1002.894f, 12.83071f),
			new Vector3(379.6143f, 323.7968f, 103.5664f),
			new Vector3(-47.99979f, -1752.15f, 29.42101f),
			new Vector3(543.8217f, 2668.392f, 42.15649f),
			new Vector3(-711.4207f, -913.915f, 19.2156f),
			new Vector3(-2970.124f, 388.2447f, 15.04331f)
		};

		
		private readonly List<float> suspectHeadings = new List<float>
		{
			286.043f,
			99.46719f,
			181.9695f,
			119.5595f,
			42.03574f,
			97.62618f,
			329.6634f,
			43.51751f,
			297.8995f,
			359.9999f,
			89.09688f,
			144.9468f,
			106.2863f,
			83.32326f,
			156.3688f,
			319.9807f,
			93.40808f,
			308.9696f,
			173.6158f
		};
	}
}
