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
	
	[CalloutInfo("[EC] Suspicious Activity", 3)]
	public class SuspiciousActivity : Callout
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
			base.CalloutMessage = "Suspicious Activity";
			base.CalloutAdvisory = "Passersby called regarding a person acting suspicious.";
			Helper.CalloutScenario = Helper.random.Next(1, 4);
			Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT CRIME_SUSPICIOUS_ACTIVITY IN_OR_ON_POSITION", base.CalloutPosition);
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
					this.Center = new Vector3(-616.0434f, -1600.232f, 26.75098f);
					this.Entrance = new Vector3(-646.7701f, -1639.802f, 25.06787f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[1])
				{
					this.Center = new Vector3(-1283.511f, -811.2982f, 17.32025f);
					this.Entrance = new Vector3(-1364.522f, -709.0762f, 24.67615f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[2])
				{
					this.Center = new Vector3(597.8428f, 2796.708f, 41.99812f);
					this.Entrance = new Vector3(651.5822f, 2762.731f, 41.94574f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[3])
				{
					this.Center = new Vector3(1243.041f, -2395.421f, 47.91381f);
					this.Entrance = new Vector3(1115.294f, -2555.428f, 31.27009f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[4])
				{
					this.Center = new Vector3(737.6351f, 1285.04f, 359.7698f);
					this.Entrance = new Vector3(808.5509f, 1275.401f, 359.9711f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[5])
				{
					this.Center = new Vector3(2118.948f, 4802.422f, 41.19594f);
					this.Entrance = new Vector3(2165.78f, 4758.762f, 42f);
				}
				else if (base.CalloutPosition == this.CalloutPositions[6])
				{
					this.Center = new Vector3(1477.096f, 6343.949f, 22.35379f);
					this.Entrance = new Vector3(1485.026f, 6412.347f, 22.35379f);
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
				this.Suspect2 = new Ped(Helper.Entity.GetRandomMaleModel(), Vector3.Zero, 0f);
				this.Suspect2Persona = Functions.GetPersonaForPed(this.Suspect2);
				this.Suspect2.IsPersistent = true;
				this.Suspect2.BlockPermanentEvents = true;
				this.Suspect2Blip = this.Suspect2.AttachBlip();
				this.Suspect2Blip.SetColorRed();
				this.Suspect2Blip.Scale = (float)Settings.PedBlipScale;
				this.Suspect2Blip.Alpha = 0f;
				this.SuspectVehicle = new Vehicle(Helper.Vehicles.GetRandomSedan(), Vector3.Zero, 0f);
				this.SuspectVehicle.IsPersistent = true;
				this.vehDoors = this.SuspectVehicle.GetDoors();
				this.vehDoors[this.vehDoors.Length - 1].Open(false);
				this.Suspect2Vehicle = new Vehicle(Helper.Vehicles.GetRandomSedan(), Vector3.Zero, 0f);
				this.Suspect2Vehicle.IsPersistent = true;
				this.veh2Doors = this.Suspect2Vehicle.GetDoors();
				this.veh2Doors[this.veh2Doors.Length - 1].Open(false);
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

		
		private void RetrievePedPosition()
		{
			if (base.CalloutPosition == this.CalloutPositions[0])
			{
				this.Suspect.Position = this.LaPuertaSuspectPosition;
				this.Suspect.Heading = this.LaPuertaSuspectHeading;
				this.Suspect2.Position = this.LaPuertaSuspect2Position;
				this.Suspect2.Heading = this.LaPuertaSuspect2Heading;
				this.SuspectVehicle.Position = this.LaPuertaVehiclePosition;
				this.SuspectVehicle.Heading = this.LaPuertaVehicleHeading;
				this.Suspect2Vehicle.Position = this.LaPuertaVehicle2Position;
				this.Suspect2Vehicle.Heading = this.LaPuertaVehicle2Heading;
			}
			else if (base.CalloutPosition == this.CalloutPositions[1])
			{
				this.Suspect.Position = this.DelPerroSuspectPosition;
				this.Suspect.Heading = this.DelPerroSuspectHeading;
				this.Suspect2.Position = this.DelPerroSuspect2Position;
				this.Suspect2.Heading = this.DelPerroSuspect2Heading;
				this.SuspectVehicle.Position = this.DelPerroVehiclePosition;
				this.SuspectVehicle.Heading = this.DelPerroVehicleHeading;
				this.Suspect2Vehicle.Position = this.DelPerroVehicle2Position;
				this.Suspect2Vehicle.Heading = this.DelPerroVehicle2Heading;
			}
			else if (base.CalloutPosition == this.CalloutPositions[2])
			{
				this.Suspect.Position = this.HarmonySuspectPosition;
				this.Suspect.Heading = this.HarmonySuspectHeading;
				this.Suspect2.Position = this.HarmonySuspect2Position;
				this.Suspect2.Heading = this.HarmonySuspect2Heading;
				this.SuspectVehicle.Position = this.HarmonyVehiclePosition;
				this.SuspectVehicle.Heading = this.HarmonyVehicleHeading;
				this.Suspect2Vehicle.Position = this.HarmonyVehicle2Position;
				this.Suspect2Vehicle.Heading = this.HarmonyVehicle2Heading;
			}
			else if (base.CalloutPosition == this.CalloutPositions[3])
			{
				this.Suspect.Position = this.ElBurroSuspectPosition;
				this.Suspect.Heading = this.ElBurroSuspectHeading;
				this.Suspect2.Position = this.ElBurroSuspect2Position;
				this.Suspect2.Heading = this.ElBurroSuspect2Heading;
				this.SuspectVehicle.Position = this.ElBurroVehiclePosition;
				this.SuspectVehicle.Heading = this.ElBurroVehicleHeading;
				this.Suspect2Vehicle.Position = this.ElBurroVehicle2Position;
				this.Suspect2Vehicle.Heading = this.ElBurroVehicle2Heading;
			}
			else if (base.CalloutPosition == this.CalloutPositions[4])
			{
				this.Suspect.Position = this.CountySuspectPosition;
				this.Suspect.Heading = this.CountySuspectHeading;
				this.Suspect2.Position = this.CountySuspect2Position;
				this.Suspect2.Heading = this.CountySuspect2Heading;
				this.SuspectVehicle.Position = this.CountyVehiclePosition;
				this.SuspectVehicle.Heading = this.CountyVehicleHeading;
				this.Suspect2Vehicle.Position = this.CountyVehicle2Position;
				this.Suspect2Vehicle.Heading = this.CountyVehicle2Heading;
			}
			else if (base.CalloutPosition == this.CalloutPositions[5])
			{
				this.Suspect.Position = this.McKenzieFieldSuspectPosition;
				this.Suspect.Heading = this.McKenzieFieldSuspectHeading;
				this.Suspect2.Position = this.McKenzieFieldSuspect2Position;
				this.Suspect2.Heading = this.McKenzieFieldSuspect2Heading;
				this.SuspectVehicle.Position = this.McKenzieFieldVehiclePosition;
				this.SuspectVehicle.Heading = this.McKenzieFieldVehicleHeading;
				this.Suspect2Vehicle.Position = this.McKenzieFieldVehicle2Position;
				this.Suspect2Vehicle.Heading = this.McKenzieFieldVehicle2Heading;
			}
			else if (base.CalloutPosition == this.CalloutPositions[6])
			{
				this.Suspect.Position = this.PaletoBaySuspectPosition;
				this.Suspect.Heading = this.PaletoBaySuspectHeading;
				this.Suspect2.Position = this.PaletoBaySuspect2Position;
				this.Suspect2.Heading = this.PaletoBaySuspect2Heading;
				this.SuspectVehicle.Position = this.PaletoBayVehiclePosition;
				this.SuspectVehicle.Heading = this.PaletoBayVehicleHeading;
				this.Suspect2Vehicle.Position = this.PaletoBayVehicle2Position;
				this.Suspect2Vehicle.Heading = this.PaletoBayVehicle2Heading;
			}
			Helper.Log.Creation(this.Suspect, Helper.PedCategory.Suspect);
			Helper.Log.Creation(this.Suspect2, Helper.PedCategory.Suspect2);
			Helper.Log.Creation(this.SuspectVehicle, Helper.PedCategory.Suspect);
			Helper.Log.Creation(this.Suspect2Vehicle, Helper.PedCategory.Suspect2);
		}

		
		private void RetrieveFriendlyPosition()
		{
			if (base.CalloutPosition == this.CalloutPositions[0])
			{
				this.SuspectVehicle.Position = new Vector3(-624.1057f, -1641.472f, 25.53772f);
				this.SuspectVehicle.Heading = 238.88f;
			}
			else if (base.CalloutPosition == this.CalloutPositions[1])
			{
				this.SuspectVehicle.Position = new Vector3(-1269.257f, -823.7877f, 16.71213f);
				this.SuspectVehicle.Heading = 128.39f;
			}
			else if (base.CalloutPosition == this.CalloutPositions[2])
			{
				this.SuspectVehicle.Position = new Vector3(573.72f, 2796.019f, 41.69397f);
				this.SuspectVehicle.Heading = 279.09f;
			}
			else if (base.CalloutPosition == this.CalloutPositions[3])
			{
				this.SuspectVehicle.Position = new Vector3(1119.511f, -2379.631f, 30.521f);
				this.SuspectVehicle.Heading = 5.75f;
			}
			else if (base.CalloutPosition == this.CalloutPositions[4])
			{
				this.SuspectVehicle.Position = new Vector3(783.7767f, 1281.481f, 359.9094f);
				this.SuspectVehicle.Heading = 358.61f;
			}
			else if (base.CalloutPosition == this.CalloutPositions[5])
			{
				this.SuspectVehicle.Position = new Vector3(2148.915f, 4796.483f, 40.75656f);
				this.SuspectVehicle.Heading = 243.75f;
			}
			else if (base.CalloutPosition == this.CalloutPositions[6])
			{
				this.SuspectVehicle.Position = new Vector3(1430.501f, 6350.87f, 23.5983f);
				this.SuspectVehicle.Heading = 99.83f;
			}
			Helper.Log.Creation(this.Suspect, Helper.PedCategory.Suspect);
			Helper.Log.Creation(this.SuspectVehicle, Helper.PedCategory.Suspect);
			this.Suspect.Position = this.SuspectVehicle.GetOffsetPositionFront(-this.SuspectVehicle.Length + this.SuspectVehicle.Length / 2.85f);
			this.Suspect.Heading = this.SuspectVehicle.Heading;
		}

		
		private void Scenario1()
		{
			try
			{
				this.RetrievePedPosition();
				this.Suspect.GiveRandomAssaultRifle(-1, true);
				this.Suspect2.GiveRandomHandgun(-1, true);
				this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@machinery@weapon_test@"), "base_amy_skater_01", 5f, 1);
				this.Suspect2.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@peds@"), "amb_world_human_hang_out_street_male_c_base", 5f, 0);
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) < 25f && this.PlayerArrived)
						{
							if (EntityExtensions.Exists(this.Suspect2Blip))
							{
								this.Suspect2Blip.Delete();
							}
							this.Suspect.Tasks.FightAgainst(Helper.MainPlayer);
							this.Suspect2.Tasks.ClearImmediately();
							this.pursuit = Functions.CreatePursuit();
							Functions.AddPedToPursuit(this.pursuit, this.Suspect2);
							Functions.RequestBackup(this.Suspect2.Position, 2, 0);
							return;
						}
					}
				});
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if ((this.Suspect.IsDead || this.Suspect.IsCuffed) && EntityExtensions.Exists(this.Suspect))
						{
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
							Functions.SetPursuitIsActiveForPlayer(this.pursuit, true);
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
				this.vehDoors = this.SuspectVehicle.GetDoors();
				this.vehDoors[this.vehDoors.Length - 1].Close(false);
				this.RetrievePedPosition();
				Functions.SetPedResistanceChance(this.Suspect, 100f);
				this.Suspect.GiveRandomShotgun(-1, true);
				Functions.AddPedContraband(this.Suspect, 1, "Shotgun");
				this.Suspect2.Health = 110;
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 30f && this.PlayerArrived && EntityExtensions.Exists(this.Suspect))
						{
							if (this.Suspect.IsAlive)
							{
								this.Suspect.Tasks.FightAgainst(this.Suspect2);
							}
							IL_D3:
							while (this.CalloutActive)
							{
								GameFiber.Yield();
								if (this.Suspect.IsAlive && this.Suspect2.IsDead && EntityExtensions.Exists(this.Suspect) && EntityExtensions.Exists(this.Suspect2))
								{
									this.Suspect.Tasks.Clear();
									this.Suspect.Tasks.FightAgainst(Helper.MainPlayer);
									return;
								}
							}
							return;
						}
					}
					goto IL_D3;
				});
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (this.Suspect2.IsAlive && (this.Suspect.IsDead || this.Suspect.IsCuffed))
						{
							this.Suspect2.Tasks.PutHandsUp(-1, Helper.MainPlayer);
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
			try
			{
				this.RetrieveFriendlyPosition();
				this.SuspectBlip.SetColorYellow();
				if (EntityExtensions.Exists(this.Suspect2Blip))
				{
					this.Suspect2Blip.Delete();
				}
				if (EntityExtensions.Exists(this.Suspect2Vehicle))
				{
					this.Suspect2Vehicle.Delete();
				}
				this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@heists@box_carry@"), "idle", 4f, 49);
				if (SuspiciousActivity.<>o__89.<>p__1 == null)
				{
					SuspiciousActivity.<>o__89.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(SuspiciousActivity)));
				}
				Func<CallSite, object, int> target = SuspiciousActivity.<>o__89.<>p__1.Target;
				CallSite <>p__ = SuspiciousActivity.<>o__89.<>p__1;
				if (SuspiciousActivity.<>o__89.<>p__0 == null)
				{
					SuspiciousActivity.<>o__89.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
					{
						typeof(int)
					}, typeof(SuspiciousActivity), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				int num = target(<>p__, SuspiciousActivity.<>o__89.<>p__0.Target(SuspiciousActivity.<>o__89.<>p__0, NativeFunction.Natives, this.Suspect, 23553));
				if (SuspiciousActivity.<>o__89.<>p__2 == null)
				{
					SuspiciousActivity.<>o__89.<>p__2 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(SuspiciousActivity), new CSharpArgumentInfo[]
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
				SuspiciousActivity.<>o__89.<>p__2.Target(SuspiciousActivity.<>o__89.<>p__2, NativeFunction.Natives, this.Box, this.Suspect, num, 0f, 0.45f, 0f, 180f, 270f, 180f, true, true, false, false, 2, 1);
				string str = string.Empty;
				if (this.SuspectPersona.Gender == null)
				{
					str = "Sir";
				}
				else
				{
					str = "Ma'am";
				}
				string[] boxContents = new string[]
				{
					"a dozen magazines",
					"a pair of shoes",
					"printer ink cartridges",
					"PC hardware"
				};
				int randomContent = Helper.random.Next(boxContents.Length);
				string[] dialogueSuspect = new string[]
				{
					"~b~You~s~: Hello " + str + ", how are you doing today?",
					"~y~Suspect~s~: I'm doing okay, I just bought something from Craigslist, did I do something wrong?",
					"~b~You~s~: We got a call that a person is acting suspicious, you matched the description.",
					"~y~Suspect~s~: Yeah well this part here is kinda sketchy, I don't wanna get hurt here.",
					"~b~You~s~: I understand, what's in the box?",
					"~y~Suspect~s~: Oh, it's " + boxContents[randomContent] + ".",
					"~b~You~s~: Can I take a look?",
					"~y~Suspect~s~: Sure go ahead.",
					"~b~You~s~: Okay, I'm gonna check you in the system real quick and then you'll be free to go.",
					"~y~Suspect~s~: Okay."
				};
				int line = 0;
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (this.Suspect.IsAlive && EntityExtensions.Exists(this.Suspect))
						{
							if (Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 3f && Helper.MainPlayer.IsOnFoot && this.Suspect.IsAlive)
							{
								if (Game.IsKeyDown(Settings.InteractKey) || (Game.IsControllerButtonDown(Settings.ControllerInteractKey) && Settings.AllowController && UIMenu.IsUsingController))
								{
									if (!this.DialogueStarted)
									{
										Game.LogTrivial("[Emergency Callouts]: Dialogue started with " + this.SuspectPersona.FullName);
										this.Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@heists@box_carry@"), "idle", 4f, 49);
									}
									this.DialogueStarted = true;
									this.Suspect.Tasks.AchieveHeading(Helper.MainPlayer.Heading - 180f);
									int line;
									Game.DisplaySubtitle(dialogueSuspect[line], 15000);
									Game.LogTrivial("[Emergency Callouts]: Displayed dialogue line " + line.ToString() + 1.ToString());
									line = line;
									line++;
									if (line == 8)
									{
										GameFiber.Sleep(3000);
										Game.DisplayHelp("~y~Looking~s~...");
										GameFiber.Sleep(5000);
										Game.DisplayHelp("You found ~g~" + boxContents[randomContent] + "~s~.");
									}
									if (line == dialogueSuspect.Length)
									{
										Game.LogTrivial("[Emergency Callouts]: Dialogue Ended");
										Functions.SetPedAsStopped(this.Suspect, true);
										this.Suspect.Tasks.Clear();
										if (EntityExtensions.Exists(this.Box))
										{
											this.Box.Delete();
										}
										GameFiber.Sleep(3000);
										Helper.Handle.AdvancedEndingSequence();
										return;
									}
								}
								else if (!this.DialogueStarted && Helper.MainPlayer.Position.DistanceTo(this.Suspect.Position) <= 2f)
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
						else if (this.Suspect.IsDead)
						{
							break;
						}
					}
				});
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutActive)
					{
						GameFiber.Yield();
						if (this.Suspect.IsCuffed || this.Suspect.IsDead)
						{
							this.Suspect.Tasks.Clear();
							if (EntityExtensions.Exists(this.Box))
							{
								this.Box.Delete();
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
				Helper.Handle.PreventPickupCrash(this.Suspect, this.Suspect2);
				if (Helper.MainPlayer.Position.DistanceTo(base.CalloutPosition) <= 200f && !this.WithinRange)
				{
					this.WithinRange = true;
					Helper.Handle.DeleteNearbyTrailers(this.Entrance, 100f);
					Helper.Handle.DeleteNearbyPeds(this.Suspect, this.Suspect2, 40f);
					Game.LogTrivial("[Emergency Callouts]: " + Helper.PlayerPersona.FullName + " is within 200 meters");
				}
				if (Helper.MainPlayer.Position.DistanceTo(this.Entrance) < 15f && !this.PlayerArrived)
				{
					this.PlayerArrived = true;
					Helper.Handle.BlockPermanentEventsRadius(this.Center, 60f);
					Game.DisplaySubtitle("Find the ~r~suspect~s~ in the ~y~area~s~.", 10000);
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
				if (Helper.MainPlayer.Position.DistanceTo(this.Suspect2.Position) < 5f && !this.Ped2Found && this.PlayerArrived && EntityExtensions.Exists(this.Suspect2))
				{
					this.Ped2Found = true;
					Helper.Display.HideSubtitle();
					if (EntityExtensions.Exists(this.Suspect2Blip))
					{
						this.Suspect2Blip.Alpha = 1f;
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
						this.Suspect2Persona.FullName,
						" (Suspect2)"
					}));
				}
				if (Functions.IsPedStoppedByPlayer(this.Suspect) && !this.PedDetained && EntityExtensions.Exists(this.Suspect))
				{
					this.PedDetained = true;
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
			this.Suspect.Tasks.Clear();
			Functions.SetPedCantBeArrestedByPlayer(this.Suspect, false);
			if (EntityExtensions.Exists(this.Suspect))
			{
				this.Suspect.Dismiss();
			}
			if (EntityExtensions.Exists(this.Suspect2))
			{
				this.Suspect2.Dismiss();
			}
			if (EntityExtensions.Exists(this.SuspectVehicle))
			{
				this.SuspectVehicle.Dismiss();
			}
			if (EntityExtensions.Exists(this.Suspect2Vehicle))
			{
				this.Suspect2Vehicle.Dismiss();
			}
			if (EntityExtensions.Exists(this.SuspectBlip))
			{
				this.SuspectBlip.Delete();
			}
			if (EntityExtensions.Exists(this.Suspect2Blip))
			{
				this.Suspect2Blip.Delete();
			}
			if (EntityExtensions.Exists(this.SearchArea))
			{
				this.SearchArea.Delete();
			}
			if (EntityExtensions.Exists(this.EntranceBlip))
			{
				this.EntranceBlip.Delete();
			}
			if (EntityExtensions.Exists(this.Box))
			{
				this.Box.Delete();
			}
			Helper.Display.HideSubtitle();
			Helper.Display.EndNotification();
			Helper.Log.OnCalloutEnded(base.CalloutMessage, Helper.CalloutScenario);
		}

		
		private bool CalloutActive;

		
		private bool PlayerArrived;

		
		private bool PedFound;

		
		private bool Ped2Found;

		
		private bool PedDetained;

		
		private bool WithinRange;

		
		private bool DialogueStarted;

		
		private Vector3 Entrance;

		
		private Vector3 Center;

		
		private readonly Object Box = new Object(new Model("prop_cs_cardbox_01"), new Vector3(0f, 0f, 0f));

		
		private readonly Vector3[] CalloutPositions = new Vector3[]
		{
			new Vector3(-646.7701f, -1639.802f, 25.06787f),
			new Vector3(-1283.511f, -811.2982f, 17.32025f),
			new Vector3(651.5822f, 2762.731f, 41.94574f),
			new Vector3(1243.041f, -2395.421f, 47.91381f),
			new Vector3(808.5509f, 1275.401f, 359.9711f),
			new Vector3(2165.78f, 4758.762f, 42f),
			new Vector3(1485.026f, 6412.347f, 22.35379f)
		};

		
		private readonly Vector3 LaPuertaSuspectPosition = new Vector3(-587.6331f, -1587.74f, 26.75113f);

		
		private readonly float LaPuertaSuspectHeading = 14.97f;

		
		private readonly Vector3 LaPuertaSuspect2Position = new Vector3(-587.4804f, -1585.351f, 26.75113f);

		
		private readonly float LaPuertaSuspect2Heading = 163.78f;

		
		private readonly Vector3 LaPuertaVehiclePosition = new Vector3(-591.1713f, -1587.647f, 26.41216f);

		
		private readonly float LaPuertaVehicleHeading = 89.17f;

		
		private readonly Vector3 LaPuertaVehicle2Position = new Vector3(-590.5353f, -1584.49f, 26.44631f);

		
		private readonly float LaPuertaVehicle2Heading = 77.31f;

		
		private readonly Vector3 DelPerroSuspectPosition = new Vector3(-1260.832f, -826.6248f, 17.0973f);

		
		private readonly float DelPerroSuspectHeading = 25.81f;

		
		private readonly Vector3 DelPerroSuspect2Position = new Vector3(-1261.028f, -824.2448f, 17.09965f);

		
		private readonly float DelPerroSuspect2Heading = 179.82f;

		
		private readonly Vector3 DelPerroVehiclePosition = new Vector3(-1264.427f, -818.228f, 16.62829f);

		
		private readonly float DelPerroVehicleHeading = 35.55f;

		
		private readonly Vector3 DelPerroVehicle2Position = new Vector3(-1269.506f, -824.8015f, 16.2714f);

		
		private readonly float DelPerroVehicle2Heading = 129.07f;

		
		private readonly Vector3 HarmonySuspectPosition = new Vector3(604.8357f, 2789.544f, 42.1919f);

		
		private readonly float HarmonySuspectHeading = 4.81f;

		
		private readonly Vector3 HarmonySuspect2Position = new Vector3(604.5134f, 2792.431f, 42.14416f);

		
		private readonly float HarmonySuspect2Heading = 187.87f;

		
		private readonly Vector3 HarmonyVehiclePosition = new Vector3(606.4803f, 2791.021f, 41.69831f);

		
		private readonly float HarmonyVehicleHeading = 6.87f;

		
		private readonly Vector3 HarmonyVehicle2Position = new Vector3(602.7837f, 2790.826f, 41.7882f);

		
		private readonly float HarmonyVehicle2Heading = 7.75f;

		
		private readonly Vector3 ElBurroSuspectPosition = new Vector3(1228.299f, -2354.579f, 50.30099f);

		
		private readonly float ElBurroSuspectHeading = 233.1f;

		
		private readonly Vector3 ElBurroSuspect2Position = new Vector3(1230.999f, -2353.718f, 50.26699f);

		
		private readonly float ElBurroSuspect2Heading = 102.5f;

		
		private readonly Vector3 ElBurroVehiclePosition = new Vector3(1230.793f, -2357.722f, 49.84827f);

		
		private readonly float ElBurroVehicleHeading = 214.59f;

		
		private readonly Vector3 ElBurroVehicle2Position = new Vector3(1233.492f, -2355.017f, 49.81187f);

		
		private readonly float ElBurroVehicle2Heading = 242.13f;

		
		private readonly Vector3 CountySuspectPosition = new Vector3(745.3344f, 1305.453f, 360.2965f);

		
		private readonly float CountySuspectHeading = 110.52f;

		
		private readonly Vector3 CountySuspect2Position = new Vector3(741.4516f, 1304.321f, 360.2965f);

		
		private readonly float CountySuspect2Heading = 288.07f;

		
		private readonly Vector3 CountyVehiclePosition = new Vector3(745.2075f, 1301.47f, 359.9155f);

		
		private readonly float CountyVehicleHeading = 181.38f;

		
		private readonly Vector3 CountyVehicle2Position = new Vector3(740.5121f, 1300.163f, 359.916f);

		
		private readonly float CountyVehicle2Heading = 215f;

		
		private readonly Vector3 McKenzieFieldSuspectPosition = new Vector3(2142.929f, 4781.304f, 40.97033f);

		
		private readonly float McKenzieFieldSuspectHeading = 134.41f;

		
		private readonly Vector3 McKenzieFieldSuspect2Position = new Vector3(2141.337f, 4779.794f, 40.97033f);

		
		private readonly float McKenzieFieldSuspect2Heading = 305.29f;

		
		private readonly Vector3 McKenzieFieldVehiclePosition = new Vector3(2141.803f, 4784.83f, 40.50724f);

		
		private readonly float McKenzieFieldVehicleHeading = 26.35f;

		
		private readonly Vector3 McKenzieFieldVehicle2Position = new Vector3(2139.113f, 4781.735f, 40.38437f);

		
		private readonly float McKenzieFieldVehicle2Heading = 63.12f;

		
		private readonly Vector3 PaletoBaySuspectPosition = new Vector3(1534.92f, 6341.109f, 24.1971f);

		
		private readonly float PaletoBaySuspectHeading = 141.26f;

		
		private readonly Vector3 PaletoBaySuspect2Position = new Vector3(1533.391f, 6338.765f, 24.20876f);

		
		private readonly float PaletoBaySuspect2Heading = 333.21f;

		
		private readonly Vector3 PaletoBayVehiclePosition = new Vector3(1532.055f, 6342.446f, 23.83713f);

		
		private readonly float PaletoBayVehicleHeading = 57.85f;

		
		private readonly Vector3 PaletoBayVehicle2Position = new Vector3(1530.411f, 6339.453f, 23.86996f);

		
		private readonly float PaletoBayVehicle2Heading = 80.94f;

		
		private Vehicle SuspectVehicle;

		
		private Vehicle Suspect2Vehicle;

		
		private VehicleDoor[] vehDoors;

		
		private VehicleDoor[] veh2Doors;

		
		private Ped Suspect;

		
		private Ped Suspect2;

		
		private Persona SuspectPersona;

		
		private Persona Suspect2Persona;

		
		private Blip SuspectBlip;

		
		private Blip Suspect2Blip;

		
		private Blip EntranceBlip;

		
		private Blip SearchArea;

		
		private LHandle pursuit;
	}
}
