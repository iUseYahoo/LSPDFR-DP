using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
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
	
	[CalloutInfo("[EC] Public Intoxication", 3)]
	public class PublicIntoxication : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int num = 0;
			base.CalloutMessage = "Public Intoxication";
			base.CalloutAdvisory = "Reports of a person under the influence of alcohol.";
			Helper.CalloutScenario = Helper.random.Next(1, 4);
			while (!World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around2D(200f, (float)Settings.MaxCalloutDistance)).GetSafePositionForPed(out this.CalloutPosition))
			{
				GameFiber.Yield();
				num++;
				if (num >= 10)
				{
					this.CalloutPosition = World.GetNextPositionOnStreet(Helper.MainPlayer.Position.Around2D(200f, (float)Settings.MaxCalloutDistance));
				}
				Helper.CalloutArea = World.GetStreetName(this.CalloutPosition);
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.CalloutPosition, (float)Settings.SearchAreaSize / 2.5f);
			base.AddMinimumDistanceCheck(30f, this.CalloutPosition);
			Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT CRIME_PUBLIC_INTOXICATION IN_OR_ON_POSITION UNITS_RESPOND_CODE_02", this.CalloutPosition);
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
			Helper.Log.OnCalloutAccepted(base.CalloutMessage, Helper.CalloutScenario);
			Helper.Display.AcceptSubtitle(base.CalloutMessage, Helper.CalloutArea);
			Helper.Display.OutdatedReminder();
			this.EntranceBlip = new Blip(this.CalloutPosition);
			if (EntityExtensions.Exists(this.EntranceBlip))
			{
				this.EntranceBlip.IsRouteEnabled = true;
			}
			this.Suspect = new Ped(Helper.Entity.GetRandomMaleModel(), this.CalloutPosition, 0f);
			this.SuspectPersona = Functions.GetPersonaForPed(this.Suspect);
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.SetIntoxicated();
			Helper.Log.Creation(this.Suspect, Helper.PedCategory.Suspect);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.SetColorYellow();
			this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
			this.SuspectBlip.Alpha = 0f;
			this.Suspect.Tasks.Wander();
			this.CalloutHandler();
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
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
			}
		}

		
		private void Dialogue()
		{
			try
			{
				bool stopDialogue = false;
				int line = 0;
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
					str = "right now, shouldn't you go home?";
				}
				string[] array = new string[]
				{
					"Hey you, come here for a second.",
					"Hey, why don't you come over here?",
					"Hello, let's have a talk okay?",
					"Hi sir how are you doing today?"
				};
				string[] array2 = new string[]
				{
					"Leave me alone.",
					"Let me be!",
					"Gimme some privacy you piggy.",
					"I clearly don't want to!",
					"You people always harassing me out here!",
					"Nuh-uh, stranger danger!",
					"Hell no, stranger equals danger remember!?",
					"No thanks and... do you guys live in piggy banks by any chance?",
					"Unless you have some beer we have nothing to talk about!"
				};
				string[] array3 = new string[]
				{
					"Calm down sir, just have talk with me okay?",
					"Calm down sir, we don't want things to escalate.",
					"Sir, if you keep this up it will be annoying for both of us.",
					"Let's just get this over with okay?"
				};
				string[] array4 = new string[]
				{
					"FINE!",
					"Alrighty!",
					"Alright then.",
					"OK!",
					"Okay.",
					"Sure.",
					"Okay buddy, fine.",
					"Yes sir!",
					"Yes mom!"
				};
				string[] array5 = new string[]
				{
					"So what are you doing here being drunk ",
					"Why are you drunk ",
					"Now is not the time to be roaming the streets "
				};
				string[] array6 = new string[]
				{
					"Who cares what or when I do things?",
					"Who cares? I aint hurting people!",
					"Who gives a damn!",
					"Who cares?"
				};
				string[] array7 = new string[]
				{
					"I do.",
					"I care.",
					"I care about what you do here sir.",
					"I just don't want anyone to get hurt sir."
				};
				string[] array8 = new string[]
				{
					"Okay, what now?",
					"Sure, so what now?",
					"Alright, what now?",
					"Ok so... what do we do now?",
					"Okay, and what exactly are we going to do now?"
				};
				int num = Helper.random.Next(0, array.Length);
				int num2 = Helper.random.Next(0, array2.Length);
				int num3 = Helper.random.Next(0, array3.Length);
				int num4 = Helper.random.Next(0, array4.Length);
				int num5 = Helper.random.Next(0, array5.Length);
				int num6 = Helper.random.Next(0, array6.Length);
				int num7 = Helper.random.Next(0, array7.Length);
				int num8 = Helper.random.Next(0, array8.Length);
				string[] dialogue = new string[]
				{
					"~b~You~s~: " + array[num],
					"~y~Suspect~s~: " + array2[num2],
					"~b~You~s~: " + array3[num3],
					"~y~Suspect~s~: " + array4[num4],
					"~b~You~s~: " + array5[num5] + str,
					"~y~Suspect~s~: " + array6[num6],
					"~b~You~s~: " + array7[num7],
					"~y~Suspect~s~: " + array8[num8],
					"~r~Arrest~s~ or ~g~dismiss~s~ the person."
				};
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 5f && this.Suspect.IsAlive && Helper.MainPlayer.IsOnFoot)
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
								int line;
								Game.DisplaySubtitle(dialogue[line], 15000);
								if (!stopDialogue)
								{
									line = line;
									line++;
								}
								Game.LogTrivial("[Emergency Callouts]: Displayed dialogue line " + line.ToString());
								if (line == dialogue.Length)
								{
									stopDialogue = true;
									Game.LogTrivial("[Emergency Callouts]: Dialogue ended");
									GameFiber.Sleep(1500);
									if (this.HasBottle)
									{
										if (Settings.AllowController && UIMenu.IsUsingController)
										{
											Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(4) + "~ to ~g~dismiss~s~ the ~y~suspect~s~ and ~o~confiscate~s~ the bottle");
										}
										else
										{
											Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Keys.N) + "~ to ~g~dismiss~s~ the ~y~suspect~s~ and ~o~confiscate~s~ the bottle");
										}
									}
									else if (Settings.AllowController && UIMenu.IsUsingController)
									{
										Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(4) + "~ to ~g~dismiss~s~ the ~y~suspect");
									}
									else
									{
										Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Keys.N) + "~ to ~g~dismiss~s~ the ~y~suspect");
									}
									while (this.CalloutActive)
									{
										GameFiber.Yield();
										if (Game.IsKeyDown(Keys.N) || (Game.IsControllerButtonDown(4) && Settings.AllowController && UIMenu.IsUsingController))
										{
											if (this.HasBottle)
											{
												Game.DisplaySubtitle("~b~You~s~: I'm letting you go, I will need that bottle from you though. You will also head straight to home.");
												GameFiber.Sleep(3000);
												Helper.MainPlayer.Tasks.GoToOffsetFromEntity(this.Suspect, 1f, 0f, 2f);
												GameFiber.Sleep(500);
												this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, 32);
												Helper.MainPlayer.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, 32);
												GameFiber.Sleep(1000);
												this.Suspect.Inventory.Weapons.Clear();
												GameFiber.Sleep(4000);
											}
											else
											{
												Game.DisplaySubtitle("~b~You~s~: So this is what you're going to do, you're gonna head straight to home.");
											}
											Helper.Handle.AdvancedEndingSequence();
											break;
										}
										if (this.Suspect.IsCuffed)
										{
											GameFiber.Sleep(3000);
											Helper.Handle.AdvancedEndingSequence();
											break;
										}
									}
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

		
		private void Scenario1()
		{
			try
			{
				this.Dialogue();
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
				this.Suspect.Inventory.GiveNewWeapon("WEAPON_BOTTLE", -1, true);
				this.HasBottle = true;
				this.Dialogue();
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
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 7f && this.Suspect.IsAlive && Helper.MainPlayer.IsOnFoot && this.PlayerArrived)
						{
							Game.DisplaySubtitle("~y~Suspect~s~: I'm drunk, sooo wha...", 10000);
							GameFiber.Sleep(1250);
							if (EntityExtensions.Exists(this.Suspect))
							{
								this.Suspect.Kill();
							}
							GameFiber.Sleep(5000);
							Game.DisplaySubtitle("Request an ~g~ambulance~s~.", 7500);
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

		
		public override void Process()
		{
			base.Process();
			try
			{
				Helper.Handle.ManualEnding();
				Helper.Handle.PreventPickupCrash(this.Suspect);
				if (Settings.AllowController)
				{
					if (PublicIntoxication.<>o__22.<>p__0 == null)
					{
						PublicIntoxication.<>o__22.<>p__0 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xFE99B66D079CF6BC", null, typeof(PublicIntoxication), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					PublicIntoxication.<>o__22.<>p__0.Target(PublicIntoxication.<>o__22.<>p__0, NativeFunction.Natives, 0, 27, true);
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.CalloutPosition) < (float)Settings.SearchAreaSize && !this.PlayerArrived)
				{
					if (EntityExtensions.Exists(this.EntranceBlip))
					{
						this.EntranceBlip.Delete();
					}
					this.SearchArea = new Blip(this.Suspect.Position.Around2D(30f), (float)Settings.SearchAreaSize);
					this.SearchArea.SetColorYellow();
					this.SearchArea.Alpha = 0.5f;
					Game.DisplaySubtitle("Find the ~y~drunk person~s~ in the ~y~area~s~.", 10000);
					Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " has arrived on scene");
					this.PlayerArrived = true;
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 5f && !this.PedFound && this.PlayerArrived && this.Suspect)
				{
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
					this.PedFound = true;
				}
				if (Functions.IsPedStoppedByPlayer(this.Suspect) && !this.PedDetained && EntityExtensions.Exists(this.Suspect))
				{
					if (EntityExtensions.Exists(this.SuspectBlip))
					{
						this.SuspectBlip.Delete();
					}
					Game.LogTrivial(string.Concat(new string[]
					{
						"[Emergency Callouts]: ",
						Helper.PlayerPersona.FullName,
						" has detained ",
						this.SuspectPersona.FullName,
						" (Suspect)"
					}));
					this.PedDetained = true;
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.CalloutPosition) > (float)Settings.SearchAreaSize * 3f && this.PlayerArrived && !this.PedFound)
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
					this.EntranceBlip = new Blip(this.CalloutPosition);
					if (EntityExtensions.Exists(this.EntranceBlip))
					{
						this.EntranceBlip.IsRouteEnabled = true;
					}
					Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " has left the scene");
				}
				if (!this.PedFound)
				{
					if (this.Suspect.Position.DistanceTo(this.CalloutPosition) < (float)Settings.SearchAreaSize)
					{
						this.NeedsRefreshing = false;
					}
					else
					{
						this.NeedsRefreshing = true;
					}
				}
				if (this.Suspect.Position.DistanceTo(this.CalloutPosition) > (float)Settings.SearchAreaSize && this.NeedsRefreshing)
				{
					this.CalloutPosition = this.Suspect.Position;
					if (EntityExtensions.Exists(this.SearchArea))
					{
						this.SearchArea.Delete();
					}
					this.SearchArea = new Blip(this.Suspect.Position.Around2D(30f), (float)Settings.SearchAreaSize);
					this.SearchArea.SetColorYellow();
					this.SearchArea.Alpha = 0.5f;
					Game.LogTrivial("[Emergency Callouts]: Refreshed SearchArea");
					Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", this.Suspect.Position);
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
			Helper.Display.HideSubtitle();
			Helper.Display.EndNotification();
			Helper.Log.OnCalloutEnded(base.CalloutMessage, Helper.CalloutScenario);
		}

		
		private bool PlayerArrived;

		
		private bool PedFound;

		
		private bool PedDetained;

		
		private bool NeedsRefreshing;

		
		private bool CalloutActive;

		
		private bool DialogueStarted;

		
		private bool HasBottle;

		
		private Vector3 CalloutPosition;

		
		private Ped Suspect;

		
		private Persona SuspectPersona;

		
		private Blip EntranceBlip;

		
		private Blip SearchArea;

		
		private Blip SuspectBlip;
	}
}
