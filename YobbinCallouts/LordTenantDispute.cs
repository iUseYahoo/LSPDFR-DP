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
	
	[CalloutInfo("Landlord-Tenant Dispute", 2)]
	internal class LandlordTenantDispute : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Landlord-Tenant Dispute Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 0);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario value is: " + this.MainScenario.ToString());
			CallHandler.locationChooser(CallHandler.HouseList, 696f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			bool result;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
				base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 25f);
				base.AddMinimumDistanceCheck(60f, this.MainSpawnPoint);
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CITIZENS_REPORT_01 YC_CIVIL_DISTURBANCE");
				base.CalloutMessage = "Landlord-Tenant Dispute";
				base.CalloutPosition = this.MainSpawnPoint;
				bool flag = this.MainScenario == 0;
				if (flag)
				{
					base.CalloutAdvisory = "A landlord reports a tenant refusing to leave their property.";
				}
				result = base.OnBeforeCalloutDisplayed();
			}
			else
			{
				Game.LogTrivial("YOBBINCALLOUTS: No house found. Aborting...");
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			try
			{
				Game.LogTrivial("YOBBINCALLOUTS: Landlord-Tenant Dispute Callout Accepted by User");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 02", "");
				}
				else
				{
					Game.DisplayNotification("Respond ~b~Code 2.");
				}
				bool flag = this.MainScenario == 0;
				if (flag)
				{
					this.Landlord = new Ped(this.MainSpawnPoint.Around(2f));
					this.Landlord.IsPersistent = true;
					this.Landlord.BlockPermanentEvents = true;
					this.LandlordBlip = CallHandler.AssignBlip(this.Landlord, Color.Blue, 0.69f, "Caller", true, 1f);
					if (LandlordTenantDispute.<>o__24.<>p__0 == null)
					{
						LandlordTenantDispute.<>o__24.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(LandlordTenantDispute), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					LandlordTenantDispute.<>o__24.<>p__0.Target(LandlordTenantDispute.<>o__24.<>p__0, NativeFunction.Natives, this.Landlord, this.player, -1);
					bool displayHelp = Config.DisplayHelp;
					if (displayHelp)
					{
						Game.DisplayHelp("Go to the scene and speak with the ~b~Landlord.");
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
			bool flag2 = !this.CalloutRunning;
			if (flag2)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Landlord-Tenant Dispute Callout Not Accepted by User.");
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
						while (this.player.DistanceTo(this.Landlord) >= 25f && !Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(Config.CalloutEndKey);
						if (!flag)
						{
							CallHandler.IdleAction(this.Landlord, false);
							while (this.player.DistanceTo(this.Landlord) >= 5f)
							{
								GameFiber.Wait(0);
							}
							this.Landlord.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
							bool displayHelp = Config.DisplayHelp;
							if (displayHelp)
							{
								Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to speak with the ~b~Landlord.");
							}
							Random dialogue = new Random();
							int OpeningDialogue = dialogue.Next(0, 0);
							bool flag2 = OpeningDialogue == 0;
							if (flag2)
							{
								CallHandler.Dialogue(this.LandlordOpening1, this.Landlord, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
							Game.DisplayHelp(string.Concat(new string[]
							{
								"~y~",
								Config.Key1.ToString(),
								":~b~ Okay, I'll talk to the Resident. ~y~",
								Config.Key2.ToString(),
								": ~b~I can't do that for you I'm afraid."
							}));
							CallHandler.IdleAction(this.Landlord, false);
							while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
							{
								GameFiber.Wait(0);
							}
							bool flag3 = Game.IsKeyDown(Config.Key1);
							if (flag3)
							{
								Random dialogue2 = new Random();
								Game.DisplaySubtitle(this.AcceptSpeech[dialogue2.Next(0, this.AcceptSpeech.Count)], 2500);
								GameFiber.Wait(3000);
								Game.DisplayHelp("Knock on the ~o~Door.");
								this.HouseBlip = new Blip(this.MainSpawnPoint);
								this.HouseBlip.Color = Color.Orange;
								this.HouseBlip.Alpha = 0.69f;
								bool flag4 = this.Landlord.DistanceTo(this.MainSpawnPoint) > 8f;
								if (flag4)
								{
									this.Landlord.Tasks.FollowNavigationMeshToPosition(this.MainSpawnPoint, this.player.Heading, 2.69f, 2f);
								}
								while (this.player.DistanceTo(this.MainSpawnPoint) >= 3f)
								{
									GameFiber.Wait(0);
								}
								Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to ~b~Ring~w~ the Doorbell.");
								while (!Game.IsKeyDown(Config.MainInteractionKey))
								{
									GameFiber.Wait(0);
								}
								CallHandler.Doorbell();
								bool flag5 = EntityExtensions.Exists(this.HouseBlip);
								if (flag5)
								{
									this.HouseBlip.Delete();
								}
								GameFiber.Wait(2500);
								Random dialogue3 = new Random();
								int ReasonDialogue = dialogue3.Next(0, 0);
								bool displayHelp2 = Config.DisplayHelp;
								if (displayHelp2)
								{
									Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to reason with the ~r~Tenant.");
								}
								bool flag6 = this.MainScenario == 0;
								if (flag6)
								{
									bool flag7 = ReasonDialogue == 0;
									if (flag7)
									{
										CallHandler.Dialogue(this.SuspectCoop1, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
									}
									GameFiber.Wait(2000);
									this.Suspect = new Ped(this.MainSpawnPoint, this.player.Heading - 180f);
									this.SuspectBlip = CallHandler.AssignBlip(this.Suspect, Color.Red, 0.69f, "Tenant", false, 1f);
									Random r2 = new Random();
									int action = r2.Next(0, 4);
									Game.LogTrivial("YOBBINCALLOUTS: Tenant action value is " + action.ToString());
									bool flag8 = action <= 1;
									if (flag8)
									{
										GameFiber.Wait(1500);
										bool flag9 = CallHandler.FiftyFifty();
										if (flag9)
										{
											CallHandler.Dialogue(this.SuspectLeaves1, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
										}
										else
										{
											CallHandler.Dialogue(this.SuspectLeaves2, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
										}
										GameFiber.Wait(1500);
										CallHandler.IdleAction(this.Suspect, false);
										Game.DisplayHelp("Talk to the ~b~Landlord.");
										while (this.player.DistanceTo(this.Landlord) > 5f)
										{
											GameFiber.Wait(0);
										}
										this.Landlord.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
										Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to Inform the ~b~Landlord.");
										while (!Game.IsKeyDown(Config.MainInteractionKey))
										{
											GameFiber.Wait(0);
										}
										bool flag10 = action == 0;
										if (flag10)
										{
											Game.LogTrivial("YOBBINCALLOUTS: Landlord leaves.");
											bool flag11 = CallHandler.FiftyFifty();
											if (flag11)
											{
												CallHandler.Dialogue(this.LandlordLeaves1, this.Landlord, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
											}
											else
											{
												CallHandler.Dialogue(this.LandlordLeaves2, this.Landlord, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
											}
											GameFiber.Wait(1500);
											bool flag12 = EntityExtensions.Exists(this.Suspect);
											if (flag12)
											{
												this.Suspect.Tasks.AchieveHeading(this.Suspect.Heading - 180f).WaitForCompletion(500);
											}
											bool flag13 = EntityExtensions.Exists(this.Suspect);
											if (flag13)
											{
												this.Suspect.Delete();
											}
											bool flag14 = EntityExtensions.Exists(this.Landlord);
											if (flag14)
											{
												this.Landlord.Dismiss();
											}
										}
										else
										{
											Game.LogTrivial("YOBBINCALLOUTS: Landlord wants tenant arrested.");
											bool flag15 = CallHandler.FiftyFifty();
											if (flag15)
											{
												CallHandler.Dialogue(this.LandlordArrests1, this.Landlord, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
											}
											else
											{
												CallHandler.Dialogue(this.LandlordArrests2, this.Landlord, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
											}
											GameFiber.Wait(1500);
											CallHandler.IdleAction(this.Landlord, false);
											Game.DisplayHelp("Deal with the ~r~Tenant~w~ as you see fit. Press ~y~" + Config.CalloutEndKey.ToString() + " ~w~when ~b~Done.~w~");
											while (!Game.IsKeyDown(Config.CalloutEndKey))
											{
												GameFiber.Wait(0);
											}
										}
									}
									else
									{
										Game.LogTrivial("YOBBINCALLOUTS: Landlord attacks.");
										Random r3 = new Random();
										GameFiber.Wait(r3.Next(500, 3000));
										Random dialogue4 = new Random();
										Game.DisplaySubtitle(this.AttackSpeech[dialogue4.Next(0, this.AttackSpeech.Count)], 2000);
										GameFiber.Wait(r3.Next(500, 2000));
										this.Suspect.BlockPermanentEvents = false;
										this.Landlord.Tasks.FightAgainst(this.Suspect, -1);
										bool flag16 = action == 2;
										if (flag16)
										{
											this.Suspect.BlockPermanentEvents = false;
											this.Suspect.Tasks.ReactAndFlee(this.Landlord);
											Game.LogTrivial("YOBBINCALLOUTS: Tenant flees/fights back.");
										}
										else
										{
											this.Suspect.Tasks.FightAgainst(this.Landlord, -1);
											Game.LogTrivial("YOBBINCALLOUTS: Tenant fights back.");
										}
										this.LandlordBlip.Color = Color.Orange;
										bool flag17 = action == 2;
										if (flag17)
										{
											CallHandler.SuspectWait(this.Landlord);
										}
										else
										{
											while (EntityExtensions.Exists(this.Suspect) || EntityExtensions.Exists(this.Landlord))
											{
												GameFiber.Yield();
												bool flag18 = !EntityExtensions.Exists(this.Suspect) || this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect);
												if (flag18)
												{
													bool flag19 = !EntityExtensions.Exists(this.Landlord) || this.Landlord.IsDead || Functions.IsPedArrested(this.Landlord);
													if (flag19)
													{
														break;
													}
												}
											}
										}
										GameFiber.Wait(2500);
										Game.LogTrivial("YOBBINCALLOUTS: Tenant and Landlord either arrested/killed.");
										Game.DisplayNotification("Deal with the ~o~Landlord~w~ and ~r~Tenant~w~ as you see fit. Press ~b~" + Config.CalloutEndKey.ToString() + " ~w~when done.");
										while (!Game.IsKeyDown(Config.CalloutEndKey))
										{
											GameFiber.Wait(0);
										}
									}
								}
							}
							else
							{
								int EndingDialogue = dialogue.Next(0, 3);
								bool flag20 = EndingDialogue == 0;
								if (flag20)
								{
									CallHandler.Dialogue(this.Deny1, this.Landlord, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								}
								else
								{
									bool flag21 = EndingDialogue == 1;
									if (flag21)
									{
										CallHandler.Dialogue(this.Deny2, this.Landlord, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
									}
									else
									{
										CallHandler.Dialogue(this.Deny3, this.Landlord, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
									}
								}
								this.Landlord.Dismiss();
								bool flag22 = EntityExtensions.Exists(this.LandlordBlip);
								if (flag22)
								{
									this.LandlordBlip.Delete();
								}
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
			bool flag2 = EntityExtensions.Exists(this.LandlordBlip);
			if (flag2)
			{
				this.LandlordBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.HouseBlip);
			if (flag3)
			{
				this.HouseBlip.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.Landlord);
			if (flag4)
			{
				this.Landlord.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.Suspect);
			if (flag5)
			{
				this.Suspect.Dismiss();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Landlord-Tenant Dispute Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private Vector3 MainSpawnPoint;

		
		private Ped Suspect;

		
		private Ped Landlord;

		
		private Blip LandlordBlip;

		
		private Blip SuspectBlip;

		
		private Blip HouseBlip;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private LHandle MainPursuit;

		
		private int MainScenario;

		
		private bool CalloutRunning;

		
		private readonly List<string> LandlordOpening1 = new List<string>
		{
			"~b~Landlord:~w~ Hey Officer, Appreciate you coming so quick.",
			"~g~You:~w~ No worries, what's going on here?",
			"~b~Landlord:~w~ So I've got a tenant living here who I advised 30 days ago that they'd have to move out.",
			"~b~Landlord:~w~ They were very upset by this, but I have a legitimate reason and have given them enough notice to get out.",
			"~b~Landlord:~w~ According to San Andreas State Law, They have 30 days notice to leave, which they have recieved.",
			"~g~You:~w~ Okay. Have you been able to enter the house?",
			"~b~Landlord:~w~ No, Officer. I have a key, but they dead-bolted the door from inside.",
			"~b~Landlord:~w~ I'm done wasting time with them! Please, get them off my property, Officer!"
		};

		
		private readonly List<string> AcceptSpeech = new List<string>
		{
			"~g~You:~w~ Alright, I'll see what I can do for you.",
			"~g~You:~w~ Sure, let's see what I can do here.",
			"~g~You:~w~ I understand, let me see if I can reason with them."
		};

		
		private readonly List<string> Deny1 = new List<string>
		{
			"~g~You:~w~ It doesn't seem like there's much I can do for you here, I'm afraid.",
			"~b~Landlord:~w~ What?! Why not? They're no longer welcome on my property. They're trespassing now!",
			"~g~You:~w~ You're going to have to take this up with the housing board of San Andreas. If they side with you and the tenant still doesn't leave, then you can call us.",
			"~b~Landlord:~w~ Unbelievable. I knew it was a waste of time calling you guys."
		};

		
		private readonly List<string> Deny2 = new List<string>
		{
			"~g~You:~w~ It doesn't seem like I have the authority to interfere in this situation, I'm afraid.",
			"~b~Landlord:~w~ What makes you say that? They're trespassing on my property now, Officer! I don't want them living on my property anymore.",
			"~g~You:~w~ You're going to have to take this up with the housing board of San Andreas. I don't have the authority to do anything more.",
			"~b~Landlord:~w~ Unbelievable. It's always a waste of time calling you guys."
		};

		
		private readonly List<string> Deny3 = new List<string>
		{
			"~g~You:~w~ I'm afraid I don't have the authority to help you in this kind of a situation.",
			"~b~Landlord:~w~ How so? They're trespassing on my property now, Officer! I don't want them living on my property anymore.",
			"~g~You:~w~ You're going to have to call up the housing board of San Andreas. I don't have the authority to do anything more.",
			"~b~Landlord:~w~ *Sigh* Okay Officer. Do you have their number?",
			"~g~You:~w~ Yeah, you can reach them at 323-555-6969.",
			"~b~Landlord:~w~ Alright, hopefully this turns out okay. Thanks for your help."
		};

		
		private readonly List<string> AttackSpeech = new List<string>
		{
			"~b~Landlord:~w~ You piece of shit! Squatting on MY property for months on end!",
			"~b~Landlord:~w~ You useless freeloader! You think you can get away with wasting MONTHS of my rent money?",
			"~b~Landlord:~w~ You thought you could get away with living on MY property RENT-FREE for this long?!",
			"~b~Landlord:~w~ Weren't you scared of what I would do to you after squatting in MY HOUSE for MONTHS?!"
		};

		
		private readonly List<string> SuspectCoop1 = new List<string>
		{
			"~g~You:~w~ Hey, can you please open the door and come out here? It's the Police!",
			"~r~Tenant:~w~ No Officer, I will not be unlawfully evicted from my own property!!",
			"~g~You:~w~ Look, let's at least talk this through! Let's not make this any harder than it has to be for anybody here!",
			"~r~Tenant:~w~ Okay fine, I'll come out! But you better not arrest me or unlawfully search my property, Officer! I will not be mistreated!",
			"~g~You:~w~ Of Course, just come out here and talk!"
		};

		
		private readonly List<string> SuspectLeaves1 = new List<string>
		{
			"~g~You:~w~ Thank you. Are you aware that the 30 day period for moving out of the property has expired?",
			"~r~Tenant:~w~ Yes I am Officer, I wanted to be out sooner, I haven't been able to move all my stuff out yet.",
			"~r~Tenant:~w~ I should be able to get out tomorrow if that's okay with you, most of it is gone already.",
			"~g~You:~w~ Okay, because it seems like your landlord is pretty upset about the whole thing.",
			"~r~Tenant:~w~ Yes I understand and I'm really sorry, if you could just give me one more day and I'll be out!",
			"~g~You:~w~ Okay, let me see what I can do."
		};

		
		private readonly List<string> SuspectLeaves2 = new List<string>
		{
			"~g~You:~w~ Thanks for your cooperation. Do you know why I'm here?",
			"~r~Tenant:~w~ Yes Officer, I know the 30 day period to leave is up today.",
			"~r~Tenant:~w~ It's been really hard for me to find a new place, I just found one a week ago but I need a couple more days to pack my things and leave.",
			"~g~You:~w~ Well that's good to hear. Legally, the landlord has the right to have you removed, but I'll let them know you're almost ready and I'll see what I can do.",
			"~r~Tenant:~w~ Okay, thank you so much!"
		};

		
		private readonly List<string> LandlordLeaves1 = new List<string>
		{
			"~g~You:~w~ Alright, I talked to the resident and they have a new place and are almost ready to leave.",
			"~g~You:~w~ They just need a couple more days to get the rest of their stuff moved out. Is that okay for you?",
			"~b~Landlord:~w~ Can I call you guys back again if they're still hear after then?",
			"~g~You:~w~ Absolutely, you're well within your rights to have them removed now, but if you can spare a couple more days, it'll be easier for everyone.",
			"~b~Landlord:~w~ *Sigh* Alright Officer, that sounds fair enough. Hopefully this is all over soon.",
			"~g~You:~w~ Awesome, I appreciate the cooperation, glad we were able to find a middle ground.",
			"~b~Landlord:~w~ Alright, hopefully this turns out okay. Thanks for your help."
		};

		
		private readonly List<string> LandlordLeaves2 = new List<string>
		{
			"~g~You:~w~ Alright, I talked to the resident, turns out they have a new place and are almost ready to leave.",
			"~g~You:~w~ They just require a couple more days to get the rest of their stuff moved out. Is that okay for you?",
			"~b~Landlord:~w~ Can I get them arrested if they're still hear after then?",
			"~g~You:~w~ Absolutely, you're well within your rights to have them removed now, but if you can spare a couple more days, it'll be easier for everyone.",
			"~b~Landlord:~w~ *Sigh* Okay Officer, fair enough. Hopefully this is all over soon.",
			"~g~You:~w~ Great, I thanks for the cooperation, glad we were able to find a middle ground.",
			"~b~Landlord:~w~ No worries, thanks for your help."
		};

		
		private readonly List<string> LandlordArrests1 = new List<string>
		{
			"~g~You:~w~ Alright, I talked to the resident and they have a new place and are almost ready to leave.",
			"~g~You:~w~ They just need a couple more days to get the rest of their stuff moved out. Is that okay for you?",
			"~b~Landlord:~w~ ABSOLUTELY NOT! They've already outstayed their welcome. I'm not gonna just give them more time for free!",
			"~g~You:~w~ Okay, please calm down -.",
			"~b~Landlord:~w~ I WANT THEM REMOVED! ARRESTED! NOW!"
		};

		
		private readonly List<string> LandlordArrests2 = new List<string>
		{
			"~g~You:~w~ Alright, I talked to the resident and they have a new place and are almost ready to leave.",
			"~g~You:~w~ They just need a couple more days to get the rest of their stuff moved out. Does that work for you?",
			"~b~Landlord:~w~ ABSOLUTELY NOT! They've wasted enough of my time, I'm done playing games!",
			"~g~You:~w~ Okay, please calm down -.",
			"~b~Landlord:~w~ I WANT THEM REMOVED! ARRESTED! NOW! DO YOUR JOB AND GET THEM OUT!"
		};
	}
}
