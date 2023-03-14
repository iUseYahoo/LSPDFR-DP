using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Sovereign Citizen", 2)]
	public class SovereignCitizen : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Sovereign Citizen Callout Start==========");
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			Vector3 Spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(550f));
			try
			{
				if (SovereignCitizen.<>o__24.<>p__0 == null)
				{
					SovereignCitizen.<>o__24.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(SovereignCitizen), new CSharpArgumentInfo[]
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
				SovereignCitizen.<>o__24.<>p__0.Target(SovereignCitizen.<>o__24.<>p__0, NativeFunction.Natives, Spawn, ref nodePosition, ref heading, 1, 3f, 0);
				if (SovereignCitizen.<>o__24.<>p__2 == null)
				{
					SovereignCitizen.<>o__24.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(SovereignCitizen)));
				}
				Func<CallSite, object, bool> target = SovereignCitizen.<>o__24.<>p__2.Target;
				CallSite <>p__ = SovereignCitizen.<>o__24.<>p__2;
				if (SovereignCitizen.<>o__24.<>p__1 == null)
				{
					SovereignCitizen.<>o__24.<>p__1 = CallSite<<>F{00000010}<CallSite, object, Vector3, float, Vector3, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(SovereignCitizen), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				bool success = target(<>p__, SovereignCitizen.<>o__24.<>p__1.Target(SovereignCitizen.<>o__24.<>p__1, NativeFunction.Natives, Spawn, heading, ref outPosition));
				bool flag = success;
				if (!flag)
				{
					Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
					return false;
				}
				this.MainSpawnPoint = outPosition;
				this.VehicleHeading = heading;
			}
			catch (TargetInvocationException)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Could Not Find Spawnpoint. Aborting Callout.");
				return false;
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 25f);
			base.AddMinimumDistanceCheck(50f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("WE_HAVE_01 CRIME_RESIST_ARREST_04");
			base.CalloutMessage = "Sovereign Citizen";
			base.CalloutPosition = this.MainSpawnPoint;
			base.CalloutAdvisory = "A Sovereign Citizen is ~r~Refusing to Cooperate~w~ with Other Officers.";
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Sovereign Citizen Callout Accepted by User");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~b~Code 2~w~.");
			}
			this.SuspectVehicle = CallHandler.SpawnVehicle(this.MainSpawnPoint, this.VehicleHeading, true);
			Game.LogTrivial("YOBBINCALLOUTS: Suspect Vehicle Spawned");
			this.SuspectVehicle.IsPersistent = true;
			this.SuspectVehicle.IsEngineOn = true;
			this.SuspectVehicle.IsDriveable = false;
			this.Suspect = this.SuspectVehicle.CreateRandomDriver();
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.Tasks.CruiseWithVehicle(0f);
			this.OfficerVehicle = new Vehicle(Config.PoliceVehicle, this.SuspectVehicle.GetOffsetPositionFront(-7f), this.SuspectVehicle.Heading);
			Game.LogTrivial("YOBBINCALLOUTS: Officer Vehicle Spawned");
			this.OfficerVehicle.IsPersistent = true;
			this.Officer = this.OfficerVehicle.CreateRandomDriver();
			this.Officer.Tasks.LeaveVehicle(this.OfficerVehicle, 0);
			this.Officer.Tasks.FollowNavigationMeshToPosition(this.SuspectVehicle.GetBonePosition("wheel_lr"), this.SuspectVehicle.Heading + 90f, 2f, 1f, 5000);
			Game.LogTrivial("YOBBINCALLOUTS: Officer Spawned");
			this.Officer.IsPersistent = true;
			this.Officer.BlockPermanentEvents = true;
			this.Area = new Blip(this.MainSpawnPoint, 25f);
			this.Area.Alpha = 0.67f;
			this.Area.Color = Color.Yellow;
			this.Area.IsRouteEnabled = true;
			Random yuy = new Random();
			int ScenarioChooser = yuy.Next(0, 2);
			this.MainScenario = ScenarioChooser;
			bool flag = !this.CalloutRunning;
			if (flag)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Sovereign Citizen Callout Not Accepted by User.");
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
					while (this.CalloutRunning)
					{
						while (this.player.DistanceTo(this.MainSpawnPoint) >= 25f && !Game.IsKeyDown(Keys.End))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(Config.CalloutEndKey);
						if (flag)
						{
							EndCalloutHandler.CalloutForcedEnd = true;
							break;
						}
						bool flag2 = this.MainScenario <= 1;
						if (flag2)
						{
							this.TalkToOfficer();
							break;
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
			bool flag = EntityExtensions.Exists(this.Area);
			if (flag)
			{
				this.Area.Delete();
			}
			bool flag2 = EntityExtensions.Exists(this.OfficerBlip);
			if (flag2)
			{
				this.OfficerBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.Suspect);
			if (flag3)
			{
				this.Suspect.Dismiss();
			}
			bool flag4 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag4)
			{
				this.SuspectBlip.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this.Area);
			if (flag5)
			{
				this.Area.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.Officer);
			if (flag6)
			{
				this.Officer.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Ticket);
			if (flag7)
			{
				this.Ticket.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Sovereign Citizen Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void TalkToOfficer()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = EntityExtensions.Exists(this.Area);
				if (flag)
				{
					this.Area.Delete();
				}
				this.OfficerBlip = this.Officer.AttachBlip();
				this.OfficerBlip.Scale = 0.75f;
				this.OfficerBlip.IsFriendly = true;
				this.OfficerBlip.Name = "Officer";
				Game.DisplayHelp("Speak with the ~b~Officer.");
				if (SovereignCitizen.<>o__30.<>p__0 == null)
				{
					SovereignCitizen.<>o__30.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(SovereignCitizen), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				SovereignCitizen.<>o__30.<>p__0.Target(SovereignCitizen.<>o__30.<>p__0, NativeFunction.Natives, this.Officer, this.player, -1);
				while (this.player.DistanceTo(this.Officer) >= 5f)
				{
					GameFiber.Wait(0);
				}
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Talk to the ~b~Officer.");
				}
				Random r = new Random();
				int OpeningDialogue = r.Next(0, 2);
				int num = OpeningDialogue;
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 == 1)
					{
						CallHandler.Dialogue(this.OpeningDialogue2, this.Officer, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
				}
				else
				{
					CallHandler.Dialogue(this.OpeningDialogue1, this.Officer, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				}
				GameFiber.Wait(2000);
				this.Officer.Tasks.ClearImmediately();
				this.OfficerBlip.Delete();
				this.SuspectBlip = this.Suspect.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				this.SuspectBlip.Scale = 0.75f;
				Game.DisplayHelp("Talk to the ~r~Sovereign Citizen.");
				this.Question();
			}
		}

		
		private void Question()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				while (this.player.DistanceTo(this.Suspect) >= 3f)
				{
					GameFiber.Wait(0);
				}
				this.Officer.Tasks.AchieveHeading(this.SuspectVehicle.Heading).WaitForCompletion(500);
				CallHandler.IdleAction(this.Officer, true);
				Game.DisplaySubtitle("~g~You:~w~ Driver, Roll Your Window Down Please.", 3000);
				GameFiber.Wait(3500);
				bool flag = this.MainScenario == 0;
				if (flag)
				{
					try
					{
						if (SovereignCitizen.<>o__31.<>p__0 == null)
						{
							SovereignCitizen.<>o__31.<>p__0 = CallSite<Action<CallSite, object, Vehicle, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ROLL_DOWN_WINDOW", null, typeof(SovereignCitizen), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						SovereignCitizen.<>o__31.<>p__0.Target(SovereignCitizen.<>o__31.<>p__0, NativeFunction.Natives, this.SuspectVehicle, 0);
					}
					catch
					{
						Game.LogTrivial("YOBBINCALLOUTS: Error Rolling Down Driver Window.");
					}
					GameFiber.Wait(500);
					bool displayHelp = Config.DisplayHelp;
					if (displayHelp)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Talk to the ~r~Suspect.");
					}
					Random r = new Random();
					int OpeningDialogue = r.Next(0, 2);
					int num = OpeningDialogue;
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 == 1)
						{
							CallHandler.Dialogue(this.CooperatesOpening2, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
					}
					else
					{
						CallHandler.Dialogue(this.CooperatesOpening1, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					GameFiber.Wait(2000);
					Game.DisplayHelp(string.Concat(new string[]
					{
						"~y~",
						Config.Key1.ToString(),
						":~b~ Reason with the Suspect.~y~ ",
						Config.Key2.ToString(),
						":~b~ Scold the Suspect."
					}));
					while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
					{
						GameFiber.Wait(0);
					}
					bool displayHelp2 = Config.DisplayHelp;
					if (displayHelp2)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Continue Talking to the ~r~Suspect.");
					}
					bool flag2 = Game.IsKeyDown(Config.Key1);
					if (flag2)
					{
						Random r2 = new Random();
						int CooperateDialogue = r2.Next(0, 2);
						int num3 = CooperateDialogue;
						int num4 = num3;
						if (num4 != 0)
						{
							if (num4 == 1)
							{
								CallHandler.Dialogue(this.CooperatesReason2, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
						}
						else
						{
							CallHandler.Dialogue(this.CooperatesReason1, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
					}
					else
					{
						Random r3 = new Random();
						int ScoldDialogue = r3.Next(0, 2);
						int num5 = ScoldDialogue;
						int num6 = num5;
						if (num6 != 0)
						{
							if (num6 == 1)
							{
								CallHandler.Dialogue(this.Scold2, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							}
						}
						else
						{
							CallHandler.Dialogue(this.Scold1, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
					}
					GameFiber.Wait(2000);
					Game.DisplayHelp(string.Concat(new string[]
					{
						"~y~",
						Config.Key1.ToString(),
						":~b~ Arrest the Suspect.~y~ ",
						Config.Key2.ToString(),
						":~b~ Ticket the Suspect."
					}));
					while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
					{
						GameFiber.Wait(0);
					}
					bool flag3 = Game.IsKeyDown(Config.Key1);
					if (flag3)
					{
						this.Detain();
					}
					else
					{
						this.Ticketed();
					}
				}
				else
				{
					Game.DisplaySubtitle("~r~Suspect:~w~ I Will Not Follow Unlawful Commands Officer!", 3000);
					GameFiber.Wait(3500);
					bool displayHelp3 = Config.DisplayHelp;
					if (displayHelp3)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Reason With the ~r~Suspect.");
					}
					Random r4 = new Random();
					int ReasonDialogue = r4.Next(0, 2);
					int num7 = ReasonDialogue;
					int num8 = num7;
					if (num8 != 0)
					{
						if (num8 == 1)
						{
							CallHandler.Dialogue(this.Refuses2, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						}
					}
					else
					{
						CallHandler.Dialogue(this.Refuses1, null, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					GameFiber.Wait(2000);
					Game.DisplayHelp(string.Concat(new string[]
					{
						"~y~",
						Config.Key1.ToString(),
						":~b~ Arrest the Suspect.~y~ ",
						Config.Key2.ToString(),
						":~b~ Ticket the Suspect."
					}));
					while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
					{
						GameFiber.Wait(0);
					}
					bool flag4 = Game.IsKeyDown(Config.Key1);
					if (flag4)
					{
						this.Detain();
					}
					else
					{
						this.Ticketed();
					}
				}
			}
		}

		
		private void Ticketed()
		{
			Game.DisplayHelp("Go back to Your ~g~Vehicle~w~ to Write the Suspect a ~r~Ticket.");
			bool flag = EntityExtensions.Exists(this.player.LastVehicle);
			if (flag)
			{
				this.Area = this.player.LastVehicle.AttachBlip();
				this.Area.Color = Color.Green;
				while (!this.player.IsInAnyVehicle(false))
				{
					GameFiber.Wait(0);
				}
			}
			else
			{
				Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Write the Suspect a ~r~Ticket.");
				while (!Game.IsKeyDown(Config.MainInteractionKey))
				{
					GameFiber.Wait(0);
				}
			}
			GameFiber.Wait(2500);
			this.Ticket = new Object("prop_cd_paper_pile1", Vector3.Zero);
			this.Ticket.IsPersistent = true;
			this.Ticket.AttachTo(this.player, this.player.GetBoneIndex(57005), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
			Game.DisplayHelp("Go to the ~r~Suspect~w~ to Hand Them the Ticket.");
			bool flag2 = EntityExtensions.Exists(this.Area);
			if (flag2)
			{
				this.Area.Delete();
			}
			while (this.player.DistanceTo(this.Suspect) >= 4f)
			{
				GameFiber.Wait(0);
			}
			Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Give the ~r~Suspect~w~ the Ticket.");
			while (!Game.IsKeyDown(Config.MainInteractionKey))
			{
				GameFiber.Wait(0);
			}
			this.player.Tasks.PlayAnimation("mp_common", "givetake1_b", 1f, 1);
			GameFiber.Wait(1000);
			this.Ticket.Delete();
			GameFiber.Wait(1000);
			Game.DisplaySubtitle("~g~You:~w~ Here is your Ticket Sir. You Will be Expected to Appear in Court on the Date Specified.");
			this.player.Tasks.Clear();
			GameFiber.Wait(3500);
			this.Suspect.Dismiss();
		}

		
		private void Detain()
		{
			Game.DisplayHelp("Arrest the ~r~Suspect.");
			while (EntityExtensions.Exists(this.Suspect))
			{
				GameFiber.Yield();
				bool flag = this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect) || !EntityExtensions.Exists(this.Suspect);
				if (flag)
				{
					break;
				}
			}
			bool flag2 = EntityExtensions.Exists(this.Suspect);
			if (flag2)
			{
				bool flag3 = Functions.IsPedArrested(this.Suspect);
				if (flag3)
				{
					GameFiber.Wait(1000);
					Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ for Obstruction.");
				}
				else
				{
					GameFiber.Wait(1000);
					Game.DisplayNotification("Dispatch, a Suspect is ~r~Dead.");
				}
			}
			else
			{
				GameFiber.Wait(5000);
				Game.DisplayNotification("Dispatch, We are ~g~Code 4~w~.");
			}
			GameFiber.Wait(2000);
			Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
			GameFiber.Wait(2000);
		}

		
		private Vector3 MainSpawnPoint;

		
		private Blip Area;

		
		private Blip SuspectBlip;

		
		private Blip OfficerBlip;

		
		private Vehicle SuspectVehicle;

		
		private Vehicle OfficerVehicle;

		
		private Ped Suspect;

		
		private Ped Officer;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private Object Ticket;

		
		private float VehicleHeading;

		
		private int MainScenario;

		
		private string Zone;

		
		private bool CalloutRunning = false;

		
		private readonly List<string> OpeningDialogue1 = new List<string>
		{
			"~g~You:~w~ What's up dude, what seems to be the issue?",
			"~b~Officer:~w~ A fucking sovereign citizen. I swear I can't deal with these idiots, they piss me off so much!",
			"~b~Officer:~w~ Pulled him over for a speeding violation. He keeps talking nonsense and refuses to identify himself.",
			"~b~Officer:~w~ Maybe you can deal with his ass, I can't anymore or I'm gonna lose it.",
			"~g~You:~w~ I'll try my best, wish me luck.",
			"~b~Officer:~w~ Thanks, you're gonna need it."
		};

		
		private readonly List<string> OpeningDialogue2 = new List<string>
		{
			"~g~You:~w~ Hey, what's going on?",
			"~b~Officer:~w~ A damn sovereign citizen. Always dreaded the day I'd have to deal with an idiot like this.",
			"~b~Officer:~w~ I stopped him for failing to signal a turn. He keeps talking nonsense and refusing to cooperate with the stop.",
			"~b~Officer:~w~ I can't deal with this guy anymore, so maybe you can take a whirl at it.",
			"~g~You:~w~ I'll do my best, although I don't know how well it'll go.",
			"~b~Officer:~w~ Can't be worse than me, that's for sure. I'll back you up."
		};

		
		private readonly List<string> CooperatesOpening1 = new List<string>
		{
			"~g~You:~w~ Thank you. I'm here because you haven't been cooperative with my partner's investigation so far.",
			"~r~Suspect:~w~ I will not cooperate with an unlawful detainment officer. You should know that, after all you are the expert in the law here.",
			"~g~You:~w~ And what's so unlawful about any of this that we are doing here?",
			"~r~Suspect:~w~ I haven't committed any crime, you officers have no business interfering with my right to travel if I haven't done anything wrong."
		};

		
		private readonly List<string> CooperatesOpening2 = new List<string>
		{
			"~g~You:~w~ Thank you. I'm here because you haven't been cooperating with my partner.",
			"~r~Suspect:~w~ I refuse to cooperate with an unlawful detainment like this. You should know that, after all you are the expert in the law here.",
			"~g~You:~w~ And what's so unlawful about any of this that we are doing here?",
			"~r~Suspect:~w~ I haven't committed any crime, you officers have no business interfering with my right to travel if I haven't broken any law."
		};

		
		private readonly List<string> CooperatesReason1 = new List<string>
		{
			"~g~You:~w~ You committed a traffic violation, which gives us probable cause to pull you over. We haven't violated any of your rights.",
			"~r~Suspect:~w~ I know my rights! You officers have no power to harass me and ask for my id!",
			"~g~You:~w~ We actually do, you're operating a vehicle on a public roadway, you must surrender your license and proof of insurance to law enforcement.",
			"~r~Suspect:~w~ I'm not driving, I'm travelling you stupid cop! Stop violating my rights and wasting my time!",
			"~g~You:~w~ Listen, you're going to either cooperate, or things are going to get much worse. Your decision. Either identify yourself or go to jail.",
			"~r~Suspect:~w~ I want your name and badge number officer. And get your supervisor out here too while you're at it."
		};

		
		private readonly List<string> CooperatesReason2 = new List<string>
		{
			"~g~You:~w~ Okay, I'm going to do my best to explain this to you. You committed a traffic violation, which gives us probable cause to pull you over. Nobody has violated any rights here.",
			"~r~Suspect:~w~ Iou detained me without cause, violating my right to travel officer. Of course you violated my rights, and I don't have to give you my id either!",
			"~g~You:~w~ You actually do, you're operating a vehicle on a public roadway, you must surrender your license and proof of insurance to law enforcement.",
			"~r~Suspect:~w~ I'm not driving, I'm travelling you stupid cop! Stop violating my rights and wasting my time!",
			"~g~You:~w~ Listen, you're going to either cooperate, or things are go to jail. Your decision.",
			"~r~Suspect:~w~ I want your name and badge number officer. And get your supervisor out here too while you're at it. I want to file a formal complaining with your shitty department."
		};

		
		private readonly List<string> Scold1 = new List<string>
		{
			"~g~You:~w~ Okay listen. I'm not going to waste any more time with you, understand? You are required by law to identify yourself on a traffic stop, this is your final chance.",
			"~r~Suspect:~w~ I know my rights! You officers have no power to harass me and ask for my id!",
			"~g~You:~w~ You act like you know every single law in the book, but unfortunately there is no cure for stupidity no matter how many laws you memorize.",
			"~g~You:~w~ I'm done with you, you've wasted enough of my time and that officer's time. Stay right here you piece of shit.",
			"~r~Suspect:~w~ I want your name and badge number officer, that is no way to talk to a taxpayer! I pay your salary!"
		};

		
		private readonly List<string> Scold2 = new List<string>
		{
			"~g~You:~w~ Okay listen. I'm not wasting any more time arguing with you, understand? You are required by law to identify yourself on a traffic stop, this is your final chance.",
			"~r~Suspect:~w~ I know my rights! You officers have no power to harass me and ask for my id! You are a disgrace!",
			"~g~You:~w~ We'll see who's a disgrace once I arrest your ignorant ass for obstruction.",
			"~g~You:~w~ Stay in your car while I sort this out.",
			"~r~Suspect:~w~ I want your name and badge number officer, that is no way to talk to a citizen! I pay your salary!"
		};

		
		private readonly List<string> Refuses1 = new List<string>
		{
			"~g~You:~w~ Okay listen. I'm not going to waste any more time with you, understand? You are required by law to identify yourself on a traffic stop, this is your final chance.",
			"~r~Suspect:~w~ I know my rights! You officers have no power to harass me and ask for my id, let alone tell me what to do!",
			"~g~You:~w~ This is your final warning to comply with my instructions!",
			"~r~Suspect:~w~ I want your name and badge number officer, that is no way to treat to a citizen in a free country! I pay your salary!"
		};

		
		private readonly List<string> Refuses2 = new List<string>
		{
			"~g~You:~w~ listen. I'm not wasting any more time arguing with you, understand? This is your final warning to cooperate with my instructions.",
			"~r~Suspect:~w~ I told you already, I will not cooperate with unlawful commands!",
			"~g~You:~w~ You're only making this worse for yourself.",
			"~r~Suspect:~w~ I want your name and badge number officer, I'll make things worse for you!"
		};
	}
}
