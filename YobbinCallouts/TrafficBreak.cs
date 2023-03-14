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
	
	[CalloutInfo("Traffic Break", 3)]
	public class TrafficBreak : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			bool flag = this.Zone == "Davis" || this.Zone == "AirP" || this.Zone == "Stad" || this.Zone == "STRAW" || this.Zone == "Banning" || this.Zone == "RANCHO" || this.Zone == "ChamH" || this.Zone == "PBOX" || this.Zone == "LegSqu" || this.Zone == "SKID" || this.Zone == "TEXTI";
			if (flag)
			{
				this.MainSpawnPoint = this.Strawberry;
				this.Accident = this.StrawberryAccident;
			}
			else
			{
				bool flag2 = this.Zone == "Cypre" || this.Zone == "Murri" || this.Zone == "EBuro" || this.Zone == "LMesa" || this.Zone == "Mirr" || this.Zone == "East_V";
				if (flag2)
				{
					this.MainSpawnPoint = this.Strawberry;
					this.Accident = this.StrawberryAccident;
				}
				else
				{
					bool flag3 = this.Zone == "Vesp" || this.Zone == "VCana" || this.Zone == "Beach" || this.Zone == "DelSol" || this.Zone == "Koreat";
					if (flag3)
					{
						this.MainSpawnPoint = this.LittleSoul;
						this.Accident = this.LittleSoulAccident;
					}
					else
					{
						bool flag4 = this.Zone == "DeLBe" || this.Zone == "DelPe" || this.Zone == "Morn" || this.Zone == "PBluff" || this.Zone == "Movie";
						if (flag4)
						{
							this.MainSpawnPoint = this.Beach;
							this.Accident = this.BeachAccident;
						}
						else
						{
							bool flag5 = this.Zone == "Rockf" || this.Zone == "Burton" || this.Zone == "Richm" || this.Zone == "Golf";
							if (flag5)
							{
								this.MainSpawnPoint = this.Vinewood;
								this.Accident = this.VinewoodAccident;
							}
							else
							{
								bool flag6 = this.Zone == "CHIL" || this.Zone == "Vine" || this.Zone == "DTVine" || this.Zone == "WVine" || this.Zone == "Alta" || this.Zone == "Hawick";
								if (flag6)
								{
									this.MainSpawnPoint = this.Vinewood;
									this.Accident = this.VinewoodAccident;
								}
								else
								{
									bool flag7 = this.Zone == "Sandy" || this.Zone == "GrapeS" || this.Zone == "Desrt";
									if (flag7)
									{
										this.MainSpawnPoint = this.Desert;
										this.Accident = this.DesertAccident;
									}
									else
									{
										bool flag8 = this.Zone == "ProcoB" || this.Zone == "PalFor" || this.Zone == "Paleto" || this.Zone == "MTChil";
										if (flag8)
										{
											this.MainSpawnPoint = this.Chiliad;
											this.Accident = this.ChiliadAccident;
										}
										else
										{
											bool flag9 = this.Zone == "Tatamo";
											if (flag9)
											{
												this.MainSpawnPoint = this.Tatamo;
												this.Accident = this.TatamoAccident;
											}
											else
											{
												bool flag10 = this.Zone == "PalHigh";
												if (flag10)
												{
													this.MainSpawnPoint = this.Palomino;
													this.Accident = this.PalominoAccident;
												}
												else
												{
													bool flag11 = this.Zone == "Termina" || this.Zone == "Elysian";
													if (flag11)
													{
														this.MainSpawnPoint = this.Docks;
														this.Accident = this.DocksAccident;
													}
													else
													{
														Game.LogTrivial("YOBBINCALLOUTS: Player is not near any freeway. Choosing Another Random Location.");
														this.MainSpawnPoint = this.Desert;
														this.Accident = this.DesertAccident;
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
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 100f);
			base.AddMinimumDistanceCheck(150f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("WE_HAVE_01 CRIME_MOTOR_VEHICLE_ACCIDENT_02");
			base.CalloutMessage = "Traffic Break";
			base.CalloutPosition = this.MainSpawnPoint;
			base.CalloutAdvisory = "Perform a ~y~Traffic Break~w~ Following an Accident on the Freeway.";
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Traffic Break Callout Accepted by User");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~b~Code 2.~w~");
			}
			this.Area = new Blip(this.MainSpawnPoint, 50f);
			this.Area.Color = Color.Yellow;
			this.Area.Alpha = 0.67f;
			this.Area.IsRouteEnabled = true;
			this.Area.Name = "Traffic Break Start";
			Random r = new Random();
			int Scenario = r.Next(0, 0);
			int num = Scenario;
			int num2 = num;
			if (num2 == 0)
			{
				this.MainScenario = 0;
				Game.LogTrivial("YOBBINCALLOUTS: Traffic Break Scenario 0 Chosen");
				if (TrafficBreak.<>o__34.<>p__0 == null)
				{
					TrafficBreak.<>o__34.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(TrafficBreak), new CSharpArgumentInfo[]
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
				TrafficBreak.<>o__34.<>p__0.Target(TrafficBreak.<>o__34.<>p__0, NativeFunction.Natives, this.Accident, ref nodePosition, ref heading, 1, 3f, 0);
				CallHandler.SpawnVehicle(this.Accident, heading, true);
				this.AccidentVehicle.IsPersistent = true;
				this.AccidentVehicle.EngineHealth = 0f;
				this.AccidentVehicle.IsDeformationEnabled = true;
				this.AccidentVehicle.Deform(this.AccidentVehicle.GetPositionOffset(this.AccidentVehicle.GetBonePosition("door_dside_f")), 100f, 700f);
				this.AccidentVehicle.IsDriveable = false;
				Game.LogTrivial("YOBBINCALLOUTS: Accident Vehicle Vehicle Spawned");
				this.Ambulance = new Vehicle("AMBULANCE", this.AccidentVehicle.GetOffsetPositionFront(10f), this.AccidentVehicle.Heading);
				this.Ambulance.IsPersistent = true;
				this.Ambulance.IndicatorLightsStatus = 3;
				this.TowTruck = new Vehicle("TOWTRUCK", this.AccidentVehicle.GetOffsetPositionFront(-10f), this.AccidentVehicle.Heading);
				this.TowTruck.IsPersistent = true;
				this.TowTruck.IndicatorLightsStatus = 3;
				this.Paramedic = new Ped("s_m_m_paramedic_01", this.Ambulance.GetOffsetPositionFront(-7f), this.Ambulance.Heading - 180f);
				this.Paramedic.BlockPermanentEvents = true;
				this.Paramedic.IsPersistent = true;
				this.Paramedic.Tasks.PlayAnimation("amb@medic@standing@timeofdeath@base", "base", -0.5f, 1);
				this.Mechanic = new Ped("s_m_m_trucker_01", this.AccidentVehicle.GetOffsetPositionRight(-1.5f), this.AccidentVehicle.Heading - 90f);
				this.Mechanic.BlockPermanentEvents = true;
				this.Mechanic.IsPersistent = true;
				this.Mechanic.Tasks.PlayAnimation("mini@repair", "fixing_a_ped", -1f, 1);
			}
			bool flag = !this.CalloutRunning;
			if (flag)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Traffic Break Callout Not Accepted by User.");
			Functions.PlayScannerAudio("OTHER_UNIT_TAKING_CALL_01");
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
						bool flag = this.MainScenario == 0;
						if (flag)
						{
							while (Game.LocalPlayer.Character.DistanceTo(this.MainSpawnPoint) >= 50f && !Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
							bool flag2 = Game.IsKeyDown(Config.CalloutEndKey);
							if (flag2)
							{
								EndCalloutHandler.CalloutForcedEnd = true;
								goto IL_222;
							}
							Game.LogTrivial("YOBBINCALLOUTS: Player is On Scene.");
							Game.DisplaySubtitle("Press " + Config.MainInteractionKey.ToString() + " to ~g~Start~w~ the Traffic Break.", 2000);
							bool displayHelp = Config.DisplayHelp;
							if (displayHelp)
							{
								CallHandler.Dialogue(this.Instructions, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
							else
							{
								while (!Game.IsKeyDown(Config.MainInteractionKey))
								{
									GameFiber.Wait(0);
								}
							}
							GameFiber.Wait(2500);
							Game.DisplayNotification("Dispatch, We are Starting the ~y~Traffic Break.");
							this.AccidentBlip = new Blip(this.Accident, 25f);
							this.AccidentBlip.Color = Color.Yellow;
							this.AccidentBlip.IsRouteEnabled = true;
							this.AccidentBlip.Name = "Accident";
							this.AccidentBlip.Alpha = 0.69f;
							GameFiber.Wait(1000);
							bool flag3 = EntityExtensions.Exists(this.Area);
							if (flag3)
							{
								this.Area.Delete();
							}
							GameFiber.Wait(500);
						}
						while (Game.LocalPlayer.Character.DistanceTo(this.Accident) >= 25f)
						{
							GameFiber.Wait(0);
						}
						GameFiber.Wait(1500);
						Game.DisplayNotification("Dispatch, Traffic Break ~b~Over. ~w~Traffic Moving Back to ~b~Normal.");
						GameFiber.Wait(3000);
						Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
						bool flag4 = EntityExtensions.Exists(this.AccidentBlip);
						if (flag4)
						{
							this.AccidentBlip.Delete();
						}
						GameFiber.Wait(1000);
					}
					IL_222:
					GameFiber.Wait(2500);
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
			bool flag = EntityExtensions.Exists(this.Area);
			if (flag)
			{
				this.Area.Delete();
			}
			bool flag2 = EntityExtensions.Exists(this.AccidentBlip);
			if (flag2)
			{
				this.AccidentBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.TowTruck);
			if (flag3)
			{
				this.TowTruck.IsPersistent = false;
			}
			bool flag4 = EntityExtensions.Exists(this.AccidentVehicle);
			if (flag4)
			{
				this.AccidentVehicle.IsPersistent = false;
			}
			bool flag5 = EntityExtensions.Exists(this.Ambulance);
			if (flag5)
			{
				this.Ambulance.IsPersistent = false;
			}
			bool flag6 = EntityExtensions.Exists(this.Mechanic);
			if (flag6)
			{
				this.Mechanic.Delete();
			}
			bool flag7 = EntityExtensions.Exists(this.Paramedic);
			if (flag7)
			{
				this.Paramedic.Delete();
			}
			bool flag8 = EntityExtensions.Exists(this.Pylon);
			if (flag8)
			{
				this.Pylon.IsPersistent = false;
			}
			Game.LogTrivial("YOBBINCALLOUTS: Traffic Break Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private Vector3 MainSpawnPoint;

		
		private Vector3 Accident;

		
		private Vector3 Beach = new Vector3(-2011.691f, -446.1866f, 11.36965f);

		
		private Vector3 BeachAccident = new Vector3(-1735.433f, -702.8215f, 10.01621f);

		
		private Vector3 LittleSoul = new Vector3(-409.815f, -771.6809f, 37.1339f);

		
		private Vector3 LittleSoulAccident = new Vector3(-418.9631f, -1311.724f, 37.00277f);

		
		private Vector3 Strawberry = new Vector3(215.5652f, -1235.438f, 38.14722f);

		
		private Vector3 StrawberryAccident = new Vector3(633.7123f, -1216.477f, 42.32148f);

		
		private Vector3 Vinewood = new Vector3(16.10048f, -486.4405f, 33.82341f);

		
		private Vector3 VinewoodAccident = new Vector3(-551.613f, -487.6694f, 24.99862f);

		
		private Vector3 Desert = new Vector3(2601.184f, 3058.523f, 45.74524f);

		
		private Vector3 DesertAccident = new Vector3(2913.273f, 3675.457f, 52.55267f);

		
		private Vector3 Chiliad = new Vector3(1513.909f, 6425.665f, 22.97034f);

		
		private Vector3 ChiliadAccident = new Vector3(2057.506f, 6063.602f, 48.86989f);

		
		private Vector3 Tatamo = new Vector3(2465.136f, -151.0628f, 88.83215f);

		
		private Vector3 TatamoAccident = new Vector3(2631.071f, 355.748f, 96.81438f);

		
		private Vector3 Palomino = new Vector3(2092.122f, -595.4233f, 95.54442f);

		
		private Vector3 PalominoAccident = new Vector3(1629.288f, -933.3981f, 63.38768f);

		
		private Vector3 Docks = new Vector3(722.7678f, -2576.619f, 18.62388f);

		
		private Vector3 DocksAccident = new Vector3(715.9458f, -2798.863f, 6.28761f);

		
		private Blip Area;

		
		private Blip AccidentBlip;

		
		private Vehicle AccidentVehicle;

		
		private Vehicle Ambulance;

		
		private Vehicle TowTruck;

		
		private Ped Paramedic;

		
		private Ped Mechanic;

		
		private Object Pylon;

		
		private int MainScenario;

		
		private string Zone;

		
		private bool StartedTrafficBreak = false;

		
		private bool CalloutRunning = false;

		
		private readonly List<string> Instructions = new List<string>
		{
			"Activate your ~y~emergency lights~w~ to slow traffic down. Aim for a ~g~slow speed~w~ to keep ~b~traffic moving~w~, but only just.",
			"Once you reach the yellow ~y~waypoint,~w~ cancel your emergency lights and let traffic ~g~speed up~w~ again."
		};
	}
}
