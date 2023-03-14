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
	
	[CalloutInfo("Human Trafficking", 3)]
	public class HumanTrafficking : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Human Trafficking Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 4);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is " + this.MainScenario.ToString());
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).RealAreaName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			CallHandler.locationChooser(CallHandler.StoreList, 600f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			bool result;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
				base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 75f);
				base.AddMinimumDistanceCheck(25f, this.MainSpawnPoint);
				Functions.PlayScannerAudio("CITIZENS_REPORT CRIME_CIVILIAN_NEEDING_ASSISTANCE_01");
				base.CalloutMessage = "Human Trafficking";
				base.CalloutPosition = this.MainSpawnPoint;
				bool flag = this.MainScenario <= 1;
				if (flag)
				{
					base.CalloutAdvisory = "A Store Owner Has Spotted a Suspected Victim of ~r~Trafficking.";
				}
				else
				{
					bool calloutInterface = Main.CalloutInterface;
					if (calloutInterface)
					{
						base.CalloutAdvisory = "A Store Owner Has Spotted a Suspected Victim of ~r~Trafficking.";
						CalloutInterfaceHandler.SendMessage(this, "Suspect Reportedly Still on Scene.");
					}
					else
					{
						base.CalloutAdvisory = "A Store Owner Has Spotted a Suspected Victim of ~r~Trafficking.";
						Game.DisplayNotification("~r~Suspect ~w~Reportedly Still on Scene.");
					}
				}
				result = base.OnBeforeCalloutDisplayed();
			}
			else
			{
				Game.LogTrivial("YOBBINCALLOUTS: Not near store. Aborting callout.");
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Human Trafficking Callout Accepted by User.");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~b~Code 2.");
			}
			bool flag = this.MainScenario <= 1;
			if (flag)
			{
				this.Witness = new Ped("mp_m_shopkeep_01", this.MainSpawnPoint, 69f);
				this.Witness.IsPersistent = true;
				this.Witness.BlockPermanentEvents = true;
				this.VictimModels = new string[]
				{
					"a_f_y_bevhills_01",
					"a_f_y_bevhills_02",
					"a_f_y_bevhills_03",
					"a_f_y_bevhills_04",
					"a_f_y_business_01",
					"a_f_y_business_02",
					"a_f_y_vinewood_01",
					"a_f_y_vinewood_04"
				};
				Random lewis = new Random();
				int VictimModel = lewis.Next(0, this.VictimModels.Length);
				if (HumanTrafficking.<>o__27.<>p__0 == null)
				{
					HumanTrafficking.<>o__27.<>p__0 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(HumanTrafficking), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				HumanTrafficking.<>o__27.<>p__0.Target(HumanTrafficking.<>o__27.<>p__0, NativeFunction.Natives, World.GetNextPositionOnStreet(this.Witness.Position), 0, ref outPosition);
				this.Victim = new Ped(this.VictimModels[VictimModel], outPosition, 69f);
				this.Victim.IsPersistent = true;
				this.Victim.BlockPermanentEvents = true;
				if (HumanTrafficking.<>o__27.<>p__1 == null)
				{
					HumanTrafficking.<>o__27.<>p__1 = CallSite<Action<CallSite, object, Ped, string, float, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "APPLY_PED_DAMAGE_PACK", null, typeof(HumanTrafficking), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				HumanTrafficking.<>o__27.<>p__1.Target(HumanTrafficking.<>o__27.<>p__1, NativeFunction.Natives, this.Victim, "HOSPITAL_2", 0f, 0f);
				if (HumanTrafficking.<>o__27.<>p__2 == null)
				{
					HumanTrafficking.<>o__27.<>p__2 = CallSite<Action<CallSite, object, Ped, string, float, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "APPLY_PED_DAMAGE_PACK", null, typeof(HumanTrafficking), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				HumanTrafficking.<>o__27.<>p__2.Target(HumanTrafficking.<>o__27.<>p__2, NativeFunction.Natives, this.Victim, "HOSPITAL_1", 0f, 0f);
				this.Victim.Tasks.Cower(-1);
				this.AreaBlip = new Blip(this.Witness.Position, 25f);
			}
			else
			{
				this.VictimModels = new string[]
				{
					"a_f_y_bevhills_01",
					"a_f_y_bevhills_02",
					"a_f_y_bevhills_03",
					"a_f_y_bevhills_04",
					"a_f_y_business_01",
					"a_f_y_business_02",
					"a_f_y_vinewood_01",
					"a_f_y_vinewood_04"
				};
				Random lewis2 = new Random();
				int VictimModel2 = lewis2.Next(0, this.VictimModels.Length);
				if (HumanTrafficking.<>o__27.<>p__3 == null)
				{
					HumanTrafficking.<>o__27.<>p__3 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(HumanTrafficking), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition2;
				HumanTrafficking.<>o__27.<>p__3.Target(HumanTrafficking.<>o__27.<>p__3, NativeFunction.Natives, World.GetNextPositionOnStreet(this.MainSpawnPoint), 0, ref outPosition2);
				this.Victim = new Ped(this.VictimModels[VictimModel2], outPosition2, 69f);
				this.Victim.IsPersistent = true;
				this.Victim.BlockPermanentEvents = true;
				if (HumanTrafficking.<>o__27.<>p__4 == null)
				{
					HumanTrafficking.<>o__27.<>p__4 = CallSite<Action<CallSite, object, Ped, string, float, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "APPLY_PED_DAMAGE_PACK", null, typeof(HumanTrafficking), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				HumanTrafficking.<>o__27.<>p__4.Target(HumanTrafficking.<>o__27.<>p__4, NativeFunction.Natives, this.Victim, "HOSPITAL_2", 0f, 0f);
				if (HumanTrafficking.<>o__27.<>p__5 == null)
				{
					HumanTrafficking.<>o__27.<>p__5 = CallSite<Action<CallSite, object, Ped, string, float, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "APPLY_PED_DAMAGE_PACK", null, typeof(HumanTrafficking), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				HumanTrafficking.<>o__27.<>p__5.Target(HumanTrafficking.<>o__27.<>p__5, NativeFunction.Natives, this.Victim, "HOSPITAL_1", 0f, 0f);
				this.Victim.Tasks.Cower(-1);
				this.SuspectModels = new string[]
				{
					"A_M_Y_SouCent_01",
					"A_M_Y_StWhi_01",
					"A_M_Y_StBla_01",
					"A_M_Y_Downtown_01",
					"A_M_Y_BevHills_01",
					"G_M_Y_MexGang_01",
					"G_M_Y_MexGoon_01",
					"G_M_Y_StrPunk_01"
				};
				Random r2 = new Random();
				int SuspectModel = r2.Next(0, this.SuspectModels.Length);
				this.Suspect = new Ped(this.SuspectModels[SuspectModel], this.Victim.GetOffsetPositionFront(3f), this.Victim.Heading - 180f);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.Suspect.Heading = this.Victim.Heading - 180f;
				CallHandler.IdleAction(this.Suspect, false);
				if (HumanTrafficking.<>o__27.<>p__6 == null)
				{
					HumanTrafficking.<>o__27.<>p__6 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(HumanTrafficking), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 SuspectVehicleSpawnPoint;
				HumanTrafficking.<>o__27.<>p__6.Target(HumanTrafficking.<>o__27.<>p__6, NativeFunction.Natives, World.GetNextPositionOnStreet(this.Victim.Position), 0, ref SuspectVehicleSpawnPoint);
				try
				{
					if (HumanTrafficking.<>o__27.<>p__7 == null)
					{
						HumanTrafficking.<>o__27.<>p__7 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(HumanTrafficking), new CSharpArgumentInfo[]
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
					HumanTrafficking.<>o__27.<>p__7.Target(HumanTrafficking.<>o__27.<>p__7, NativeFunction.Natives, SuspectVehicleSpawnPoint, ref nodePosition, ref heading, 1, 3f, 0);
					this.SuspectVehicle = new Vehicle("SPEEDO", SuspectVehicleSpawnPoint, heading);
					this.SuspectVehicle.PrimaryColor = Color.White;
					this.SuspectVehicle.IsPersistent = true;
				}
				catch (Exception)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
					return false;
				}
				bool flag2 = this.MainScenario == 3;
				if (flag2)
				{
					this.Victim.WarpIntoVehicle(this.SuspectVehicle, 0);
				}
				this.AreaBlip = new Blip(this.Victim.Position, 25f);
			}
			this.AreaBlip.Color = Color.Yellow;
			this.AreaBlip.Alpha = 0.67f;
			this.AreaBlip.IsRouteEnabled = true;
			this.AreaBlip.Name = "Callout Location";
			bool flag3 = !this.CalloutRunning;
			if (flag3)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Human Trafficking Callout Not Accepted by User.");
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
						bool flag = this.MainScenario <= 1;
						if (flag)
						{
							while (this.player.DistanceTo(this.Witness) >= 25f && !Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
						}
						else
						{
							while (this.player.DistanceTo(this.Victim) >= 35f && !Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
						}
						bool flag2 = Game.IsKeyDown(Config.CalloutEndKey);
						if (flag2)
						{
							EndCalloutHandler.CalloutForcedEnd = true;
						}
						else
						{
							Game.LogTrivial("YOBBINCALLOUTS: Player Arrived on Scene.");
							this.AreaBlip.Delete();
							bool flag3 = this.MainScenario <= 1;
							if (flag3)
							{
								Game.DisplayHelp("Speak with the ~g~Store Manager.");
							}
							else
							{
								Game.DisplayHelp("Investigate the Situation.");
							}
							this.Confront();
						}
					}
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
			bool flag = EntityExtensions.Exists(this.Victim);
			if (flag)
			{
				this.Victim.Tasks.ClearImmediately();
				this.Victim.ClearLastVehicle();
			}
			bool flag2 = EntityExtensions.Exists(this.Victim);
			if (flag2)
			{
				this.Victim.Dismiss();
			}
			bool flag3 = EntityExtensions.Exists(this.SuspectArea);
			if (flag3)
			{
				this.SuspectArea.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.VictimBlip);
			if (flag4)
			{
				this.VictimBlip.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag5)
			{
				this.SuspectBlip.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.AreaBlip);
			if (flag6)
			{
				this.AreaBlip.Delete();
			}
			bool flag7 = EntityExtensions.Exists(this.Witness);
			if (flag7)
			{
				this.Witness.Dismiss();
			}
			bool flag8 = EntityExtensions.Exists(this.WitnessBlip);
			if (flag8)
			{
				this.WitnessBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Human Trafficking Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void Confront()
		{
			this.VictimBlip = this.Victim.AttachBlip();
			this.VictimBlip.Scale = 0.8f;
			this.VictimBlip.IsFriendly = true;
			this.VictimBlip.Name = "Victim";
			bool flag = this.MainScenario <= 1;
			if (flag)
			{
				this.WitnessBlip = this.Witness.AttachBlip();
				this.WitnessBlip.Scale = 0.8f;
				this.WitnessBlip.Color = Color.Purple;
				this.WitnessBlip.Name = "Store Owner";
				Game.DisplayHelp("Speak with the ~p~Store Owner.");
				if (HumanTrafficking.<>o__32.<>p__0 == null)
				{
					HumanTrafficking.<>o__32.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(HumanTrafficking), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				HumanTrafficking.<>o__32.<>p__0.Target(HumanTrafficking.<>o__32.<>p__0, NativeFunction.Natives, this.Witness, this.player, -1);
				while (this.player.DistanceTo(this.Witness) >= 5f)
				{
					GameFiber.Wait(0);
				}
				Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~p~Store Owner.");
				Random bingchilling = new Random();
				int Dialogue = bingchilling.Next(0, 2);
				bool flag2 = Dialogue == 0;
				if (flag2)
				{
					CallHandler.Dialogue(this.WitnessOpening1, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				else
				{
					CallHandler.Dialogue(this.WitnessOpening2, this.Witness, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				GameFiber.Wait(1500);
				this.Witness.Tasks.ClearImmediately();
				this.WitnessBlip.Delete();
				Game.DisplayHelp("Speak with the ~b~Victim.");
				while (this.player.DistanceTo(this.Victim) >= 7f)
				{
					GameFiber.Wait(0);
				}
				Game.DisplaySubtitle("~g~You:~w~ Hi Ma'am, Can I Speak With You? You Don't Need to Worry, Everything is Okay Now.", 3500);
				GameFiber.Wait(2500);
				this.Victim.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(1000);
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~b~Victim.");
				}
				bool flag3 = this.MainScenario == 0;
				if (flag3)
				{
					CallHandler.Dialogue(this.VictimInfo1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					Game.LogTrivial("YOBBINCALLOUTS: Dismiss victim");
					GameFiber.Wait(1000);
					bool flag4 = EntityExtensions.Exists(this.VictimBlip);
					if (flag4)
					{
						this.VictimBlip.Delete();
					}
					this.Victim.Tasks.ClearImmediately();
					bool flag5 = EntityExtensions.Exists(this.Victim);
					if (flag5)
					{
						this.Victim.Dismiss();
					}
					this.Search();
				}
				else
				{
					CallHandler.Dialogue(this.VictimInfo2, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					this.DrivePeep();
				}
			}
			else
			{
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.Scale = 0.8f;
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Name = "Suspect";
				Game.DisplaySubtitle("~r~Suspect:~w~ What?! You called the cops on me you stupid bitch?!");
				this.Suspect.RelationshipGroup.SetRelationshipWith(RelationshipGroup.Player, 5);
				bool flag6 = this.MainScenario == 2;
				if (flag6)
				{
					this.Suspect.BlockPermanentEvents = false;
					this.Victim.Tasks.Cower(-1);
					this.Suspect.Tasks.FightAgainst(this.Victim, -1);
					GameFiber.Wait(1000);
					this.Victim.Tasks.ReactAndFlee(this.Suspect);
					Game.LogTrivial("YOBBINCALLOUTS: Suspect started assaulting victim.");
					while (EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive && !Functions.IsPedArrested(this.Suspect))
					{
						GameFiber.Wait(0);
					}
					bool flag7 = EntityExtensions.Exists(this.Suspect);
					if (flag7)
					{
						bool flag8 = Functions.IsPedArrested(this.Suspect);
						if (flag8)
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ for ~r~Assault.");
						}
						else
						{
							GameFiber.Wait(2000);
							Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed.");
						}
					}
					else
					{
						GameFiber.Wait(2000);
						Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ for ~r~Assault.");
					}
					GameFiber.Wait(2000);
					Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
					GameFiber.Wait(2000);
				}
				else
				{
					this.Suspect.Tasks.EnterVehicle(this.SuspectVehicle, -1, -1, 4.2f).WaitForCompletion();
					Game.LogTrivial("YOBBINCALLOUTS: Suspect entered vehicle.");
					bool flag9 = EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive;
					if (flag9)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Suspect Pursuit Started");
						this.MainPursuit = Functions.CreatePursuit();
						Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
						Game.DisplayNotification("Suspect is ~r~Evading~w~ with a ~o~Kidnapped~w~ Female!");
						bool calloutInterface = Main.CalloutInterface;
						if (calloutInterface)
						{
							CalloutInterfaceHandler.SendMessage(this, "Suspect is ~r~Evading~w~ with a ~o~Kidnapped~w~ Female");
						}
						Functions.RequestBackup(this.player.Position, 1, 0);
						Functions.SetPursuitIsActiveForPlayer(this.MainPursuit, true);
						Functions.AddPedToPursuit(this.MainPursuit, this.Suspect);
						this.Victim.Tasks.PlayAnimation("veh@truck@ps@idle_panic", "sit", -1f, 1);
						while (Functions.IsPursuitStillRunning(this.MainPursuit))
						{
							GameFiber.Wait(0);
						}
						bool flag10 = EntityExtensions.Exists(this.Suspect);
						if (flag10)
						{
							bool flag11 = Functions.IsPedArrested(this.Suspect);
							if (flag11)
							{
								GameFiber.Wait(1000);
								Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ following the ~b~Pursuit.");
							}
							else
							{
								GameFiber.Wait(1000);
								Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ following the ~b~Pursuit.");
							}
						}
						else
						{
							GameFiber.Wait(5000);
							Game.DisplayNotification("Dispatch, We are ~g~Code 4~w~. Pursuit ~b~Over.");
						}
						GameFiber.Wait(2000);
						Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
						GameFiber.Wait(2000);
					}
				}
				bool flag12 = EntityExtensions.Exists(this.Victim) && this.Victim.IsAlive;
				if (flag12)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Started talking to victim on callout end.");
					bool flag13 = EntityExtensions.Exists(this.SuspectBlip);
					if (flag13)
					{
						this.SuspectBlip.Delete();
					}
					this.Victim.Tasks.LeaveVehicle(this.SuspectVehicle, 256).WaitForCompletion();
					this.Victim.Tasks.Cower(-1);
					Game.DisplayHelp("When Ready, Locate and Speak with the ~b~Victim.");
					while (this.player.DistanceTo(this.Victim) >= 4f)
					{
						GameFiber.Wait(0);
					}
					this.Victim.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
					bool displayHelp2 = Config.DisplayHelp;
					if (displayHelp2)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak with the ~b~Victim.");
					}
					Random bingchilling2 = new Random();
					int Dialogue2 = bingchilling2.Next(0, 2);
					bool flag14 = Dialogue2 == 0;
					if (flag14)
					{
						CallHandler.Dialogue(this.VictimEnding1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						Game.LogTrivial("YOBBINCALLOUTS: Dismiss victim");
						GameFiber.Wait(1000);
						bool flag15 = EntityExtensions.Exists(this.VictimBlip);
						if (flag15)
						{
							this.VictimBlip.Delete();
						}
						this.Victim.Tasks.ClearImmediately();
						bool flag16 = EntityExtensions.Exists(this.Victim);
						if (flag16)
						{
							this.Victim.ClearLastVehicle();
						}
					}
					else
					{
						CallHandler.Dialogue(this.VictimEnding2, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						this.DrivePeep();
					}
				}
			}
		}

		
		private void DrivePeep()
		{
			this.Victim.Tasks.AchieveHeading(Game.LocalPlayer.Character.Heading - 180f).WaitForCompletion(500);
			CallHandler.IdleAction(this.Victim, false);
			Game.DisplayHelp("Enter Your Vehicle Give the Victim a ~g~Ride~w~. To ~r~Decline~w~ the Ride, Press ~y~" + Config.CalloutEndKey.ToString() + ".");
			CallHandler.IdleAction(this.Victim, false);
			while (!Game.LocalPlayer.Character.IsInAnyPoliceVehicle && !Game.IsKeyDown(Config.CalloutEndKey))
			{
				GameFiber.Wait(0);
			}
			bool flag = Game.IsKeyDown(Config.CalloutEndKey);
			if (flag)
			{
				bool flag2 = this.MainScenario <= 1;
				if (flag2)
				{
					Game.DisplaySubtitle("~g~You:~w~ Sorry, I'll Need to Locate the Suspect First.", 3500);
				}
				else
				{
					Game.DisplaySubtitle("~g~You:~w~ Sorry, I need to take care of something else.", 3500);
				}
				GameFiber.Wait(3000);
				bool flag3 = EntityExtensions.Exists(this.Victim);
				if (flag3)
				{
					this.Victim.Dismiss();
				}
				bool flag4 = EntityExtensions.Exists(this.VictimBlip);
				if (flag4)
				{
					this.VictimBlip.Delete();
				}
				bool flag5 = this.MainScenario <= 1;
				if (flag5)
				{
					this.Search();
				}
			}
			else
			{
				GameFiber.Wait(1000);
				Game.LogTrivial("YOBBINCALLOUTS: Player Has Accepted the Ride.");
				Game.DisplaySubtitle("~g~You:~w~ I Can Give You a Ride, Hop In!", 3000);
				GameFiber.Wait(2000);
				Game.DisplayHelp(string.Concat(new string[]
				{
					"~y~",
					Config.Key1.ToString(),
					": ~b~Ask the Victim to Enter the Passenger Seat. ~y~",
					Config.Key2.ToString(),
					":~b~ Ask the Victim to Enter the Rear Seat."
				}));
				while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
				{
					GameFiber.Wait(0);
				}
				bool flag6 = Game.IsKeyDown(Config.Key1);
				if (flag6)
				{
					this.SeatIndex = Game.LocalPlayer.Character.CurrentVehicle.GetFreePassengerSeatIndex().Value;
					this.Victim.Tasks.EnterVehicle(Game.LocalPlayer.Character.CurrentVehicle, this.SeatIndex, 0).WaitForCompletion();
				}
				else
				{
					this.SeatIndex = Game.LocalPlayer.Character.CurrentVehicle.GetFreeSeatIndex(1, 2).Value;
					this.Victim.Tasks.EnterVehicle(Game.LocalPlayer.Character.CurrentVehicle, this.SeatIndex, 0).WaitForCompletion();
				}
				bool flag7 = EntityExtensions.Exists(this.VictimBlip);
				if (flag7)
				{
					this.VictimBlip.Delete();
				}
				CallHandler.locationChooser(CallHandler.HouseList, 600f, 25f);
				Vector3 DriverDestination = CallHandler.SpawnPoint;
				this.VictimBlip = new Blip(DriverDestination);
				this.VictimBlip.Color = Color.Green;
				this.VictimBlip.IsRouteEnabled = true;
				this.VictimBlip.Name = "Destination";
				GameFiber.Wait(1000);
				Game.DisplayHelp("Drive to the ~g~House~w~ Marked on the Map.");
				while (this.player.DistanceTo(DriverDestination) >= 35f && !Game.IsKeyDown(Config.CalloutEndKey))
				{
					GameFiber.Wait(0);
				}
				bool flag8 = Game.IsKeyDown(Config.CalloutEndKey);
				if (flag8)
				{
					this.End();
				}
				Game.DisplayHelp("Stop Your Vehicle to Let the ~b~Victim ~w~Out.");
				while (this.player.CurrentVehicle.Speed > 0f)
				{
					GameFiber.Wait(0);
				}
				this.Victim.PlayAmbientSpeech("generic_thanks");
				this.Victim.Tasks.LeaveVehicle(Game.LocalPlayer.Character.CurrentVehicle, 0).WaitForCompletion();
				GameFiber.Wait(1000);
				bool flag9 = EntityExtensions.Exists(this.VictimBlip);
				if (flag9)
				{
					this.VictimBlip.Delete();
				}
				GameFiber.StartNew(delegate()
				{
					this.Victim.Tasks.FollowNavigationMeshToPosition(DriverDestination, 69f, 1.25f, -1).WaitForCompletion();
				});
				bool flag10 = this.MainScenario <= 1;
				if (flag10)
				{
					this.Search();
				}
			}
		}

		
		private void Search()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				this.SuspectSpawnPoint = World.GetNextPositionOnStreet(this.player.Position.Around(500f));
				this.SuspectVehicle = new Vehicle("SPEEDO", this.SuspectSpawnPoint);
				this.SuspectVehicle.PrimaryColor = Color.White;
				this.SuspectVehicle.IsDeformationEnabled = true;
				this.SuspectVehicle.IsPersistent = true;
				this.SuspectModels = new string[]
				{
					"A_M_Y_SouCent_01",
					"A_M_Y_StWhi_01",
					"A_M_Y_StBla_01",
					"A_M_Y_Downtown_01",
					"A_M_Y_BevHills_01",
					"G_M_Y_MexGang_01",
					"G_M_Y_MexGoon_01",
					"G_M_Y_StrPunk_01"
				};
				Random r2 = new Random();
				int SuspectModel = r2.Next(0, this.SuspectModels.Length);
				this.Suspect = new Ped(this.SuspectModels[SuspectModel], this.SuspectVehicle.Position, 69f);
				this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.Suspect.Tasks.CruiseWithVehicle(15f);
				bool flag = EntityExtensions.Exists(this.Victim);
				if (flag)
				{
					this.Victim.Tasks.Clear();
					this.Victim.Dismiss();
				}
				Game.DisplayHelp("Start ~o~Searching~w~ for the ~r~Suspect.");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendMessage(this, "Suspect Vehicle is a White " + this.SuspectVehicle.Model.Name + " Utility Van.");
				}
				else
				{
					Game.DisplayNotification("Suspect Vehicle is a White " + this.SuspectVehicle.Model.Name + " Utility Van.");
				}
				this.SuspectArea = new Blip(this.Suspect.Position.Around(15f), 250f);
				this.SuspectArea.Color = Color.Orange;
				this.SuspectArea.Alpha = 0.5f;
				this.SuspectArea.IsRouteEnabled = true;
				GameFiber.Wait(1500);
				while (this.player.DistanceTo(this.Suspect) >= 125f)
				{
					GameFiber.Wait(0);
				}
				this.Suspect.Tasks.CruiseWithVehicle(this.SuspectVehicle, 15f, 4);
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01");
				GameFiber.Wait(1000);
				Game.DisplayNotification("~b~Update:~w~ A Caller Has ~y~Spotted~w~ the ~r~Suspect~w~ Driving Recklessly. ~g~Updating Map.");
				GameFiber.Wait(1000);
				bool flag2 = EntityExtensions.Exists(this.SuspectArea);
				if (flag2)
				{
					this.SuspectArea.Delete();
				}
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				GameFiber.Wait(1500);
				Game.DisplaySubtitle("Dispatch, We Have Located the ~r~Suspect!");
				GameFiber.Wait(1500);
				Game.DisplayHelp("Perform a ~o~Traffic Stop~w~ on the ~r~Suspect.");
				while (!Functions.IsPlayerPerformingPullover() && this.Suspect.IsAlive)
				{
					GameFiber.Wait(0);
				}
				bool flag3 = this.MainScenario >= 0;
				if (flag3)
				{
					this.SuspectViolent();
				}
			}
		}

		
		private void SuspectViolent()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Random r = new Random();
				int SuspectAction = r.Next(0, 2);
				bool flag = SuspectAction == 0;
				if (flag)
				{
					CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
					{
						this.Suspect
					});
				}
				else
				{
					while (this.SuspectVehicle.Speed > 0f)
					{
						GameFiber.Wait(0);
					}
					Game.DisplayHelp("Speak With the ~r~Suspect.");
					Random monke = new Random();
					int WeaponModel = monke.Next(0, 5);
					Game.LogTrivial("YOBBINCALLOUTS: Weapon Model is " + WeaponModel.ToString());
					bool flag2 = WeaponModel == 0;
					if (flag2)
					{
						this.Suspect.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE", -1, true);
					}
					else
					{
						bool flag3 = WeaponModel == 1;
						if (flag3)
						{
							this.Suspect.Inventory.GiveNewWeapon("WEAPON_SMG", -1, true);
						}
						else
						{
							bool flag4 = WeaponModel == 2;
							if (flag4)
							{
								this.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
							}
							else
							{
								bool flag5 = WeaponModel == 3;
								if (flag5)
								{
									this.Suspect.Inventory.GiveNewWeapon("WEAPON_SAWEDOFFSHOTGUN", -1, true);
								}
								else
								{
									bool flag6 = WeaponModel == 4;
									if (flag6)
									{
										this.Suspect.Inventory.GiveNewWeapon("WEAPON_COMPACTRIFLE", -1, true);
									}
								}
							}
						}
					}
					Random rondom = new Random();
					int WaitTime = rondom.Next(5, 15);
					Game.LogTrivial("YOBBINCALLOUTS: Suspect will fire when player is " + WaitTime.ToString() + " metres away.");
					while (this.player.DistanceTo(this.Suspect) >= (float)WaitTime && !Game.IsKeyDown(Config.CalloutEndKey))
					{
						GameFiber.Wait(0);
					}
					while (this.player.IsInAnyVehicle(false) || this.player.DistanceTo(this.Suspect) >= (float)WaitTime)
					{
						GameFiber.Wait(0);
					}
					this.Suspect.Tasks.ParkVehicle(this.SuspectVehicle, this.SuspectVehicle.Position, this.SuspectVehicle.Heading).WaitForCompletion(2500);
					this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256).WaitForCompletion();
					this.Suspect.Tasks.AchieveHeading(Game.LocalPlayer.Character.LastVehicle.Heading - 180f).WaitForCompletion(1500);
					this.Suspect.Tasks.AimWeaponAt(Game.LocalPlayer.Character.Position, 1500).WaitForCompletion();
					this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
					bool flag7 = Functions.IsPlayerPerformingPullover();
					if (flag7)
					{
						Functions.ForceEndCurrentPullover();
					}
					Functions.PlayScannerAudio("CRIME_ASSAULT_PEACE_OFFICER_01");
					Functions.RequestBackup(this.Suspect.Position, 1, 0);
					while (EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect) && this.Suspect.IsAlive)
					{
						GameFiber.Yield();
					}
					bool flag8 = EntityExtensions.Exists(this.Suspect);
					if (flag8)
					{
						bool flag9 = Functions.IsPedArrested(this.Suspect) || this.Suspect.IsAlive;
						if (flag9)
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Attempting to ~r~Assault an Officer.");
						}
						else
						{
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ for ~r~Assaulting an Officer.");
						}
					}
					else
					{
						GameFiber.Wait(1000);
						Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Attempting to ~r~Assault an Officer.");
					}
					GameFiber.Wait(2000);
					Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
					GameFiber.Wait(2000);
				}
			}
		}

		
		private Vector3 MainSpawnPoint;

		
		private Vector3 SuspectSpawnPoint;

		
		private Ped Suspect;

		
		private Ped Victim;

		
		private Ped Witness;

		
		private Vehicle SuspectVehicle;

		
		private LHandle MainPursuit;

		
		private Blip SuspectBlip;

		
		private Blip SuspectArea;

		
		private Blip VictimBlip;

		
		private Blip AreaBlip;

		
		private Blip WitnessBlip;

		
		private int SeatIndex;

		
		private int MainScenario;

		
		private string Zone;

		
		private string[] SuspectModels;

		
		private string[] VictimModels;

		
		private bool CalloutRunning;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private readonly List<string> WitnessOpening1 = new List<string>
		{
			"~g~You:~w~ Hello, are you the caller?",
			"~p~Store owner:~w~ Yes I am, officer. I noticed that woman over there pull up with a man in a white van.",
			"~p~Store owner:~w~ She nervously entered the store to use the restroom.",
			"~p~Store owner:~w~ She clearly was in need of assistance, as she had bruises and scrapes all over her.",
			"~g~You:~w~ I see. Is this the first time something like this has happened here?",
			"~p~Store owner:~w~ Unfortunately no, this store is a favorite for traffickers to stop and pick up supplies and gas.",
			"~p~Store owner:~w~ I'm always on the lookout for people that might need help.",
			"~g~You:~w~ Where's the suspect now?",
			"~p~Store owner:~w~ I confronted him, but he ran out of the store and drove off. I managed to get a plate, however.",
			"~g~You:~w~ Thanks so much for your help. I'll speak with the victim now."
		};

		
		private readonly List<string> WitnessOpening2 = new List<string>
		{
			"~g~You:~w~ Hello, are you the caller?",
			"~p~Store owner:~w~ Yes I am, officer. I noticed that woman over there pull up with a person in a white van.",
			"~p~Store owner:~w~ She nervously entered the store to use the restroom.",
			"~p~Store owner:~w~ She clearly was in need of assistance, as she had bruises and scrapes all over her.",
			"~g~You:~w~ I see. Is this the first time something like this has happened here?",
			"~p~Store owner:~w~ Unfortunately no, this store is a favorite for traffickers to stop and grab supplies and gas.",
			"~p~Store owner:~w~ I'm always on the lookout for people that might need help.",
			"~g~You:~w~ Where's the suspect now?",
			"~p~Store owner:~w~ I confronted him, but he ran out of the store and drove off. I managed to get a vehicle description, however.",
			"~g~You:~w~ Thanks so much for your help. I'll speak with the victim now."
		};

		
		private readonly List<string> VictimInfo1 = new List<string>
		{
			"~g~You:~w~ Hey there, do you need medical attention?",
			"~b~Victim:~w~ I-I'm alright, officer. Thanks though.",
			"~g~You:~w~ Alright. Do you mind if I ask you a few questions?",
			"~b~Victim:~w~ Yeah, go ahead. I'd love to put an end to this operation.",
			"~g~You:~w~ So is there more than just one person involved in this?",
			"~b~Victim:~w~ Uh, yeah. There's a group of guys that look for women in vulnerable positions.",
			"~b~Victim:~w~ They then trick them into working for them, before holding them against their will.",
			"~g~You:~w~ I already got the vehicle information from the store owner. These are some very good leads.",
			"~b~Victim:~w~ That's awesome. The faster we can put an end to this, the better.",
			"~g~You:~w~ Absolutely. Thanks for your help, is there anything else I can help you with?",
			"~b~Victim:~w~ I think I'm ok. Thanks for the help officer."
		};

		
		private readonly List<string> VictimInfo2 = new List<string>
		{
			"~g~You:~w~ Hey there, do you require any medical attention?",
			"~b~Victim:~w~ I-I'm alright, officer. Thanks though.",
			"~g~You:~w~ Alright, sounds good. Do you mind if I ask you a few questions?",
			"~b~Victim:~w~ Yeah, go ahead. I'd love to put an end to this operation.",
			"~g~You:~w~ So is there more than just one person involved in this?",
			"~b~Victim:~w~ Uh, yeah. There's a group of guys that look for women in vulnerable positions.",
			"~b~Victim:~w~ They then trick them into working for them, before holding them against their will.",
			"~g~You:~w~ I already got the vehicle information from the store owner. These are some very good leads.",
			"~b~Victim:~w~ That's awesome. The faster we can put an end to this, the better.",
			"~g~You:~w~ Absolutely. Thanks for your help, is there anything else I can do for you?",
			"~b~Victim:~w~ If it's not too much to ask, could I get a lift to my sister's place? I could probably stay there for a bit."
		};

		
		private readonly List<string> SuspectOpening1 = new List<string>
		{
			"~g~You:~w~ Sir, we've had reports that you've been following people around.",
			"~r~Suspect:~w~ What? I'm just taking a leisurely stroll, officer.",
			"~g~You:~w~ Ma'am, was this the man you called us about?",
			"~b~*Victim nods*",
			"~r~Suspect:~w~ What! You called the cops on me? You stupid bitch!",
			"~b~You:~w~ Sir, calm down!"
		};

		
		private readonly List<string> VictimEnding1 = new List<string>
		{
			"~g~You:~w~ Hi Ma'am, Can I Speak With You? You Don't Need to Worry, Everything is Okay Now.",
			"~g~You:~w~ Do you need medical attention?",
			"~b~Victim:~w~ I-I'm alright, officer. Thanks though.",
			"~g~You:~w~ Alright. Do you mind if I ask you a few questions?",
			"~b~Victim:~w~ Uh, Sure. I don't have anything to hide Officer.",
			"~g~You:~w~ All right. Can you tell me what happened?",
			"~b~Victim:~w~ Uh, yeah. There's a group of guys that look for women in vulnerable positions.",
			"~b~Victim:~w~ They then trick them into working for them, before holding them against their will.",
			"~g~You:~w~ I see. Do you know who might be in charge of this sort of operation?",
			"~b~Victim:~w~ No I don't. They never let me see anyone except the man who just tried to kidnap me.",
			"~b~Victim:~w~ I'm just glad I got out alive. I've heard terrible things about what they've done to people.",
			"~g~You:~w~ Absolutely, this could have ended much worse. Thanks for your help, is there anything else I can help you with?",
			"~b~Victim:~w~ I think I'm ok. Thanks for the help officer."
		};

		
		private readonly List<string> VictimEnding2 = new List<string>
		{
			"~g~You:~w~ Hi Ma'am, Can I Speak With You? You Don't Need to Worry, Everything is Okay Now.",
			"~g~You:~w~ Do you need medical attention?",
			"~b~Victim:~w~ I-I'm alright, officer. Thanks though.",
			"~g~You:~w~ Alright. Do you mind if I ask you a few questions?",
			"~b~Victim:~w~ Uh, Sure. I don't have anything to hide Officer.",
			"~g~You:~w~ All right. Can you tell me what happened?",
			"~b~Victim:~w~ Uh, yeah. There's a group of guys that look for women in vulnerable positions.",
			"~b~Victim:~w~ They then trick them into working for them, before holding them against their will.",
			"~g~You:~w~ I see. Do you know who might be in charge of this sort of operation?",
			"~b~Victim:~w~ No I don't. They never let me see anyone except the man who just tried to kidnap me.",
			"~b~Victim:~w~ I'm just glad I got out alive. I've heard terrible things about what they've done to people.",
			"~g~You:~w~ Absolutely, this could have ended much worse. Thanks for your help, is there anything else I can help you with?",
			"~b~Victim:~w~ If it's not too much to ask, could I get a lift to my sister's place? I could probably stay there for a bit."
		};
	}
}
