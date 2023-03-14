using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Bar Fight", 2)]
	public class BarFight : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Bar Fight Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 2);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is " + this.MainScenario.ToString());
			CallHandler.locationChooser(this.list, 600f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			bool result;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
				base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 75f);
				base.AddMinimumDistanceCheck(25f, this.MainSpawnPoint);
				Functions.PlayScannerAudio("CITIZENS_REPORT CRIME_DISTURBING_THE_PEACE_01");
				base.CalloutMessage = "Bar Fight";
				base.CalloutPosition = this.MainSpawnPoint;
				base.CalloutAdvisory = "A Fight Has Broken Out Between Two ~r~Suspects ~w~at a ~y~Bar.";
				result = base.OnBeforeCalloutDisplayed();
			}
			else
			{
				Game.LogTrivial("No location nearby. Ending Callout");
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Bar Fight Callout Accepted by User.");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 3", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~r~Code 3.");
			}
			this.Peds = new string[]
			{
				"A_M_Y_SouCent_01",
				"A_M_Y_StWhi_01",
				"A_M_Y_StBla_01",
				"A_M_Y_Downtown_01",
				"A_M_Y_BevHills_01",
				"G_M_Y_MexGang_01",
				"G_M_Y_MexGoon_01",
				"G_M_Y_StrPunk_01"
			};
			Random r2 = new Random();
			int SuspectModel = r2.Next(1, this.Peds.Length);
			if (BarFight.<>o__16.<>p__0 == null)
			{
				BarFight.<>o__16.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(BarFight), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			Vector3 nodePosition;
			float heading;
			BarFight.<>o__16.<>p__0.Target(BarFight.<>o__16.<>p__0, NativeFunction.Natives, this.MainSpawnPoint, ref nodePosition, ref heading, 1, 3f, 0);
			this.Suspect = new Ped(this.Peds[SuspectModel], this.MainSpawnPoint, heading);
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			Vector3 Suspect2SpawnPoint = this.Suspect.GetOffsetPositionFront(2f);
			this.Suspect2 = new Ped(this.Peds[SuspectModel - 1], Suspect2SpawnPoint, heading);
			this.Suspect2.IsPersistent = true;
			this.Suspect2.BlockPermanentEvents = true;
			this.Suspect2.Tasks.AchieveHeading(this.Suspect.Heading - 180f);
			this.AreaBlip = new Blip(this.Suspect.Position, 25f);
			this.AreaBlip.Color = Color.Yellow;
			this.AreaBlip.Alpha = 0.67f;
			this.AreaBlip.IsRouteEnabled = true;
			this.AreaBlip.Name = "Bar";
			bool flag = !this.CalloutRunning;
			if (flag)
			{
				this.CalloutRunning = true;
			}
			this.Callout();
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Bar Fight Callout Not Accepted by User.");
			base.OnCalloutNotAccepted();
		}

		
		private void Callout()
		{
			this.CalloutRunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.CalloutRunning)
					{
						while (this.player.DistanceTo(this.Suspect) >= 25f && !Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(Config.CalloutEndKey);
						if (flag)
						{
							EndCalloutHandler.CalloutForcedEnd = true;
						}
						else
						{
							Game.LogTrivial("YOBBINCALLOUTS: Player Arrived on Scene.");
							this.AreaBlip.Delete();
							this.SuspectBlip = this.Suspect.AttachBlip();
							this.SuspectBlip.IsFriendly = false;
							this.SuspectBlip.Scale = 0.75f;
							this.SuspectBlip.Name = "Suspect";
							this.Suspect.RelationshipGroup = RelationshipGroup.Gang1;
							this.Suspect.RelationshipGroup.SetRelationshipWith(RelationshipGroup.Gang2, 5);
							this.Suspect2Blip = this.Suspect2.AttachBlip();
							this.Suspect2Blip.IsFriendly = false;
							this.Suspect2Blip.Scale = 0.75f;
							this.Suspect2Blip.Name = "Suspect";
							this.Suspect2.RelationshipGroup = RelationshipGroup.Gang2;
							this.Suspect2.RelationshipGroup.SetRelationshipWith(RelationshipGroup.Gang1, 5);
							Game.DisplayHelp("Break Up the ~r~Fight.");
							while (this.player.DistanceTo(this.Suspect) >= 30f && this.player.IsInAnyVehicle(false))
							{
								GameFiber.Wait(0);
							}
							bool flag2 = this.MainScenario == 0;
							if (flag2)
							{
								bool calloutInterface = Main.CalloutInterface;
								if (calloutInterface)
								{
									CalloutInterfaceHandler.SendMessage(this, "Reporting Two Males Fighting Outside the Bar");
								}
								this.Suspect.Tasks.FightAgainstClosestHatedTarget(10f, -1);
								this.Suspect2.Tasks.FightAgainstClosestHatedTarget(10f, -1);
								while (EntityExtensions.Exists(this.Suspect) || EntityExtensions.Exists(this.Suspect2))
								{
									GameFiber.Yield();
									bool flag3 = !EntityExtensions.Exists(this.Suspect) || this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect);
									if (flag3)
									{
										bool flag4 = !EntityExtensions.Exists(this.Suspect2) || this.Suspect2.IsDead || Functions.IsPedArrested(this.Suspect2);
										if (flag4)
										{
											break;
										}
									}
								}
								bool flag5 = EntityExtensions.Exists(this.Suspect);
								if (flag5)
								{
									bool flag6 = Functions.IsPedArrested(this.Suspect);
									if (flag6)
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Following the Fight.");
									}
									else
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Fight.");
									}
								}
								else
								{
									GameFiber.Wait(1000);
									Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Fight.");
								}
								bool flag7 = EntityExtensions.Exists(this.Suspect2);
								if (flag7)
								{
									bool flag8 = Functions.IsPedArrested(this.Suspect2);
									if (flag8)
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, a Second Suspect is also Under ~g~Arrest~w~ Following the Fight.");
									}
									else
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, a Second Suspect Was also ~r~Killed~w~ Following the Fight.");
									}
								}
								else
								{
									GameFiber.Wait(1000);
									Game.DisplayNotification("Dispatch, a Second Suspect Was also ~r~Killed~w~ Following Fight.");
								}
								GameFiber.Wait(2000);
								Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
								GameFiber.Wait(2000);
							}
							else
							{
								GameFiber.Wait(1000);
								Game.DisplaySubtitle("~r~Oh Shit, It's the Cops!", 2500);
								GameFiber.Wait(2500);
								Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
								this.SuspectPursuit = Functions.CreatePursuit();
								Functions.RequestBackup(this.player.Position, 1, 0);
								Functions.SetPursuitIsActiveForPlayer(this.SuspectPursuit, true);
								Functions.AddPedToPursuit(this.SuspectPursuit, this.Suspect);
								Functions.AddPedToPursuit(this.SuspectPursuit, this.Suspect2);
								while (Functions.IsPursuitStillRunning(this.SuspectPursuit))
								{
									GameFiber.Wait(0);
								}
								while (EntityExtensions.Exists(this.Suspect) || EntityExtensions.Exists(this.Suspect2))
								{
									GameFiber.Yield();
									bool flag9 = !EntityExtensions.Exists(this.Suspect) || this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect);
									if (flag9)
									{
										bool flag10 = !EntityExtensions.Exists(this.Suspect2) || this.Suspect2.IsDead || Functions.IsPedArrested(this.Suspect2);
										if (flag10)
										{
											break;
										}
									}
								}
								bool flag11 = EntityExtensions.Exists(this.Suspect);
								if (flag11)
								{
									bool flag12 = Functions.IsPedArrested(this.Suspect);
									if (flag12)
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Following the Pursuit.");
									}
									else
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Pursuit.");
									}
								}
								else
								{
									GameFiber.Wait(1000);
									Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Pursuit.");
								}
								bool flag13 = EntityExtensions.Exists(this.Suspect2);
								if (flag13)
								{
									bool flag14 = Functions.IsPedArrested(this.Suspect2);
									if (flag14)
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, a Second Suspect is also Under ~g~Arrest~w~ Following the Pursuit.");
									}
									else
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, a Second Suspect Was also ~r~Killed~w~ Following the Pursuit.");
									}
								}
								else
								{
									GameFiber.Wait(1000);
									Game.DisplayNotification("Dispatch, a Second Suspect Was also ~r~Killed~w~ Following the Pursuit.");
								}
								GameFiber.Wait(2000);
								Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
								GameFiber.Wait(2000);
							}
						}
					}
					Game.LogTrivial("YOBBINCALLOUTS: Callout Finished, Ending...");
					EndCalloutHandler.EndCallout();
					this.End();
				}
				catch (Exception e)
				{
					bool calloutRunning = this.CalloutRunning;
					if (calloutRunning)
					{
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
						Game.LogTrivial("IN: " + ((this != null) ? this.ToString() : null));
						string error = e.ToString();
						Game.LogTrivial("ERROR: " + error);
						Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Check Your ~g~Log File.~w~ Sorry for the Inconvenience!");
						Game.DisplayNotification("Error: ~r~" + error);
						Game.LogTrivial("If You Believe this is a Bug, Please Report it on my Discord Server. Thanks!");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
					}
					else
					{
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - CALLOUT NO LONGER RUNNING==========");
						string error2 = e.ToString();
						Game.LogTrivial("ERROR: " + error2);
						Game.LogTrivial("No Need to Report This Error if it Did not Result in an LSPDFR Crash.");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - CALLOUT NO LONGER RUNNING==========");
					}
					this.End();
				}
			});
		}

		
		public override void End()
		{
			base.End();
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplayNotification("~g~Code 4~w~, return to patrol.");
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS WE_ARE_CODE_4");
			}
			this.CalloutRunning = false;
			bool flag = EntityExtensions.Exists(this.Victim);
			if (flag)
			{
				this.Victim.Tasks.ClearImmediately();
			}
			bool flag2 = EntityExtensions.Exists(this.Victim);
			if (flag2)
			{
				this.Victim.Dismiss();
			}
			bool flag3 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag3)
			{
				this.SuspectBlip.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.Suspect2Blip);
			if (flag4)
			{
				this.Suspect2Blip.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this.VictimBlip);
			if (flag5)
			{
				this.VictimBlip.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.AreaBlip);
			if (flag6)
			{
				this.AreaBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Bar Fight Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private Vector3 MainSpawnPoint;

		
		private ArrayList list = new ArrayList
		{
			new Vector3(253.8926f, -1009.604f, 29.27279f),
			new Vector3(500.8603f, -1536.218f, 29.27567f),
			new Vector3(224.254f, 314.3193f, 105.5649f),
			new Vector3(966.7332f, -119.8229f, 74.35316f),
			new Vector3(-259.9449f, 6290.934f, 31.47674f),
			new Vector3(1991.103f, 3047.539f, 47.21512f),
			new Vector3(-561.0571f, 273.3992f, 83.10964f),
			new Vector3(142.3387f, -1299.464f, 29.17999f),
			new Vector3(-1650.104f, -1001.059f, 13.0174f)
		};

		
		private Ped Suspect;

		
		private Ped Suspect2;

		
		private Ped Victim;

		
		private string[] Peds;

		
		private Blip SuspectBlip;

		
		private Blip Suspect2Blip;

		
		private Blip VictimBlip;

		
		private Blip AreaBlip;

		
		private int MainScenario;

		
		private string Zone;

		
		private bool CalloutRunning;

		
		private LHandle SuspectPursuit;

		
		private Ped player = Game.LocalPlayer.Character;
	}
}
