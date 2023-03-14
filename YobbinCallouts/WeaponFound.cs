using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Weapon Found", 2)]
	internal class WeaponFound : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Weapon Found Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 2);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario value is: " + this.MainScenario.ToString());
			string zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).RealAreaName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + zone);
			this.MainSpawnPoint = World.GetNextPositionOnStreet(this.player.Position.Around(550f));
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 25f);
			base.AddMinimumDistanceCheck(60f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("CITIZENS_REPORT A_01 YC_DEADLYWEAPON");
			base.CalloutMessage = "Weapon Found";
			base.CalloutPosition = this.MainSpawnPoint;
			bool flag = this.MainScenario >= 0;
			if (flag)
			{
				base.CalloutAdvisory = "A Caller Has Reportedly Discovered a ~r~Firearm~w~.";
			}
			else
			{
				base.CalloutAdvisory = "A Caller Has Reportedly Discovered a ~r~Melee Weapon~w~.";
			}
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			try
			{
				Game.LogTrivial("YOBBINCALLOUTS: Weapon Found Callout Accepted by User");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
				}
				else
				{
					Game.DisplayNotification("Respond ~b~Code 2");
				}
				if (WeaponFound.<>o__30.<>p__0 == null)
				{
					WeaponFound.<>o__30.<>p__0 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(WeaponFound), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				WeaponFound.<>o__30.<>p__0.Target(WeaponFound.<>o__30.<>p__0, NativeFunction.Natives, World.GetNextPositionOnStreet(this.MainSpawnPoint), 0, ref outPosition);
				this.Witness = new Ped(outPosition, 69f);
				this.Witness.IsPersistent = true;
				this.Witness.BlockPermanentEvents = true;
				this.WitnessBlip = this.Witness.AttachBlip();
				this.WitnessBlip.IsFriendly = true;
				this.WitnessBlip.IsRouteEnabled = true;
				this.WitnessBlip.Name = "Witness";
				Vector3 dir = this.player.Position - this.Witness.Position;
				this.Witness.Tasks.AchieveHeading(MathHelper.ConvertDirectionToHeading(dir)).WaitForCompletion(1100);
				this.Witness.Tasks.PlayAnimation("friends@frj@ig_1", "wave_a", 1.1f, 1);
				bool flag = this.MainScenario >= 1;
				if (flag)
				{
					this.Suspect = new Ped(this.Witness.Position.Around(250f), 69f);
					this.Suspect.IsPersistent = true;
					this.Suspect.BlockPermanentEvents = true;
					this.Suspect.Tasks.Wander();
					this.Suspect.RelationshipGroup.SetRelationshipWith(RelationshipGroup.Cop, 5);
					this.Suspect.RelationshipGroup.SetRelationshipWith(RelationshipGroup.Player, 5);
					this.Suspect.RelationshipGroup.SetRelationshipWith(RelationshipGroup.AmbientFriendEmpty, 5);
				}
				bool flag2 = this.MainScenario >= 0;
				if (flag2)
				{
					Random r = new Random();
					int WeaponType = r.Next(0, 4);
					bool flag3 = WeaponType == 0;
					if (flag3)
					{
						this.Weapon = new Object("w_pi_appistol", World.GetNextPositionOnStreet(this.Witness.Position));
						this.WeaponName = "AP Pistol";
					}
					else
					{
						bool flag4 = WeaponType == 1;
						if (flag4)
						{
							this.Weapon = new Object("w_pi_combatpistol", World.GetNextPositionOnStreet(this.Witness.Position));
							this.WeaponName = "Combat Pistol";
						}
						else
						{
							bool flag5 = WeaponType == 2;
							if (flag5)
							{
								this.Weapon = new Object("w_pi_heavypistol", World.GetNextPositionOnStreet(this.Witness.Position));
								this.WeaponName = "Heavy Pistol";
							}
							else
							{
								bool flag6 = WeaponType == 3;
								if (flag6)
								{
									this.Weapon = new Object("w_pi_pistol", World.GetNextPositionOnStreet(this.Witness.Position));
									this.WeaponName = "Pistol";
								}
							}
						}
					}
				}
				Game.LogTrivial("YOBBINCALLOUTS: Weapon is " + this.WeaponName);
				this.Weapon.IsPersistent = true;
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
			bool flag7 = !this.CalloutRunning;
			if (flag7)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Weapon Found Callout Not Accepted by User.");
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
						while (this.player.DistanceTo(this.Witness) >= 20f && !Game.IsKeyDown(Config.CalloutEndKey))
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
								CalloutInterfaceHandler.SendMessage(this, "Unit on Scene.");
							}
							this.WitnessFirst();
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
			bool flag2 = EntityExtensions.Exists(this.WitnessBlip);
			if (flag2)
			{
				this.WitnessBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.WeaponBlip);
			if (flag3)
			{
				this.WeaponBlip.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.Suspect);
			if (flag4)
			{
				this.Suspect.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.Witness);
			if (flag5)
			{
				this.Witness.Dismiss();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Weapon Found Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void WitnessFirst()
		{
			bool displayHelp = Config.DisplayHelp;
			if (displayHelp)
			{
				Game.DisplayHelp("Speak with the ~b~Witness.");
			}
			this.WitnessBlip.IsRouteEnabled = false;
			CallHandler.IdleAction(this.Witness, false);
			while (this.player.DistanceTo(this.Witness) >= 6f)
			{
				GameFiber.Wait(0);
			}
			this.Witness.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
			bool displayHelp2 = Config.DisplayHelp;
			if (displayHelp2)
			{
				Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~b~Witness.");
			}
			Random r = new Random();
			int Dialogue = r.Next(0, 3);
			bool flag = this.MainScenario == 0;
			if (flag)
			{
				bool flag2 = Dialogue == 0;
				if (flag2)
				{
					CallHandler.Dialogue(this.WitnessOpeningSuspectNotFound1, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				else
				{
					bool flag3 = Dialogue == 1;
					if (flag3)
					{
						CallHandler.Dialogue(this.WitnessOpeningSuspectNotFound2, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					else
					{
						CallHandler.Dialogue(this.WitnessOpeningSuspectNotFound3, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
				}
				bool flag4 = EntityExtensions.Exists(this.WitnessBlip);
				if (flag4)
				{
					this.WitnessBlip.Delete();
				}
				this.Witness.Tasks.ClearImmediately();
				bool flag5 = EntityExtensions.Exists(this.Witness);
				if (flag5)
				{
					this.Witness.Dismiss();
				}
				this.CollectWeapon();
			}
			else
			{
				Random r2 = new Random();
				int WeaponModel = r2.Next(0, 5);
				Game.LogTrivial("YOBBINCALLOUTS: Suspect Weapon Model is " + WeaponModel.ToString());
				bool flag6 = WeaponModel == 0;
				if (flag6)
				{
					this.Suspect.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE", -1, true);
					this.WeaponName = "Assault Rifle";
				}
				else
				{
					bool flag7 = WeaponModel == 1;
					if (flag7)
					{
						this.Suspect.Inventory.GiveNewWeapon("WEAPON_SMG", -1, true);
						this.WeaponName = "SMG";
					}
					else
					{
						bool flag8 = WeaponModel == 2;
						if (flag8)
						{
							this.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
							this.WeaponName = "Pistol";
						}
						else
						{
							bool flag9 = WeaponModel == 3;
							if (flag9)
							{
								this.Suspect.Inventory.GiveNewWeapon("WEAPON_MICROSMG", -1, true);
								this.WeaponName = "SMG";
							}
							else
							{
								bool flag10 = WeaponModel == 4;
								if (flag10)
								{
									this.Suspect.Inventory.GiveNewWeapon("WEAPON_COMPACTRIFLE", -1, true);
									this.WeaponName = "Rifle";
								}
							}
						}
					}
				}
				bool flag11 = this.MainScenario == 1;
				if (flag11)
				{
					bool flag12 = Dialogue == 0;
					if (flag12)
					{
						CallHandler.Dialogue(this.WitnessOpeningSuspectClose1, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					else
					{
						bool flag13 = Dialogue == 1;
						if (flag13)
						{
							CallHandler.Dialogue(this.WitnessOpeningSuspectClose2, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
						else
						{
							CallHandler.Dialogue(this.WitnessOpeningSuspectClose3, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
					}
					GameFiber.Wait(2000);
					this.Witness.Tasks.Clear();
					bool flag14 = EntityExtensions.Exists(this.WitnessBlip);
					if (flag14)
					{
						this.WitnessBlip.Delete();
					}
					this.Witness.Tasks.ClearImmediately();
					bool flag15 = EntityExtensions.Exists(this.Witness);
					if (flag15)
					{
						this.Witness.Dismiss();
					}
					this.SuspectVehicle = CallHandler.SpawnVehicle(World.GetNextPositionOnStreet(this.Suspect.Position), 69f, true);
					this.SuspectVehicle.IsPersistent = true;
					this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
					this.Suspect.Tasks.CruiseWithVehicle(15f, 516);
					bool calloutInterface = Main.CalloutInterface;
					if (calloutInterface)
					{
						CalloutInterfaceHandler.SendMessage(this, "Suspect is Driving a ~r~" + this.SuspectVehicle.Model.Name + "~w~ with Plate ~y~" + this.SuspectVehicle.LicensePlate);
					}
					Game.DisplayNotification("Suspect is Driving a ~r~" + this.SuspectVehicle.Model.Name + "~w~ with Plate ~y~" + this.SuspectVehicle.LicensePlate);
					this.SuspectBlip = this.Suspect.AttachBlip();
					this.SuspectBlip.IsFriendly = false;
					this.SuspectBlip.Scale = 0.69f;
					while (this.player.DistanceTo(this.Suspect) >= 20f)
					{
						GameFiber.Wait(0);
					}
					this.SuspectDecisions();
				}
				else
				{
					bool flag16 = Dialogue == 4;
					if (flag16)
					{
						CallHandler.Dialogue(this.WitnessOpeningSuspectClose4, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					else
					{
						CallHandler.Dialogue(this.WitnessOpeningSuspectClose5, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					this.Witness.Tasks.Clear();
					bool flag17 = EntityExtensions.Exists(this.WitnessBlip);
					if (flag17)
					{
						this.WitnessBlip.Delete();
					}
					this.Witness.Tasks.ClearImmediately();
					bool flag18 = EntityExtensions.Exists(this.Witness);
					if (flag18)
					{
						this.Witness.Dismiss();
					}
					this.Suspect.IsVisible = true;
					this.Suspect.Tasks.Wander();
					this.SuspectBlip = this.Suspect.AttachBlip();
					this.SuspectBlip.IsFriendly = false;
					this.SuspectBlip.Scale = 0.69f;
					while (this.player.DistanceTo(this.Suspect) >= 15f)
					{
						GameFiber.Wait(0);
					}
					Random monke = new Random();
					int decision = monke.Next(0, 2);
					bool flag19 = decision == 1;
					if (flag19)
					{
						CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
						{
							this.Suspect
						});
					}
					else
					{
						Random yuy = new Random();
						int WaitTime = yuy.Next(1500, 6000);
						GameFiber.Wait(WaitTime);
						bool flag20 = EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive;
						if (flag20)
						{
							this.Suspect.Tasks.FightAgainst(this.player, -1);
						}
						while (EntityExtensions.Exists(this.Suspect))
						{
							GameFiber.Yield();
							bool flag21 = !EntityExtensions.Exists(this.Suspect) || this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect);
							if (flag21)
							{
								break;
							}
						}
						bool flag22 = EntityExtensions.Exists(this.Suspect);
						if (flag22)
						{
							bool flag23 = Functions.IsPedArrested(this.Suspect);
							if (flag23)
							{
								GameFiber.Wait(1000);
								Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~.");
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
							Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest.");
						}
					}
				}
			}
		}

		
		private void CollectWeapon()
		{
			bool flag = EntityExtensions.Exists(this.Witness);
			if (flag)
			{
				this.Witness.Dismiss();
			}
			Game.DisplayHelp("Locate the ~p~Weapon.~w~ Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Collect it.");
			this.WeaponBlip = this.Weapon.AttachBlip();
			this.WeaponBlip.Scale = 0.35f;
			this.WeaponBlip.Color = Color.Purple;
			while (this.player.DistanceTo2D(this.Weapon) >= 0.65f)
			{
				GameFiber.Wait(0);
			}
			while (Game.IsKeyDown(Config.MainInteractionKey))
			{
				GameFiber.Wait(0);
			}
			this.Weapon.AttachTo(this.player, this.player.GetBoneIndex(57005), new Vector3(0.1f, 0.03f, 0f), new Rotator(-71f, 0f, 0f));
			bool flag2 = EntityExtensions.Exists(this.WeaponBlip);
			if (flag2)
			{
				this.WeaponBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.player.LastVehicle);
			if (flag3)
			{
				Vehicle lastvehicle = this.player.LastVehicle;
				Game.DisplayHelp("Return to Your ~g~Vehicle~w~ to Run the Serial Number of the ~p~Weapon.");
				Blip lastvehicleblip = lastvehicle.AttachBlip();
				while (!this.player.IsInAnyPoliceVehicle)
				{
					GameFiber.Wait(0);
				}
				bool flag4 = EntityExtensions.Exists(lastvehicleblip);
				if (flag4)
				{
					lastvehicleblip.Delete();
				}
				bool flag5 = EntityExtensions.Exists(this.Weapon);
				if (flag5)
				{
					this.Weapon.Delete();
				}
			}
			GameFiber.Wait(2000);
			Random r3 = new Random();
			int WeaponSerial = r3.Next(50000000, 99999999);
			Game.LogTrivial("YOBBINCALLOUTS: Checking Weapon Serial.");
			Game.DisplaySubtitle("Dispatch, Requesting Weapon Serial Check for a ~r~" + this.WeaponName + " ~w~with Serial Number ~b~" + WeaponSerial.ToString());
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendMessage(this, "Dispatch, Requesting Weapon Serial Check for a ~r~" + this.WeaponName + " with Serial Number ~b~" + WeaponSerial.ToString());
			}
			GameFiber.Wait(3000);
			bool flag6 = WeaponSerial < 50000000;
			if (flag6)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Weapon Serial Check = Hit.");
				this.SuspectVehicle = CallHandler.SpawnVehicle(World.GetNextPositionOnStreet(this.player.Position.Around(420f)), 69f, true);
				this.SuspectVehicle.IsPersistent = true;
				this.Suspect = this.SuspectVehicle.CreateRandomDriver();
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				string SuspectName = Functions.GetPersonaForPed(this.Suspect).FullName;
				double Distance = Math.Round((double)this.Suspect.DistanceTo(this.player));
				Game.DisplayNotification(string.Concat(new string[]
				{
					this.WeaponName,
					" Serial ~r~",
					WeaponSerial.ToString(),
					" ~w~Registered to ~p~",
					SuspectName,
					"~w~. ~r~Suspect~w~ was Recently ~r~Located~o~ ",
					Distance.ToString(),
					" metres~w~ Away!"
				}));
				bool calloutInterface2 = Main.CalloutInterface;
				if (calloutInterface2)
				{
					CalloutInterfaceHandler.SendMessage(this, string.Concat(new string[]
					{
						this.WeaponName,
						" Serial ~r~",
						WeaponSerial.ToString(),
						" ~w~Registered to ~p~",
						SuspectName,
						"~w~. ~r~Suspect~w~ was Recently ~r~Located~o~ ",
						Distance.ToString(),
						" metres~w~ Away!"
					}));
				}
				Game.DisplayNotification("Suspect is Driving a ~r~" + this.SuspectVehicle.Model.Name + "~w~ with Plate ~y~" + this.SuspectVehicle.LicensePlate);
				bool calloutInterface3 = Main.CalloutInterface;
				if (calloutInterface3)
				{
					CalloutInterfaceHandler.SendMessage(this, "Suspect is Driving a ~r~" + this.SuspectVehicle.Model.Name + "~w~ with Plate ~y~" + this.SuspectVehicle.LicensePlate);
				}
				this.Search();
			}
			else
			{
				Game.LogTrivial("YOBBINCALLOUTS: Weapon Serial Check = NO Hit.");
				CallHandler.locationChooser(CallHandler.HouseList, 600f, 25f);
				bool locationReturned = CallHandler.locationReturned;
				if (locationReturned)
				{
					this.House = CallHandler.SpawnPoint;
					Game.LogTrivial("YOBBINCALLOUTS: House Found.");
					this.Suspect = new Ped(this.House);
					this.Suspect.IsPersistent = true;
					this.Suspect.BlockPermanentEvents = true;
					string SuspectName2 = Functions.GetPersonaForPed(this.Suspect).FullName;
					double Distance2 = Math.Round((double)this.Suspect.DistanceTo(this.player));
					Game.DisplayNotification(string.Concat(new string[]
					{
						this.WeaponName,
						" Serial ~r~",
						WeaponSerial.ToString(),
						" ~w~Registered to ~p~",
						SuspectName2,
						"~w~. Owner~w~ Lives in ~b~",
						Functions.GetZoneAtPosition(this.House).RealAreaName,
						"~o~ ",
						Distance2.ToString(),
						" metres~w~ Away!"
					}));
					bool calloutInterface4 = Main.CalloutInterface;
					if (calloutInterface4)
					{
						CalloutInterfaceHandler.SendMessage(this, string.Concat(new string[]
						{
							this.WeaponName,
							" Serial ~r~",
							WeaponSerial.ToString(),
							" ~w~Registered to ~p~",
							SuspectName2,
							"~w~. Owner~w~ Lives in ~b~",
							Functions.GetZoneAtPosition(this.House).RealAreaName,
							"~o~ ",
							Distance2.ToString(),
							" metres~w~ Away!"
						}));
					}
					this.Suspect.Position = this.House;
					this.Suspect.IsVisible = false;
					GameFiber.Wait(1500);
					Game.DisplayHelp("Drive to the ~o~House~w~ of the ~r~Gun Owner~w~ in ~y~" + Functions.GetZoneAtPosition(this.House).RealAreaName);
					this.WitnessBlip = new Blip(this.House, 20f);
					this.WitnessBlip.IsRouteEnabled = true;
					this.WitnessBlip.Color = Color.Orange;
					this.WitnessBlip.Alpha = 0.69f;
					while (this.player.DistanceTo(this.House) >= 20f)
					{
						GameFiber.Wait(0);
					}
					bool flag7 = EntityExtensions.Exists(this.WitnessBlip);
					if (flag7)
					{
						this.WitnessBlip.Delete();
					}
					this.WitnessBlip = new Blip(this.House);
					this.WitnessBlip.Color = Color.Orange;
					this.WitnessBlip.Alpha = 1f;
					while (this.player.DistanceTo(this.House) >= 3.5f)
					{
						GameFiber.Wait(0);
					}
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to ~b~Ring~w~ the Doorbell.");
					while (!Game.IsKeyDown(Config.MainInteractionKey))
					{
						GameFiber.Wait(0);
					}
					CallHandler.Doorbell();
					GameFiber.Wait(2000);
					Game.LocalPlayer.HasControl = false;
					Game.FadeScreenOut(1500, true);
					this.Suspect = new Ped(this.House, this.player.Heading - 180f);
					this.Suspect.IsPersistent = true;
					CallHandler.IdleAction(this.Suspect, false);
					bool flag8 = EntityExtensions.Exists(this.WitnessBlip);
					if (flag8)
					{
						this.WitnessBlip.Delete();
					}
					this.SuspectBlip = this.Suspect.AttachBlip();
					this.SuspectBlip.IsFriendly = false;
					this.SuspectBlip.Scale = 0.69f;
					GameFiber.Wait(1500);
					Game.FadeScreenIn(1500, true);
					Game.LocalPlayer.HasControl = true;
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to speak with the ~o~Resident.");
					Game.LogTrivial("YOBBINCALLOUTS: Started speaking with suspect.");
					Random dud = new Random();
					int decision = dud.Next(0, 4);
					Game.LogTrivial("YOBBINCALLOUTS: Suspect Action is " + decision.ToString());
					bool flag9 = decision == 0;
					if (flag9)
					{
						Game.LogTrivial("YOBBINCALLOUTS: suspect cooperative");
						Random yud = new Random();
						int dialogue = yud.Next(0, 3);
						bool flag10 = dialogue == 0;
						if (flag10)
						{
							CallHandler.Dialogue(this.SuspectInnocent1, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
						else
						{
							bool flag11 = dialogue == 1;
							if (flag11)
							{
								CallHandler.Dialogue(this.SuspectInnocent2, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
							else
							{
								CallHandler.Dialogue(this.SuspectInnocent3, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
						}
						GameFiber.Wait(2000);
						CallHandler.IdleAction(this.Suspect, false);
						Game.DisplayHelp("Deal with the ~o~Suspect~w~ as you see fit. Press ~y~" + Config.CalloutEndKey.ToString() + " ~w~when ~b~Done.~w~");
						while (!Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
					}
					else
					{
						bool flag12 = decision == 1;
						if (flag12)
						{
							Game.LogTrivial("YOBBINCALLOUTS: suspect not cooperative");
							Random yud2 = new Random();
							int dialogue2 = yud2.Next(0, 2);
							bool flag13 = dialogue2 == 0;
							if (flag13)
							{
								CallHandler.Dialogue(this.SuspectGuilty1, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
							else
							{
								CallHandler.Dialogue(this.SuspectGuilty1, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
							GameFiber.Wait(2000);
							CallHandler.IdleAction(this.Suspect, false);
							Game.DisplayHelp("Deal with the ~o~Suspect~w~ as you see fit. Press ~y~" + Config.CalloutEndKey.ToString() + " ~w~when ~b~Done.~w~");
							while (!Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
						}
						else
						{
							bool flag14 = decision == 2;
							if (flag14)
							{
								Random r4 = new Random();
								int WeaponModel = r4.Next(0, 5);
								Random yud3 = new Random();
								int dialogue3 = yud3.Next(0, 2);
								bool flag15 = dialogue3 == 0;
								if (flag15)
								{
									CallHandler.Dialogue(this.SuspectGuilty1, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								}
								else
								{
									CallHandler.Dialogue(this.SuspectGuilty1, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								}
								CallHandler.IdleAction(this.Suspect, false);
								Game.DisplayHelp("Return to your ~p~Vehicle~w~ to conduct a ~o~Ped Check.");
								Random rondom = new Random();
								int WaitTime = rondom.Next(3000, 8000);
								Game.LogTrivial("YOBBINCALLOUTS: Waiting " + WaitTime.ToString() + " ms.");
								GameFiber.Wait(WaitTime);
								bool flag16 = EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive && !Functions.IsPedArrested(this.Suspect);
								if (flag16)
								{
									Random dec = new Random();
									int action = dec.Next(0, 2);
									bool flag17 = action == 0;
									if (flag17)
									{
										CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
										{
											this.Suspect
										});
									}
									else
									{
										Game.LogTrivial("YOBBINCALLOUTS: Suspect Weapon Model is " + WeaponModel.ToString());
										bool flag18 = WeaponModel == 0;
										if (flag18)
										{
											this.Suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", -1, true);
										}
										else
										{
											bool flag19 = WeaponModel == 1;
											if (flag19)
											{
												this.Suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, true);
											}
											else
											{
												bool flag20 = WeaponModel == 2;
												if (flag20)
												{
													this.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
												}
												else
												{
													bool flag21 = WeaponModel == 3;
													if (flag21)
													{
														this.Suspect.Inventory.GiveNewWeapon("WEAPON_MICROSMG", -1, true);
													}
													else
													{
														bool flag22 = WeaponModel == 4;
														if (flag22)
														{
															this.Suspect.Inventory.GiveNewWeapon("WEAPON_CROWBAR", -1, true);
														}
													}
												}
											}
										}
										Game.LogTrivial("YOBBINCALLOUTS: Suspect fight.");
										this.Suspect.Tasks.FightAgainst(this.player, -1);
										Functions.RequestBackup(this.Suspect.Position, 1, 0);
										while (EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive)
										{
											GameFiber.Wait(0);
										}
										bool isDead = this.Suspect.IsDead;
										if (isDead)
										{
											GameFiber.Wait(1000);
											Game.DisplayNotification("Dispatch, Suspect Was ~r~Killed~w~ Trying to Assault an Officer.");
											this.SuspectBlip.Delete();
										}
										else
										{
											GameFiber.Wait(1000);
											Game.DisplayNotification("Dispatch, Suspect is Under ~g~Arrest~w~ For Trying to Assault an Officer.");
										}
										GameFiber.Wait(2000);
										Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
									}
								}
							}
							else
							{
								CallHandler.Dialogue(this.SuspectFlees, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								Random dec2 = new Random();
								int action2 = dec2.Next(0, 2);
								bool flag23 = action2 == 0;
								if (flag23)
								{
									CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
									{
										this.Suspect
									});
								}
								else
								{
									Random r5 = new Random();
									int WeaponModel2 = r5.Next(0, 5);
									Game.LogTrivial("YOBBINCALLOUTS: Suspect Weapon Model is " + WeaponModel2.ToString());
									bool flag24 = WeaponModel2 == 0;
									if (flag24)
									{
										this.Suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", -1, true);
									}
									else
									{
										bool flag25 = WeaponModel2 == 1;
										if (flag25)
										{
											this.Suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, true);
										}
										else
										{
											bool flag26 = WeaponModel2 == 2;
											if (flag26)
											{
												this.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
											}
											else
											{
												bool flag27 = WeaponModel2 == 3;
												if (flag27)
												{
													this.Suspect.Inventory.GiveNewWeapon("WEAPON_MICROSMG", -1, true);
												}
												else
												{
													bool flag28 = WeaponModel2 == 4;
													if (flag28)
													{
														this.Suspect.Inventory.GiveNewWeapon("WEAPON_CROWBAR", -1, true);
													}
												}
											}
										}
									}
									Game.LogTrivial("YOBBINCALLOUTS: Suspect fight.");
									this.Suspect.Tasks.FightAgainst(this.player, -1);
									Functions.PlayScannerAudio("CRIME_ASSAULT_PEACE_OFFICER_01");
									Functions.RequestBackup(this.Suspect.Position, 1, 0);
									while (EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive)
									{
										GameFiber.Wait(0);
									}
									bool isDead2 = this.Suspect.IsDead;
									if (isDead2)
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, Suspect Was ~r~Killed~w~ Trying to Assault an Officer.");
										this.SuspectBlip.Delete();
									}
									else
									{
										GameFiber.Wait(1000);
										Game.DisplayNotification("Dispatch, Suspect is Under ~g~Arrest~w~ For Trying to Assault an Officer.");
									}
									GameFiber.Wait(2000);
									Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
								}
							}
						}
					}
				}
				else
				{
					Game.LogTrivial("YOBBINCALLOUTS: House NOT Found.");
					Game.DisplayNotification(this.WeaponName + " Serial ~r~" + WeaponSerial.ToString() + " ~w~did ~r~Not~w~ Match Any ~b~Entries~w~ in the Database.");
					bool calloutInterface5 = Main.CalloutInterface;
					if (calloutInterface5)
					{
						CalloutInterfaceHandler.SendMessage(this, this.WeaponName + " Serial ~r~" + WeaponSerial.ToString() + " ~w~did ~r~Not~w~ Match Any ~b~Entries~w~ in the Database.");
					}
				}
			}
		}

		
		private void Search()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplayHelp("Start ~o~Searching~w~ for the ~r~Suspect.");
				this.SuspectBlip = new Blip(this.Suspect.Position.Around(15f), 150f);
				this.SuspectBlip.Color = Color.Orange;
				this.SuspectBlip.Alpha = 0.5f;
				this.SuspectBlip.IsRouteEnabled = true;
				Random r3 = new Random();
				int WeaponModel = r3.Next(0, 5);
				Game.LogTrivial("YOBBINCALLOUTS: Suspect Weapon Model is " + WeaponModel.ToString());
				bool flag = WeaponModel == 0;
				if (flag)
				{
					this.Suspect.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE", -1, true);
				}
				else
				{
					bool flag2 = WeaponModel == 1;
					if (flag2)
					{
						this.Suspect.Inventory.GiveNewWeapon("WEAPON_SMG", -1, true);
					}
					else
					{
						bool flag3 = WeaponModel == 2;
						if (flag3)
						{
							this.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
						}
						else
						{
							bool flag4 = WeaponModel == 3;
							if (flag4)
							{
								this.Suspect.Inventory.GiveNewWeapon("WEAPON_MICROSMG", -1, true);
							}
							else
							{
								bool flag5 = WeaponModel == 4;
								if (flag5)
								{
									this.Suspect.Inventory.GiveNewWeapon("WEAPON_COMPACTRIFLE", -1, true);
								}
							}
						}
					}
				}
				GameFiber.Wait(1500);
				while (this.player.DistanceTo(this.Suspect) >= 100f)
				{
					GameFiber.Wait(0);
				}
				this.SuspectVehicle.IsDriveable = true;
				this.SuspectVehicle.IsVisible = true;
				this.Suspect.Tasks.CruiseWithVehicle(this.SuspectVehicle, 17f, 4);
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01");
				GameFiber.Wait(1000);
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Caller spotted the suspect driving recklessly, updating map.");
				}
				else
				{
					Game.DisplayNotification("~b~Update:~w~ A Caller Has ~y~Spotted~w~ the ~r~Suspect~w~ Driving Recklessly. ~g~Updating Map.");
				}
				GameFiber.Wait(1000);
				bool flag6 = EntityExtensions.Exists(this.SuspectBlip);
				if (flag6)
				{
					this.SuspectBlip.Delete();
				}
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				GameFiber.Wait(1500);
				Game.DisplaySubtitle("Dispatch, We Have Located the ~r~Suspect!");
				GameFiber.Wait(1500);
				this.SuspectDecisions();
			}
		}

		
		private void SuspectDecisions()
		{
			Game.DisplayHelp("Perform a ~o~Traffic Stop~w~ on the ~r~Suspect.");
			while (!Functions.IsPlayerPerformingPullover() && this.Suspect.IsAlive)
			{
				GameFiber.Wait(0);
			}
			Random yuy = new Random();
			int WaitTime = yuy.Next(1500, 6000);
			Random r = new Random();
			int action = r.Next(0, 4);
			Game.LogTrivial("YOBBINCALLOUTS: SUSPECT FINAL ACTION IS..." + action.ToString());
			bool flag = action == 0;
			if (flag)
			{
				CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
				{
					this.Suspect
				});
			}
			else
			{
				bool flag2 = action == 1;
				if (flag2)
				{
					GameFiber.Wait(WaitTime);
					Functions.ForceEndCurrentPullover();
					bool isAlive = this.Suspect.IsAlive;
					if (isAlive)
					{
						this.Suspect.Tasks.ParkVehicle(this.SuspectVehicle, this.SuspectVehicle.Position, this.SuspectVehicle.Heading).WaitForCompletion(5000);
						this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256).WaitForCompletion();
						this.Suspect.Tasks.AchieveHeading(Game.LocalPlayer.Character.LastVehicle.Heading - 180f).WaitForCompletion(1500);
						this.Suspect.Tasks.AimWeaponAt(Game.LocalPlayer.Character.Position, 1500).WaitForCompletion();
						this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
						bool flag3 = Functions.IsPlayerPerformingPullover();
						if (flag3)
						{
							Functions.ForceEndCurrentPullover();
						}
						GameFiber.Wait(2000);
						Functions.PlayScannerAudio("CRIME_ASSAULT_PEACE_OFFICER_01");
						Functions.RequestBackup(this.Suspect.Position, 1, 0);
						while (EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive)
						{
							GameFiber.Wait(0);
						}
						bool isDead = this.Suspect.IsDead;
						if (isDead)
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, Suspect Was ~r~Killed~w~ Trying to Assault an Officer.");
							this.SuspectBlip.Delete();
						}
						else
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, Suspect is Under ~g~Arrest~w~ For Trying to Assault an Officer.");
						}
						GameFiber.Wait(2000);
						Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
					}
				}
				else
				{
					while (this.SuspectVehicle.Speed > 0f)
					{
						GameFiber.Wait(0);
					}
					Game.DisplayHelp("Approach the ~r~Suspect.");
					GameFiber.Wait(WaitTime);
					bool flag4 = EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive;
					if (flag4)
					{
						bool flag5 = action == 2;
						if (flag5)
						{
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256).WaitForCompletion();
							this.Suspect.Tasks.AchieveHeading(Game.LocalPlayer.Character.LastVehicle.Heading - 180f).WaitForCompletion(1500);
							this.Suspect.Tasks.AimWeaponAt(Game.LocalPlayer.Character.Position, 1500).WaitForCompletion();
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
							bool flag6 = Functions.IsPlayerPerformingPullover();
							if (flag6)
							{
								Functions.ForceEndCurrentPullover();
							}
							GameFiber.Wait(2000);
							Functions.PlayScannerAudio("CRIME_ASSAULT_PEACE_OFFICER_01");
							Functions.RequestBackup(this.Suspect.Position, 1, 0);
							while (EntityExtensions.Exists(this.Suspect))
							{
								GameFiber.Yield();
								bool flag7 = !EntityExtensions.Exists(this.Suspect) || this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect);
								if (flag7)
								{
									break;
								}
							}
							bool isDead2 = this.Suspect.IsDead;
							if (isDead2)
							{
								GameFiber.Wait(1000);
								Game.DisplayNotification("Dispatch, Suspect Was ~r~Killed~w~ Trying to Assault an Officer.");
								this.SuspectBlip.Delete();
							}
							bool isCuffed = this.Suspect.IsCuffed;
							if (isCuffed)
							{
								GameFiber.Wait(1000);
								Game.DisplayNotification("Dispatch, Suspect is Under ~g~Arrest~w~ For Trying to Assault an Officer.");
							}
							GameFiber.Wait(2000);
							Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
						}
						else
						{
							CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
							{
								this.Suspect
							});
						}
					}
				}
			}
		}

		
		public Vector3 MainSpawnPoint;

		
		private Vector3 House;

		
		private Ped Suspect;

		
		private Vehicle SuspectVehicle;

		
		private Blip SuspectBlip;

		
		private Blip WitnessBlip;

		
		private Blip WeaponBlip;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private Ped Witness;

		
		private LHandle MainPursuit;

		
		private Object Weapon;

		
		public string WeaponName;

		
		public static string OriginalZone = "not far from here.";

		
		private int MainScenario;

		
		private bool CalloutRunning;

		
		private readonly List<string> WitnessOpeningSuspectNotFound1 = new List<string>
		{
			"~g~You:~w~ Hello, are you the caller?",
			"~b~Witness:~w~ Yes I am, officer. I was just walking down the street and noticed this ~r~Firearm.",
			"~b~Witness:~w~ It looks like it has some blood on it to. I'm obviously really concerned so called you guys.",
			"~g~You:~w~ For sure. Did you see who dropped it or when?",
			"~b~Witness:~w~ Unfortunately I didn't, officer. I was literally just walking down here and saw the weapon on the side of the road.",
			"~g~You:~w~ Alright, I'll collect this weapon and get it over to evidence. Thanks for your help!",
			"~b~Witness:~w~ No worries, stay safe officer!"
		};

		
		private readonly List<string> WitnessOpeningSuspectNotFound2 = new List<string>
		{
			"~g~You:~w~ Hello, did you call us?",
			"~b~Witness:~w~ Yes I did, officer. I was just going for a stroll when I noticed this ~r~Gun.",
			"~b~Witness:~w~ It seems like there's some blood on it as well. I'm obviously really concerned so called 9-1-1.",
			"~g~You:~w~ Absolutely. Did you witness anyone drop it? Maybe someone threw it out of a vehicle?",
			"~b~Witness:~w~ Unfortunately I didn't see anything, officer. I was just walking down this street and saw the weapon on the side of the road.",
			"~g~You:~w~ Alright, I'll take this weapon to evidence. Thanks for your help!",
			"~b~Witness:~w~ No worries, take care officer!"
		};

		
		private readonly List<string> WitnessOpeningSuspectNotFound3 = new List<string>
		{
			"~b~Witness:~w~ Hey officer, over here!",
			"~g~You:~w~ Are you the caller?",
			"~b~Witness:~w~ Yes I am, officer. I was just walking to the store when I noticed this ~r~Pistol.",
			"~b~Witness:~w~ It seems like there's some blood on it as well. I was obviously really scared so called 9-1-1.",
			"~g~You:~w~ Well, I'm really glad you called us. Did you see anything? Maybe someone threw it out of a vehicle?",
			"~b~Witness:~w~ Unfortunately I didn't, officer. I was just walking here and stumbled upon the weapon.",
			"~g~You:~w~ Alright, I'll take this weapon to evidence. Thanks again for your help!",
			"~b~Witness:~w~ Of course, take care officer!"
		};

		
		private readonly List<string> WitnessOpeningSuspectClose1 = new List<string>
		{
			"~b~Witness:~w~ Hey officer, over here!",
			"~g~You:~w~ Are you the caller?",
			"~b~Witness:~w~ Yes I am, officer. I was just walking to the store when I saw this guy throw this gun out of his car window!",
			"~b~Witness:~w~ He Drove off down the street! He shouldn't be far!",
			"~g~You:~w~ Alright, I'll start looking for them, Thanks!"
		};

		
		private readonly List<string> WitnessOpeningSuspectClose2 = new List<string>
		{
			"~b~Witness:~w~ Officer, over here!",
			"~g~You:~w~ Did you call us? Something about finding a weapon?",
			"~b~Witness:~w~ Yes I did, officer. I was just walking down the street when I saw this guy chuck this gun out of his car window!",
			"~b~Witness:~w~ He Drove off down the street! He couldn't have gotten far!",
			"~g~You:~w~ Alright, I'll start looking for them, Thanks!"
		};

		
		private readonly List<string> WitnessOpeningSuspectClose3 = new List<string>
		{
			"~b~Witness:~w~ Hey Officer!",
			"~g~You:~w~ Are you the caller?",
			"~b~Witness:~w~ Yes I am, officer. I was just walking down the street to the store when I saw someone throw this gun out of his car window!",
			"~b~Witness:~w~ He took off down the street! He shouldn't be far from here!",
			"~g~You:~w~ Alright, I'll start looking for them, Thanks!"
		};

		
		private readonly List<string> WitnessOpeningSuspectClose4 = new List<string>
		{
			"~b~Witness:~w~ Hey officer, over here!",
			"~g~You:~w~ Are you the caller?",
			"~b~Witness:~w~ Yes I am, officer. I was just walking to the store when this guy came running past me and dropped this gun!",
			"~b~Witness:~w~ He went off down the street! He shouldn't be far!",
			"~g~You:~w~ Alright, I'll start looking for them, Thanks!"
		};

		
		private readonly List<string> WitnessOpeningSuspectClose5 = new List<string>
		{
			"~b~Witness:~w~ Over here Officer!",
			"~g~You:~w~ Are you the caller?",
			"~b~Witness:~w~ Yes I am, officer. I was going for a stroll when I saw someone run past me and drop this gun from his bag!",
			"~b~Witness:~w~ He ran off down the street! He shouldn't be far!",
			"~g~You:~w~ Alright, I'll start looking for them, Thanks!"
		};

		
		private List<string> SuspectInnocent1 = new List<string>
		{
			"~o~Suspect:~w~ Hi Officer, what seems to be the issue? Is everything alright?",
			"~g~You:~w~ Hello, I'm here because a weapon registered to you was recovered " + WeaponFound.OriginalZone + "~w~.",
			"~o~Suspect:~w~ Oh shit, I was worried about that. I can explain everything, Officer!",
			"~o~Suspect:~w~ I have my CCW and sometimes keep my handgun in my car. Last week, someone broke in and stole it!",
			"~o~Suspect:~w~ I reported it stolen, but I was advised it might take a bit to show up in the system. I'm very sorry for the confusion!",
			"~g~You:~w~ Alright, thanks for the cooperation. I'll look into this, hang tight for me here."
		};

		
		private List<string> SuspectInnocent2 = new List<string>
		{
			"~o~Suspect:~w~ Hi Officer, is everything alright?",
			"~g~You:~w~ Well not exactly, I'm here because a weapon registered to you was recovered " + WeaponFound.OriginalZone + "~w~.",
			"~o~Suspect:~w~ Oh shit, I was hoping something like that wouldn't happen. I promise I can explain everything!",
			"~o~Suspect:~w~ I have my CCW and store my weapon in the glove compartment of my vehicle.",
			"~o~Suspect:~w~ A few days ago, my car was broken into and my gun was stolen.",
			"~o~Suspect:~w~ I reported it stolen, but I was told it might take a bit to show up in the system. I'm very sorry for the confusion!",
			"~g~You:~w~ Alright, thanks for the explanation. I'll look into this, hang tight for me here."
		};

		
		private List<string> SuspectInnocent3 = new List<string>
		{
			"~o~Suspect:~w~ Hi Officer, what seems to be going on here? Is everything okay?",
			"~g~You:~w~ Well not exactly, a weapon registered to the homeowner of this house was recovered " + WeaponFound.OriginalZone + "~w~.",
			"~o~Suspect:~w~ Oh no, I was hoping this wouldn't end up happening. I promise I can explain everything!",
			"~o~Suspect:~w~ I have my Concealed Permit, and usually keep my handgun in my home.",
			"~o~Suspect:~w~ A couple days ago, my house was broken into and my gun was stolen.",
			"~o~Suspect:~w~ I reported it stolen, but the Officer told me it might take a while to show up in the system. I'm so sorry for the confusion!",
			"~g~You:~w~ Alright, thanks for the explanation. I'll take a look into this, hang tight for me here."
		};

		
		private List<string> SuspectGuilty1 = new List<string>
		{
			"~o~Suspect:~w~ Hi Officer, what seems to be the issue? Is everything alright?",
			"~g~You:~w~ Hello, I'm here because a weapon registered to you was recovered " + WeaponFound.OriginalZone + "~w~.",
			"~o~Suspect:~w~ Oh shit, uh - wow! I don't know anything about that Officer, I don't know why that would flag me.",
			"~g~You:~w~ So you don't know anything about a firearm that was discovered on the street registered to you?",
			"~o~Suspect:~w~ No idea Officer, I've never owned a gun. Now am I free to go? I haven't done anything wrong.",
			"~g~You:~w~ Okay, I'll look into this, hang tight for me here."
		};

		
		private List<string> SuspectGuilty2 = new List<string>
		{
			"~o~Suspect:~w~ Oh, Officer! What are you doing here? Is everything alright?",
			"~g~You:~w~ Hello, I'm here because a weapon registered to you was discovered over " + WeaponFound.OriginalZone + "~w~.",
			"~o~Suspect:~w~ Oh, uh - wow! I don't know anything about that Officer, I've never even shot a gun let alone owned one.",
			"~g~You:~w~ So you don't know anything about a firearm that was discovered on the street that happened to be registered to you?",
			"~o~Suspect:~w~ No idea Officer, aren't you supposed to know that? Now am I free to go?",
			"~g~You:~w~ Okay, I'll look into this, hang tight for me here. You're not free to go at the moment, no."
		};

		
		private List<string> SuspectFlees = new List<string>
		{
			"~o~Suspect:~w~ Oh, Officer! What are you doing here? Is everything alright?",
			"~g~You:~w~ Hello, I'm here because a weapon registered to you was discovered over " + WeaponFound.OriginalZone + "~w~."
		};
	}
}
