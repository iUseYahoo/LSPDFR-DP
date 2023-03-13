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
	
	[CalloutInfo("[EC] Domestic Violence", 3)]
	public class DomesticViolence : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			base.CalloutPosition = new Vector3(0f, 0f, 3000f);
			foreach (Vector3 vector in this.CalloutPositions)
			{
				if (Vector3.Distance(Helper.MainPlayer.Position, vector) < Vector3.Distance(Helper.MainPlayer.Position, base.CalloutPosition))
				{
					base.CalloutPosition = vector;
					Helper.CalloutArea = World.GetStreetName(vector).Replace("Senora Fwy", "Grand Senora Desert");
				}
			}
			base.ShowCalloutAreaBlipBeforeAccepting(base.CalloutPosition, (float)Settings.SearchAreaSize / 2.5f);
			base.CalloutMessage = "Domestic Violence";
			base.CalloutAdvisory = "Passersby report a male continuingly hitting a female.";
			Helper.CalloutScenario = Helper.random.Next(1, 4);
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_DOMESTIC_VIOLENCE IN_OR_ON_POSITION UNITS_RESPOND_CODE_03", base.CalloutPosition);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override void OnCalloutDisplayed()
		{
			if (PluginChecker.IsCalloutInterfaceRunning)
			{
				CalloutInterfaceFunctions.SendCalloutDetails(this, "CODE 3", "");
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
					this.Center = new Vector3(0f, 526.2725f, 175.1643f);
					this.Entrance = new Vector3(11.3652f, 545.7453f, 175.8412f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[1])
				{
					this.Center = new Vector3(208.0452f, -1707.766f, 29.65307f);
					this.Entrance = new Vector3(222.883f, -1726.32f, 28.87364f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[2])
				{
					this.Center = new Vector3(-1058.305f, -995.6418f, 6.410485f);
					this.Entrance = new Vector3(-1048.924f, -1018.362f, 2.150359f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[3])
				{
					this.Center = new Vector3(1550.415f, 2203.19f, 78.74243f);
					this.Entrance = new Vector3(1504.92f, 2203.887f, 79.99944f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[4])
				{
					this.Center = new Vector3(247.4916f, 3169.519f, 42.7863f);
					this.Entrance = new Vector3(224.5887f, 3162.886f, 42.3335f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[5])
				{
					this.Center = new Vector3(1672.969f, 4670.249f, 43.40202f);
					this.Entrance = new Vector3(1687.845f, 4680.918f, 43.02761f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[6])
				{
					this.Center = new Vector3(-374.2228f, 6259.589f, 31.48723f);
					this.Entrance = new Vector3(-394.975f, 6276.961f, 29.67487f);
				}
				Helper.Log.OnCalloutAccepted(base.CalloutMessage, Helper.CalloutScenario);
				Helper.Display.AcceptSubtitle(base.CalloutMessage, Helper.CalloutArea);
				Helper.Display.OutdatedReminder();
				this.EntranceBlip = new Blip(this.Entrance);
				if (EntityExtensions.Exists(this.EntranceBlip))
				{
					this.EntranceBlip.IsRouteEnabled = true;
				}
				this.Victim = new Ped(Helper.Entity.GetRandomFemaleModel(), Vector3.Zero, 0f);
				this.VictimPersona = Functions.GetPersonaForPed(this.Victim);
				this.Victim.IsPersistent = true;
				this.Victim.BlockPermanentEvents = true;
				this.Victim.SetInjured(135);
				this.VictimBlip = this.Victim.AttachBlip();
				this.VictimBlip.SetColorOrange();
				this.VictimBlip.Scale = (float)Settings.PedBlipScale;
				this.VictimBlip.Alpha = 0f;
				this.Suspect = new Ped(Helper.Entity.GetRandomMaleModel(), this.Victim.GetOffsetPositionFront(1f), 0f);
				this.SuspectPersona = Functions.GetPersonaForPed(this.Suspect);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.SetColorRed();
				this.SuspectBlip.Scale = (float)Settings.PedBlipScale;
				this.SuspectBlip.Alpha = 0f;
				if (Helper.random.Next(2) == 1)
				{
					this.Suspect.SetIntoxicated();
					Game.LogTrivial("[Emergency Callouts]: Set Suspect intoxicated");
				}
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
			}
			catch (Exception e)
			{
				Helper.Log.Exception(e, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
			}
		}

		
		private void RetrieveFightPosition()
		{
			if (base.CalloutPosition == this.CalloutPositions[0])
			{
				int num = Helper.random.Next(this.VinewoodHillsFightPositions.Length);
				this.Victim.Position = this.VinewoodHillsFightPositions[num];
				this.Victim.Heading = this.VinewoodHillsFightHeadings[num];
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(1f);
			}
			else if (base.CalloutPosition == this.CalloutPositions[1])
			{
				int num2 = Helper.random.Next(this.DavisFightPositions.Length);
				this.Victim.Position = this.DavisFightPositions[num2];
				this.Victim.Heading = this.DavisFightHeadings[num2];
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(1f);
			}
			else if (base.CalloutPosition == this.CalloutPositions[2])
			{
				this.Victim.Position = this.VespucciFightPosition;
				this.Victim.Heading = this.VespucciFightHeading;
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(1f);
			}
			else if (base.CalloutPosition == this.CalloutPositions[3])
			{
				int num3 = Helper.random.Next(this.CountyFightPositions.Length);
				this.Victim.Position = this.CountyFightPositions[num3];
				this.Victim.Heading = this.CountyFightHeadings[num3];
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(1f);
			}
			else if (base.CalloutPosition == this.CalloutPositions[4])
			{
				int num4 = Helper.random.Next(this.SandyShoresFightPositions.Length);
				this.Victim.Position = this.SandyShoresFightPositions[num4];
				this.Victim.Heading = this.SandyShoresFightHeadings[num4];
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(1f);
			}
			else if (base.CalloutPosition == this.CalloutPositions[5])
			{
				int num5 = Helper.random.Next(this.GrapeseedFightPositions.Length);
				this.Victim.Position = this.GrapeseedFightPositions[num5];
				this.Victim.Heading = this.GrapeseedFightHeadings[num5];
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(1f);
			}
			else if (base.CalloutPosition == this.CalloutPositions[6])
			{
				int num6 = Helper.random.Next(this.PaletoBayFightPositions.Length);
				this.Victim.Position = this.PaletoBayFightPositions[num6];
				this.Victim.Heading = this.PaletoBayFightHeadings[num6];
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(1f);
			}
			Helper.Log.Creation(this.Suspect, Helper.PedCategory.Suspect);
			Helper.Log.Creation(this.Victim, Helper.PedCategory.Victim);
		}

		
		private void Dialogue()
		{
			try
			{
				string[] dialogueArrested = new string[]
				{
					"~b~You~s~: Ma'am, are you injured?",
					"~o~Victim~s~: Yes, only a few bruises but that's nothing new.",
					"~b~You~s~: Okay, is this your property?",
					"~o~Victim~s~: Thankfully it is, otherwise I'd be homeless tonight",
					"~b~You~s~: I assume you want to press charges?",
					"~o~Victim~s~: Yes, and how do I get a restraining order?",
					"~b~You~s~: You'll need to go to the courthouse and get the necessary forms.",
					"~o~Victim~s~: Thank you for helping me.",
					"~b~You~s~: No problem, here is my card if you have any questions or need any help.",
					"~o~Victim~s~: Thanks, one more thing, how long will he be in jail?",
					"~b~You~s~: I don't know exactly how long, but it's gonna be long.",
					"~o~Victim~s~: Good, he's an ex-convict so they'll be harder on him.",
					"~b~You~s~: I'm gonna have to process him, other officers will help you further.",
					"~m~dialogue ended"
				};
				string[] dialogueDeceased = new string[]
				{
					"~b~You~s~: Ma'am, are you hurt?",
					"~o~Victim~s~: Uh, yes I think so...",
					"~b~You~s~: Okay, is this property yours?",
					"~o~Victim~s~: Yes it is.",
					"~b~You~s~: Okay, this is now a crime scene, it will take some time before you enter your house again.",
					"~o~Victim~s~: Oh, what about the blood?",
					"~b~You~s~: That will be taken care of by crime scene cleaners.",
					"~o~Victim~s~: Okay, that's good",
					"~b~You~s~: Here is my card if you have any questions or need any help.",
					"~o~Victim~s~: Thanks.",
					"~b~You~s~: No problem, I'm gonna have to do some more things, other officers will help you further.",
					"~m~dialogue ended"
				};
				int line = 0;
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (this.Victim.IsAlive && (this.Suspect.IsDead || this.Suspect.IsCuffed))
						{
							if (!this.DialogueStarted && !this.FirstTime)
							{
								GameFiber.Sleep(5000);
								Game.DisplaySubtitle("Speak to the ~o~victim", 10000);
								this.FirstTime = true;
							}
							if (Helper.MainPlayer.Position.DistanceTo(this.Victim.Position) < 3f && this.FirstTime)
							{
								if (Game.IsKeyDown(Settings.InteractKey) || (Game.IsControllerButtonDown(Settings.ControllerInteractKey) && Settings.AllowController && UIMenu.IsUsingController))
								{
									if (!this.DialogueStarted)
									{
										if (!Functions.IsPedKneelingTaskActive(this.Victim))
										{
											this.Victim.Tasks.Clear();
										}
										Game.LogTrivial("[Emergency Callouts]: Dialogue started with " + this.VictimPersona.FullName);
									}
									this.DialogueStarted = true;
									if (!Functions.IsPedKneelingTaskActive(this.Victim))
									{
										this.Victim.Tasks.AchieveHeading(Helper.MainPlayer.Heading - 180f);
									}
									if (this.Suspect.IsCuffed)
									{
										Game.DisplaySubtitle(dialogueArrested[line], 15000);
										Game.LogTrivial("[Emergency Callouts]: Displayed dialogue line " + line.ToString());
										int i = line;
										line = i + 1;
										if (line == dialogueArrested.Length)
										{
											Game.LogTrivial("[Emergency Callouts]: Dialogue Ended");
											foreach (Ped ped in World.GetAllPeds())
											{
												if (Functions.IsPedACop(ped) && ped.IsAlive && this.Victim.Position.DistanceTo(ped.Position) <= 10f && ped != Helper.MainPlayer)
												{
													this.Victim.Tasks.GoStraightToPosition(ped.Position, 1f, 1f, 0f, 0);
												}
											}
											GameFiber.Sleep(3000);
											Helper.Handle.AdvancedEndingSequence();
											return;
										}
									}
									else if (this.Suspect.IsDead)
									{
										Game.DisplaySubtitle(dialogueDeceased[line], 15000);
										Game.LogTrivial("[Emergency Callouts]: Displayed dialogue line " + line.ToString());
										int i = line;
										line = i + 1;
										if (line == dialogueDeceased.Length)
										{
											Game.LogTrivial("[Emergency Callouts]: Dialogue Ended");
											foreach (Ped ped2 in World.GetAllPeds())
											{
												if (Functions.IsPedACop(ped2) && ped2.IsAlive && this.Victim.Position.DistanceTo(ped2.Position) <= 10f && ped2 != Helper.MainPlayer)
												{
													this.Victim.Tasks.GoToOffsetFromEntity(ped2, 2f, 0f, 1f);
												}
											}
											GameFiber.Sleep(5000);
											Helper.Handle.AdvancedEndingSequence();
											return;
										}
									}
									if (line == 9)
									{
										Helper.MainPlayer.Tasks.GoToOffsetFromEntity(this.Victim, 1f, 0f, 2f);
										this.Victim.Tasks.ClearImmediately();
										this.Victim.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, 48);
										Helper.MainPlayer.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, 48);
									}
								}
								else if (!this.DialogueStarted && Helper.MainPlayer.Position.DistanceTo(this.Victim.Position) <= 2f)
								{
									if (Settings.AllowController && UIMenu.IsUsingController)
									{
										Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.ControllerInteractKey) + "~ to talk to the ~o~victim");
									}
									else
									{
										Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.InteractKey) + "~ to talk to the ~o~victim");
									}
								}
							}
						}
						else if (this.Victim.IsDead)
						{
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

		
		private void Scenario1()
		{
			try
			{
				this.RetrieveFightPosition();
				this.Victim.IsInvincible = true;
				this.Victim.Tasks.Cower(-1);
				this.Suspect.Tasks.FightAgainst(this.Victim);
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 10f && EntityExtensions.Exists(this.Suspect) && this.PlayerArrived)
						{
							this.Victim.IsInvincible = false;
							this.Victim.Tasks.Cower(-1);
							return;
						}
					}
				});
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
				this.RetrieveFightPosition();
				GameFiber.Sleep(100);
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(2f);
				this.Suspect.GiveRandomHandgun(-1, true);
				this.Suspect.Tasks.AimWeaponAt(this.Victim, -1);
				this.Victim.Tasks.Cower(-1);
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 15f && this.PlayerArrived)
						{
							Game.DisplaySubtitle("~r~Suspect~s~: YOU SHOULD HAVE NEVER DONE THIS!", 5000);
							IL_96:
							while (this.CalloutActive)
							{
								GameFiber.Yield();
								if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 10f && this.PlayerArrived)
								{
									this.Suspect.Tasks.FightAgainst(Helper.MainPlayer);
									return;
								}
							}
							return;
						}
					}
					goto IL_96;
				});
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
				this.RetrieveFightPosition();
				this.Suspect.Position = this.Victim.GetOffsetPositionFront(2f);
				this.Victim.Kill();
				this.Suspect.GiveRandomHandgun(0, true);
				this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("amb@code_human_cower@male@base"), "base", -1, 3.2f, -3f, 0f, 1);
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 10f && this.PlayerArrived)
						{
							Game.DisplaySubtitle("~r~Suspect~s~: WHAT THE HELL DID I DO!?");
							GameFiber.Sleep(3000);
							this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("mp_suicide"), "pistol", 4f, 0);
							GameFiber.Sleep(700);
							if (this.Suspect.IsAlive && EntityExtensions.Exists(this.Suspect))
							{
								this.Suspect.Kill();
							}
							string text = "lspdfr\\audio\\scanner\\Emergency Callouts Audio\\GUNSHOT.wav";
							SoundPlayer soundPlayer = new SoundPlayer(text);
							if (File.Exists(text))
							{
								soundPlayer.Load();
								soundPlayer.Play();
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
				Helper.Handle.PreventDistanceCrash(base.CalloutPosition, this.PlayerArrived, this.PedFound);
				Helper.Handle.PreventPickupCrash(this.Suspect, this.Victim);
				if (Settings.AllowController)
				{
					if (DomesticViolence.<>o__43.<>p__0 == null)
					{
						DomesticViolence.<>o__43.<>p__0 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xFE99B66D079CF6BC", null, typeof(DomesticViolence), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					DomesticViolence.<>o__43.<>p__0.Target(DomesticViolence.<>o__43.<>p__0, NativeFunction.Natives, 0, 27, true);
				}
				if (Helper.MainPlayer.Position.DistanceTo(base.CalloutPosition) <= 200f && !this.WithinRange)
				{
					this.WithinRange = true;
					Helper.Handle.DeleteNearbyPeds(this.Suspect, this.Victim, 40f);
					Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " is within 200 meters");
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.Entrance) < 15f && !this.PlayerArrived)
				{
					this.PlayerArrived = true;
					Helper.Handle.BlockPermanentEventsRadius(this.Center, 200f);
					Game.DisplaySubtitle("Find the ~o~victim~s~ and the ~r~suspect~s~ in the ~y~area~s~.", 10000);
					if (EntityExtensions.Exists(this.EntranceBlip))
					{
						this.EntranceBlip.Delete();
					}
					this.SearchArea = new Blip(this.Suspect.Position.Around2D(30f), (float)Settings.SearchAreaSize);
					this.SearchArea.SetColorYellow();
					this.SearchArea.Alpha = 0.5f;
					Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " has arrived on scene");
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 5f && !this.PedFound && this.PlayerArrived && EntityExtensions.Exists(this.Suspect))
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
				if (Helper.MainPlayer.Position.DistanceTo(this.Victim.Position) < 5f && !this.Ped2Found && this.PlayerArrived && EntityExtensions.Exists(this.Victim))
				{
					this.Ped2Found = true;
					Helper.Display.HideSubtitle();
					if (EntityExtensions.Exists(this.VictimBlip))
					{
						this.VictimBlip.Alpha = 1f;
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
						this.VictimPersona.FullName,
						" (Victim)"
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
					}
					Game.LogTrivial("[Emergency Callouts]: Deleted SuspectBlip");
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
			if (EntityExtensions.Exists(this.Victim))
			{
				this.Victim.Dismiss();
			}
			if (EntityExtensions.Exists(this.SuspectBlip))
			{
				this.SuspectBlip.Delete();
			}
			if (EntityExtensions.Exists(this.VictimBlip))
			{
				this.VictimBlip.Delete();
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

		
		public DomesticViolence()
		{
			float[] array = new float[3];
			array[0] = 186f;
			this.SandyShoresFightHeadings = array;
			this.GrapeseedFightPositions = new Vector3[]
			{
				new Vector3(1684.434f, 4692.222f, 43.00724f),
				new Vector3(1661.172f, 4688.735f, 43.20671f),
				new Vector3(1673.626f, 4680.47f, 43.05536f)
			};
			this.GrapeseedFightHeadings = new float[]
			{
				169.98f,
				180.06f,
				272.81f
			};
			this.PaletoBayFightPositions = new Vector3[]
			{
				new Vector3(-388.4336f, 6255.619f, 31.48756f),
				new Vector3(-374.013f, 6243.474f, 31.48722f),
				new Vector3(-374.2228f, 6259.589f, 31.48723f)
			};
			this.PaletoBayFightHeadings = new float[]
			{
				301.78f,
				144.9f,
				132.13f
			};
			base..ctor();
		}

		
		private bool CalloutActive;

		
		private bool PlayerArrived;

		
		private bool PedFound;

		
		private bool Ped2Found;

		
		private bool PedDetained;

		
		private bool DialogueStarted;

		
		private bool FirstTime;

		
		private bool WithinRange;

		
		private Vector3 Entrance;

		
		private Vector3 Center;

		
		private readonly Vector3[] CalloutPositions = new Vector3[]
		{
			new Vector3(11.3652f, 545.7453f, 175.8412f),
			new Vector3(222.883f, -1726.32f, 28.87364f),
			new Vector3(-1048.924f, -1018.362f, 2.150359f),
			new Vector3(1504.92f, 2203.887f, 79.99944f),
			new Vector3(224.5887f, 3162.886f, 42.3335f),
			new Vector3(1687.845f, 4680.918f, 43.02761f),
			new Vector3(-394.975f, 6276.961f, 29.67487f)
		};

		
		private readonly Vector3[] VinewoodHillsFightPositions = new Vector3[]
		{
			new Vector3(24.13852f, 520.5587f, 170.2275f),
			new Vector3(-6.628098f, 509.4984f, 170.6278f)
		};

		
		private readonly float[] VinewoodHillsFightHeadings = new float[]
		{
			24.09f,
			61.64f
		};

		
		private readonly Vector3[] DavisFightPositions = new Vector3[]
		{
			new Vector3(203.894f, -1706.848f, 29.30457f),
			new Vector3(210.6721f, -1720.645f, 29.2917f)
		};

		
		private readonly float[] DavisFightHeadings = new float[]
		{
			310f,
			34f
		};

		
		private readonly Vector3 VespucciFightPosition = new Vector3(-1058.305f, -995.6418f, 6.410485f);

		
		private readonly float VespucciFightHeading = 205.96f;

		
		private readonly Vector3[] CountyFightPositions = new Vector3[]
		{
			new Vector3(1534.577f, 2228.416f, 77.69907f),
			new Vector3(1551.911f, 2228.493f, 77.83331f),
			new Vector3(1538.637f, 2238.759f, 77.69897f)
		};

		
		private readonly float[] CountyFightHeadings = new float[]
		{
			359.88f,
			3.25f,
			271.88f
		};

		
		private readonly Vector3[] SandyShoresFightPositions = new Vector3[]
		{
			new Vector3(245.5203f, 3169.379f, 42.8357f),
			new Vector3(264.1511f, 3176.024f, 42.52968f),
			new Vector3(250.4642f, 3192.325f, 43.07049f)
		};

		
		private readonly float[] SandyShoresFightHeadings;

		
		private readonly Vector3[] GrapeseedFightPositions;

		
		private readonly float[] GrapeseedFightHeadings;

		
		private readonly Vector3[] PaletoBayFightPositions;

		
		private readonly float[] PaletoBayFightHeadings;

		
		private Ped Suspect;

		
		private Ped Victim;

		
		private Persona SuspectPersona;

		
		private Persona VictimPersona;

		
		private Blip SuspectBlip;

		
		private Blip VictimBlip;

		
		private Blip EntranceBlip;

		
		private Blip SearchArea;
	}
}
