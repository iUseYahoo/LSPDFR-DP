using System;
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
	
	[CalloutInfo("[EC] Burglary", 3)]
	public class Burglary : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			base.CalloutPosition = new Vector3(0f, 0f, 3000f);
			foreach (Vector3 vector in this.CalloutPositions)
			{
				if (Vector3.Distance(Helper.MainPlayer.Position, vector) < Vector3.Distance(Helper.MainPlayer.Position, base.CalloutPosition))
				{
					base.CalloutPosition = vector;
					Helper.CalloutArea = World.GetStreetName(vector);
				}
			}
			base.ShowCalloutAreaBlipBeforeAccepting(base.CalloutPosition, (float)Settings.SearchAreaSize / 2.5f);
			base.CalloutMessage = "Burglary";
			base.CalloutAdvisory = "Reports of a person attempting to break into a building.";
			Helper.CalloutScenario = Helper.random.Next(1, 4);
			Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT CRIME_BURGLARY IN_OR_ON_POSITION", base.CalloutPosition);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override void OnCalloutDisplayed()
		{
			if (PluginChecker.IsCalloutInterfaceRunning)
			{
				CalloutInterfaceFunctions.SendCalloutDetails(this, "CODE-2-HIGH", "");
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
					this.Center = new Vector3(888.6841f, -625.1655f, 58.04898f);
					this.Entrance = new Vector3(916.261f, -623.7192f, 58.05202f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[1])
				{
					this.Center = new Vector3(-923.5932f, -1287.796f, 5.278366f);
					this.Entrance = new Vector3(-835.1504f, -1275.611f, 4.458926f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[2])
				{
					this.Center = new Vector3(1281.405f, -1710.742f, 55.05928f);
					this.Entrance = new Vector3(1300.166f, -1719.278f, 54.04285f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[3])
				{
					this.Center = new Vector3(-101.6556f, 1909.48f, 196.4986f);
					this.Entrance = new Vector3(-73.21523f, 1866.276f, 198.7027f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[4])
				{
					this.Center = new Vector3(2685.283f, 4256.731f, 45.41756f);
					this.Entrance = new Vector3(2652.853f, 4308.485f, 44.39388f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[5])
				{
					this.Center = new Vector3(1223.067f, 2719.288f, 38.00484f);
					this.Entrance = new Vector3(1207.165f, 2694.605f, 37.82369f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[6])
				{
					this.Center = new Vector3(126.4832f, 6640.071f, 31.81017f);
					this.Entrance = new Vector3(194.8364f, 6576.915f, 31.82028f);
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
				Functions.AddPedContraband(this.Suspect, 4, "Lockpick set");
				Functions.AddPedContraband(this.Suspect, 4, "Car window breaker");
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

		
		private void RetrievePedPositions()
		{
			if (base.CalloutPosition == this.CalloutPositions[0])
			{
				int num = Helper.random.Next(this.MirrorParkBreakInPositions.Length);
				this.Suspect.Position = this.MirrorParkBreakInPositions[num];
				this.Suspect.Heading = this.MirrorParkBreakInHeadings[num];
				this.DamagedProperty = this.MirrorParkBreakInPositions[num];
				this.DamagedPropertyHeading = this.MirrorParkBreakInHeadings[num];
			}
			else if (base.CalloutPosition == this.CalloutPositions[1])
			{
				int num2 = Helper.random.Next(this.LaPuertaBreakInPositions.Length);
				this.Suspect.Position = this.LaPuertaBreakInPositions[num2];
				this.Suspect.Heading = this.LaPuertaBreakInHeadings[num2];
				this.DamagedProperty = this.LaPuertaBreakInPositions[num2];
				this.DamagedPropertyHeading = this.LaPuertaBreakInHeadings[num2];
			}
			else if (base.CalloutPosition == this.CalloutPositions[2])
			{
				int num3 = Helper.random.Next(this.ElBurroBreakInPositions.Length);
				this.Suspect.Position = this.ElBurroBreakInPositions[num3];
				this.Suspect.Heading = this.ElBurroBreakInHeadings[num3];
				this.DamagedProperty = this.ElBurroBreakInPositions[num3];
				this.DamagedPropertyHeading = this.ElBurroBreakInHeadings[num3];
			}
			else if (base.CalloutPosition == this.CalloutPositions[3])
			{
				int num4 = Helper.random.Next(this.CountyBreakInPositions.Length);
				this.Suspect.Position = this.CountyBreakInPositions[num4];
				this.Suspect.Heading = this.CountyBreakInHeadings[num4];
				this.DamagedProperty = this.CountyBreakInPositions[num4];
				this.DamagedPropertyHeading = this.CountyBreakInHeadings[num4];
			}
			else if (base.CalloutPosition == this.CalloutPositions[4])
			{
				int num5 = Helper.random.Next(this.GrapeseedBreakInPositions.Length);
				this.Suspect.Position = this.GrapeseedBreakInPositions[num5];
				this.Suspect.Heading = this.GrapeseedBreakInHeadings[num5];
				this.DamagedProperty = this.GrapeseedBreakInPositions[num5];
				this.DamagedPropertyHeading = this.GrapeseedBreakInHeadings[num5];
			}
			else if (base.CalloutPosition == this.CalloutPositions[5])
			{
				int num6 = Helper.random.Next(this.HarmonyBreakInPositions.Length);
				this.Suspect.Position = this.HarmonyBreakInPositions[num6];
				this.Suspect.Heading = this.HarmonyBreakInHeadings[num6];
				this.DamagedProperty = this.HarmonyBreakInPositions[num6];
				this.DamagedPropertyHeading = this.HarmonyBreakInHeadings[num6];
			}
			else if (base.CalloutPosition == this.CalloutPositions[6])
			{
				int num7 = Helper.random.Next(this.PaletoBayBreakInPositions.Length);
				this.Suspect.Position = this.PaletoBayBreakInPositions[num7];
				this.Suspect.Heading = this.PaletoBayBreakInHeadings[num7];
				this.DamagedProperty = this.PaletoBayBreakInPositions[num7];
				this.DamagedPropertyHeading = this.PaletoBayBreakInHeadings[num7];
			}
			this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("mp_common_heist"), "pick_door", 5f, 1);
			Helper.Log.Creation(this.Suspect, Helper.PedCategory.Suspect);
		}

		
		private void Dialogue()
		{
			try
			{
				int line = 0;
				string[] array = new string[]
				{
					"So, why did you do it?",
					"Why would you do this?",
					"Why are you stealing from other people",
					"So... what's your reason?"
				};
				string[] array2 = new string[]
				{
					"For the money!",
					"Easy cash!",
					"My family man, we're broke!",
					"Child alimony sucks dude!",
					"Getting evicted tomorrow if I don't pay them right now.",
					"Hospital bills!"
				};
				string[] array3 = new string[]
				{
					"So you don't have a job?",
					"I'm assuming you don't have a job then?",
					"So no work for you?"
				};
				string[] array4 = new string[]
				{
					"Yeah... I don't",
					"Nope, nada!",
					"Nah, nobody wants me as an employee.",
					"Correct.",
					"That's right."
				};
				string[] array5 = new string[]
				{
					"You expect me to believe that?",
					"I don't believe a word of it.",
					"I don't buy it."
				};
				string[] array6 = new string[]
				{
					"Cops only want to hear what they want to hear right?",
					"Ofcourse not I'm messing with you.",
					"Yes sir.",
					"Yep.",
					"Maybe.",
					"Your choice.",
					"No.",
					"Not up to me isn't it?"
				};
				string[] array7 = new string[]
				{
					"I'm staying silent until I can speak to my lawyer.",
					"I want my attorney ASAP.",
					"I'm going to use my right to remain silent."
				};
				string[] array8 = new string[]
				{
					"No problem.",
					"Works for me.",
					"Perfect.",
					"Sure.",
					"Copy that...",
					"Okay.",
					"Great.",
					"Win-win situation."
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
					"~r~Suspect~s~: " + array2[num2],
					"~b~You~s~: " + array3[num3],
					"~r~Suspect~s~: " + array4[num4],
					"~b~You~s~: " + this.DamageLine,
					"~r~Suspect~s~: " + this.DamageLine2,
					"~b~You~s~: " + array5[num5],
					"~r~Suspect~s~: " + array6[num6],
					"~r~Suspect~s~: " + array7[num7],
					"~b~You~s~: " + array8[num8],
					"~m~dialogue ended"
				};
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (this.Suspect.IsCuffed && this.Suspect.IsAlive && this.CheckedForDamage)
						{
							if (!this.DialogueStarted && !this.FirstTime)
							{
								GameFiber.Sleep(3000);
								Game.DisplaySubtitle("Speak to the ~r~suspect", 10000);
								this.FirstTime = true;
							}
							if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 2f)
							{
								if ((Game.IsKeyDown(Settings.InteractKey) || (Game.IsControllerButtonDown(Settings.ControllerInteractKey) && Settings.AllowController && UIMenu.IsUsingController)) && this.FirstTime)
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
									line = line;
									line++;
									Game.LogTrivial("[Emergency Callouts]: Displayed dialogue line " + line.ToString());
									if (line == dialogue.Length)
									{
										Game.LogTrivial("[Emergency Callouts]: Dialogue Ended");
										Helper.Handle.AdvancedEndingSequence();
										this.DialogueEnded = true;
										return;
									}
								}
								else if (!this.DialogueStarted)
								{
									if (Settings.AllowController && UIMenu.IsUsingController)
									{
										Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.ControllerInteractKey) + "~ to talk to the ~r~suspect");
									}
									else
									{
										Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.InteractKey) + "~ to talk to the ~r~suspect");
									}
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

		
		private void CheckForDamage()
		{
			try
			{
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (this.Suspect.IsCuffed && this.Suspect.IsAlive && Helper.MainPlayer.Position.DistanceTo(base.CalloutPosition) <= 300f && EntityExtensions.Exists(this.Suspect))
						{
							GameFiber.Sleep(7500);
							Game.DisplaySubtitle("Inspect the ~p~door~s~ for any ~y~property damage", 10000);
							this.DamagedPropertyBlip = new Blip(this.DamagedProperty);
							this.DamagedPropertyBlip.SetColorPurple();
							this.DamagedPropertyBlip.Scale = 0.6f;
							this.DamagedPropertyBlip.Flash(500, -1);
							IL_75E:
							while (this.CalloutActive)
							{
								GameFiber.Yield();
								if (Helper.MainPlayer.Position.DistanceTo(this.DamagedProperty) <= 3f && !this.CheckedForDamage && this.Suspect.IsAlive && this.Suspect.IsCuffed && EntityExtensions.Exists(this.Suspect))
								{
									if (Settings.AllowController && UIMenu.IsUsingController)
									{
										Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.ControllerInteractKey) + "~ to look for any ~y~property damage");
									}
									else
									{
										Game.DisplayHelp("Press ~" + InstructionalKeyExtensions.GetInstructionalId(Settings.InteractKey) + "~ to look for any ~y~property damage");
									}
									if (Game.IsKeyDown(Settings.InteractKey) || (Game.IsControllerButtonDown(Settings.ControllerInteractKey) && Settings.AllowController && UIMenu.IsUsingController))
									{
										if (Functions.GetPlayerWalkStyle() == 8)
										{
											this.CopWalkStyle = true;
											Functions.SetPlayerWalkStyle(0);
										}
										Helper.MainPlayer.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@business@bgen@bgen_inspecting@"), "inspecting_high_idle_02_inspector", -1, 2f, -1f, 0f, 49);
										if (Burglary.<>o__47.<>p__1 == null)
										{
											Burglary.<>o__47.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(Burglary)));
										}
										Func<CallSite, object, int> target = Burglary.<>o__47.<>p__1.Target;
										CallSite <>p__ = Burglary.<>o__47.<>p__1;
										if (Burglary.<>o__47.<>p__0 == null)
										{
											Burglary.<>o__47.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
											{
												typeof(int)
											}, typeof(Burglary), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										int num = target(<>p__, Burglary.<>o__47.<>p__0.Target(Burglary.<>o__47.<>p__0, NativeFunction.Natives, Helper.MainPlayer, 60309));
										if (Burglary.<>o__47.<>p__2 == null)
										{
											Burglary.<>o__47.<>p__2 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(Burglary), new CSharpArgumentInfo[]
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
										Burglary.<>o__47.<>p__2.Target(Burglary.<>o__47.<>p__2, NativeFunction.Natives, this.Clipboard, Helper.MainPlayer, num, 0f, 0f, 0.009f, -90f, 0f, 0f, true, true, false, false, 2, 1);
										if (Burglary.<>o__47.<>p__4 == null)
										{
											Burglary.<>o__47.<>p__4 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(Burglary)));
										}
										Func<CallSite, object, int> target2 = Burglary.<>o__47.<>p__4.Target;
										CallSite <>p__2 = Burglary.<>o__47.<>p__4;
										if (Burglary.<>o__47.<>p__3 == null)
										{
											Burglary.<>o__47.<>p__3 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
											{
												typeof(int)
											}, typeof(Burglary), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										int num2 = target2(<>p__2, Burglary.<>o__47.<>p__3.Target(Burglary.<>o__47.<>p__3, NativeFunction.Natives, Helper.MainPlayer, 28422));
										if (Burglary.<>o__47.<>p__5 == null)
										{
											Burglary.<>o__47.<>p__5 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(Burglary), new CSharpArgumentInfo[]
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
										Burglary.<>o__47.<>p__5.Target(Burglary.<>o__47.<>p__5, NativeFunction.Natives, this.Pencil, Helper.MainPlayer, num2, 0f, 0f, 0f, 0f, 0f, 0f, true, true, false, false, 2, 1);
										if (Helper.random.Next(0, 101) <= Settings.ChanceOfPropertyDamage)
										{
											GameFiber.Sleep(15000);
											Game.DisplayHelp("You found ~r~damage~s~ on the ~p~door");
											GameFiber.Sleep(3000);
											Helper.MainPlayer.Tasks.Clear();
											if (EntityExtensions.Exists(this.Clipboard))
											{
												this.Clipboard.Delete();
											}
											if (EntityExtensions.Exists(this.Pencil))
											{
												this.Pencil.Delete();
											}
											if (EntityExtensions.Exists(this.DamagedPropertyBlip))
											{
												this.DamagedPropertyBlip.Delete();
											}
											Functions.SetPlayerWalkStyle(0);
											this.DamageLine = "Anyway, you also left some dagage behind.";
											this.DamageLine2 = "Bro that was already there when I came here!";
											this.CheckedForDamage = true;
										}
										else
										{
											GameFiber.Sleep(15000);
											Game.DisplayHelp("You found ~g~no damage~s~ on the ~p~door");
											GameFiber.Sleep(3000);
											Helper.MainPlayer.Tasks.Clear();
											GameFiber.Sleep(1000);
											if (EntityExtensions.Exists(this.Clipboard))
											{
												this.Clipboard.Delete();
											}
											if (EntityExtensions.Exists(this.Pencil))
											{
												this.Pencil.Delete();
											}
											if (EntityExtensions.Exists(this.DamagedPropertyBlip))
											{
												this.DamagedPropertyBlip.Delete();
											}
											if (this.CopWalkStyle)
											{
												Functions.SetPlayerWalkStyle(8);
											}
											this.DamageLine = "Luckily for you I didn't find any damage.";
											this.DamageLine2 = "Nah man I'm a pro, I don't leave anything behind.";
											this.CheckedForDamage = true;
										}
										this.Dialogue();
										return;
									}
								}
								else if (this.Suspect.IsDead)
								{
									break;
								}
							}
							return;
						}
						if (this.Suspect.IsCuffed && this.Suspect.IsAlive && Helper.MainPlayer.Position.DistanceTo(base.CalloutPosition) >= 100f)
						{
							Helper.Handle.AdvancedEndingSequence();
						}
					}
					goto IL_75E;
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
				this.CheckForDamage();
				this.RetrievePedPositions();
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 5f && EntityExtensions.Exists(this.Suspect) && this.PlayerArrived)
						{
							this.StopChecking = true;
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
							LHandle lhandle = Functions.CreatePursuit();
							Functions.AddPedToPursuit(lhandle, this.Suspect);
							Functions.SetPursuitIsActiveForPlayer(lhandle, true);
							Helper.Play.PursuitAudio();
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
				this.RetrievePedPositions();
				this.CheckForDamage();
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 10f && EntityExtensions.Exists(this.Suspect) && this.PlayerArrived)
						{
							this.Suspect.Tasks.ClearImmediately();
							this.Suspect.Tasks.GoStraightToPosition(Helper.MainPlayer.Position, 1f, Helper.MainPlayer.Heading - 180f, 0f, 30);
							GameFiber.Sleep(30);
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

		
		private void Scenario3()
		{
			this.RetrievePedPositions();
			this.CheckForDamage();
			GameFiber.StartNew(delegate()
			{
				while (this.CalloutActive)
				{
					GameFiber.Yield();
					if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 10f && EntityExtensions.Exists(this.Suspect) && this.PlayerArrived)
					{
						this.Suspect.Tasks.ClearImmediately();
						this.Suspect.Tasks.GoStraightToPosition(Helper.MainPlayer.Position, 1f, Helper.MainPlayer.Heading - 180f, 0f, 30);
						this.Suspect.Tasks.AchieveHeading(Helper.MainPlayer.Heading - 180f);
						GameFiber.Sleep(1000);
						this.Suspect.GiveRandomHandgun(-1, true);
						this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("amb@code_human_cower@male@base"), "base", -1, 3.2f, -3f, 0f, 1);
						return;
					}
				}
			});
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
					if (Burglary.<>o__51.<>p__0 == null)
					{
						Burglary.<>o__51.<>p__0 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xFE99B66D079CF6BC", null, typeof(Burglary), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Burglary.<>o__51.<>p__0.Target(Burglary.<>o__51.<>p__0, NativeFunction.Natives, 0, 27, true);
				}
				if (Helper.MainPlayer.Position.DistanceTo(base.CalloutPosition) <= 200f && !this.WithinRange)
				{
					this.WithinRange = true;
					Helper.Handle.DeleteNearbyPeds(this.Suspect, 40f);
					Helper.Handle.DeleteNearbyTrailers(this.Center, 40f);
					Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " is within 200 meters");
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.Entrance) < 15f && !this.PlayerArrived)
				{
					this.PlayerArrived = true;
					Game.DisplaySubtitle("Find the ~r~burglar~s~ in the ~y~area~s~.", 10000);
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
			if (EntityExtensions.Exists(this.DamagedPropertyBlip))
			{
				this.DamagedPropertyBlip.Delete();
			}
			if (EntityExtensions.Exists(this.Clipboard))
			{
				this.Clipboard.Delete();
			}
			if (EntityExtensions.Exists(this.Pencil))
			{
				this.Pencil.Delete();
			}
			Helper.Display.HideSubtitle();
			Helper.Display.EndNotification();
			Helper.Log.OnCalloutEnded(base.CalloutMessage, Helper.CalloutScenario);
		}

		
		private bool CalloutActive;

		
		private bool PlayerArrived;

		
		private bool PedFound;

		
		private bool PedDetained;

		
		private bool StopChecking;

		
		private bool WithinRange;

		
		private bool FirstTime;

		
		private bool DialogueStarted;

		
		private bool DialogueEnded;

		
		private bool CheckedForDamage;

		
		private bool CopWalkStyle;

		
		private string DamageLine;

		
		private string DamageLine2;

		
		private float DamagedPropertyHeading;

		
		private Vector3 Entrance;

		
		private Vector3 Center;

		
		private Vector3 DamagedProperty;

		
		private readonly Object Clipboard = new Object(new Model("p_amb_clipboard_01"), new Vector3(0f, 0f, 0f));

		
		private readonly Object Pencil = new Object(new Model("prop_pencil_01"), new Vector3(0f, 0f, 0f));

		
		private Ped Suspect;

		
		private Persona SuspectPersona;

		
		private Blip SuspectBlip;

		
		private Blip EntranceBlip;

		
		private Blip SearchArea;

		
		private Blip DamagedPropertyBlip;

		
		private readonly Vector3[] CalloutPositions = new Vector3[]
		{
			new Vector3(916.261f, -623.7192f, 58.05202f),
			new Vector3(-835.1504f, -1275.611f, 4.45892f),
			new Vector3(1300.166f, -1719.278f, 54.04285f),
			new Vector3(-73.21523f, 1866.276f, 198.7027f),
			new Vector3(2652.853f, 4308.485f, 44.39388f),
			new Vector3(1207.165f, 2694.605f, 37.82369f),
			new Vector3(194.8364f, 6576.915f, 31.82028f)
		};

		
		private readonly Vector3[] MirrorParkBreakInPositions = new Vector3[]
		{
			new Vector3(880.1386f, -610.4592f, 58.44222f),
			new Vector3(905.5065f, -632.9874f, 58.04898f),
			new Vector3(869.7964f, -607.5421f, 58.21951f)
		};

		
		private readonly float[] MirrorParkBreakInHeadings = new float[]
		{
			313.57f,
			212f,
			39.6f
		};

		
		private readonly Vector3[] LaPuertaBreakInPositions = new Vector3[]
		{
			new Vector3(-911.7646f, -1269.634f, 5.22196f),
			new Vector3(-880.3901f, -1300.779f, 6.200158f),
			new Vector3(-914.1393f, -1312.992f, 6.200161f),
			new Vector3(-925.3542f, -1307.262f, 6.200159f)
		};

		
		private readonly float[] LaPuertaBreakInHeadings = new float[]
		{
			285.27f,
			113.51f,
			112.19f,
			205.55f
		};

		
		private readonly Vector3[] ElBurroBreakInPositions = new Vector3[]
		{
			new Vector3(1283.446f, -1699.925f, 55.47572f),
			new Vector3(1295.854f, -1697.502f, 55.07866f),
			new Vector3(1267.995f, -1713.858f, 54.65507f)
		};

		
		private readonly float[] ElBurroBreakInHeadings = new float[]
		{
			175.15f,
			285.86f,
			313.77f
		};

		
		private readonly Vector3[] CountyBreakInPositions = new Vector3[]
		{
			new Vector3(-50.24876f, 1910.59f, 195.7051f),
			new Vector3(-46.04625f, 1918.016f, 195.7053f),
			new Vector3(-30.14269f, 1942.518f, 190.1862f),
			new Vector3(-34.91757f, 1950.415f, 190.5546f),
			new Vector3(-47.10833f, 1946.867f, 190.5557f),
			new Vector3(-43.29532f, 1960.134f, 190.3533f)
		};

		
		private readonly float[] CountyBreakInHeadings = new float[]
		{
			271.3f,
			188.5f,
			296.15f,
			116.86f,
			33.96f,
			203.01f
		};

		
		private readonly Vector3[] GrapeseedBreakInPositions = new Vector3[]
		{
			new Vector3(2641.462f, 4235.202f, 45.49297f),
			new Vector3(2709.033f, 4316.569f, 46.15852f),
			new Vector3(2736.02f, 4279.527f, 48.49361f)
		};

		
		private readonly float[] GrapeseedBreakInHeadings = new float[]
		{
			51f,
			83f,
			283.19f
		};

		
		private readonly Vector3[] HarmonyBreakInPositions = new Vector3[]
		{
			new Vector3(1194.485f, 2721.754f, 38.81226f),
			new Vector3(1233.377f, 2737.641f, 38.0054f),
			new Vector3(1258.049f, 2740.197f, 38.70864f)
		};

		
		private readonly float[] HarmonyBreakInHeadings = new float[]
		{
			209.57f,
			86.06f,
			342.01f
		};

		
		private readonly Vector3[] PaletoBayBreakInPositions = new Vector3[]
		{
			new Vector3(125.4187f, 6643.836f, 31.79918f),
			new Vector3(174.3788f, 6642.977f, 31.57312f),
			new Vector3(156.7488f, 6657.068f, 31.56969f)
		};

		
		private readonly float[] PaletoBayBreakInHeadings = new float[]
		{
			231.12f,
			138.59f,
			216.67f
		};
	}
}
