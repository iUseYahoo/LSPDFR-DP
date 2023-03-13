using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using EmergencyCallouts.Essential;
using EmergencyCallouts.Other;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;
using RAGENativeUI;

namespace EmergencyCallouts.Callouts
{
	
	[CalloutInfo("[EC] Trespassing", 3)]
	public class Trespassing : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			base.CalloutPosition = new Vector3(0f, 0f, 3000f);
			foreach (Vector3 vector in this.CalloutPositions)
			{
				if (Vector3.Distance(Helper.MainPlayer.Position, vector) < Vector3.Distance(Helper.MainPlayer.Position, base.CalloutPosition))
				{
					base.CalloutPosition = vector;
					Helper.CalloutArea = World.GetStreetName(vector).Replace("Great Ocean Hwy", "Zancudo Grain Growers");
				}
			}
			base.ShowCalloutAreaBlipBeforeAccepting(base.CalloutPosition, (float)Settings.SearchAreaSize / 2.5f);
			base.CalloutMessage = "Trespassing";
			base.CalloutAdvisory = "Reports of a person trespassing on private property.";
			Helper.CalloutScenario = Helper.random.Next(1, 4);
			Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT CRIME_TRESPASSING IN_OR_ON_POSITION", base.CalloutPosition);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override void OnCalloutDisplayed()
		{
			if (PluginChecker.IsCalloutInterfaceRunning)
			{
				CalloutInterfaceFunctions.SendCalloutDetails(this, "CODE 2", "");
			}
			base.OnCalloutDisplayed();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " ignored the callout");
			if (!PluginChecker.IsCalloutInterfaceRunning)
			{
				Functions.PlayScannerAudio("PED_RESPONDING_DISPATCH");
			}
			base.OnCalloutNotAccepted();
		}

		
		public override bool OnCalloutAccepted()
		{
			try
			{
				if (base.CalloutPosition == this.CalloutPositions[0])
				{
					this.Center = new Vector3(512f, -610.72f, 24.43f);
					this.Entrance = new Vector3(510.59f, -666.95f, 24.4f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[1])
				{
					this.Center = new Vector3(-1170.024f, -2045.655f, 14.22536f);
					this.Entrance = new Vector3(-1156.879f, -1988.801f, 13.16036f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[2])
				{
					this.Center = new Vector3(1254.056f, -2948.477f, 9.319256f);
					this.Entrance = new Vector3(1218.99f, -2915.958f, 5.866064f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[3])
				{
					this.Center = new Vector3(737.6351f, 1285.04f, 359.7698f);
					this.Entrance = new Vector3(808.5509f, 1275.401f, 359.9711f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[4])
				{
					this.Center = new Vector3(2118.948f, 4802.422f, 41.19594f);
					this.Entrance = new Vector3(2165.78f, 4758.762f, 42f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[5])
				{
					this.Center = new Vector3(195.43f, 2786.759f, 45.65519f);
					this.Entrance = new Vector3(191.53f, 2840.427f, 44.50375f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[6])
				{
					this.Center = new Vector3(424.5334f, 6508.625f, 27.75672f);
					this.Entrance = new Vector3(426.6624f, 6549.066f, 27.6012f);
				}
				Helper.Log.OnCalloutAccepted(base.CalloutMessage, Helper.CalloutScenario);
				Helper.Display.AcceptSubtitle(base.CalloutMessage, Helper.CalloutArea);
				Helper.Display.OutdatedReminder();
				this.EntranceBlip = new Blip(this.Entrance);
				if (EntityExtensions.Exists(this.EntranceBlip))
				{
					this.EntranceBlip.IsRouteEnabled = true;
				}
				this.Suspect = new Ped(Helper.Entity.GetRandomMaleModel(), Vector3.Zero, 0f);
				this.SuspectPersona = Functions.GetPersonaForPed(this.Suspect);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorRed();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				this.SuspectBlip.Alpha = 0f;
				this.CalloutHandler();
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
			}
			return base.OnCalloutAccepted();
		}

		
		private void CalloutHandler()
		{
			try
			{
				this.CalloutActive = true;
				switch (Helper.CalloutScenario)
				{
				case 1:
					this.Scenario1();
					break;
				case 2:
					this.Scenario2();
					break;
				case 3:
					this.Scenario3();
					break;
				}
				Helper.Log.Creation(this.Suspect, Helper.PedCategory.Suspect);
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
			}
		}

		
		private void RetrieveHidingPosition(Ped suspect)
		{
			if (base.CalloutPosition == this.CalloutPositions[0])
			{
				int num = Helper.random.Next(this.RailyardHidingPositions.Length);
				suspect.Position = this.RailyardHidingPositions[num];
				suspect.Heading = this.RailyardHidingPositionsHeadings[num];
			}
			else if (base.CalloutPosition == this.CalloutPositions[1])
			{
				int num2 = Helper.random.Next(this.ScrapyardHidingPositions.Length);
				suspect.Position = this.ScrapyardHidingPositions[num2];
				suspect.Heading = this.ScrapyardHidingPositionsHeadings[num2];
			}
			else if (base.CalloutPosition == this.CalloutPositions[2])
			{
				int num3 = Helper.random.Next(this.TerminalHidingPositions.Length);
				suspect.Position = this.TerminalHidingPositions[num3];
				suspect.Heading = this.TerminalHidingPositionsHeadings[num3];
			}
			else if (base.CalloutPosition == this.CalloutPositions[3])
			{
				int num4 = Helper.random.Next(this.CountyHidingPositions.Length);
				suspect.Position = this.CountyHidingPositions[num4];
				suspect.Heading = this.CountyHidingPositionsHeadings[num4];
			}
			else if (base.CalloutPosition == this.CalloutPositions[4])
			{
				int num5 = Helper.random.Next(this.AirstripHidingPositions.Length);
				suspect.Position = this.AirstripHidingPositions[num5];
				suspect.Heading = this.AirstripHidingPositionsHeadings[num5];
			}
			else if (base.CalloutPosition == this.CalloutPositions[5])
			{
				int num6 = Helper.random.Next(this.LoadingDockHidingPositions.Length);
				suspect.Position = this.LoadingDockHidingPositions[num6];
				suspect.Heading = this.LoadingDockHidingHeadings[num6];
			}
			else if (base.CalloutPosition == this.CalloutPositions[6])
			{
				int num7 = Helper.random.Next(this.BarnHidingPositions.Length);
				suspect.Position = this.BarnHidingPositions[num7];
				suspect.Heading = this.BarnHidingHeadings[num7];
			}
			suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, 2);
		}

		
		private void RetrieveManagerPosition()
		{
			if (EntityExtensions.Exists(this.Suspect))
			{
				this.Suspect.Delete();
			}
			if (base.CalloutPosition == this.CalloutPositions[0])
			{
				this.Suspect = new Ped("ig_lifeinvad_01", base.CalloutPosition, 0f);
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorYellow();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				if (EntityExtensions.Exists(this.SuspectBlip))
				{
					this.SuspectBlip.Alpha = 0f;
				}
				int num = Helper.random.Next(this.RailyardManagerPositions.Length);
				this.Suspect.Position = this.RailyardManagerPositions[num];
				this.Suspect.Heading = this.RailyardManagerHeadings[num];
			}
			else if (base.CalloutPosition == this.CalloutPositions[1])
			{
				this.Suspect = new Ped("ig_chef", base.CalloutPosition, 0f);
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorYellow();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				if (EntityExtensions.Exists(this.SuspectBlip))
				{
					this.SuspectBlip.Alpha = 0f;
				}
				int num2 = Helper.random.Next(this.ScrapyardManagerPositions.Length);
				this.Suspect.Position = this.ScrapyardManagerPositions[num2];
				this.Suspect.Heading = this.ScrapyardManagerHeadings[num2];
			}
			else if (base.CalloutPosition == this.CalloutPositions[2])
			{
				this.Suspect = new Ped("mp_m_boatstaff_01", base.CalloutPosition, 0f);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorYellow();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				if (EntityExtensions.Exists(this.SuspectBlip))
				{
					this.SuspectBlip.Alpha = 0f;
				}
				int num3 = Helper.random.Next(this.TerminalManagerPositions.Length);
				this.Suspect.Position = this.TerminalManagerPositions[num3];
				this.Suspect.Heading = this.TerminalManagerHeadings[num3];
			}
			else if (base.CalloutPosition == this.CalloutPositions[3])
			{
				this.Suspect = new Ped("player_two", base.CalloutPosition, 0f);
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorYellow();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				if (EntityExtensions.Exists(this.SuspectBlip))
				{
					this.SuspectBlip.Alpha = 0f;
				}
				int num4 = Helper.random.Next(this.AirstripManagerPositions.Length);
				this.Suspect.Position = this.AirstripManagerPositions[num4];
				this.Suspect.Heading = this.AirstripManagerHeadings[num4];
			}
			else if (base.CalloutPosition == this.CalloutPositions[4])
			{
				this.Suspect = new Ped("ig_barry", base.CalloutPosition, 0f);
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorYellow();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				if (EntityExtensions.Exists(this.SuspectBlip))
				{
					this.SuspectBlip.Alpha = 0f;
				}
				int num5 = Helper.random.Next(this.LoadingDockManagerPositions.Length);
				this.Suspect.Position = this.LoadingDockManagerPositions[num5];
				this.Suspect.Heading = this.LoadingDockManagerHeadings[num5];
			}
			else if (base.CalloutPosition == this.CalloutPositions[5])
			{
				this.Suspect = new Ped("csb_oscar", base.CalloutPosition, 0f);
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorYellow();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				if (EntityExtensions.Exists(this.SuspectBlip))
				{
					this.SuspectBlip.Alpha = 0f;
				}
				int num6 = Helper.random.Next(this.BarnManagerPositions.Length);
				this.Suspect.Position = this.BarnManagerPositions[num6];
				this.Suspect.Heading = this.BarnManagerHeadings[num6];
			}
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, 2);
		}

		
		private void SuspectDialogue()
		{
			try
			{
				bool stopDialogue = false;
				bool stopDialogue2 = false;
				bool CompletedSuspectDialogue = false;
				string str;
				if (World.TimeOfDay.TotalHours >= 6.0 && World.TimeOfDay.TotalHours < 12.0)
				{
					str = "so early?";
				}
				else if (World.TimeOfDay.TotalHours >= 12.0 && World.TimeOfDay.TotalHours <= 21.0)
				{
					str = "in the middle of the day?";
				}
				else
				{
					str = "in the middle of the night?";
				}
				string str2 = string.Empty;
				string str3 = string.Empty;
				bool acceptsSuggestion = false;
				if (Helper.random.Next(1, 101) <= Settings.ChanceOfCallingOwner)
				{
					string[] array = new string[]
					{
						"Hmm... okay then.",
						"You know what? Fine.",
						"Sure.",
						"Seems like it's your lucky day."
					};
					string[] array2 = new string[]
					{
						"We need more officers like you sir!",
						"Hell yeah!",
						"Thank god that you are the responding officer!",
						"I knew it! Thank you!",
						"YESSS!"
					};
					int num = Helper.random.Next(array.Length);
					int num2 = Helper.random.Next(array2.Length);
					str2 = array[num];
					str3 = array2[num2];
					acceptsSuggestion = true;
				}
				else
				{
					string[] array3 = new string[]
					{
						"Ofcourse not, what are you thinking?",
						"No that'd be unprofessional.",
						"No?",
						"Uhm, I'm not even gonna answer that."
					};
					string[] array4 = new string[]
					{
						"Screw you man, we'll see in court if he presses charges.",
						"Well I guess that's that.",
						"That's just great.",
						"Ofcourse that's your answer!",
						"Ughhhhhh."
					};
					int num3 = Helper.random.Next(array3.Length);
					int num4 = Helper.random.Next(array4.Length);
					str2 = array3[num3];
					str3 = array4[num4];
					acceptsSuggestion = false;
				}
				string[] array5 = new string[]
				{
					"So, what are you doing here ",
					"What were you doing here ",
					"Why are you here "
				};
				string[] array6 = new string[]
				{
					"Do you have permission to be here?",
					"Are you allowed to be here?",
					"You're not supposed to be here are you?",
					"You're obviously not allowed to trespass.",
					"You have no business here right?"
				};
				string[] array7 = new string[]
				{
					"No, but I know the owner.. we chill man, don't ruin my friendship, at least don't tell him!",
					"Hey please, I know the owner I'm sure he and I can work something out!",
					"Hey man, I know I messed up but I know the owner and we're pretty chill! Can he and I figure something out?"
				};
				string[] array8 = new string[]
				{
					"I'll be notifying the owner soon, I can tell he's not gonna be happy to hear that you're stealing from him.",
					"I'll obviously be contacting the owner and it's up to him.",
					"It's up to the owner if he wants to press charges, not me.",
					"It's not up to me to decide that."
				};
				string[] array9 = new string[]
				{
					"Can't you just call him?",
					"Please just call him!",
					"Oh no... can you please call him for me?",
					"Please call him for me!"
				};
				int num5 = Helper.random.Next(0, array5.Length);
				int num6 = Helper.random.Next(0, array6.Length);
				int num7 = Helper.random.Next(0, array7.Length);
				int num8 = Helper.random.Next(0, array8.Length);
				int num9 = Helper.random.Next(0, array9.Length);
				string[] dialogueSuspect = new string[]
				{
					"~b~You~s~: " + array5[num5] + str,
					"~b~You~s~: " + array6[num6],
					"~y~Suspect~s~: " + array7[num7],
					"~b~You~s~: " + array8[num8],
					"~y~Suspect~s~: " + array9[num9],
					"~b~You~s~: " + str2,
					"~y~Suspect~s~: " + str3,
					"~m~Suspect Dialogue Ended"
				};
				string[] array10 = new string[]
				{
					"Hello, how can I help?",
					"Good day officer, how can I help you?",
					"Uh-oh, uhmm... what is it?",
					"Oh that's not good, what happened?",
					"Police on the line is never good, what happened?",
					"Hello, what happened?",
					"Hi, so what happened?"
				};
				string[] array11 = new string[]
				{
					"Hi, we caught a person trespassing on your property.",
					"Hello, I just caught a person trespassing on your property.",
					"Hello, I just apprehended someone trespassing on your property."
				};
				string[] array12 = new string[]
				{
					"I don't know what his intentions were, but the suspect says he knows you.",
					"He says that he knows you.",
					"The person said that you might help him get out of this."
				};
				string[] array13 = new string[]
				{
					"What's his name?",
					"Okay uhm, what's his name?",
					"What's the name of the person?",
					"Do you have a name for me?"
				};
				string[] array14 = new string[]
				{
					"Give me a second. ~m~Hey you, what's your name?",
					"I'll ask him. ~m~Hey what's your name?",
					"I'll get his name real quick..."
				};
				string[] array15 = new string[]
				{
					"It's ",
					"My name is ",
					"The name is ",
					"That would be "
				};
				string[] array16 = new string[]
				{
					"His name is ",
					"The name is ",
					"It's "
				};
				string[] array17 = new string[]
				{
					"Okay, then I'm going ahead and do that, have a nice day sir.",
					"I'll go do that then, have a nice day sir.",
					"Okay then, have a good day sir."
				};
				string[] array18 = new string[]
				{
					"You too Officer... uhh...",
					"You too, and what was your name again?",
					"Thanks, what was your name again?"
				};
				string[] array19 = new string[]
				{
					"It's Officer ",
					"I'm Officer "
				};
				string[] array20 = new string[]
				{
					"Okay, goodbye officer.",
					"Okay, have a nice day.",
					"Okay then, have a good day sir."
				};
				int num10 = Helper.random.Next(0, array10.Length);
				int num11 = Helper.random.Next(0, array11.Length);
				int num12 = Helper.random.Next(0, array12.Length);
				int num13 = Helper.random.Next(0, array13.Length);
				int num14 = Helper.random.Next(0, array14.Length);
				int num15 = Helper.random.Next(0, array15.Length);
				int num16 = Helper.random.Next(0, array16.Length);
				int num17 = Helper.random.Next(0, array17.Length);
				int num18 = Helper.random.Next(0, array18.Length);
				int num19 = Helper.random.Next(0, array19.Length);
				int num20 = Helper.random.Next(0, array20.Length);
				string str4;
				if (Helper.random.Next(1, 101) <= Settings.ChanceOfPressingCharges)
				{
					string[] array21 = new string[]
					{
						this.SuspectPersona.Forename + "? Yeah screw that guy, you can arrest that person Officer " + Helper.PlayerPersona.Surname + ".",
						"He got caught this time! Good job, have fun with him!",
						this.SuspectPersona.Forename + "? Doesn't ring a bell, I'd like to press charges, I'll come by the station ASAP.",
						"Damn it! Let him rot please."
					};
					int num21 = Helper.random.Next(array21.Length);
					str4 = array21[num21];
				}
				else
				{
					string[] array22 = new string[]
					{
						"Ugh, I don't have time for this, you can let that person go Officer " + Helper.PlayerPersona.Surname,
						"I made a few mistakes in the past too, I'll give him a second chance.",
						"I used to be like him back in the day, turned my life around, you can let him go officer.",
						this.SuspectPersona.Forename + "? You know what? I'll let it slide this time."
					};
					int num22 = Helper.random.Next(array22.Length);
					str4 = array22[num22];
				}
				string[] dialogueOwner = new string[]
				{
					"~b~You~s~: Hello sir, my name is " + Helper.PlayerPersona.FullName + ", I'm with the police department.",
					"~g~Owner~s~: " + array10[num10],
					"~b~You~s~: " + array11[num11],
					"~b~You~s~: " + array12[num12],
					"~g~Owner~s~: " + array13[num13],
					"~b~You~s~: " + array14[num14],
					"~r~Suspect~s~: " + array15[num15] + this.SuspectPersona.Forename,
					"~b~You~s~: " + array16[num16] + this.SuspectPersona.Forename,
					"~g~Owner~s~: " + str4,
					"~b~You~s~: " + array17[num17],
					"~g~Owner~s~: " + array18[num18],
					"~b~You~s~: " + array19[num19] + Helper.PlayerPersona.Surname,
					"~g~Owner~s~: " + array20[num20],
					"~m~Call Ended"
				};
				int lineSuspectCount = 0;
				int lineOwnerCount = 0;
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (this.Suspect.IsCuffed)
						{
							GameFiber.Sleep(5000);
							IL_24C:
							while (this.CalloutActive)
							{
								GameFiber.Yield();
								if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 5f && this.Suspect.IsCuffed && this.Suspect.IsAlive && Helper.MainPlayer.IsOnFoot && !CompletedSuspectDialogue)
								{
									if (Game.IsKeyDown(Settings.InteractKey) || (Game.IsControllerButtonDown(Settings.ControllerInteractKey) && Settings.AllowController && UIMenu.IsUsingController))
									{
										if (!this.DialogueStarted)
										{
											if (!Functions.IsPedKneelingTaskActive(this.Suspect))
											{
												this.Suspect.Tasks.Clear();
											}
											Game.LogTrivial("[Emergency Callouts]: Dialogue started with " + this.SuspectPersona.FullName);
										}
										this.DialogueStarted = true;
										if (!Functions.IsPedKneelingTaskActive(this.Suspect))
										{
											this.Suspect.Tasks.AchieveHeading(Helper.MainPlayer.Heading - 180f);
										}
										int lineSuspectCount;
										Game.DisplaySubtitle(dialogueSuspect[lineSuspectCount], 15000);
										if (!stopDialogue)
										{
											lineSuspectCount = lineSuspectCount;
											lineSuspectCount++;
										}
										Game.LogTrivial("[Emergency Callouts]: Displayed dialogue line " + lineSuspectCount.ToString());
										if (lineSuspectCount == dialogueSuspect.Length)
										{
											stopDialogue = true;
											Game.LogTrivial("[Emergency Callouts]: Suspect dialogue ended");
											CompletedSuspectDialogue = true;
											this.DialogueStarted = false;
											return;
										}
									}
									else if (!this.DialogueStarted)
									{
										if (Settings.AllowController && UIMenu.IsUsingController)
										{
											Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.ControllerInteractKey) + "~ to talk to the ~y~suspect");
										}
										else
										{
											Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.InteractKey) + "~ to talk to the ~y~suspect");
										}
									}
								}
							}
							return;
						}
					}
					goto IL_24C;
				});
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (acceptsSuggestion)
						{
							if (((Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 5f && this.Suspect.IsCuffed && this.Suspect.IsAlive && Helper.MainPlayer.IsOnFoot) & CompletedSuspectDialogue) && Game.IsKeyDown(Settings.InteractKey))
							{
								if (!this.DialogueStarted)
								{
									GameFiber.Sleep(4000);
									Game.LogTrivial("[Emergency Callouts]: Dialogue started with Owner");
									if (Functions.GetPlayerWalkStyle() == 8)
									{
										this.CopWalkStyle = true;
										Functions.SetPlayerWalkStyle(0);
									}
									if (Trespassing.<>o__55.<>p__1 == null)
									{
										Trespassing.<>o__55.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(Trespassing)));
									}
									Func<CallSite, object, int> target = Trespassing.<>o__55.<>p__1.Target;
									CallSite <>p__ = Trespassing.<>o__55.<>p__1;
									if (Trespassing.<>o__55.<>p__0 == null)
									{
										Trespassing.<>o__55.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
										{
											typeof(int)
										}, typeof(Trespassing), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									int num23 = target(<>p__, Trespassing.<>o__55.<>p__0.Target(Trespassing.<>o__55.<>p__0, NativeFunction.Natives, Helper.MainPlayer, 28422));
									if (Trespassing.<>o__55.<>p__2 == null)
									{
										Trespassing.<>o__55.<>p__2 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(Trespassing), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									Trespassing.<>o__55.<>p__2.Target(Trespassing.<>o__55.<>p__2, NativeFunction.Natives, this.Phone, Helper.MainPlayer, num23, 0f, 0f, 0f, 0f, 0f, 0f, true, true, false, false, 2, 1);
									Helper.MainPlayer.Tasks.PlayAnimation("cellphone@", "cellphone_call_listen_base", -1, 2f, -2f, 0f, 49);
									string text = "lspdfr\\audio\\scanner\\Emergency Callouts Audio\\PHONE_RINGING.wav";
									SoundPlayer soundPlayer = new SoundPlayer(text);
									if (File.Exists(text))
									{
										soundPlayer.Load();
										soundPlayer.Play();
									}
									GameFiber.Sleep(12000);
									Game.DisplaySubtitle("~g~Owner~s~: Hello? Who's this?", 15000);
									this.DialogueStarted = true;
								}
								else
								{
									int lineOwnerCount;
									Game.DisplaySubtitle(dialogueOwner[lineOwnerCount], 15000);
									if (!stopDialogue2)
									{
										lineOwnerCount = lineOwnerCount;
										lineOwnerCount++;
									}
									Game.LogTrivial("[Emergency Callouts]: Displayed dialogue line " + lineOwnerCount.ToString());
									if (lineOwnerCount == dialogueOwner.Length)
									{
										stopDialogue2 = true;
										Game.LogTrivial("[Emergency Callouts]: Owner Dialogue Ended");
										Helper.MainPlayer.Tasks.Clear();
										if (EntityExtensions.Exists(this.Phone))
										{
											this.Phone.Delete();
										}
										if (this.CopWalkStyle)
										{
											Functions.SetPlayerWalkStyle(8);
										}
										GameFiber.Sleep(3000);
										Helper.Handle.AdvancedEndingSequence();
										return;
									}
								}
							}
						}
						else if (CompletedSuspectDialogue)
						{
							GameFiber.Sleep(3000);
							Helper.Handle.AdvancedEndingSequence();
							return;
						}
					}
				});
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
			}
		}

		
		private void Scenario1()
		{
			try
			{
				this.RetrieveHidingPosition(this.Suspect);
				this.SuspectDialogue();
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 5f && EntityExtensions.Exists(this.Suspect) && this.PlayerArrived)
						{
							this.Suspect.Tasks.Clear();
							this.Suspect.Tasks.PutHandsUp(-1, Helper.MainPlayer);
							return;
						}
					}
				});
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
			}
		}

		
		private void Scenario2()
		{
			try
			{
				this.RetrieveManagerPosition();
				string[] dialogue = new string[]
				{
					"~y~Person~s~: Can I help you sir? I'm the person in charge.",
					"~b~You~s~: Yes, we're looking for a person matching your description, do you have anything to prove that you work here?",
					"~y~Person~s~: Yes ofcourse, here it is.",
					"~b~You~s~: Okay, looks fine to me, when did you last enter?",
					"~g~Person~s~: A few minutes ago, when my shift started.",
					"~b~You~s~: Then the caller must've made a mistake.",
					"~g~Person~s~: Well, I'm glad he called, we actually have alot of kids sneaking around here.",
					"~b~You~s~: Okay, I'm going to have a look around and make sure there is no-one else.",
					"~g~Person~s~: Okay, bye.",
					"~b~You~s~: Goodbye."
				};
				int line = 0;
				Helper.random.Next(this.RailyardManagerPositions.Length);
				int day = Helper.random.Next(1, 31);
				int month = Helper.random.Next(1, 13);
				int year = Helper.random.Next(DateTime.Now.Year, DateTime.Now.Year + 5);
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorYellow();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				if (EntityExtensions.Exists(this.SuspectBlip))
				{
					this.SuspectBlip.Alpha = 0f;
				}
				if (Trespassing.<>o__57.<>p__1 == null)
				{
					Trespassing.<>o__57.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(Trespassing)));
				}
				Func<CallSite, object, int> target = Trespassing.<>o__57.<>p__1.Target;
				CallSite <>p__ = Trespassing.<>o__57.<>p__1;
				if (Trespassing.<>o__57.<>p__0 == null)
				{
					Trespassing.<>o__57.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
					{
						typeof(int)
					}, typeof(Trespassing), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				int num = target(<>p__, Trespassing.<>o__57.<>p__0.Target(Trespassing.<>o__57.<>p__0, NativeFunction.Natives, this.Suspect, 60309));
				if (Trespassing.<>o__57.<>p__2 == null)
				{
					Trespassing.<>o__57.<>p__2 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(Trespassing), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Trespassing.<>o__57.<>p__2.Target(Trespassing.<>o__57.<>p__2, NativeFunction.Natives, this.Clipboard, this.Suspect, num, 0f, 0f, 0f, 0f, 0f, 0f, true, true, false, false, 2, 1);
				this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("amb@world_human_clipboard@male@base"), "base", 5f, 1);
				Functions.SetPedCantBeArrestedByPlayer(this.Suspect, true);
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 3f && this.PlayerArrived && this.Suspect.IsAlive)
						{
							if (Game.IsKeyDown(Settings.InteractKey) || (Game.IsControllerButtonDown(Settings.ControllerInteractKey) && Settings.AllowController && UIMenu.IsUsingController))
							{
								if (!this.DialogueStarted)
								{
									if (EntityExtensions.Exists(this.Clipboard))
									{
										this.Clipboard.Delete();
									}
									if (!Functions.IsPedKneelingTaskActive(this.Suspect))
									{
										this.Suspect.Tasks.Clear();
									}
									Game.LogTrivial("[Emergency Callouts]: Dialogue started with " + this.SuspectPersona.FullName);
								}
								this.DialogueStarted = true;
								if (!Functions.IsPedKneelingTaskActive(this.Suspect))
								{
									this.Suspect.Tasks.AchieveHeading(Helper.MainPlayer.Heading - 180f);
								}
								int line;
								Game.DisplaySubtitle(dialogue[line], 15000);
								line = line;
								line++;
								if (line == 3)
								{
									this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, 0).WaitForCompletion();
									if (this.CalloutPosition == this.CalloutPositions[0])
									{
										Game.DisplayNotification("char_rickie", "char_rickie", "Go Loco Railroad", "~y~" + this.SuspectPersona.FullName, string.Format("~b~Position~s~: Manager \n~g~Location~s~: La Mesa \n~c~Valid until {0}/{1}/{2}", month, day, year));
									}
									else if (this.CalloutPosition == this.CalloutPositions[1])
									{
										Game.DisplayNotification("char_chef", "char_chef", "Los Santos Customs", "~y~" + this.SuspectPersona.FullName, string.Format("~b~Position~s~: Manager \n~g~Location~s~: Los Santos Int'l \n~c~Valid until {0}/{1}/{2}", month, day, year));
									}
									else if (this.CalloutPosition == this.CalloutPositions[2])
									{
										Game.DisplayNotification("char_boatsite2", "char_boatsite2", "Daisy-Lee", "~y~" + this.SuspectPersona.FullName, string.Format("~b~Position~s~: Captain \n~g~Ship~s~: Daisy-Lee \n~c~Valid until {0}/{1}/{2}", month, day, year));
									}
									else if (this.CalloutPosition == this.CalloutPositions[3])
									{
										this.SuspectPersona.Forename = "Trevor";
										this.SuspectPersona.Surname = "Philips";
										this.SuspectPersona.Wanted = true;
										Game.DisplayNotification("hush_trevor", "hush_trevor", "Trevor Philips Industries", "~y~" + this.SuspectPersona.FullName, "~b~Position~s~: CEO \n~g~Location~s~: Grapeseed \n~c~The best drugs you can buy!");
									}
									else if (this.CalloutPosition == this.CalloutPositions[4])
									{
										Game.DisplayNotification("char_barry", "char_barry", "VTA Shipping Company", "~y~" + this.SuspectPersona.FullName, string.Format("~b~Position~s~: Manager \n~g~Location~s~: Blaine County \n~c~Valid until {0}/{1}/{2}", month, day, year));
									}
									else if (this.CalloutPosition == this.CalloutPositions[5])
									{
										Game.DisplayNotification("char_oscar", "char_oscar", "Wildflower Fields", "~y~" + this.SuspectPersona.FullName, string.Format("~b~Position~s~: Owner \n~g~Location~s~: Paleto Bay \n~c~Valid until {0}/{1}/{2}", month, day, year));
									}
									Game.LogTrivial("[Emergency Callouts]: Displayed " + this.SuspectPersona.FullName + "'s credentials");
								}
								if (line == 4)
								{
									Helper.MainPlayer.Tasks.GoToOffsetFromEntity(this.Suspect, 1f, 0f, 2f);
									GameFiber.Sleep(500);
									this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, 0);
									Helper.MainPlayer.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, 0);
									GameFiber.Sleep(2000);
									this.SuspectBlip.SetColorGreen();
								}
								if (line == dialogue.Length)
								{
									GameFiber.Sleep(3000);
									Helper.Handle.AdvancedEndingSequence();
									return;
								}
							}
							else if (!this.DialogueStarted)
							{
								if (Settings.AllowController && UIMenu.IsUsingController)
								{
									Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.ControllerInteractKey) + "~ to talk to the ~y~suspect");
								}
								else
								{
									Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.InteractKey) + "~ to talk to the ~y~suspect");
								}
							}
						}
					}
				});
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
			}
		}

		
		private void Scenario3()
		{
			try
			{
				this.RetrieveHidingPosition(this.Suspect);
				this.SuspectDialogue();
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 7f && EntityExtensions.Exists(this.Suspect) && this.PlayerArrived)
						{
							this.Suspect.Tasks.Clear();
							LHandle lhandle = Functions.CreatePursuit();
							Functions.AddPedToPursuit(lhandle, this.Suspect);
							Functions.SetPursuitIsActiveForPlayer(lhandle, true);
							Helper.Play.PursuitAudio();
							if (EntityExtensions.Exists(this.SuspectBlip))
							{
								this.SuspectBlip.Delete();
							}
							if (EntityExtensions.Exists(this.SearchArea))
							{
								this.SearchArea.Delete();
							}
							if (EntityExtensions.Exists(this.EntranceBlip))
							{
								this.EntranceBlip.Delete();
								return;
							}
							break;
						}
					}
				});
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
			}
		}

		
		public override void Process()
		{
			base.Process();
			try
			{
				Helper.Handle.ManualEnding();
				Helper.Handle.PreventPickupCrash(this.Suspect);
				if (Settings.AllowController)
				{
					if (Trespassing.<>o__59.<>p__0 == null)
					{
						Trespassing.<>o__59.<>p__0 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xFE99B66D079CF6BC", null, typeof(Trespassing), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Trespassing.<>o__59.<>p__0.Target(Trespassing.<>o__59.<>p__0, NativeFunction.Natives, 0, 27, true);
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.Entrance) < 15f && !this.PlayerArrived)
				{
					this.PlayerArrived = true;
					Helper.Handle.DeleteNearbyPeds(this.Suspect, 30f);
					Game.DisplaySubtitle("Find the ~r~trespasser~s~ in the ~y~area~s~.", 10000);
					if (EntityExtensions.Exists(this.EntranceBlip))
					{
						this.EntranceBlip.Delete();
					}
					this.SearchArea = new Blip(this.Suspect.Position.Around2D(30f), (float)Settings.SearchAreaSize);
					this.SearchArea.SetColorYellow();
					this.SearchArea.Alpha = 0.5f;
					Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " has arrived on scene");
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 5f && !this.PedFound && this.PlayerArrived && EntityExtensions.Exists(this.Suspect))
				{
					this.PedFound = true;
					Helper.Display.HideSubtitle();
					if (EntityExtensions.Exists(this.SuspectBlip))
					{
						this.SuspectBlip.Alpha = 1f;
					}
					if (EntityExtensions.Exists(this.SearchArea))
					{
						this.SearchArea.Delete();
					}
					Game.LogTrivial(string.Concat(new string[]
					{
						"[Emergency Callouts]: ",
						Helper.PlayerPersona.FullName,
						" has found ",
						this.SuspectPersona.FullName,
						" (Suspect)"
					}));
				}
				if (Functions.IsPedStoppedByPlayer(this.Suspect) && !this.PedDetained && EntityExtensions.Exists(this.Suspect))
				{
					this.PedDetained = true;
					Game.LogTrivial(string.Concat(new string[]
					{
						"[Emergency Callouts]: ",
						Helper.PlayerPersona.FullName,
						" has detained ",
						this.SuspectPersona.FullName,
						" (Suspect)"
					}));
					if (EntityExtensions.Exists(this.SuspectBlip))
					{
						this.SuspectBlip.Delete();
						Game.LogTrivial("[Emergency Callouts]: Deleted SuspectBlip");
					}
				}
				if (Helper.MainPlayer.Position.DistanceTo(base.CalloutPosition) > (float)Settings.SearchAreaSize * 3.5f && this.PlayerArrived && !this.PedFound)
				{
					this.PlayerArrived = false;
					if (EntityExtensions.Exists(this.SuspectBlip))
					{
						this.SuspectBlip.Alpha = 0f;
					}
					if (EntityExtensions.Exists(this.SearchArea))
					{
						this.SearchArea.Delete();
					}
					this.EntranceBlip = new Blip(this.Entrance);
					if (EntityExtensions.Exists(this.EntranceBlip))
					{
						this.EntranceBlip.IsRouteEnabled = true;
					}
					Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " has left the scene");
				}
				if (Helper.MainPlayer.IsClimbing && !this.PedFound)
				{
					Game.DisplayHelp("~p~Clue~s~: The ~r~suspect~s~ has not climbed anything");
					GameFiber.Sleep(5000);
				}
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
				this.End();
			}
		}

		
		public override void End()
		{
			base.End();
			this.CalloutActive = false;
			Functions.SetPedCantBeArrestedByPlayer(this.Suspect, false);
			if (EntityExtensions.Exists(this.Suspect))
			{
				this.Suspect.Dismiss();
			}
			if (EntityExtensions.Exists(this.SuspectBlip))
			{
				this.SuspectBlip.Delete();
			}
			if (EntityExtensions.Exists(this.SearchArea))
			{
				this.SearchArea.Delete();
			}
			if (EntityExtensions.Exists(this.EntranceBlip))
			{
				this.EntranceBlip.Delete();
			}
			if (EntityExtensions.Exists(this.WeldingDevice))
			{
				this.WeldingDevice.Delete();
			}
			if (EntityExtensions.Exists(this.Clipboard))
			{
				this.Clipboard.Delete();
			}
			if (EntityExtensions.Exists(this.Phone))
			{
				this.Phone.Delete();
			}
			Helper.Display.HideSubtitle();
			Helper.Display.EndNotification();
			Helper.Log.OnCalloutEnded(base.CalloutMessage, Helper.CalloutScenario);
		}

		
		private bool CalloutActive;

		
		private bool PlayerArrived;

		
		private bool PedFound;

		
		private bool PedDetained;

		
		private bool DialogueStarted;

		
		private bool CopWalkStyle;

		
		private Vector3 Entrance;

		
		private Vector3 Center;

		
		private readonly Vector3[] CalloutPositions = new Vector3[]
		{
			new Vector3(512.01f, -610.72f, 24.4312f),
			new Vector3(-1106.7f, -1975.5f, 24.562f),
			new Vector3(1225.66f, -2923.435f, 9.4783f),
			new Vector3(808.5509f, 1275.401f, 359.9711f),
			new Vector3(2165.78f, 4758.762f, 42.0235f),
			new Vector3(191.53f, 2840.427f, 44.50375f),
			new Vector3(426.6624f, 6549.066f, 27.601f)
		};

		
		private readonly Vector3[] RailyardHidingPositions = new Vector3[]
		{
			new Vector3(488.5f, -631f, 24.98f),
			new Vector3(523.5f, -563f, 24.765f),
			new Vector3(524.5f, -592f, 24.788f),
			new Vector3(487f, -555.5f, 25.992f),
			new Vector3(498f, -532f, 24.75114f),
			new Vector3(532f, -560.5f, 24.8f),
			new Vector3(492f, -588.5f, 24.7189f),
			new Vector3(493f, -579.913f, 24.57f),
			new Vector3(481.693f, -591f, 24.75f)
		};

		
		private readonly float[] RailyardHidingPositionsHeadings = new float[]
		{
			146f,
			187f,
			15f,
			350f,
			80f,
			80f,
			1f,
			186f,
			300f
		};

		
		private readonly Vector3[] RailyardManagerPositions = new Vector3[]
		{
			new Vector3(495.3361f, -585.4279f, 24.73708f),
			new Vector3(495.5332f, -577.2258f, 24.65661f),
			new Vector3(485.5925f, -634.8498f, 24.92816f)
		};

		
		private readonly float[] RailyardManagerHeadings = new float[]
		{
			90f,
			55f,
			112f
		};

		
		private readonly Vector3[] ScrapyardHidingPositions = new Vector3[]
		{
			new Vector3(-1155.449f, -2030.024f, 13.16065f),
			new Vector3(-1178.147f, -2083.881f, 13.41349f),
			new Vector3(-1180.712f, -2072.301f, 14.4559f),
			new Vector3(-1168.165f, -2052.083f, 14.43985f),
			new Vector3(-1181.182f, -2046.646f, 13.92571f),
			new Vector3(-1154.528f, -2052.879f, 13.91131f)
		};

		
		private readonly float[] ScrapyardHidingPositionsHeadings = new float[]
		{
			1.42f,
			346f,
			270f,
			66.24f,
			156f,
			102.71f
		};

		
		private readonly Vector3[] ScrapyardManagerPositions = new Vector3[]
		{
			new Vector3(-1161.296f, -2060.376f, 13.81086f),
			new Vector3(-1157.456f, -2033.004f, 13.16054f),
			new Vector3(-1179.293f, -2059.016f, 14.13962f)
		};

		
		private readonly float[] ScrapyardManagerHeadings = new float[]
		{
			45.99f,
			2.16f,
			74.74f
		};

		
		private readonly Vector3[] TerminalHidingPositions = new Vector3[]
		{
			new Vector3(1249.729f, -2887.772f, 9.319264f),
			new Vector3(1242.891f, -2947.398f, 9.319264f),
			new Vector3(1236.903f, -2955.032f, 9.319268f),
			new Vector3(1238.818f, -3006.345f, 9.319253f),
			new Vector3(1227.453f, -3009.943f, 9.319252f)
		};

		
		private readonly float[] TerminalHidingPositionsHeadings = new float[]
		{
			128.07f,
			160.45f,
			2.73f,
			92.93f,
			347.75f
		};

		
		private readonly Vector3[] TerminalManagerPositions = new Vector3[]
		{
			new Vector3(1228.202f, -2970.058f, 9.319256f),
			new Vector3(1238.923f, -2940.416f, 9.319255f),
			new Vector3(1229.698f, -2908.376f, 9.319265f)
		};

		
		private readonly float[] TerminalManagerHeadings = new float[]
		{
			81.28f,
			30.57f,
			310.89f
		};

		
		private readonly Vector3[] CountyHidingPositions = new Vector3[]
		{
			new Vector3(762.8389f, 1316.628f, 359.9371f),
			new Vector3(752.77f, 1317.433f, 359.8556f),
			new Vector3(720.8849f, 1296.344f, 360.2961f),
			new Vector3(664.7129f, 1287.845f, 360.2961f),
			new Vector3(757.0978f, 1265.797f, 360.2964f)
		};

		
		private readonly float[] CountyHidingPositionsHeadings = new float[]
		{
			104.9f,
			101.08f,
			63.67f,
			163.5f,
			259.55f
		};

		
		private readonly Vector3[] CountyManagerPositions = new Vector3[]
		{
			new Vector3(744.5788f, 1306.545f, 360.0878f),
			new Vector3(718.0433f, 1291.299f, 360.2962f),
			new Vector3(686.3436f, 1285.599f, 360.2962f)
		};

		
		private readonly float[] CountyManagerHeadings = new float[]
		{
			190.66f,
			188.67f,
			88.56f
		};

		
		private readonly Vector3[] AirstripHidingPositions = new Vector3[]
		{
			new Vector3(2149.073f, 4781.637f, 41.01651f),
			new Vector3(2121.007f, 4783.326f, 40.97028f),
			new Vector3(2120.194f, 4774.568f, 41.17796f),
			new Vector3(2093.352f, 4738.548f, 41.3352f),
			new Vector3(2112.155f, 4759.638f, 41.25103f)
		};

		
		private readonly float[] AirstripHidingPositionsHeadings = new float[]
		{
			200f,
			220f,
			120f,
			190f,
			5f
		};

		
		private readonly Vector3[] AirstripManagerPositions = new Vector3[]
		{
			new Vector3(2139.753f, 4791.316f, 40.97028f),
			new Vector3(2135.525f, 4772.93f, 40.97032f),
			new Vector3(2144.859f, 4779.579f, 40.97027f)
		};

		
		private readonly float[] AirstripManagerHeadings = new float[]
		{
			282.48f,
			187.58f,
			234.06f
		};

		
		private readonly Vector3[] AirstripArsonPositions = new Vector3[]
		{
			new Vector3(2144.962f, 4776.65f, 40.97034f),
			new Vector3(2108.356f, 4762.68f, 41.04375f),
			new Vector3(2125.861f, 4774.83f, 40.97033f)
		};

		
		private readonly Vector3[] LoadingDockHidingPositions = new Vector3[]
		{
			new Vector3(216.5567f, 2808.267f, 45.65519f),
			new Vector3(223.5032f, 2802.028f, 45.65519f),
			new Vector3(167.6332f, 2736.069f, 43.37733f),
			new Vector3(193.4705f, 2766.353f, 43.42632f),
			new Vector3(163.1098f, 2770.249f, 45.69321f)
		};

		
		private readonly float[] LoadingDockHidingHeadings = new float[]
		{
			182.41f,
			43.53f,
			7.96f,
			183.06f,
			9.44f
		};

		
		private readonly Vector3[] LoadingDockManagerPositions = new Vector3[]
		{
			new Vector3(221.0605f, 2774.253f, 45.65525f),
			new Vector3(221.5897f, 2734.374f, 42.996f),
			new Vector3(219.1572f, 2806.581f, 45.65519f)
		};

		
		private readonly float[] LoadingDockManagerHeadings = new float[]
		{
			148.58f,
			214.67f,
			36.07f
		};

		
		private readonly Vector3[] LoadingDockArsonPositions = new Vector3[]
		{
			new Vector3(202.8873f, 2776.132f, 45.65527f),
			new Vector3(164.554f, 2777.182f, 45.70289f),
			new Vector3(197.8674f, 2803.913f, 45.65517f)
		};

		
		private readonly Vector3[] BarnHidingPositions = new Vector3[]
		{
			new Vector3(435.1073f, 6456.916f, 28.74582f),
			new Vector3(426.8463f, 6479.765f, 28.86706f),
			new Vector3(436.3864f, 6502.764f, 28.77272f),
			new Vector3(399.7721f, 6474.169f, 29.33945f),
			new Vector3(432.5182f, 6499.175f, 28.89931f)
		};

		
		private readonly float[] BarnHidingHeadings = new float[]
		{
			337.39f,
			185.75f,
			29.36f,
			256.73f,
			121.03f
		};

		
		private readonly Vector3[] BarnManagerPositions = new Vector3[]
		{
			new Vector3(409.8339f, 6493.519f, 28.12436f),
			new Vector3(430.6472f, 6502.231f, 28.71397f),
			new Vector3(425.288f, 6467.432f, 28.79181f)
		};

		
		private readonly float[] BarnManagerHeadings = new float[]
		{
			327.94f,
			97.09f,
			19.64f
		};

		
		private readonly Vector3 BarnArsonPosition = new Vector3(419.651f, 6467.322f, 28.82159f);

		
		private readonly Object WeldingDevice = new Object(new Model("prop_weld_torch"), new Vector3(0f, 0f, 0f));

		
		private readonly Object Clipboard = new Object(new Model("p_amb_clipboard_01"), new Vector3(0f, 0f, 0f));

		
		private readonly Object Phone = new Object(new Model("prop_police_phone"), new Vector3(0f, 0f, 0f));

		
		private Ped Suspect;

		
		private Persona SuspectPersona;

		
		private Blip SuspectBlip;

		
		private Blip EntranceBlip;

		
		private Blip SearchArea;
	}
}
