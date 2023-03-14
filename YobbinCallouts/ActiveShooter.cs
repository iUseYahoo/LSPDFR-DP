using System;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Active Shooter", 3)]
	internal class ActiveShooter : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Active Shooter Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(1, 2);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario value is: " + this.MainScenario.ToString());
			this.MainSpawnPoint = World.GetNextPositionOnStreet(this.player.Position.Around(600f));
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 25f);
			base.AddMinimumDistanceCheck(60f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("ATTENTION_ALL_SWAT_UNITS_01 WE_HAVE_01 CRIME_ASSAULT_WITH_A_DEADLY_WEAPON_01 UNITS_RESPOND_CODE_99_01");
			base.CalloutMessage = "Active Shooter";
			base.CalloutPosition = this.MainSpawnPoint;
			bool flag = this.MainScenario == 0;
			if (flag)
			{
				base.CalloutAdvisory = "Two Suspects are Reported ~r~Discharging a Firearm ~w~at Random from a Vehicle.";
			}
			else
			{
				base.CalloutAdvisory = "Suspect is Reported ~r~Discharging a Firearm ~w~at Random.";
			}
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			try
			{
				Game.LogTrivial("YOBBINCALLOUTS: Active Shooter Callout Accepted by User");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 99", "");
				}
				else
				{
					Game.DisplayNotification("Respond ~r~Code 99, ~w~Emergency.~w~");
				}
				bool flag = this.MainScenario == 0;
				if (flag)
				{
					string[] Vehicles = new string[]
					{
						"zion",
						"oracle",
						"tampa",
						"virgo",
						"serrano",
						"asea",
						"asterope",
						"ingot",
						"primo2",
						"premier",
						"regina",
						"stratum",
						"washington",
						"baller",
						"huntley",
						"mesa"
					};
					Random r = new Random();
					int VictimVeh = r.Next(0, Vehicles.Length);
					this.SuspectVehicle = new Vehicle(Vehicles[VictimVeh], this.MainSpawnPoint);
					this.SuspectVehicle.IsPersistent = true;
					this.SuspectBlip = this.SuspectVehicle.AttachBlip();
					this.SuspectBlip.IsFriendly = false;
					this.SuspectBlip.IsRouteEnabled = true;
					this.Suspect = this.SuspectVehicle.CreateRandomDriver();
					this.Suspect.IsPersistent = true;
					this.Suspect.BlockPermanentEvents = true;
					this.Suspect.RelationshipGroup = RelationshipGroup.Gang1;
					this.Suspect.RelationshipGroup.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, 5);
					this.Suspect.RelationshipGroup.SetRelationshipWith(RelationshipGroup.Cop, 5);
					Random r2 = new Random();
					int WeaponModel = r2.Next(0, 5);
					Game.LogTrivial("YOBBINCALLOUTS: Suspect 1 Weapon Model is " + WeaponModel.ToString());
					bool flag2 = WeaponModel == 0;
					if (flag2)
					{
						this.Suspect.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE", -1, true);
					}
					else
					{
						bool flag3 = WeaponModel == 1;
						if (flag3)
						{
							this.Suspect.Inventory.GiveNewWeapon("WEAPON_SMG", -1, true);
						}
						else
						{
							bool flag4 = WeaponModel == 2;
							if (flag4)
							{
								this.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
							}
							else
							{
								bool flag5 = WeaponModel == 3;
								if (flag5)
								{
									this.Suspect.Inventory.GiveNewWeapon("WEAPON_MICROSMG", -1, true);
								}
								else
								{
									bool flag6 = WeaponModel == 4;
									if (flag6)
									{
										this.Suspect.Inventory.GiveNewWeapon("WEAPON_COMPACTRIFLE", -1, true);
									}
								}
							}
						}
					}
					this.Suspect2 = new Ped(this.SuspectVehicle.Position);
					this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, -2);
					this.Suspect2.IsPersistent = true;
					this.Suspect2.BlockPermanentEvents = true;
					this.Suspect2.RelationshipGroup = RelationshipGroup.Gang1;
					this.Suspect2.RelationshipGroup.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, 5);
					this.Suspect2.RelationshipGroup.SetRelationshipWith(RelationshipGroup.Cop, 5);
					Random r3 = new Random();
					int WeaponModel2 = r3.Next(0, 5);
					Game.LogTrivial("YOBBINCALLOUTS: Suspect 2 Weapon Model is " + WeaponModel.ToString());
					bool flag7 = WeaponModel == 0;
					if (flag7)
					{
						this.Suspect2.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, true);
					}
					else
					{
						bool flag8 = WeaponModel == 1;
						if (flag8)
						{
							this.Suspect2.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", -1, true);
						}
						else
						{
							bool flag9 = WeaponModel == 2;
							if (flag9)
							{
								this.Suspect2.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
							}
							else
							{
								bool flag10 = WeaponModel == 3;
								if (flag10)
								{
									this.Suspect2.Inventory.GiveNewWeapon("WEAPON_PISTOL50", -1, true);
								}
								else
								{
									bool flag11 = WeaponModel == 4;
									if (flag11)
									{
										this.Suspect2.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
									}
								}
							}
						}
					}
					Ped[] Randos = World.GetAllPeds();
					for (int i = 0; i < 25; i++)
					{
						GameFiber.Yield();
						bool flag12 = EntityExtensions.Exists(Randos[i]);
						if (flag12)
						{
							bool flag13 = Randos[i] != this.player && Randos[i] != this.Suspect;
							if (flag13)
							{
								this.Suspect.RelationshipGroup.SetRelationshipWith(Randos[i].RelationshipGroup, 5);
							}
							bool flag14 = Randos[i] != this.player && Randos[i] != this.Suspect2;
							if (flag14)
							{
								this.Suspect2.RelationshipGroup.SetRelationshipWith(Randos[i].RelationshipGroup, 5);
							}
						}
					}
				}
				else
				{
					this.Suspect = new Ped(this.MainSpawnPoint)
					{
						Health = 150,
						Armor = 200,
						BlockPermanentEvents = true,
						IsPersistent = true
					};
					Random r4 = new Random();
					int WeaponModel3 = r4.Next(0, 5);
					Game.LogTrivial("YOBBINCALLOUTS: Weapon Model is " + WeaponModel3.ToString());
					bool flag15 = WeaponModel3 == 0;
					if (flag15)
					{
						this.Suspect.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE", -1, true);
					}
					else
					{
						bool flag16 = WeaponModel3 == 1;
						if (flag16)
						{
							this.Suspect.Inventory.GiveNewWeapon("WEAPON_SMG", -1, true);
						}
						else
						{
							bool flag17 = WeaponModel3 == 2;
							if (flag17)
							{
								this.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
							}
							else
							{
								bool flag18 = WeaponModel3 == 3;
								if (flag18)
								{
									this.Suspect.Inventory.GiveNewWeapon("weapon_sawnoffshotgun", -1, true);
								}
								else
								{
									bool flag19 = WeaponModel3 == 4;
									if (flag19)
									{
										this.Suspect.Inventory.GiveNewWeapon("weapon_compactrifle", -1, true);
									}
								}
							}
						}
					}
					this.SuspectBlip = this.Suspect.AttachBlip();
					this.SuspectBlip.IsRouteEnabled = true;
					this.SuspectBlip.IsFriendly = false;
					this.Suspect.RelationshipGroup = RelationshipGroup.Gang1;
					this.Suspect.RelationshipGroup.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, 5);
					this.Suspect.RelationshipGroup.SetRelationshipWith(RelationshipGroup.Cop, 5);
					Ped[] Randos2 = World.GetAllPeds();
					for (int j = 0; j < 25; j++)
					{
						try
						{
							bool flag20 = EntityExtensions.Exists(Randos2[j]);
							if (flag20)
							{
								bool flag21 = Randos2[j] != this.player && Randos2[j] != this.Suspect;
								if (flag21)
								{
									this.Suspect.RelationshipGroup.SetRelationshipWith(Randos2[j].RelationshipGroup, 5);
								}
							}
						}
						catch (IndexOutOfRangeException)
						{
							Game.LogTrivial("YOBBINCALLOUTS: Index out of Bounds Exception caught.");
							break;
						}
						GameFiber.Yield();
					}
				}
			}
			catch (Exception e)
			{
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT ON CALLOUT INITIALIZATION==========");
				Game.LogTrivial("IN: " + ((this != null) ? this.ToString() : null));
				string error = e.ToString();
				Game.LogTrivial("ERROR: " + error);
				Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Check Your ~g~Log File.~w~ Sorry for the Inconvenience!");
				Game.DisplayNotification("Error: ~r~" + error);
				Game.LogTrivial("If You Believe this is a Bug, Please Report it on my Discord Server. Thanks!");
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT ON CALLOUT INITIALIZATION==========");
			}
			bool flag22 = !this.CalloutRunning;
			if (flag22)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Active Shooter Callout Not Accepted by User.");
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
						Random rondom = new Random();
						int WaitTime = rondom.Next(100, 225);
						Game.LogTrivial("YOBBINCALLOUTS: Suspect will fire when player is " + WaitTime.ToString() + " metres away.");
						while (this.player.DistanceTo(this.Suspect) >= (float)WaitTime && !Game.IsKeyDown(Config.CalloutEndKey))
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
							bool calloutInterface = Main.CalloutInterface;
							if (calloutInterface)
							{
								CalloutInterfaceHandler.SendMessage(this, "ALL UNITS: Warning - Use Extreme Caution Suspect is Firing at Random");
							}
							bool flag2 = this.MainScenario == 0;
							if (flag2)
							{
								this.Suspect.Tasks.CruiseWithVehicle(12.5f, 516);
								this.Suspect2.Tasks.FightAgainstClosestHatedTarget((float)WaitTime, -1);
								Game.LogTrivial("YOBBINCALLOUTS: Suspect Pursuit Started");
								Functions.ForceEndCurrentPullover();
								this.MainPursuit = Functions.CreatePursuit();
								Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
								Game.DisplayNotification("Suspect is ~r~Evading!");
								Functions.RequestBackup(this.player.Position, 1, 0);
								Functions.SetPursuitIsActiveForPlayer(this.MainPursuit, true);
								Functions.AddPedToPursuit(this.MainPursuit, this.Suspect);
								Functions.AddPedToPursuit(this.MainPursuit, this.Suspect2);
								while (Functions.IsPursuitStillRunning(this.MainPursuit))
								{
									GameFiber.Wait(0);
								}
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
							}
							else
							{
								this.Suspect.Tasks.FightAgainstClosestHatedTarget((float)WaitTime, -1);
								this.Suspect.IsInvincible = false;
								this.SuspectBlip.IsRouteEnabled = false;
								while (this.Suspect.IsAlive && EntityExtensions.Exists(this.Suspect))
								{
									GameFiber.Wait(0);
								}
							}
						}
					}
					bool flag5 = this.MainScenario == 0;
					if (flag5)
					{
						bool calloutInterface2 = Main.CalloutInterface;
						if (calloutInterface2)
						{
							CalloutInterfaceHandler.SendMessage(this, "Code 4, Suspects Neutralized.");
						}
						else
						{
							Game.DisplayNotification("~g~Code 4, ~w~Suspects Neutralized.");
						}
					}
					else
					{
						bool calloutInterface3 = Main.CalloutInterface;
						if (calloutInterface3)
						{
							CalloutInterfaceHandler.SendMessage(this, "Code 4, Suspect Neutralized.");
						}
						else
						{
							Game.DisplayNotification("~g~Code 4, ~w~Suspect Neutralized.");
						}
					}
					GameFiber.Wait(2000);
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
			bool flag = EntityExtensions.Exists(this.SuspectBlip);
			if (flag)
			{
				this.SuspectBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Active Shooter Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private Vector3 MainSpawnPoint;

		
		private Ped Suspect;

		
		private Ped Suspect2;

		
		private Vehicle SuspectVehicle;

		
		private Blip SuspectBlip;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private LHandle MainPursuit;

		
		private int MainScenario;

		
		private bool CalloutRunning;
	}
}
