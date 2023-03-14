using System;
using System.Collections.Generic;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Property Checkup", 2)]
	public class PropertyCheck : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Property Checkup Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 8);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is Value is " + this.MainScenario.ToString());
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			CallHandler.locationChooser(CallHandler.HouseList, 600f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			bool result;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
				base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 75f);
				base.AddMinimumDistanceCheck(50f, this.MainSpawnPoint);
				Functions.PlayScannerAudio("CITIZENS_REPORT YC_POSSIBLE_TRESPASSING");
				base.CalloutMessage = "Property Checkup";
				base.CalloutPosition = this.MainSpawnPoint;
				base.CalloutAdvisory = "A Checkup on a Resident's ~b~Property~w~ is Requested.";
				result = base.OnBeforeCalloutDisplayed();
			}
			else
			{
				Game.LogTrivial("YOBBINCALLOUTS: Player is not near any house. Aborting Callout.");
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Property Checkup Callout Accepted by User.");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~b~Code 2.");
			}
			this.House = new Blip(this.MainSpawnPoint, 25f);
			this.House.IsRouteEnabled = true;
			this.House.Color = Color.Yellow;
			this.House.Alpha = 0.67f;
			this.House.Name = "Property";
			bool flag = this.MainScenario == 0 || this.MainScenario == 1;
			if (flag)
			{
				Random rondom = new Random();
				int Message = rondom.Next(0, 2);
				bool flag2 = Message == 0;
				if (flag2)
				{
					bool calloutInterface2 = Main.CalloutInterface;
					if (calloutInterface2)
					{
						CalloutInterfaceHandler.SendMessage(this, "Resident away on vacation requested a checkup on their property.");
					}
					else
					{
						Game.DisplayNotification("~b~Resident ~r~Away~w~ on Vacation and Requested a Checkup on their ~y~Property.");
					}
				}
				else
				{
					bool calloutInterface3 = Main.CalloutInterface;
					if (calloutInterface3)
					{
						CalloutInterfaceHandler.SendMessage(this, "Resident reported suspicious movement in their Neighbour's property.");
					}
					else
					{
						Game.DisplayNotification("~b~Resident ~w~Reported ~r~Suspicious Movement~w~ in their ~y~Neighbour's Property.");
					}
				}
			}
			else
			{
				bool flag3 = this.MainScenario == 2 || this.MainScenario == 3;
				if (flag3)
				{
					bool calloutInterface4 = Main.CalloutInterface;
					if (calloutInterface4)
					{
						CalloutInterfaceHandler.SendMessage(this, "Resident reported suspicious movement in their Neighbour's property.");
					}
					else
					{
						Game.DisplayNotification("~b~Resident ~w~Reported ~r~Suspicious Movement~w~ in their ~y~Neighbour's Property.");
					}
					Random rondom2 = new Random();
					int AnimalChooser = rondom2.Next(0, 4);
					bool flag4 = AnimalChooser == 0;
					if (flag4)
					{
						this.Animal = new Ped("a_c_coyote", this.MainSpawnPoint, 0f);
						this.AnimalType = "Wild Coyote";
					}
					bool flag5 = AnimalChooser == 1;
					if (flag5)
					{
						this.Animal = new Ped("a_c_rottweiler", this.MainSpawnPoint, 0f);
						this.AnimalType = "Rotweiller";
					}
					bool flag6 = AnimalChooser == 2;
					if (flag6)
					{
						this.Animal = new Ped("a_c_boar", this.MainSpawnPoint, 0f);
						this.AnimalType = "Wild Boar";
					}
					bool flag7 = AnimalChooser == 3;
					if (flag7)
					{
						this.Animal = new Ped("a_c_retriever", this.MainSpawnPoint, 0f);
						this.AnimalType = "Dog";
					}
					bool flag8 = !EntityExtensions.Exists(this.Animal);
					if (flag8)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Animal does not Exist. Ending Callout.");
						return false;
					}
					this.Animal.IsPersistent = true;
					this.Animal.BlockPermanentEvents = true;
				}
				else
				{
					bool calloutInterface5 = Main.CalloutInterface;
					if (calloutInterface5)
					{
						CalloutInterfaceHandler.SendMessage(this, "Resident reported suspicious movement in their Neighbour's property.");
					}
					else
					{
						Game.DisplayNotification("~b~Resident ~w~Reported ~r~Suspicious Movement~w~ in their ~y~Neighbour's Property.");
					}
					string[] Suspects = new string[]
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
					int SuspectModel = r2.Next(0, Suspects.Length);
					this.Suspect = new Ped(Suspects[SuspectModel], this.MainSpawnPoint, 69f);
					Game.LogTrivial("YOBBINCALLOUTS: Suspect Spawned.");
					this.Suspect.IsPersistent = true;
					this.Suspect.BlockPermanentEvents = true;
					this.Suspect.Inventory.GiveNewWeapon("WEAPON_CROWBAR", -1, true);
				}
			}
			bool displayHelp = Config.DisplayHelp;
			if (displayHelp)
			{
				Game.DisplayHelp("Go to the ~y~Property~w~ Shown on The Map to Investigate.");
			}
			bool flag9 = !this.CalloutRunning;
			if (flag9)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Property Checkup Callout Not Accepted by User.");
			base.OnCalloutNotAccepted();
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void Callout()
		{
			this.CalloutRunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.CalloutRunning)
					{
						while (this.player.Character.DistanceTo(this.MainSpawnPoint) >= 35f && !Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(Config.CalloutEndKey);
						if (flag)
						{
							break;
						}
						Game.DisplayHelp("Search the ~y~Property~w~ for ~r~Suspicious Activity.");
						bool flag2 = this.MainScenario <= 1;
						if (flag2)
						{
							GameFiber.Wait(20000);
							Game.DisplayHelp("If You ~g~Do Not See Anything~w~, Press End to ~b~Finish the Callout.");
							while (!Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
							Game.DisplayNotification("Dispatch, we are Code 4. We Have ~b~Secured~w~ the Property.");
							break;
						}
						bool flag3 = this.MainScenario == 2 || this.MainScenario == 3;
						if (flag3)
						{
							while (this.player.Character.DistanceTo(this.Animal) >= 6f)
							{
								GameFiber.Wait(0);
							}
							Game.DisplaySubtitle("Oh, Looks Like it Might Have Just Been an Animal.", 3000);
							this.Animal.Dismiss();
							GameFiber.Wait(5000);
							Game.DisplayHelp("When You are Done ~y~Investigating, ~w~Press End to ~b~Finish the Callout.");
							while (!Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
							Game.DisplayNotification("Dispatch, The Property is ~g~Clear.~w~ Turned out to be a ~b~" + this.AnimalType + " ~w~on the Property.");
							GameFiber.Wait(2500);
							Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_04");
							GameFiber.Wait(2500);
							Game.DisplayNotification("Dispatch, we are Code 4. We Have ~b~Secured~w~ the Property.");
							break;
						}
						bool flag4 = this.MainScenario == 4;
						if (flag4)
						{
							while (this.player.Character.DistanceTo(this.Suspect) >= 6f)
							{
								GameFiber.Wait(0);
							}
							this.SuspectBlip = this.Suspect.AttachBlip();
							this.SuspectBlip.IsFriendly = false;
							this.SuspectBlip.Scale = 0.75f;
							this.SuspectBlip.Name = "Suspect";
							this.House.Delete();
							Game.DisplaySubtitle("Hey Sir! Stay Where You Are!", 2000);
							GameFiber.Wait(1500);
							this.Suspect.Tasks.AchieveHeading(this.player.Character.Heading - 180f).WaitForCompletion(500);
							bool isAlive = this.Suspect.IsAlive;
							if (isAlive)
							{
								CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
								{
									this.Suspect
								});
							}
							Game.DisplayHelp("When You are Done ~y~Investigating, ~w~Press End to ~b~Finish the Callout.");
							while (!Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
							Game.DisplayNotification("Dispatch, we are Code 4. We Have ~b~Secured~w~ the Property.");
							break;
						}
						bool flag5 = this.MainScenario == 5;
						if (flag5)
						{
							while (this.player.Character.DistanceTo(this.Suspect) >= 7f)
							{
								GameFiber.Wait(0);
							}
							this.SuspectBlip = this.Suspect.AttachBlip();
							this.SuspectBlip.IsFriendly = false;
							this.SuspectBlip.Scale = 0.75f;
							this.SuspectBlip.Name = "Suspect";
							this.House.Delete();
							Game.DisplaySubtitle("~g~You:~w~ Hey Sir! Drop Your Weapon and Put Your Hands Up!", 2500);
							GameFiber.Wait(1500);
							this.Suspect.Tasks.AimWeaponAt(Game.LocalPlayer.Character, 1000);
							Game.LogTrivial("YOBBINCALLOUTS: Suspect Threatened Officer With Weapon.");
							Functions.PlayScannerAudio("CRIME_ASSAULT_PEACE_OFFICER_03");
							try
							{
								Functions.RequestBackup(this.Suspect.Position, 1, 0);
								Game.LogTrivial("YOBBINCALLOUTS: Backup Dispatched");
							}
							catch (Exception e)
							{
								string str = "YOBBINCALLOUTS: Error spawning Code 3 Backup. Most Likely User Error. ERROR - ";
								Exception ex = e;
								Game.LogTrivial(str + ((ex != null) ? ex.ToString() : null));
							}
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
							while (EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect) && this.Suspect.IsAlive)
							{
								GameFiber.Yield();
							}
							bool flag6 = EntityExtensions.Exists(this.Suspect);
							if (flag6)
							{
								bool flag7 = Functions.IsPedArrested(this.Suspect) || this.Suspect.IsAlive;
								if (flag7)
								{
									GameFiber.Wait(1000);
									Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Attempting to ~r~Assault an Officer.");
								}
								else
								{
									GameFiber.Wait(1000);
									Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ for ~r~Assaulting an Officer.");
								}
							}
							else
							{
								GameFiber.Wait(1000);
								Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Attempting to ~r~Assault an Officer.");
							}
							GameFiber.Wait(2000);
							Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
							GameFiber.Wait(4500);
							Game.DisplayHelp("When You are Done ~y~Investigating, ~w~Press End to ~b~Finish the Callout.");
							while (!Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
							Game.DisplayNotification("Dispatch, we are Code 4. We Have ~b~Secured~w~ the Property.");
							break;
						}
						bool flag8 = this.MainScenario >= 6;
						if (flag8)
						{
							this.Suspect.Tasks.PlayAnimation("missheist_agency3aig_13", "wait_loops_player0", -1f, 1);
							while (this.player.Character.DistanceTo(this.Suspect) >= 7f)
							{
								GameFiber.Wait(0);
							}
							this.SuspectBlip = this.Suspect.AttachBlip();
							this.SuspectBlip.IsFriendly = false;
							this.SuspectBlip.Scale = 0.75f;
							this.SuspectBlip.Name = "Suspect";
							this.House.Delete();
							Game.DisplaySubtitle("~g~You:~w~ Hey Sir! Drop Your Weapon and Put Your Hands Up!", 2000);
							GameFiber.Wait(1500);
							bool isAlive2 = this.Suspect.IsAlive;
							if (isAlive2)
							{
								this.Suspect.Tasks.PutHandsUp(-1, Game.LocalPlayer.Character);
								GameFiber.Wait(500);
								bool displayHelp = Config.DisplayHelp;
								if (displayHelp)
								{
									Game.DisplayHelp("Press " + Config.MainInteractionKey.ToString() + " to Advance the Conversation.");
								}
								Random r = new Random();
								int OpeningDialogue = r.Next(0, 2);
								int num = OpeningDialogue;
								int num2 = num;
								if (num2 != 0)
								{
									if (num2 == 1)
									{
										CallHandler.Dialogue(this.SuspectCooperative2, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
									}
								}
								else
								{
									CallHandler.Dialogue(this.SuspectCooperative1, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								}
								GameFiber.Wait(1000);
								Game.DisplayHelp("Arrest the ~r~Suspect.");
								bool flag9 = this.MainScenario == 6;
								if (flag9)
								{
									this.Suspect.Tasks.ClearImmediately();
									CallHandler.SuspectWait(this.Suspect);
									GameFiber.Wait(3000);
									Game.DisplayHelp("When You are Done ~y~Investigating, ~w~Press End to ~b~Finish the Callout.");
									while (!Game.IsKeyDown(Config.CalloutEndKey))
									{
										GameFiber.Wait(0);
									}
								}
								else
								{
									while (this.player.Character.DistanceTo(this.Suspect) >= 3f)
									{
										GameFiber.Wait(0);
									}
									bool flag10 = this.Suspect.IsDead && !this.Suspect.IsCuffed;
									if (flag10)
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, Suspect is ~r~Dead.");
										GameFiber.Wait(2000);
										Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
										GameFiber.Wait(4000);
										Game.DisplayHelp("When You are Done ~y~Investigating, ~w~Press End to ~b~Finish the Callout.");
										while (!Game.IsKeyDown(Config.CalloutEndKey))
										{
											GameFiber.Wait(0);
										}
									}
									else
									{
										bool flag11 = Functions.IsPedArrested(this.Suspect);
										if (flag11)
										{
											bool flag12 = EntityExtensions.Exists(this.Suspect);
											if (flag12)
											{
												bool flag13 = Functions.IsPedArrested(this.Suspect) || this.Suspect.IsAlive;
												if (flag13)
												{
													GameFiber.Wait(1000);
													Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest.");
												}
												else
												{
													GameFiber.Wait(1000);
													Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~.");
												}
											}
											else
											{
												GameFiber.Wait(1000);
												Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~.");
											}
										}
										else
										{
											this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
											CallHandler.SuspectWait(this.Suspect);
											GameFiber.Wait(3000);
											Game.DisplayHelp("When You are Done ~y~Investigating, ~w~Press End to ~b~Finish the Callout.");
											while (!Game.IsKeyDown(Config.CalloutEndKey))
											{
												GameFiber.Wait(0);
											}
											Game.DisplayNotification("Dispatch, we are Code 4. We Have ~b~Secured~w~ the Property.");
										}
									}
								}
							}
							else
							{
								GameFiber.Wait(1000);
								Game.DisplayNotification("Dispatch, Suspect is ~r~Dead.");
								GameFiber.Wait(2000);
								Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
								GameFiber.Wait(4000);
								Game.DisplayHelp("When You are Done ~y~Investigating, ~w~Press End to ~b~Finish the Callout.");
								while (!Game.IsKeyDown(Config.CalloutEndKey))
								{
									GameFiber.Wait(0);
								}
							}
							Game.DisplayNotification("Dispatch, we are Code 4. We Have ~b~Secured~w~ the Property.");
							break;
						}
					}
				}
				catch (Exception e2)
				{
					bool calloutRunning = this.CalloutRunning;
					if (calloutRunning)
					{
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
						Game.LogTrivial("IN: " + ((this != null) ? this.ToString() : null));
						string error = e2.ToString();
						Game.LogTrivial("ERROR: " + error);
						Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Check Your ~g~Log File.~w~ Sorry for the Inconvenience!");
						Game.DisplayNotification("Error: ~r~" + error);
						Game.LogTrivial("If You Believe this is a Bug, Please Report it on my Discord Server. Thanks!");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
						this.End();
					}
					else
					{
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - CALLOUT NO LONGER RUNNING==========");
						string error2 = e2.ToString();
						Game.LogTrivial("ERROR: " + error2);
						Game.LogTrivial("No Need to Report This Error if it Did not Result in an LSPDFR Crash.");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - CALLOUT NO LONGER RUNNING==========");
					}
				}
				EndCalloutHandler.EndCallout();
				this.End();
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
			bool flag = EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				this.Suspect.Dismiss();
			}
			bool flag2 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag2)
			{
				this.SuspectBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.House);
			if (flag3)
			{
				this.House.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Property Checkup Callout Finished Cleaning Up.");
		}

		
		private Vector3 MainSpawnPoint;

		
		private Blip House;

		
		private Blip SuspectBlip;

		
		private Ped Animal;

		
		private Ped Suspect;

		
		private int MainScenario;

		
		private bool Backup = false;

		
		private string Zone;

		
		private string AnimalType;

		
		private Player player = Game.LocalPlayer;

		
		private LHandle MainPursuit;

		
		private bool CalloutRunning = false;

		
		private readonly List<string> SuspectCooperative1 = new List<string>
		{
			"~r~Suspect:~w~ Okay officer, just please don't hurt me!",
			"~g~You:~w~ Gust stay where you are, keep your hands there. What are you doing here?",
			"~r~Suspect:~w~ Uh, nothing, why?",
			"~g~You:~w~ Is this your house? Why are you walking around with a crowbar?",
			"~r~Suspect:~w~ I don't have to answer any questions. Just please don't kill me!",
			"~g~You:~w~ You are under arrest, sir. Please stay where you are!"
		};

		
		private readonly List<string> SuspectCooperative2 = new List<string>
		{
			"~r~Suspect:~w~ Alright officer, just don't hurt me!",
			"~g~You:~w~ Just stay where you are, keep your hands there. What are you doing here?",
			"~r~Suspect:~w~ That's none of your business, just don't hurt me please!",
			"~g~You:~w~ Is this your house? Why are you walking around here with a crowbar?",
			"~r~Suspect:~w~ I don't need to answer any questions.",
			"~g~You:~w~ Alright, you are under arrest, sir. Please stay where you are!"
		};

		
		private int SuspectOpeningCount;
	}
}
