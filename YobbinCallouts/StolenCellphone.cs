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
	
	[CalloutInfo("Stolen Cellphone", 3)]
	public class StolenCellPhone : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Stolen Cellphone Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 5);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario is Value is " + this.MainScenario.ToString());
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).RealAreaName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			CallHandler.locationChooser(CallHandler.HouseList, 600f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			bool result;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
				base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 75f);
				base.AddMinimumDistanceCheck(50f, this.MainSpawnPoint);
				Functions.PlayScannerAudio("CITIZENS_REPORT YC_STOLEN_PROPERTY");
				base.CalloutMessage = "Stolen Cellphone";
				base.CalloutPosition = this.MainSpawnPoint;
				base.CalloutAdvisory = "Caller Has Reported a ~r~Cellphone Stolen.";
				result = base.OnBeforeCalloutDisplayed();
			}
			else
			{
				Game.LogTrivial("YOBBINCALLOUTS: Could not find suitable house for callout location. Aborting Callout.");
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Stolen Cellphone Callout Accepted by User.");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~b~Code 2~w~.");
			}
			this.House = new Blip(this.MainSpawnPoint, 35f);
			this.House.IsRouteEnabled = true;
			this.House.Color = Color.Yellow;
			this.House.Alpha = 0.67f;
			this.House.Name = "Caller";
			this.Victim = new Ped(this.MainSpawnPoint, 69f);
			this.Victim.IsPersistent = true;
			this.Victim.BlockPermanentEvents = true;
			this.Victim.IsInvincible = true;
			this.VictimModel = this.Victim.Model;
			bool displayHelp = Config.DisplayHelp;
			if (displayHelp)
			{
				bool locationReturned = CallHandler.locationReturned;
				if (locationReturned)
				{
					Game.DisplayHelp("Go to the ~y~Property~w~ Shown on The Map to Investigate.");
				}
				else
				{
					Game.DisplayHelp("Go to the ~y~Caller~w~ Shown on The Map to Investigate.");
				}
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
			Game.LogTrivial("YOBBINCALLOUTS: Stolen Cellphone Callout Not Accepted by User.");
			base.OnCalloutNotAccepted();
		}

		
		private void Callout()
		{
			this.CalloutRunning = true;
			try
			{
				GameFiber.StartNew(delegate()
				{
					while (this.CalloutRunning)
					{
						while (this.player.DistanceTo(this.Victim) >= 35f && !Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(Config.CalloutEndKey);
						if (flag)
						{
							EndCalloutHandler.CalloutForcedEnd = true;
							break;
						}
						this.House.Delete();
						this.VictimBlip = this.Victim.AttachBlip();
						this.VictimBlip.IsFriendly = true;
						this.VictimBlip.Scale = 0.75f;
						Game.DisplayHelp("Talk to the ~b~Caller.");
						if (StolenCellPhone.<>o__27.<>p__0 == null)
						{
							StolenCellPhone.<>o__27.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(StolenCellPhone), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						StolenCellPhone.<>o__27.<>p__0.Target(StolenCellPhone.<>o__27.<>p__0, NativeFunction.Natives, this.Victim, this.player, -1);
						while (this.player.DistanceTo(this.Victim) >= 5f)
						{
							GameFiber.Wait(0);
						}
						bool displayHelp = Config.DisplayHelp;
						if (displayHelp)
						{
							Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to talk to the ~b~Caller.");
						}
						CallHandler.Dialogue(this.OpeningDialogue1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						GameFiber.Wait(1500);
						this.Victim.Tasks.ClearImmediately();
						Game.DisplayHelp(string.Concat(new string[]
						{
							"~y~",
							Config.Key1.ToString(),
							":~b~ Of Course, I'll Try to Find Your Phone. ~y~",
							Config.Key2.ToString(),
							": ~b~That's a Waste of Time, I'll Just Take Your Statement."
						}));
						while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
						{
							GameFiber.Wait(0);
						}
						bool flag2 = Game.IsKeyDown(Config.Key1);
						if (flag2)
						{
							bool displayHelp2 = Config.DisplayHelp;
							if (displayHelp2)
							{
								Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Continue Speaking With the ~b~Caller.");
							}
							CallHandler.Dialogue(this.Accept, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							GameFiber.Wait(1500);
							this.Victim.Tasks.ClearImmediately();
							bool flag3 = EntityExtensions.Exists(this.VictimBlip);
							if (flag3)
							{
								this.VictimBlip.Delete();
							}
							CallHandler.IdleAction(this.Victim, false);
							Game.DisplayHelp("Return to your ~b~Vehicle~w~ to Start ~o~Tracking ~w~the~y~ Phone.");
							this.TrackPhone();
						}
						else
						{
							this.End();
						}
					}
				});
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
			bool flag = EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				this.Suspect.Dismiss();
			}
			bool flag2 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag2)
			{
				this.SuspectBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.House);
			if (flag3)
			{
				this.House.Delete();
			}
			bool flag4 = EntityExtensions.Exists(this.Phone);
			if (flag4)
			{
				this.Phone.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this.Victim);
			if (flag5)
			{
				this.Victim.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this.VictimBlip);
			if (flag6)
			{
				this.VictimBlip.Delete();
			}
			bool flag7 = EntityExtensions.Exists(this.CellPhoneAreaBlip);
			if (flag7)
			{
				this.CellPhoneAreaBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Stolen Cellphone Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void TrackPhone()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				while (!this.player.IsInAnyVehicle(false))
				{
					GameFiber.Wait(0);
				}
				Vector3 SuspectSpawn = World.GetNextPositionOnStreet(this.player.Position.Around(550f));
				this.Suspect = new Ped(SuspectSpawn, 69f);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.Phone = new Object("prop_npc_phone", Vector3.Zero);
				this.Phone.AttachTo(this.Suspect, this.Suspect.GetBoneIndex(18905), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
				this.Phone.IsPersistent = true;
				this.Suspect.Tasks.Wander();
				this.CellPhoneAreaBlip = new Blip(this.Suspect.Position.Around(15f), 100f);
				this.CellPhoneAreaBlip.Color = Color.Yellow;
				this.CellPhoneAreaBlip.Alpha = 0.67f;
				this.CellPhoneAreaBlip.IsRouteEnabled = true;
				bool flag = EntityExtensions.Exists(this.Suspect);
				if (flag)
				{
					while (this.player.DistanceTo(this.Suspect) >= 50f)
					{
						GameFiber.Wait(0);
					}
				}
				Game.DisplayNotification("You are ~g~Close~w~ to the ~y~Phone!~w~ Enabling ~b~Fine Location~w~ Information.");
				bool flag2 = EntityExtensions.Exists(this.CellPhoneAreaBlip);
				if (flag2)
				{
					this.CellPhoneAreaBlip.Delete();
				}
				this.CellPhoneAreaBlip = this.Suspect.AttachBlip();
				this.CellPhoneAreaBlip.IsFriendly = false;
				this.CellPhoneAreaBlip.Scale = 0.75f;
				while (this.player.DistanceTo(this.Suspect) >= 5f)
				{
					GameFiber.Wait(0);
				}
				Game.DisplaySubtitle("~g~You:~w~ Hey, Could I Speak With You for a Sec?", 3000);
				GameFiber.Wait(3000);
				this.Suspect.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to talk to the ~r~Suspect.");
				}
				bool flag3 = this.MainScenario == 0;
				if (flag3)
				{
					this.Cooperates();
				}
				else
				{
					bool flag4 = this.MainScenario == 1 || this.MainScenario == 2;
					if (flag4)
					{
						this.Denies();
					}
					else
					{
						bool flag5 = this.MainScenario == 3;
						if (flag5)
						{
							this.Runs();
						}
					}
				}
			}
		}

		
		private void Cooperates()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				CallHandler.Dialogue(this.SuspectCoop, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				this.Decide();
			}
		}

		
		private void Denies()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				CallHandler.Dialogue(this.SuspectDenies, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				GameFiber.Wait(1500);
				bool flag = this.MainScenario == 2;
				if (flag)
				{
					this.Decide();
				}
				else
				{
					this.Runs();
				}
			}
		}

		
		private void Runs()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
				{
					this.Suspect
				});
				bool flag = EntityExtensions.Exists(this.SuspectBlip);
				if (flag)
				{
					this.SuspectBlip.Delete();
				}
				this.WrapUp();
			}
		}

		
		private void Decide()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				GameFiber.Wait(1500);
				this.Suspect.Tasks.ClearImmediately();
				this.Phone.IsVisible = true;
				this.Suspect.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
				GameFiber.Wait(1000);
				this.Suspect.Tasks.PlayAnimation("mp_common", "givetake1_b", 1f, 1);
				this.Phone.Detach();
				this.Phone.AttachTo(this.player, this.player.GetBoneIndex(57005), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
				GameFiber.Wait(1000);
				this.Suspect.Tasks.ClearImmediately();
				GameFiber.Wait(1500);
				this.Phone.Delete();
				GameFiber.Wait(2000);
				Game.DisplayHelp(string.Concat(new string[]
				{
					"~y~",
					Config.Key1.ToString(),
					":~b~ Arrest the Suspect.~y~ ",
					Config.Key2.ToString(),
					":~b~ Let the Suspect Off the Hook."
				}));
				while (!Game.IsKeyDown(Config.Key1) && !Game.IsKeyDown(Config.Key2))
				{
					GameFiber.Wait(0);
				}
				bool flag = Game.IsKeyDown(Config.Key1);
				if (flag)
				{
					bool displayHelp = Config.DisplayHelp;
					if (displayHelp)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Continue Talking to the ~r~Suspect.");
					}
					GameFiber.Yield();
					CallHandler.Dialogue(this.SuspectCoopArrest, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					GameFiber.Wait(1500);
					this.Suspect.Tasks.ClearImmediately();
					Game.DisplayHelp("Arrest the ~r~Suspect.");
					bool flag2 = this.MainScenario == 4;
					if (flag2)
					{
						GameFiber.Wait(500);
						this.Runs();
					}
					else
					{
						CallHandler.SuspectWait(this.Suspect);
						bool flag3 = EntityExtensions.Exists(this.SuspectBlip);
						if (flag3)
						{
							this.SuspectBlip.Delete();
						}
						this.WrapUp();
					}
				}
				else
				{
					bool displayHelp2 = Config.DisplayHelp;
					if (displayHelp2)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Continue Talking to the ~r~Suspect.");
					}
					bool flag4 = EntityExtensions.Exists(this.Suspect);
					if (flag4)
					{
						CallHandler.Dialogue(this.SuspectCoopLetGo, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					}
					GameFiber.Wait(1500);
					bool flag5 = EntityExtensions.Exists(this.Suspect);
					if (flag5)
					{
						this.Suspect.Tasks.ClearImmediately();
						bool isAlive = this.Suspect.IsAlive;
						if (isAlive)
						{
							this.Suspect.Dismiss();
						}
					}
					bool flag6 = EntityExtensions.Exists(this.SuspectBlip);
					if (flag6)
					{
						this.SuspectBlip.Alpha = 0f;
					}
					bool flag7 = EntityExtensions.Exists(this.SuspectBlip);
					if (flag7)
					{
						this.SuspectBlip.Delete();
					}
					this.WrapUp();
				}
			}
		}

		
		private void WrapUp()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = EntityExtensions.Exists(this.Phone);
				if (flag)
				{
					this.Phone.Delete();
				}
				bool flag2 = EntityExtensions.Exists(this.SuspectBlip);
				if (flag2)
				{
					this.SuspectBlip.Delete();
				}
				GameFiber.Wait(1500);
				Game.DisplayHelp("Take the Phone Back to the ~b~Caller.");
				bool flag3 = !EntityExtensions.Exists(this.Victim);
				if (flag3)
				{
					this.Victim = new Ped(this.VictimModel, this.MainSpawnPoint, 69f);
				}
				this.VictimBlip = this.Victim.AttachBlip();
				this.VictimBlip.IsFriendly = true;
				this.VictimBlip.IsRouteEnabled = true;
				this.VictimBlip.Scale = 0.75f;
				bool calloutRunning2 = this.CalloutRunning;
				if (calloutRunning2)
				{
					while (this.player.DistanceTo(this.Victim) >= 5f)
					{
						GameFiber.Wait(0);
					}
				}
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Return the Phone ~b~Caller.");
				}
				CallHandler.Dialogue(this.VictimEnding1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
				this.Phone = new Object("prop_npc_phone", Vector3.Zero);
				this.Phone.IsPersistent = true;
				GameFiber.Wait(1000);
				this.player.Tasks.PlayAnimation("mp_common", "givetake1_b", 1f, 1);
				this.Phone.Detach();
				this.Phone.AttachTo(this.player, this.player.GetBoneIndex(57005), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
				GameFiber.Wait(1000);
				this.player.Tasks.ClearImmediately();
				GameFiber.Wait(1500);
				this.Phone.Delete();
				GameFiber.Wait(2000);
				this.Victim.Dismiss();
				Game.LogTrivial("YOBBINCALLOUTS: Callout Finished, Ending...");
				EndCalloutHandler.EndCallout();
				this.End();
			}
		}

		
		private Vector3 MainSpawnPoint;

		
		private Blip House;

		
		private Blip SuspectBlip;

		
		private Blip VictimBlip;

		
		private Blip CellPhoneAreaBlip;

		
		private Ped Victim;

		
		private Ped Suspect;

		
		private Object Phone;

		
		private LHandle SuspectPursuit;

		
		private int MainScenario;

		
		private string Zone;

		
		private Model VictimModel;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private LHandle MainPursuit;

		
		private bool CalloutRunning = false;

		
		private bool IsHouse = true;

		
		private readonly List<string> OpeningDialogue1 = new List<string>
		{
			"~b~Caller:~w~ How are you doing, Officer?",
			"~g~You:~w~ Not too bad, thanks. What's the issue here?",
			"~b~Caller:~w~ Somebody stole my cell phone a few hours ago. I didn't see who took it, unfortunately.",
			"~b~Caller:~w~ However, they just turned it on, and it can be tracked using the phone's tracking app.",
			"~b~Caller:~w~ Would you be able to locate who took my phone? I really need it."
		};

		
		private readonly List<string> Accept = new List<string>
		{
			"~g~You:~w~ Yeah of course, we can see if we'll be able to locate your property.",
			"~b~Caller:~w~ Great, I have some important information on that phone that I really need back. Let me get you the information.",
			"~b~Caller:~w~ You can use it on your phone's app to locate where the phone is.",
			"~g~You:~w~ Alright, hopefully I'll be able to find it. I'll let you know if I do."
		};

		
		private readonly List<string> Decline = new List<string>
		{
			"~b~Caller:~w~ How are you doing, officer?",
			"~g~You:~w~ Not too bad, thanks. What's the issue here?",
			"~b~Caller:~w~ Somebody stole my cell phone a few hours ago. I didn't see who took it, unfortunately.",
			"~b~Caller:~w~ However, they just turned it on, and it can be tracked using the phone's tracking app.",
			"~b~Caller:~w~ Would you be able to locate who took my phone? I'd like to press charges."
		};

		
		private readonly List<string> SuspectCoop = new List<string>
		{
			"~r~Suspect:~w~ Oh, hello officer, what's wrong?",
			"~g~You:~w~ I'm conducting an investigation into a stolen phone. The phone appears to be on your person according to the tracking app I have.",
			"~g~You:~w~ If you're honest with me, it'll make this go a lot easier. Do you have a stolen phone in your posession?",
			"~r~Suspect:~w~ Alright officer, I ain't gonna lie to you, yeah I have the phone. Stole it a couple hours ago.",
			"~r~Suspect:~w~ Things are tough, though I could get away with it. Guess I'm new at this, because I forgot to turn it off.",
			"~g~You:~w~ I appreciate you being honest with me. Could you hand me the phone please?"
		};

		
		private readonly List<string> SuspectCoopArrest = new List<string>
		{
			"~g~You:~w~ I appreciate you being cooperative, however unfortunately I'm going to have to book you for this.",
			"~r~Suspect:~w~ I understand officer. That was a poor decision on my part.",
			"~g~You:~w~ Hands behind your back, please."
		};

		
		private readonly List<string> SuspectCoopLetGo = new List<string>
		{
			"~r~Suspect:~w~ Are you going to arrest me now, officer?",
			"~g~You:~w~ I normally would, but you've been very cooperative with my investigation. This seems like your first time doing something like this as well.",
			"~g~You:~w~ I'm going to let you off the hook, but please learn from this experience.",
			"~r~Suspect:~w~ I absolutely will officer. Thanks so much for your understanding, I really appreciate it.",
			"~g~You:~w~ Alright, take care now."
		};

		
		private readonly List<string> SuspectDenies = new List<string>
		{
			"~r~Suspect:~w~ Oh, hello officer, what's wrong?",
			"~g~You:~w~ I'm conducting an investigation into a stolen phone. The phone appears to be on your person according to the tracking app I have.",
			"~g~You:~w~ If you're honest with me, it'll make this go a lot easier. Do you have a stolen phone in your posession?",
			"~r~Suspect:~w~ I didn't do anything officer. Please stop harassing me.",
			"~g~You:~w~ I have the victim's phone tracking right here. Their phone is clearly showing up as on your person.",
			"~g~You:~w~ Now, please don't make this harder than it has to be. Could I have the phone, please?"
		};

		
		private readonly List<string> VictimEnding1 = new List<string>
		{
			"~b~Caller:~w~ Did you find it Officer?",
			"~g~You:~w~ I did. Here you go!",
			"~b~Caller:~w~ Awesome! Thanks so much, I really needed it!"
		};
	}
}
