using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace LeroyCalloutsV2.Callouts
{
	
	[CalloutInfo("LC - UnderageWithFakeID", 2)]
	internal class UnderageWithFakeID : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Underage With Fake ID";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPointIndex = Common.FindSpawnPointIndex(this.possibleSpawns, true, "CITIZENS_REPORT IN_OR_ON_POSITION", 2, 6000);
				this.spawnPoint = this.possibleSpawns[this.spawnPointIndex];
				base.ShowCalloutAreaBlipBeforeAccepting(this.spawnPoint, 30f);
				base.AddMinimumDistanceCheck(100f, this.spawnPoint);
				base.CalloutMessage = Common.activeCall + "~n~~y~Code 2 Response";
				base.CalloutPosition = this.spawnPoint;
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
						this.clerk = new Ped("mp_m_shopkeep_01", this.spawnPoint, Common.GetHeading(this.spawnPoint, this.possibleSpawns, this.clerkHeadings));
						this.clerk.IsPersistent = true;
						this.clerk.BlockPermanentEvents = true;
						this.clerkBlip = Common.CreateBlip(this.clerk, Color.Orange);
						this.clerkBlip.EnableRoute(Color.Yellow);
						this.suspect = Common.CreatePed(this.suspectModel, this.suspectSpawns[this.spawnPointIndex], true, true);
						this.suspect.Heading = this.suspectHeadings[this.spawnPointIndex];
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
						this.numOfDialog = this.dialogWithSuspectOne.Count;
					}
					else
					{
						this.numOfDialog = this.dialogWithSuspectTwo.Count;
					}
					this.calloutRunning = true;
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag2 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.clerk) < 100f;
						if (flag2)
						{
							Common.WriteToLog("Removing near peds");
							Ped[] allPeds = World.GetAllPeds();
							foreach (Ped ped in allPeds)
							{
								bool flag3 = !ped;
								if (!flag3)
								{
									bool flag4 = ped.DistanceTo2D(this.clerk) > 20f;
									if (!flag4)
									{
										bool flag5 = ped == this.clerk;
										if (!flag5)
										{
											bool flag6 = ped == this.suspect;
											if (!flag6)
											{
												bool flag7 = ped != Game.LocalPlayer.Character;
												if (flag7)
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
						bool flag8 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.clerk) < 20f;
						if (flag8)
						{
							Common.WriteToLog("Player arrived at clerk");
							this.clerkBlip.DisableRoute();
							Persona currentPersona = Functions.GetPersonaForPed(this.suspect);
							this.currentYear = DateTime.Now.Year;
							this.age = this.num.Next(15, 20);
							this.day = this.num.Next(1, 28);
							this.month = this.num.Next(1, 13);
							this.PedBirthday = new DateTime(this.currentYear - this.age, this.month, this.day);
							this.firstName = currentPersona.FullName.Substring(0, currentPersona.FullName.IndexOf(' ') - 1);
							this.lastName = currentPersona.FullName.Substring(currentPersona.FullName.IndexOf(' ') + 1, currentPersona.FullName.Length - currentPersona.FullName.IndexOf(' ') - 1);
							Persona newOwnerpersona = new Persona(this.firstName, this.lastName, currentPersona.Gender, this.PedBirthday);
							Functions.SetPersonaForPed(this.suspect, newOwnerpersona);
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~o~clerk ~s~to advance the conversation");
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag9 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.clerk) < 10f;
						if (flag9)
						{
							if (UnderageWithFakeID.<>o__33.<>p__0 == null)
							{
								UnderageWithFakeID.<>o__33.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(UnderageWithFakeID), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							UnderageWithFakeID.<>o__33.<>p__0.Target(UnderageWithFakeID.<>o__33.<>p__0, NativeFunction.Natives, this.clerk, Game.LocalPlayer.Character, -1);
							bool flag10 = Game.IsKeyDown(this.talk);
							if (flag10)
							{
								Common.WriteToLog("Clerk dialog index: " + this.dialogWithClerkIndex.ToString());
								Game.DisplaySubtitle(this.dialogWithClerk[this.dialogWithClerkIndex]);
								this.dialogWithClerkIndex++;
								bool flag11 = this.dialogWithClerkIndex == this.dialogWithClerk.Count;
								if (flag11)
								{
									Common.WriteToLog("Routing to suspect");
									this.suspectBlip = Common.CreateBlip(this.suspect, Color.Red);
									Game.DisplayHelp("Talk to the ~r~suspect");
									break;
								}
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag12 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 10f;
						if (flag12)
						{
							bool flag13 = Game.IsKeyDown(this.talk);
							if (flag13)
							{
								this.suspect.Tasks.Clear();
								if (UnderageWithFakeID.<>o__33.<>p__1 == null)
								{
									UnderageWithFakeID.<>o__33.<>p__1 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(UnderageWithFakeID), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								UnderageWithFakeID.<>o__33.<>p__1.Target(UnderageWithFakeID.<>o__33.<>p__1, NativeFunction.Natives, this.suspect, Game.LocalPlayer.Character, -1);
								Common.WriteToLog("Suspect dialog index: " + this.dialogIndex.ToString());
								bool flag14 = this.outcome == 1;
								if (flag14)
								{
									Game.DisplaySubtitle(this.dialogWithSuspectOne[this.dialogIndex]);
								}
								else
								{
									bool flag15 = this.outcome == 2;
									if (flag15)
									{
										Game.DisplaySubtitle(this.dialogWithSuspectTwo[this.dialogIndex]);
									}
								}
								this.dialogIndex++;
								bool flag16 = this.dialogIndex == this.numOfDialog;
								if (flag16)
								{
									Common.WriteToLog("Finished with dialog");
									bool flag17 = this.outcome == 1;
									if (flag17)
									{
										GameFiber.Wait(2000);
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
						bool flag18 = !EntityExtensions.Exists(this.suspect) || Functions.IsPedArrested(this.suspect) || this.suspect.IsDead;
						if (flag18)
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

		
		private LHandle pursuit;

		
		private int dialogIndex = 0;

		
		private int dialogWithWitnessIndex = 0;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private int outcome;

		
		private int spawnPointIndex;

		
		private int numOfDialog;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private Ped clerk;

		
		private Blip clerkBlip;

		
		private DateTime PedBirthday;

		
		private int currentYear;

		
		private int day;

		
		private int month;

		
		private int age;

		
		private string firstName;

		
		private string lastName;

		
		private Random num = new Random();

		
		private readonly List<string> dialogWithClerk = new List<string>
		{
			"~o~Clerk: ~s~Hello officer. This kid just tried to buy alcohol with a fake ID",
			"~b~You: ~s~How do you know it is fake?",
			"~o~Clerk: ~s~Well for starters no one's real name is actually \"John Doe\"",
			"~o~Clerk: ~s~And secondly, you can tell where it has been printed with a home printer.",
			"~b~You: ~s~Alright, thank you."
		};

		
		private int dialogWithClerkIndex = 0;

		
		private readonly List<string> dialogWithSuspectOne = new List<string>
		{
			"~r~Suspect: ~s~What he said is a lie!",
			"~b~You: ~s~Oh? Then can I see the ID you showed him?",
			"~r~Suspect: ~s~Uh....no.",
			"~b~You: ~s~If you don't show me the ID, I'm going to assume that you are the one lying.",
			"~r~Suspect: ~s~I don't have to show you anything."
		};

		
		private readonly List<string> dialogWithSuspectTwo = new List<string>
		{
			"~b~You: ~s~Hey, are you trying to buy alcohol with a fake ID?",
			"~r~Suspect: ~s~Um..no.",
			"~b~You: ~s~Really? Becuase the clerk is telling me that you are.",
			"~r~Suspect: ~s~....yes I am. My friends are having a party tonight and I really just wanted to be the cool kid.",
			"~b~You: ~s~You don't have to break the law to be cool.",
			"~r~Suspect: ~s~I know. Please don't arrest me."
		};

		
		private readonly List<Vector3> possibleSpawns = new List<Vector3>
		{
			new Vector3(-2966.444f, 390.2466f, 15.04331f),
			new Vector3(-1485.795f, -378.3975f, 40.16344f),
			new Vector3(1133.787f, -981.9125f, 46.41585f),
			new Vector3(1696.683f, 4923.199f, 42.06364f),
			new Vector3(1165.31f, -323.576f, 69.20511f),
			new Vector3(-1819.074f, 793.7687f, 138.0759f),
			new Vector3(-1222.435f, -908.8213f, 12.32636f),
			new Vector3(-705.698f, -914.5412f, 19.21559f),
			new Vector3(-46.91849f, -1758.936f, 29.421f),
			new Vector3(549.3503f, 2671.28f, 42.15654f),
			new Vector3(1959.418f, 3740.303f, 32.34375f),
			new Vector3(24.12959f, -1346.609f, 29.49702f),
			new Vector3(2556.623f, 380.5275f, 108.6229f),
			new Vector3(372.3212f, 327.2576f, 103.5664f),
			new Vector3(2677.665f, 3279.295f, 55.24113f),
			new Vector3(1727.728f, 6415.88f, 35.03722f),
			new Vector3(-3039.334f, 584.024f, 7.908929f),
			new Vector3(-3242.933f, 999.6537f, 12.83071f),
			new Vector3(1166.602f, 2711.259f, 38.15771f)
		};

		
		private readonly List<float> clerkHeadings = new List<float>
		{
			84.8942f,
			137.5399f,
			273.0878f,
			317.7079f,
			97.56994f,
			131.8244f,
			30.56647f,
			88.78531f,
			41.83687f,
			91.77502f,
			298.8092f,
			271.519f,
			354.4906f,
			252.5417f,
			335.7393f,
			248.5619f,
			16.87262f,
			355.0541f,
			175.639f
		};

		
		private readonly List<Vector3> suspectSpawns = new List<Vector3>
		{
			new Vector3(-2970.124f, 388.2447f, 15.04331f),
			new Vector3(-1489.261f, -378.3443f, 40.16343f),
			new Vector3(1139.348f, -983.5212f, 46.41582f),
			new Vector3(1706.066f, 4929.756f, 42.06364f),
			new Vector3(1153.23f, -325.7385f, 69.20511f),
			new Vector3(-1828.666f, 791.9163f, 138.2538f),
			new Vector3(-1222.369f, -904.2727f, 12.32635f),
			new Vector3(-711.4207f, -913.915f, 19.2156f),
			new Vector3(-47.99979f, -1752.15f, 29.42101f),
			new Vector3(543.8217f, 2668.392f, 42.15649f),
			new Vector3(1967.293f, 3744.811f, 32.34377f),
			new Vector3(32.4838f, -1343.418f, 29.49702f),
			new Vector3(2553.058f, 385.8712f, 108.6229f),
			new Vector3(379.6143f, 323.7968f, 103.5664f),
			new Vector3(2680.204f, 3287.169f, 55.24113f),
			new Vector3(1733.681f, 6412.224f, 35.03722f),
			new Vector3(-3044.768f, 590.7498f, 7.908933f),
			new Vector3(-3246.068f, 1002.894f, 12.83071f),
			new Vector3(1168.425f, 2706.349f, 38.15771f)
		};

		
		private readonly List<float> suspectHeadings = new List<float>
		{
			173.6158f,
			42.03574f,
			286.043f,
			97.62618f,
			99.46719f,
			43.51751f,
			119.5595f,
			308.9696f,
			319.9807f,
			93.40808f,
			297.8995f,
			359.9999f,
			89.09688f,
			156.3688f,
			144.9468f,
			329.6634f,
			106.2863f,
			83.32326f,
			181.9695f
		};

		
		private readonly List<string> suspectModel = new List<string>
		{
			"a_f_y_bevhills_01",
			"a_f_y_bevhills_02",
			"ig_ramp_hipster",
			"a_f_y_soucent_03"
		};
	}
}
