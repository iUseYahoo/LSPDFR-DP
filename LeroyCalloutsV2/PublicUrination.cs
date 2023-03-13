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
	
	[CalloutInfo("LC - PublicUrination", 2)]
	internal class PublicUrination : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Public Urination";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPointIndex = Common.FindSpawnPointIndex(this.possibleSpawns, true, "CITIZENS_REPORT CRIME_DISTURBANCE IN_OR_ON_POSITION", 2, 7000);
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
						this.suspect = Common.CreatePed(true, this.spawnPoint, true, true);
						this.searchArea = Common.CreateSearchArea(this.suspect.Position, 100f);
						this.searchArea.EnableRoute(Color.Yellow);
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
						this.numOfDialog = this.dialogWithPedOne.Count;
					}
					else
					{
						bool flag2 = this.outcome == 2;
						if (flag2)
						{
							this.numOfDialog = this.dialogWithPedTwo.Count;
						}
						else
						{
							bool flag3 = this.outcome == 3;
							if (flag3)
							{
								this.numOfDialog = this.dialogWithPedThree.Count;
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
								this.numOfDialog = this.dialogIndex;
							}
						}
					}
					this.suspect.Tasks.PlayAnimation("missheist_agency3aig_23", "urinal_asifyouneededproof", 1f, 1);
					this.calloutRunning = true;
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag6 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 30f;
						if (flag6)
						{
							Common.WriteToLog("Player arrived at suspect");
							this.searchArea.DisableRoute();
							this.searchArea.Delete();
							this.suspectBlip = this.suspect.AttachBlip();
							this.suspectBlip.Color = Color.Red;
							Game.DisplayNotification("~b~You: ~s~Dispatch, suspect located.");
							Functions.PlayScannerAudio("REPORT_RESPONSE_COPY OUTRO");
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~r~suspect ~s~to advance the conversation");
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag7 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 10f;
						if (flag7)
						{
							bool flag8 = this.dialogIndex > 0;
							if (flag8)
							{
								if (PublicUrination.<>o__20.<>p__0 == null)
								{
									PublicUrination.<>o__20.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(PublicUrination), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								PublicUrination.<>o__20.<>p__0.Target(PublicUrination.<>o__20.<>p__0, NativeFunction.Natives, this.suspect, Game.LocalPlayer.Character, -1);
							}
							bool flag9 = Game.IsKeyDown(this.talk);
							if (flag9)
							{
								Common.WriteToLog("Suspect dialog index: " + this.dialogIndex.ToString());
								bool flag10 = this.outcome == 1;
								if (flag10)
								{
									Game.DisplaySubtitle(this.dialogWithPedOne[this.dialogIndex]);
								}
								else
								{
									bool flag11 = this.outcome == 2;
									if (flag11)
									{
										Game.DisplaySubtitle(this.dialogWithPedTwo[this.dialogIndex]);
									}
									else
									{
										bool flag12 = this.outcome == 3;
										if (flag12)
										{
											Game.DisplaySubtitle(this.dialogWithPedThree[this.dialogIndex]);
										}
									}
								}
								this.dialogIndex++;
							}
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
				bool flag3 = !EntityExtensions.Exists(this.suspect);
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
			Common.Dismiss(this.suspectBlip);
			base.End();
		}

		
		private LHandle pursuit;

		
		private int dialogIndex = 0;

		
		private int spawnPointIndex;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private int outcome;

		
		private int numOfDialog;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private Random num = new Random();

		
		private Blip searchArea;

		
		private readonly List<string> dialogWithPedOne = new List<string>
		{
			"~b~You: ~s~Hey man. What are you doing?",
			"~r~Suspect: ~s~Uh...nothing. What are you doing?",
			"~b~You: ~s~Stopping you from urinating in public. You know that's illegal right?",
			"~r~Suspect: ~s~But I couldn't hold it! What am I supposed to do?",
			"~b~You: ~s~There are plenty of public restrooms you can use."
		};

		
		private readonly List<string> dialogWithPedTwo = new List<string>
		{
			"~b~You: ~s~Hey man. What are you doing?",
			"~r~Suspect: ~s~I'm taking a piss! What does it look like I'm doing!?",
			"~b~You: ~s~Well you know what you're doing is illegal right?",
			"~r~Suspect: ~s~It's a bodilimilaly function.",
			"~b~You: ~s~Are you drunk sir?",
			"~r~Suspect: ~s~You're the cop. You tell me."
		};

		
		private readonly List<string> dialogWithPedThree = new List<string>
		{
			"~b~You: ~s~Hey man. What are you doing?",
			"~r~Suspect: ~s~Minding my own business. Unlike you.",
			"~b~You: ~s~You're urinating in public. That makes it my business.",
			"~r~Suspect: ~s~In that case, know what else is your business?",
			"~b~You: ~s~What?",
			"~r~Suspect: ~s~This."
		};

		
		private readonly List<Vector3> possibleSpawns = new List<Vector3>
		{
			new Vector3(-1010.741f, -2455.16f, 13.88068f),
			new Vector3(1017.207f, -2104.174f, 30.86746f),
			new Vector3(-1020.262f, 484.3104f, 79.17031f),
			new Vector3(-1037.076f, -239.7621f, 37.84232f),
			new Vector3(1048.383f, -707.3843f, 57.08696f),
			new Vector3(1053.514f, -591.475f, 57.83666f),
			new Vector3(-1064.277f, -2725.06f, 13.90734f),
			new Vector3(1095.158f, -3034.686f, 5.901039f),
			new Vector3(1109.69f, -503.7403f, 63.66413f),
			new Vector3(-1130.725f, 567.0638f, 102.1038f),
			new Vector3(120.1067f, -1491.705f, 29.14253f),
			new Vector3(1222.022f, -779.9246f, 59.352f),
			new Vector3(-1248.561f, 460.8792f, 93.33126f),
			new Vector3(-141.4844f, -1785.691f, 29.82232f),
			new Vector3(1439.873f, 3589.242f, 35.00494f),
			new Vector3(-1517.961f, -859.8929f, 23.30872f),
			new Vector3(-152.9462f, 6399.213f, 31.49254f),
			new Vector3(-1587.923f, 456.2684f, 109.1438f),
			new Vector3(1660.296f, 3843.837f, 34.79649f),
			new Vector3(1662.807f, 4816.597f, 41.85242f),
			new Vector3(1681.738f, 4890.935f, 42.03154f),
			new Vector3(-1710.846f, -1066.532f, 13.01735f),
			new Vector3(173.2321f, 6636.256f, 31.60192f),
			new Vector3(-1842.37f, -1208.899f, 13.01723f),
			new Vector3(1866.105f, 3880.311f, 32.92999f),
			new Vector3(1922.764f, 3717.49f, 32.59797f),
			new Vector3(-197.2676f, -772.1279f, 30.45401f),
			new Vector3(-25.12197f, 6525.409f, 31.49085f),
			new Vector3(2527.406f, 4218.503f, 39.94378f),
			new Vector3(-257.7882f, 6294.273f, 31.47636f),
			new Vector3(2711.188f, 4340.3f, 45.85197f),
			new Vector3(278.4664f, 2614.286f, 44.63428f),
			new Vector3(295.5348f, -904.9543f, 29.26789f),
			new Vector3(303.6479f, -1179.192f, 29.3893f),
			new Vector3(36.48035f, -46.55709f, 66.55117f),
			new Vector3(365.7429f, -71.84046f, 67.33693f),
			new Vector3(-378.9965f, -29.60985f, 47.16819f),
			new Vector3(391.2911f, -2000.496f, 23.5487f),
			new Vector3(-391.5312f, 686.7051f, 162.9257f),
			new Vector3(-398.2058f, -1879.553f, 20.52785f),
			new Vector3(-404.2859f, 6232.424f, 31.13232f),
			new Vector3(-507.615f, -1234.391f, 23.53824f),
			new Vector3(-520.1835f, -1788.741f, 21.84936f),
			new Vector3(552.7163f, -1564.617f, 29.23797f),
			new Vector3(-577.4209f, -1101.073f, 22.37851f),
			new Vector3(590.9401f, -1845.803f, 24.89995f),
			new Vector3(-614.4665f, -907.0989f, 24.11508f),
			new Vector3(-619.6902f, 740.3724f, 180.1299f),
			new Vector3(645.311f, 2720.665f, 41.84392f),
			new Vector3(-666.4569f, 5833.41f, 17.33124f),
			new Vector3(692.0746f, -29.69786f, 83.3097f),
			new Vector3(-700.9319f, 5784.743f, 17.33095f),
			new Vector3(-752.0809f, -84.5169f, 37.32092f),
			new Vector3(-755.1775f, 5601.731f, 41.66511f),
			new Vector3(-760.134f, 5519.781f, 34.75682f),
			new Vector3(774.9382f, -2993.529f, 5.881294f),
			new Vector3(824.6061f, -3204.2f, 5.994989f),
			new Vector3(-857.2831f, -2334.603f, 13.95571f),
			new Vector3(915.552f, 3594.834f, 33.07991f),
			new Vector3(-927.0977f, -2545.533f, 14.05215f),
			new Vector3(959.6217f, -1828.613f, 31.23443f),
			new Vector3(-967.4885f, -485.1422f, 37.31918f),
			new Vector3(375.2837f, -735.5702f, 29.28282f)
		};

		
		private readonly List<float> possibleHeadings = new List<float>
		{
			102.2404f,
			221.8473f,
			235.3945f,
			59.64856f,
			21.62986f,
			148.1056f,
			212.7808f,
			232.9196f,
			30.73881f,
			58.47614f,
			195.323f,
			224.5874f,
			190.8586f,
			190.3175f,
			198.536f,
			222.358f,
			67.61678f,
			114.6249f,
			158.0331f,
			70.23665f,
			306.9579f,
			48.05253f,
			45.00497f,
			19.92123f,
			115.9866f,
			29.92277f,
			38.2953f,
			21.98178f,
			22.21887f,
			52.33223f,
			252.81f,
			237.0567f,
			217.1858f,
			307.2975f,
			291.7818f,
			30.67217f,
			197.2334f,
			91.13591f,
			350.5897f,
			174.0015f,
			277.2698f,
			46.81156f,
			104.4888f,
			260.0735f,
			1.242505f,
			257.978f,
			299.3028f,
			155.8708f,
			4.588594f,
			132.8138f,
			237.3626f,
			314.1893f,
			345.2593f,
			52.11481f,
			210.476f,
			265.0364f,
			15.19892f,
			302.3857f,
			115.7975f,
			6.609747f,
			263.3299f,
			65.40585f,
			33.93504f
		};
	}
}
