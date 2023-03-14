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
	
	[CalloutInfo("Hospital Emergency", 2)]
	internal class HospitalEmergency : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Hospital Emergency Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 0);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario value is: " + this.MainScenario.ToString());
			CallHandler.locationChooser(CallHandler.HospitalList, 1000f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			bool result;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
				base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 25f);
				base.AddMinimumDistanceCheck(60f, this.MainSpawnPoint);
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE YC_DISTURBANCE IN_A_01 YC_HOSPITAL");
				base.CalloutMessage = "Hospital Emergency";
				base.CalloutPosition = this.MainSpawnPoint;
				bool flag = this.MainScenario == 0;
				if (flag)
				{
					base.CalloutAdvisory = "An unstable patient has reportedly escaped from Hospital Custody.";
				}
				else
				{
					base.CalloutAdvisory = "An unstable patient has reportedly escaped from Hospital Custody.";
				}
				result = base.OnBeforeCalloutDisplayed();
			}
			else
			{
				Game.LogTrivial("No Hospital location found within range. Aborting Callout.");
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			try
			{
				Game.LogTrivial("YOBBINCALLOUTS: Hospital Emergency Callout Accepted by User");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 3", "");
				}
				else
				{
					Game.DisplayNotification("Respond ~r~Code 3");
				}
				bool flag = this.MainScenario == 0;
				if (flag)
				{
					this.Nurse = new Ped("s_f_y_scrubs_01", this.MainSpawnPoint, 69f);
					this.Nurse.IsPersistent = true;
					this.Nurse.BlockPermanentEvents = true;
					this.NurseBlip = this.Nurse.AttachBlip();
					this.NurseBlip.IsFriendly = true;
					this.NurseBlip.IsRouteEnabled = true;
					Vector3 dir = this.player.Position - this.Nurse.Position;
					this.Nurse.Tasks.AchieveHeading(MathHelper.ConvertDirectionToHeading(dir)).WaitForCompletion(1100);
					this.Nurse.Tasks.PlayAnimation("friends@frj@ig_1", "wave_a", 1.1f, 1);
					Vector3 GuardSpawnPoint = World.GetNextPositionOnStreet(this.Nurse.Position);
					if (HospitalEmergency.<>o__23.<>p__0 == null)
					{
						HospitalEmergency.<>o__23.<>p__0 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
						{
							typeof(bool)
						}, typeof(HospitalEmergency), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
						}));
					}
					Vector3 outPosition;
					HospitalEmergency.<>o__23.<>p__0.Target(HospitalEmergency.<>o__23.<>p__0, NativeFunction.Natives, GuardSpawnPoint, 0, ref outPosition);
					bool flag2 = CallHandler.FiftyFifty();
					if (flag2)
					{
						this.Guard = new Ped("s_m_m_security_01", outPosition, this.Nurse.Heading - 15f);
					}
					else
					{
						this.Guard = new Ped("s_m_m_security_01", outPosition, this.Nurse.Heading + 15f);
					}
					this.Guard.IsPersistent = true;
					this.Guard.BlockPermanentEvents = true;
					CallHandler.IdleAction(this.Guard, true);
					HospitalEmergency.Suspect = new Citizen(World.GetNextPositionOnStreet(this.MainSpawnPoint.Around(200f)));
					HospitalEmergency.Suspect.IsPersistent = true;
					HospitalEmergency.Suspect.BlockPermanentEvents = true;
					HospitalEmergency.Suspect.Tasks.Wander();
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
			bool flag3 = !this.CalloutRunning;
			if (flag3)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Hospital Emergency Callout Not Accepted by User.");
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
						bool displayHelp = Config.DisplayHelp;
						if (displayHelp)
						{
							Game.DisplayHelp("Drive to the ~b~Hospital.");
						}
						while (this.player.DistanceTo(this.MainSpawnPoint) >= 20f && !Game.IsKeyDown(Config.CalloutEndKey))
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
							bool flag2 = this.MainScenario == 0;
							if (flag2)
							{
								this.NurseOpening();
							}
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
			bool flag2 = EntityExtensions.Exists(this.NurseBlip);
			if (flag2)
			{
				this.NurseBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.Area);
			if (flag3)
			{
				this.Area.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.Hostage);
			if (flag4)
			{
				this.Hostage.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.HostageBlip);
			if (flag5)
			{
				this.HostageBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Hospital Emergency Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void NurseOpening()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Unit on Scene.");
				}
				this.NurseBlip.Scale = 0.69f;
				this.NurseBlip.IsRouteEnabled = false;
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Speak with the ~b~Nurse.");
				}
				while (this.player.DistanceTo(this.Nurse) >= 6f)
				{
					GameFiber.Wait(0);
				}
				this.Nurse.Tasks.AchieveHeading(this.player.Heading - 180f);
				bool displayHelp2 = Config.DisplayHelp;
				if (displayHelp2)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~b~Nurse.");
				}
				Random r = new Random();
				int Dialogue = r.Next(0, 0);
				bool flag = Dialogue == 0;
				if (flag)
				{
					CallHandler.Dialogue(this.NurseOpening1, this.Nurse, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				GameFiber.Wait(1000);
				Object document = new Object("prop_cd_paper_pile1", Vector3.Zero);
				document.IsPersistent = true;
				document.AttachTo(this.Nurse, this.Nurse.GetBoneIndex(57005), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
				this.Nurse.Tasks.PlayAnimation("mp_common", "givetake1_b", 1f, 1);
				GameFiber.Wait(1000);
				document.Delete();
				GameFiber.Wait(1000);
				HospitalEmergency.Suspect.setMedicalProblemsForMentallyIllSuspect();
				Game.DisplayNotification("commonmenu", "shop_health_icon_b", "~g~Patient Information", "~b~" + HospitalEmergency.Suspect.FullName + " | " + HospitalEmergency.Suspect.Gender, HospitalEmergency.Suspect.ToString());
				GameFiber.Wait(3000);
				CallHandler.IdleAction(this.Nurse, false);
				bool flag2 = EntityExtensions.Exists(this.NurseBlip);
				if (flag2)
				{
					this.NurseBlip.Delete();
				}
				this.SuspectSearch();
			}
		}

		
		private void SuspectSearch()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Start ~o~Searching~w~ for the Patient.");
				}
				this.Area = new Blip(HospitalEmergency.Suspect.Position.Around(5f), 100f);
				this.Area.Alpha = 0.69f;
				this.Area.Color = Color.Orange;
				this.Area.IsRouteEnabled = true;
				HospitalEmergency.Suspect.Tasks.Wander();
				Random chez = new Random();
				int WaitDistance = chez.Next(15, 25);
				while (this.player.DistanceTo(HospitalEmergency.Suspect) >= (float)WaitDistance)
				{
					GameFiber.Wait(0);
				}
				Random r2 = new Random();
				int SuspectScenario = r2.Next(0, 0);
				bool flag = SuspectScenario == 0;
				if (flag)
				{
					this.Area.Delete();
					this.SuspectBlip = HospitalEmergency.Suspect.AttachBlip();
					this.SuspectBlip.IsFriendly = false;
					this.SuspectBlip.Scale = 0.69f;
					bool isAlive = HospitalEmergency.Suspect.IsAlive;
					if (isAlive)
					{
						Ped[] Peds = HospitalEmergency.Suspect.GetNearbyPeds(10);
						for (int i = 0; i < Peds.Length; i++)
						{
							GameFiber.Yield();
							bool flag2 = EntityExtensions.Exists(Peds[i]) && !Peds[i].IsPlayer && !Peds[i].IsInAnyVehicle(false);
							if (flag2)
							{
								this.Hostage = Peds[i];
								break;
							}
						}
						bool flag3 = EntityExtensions.Exists(this.Hostage) && this.Hostage.DistanceTo(HospitalEmergency.Suspect) <= 20f;
						if (flag3)
						{
							Game.LogTrivial("YOBBINCALLOUTS: HOSTAGE SCENARIO STARTED");
							Game.LogTrivial("YOBBINCALLOUTS: Hostage Location = " + this.Hostage.Position.ToString());
							this.Hostage.IsPersistent = true;
							this.Hostage.BlockPermanentEvents = true;
							HospitalEmergency.Suspect.Tasks.FollowNavigationMeshToPosition(this.Hostage.Position, this.Hostage.Heading, 5.5f, 1f).WaitForCompletion();
							this.Hostage.Position = HospitalEmergency.Suspect.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
							Random rhcp = new Random();
							int WeaponModel = rhcp.Next(1, 4);
							Game.LogTrivial("YOBBINCALLOUTS: Suspect Weapon Model is " + WeaponModel.ToString());
							bool flag4 = WeaponModel == 1;
							if (flag4)
							{
								HospitalEmergency.Suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, true);
							}
							else
							{
								bool flag5 = WeaponModel == 2;
								if (flag5)
								{
									HospitalEmergency.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
								}
								else
								{
									bool flag6 = WeaponModel == 3;
									if (flag6)
									{
										HospitalEmergency.Suspect.Inventory.GiveNewWeapon("WEAPON_MICROSMG", -1, true);
									}
								}
							}
							Game.DisplaySubtitle("~r~Patient:~w~ Don't come any closer, or they'll die!!");
							HospitalEmergency.Suspect.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 1f, 0).WaitForCompletion(500);
							HospitalEmergency.Suspect.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 1f, 33);
							this.HostageBlip = this.Hostage.AttachBlip();
							this.HostageBlip.IsFriendly = true;
							this.HostageBlip.Scale = 0.69f;
							bool isFemale = this.Hostage.IsFemale;
							if (isFemale)
							{
								this.Hostage.Tasks.PlayAnimation("anim@move_hostages@female", "female_idle", 1f, 1);
							}
							else
							{
								this.Hostage.Tasks.PlayAnimation("anim@move_hostages@male", "male_idle", 1f, 33);
							}
							int lewis = 0;
							int num = lewis;
							int num2 = num;
							if (num2 == 0)
							{
								GameFiber.Wait(1000);
								Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to Reason with the ~o~Patient.");
								this.HostageHold();
								bool isAlive2 = HospitalEmergency.Suspect.IsAlive;
								if (isAlive2)
								{
									Game.DisplaySubtitle("~g~You:~w~ " + HospitalEmergency.Suspect.Forename + "! You don't have to do this! Let's talk this through!");
									this.HostageHold();
									bool isAlive3 = HospitalEmergency.Suspect.IsAlive;
									if (isAlive3)
									{
										Game.DisplaySubtitle(this.DialogueAdvance(this.Hostage2));
										this.HostageHold();
										bool isAlive4 = HospitalEmergency.Suspect.IsAlive;
										if (isAlive4)
										{
											Game.DisplaySubtitle(this.DialogueAdvance(this.Hostage3));
											Random morsha = new Random();
											int action = morsha.Next(0, 2);
											Game.LogTrivial("YOBBINCALLOUTS: Suspect Action is " + WeaponModel.ToString());
											bool flag7 = action == 0;
											if (flag7)
											{
												this.HostageHold();
												bool isAlive5 = HospitalEmergency.Suspect.IsAlive;
												if (isAlive5)
												{
													Game.DisplaySubtitle(this.DialogueAdvance(this.Release1));
													this.HostageHold();
													bool isAlive6 = HospitalEmergency.Suspect.IsAlive;
													if (isAlive6)
													{
														Game.DisplaySubtitle(this.DialogueAdvance(this.Release2));
														Random zach = new Random();
														int WaitTime = zach.Next(2000, 5000);
														GameFiber.Wait(WaitTime);
														HospitalEmergency.Suspect.Tasks.PutHandsUp(-1, this.player);
														bool isDead = HospitalEmergency.Suspect.IsDead;
														if (!isDead)
														{
															Game.DisplaySubtitle("~r~Patient:~w~ Okay Officer, If you say so! Just don't let them hurt me!!");
															GameFiber.Wait(500);
															this.Hostage.Tasks.ReactAndFlee(HospitalEmergency.Suspect);
															GameFiber.Wait(2000);
															Game.DisplayHelp("Take the ~o~Patient~w~ into Custody.");
															while (!Functions.IsPedArrested(HospitalEmergency.Suspect))
															{
																GameFiber.Wait(0);
															}
															bool flag8 = EntityExtensions.Exists(this.HostageBlip);
															if (flag8)
															{
																this.HostageBlip.Delete();
															}
														}
													}
												}
											}
											else
											{
												this.HostageHold();
												bool isAlive7 = HospitalEmergency.Suspect.IsAlive;
												if (isAlive7)
												{
													Game.DisplaySubtitle(this.DialogueAdvance(this.Kill1));
													this.HostageHold();
													bool isAlive8 = HospitalEmergency.Suspect.IsAlive;
													if (isAlive8)
													{
														Game.DisplaySubtitle(this.DialogueAdvance(this.Kill2));
														this.HostageHold();
														bool isAlive9 = HospitalEmergency.Suspect.IsAlive;
														if (isAlive9)
														{
															Game.DisplaySubtitle(this.DialogueAdvance(this.Kill3));
															Random zach2 = new Random();
															int WaitTime2 = zach2.Next(1500, 5000);
															GameFiber.Wait(WaitTime2);
															bool isDead2 = HospitalEmergency.Suspect.IsDead;
															if (!isDead2)
															{
																HospitalEmergency.Suspect.Tasks.FireWeaponAt(this.Hostage, -1, 1566631136).WaitForCompletion();
																while (EntityExtensions.Exists(HospitalEmergency.Suspect) && !Functions.IsPedArrested(HospitalEmergency.Suspect) && HospitalEmergency.Suspect.IsAlive)
																{
																	GameFiber.Wait(0);
																}
																bool flag9 = EntityExtensions.Exists(this.HostageBlip);
																if (flag9)
																{
																	this.HostageBlip.Delete();
																}
																bool flag10 = EntityExtensions.Exists(this.Hostage) && this.Hostage.IsAlive;
																if (flag10)
																{
																	this.Hostage.Tasks.ReactAndFlee(HospitalEmergency.Suspect);
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
							bool flag11 = EntityExtensions.Exists(this.HostageBlip);
							if (flag11)
							{
								this.HostageBlip.Delete();
							}
							bool flag12 = EntityExtensions.Exists(this.Hostage) && this.Hostage.IsAlive;
							if (flag12)
							{
								this.Hostage.Tasks.ReactAndFlee(this.player);
							}
							HospitalEmergency.Suspect.Tasks.PutHandsUp(5000, this.player);
							bool flag13 = Functions.IsPedArrested(HospitalEmergency.Suspect) || HospitalEmergency.Suspect.IsAlive;
							if (flag13)
							{
								Game.DisplayNotification("Dispatch, we have taken the Patient into ~r~Custody.");
								GameFiber.Wait(1500);
								Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
								GameFiber.Wait(1500);
								this.DriveBack();
							}
							else
							{
								Game.DisplayNotification("Dispatch, Suspect has been ~r~Killed.");
							}
							GameFiber.Wait(2000);
							Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
							GameFiber.Wait(1500);
						}
						else
						{
							Game.LogTrivial("YOBBINCALLOUTS: PURSUIT SCENARIO STARTED");
							CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
							{
								HospitalEmergency.Suspect
							});
						}
					}
					else
					{
						Game.DisplayNotification("Dispatch, Suspect has been ~r~Killed.");
					}
				}
				else
				{
					CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
					{
						HospitalEmergency.Suspect
					});
				}
			}
		}

		
		private void DriveBack()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				GameFiber.Wait(1500);
				Game.DisplayHelp("Take the ~r~Patient~w~ back to the ~b~Hospital.");
				this.NurseBlip = this.Nurse.AttachBlip();
				this.NurseBlip.IsFriendly = true;
				this.NurseBlip.IsRouteEnabled = true;
				GameFiber.Wait(1500);
				bool flag = !Main.STP;
				if (flag)
				{
					Game.DisplayHelp(string.Concat(new string[]
					{
						"~y~",
						Config.Key1.ToString(),
						": ~b~Ask the Patient to Enter the Passenger Seat. ~y~",
						Config.Key2.ToString(),
						":~b~ Ask the Patient to Enter the Rear Seat."
					}));
					while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
					{
						GameFiber.Wait(0);
					}
					bool flag2 = Game.IsKeyDown(Config.Key1);
					if (flag2)
					{
						int SeatIndex = Game.LocalPlayer.Character.LastVehicle.GetFreePassengerSeatIndex().Value;
						HospitalEmergency.Suspect.Tasks.EnterVehicle(Game.LocalPlayer.Character.LastVehicle, SeatIndex, 0).WaitForCompletion();
					}
					else
					{
						int SeatIndex2 = Game.LocalPlayer.Character.LastVehicle.GetFreeSeatIndex(1, 2).Value;
						HospitalEmergency.Suspect.Tasks.EnterVehicle(Game.LocalPlayer.Character.LastVehicle, SeatIndex2, 0).WaitForCompletion();
					}
					while (this.player.DistanceTo(this.Nurse) >= 15f && !Game.IsKeyDown(Config.CalloutEndKey))
					{
						GameFiber.Wait(0);
					}
					Game.DisplayHelp("Stop Your Vehicle to Let the ~r~Patient ~w~Out.");
					while (this.player.CurrentVehicle.Speed > 0f)
					{
						GameFiber.Wait(0);
					}
					HospitalEmergency.Suspect.Tasks.LeaveVehicle(Game.LocalPlayer.Character.CurrentVehicle, 256).WaitForCompletion();
					GameFiber.Wait(1000);
				}
				else
				{
					Game.DisplayHelp("Use ~b~StopThePed~w~ to Take the Patient ~y~Back.");
					while (this.player.DistanceTo(this.Nurse) >= 15f && !Game.IsKeyDown(Config.CalloutEndKey))
					{
						GameFiber.Wait(0);
					}
					Game.DisplayHelp("Let the ~r~Patient ~w~Out of the Car.");
					while (HospitalEmergency.Suspect.IsInAnyVehicle(false))
					{
						GameFiber.Wait(0);
					}
				}
				this.Nurse.Tasks.GoStraightToPosition(HospitalEmergency.Suspect.GetOffsetPositionFront(-1f), 3f, HospitalEmergency.Suspect.Heading, 1f, 4000).WaitForCompletion(4000);
				CallHandler.IdleAction(this.Nurse, false);
				CallHandler.IdleAction(this.Guard, true);
				Game.DisplayNotification("Dispatch, we are ~g~Code 4.~w~ We have taken the Patient back to the ~b~Hospital.");
				GameFiber.Wait(1500);
				Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
				GameFiber.Wait(1500);
			}
		}

		
		private void Pursuit()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.DisplayNotification("Suspect is ~r~Evading!");
				HospitalEmergency.Suspect.Tasks.ClearImmediately();
				Functions.ForceEndCurrentPullover();
				this.MainPursuit = Functions.CreatePursuit();
				Functions.RequestBackup(HospitalEmergency.Suspect.Position, 1, 0);
				Functions.SetPursuitIsActiveForPlayer(this.MainPursuit, true);
				Functions.AddPedToPursuit(this.MainPursuit, HospitalEmergency.Suspect);
				GameFiber.Wait(1500);
				Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
				while (Functions.IsPursuitStillRunning(this.MainPursuit))
				{
					GameFiber.Wait(0);
				}
				while (EntityExtensions.Exists(HospitalEmergency.Suspect))
				{
					GameFiber.Yield();
					bool flag = !EntityExtensions.Exists(HospitalEmergency.Suspect) || HospitalEmergency.Suspect.IsDead || Functions.IsPedArrested(HospitalEmergency.Suspect);
					if (flag)
					{
						break;
					}
				}
				bool isAlive = HospitalEmergency.Suspect.IsAlive;
				if (isAlive)
				{
					Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Following the Pursuit.");
					this.DriveBack();
				}
				else
				{
					GameFiber.Wait(1000);
					Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Pursuit.");
				}
				GameFiber.Wait(2000);
				Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
			}
		}

		
		private void HostageHold()
		{
			bool flag3;
			do
			{
				GameFiber.Yield();
				bool flag = HospitalEmergency.Suspect.Tasks.CurrentTaskStatus == 2;
				if (flag)
				{
					HospitalEmergency.Suspect.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 1f, 1);
				}
				bool flag2 = HospitalEmergency.Suspect.IsDead || Functions.IsPedArrested(HospitalEmergency.Suspect) || this.Hostage.IsDead;
				if (flag2)
				{
					break;
				}
				flag3 = Game.IsKeyDown(Config.MainInteractionKey);
			}
			while (!flag3);
		}

		
		private string DialogueAdvance(List<string> dialogue)
		{
			Random twboop = new Random();
			int dialoguechosen = twboop.Next(0, dialogue.Count);
			return dialogue[dialoguechosen];
		}

		
		private Vector3 MainSpawnPoint;

		
		private static Citizen Suspect;

		
		private Ped Nurse;

		
		private Ped Guard;

		
		private Ped Hostage;

		
		private Blip SuspectBlip;

		
		private Blip NurseBlip;

		
		private Blip Area;

		
		private Blip HostageBlip;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private LHandle MainPursuit;

		
		private int MainScenario;

		
		private bool CalloutRunning;

		
		private readonly List<string> NurseOpening1 = new List<string>
		{
			"~b~Nurse:~w~ Hey Officer, Over Here!!",
			"~g~You:~w~ What's going on? Are you guys okay?",
			"~b~Nurse:~w~ Yeah we're fine, but we got a big problem here!",
			"~b~Nurse:~w~ We just had a patient escape who has a known history of serious mental health issues.",
			"~b~Nurse:~w~ They were saying some really concerning and threatening things before they escaped!",
			"~g~You:~w~ Do you know where they went?",
			"~b~Nurse:~w~ I have no clue. You got to find them as soon as possible, for their safety and everyone else's!",
			"~g~You:~w~ Is there any information on the patient, or a description?",
			"~b~Nurse:~w~ Yes, I have some medical records right here, take them! This will explain their diagnoses too."
		};

		
		private readonly List<string> GuardOpening1 = new List<string>
		{
			"~b~Security Guard: ~w~Officer, Over Here!",
			"~g~You:~w~ What's going on? Are you guys okay?",
			"~b~Security Guard: ~w~We're fine, but our situation here isn't!",
			"~g~You:~w~ What happened?",
			"~b~Security Guard: ~w~A person came in bleeding.",
			"~g~You:~w~ Hospital staff started treating them immediately, but it became clear we'd need to contact the police regarding their condition.",
			"~b~Security Guard: ~w~When we mentioned the Police were on their way, the suspect freaked out and became violent with the nurses and doctors.",
			"~b~Security Guard: ~w~They ran out of the ER before anyone could stop them! I'm worried they won't make it far with their injuries, or worse yet, hurt someone else!",
			"~g~You:~w~ Do you have a description or location I should start looking?!",
			"~b~Security Guard:~w~ Yes, I have some medical records right here, take them! This will explain their diagnoses too."
		};

		
		private readonly List<string> Hostage2 = new List<string>
		{
			"~r~Patient:~w~ No! They have to die! They're Gonna kill me!!",
			"~r~Patient:~w~ Don't step closer Officer!! They gotta die! They're out to kill me!",
			"~r~Patient:~w~ I can't do that Officer!! This person is trying to kill me!!"
		};

		
		private readonly List<string> Hostage3 = new List<string>
		{
			"~g~You:~w~ I want to help you! The people at the hospital do, too! Just let them go and we can work this through.",
			"~g~You:~w~ I want to help you! We can't do that until you let them go! We can get you safe once you do that!",
			"~g~You:~w~ We want to help you! We can make sure that doesn't happen once you let them go!"
		};

		
		private readonly List<string> Release1 = new List<string>
		{
			"~r~Patient:~w~ Do you promise? They'll keep me safe from these people trying to kill me?",
			"~r~Patient:~w~ Are you sure Officer? They'll keep all these people trying to kill me away?",
			"~r~Patient:~w~ You'll help me? I need protection from all these people around me trying to kill me!!"
		};

		
		private readonly List<string> Release2 = new List<string>
		{
			"~g~You:~w~ Yes I promise! Just let them go, please!",
			"~g~You:~w~ Yes I promise! Let them go and we can work this through!",
			"~g~You:~w~ I promise we'll keep you safe once you let them go!"
		};

		
		private readonly List<string> Kill1 = new List<string>
		{
			"~r~Patient:~w~ No you don't! Nobody ever takes me seriously!!",
			"~r~Patient:~w~ Nobody takes me seriously! Not a single person has ever done anything for me!",
			"~r~Patient:~w~ No you don't! This person has to die before they kill me!"
		};

		
		private readonly List<string> Kill2 = new List<string>
		{
			"~g~You~w~ I'll make sure you're looked after! You need to let them go for me first!",
			"~g~You~w~ Let them go and I promise you you'll be okay! Just do that for me, please!!",
			"~g~You~w~ Whatever has happened to you, I'm sorry! We'll make sure everything is alright once you let them go!"
		};

		
		private readonly List<string> Kill3 = new List<string>
		{
			"~r~Patient:~w~ I can't do that! That would be bad for everybody!!",
			"~r~Patient:~w~ I can't do that Officer! I can't let these people go! They're evil!",
			"~r~Patient:~w~ I will not do that! I won't let them go! They must die!!"
		};
	}
}
