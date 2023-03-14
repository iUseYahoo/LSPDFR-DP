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
	
	[CalloutInfo("DUI Reported", 2)]
	internal class DUIReported : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: DUI Reported Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(1, 2);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario value is: " + this.MainScenario.ToString());
			CallHandler.locationChooser(CallHandler.HouseList, 600f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
			}
			else
			{
				this.MainScenario = 1;
				this.MainSpawnPoint = World.GetNextPositionOnStreet(this.player.Position.Around(600f));
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 25f);
			base.AddMinimumDistanceCheck(60f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("ATTENTION_ALL_SWAT_UNITS_01 WE_HAVE_01 UNITS_RESPOND_CODE_03_01");
			base.CalloutMessage = "DUI Reported";
			base.CalloutPosition = this.MainSpawnPoint;
			bool flag = this.MainScenario == 0;
			if (flag)
			{
				base.CalloutAdvisory = "Caller has reported an intoxicated individual who has just left their residence.";
			}
			else
			{
				base.CalloutAdvisory = "Caller has reported an intoxicated individual attempting to enter a vehicle.";
			}
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			try
			{
				Game.LogTrivial("YOBBINCALLOUTS: DUI Reported Callout Accepted by User");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 03", "");
				}
				else
				{
					Game.DisplayNotification("Respond ~r~Code 3.");
				}
				bool flag = this.MainScenario >= 0;
				if (flag)
				{
					try
					{
						if (DUIReported.<>o__23.<>p__0 == null)
						{
							DUIReported.<>o__23.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(DUIReported), new CSharpArgumentInfo[]
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
						DUIReported.<>o__23.<>p__0.Target(DUIReported.<>o__23.<>p__0, NativeFunction.Natives, this.MainSpawnPoint, ref nodePosition, ref heading, 1, 3f, 0);
						if (DUIReported.<>o__23.<>p__2 == null)
						{
							DUIReported.<>o__23.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(DUIReported)));
						}
						Func<CallSite, object, bool> target = DUIReported.<>o__23.<>p__2.Target;
						CallSite <>p__ = DUIReported.<>o__23.<>p__2;
						if (DUIReported.<>o__23.<>p__1 == null)
						{
							DUIReported.<>o__23.<>p__1 = CallSite<<>F{00000010}<CallSite, object, Vector3, float, Vector3, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "xA0F8A7517A273C05", new Type[]
							{
								typeof(bool)
							}, typeof(DUIReported), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
							}));
						}
						Vector3 outPosition;
						bool success = target(<>p__, DUIReported.<>o__23.<>p__1.Target(DUIReported.<>o__23.<>p__1, NativeFunction.Natives, this.MainSpawnPoint, heading, ref outPosition));
						bool flag2 = success;
						if (!flag2)
						{
							Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
							return false;
						}
						this.SuspectVehicle = CallHandler.SpawnVehicle(outPosition, heading, true);
					}
					catch (Exception)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
						return false;
					}
					this.Suspect = new Citizen(this.MainSpawnPoint);
					this.Suspect.IsPersistent = true;
					this.Suspect.BlockPermanentEvents = true;
					if (DUIReported.<>o__23.<>p__3 == null)
					{
						DUIReported.<>o__23.<>p__3 = CallSite<Action<CallSite, object, Citizen, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_IS_DRUNK", null, typeof(DUIReported), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					DUIReported.<>o__23.<>p__3.Target(DUIReported.<>o__23.<>p__3, NativeFunction.Natives, this.Suspect, true);
					bool flag3 = this.MainScenario == 0;
					if (flag3)
					{
						this.Witness = new Ped(this.MainSpawnPoint);
					}
					else
					{
						this.Witness = new Ped(this.Suspect.GetOffsetPosition(this.Suspect.Position.Around(3f)));
					}
					this.Witness.IsPersistent = true;
					this.Witness.BlockPermanentEvents = true;
					this.WitnessBlip = CallHandler.AssignBlip(this.Witness, Color.Blue, 0.69f, "Witness", true, 1f);
				}
				bool flag4 = this.MainScenario == 0;
				if (flag4)
				{
					this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
				}
				else
				{
					this.SuspectBlip = CallHandler.AssignBlip(this.Suspect, Color.Red, 0.69f, "Suspect", true, 1f);
					this.Witness.Heading = this.Suspect.Heading - 180f;
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
			bool flag5 = !this.CalloutRunning;
			if (flag5)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: DUI Reported Callout Not Accepted by User.");
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
						Random r = new Random();
						int TriggerDistance = r.Next(25, 45);
						Game.LogTrivial("YOBBINCALLOUTS: Callout will trigger when player is " + TriggerDistance.ToString() + " Away.");
						while (this.player.DistanceTo(this.Suspect) >= (float)TriggerDistance && !Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(Config.CalloutEndKey);
						if (!flag)
						{
							bool flag2 = this.MainScenario == 0;
							if (flag2)
							{
								Random twboop = new Random();
								int dialoguechosen = twboop.Next(0, 3);
								bool flag3 = dialoguechosen == 0;
								if (flag3)
								{
									CallHandler.Dialogue(this.WitnessOpening1, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								}
								else
								{
									bool flag4 = dialoguechosen == 1;
									if (flag4)
									{
										CallHandler.Dialogue(this.WitnessOpening2, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
									}
									else
									{
										CallHandler.Dialogue(this.WitnessOpening3, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
									}
								}
								GameFiber.Wait(1500);
								CallHandler.IdleAction(this.Witness, false);
								this.Search();
							}
							else
							{
								bool displayHelp = Config.DisplayHelp;
								if (displayHelp)
								{
									Game.DisplayHelp("~b~Investigate~w~ the Situation.");
								}
								bool isMale = this.Suspect.IsMale;
								if (isMale)
								{
									Game.DisplaySubtitle("~b~Caller:~w~ You can't drive bro! I won't let you get in that car!", 2500);
								}
								else
								{
									Game.DisplaySubtitle("~b~Caller:~w~ You can't drive miss! I won't let you get in that car!", 2500);
								}
								GameFiber.Wait(2500);
								Random monke = new Random();
								int speechtowait = monke.Next(1, 6);
								int useless = 0;
								int num = useless;
								int num2 = num;
								if (num2 == 0)
								{
									bool flag5 = speechtowait == 1 || Functions.IsPedStoppedByPlayer(this.Suspect);
									if (!flag5)
									{
										this.DialogueAdvance(this.Argument1);
										GameFiber.Wait(2500);
										bool flag6 = speechtowait == 2 || Functions.IsPedStoppedByPlayer(this.Suspect);
										if (!flag6)
										{
											this.DialogueAdvance(this.Argument2);
											GameFiber.Wait(2500);
											bool flag7 = speechtowait == 3 || Functions.IsPedStoppedByPlayer(this.Suspect);
											if (!flag7)
											{
												this.DialogueAdvance(this.Argument3);
												GameFiber.Wait(2500);
												bool flag8 = speechtowait == 4 || Functions.IsPedStoppedByPlayer(this.Suspect);
												if (!flag8)
												{
													this.DialogueAdvance(this.Argument4);
													GameFiber.Wait(2500);
												}
											}
										}
									}
								}
								bool flag9 = Functions.IsPedStoppedByPlayer(this.Suspect);
								if (flag9)
								{
									Random twboop2 = new Random();
									int dialoguechosen2 = twboop2.Next(0, 3);
									bool flag10 = dialoguechosen2 == 0;
									if (flag10)
									{
										CallHandler.Dialogue(this.Reason1, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
									}
									else
									{
										bool flag11 = dialoguechosen2 == 1;
										if (flag11)
										{
											CallHandler.Dialogue(this.Reason2, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
										}
										else
										{
											CallHandler.Dialogue(this.Reason3, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
										}
									}
									Random r2 = new Random();
									int action = r2.Next(0, 2);
									bool flag12 = action == 0;
									if (flag12)
									{
										Game.DisplayHelp("Deal with the ~r~Suspect~w~ as you see fit. Press ~b~" + Config.CalloutEndKey.ToString() + " ~w~when done.");
										while (!Game.IsKeyDown(Config.CalloutEndKey))
										{
											GameFiber.Wait(0);
										}
									}
									else
									{
										this.Pursuit();
										CallHandler.SuspectWait(this.Suspect);
									}
								}
								else
								{
									this.Suspect.Tasks.FollowNavigationMeshToPosition(this.SuspectVehicle.Position, this.SuspectVehicle.Heading - 90f, 5f, 1f, -1).WaitForCompletion();
									this.Suspect.Tasks.EnterVehicle(this.SuspectVehicle, -1).WaitForCompletion();
									this.Suspect.Tasks.CruiseWithVehicle(20f, 516);
									Game.DisplayHelp("Stop the ~r~Suspect.");
									while (!Functions.IsPlayerPerformingPullover())
									{
										GameFiber.Wait(0);
									}
									this.Pursuit();
									CallHandler.SuspectWait(this.Suspect);
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
			bool flag2 = EntityExtensions.Exists(this.WitnessBlip);
			if (flag2)
			{
				this.WitnessBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: DUI Reported Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private string DialogueAdvance(List<string> dialogue)
		{
			Random twboop = new Random();
			int dialoguechosen = twboop.Next(0, dialogue.Count);
			this.SpeechCounter++;
			return dialogue[dialoguechosen];
		}

		
		private void Pursuit()
		{
			Random monke2 = new Random();
			int waitforpursuit = monke2.Next(2500, 6000);
			GameFiber.Wait(waitforpursuit);
			Game.LogTrivial("YOBBINCALLOUTS: Suspect Pursuit Started");
			Functions.ForceEndCurrentPullover();
			this.MainPursuit = Functions.CreatePursuit();
			Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
			Game.DisplayNotification("Suspect is ~r~Evading!");
			Functions.RequestBackup(this.player.Position, 1, 0);
			Functions.SetPursuitIsActiveForPlayer(this.MainPursuit, true);
			Functions.AddPedToPursuit(this.MainPursuit, this.Suspect);
			while (Functions.IsPursuitStillRunning(this.MainPursuit))
			{
				GameFiber.Wait(0);
			}
		}

		
		private void Search()
		{
			CallHandler.VehicleInfo(this.SuspectVehicle, this.Suspect);
			GameFiber.Wait(1000);
			bool displayHelp = Config.DisplayHelp;
			if (displayHelp)
			{
				Game.DisplayHelp("Search for the ~r~Suspect~w~.");
			}
			this.Suspect.Tasks.CruiseWithVehicle(20f, 516);
			this.SuspectBlip = new Blip(this.Suspect.Position.Around(15f), 200f);
			this.SuspectBlip.IsRouteEnabled = true;
			this.SuspectBlip.Color = Color.Orange;
			this.SuspectBlip.Alpha = 0.69f;
			while (this.player.DistanceTo(this.Suspect) >= 50f)
			{
				GameFiber.Wait(0);
			}
			Game.DisplayNotification("Callers have spoted the ~r~Suspect~w~ driving ~o~Recklessly~w~. Updating Map...");
			bool flag = EntityExtensions.Exists(this.SuspectBlip);
			if (flag)
			{
				this.SuspectBlip.Delete();
			}
			CallHandler.AssignBlip(this.Suspect, Color.Red, 1f, "Suspect", false, 1f);
			while (!Functions.IsPlayerPerformingPullover())
			{
				GameFiber.Wait(0);
			}
			this.Pursuit();
			CallHandler.SuspectWait(this.Suspect);
		}

		
		private Vector3 MainSpawnPoint;

		
		private Citizen Suspect;

		
		private Ped Suspect2;

		
		private Ped Witness;

		
		private Vehicle SuspectVehicle;

		
		private Blip SuspectBlip;

		
		private Blip WitnessBlip;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private LHandle MainPursuit;

		
		private int SpeechCounter = 1;

		
		private int MainScenario;

		
		private bool CalloutRunning;

		
		private readonly List<string> WitnessOpening1 = new List<string>
		{
			"~b~Caller:~w~ Officer, come quick! We got an emergency!",
			"~g~You:~w~ Are you the caller who reported a DUI?",
			"~b~Caller:~w~ Yes that's me! A friend of mine was in no condition to drive a car, but managed to get behind the wheel and drive off!",
			"~g~You:~w~ Do you know what the vehicle looks like?",
			"~b~Caller:~w~ I do, here's a vehicle description. Please find them before they hurt themselves or others!"
		};

		
		private readonly List<string> WitnessOpening2 = new List<string>
		{
			"~b~Caller:~w~ Officer, over here!",
			"~g~You:~w~ Are you the person who called in a DUI?",
			"~b~Caller:~w~ Yes that's me! Someone at this party was in no condition to drive, but managed to get behind the wheel and drive off!",
			"~g~You:~w~ Do you have a vehicle description?",
			"~b~Caller:~w~ I do, here you go. Please find them before they hurt themselves or others!"
		};

		
		private readonly List<string> WitnessOpening3 = new List<string>
		{
			"~b~Caller:~w~ Over here, Officer",
			"~g~You:~w~ Did you call in the DUI?",
			"~b~Caller:~w~ Yes that's me! My friend and I were drinking and they had a little to much.",
			"~b~Caller:~w~ When it was time for them to leave, I offered to call them a taxi, but they refused and managed to drive off despite my best efforts!",
			"~g~You:~w~ Do you know what their vehicle looks like?",
			"~b~Caller:~w~ I do, here's a description of their vehicle. Please find them before they hurt themselves or others!"
		};

		
		private readonly List<string> Argument1 = new List<string>
		{
			"~r~Suspect:~w~ I-I told you, I'm TOTALLY fine to drive! Leave m-m-me alone!!",
			"~r~Suspect:~w~ L-Leave me alone! You can't tell me what to d-do!!",
			"~r~Suspect:~w~ You're not my mom!! I feel fine to d-drive, leave me alone!",
			"~r~Suspect:~w~ Shut up! I'm fine to drive, stop telling me what to do!"
		};

		
		private readonly List<string> Argument2 = new List<string>
		{
			"~b~Caller:~w~ You've had way to many drinks! Come on, just hand me the keys!",
			"~b~Caller:~w~ I can't let you do that! Don't get behind that wheel!",
			"~b~Caller:~w~ No, you're way to drunk! I've seen how many you've had, you can't drive!",
			"~b~Caller:~w~ Trust me, you can't drive! I'll give you a lift home later, just pass me the keys!",
			"~b~Caller:~w~ Please give me the keys! You're not going anywhere, I'll call you a taxi!"
		};

		
		private readonly List<string> Argument3 = new List<string>
		{
			"~r~Suspect:~w~ I've driven before when I was WAY more drunk than this, I can do it again!",
			"~r~Suspect:~w~ B-Believe me, this is nothing! I feel fine!",
			"~r~Suspect:~w~ Stop being such a baby about it, I've driven after having more, and I'm still here!!",
			"~r~Suspect:~w~ I ain't listening to you, stop whining like a little bitch!"
		};

		
		private readonly List<string> Argument4 = new List<string>
		{
			"~b~Caller:~w~ I've already called the cops, they'll be here any moment! Do yourself a favor and lose the keys!",
			"~b~Caller:~w~ The cops are already on their way! Don't make this any worse for yourself!",
			"~b~Caller:~w~ The police are on the way now, they're gonna be here soon! Give me the keys now!",
			"~b~Caller:~w~ This won't end well for you unless you give me the keys right now!"
		};

		
		private readonly List<string> Reason1 = new List<string>
		{
			"~g~You:~w~ Hey, stop right there for me!",
			"~r~Suspect:~w~ What do YOU want, Officer? I d-don't remember calling you!",
			"~g~You:~w~ Actually, someone called us saying that you were trying to drive after drinking too much.",
			"~r~Suspect:~w~ What?! You got the wrong person, I'm totally fine! Who snitched on me?!",
			"~g~You:~w~ Alright, well to me, you don't seem to be. Hang tight right here for me."
		};

		
		private readonly List<string> Reason2 = new List<string>
		{
			"~g~You:~w~ Hey, hold up right there for me!",
			"~r~Suspect:~w~ Who, me?! What do you want from me, Officer?! I'm just trying to get back home!!",
			"~g~You:~w~ Well, we got a call saying that you were trying to drive after drinking too much.",
			"~r~Suspect:~w~ H-huh?! ME? You got the wrong person!! Leave me alone and let me go!!",
			"~g~You:~w~ Alright, we'll see about that. Hang tight right here for me."
		};

		
		private readonly List<string> Reason3 = new List<string>
		{
			"~g~You:~w~ Hey, stop right there!",
			"~r~Suspect:~w~ What do YOU want, Officer? I never called YOU!",
			"~g~You:~w~ Actually, someone called us saying that you were going to drive off after drinking too much.",
			"~r~Suspect:~w~ That ain't me, Officer! I'm just trying to exercise my freedom of travel!",
			"~g~You:~w~ Alright, well to me, you don't seem fit to do that right now. Hang tight right here for me."
		};
	}
}

