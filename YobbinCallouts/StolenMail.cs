using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Stolen Mail", 3)]
	public class StolenMail : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Explosion Callout Start==========");
			this.MainScenario = this.monke.Next(0, 0);
			Game.LogTrivial("YOBBINCALLOUTS: Scenario Number is " + this.MainScenario.ToString());
			Game.LogTrivial("Getting Location");
			CallHandler.locationChooser(CallHandler.HouseList, 600f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			bool result;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
				string str = "Spawnpoint vector is ";
				Vector3 mainSpawnPoint = this.MainSpawnPoint;
				Game.LogTrivial(str + mainSpawnPoint.ToString());
				base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 25f);
				Functions.PlayScannerAudio("CITIZENS_REPORT YC_STOLEN_PROPERTY");
				base.CalloutMessage = "Stolen Mail";
				base.CalloutPosition = this.MainSpawnPoint;
				base.CalloutAdvisory = "RP states that he has not gotten mail in several days. He thinks that his mail is being stolen.";
				result = base.OnBeforeCalloutDisplayed();
			}
			else
			{
				Game.LogTrivial("No Location found. Ending Callout");
				result = false;
			}
			return result;
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("Callout Not Accepted by User.");
			base.OnCalloutNotAccepted();
		}

		
		public override bool OnCalloutAccepted()
		{
			try
			{
				Game.LogTrivial("Callout accepted by user");
				bool calloutInterface = Main.CalloutInterface;
				if (calloutInterface)
				{
					CalloutInterfaceHandler.SendCalloutDetails(this, "Code 2", "");
				}
				else
				{
					Game.DisplayNotification("Respond ~b Code 2.");
				}
				this.HouseOwner = new Ped(this.MainSpawnPoint.Around(2f));
				this.HouseOwner.IsPersistent = true;
				this.HouseOwner.BlockPermanentEvents = true;
				this.HouseOwnerBlip = CallHandler.AssignBlip(this.HouseOwner, Color.Blue, 0.69f, "Caller", true, 1f);
				if (StolenMail.<>o__20.<>p__0 == null)
				{
					StolenMail.<>o__20.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(StolenMail), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				StolenMail.<>o__20.<>p__0.Target(StolenMail.<>o__20.<>p__0, NativeFunction.Natives, this.HouseOwner, this.player, -1);
				Game.DisplayHelp("Speak with RP.");
			}
			catch (Exception e)
			{
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT ON CALLOUT INTIALIZATION==========");
				Game.LogTrivial("IN: " + ((this != null) ? this.ToString() : null));
				string error = e.ToString();
				Game.LogTrivial("ERROR: " + error);
				Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Chck Your ~g~Log File.~w~ Sorry for the Inconvenience!");
				Game.DisplayNotification("Error: ~r~" + error);
				Game.LogTrivial("If You Believe this is a Bug, Please Report it on my Discord Server. Thanks!");
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT ON CALLOUT INTIALIZATION==========");
			}
			int num = this.monke.Next(0, 101);
			bool flag = num <= 10;
			if (flag)
			{
				this.FalseAlarm();
			}
			else
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
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
						while (Vector3.Distance(this.player.Position, this.HouseOwner.Position) >= 25f && !Game.IsKeyDown(this.EndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(this.EndKey);
						if (flag)
						{
							break;
						}
						CallHandler.IdleAction(this.HouseOwner, false);
						while (Vector3.Distance(this.player.Position, this.HouseOwner.Position) >= 7.5f)
						{
							GameFiber.Wait(0);
						}
						Game.DisplaySubtitle("~g~You:~w~ Hello Sir. Did you call about your mail being stolen.");
						this.HouseOwnerBlip.IsRouteEnabled = false;
						this.HouseOwner.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
						bool displayHelp = Config.DisplayHelp;
						if (displayHelp)
						{
							Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to speak with the ~b~Landlord.");
						}
						Vector3 SuspectSpawn = World.GetNextPositionOnStreet(this.player.Position.Around(550f));
						this.Suspect = new Citizen(SuspectSpawn, 69f);
						this.Suspect.IsPersistent = true;
						this.Suspect.BlockPermanentEvents = true;
						this.Suspect.Tasks.Wander();
						CallHandler.Dialogue(this.HouseOwnerDialogue, this.HouseOwner, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						Game.DisplaySubtitle("You: Really, you cannot tell me anything about the suspect? I cannot guarantee I will be able to find him, but I will try my best.");
						bool displayHelp2 = Config.DisplayHelp;
						if (displayHelp2)
						{
							Game.DisplayNotification("Press " + this.InteractionKey.ToString() + " to continue dialogue");
						}
						while (!Game.IsKeyDown(this.InteractionKey))
						{
							GameFiber.Wait(0);
						}
						Game.DisplaySubtitle("Home Owner: Umm...the person ran off....before I could remember any of that. I think the person was " + this.Suspect.Gender + " and was holding my mail while wandering off.");
						while (!Game.IsKeyDown(this.InteractionKey))
						{
							GameFiber.Wait(0);
						}
						Game.DisplaySubtitle("You: Ok. Will try my best. Thank you.");
						this.SearchArea = new Blip(this.Suspect.Position.Around(15f), 50f);
						this.FindSuspect();
					}
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
						Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Chck Your ~g~Log File.~w~ Sorry for the Inconvenience!");
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

		
		private void FalseAlarm()
		{
			this.CalloutRunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.CalloutRunning)
					{
						while (Vector3.Distance(this.player.Position, this.HouseOwner.Position) >= 25f && !Game.IsKeyDown(this.EndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(this.EndKey);
						if (flag)
						{
							break;
						}
						CallHandler.IdleAction(this.HouseOwner, false);
						while (Vector3.Distance(this.player.Position, this.HouseOwner.Position) >= 7.5f)
						{
							GameFiber.Wait(0);
						}
						Game.DisplaySubtitle("~g~You:~w~ Hello Sir. Did you call about your mail being stolen.");
						this.HouseOwnerBlip.IsRouteEnabled = false;
						this.HouseOwner.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
						bool displayHelp = Config.DisplayHelp;
						if (displayHelp)
						{
							Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to speak with the ~b~Landlord.");
						}
						CallHandler.Dialogue(this.HouseOwnerFalseAlarmDialogue, this.HouseOwner, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						GameFiber.Wait(1500);
						Game.DisplayNotification("Dispatch, It was a ~g~False Alarm~w~. I will be ~g~Code 4~w~.");
						GameFiber.Wait(2000);
						Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
						GameFiber.Wait(2000);
						this.End();
					}
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
						Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Chck Your ~g~Log File.~w~ Sorry for the Inconvenience!");
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

		
		private void DetachAndSetBlip()
		{
			bool flag = EntityExtensions.Exists(this.Mail);
			if (flag)
			{
				this.Mail.Detach();
				this.DroppedMail = this.Mail.Position;
				this.DroppedMailBlip = CallHandler.AssignBlip(this.Mail, Color.Blue, 0.4f, "", false, 1f);
			}
		}

		
		private void FindSuspect()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				this.Mail = new Object("prop_cs_envolope_01", Vector3.Zero);
				this.Mail.IsPersistent = true;
				this.Mail.AttachTo(this.Suspect, this.Suspect.GetBoneIndex(18905), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
				while (!this.player.IsInAnyVehicle(false) && Vector3.Distance(this.player.Position, this.Suspect.Position) >= 10f)
				{
					GameFiber.Wait(0);
				}
				bool flag = EntityExtensions.Exists(this.SearchArea);
				if (flag)
				{
					this.SearchArea.Delete();
				}
				this.SuspectBlip = CallHandler.AssignBlip(this.Suspect, Color.Red, 0.69f, "", false, 1f);
				while (this.player.DistanceTo(this.Suspect) >= 5f)
				{
					GameFiber.Wait(0);
				}
				Game.DisplaySubtitle("You: Hey, Could I Speak With You for a Sec?", 3000);
				bool flag2 = CallHandler.FiftyFifty();
				if (flag2)
				{
					this.Cooperates();
				}
				else
				{
					bool flag3 = CallHandler.FiftyFifty();
					if (flag3)
					{
						this.Runs();
					}
					else
					{
						this.Shoots();
					}
				}
			}
		}

		
		private void Cooperates()
		{
			CallHandler.Dialogue(this.SuspectDialogue, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
			this.DetachAndSetBlip();
			bool displayHelp = Config.DisplayHelp;
			if (displayHelp)
			{
				Game.DisplayNotification("Arrest the suspect");
			}
			while (EntityExtensions.Exists(this.Suspect) && !this.Suspect.IsCuffed)
			{
				GameFiber.Wait(0);
			}
			this.WrapUp();
		}

		
		private void Runs()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
				this.DetachAndSetBlip();
				LHandle SuspectPursuit = Functions.CreatePursuit();
				this.Suspect.Inventory.Weapons.Clear();
				Functions.SetPursuitIsActiveForPlayer(SuspectPursuit, true);
				Functions.AddPedToPursuit(SuspectPursuit, this.Suspect);
				while (Functions.IsPursuitStillRunning(SuspectPursuit))
				{
					GameFiber.Wait(0);
				}
				while (EntityExtensions.Exists(this.Suspect))
				{
					GameFiber.Yield();
					bool flag = !EntityExtensions.Exists(this.Suspect) || this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect);
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
						Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Following the Pursuit.");
					}
				}
				bool flag4 = EntityExtensions.Exists(this.SuspectBlip);
				if (flag4)
				{
					this.SuspectBlip.Delete();
				}
				GameFiber.Wait(2000);
				Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
				GameFiber.Wait(2000);
				bool flag5 = EntityExtensions.Exists(this.SuspectBlip);
				if (flag5)
				{
					this.SuspectBlip.Delete();
				}
				this.WrapUp();
			}
		}

		
		private void Shoots()
		{
			this.DetachAndSetBlip();
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_APPISTOL", -1, true);
			this.Suspect.Tasks.GoToWhileAiming(World.GetNextPositionOnStreet(this.Suspect.Position.Around(550f)), this.player.Position, 5f, 5f, true, -957453492).WaitForCompletion(1500);
			while (EntityExtensions.Exists(this.Suspect) && this.Suspect.IsAlive)
			{
				GameFiber.Wait(0);
			}
			bool flag = !EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				Game.DisplayNotification("Dispatch, a Suspect was ~r~killed~w~ following a foot chase and a shootout.");
			}
			GameFiber.Wait(2000);
			Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
			GameFiber.Wait(2000);
			bool flag2 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag2)
			{
				this.SuspectBlip.Delete();
			}
			this.WrapUp();
		}

		
		private void WrapUp()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Game.LogTrivial("Starting Wrap Up Method");
				bool displayHelp = Config.DisplayHelp;
				if (displayHelp)
				{
					Game.DisplayNotification("Retrieve the mail");
				}
				while (Vector3.Distance(this.player.Position, this.DroppedMail) >= 5f)
				{
					GameFiber.Wait(0);
				}
				bool displayHelp2 = Config.DisplayHelp;
				if (displayHelp2)
				{
					Game.DisplayNotification("Press " + this.InteractionKey.ToString() + " in order to retrieve the mail");
				}
				while (!Game.IsKeyDown(this.InteractionKey))
				{
					GameFiber.Wait(0);
				}
				this.player.Tasks.PlayAnimation("amb@medic@standing@kneel@idle_a", "idle_b", 1f, 1);
				GameFiber.Wait(1000);
				this.Mail.AttachTo(this.player, this.player.GetBoneIndex(18905), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
				GameFiber.Wait(1000);
				this.player.Tasks.Clear();
				GameFiber.Wait(1000);
				bool displayHelp3 = Config.DisplayHelp;
				if (displayHelp3)
				{
					Game.DisplayNotification("Go return mail to home owner.");
				}
				this.HouseOwnerBlip.IsRouteEnabled = true;
				while (Vector3.Distance(this.player.Position, this.HouseOwner.Position) >= 7.5f)
				{
					GameFiber.Wait(0);
				}
				this.HouseOwner.Tasks.AchieveHeading(this.player.Heading - 180f).WaitForCompletion(500);
				Game.DisplaySubtitle("You: I have retrieved your mail sir.");
				bool displayHelp4 = Config.DisplayHelp;
				if (displayHelp4)
				{
					Game.DisplayNotification("Press + " + this.InteractionKey.ToString() + " to return mail.");
				}
				while (!Game.IsKeyDown(this.InteractionKey))
				{
					GameFiber.Wait(0);
				}
				this.player.Tasks.PlayAnimation("mp_common", "givetake1_b", 1f, 1);
				GameFiber.Wait(1000);
				this.Mail.AttachTo(this.HouseOwner, this.HouseOwner.GetBoneIndex(18905), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
				GameFiber.Wait(1000);
				this.player.Tasks.Clear();
				GameFiber.Wait(1000);
				Game.DisplaySubtitle("Home Owner: Thank you so much officer.");
				GameFiber.Wait(2000);
				Game.DisplaySubtitle("You: No worries");
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
			bool flag = EntityExtensions.Exists(this.SuspectBlip);
			if (flag)
			{
				this.SuspectBlip.Delete();
			}
			bool flag2 = EntityExtensions.Exists(this.HouseOwnerBlip);
			if (flag2)
			{
				this.HouseOwnerBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.Suspect);
			if (flag3)
			{
				this.Suspect.Dismiss();
			}
			bool flag4 = EntityExtensions.Exists(this.Mail);
			if (flag4)
			{
				this.Mail.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this.HouseOwner);
			if (flag5)
			{
				this.HouseOwner.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this.SearchArea);
			if (flag6)
			{
				this.SearchArea.Delete();
			}
			bool flag7 = EntityExtensions.Exists(this.DroppedMailBlip);
			if (flag7)
			{
				this.DroppedMailBlip.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Stolen Mail Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private Vector3 MainSpawnPoint;

		
		private Citizen Suspect;

		
		private Ped HouseOwner;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private Blip HouseOwnerBlip;

		
		private Blip SuspectBlip;

		
		private Blip SearchArea;

		
		private Vector3 DroppedMail;

		
		private Blip DroppedMailBlip;

		
		private int MainScenario;

		
		private Object Mail;

		
		private bool CalloutRunning;

		
		private List<string> HouseOwnerDialogue = new List<string>
		{
			"Home Owner: Hello Officer. Yes, I was the one who called about my mail being stolen. Thank you for coming.",
			"You: How long has this been happening for?",
			"Home Owner: This has been happening for a week now.",
			"You: Why didn't you call us before?",
			"Home Owner: I thought the mail company took a break.",
			"You: That is an excuse for being lazy if I have ever heard of one. Anyways, do you have any description of the person?",
			"Home Owner: No."
		};

		
		private List<string> HouseOwnerFalseAlarmDialogue = new List<string>
		{
			"Home Owner: Hello Officer. Sorry for the trouble. My mail was just put on hold for a week longer than it should have by the mail company because our vacation was cut short.",
			"You: So you are getting your mail?",
			"Home Owner: Yes. It was a false alarm. Sorry about that.",
			"You: No worries. Please make sure you contact 911 only for emergencies.",
			"Home Owner: Sorry about that. Will do. Stay safe"
		};

		
		private List<string> SuspectDialogue = new List<string>
		{
			"Suspect: Watchu want",
			"You: Hey, just want to talk to you. Whats in your hand there?",
			"Suspect: Watchu think it is, mail. punk ass",
			"You: Where did you get that mail from?",
			"Suspect: Places.....",
			"You: I am going to go straight to the point if you straight with me, aight. We got a call about someone stealing mail. You are the only one in this vicinity that is walking around with mail. Did you steal it",
			"Suspect: fine man, you got me...good job sherlock",
			"You: Do you got any weapons on you?",
			"Suspect: nah man, who the fuck you think I am. Don't piss me off 'fore I beat yo ass up."
		};

		
		private Keys EndKey = Config.CalloutEndKey;

		
		private Keys InteractionKey = Config.MainInteractionKey;

		
		private Random monke = new Random();
	}
}
