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
	
	[CalloutInfo("Photography of Private Property", 2)]
	public class PhotographyOfPrivateProperty : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Photography of Private Property Callout Start==========");
			Random r = new Random();
			int Scenario = r.Next(0, 0);
			Scenario = this.MainScenario;
			this.Zone = Functions.GetZoneAtPosition(Game.LocalPlayer.Character.Position).GameName;
			Game.LogTrivial("YOBBINCALLOUTS: Zone is " + this.Zone);
			CallHandler.locationChooser(CallHandler.HouseList, 600f, 25f);
			bool locationReturned = CallHandler.locationReturned;
			if (locationReturned)
			{
				this.MainSpawnPoint = CallHandler.SpawnPoint;
			}
			else
			{
				this.MainScenario = 1;
				this.MainSpawnPoint = World.GetNextPositionOnStreet(this.player.Position.Around(550f));
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 75f);
			base.AddMinimumDistanceCheck(25f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("CITIZENS_REPORT CRIME_DISTURBING_THE_PEACE_01");
			base.CalloutMessage = "Photography of Private Property";
			base.CalloutPosition = this.MainSpawnPoint;
			bool flag = this.MainScenario == 0;
			if (flag)
			{
				base.CalloutAdvisory = "Caller Has Expressed Concern With Someone ~r~Taking Pictures~w~ of their ~y~Property.";
			}
			else
			{
				base.CalloutAdvisory = "Caller Has Expressed Concern With Someone ~r~Taking Pictures~w~ of Them.";
			}
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Photography of Private Property Callout Accepted by User.");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 2", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~b~Code 2.");
			}
			bool flag = this.MainScenario == 0;
			if (flag)
			{
				this.Victim = new Ped(this.MainSpawnPoint, 360f);
				this.Victim.IsPersistent = true;
				this.Victim.BlockPermanentEvents = true;
				Game.LogTrivial("YOBBINCALLOUTS: Victim Spawned.");
				this.SuspectSpawnPoint = World.GetNextPositionOnStreet(this.MainSpawnPoint);
				if (PhotographyOfPrivateProperty.<>o__35.<>p__0 == null)
				{
					PhotographyOfPrivateProperty.<>o__35.<>p__0 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(PhotographyOfPrivateProperty), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition;
				PhotographyOfPrivateProperty.<>o__35.<>p__0.Target(PhotographyOfPrivateProperty.<>o__35.<>p__0, NativeFunction.Natives, this.SuspectSpawnPoint, 0, ref outPosition);
				this.Suspect = new Ped(outPosition, 360f);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
			}
			else
			{
				this.SuspectSpawnPoint = World.GetNextPositionOnStreet(this.MainSpawnPoint);
				if (PhotographyOfPrivateProperty.<>o__35.<>p__1 == null)
				{
					PhotographyOfPrivateProperty.<>o__35.<>p__1 = CallSite<<>A{00000010}<CallSite, object, Vector3, int, Vector3>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xA0F8A7517A273C05", new Type[]
					{
						typeof(bool)
					}, typeof(PhotographyOfPrivateProperty), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null)
					}));
				}
				Vector3 outPosition2;
				PhotographyOfPrivateProperty.<>o__35.<>p__1.Target(PhotographyOfPrivateProperty.<>o__35.<>p__1, NativeFunction.Natives, this.SuspectSpawnPoint, 0, ref outPosition2);
				this.Suspect = new Ped(outPosition2, 360f);
				this.Suspect.IsPersistent = true;
				this.Suspect.BlockPermanentEvents = true;
				this.Suspect.Position = this.Suspect.GetOffsetPositionRight(2f);
				this.Victim = new Ped(this.Suspect.GetOffsetPositionFront(-10f), 360f);
				this.Victim.IsPersistent = true;
				this.Victim.BlockPermanentEvents = true;
				Game.LogTrivial("YOBBINCALLOUTS: Victim Spawned.");
			}
			Random r = new Random();
			int SuspectCamera = r.Next(0, 2);
			bool flag2 = SuspectCamera == 0;
			if (flag2)
			{
				this.Camera = new Object("prop_npc_phone", Vector3.Zero);
			}
			else
			{
				this.Camera = new Object("prop_pap_camera_01", Vector3.Zero);
			}
			this.Camera.AttachTo(this.Suspect, this.Suspect.GetBoneIndex(18905), new Vector3(0.149f, 0.056f, -0.01f), new Rotator(-17f, -142f, -151f));
			Game.LogTrivial("YOBBINCALLOUTS: Spawned Suspect, Gave Them Camera");
			this.House = new Blip(this.MainSpawnPoint, 20f);
			this.House.Alpha = 0.67f;
			this.House.IsRouteEnabled = true;
			this.House.Color = Color.Yellow;
			this.House.Name = "Callout Location";
			Random ryuy = new Random();
			this.SuspectAction = ryuy.Next(0, 3);
			Game.LogTrivial("YOBBINCALLOUTS: SuspectAction Value is " + this.SuspectAction.ToString());
			bool flag3 = !this.CalloutRunning;
			if (flag3)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Photography of Private Property Callout Not Accepted by User.");
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
						while (Game.LocalPlayer.Character.DistanceTo(this.MainSpawnPoint) >= 35f && !Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
						bool flag = Game.IsKeyDown(Config.CalloutEndKey);
						if (flag)
						{
							EndCalloutHandler.CalloutForcedEnd = true;
						}
						else
						{
							this.TalkToVictim();
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
						this.End();
					}
					else
					{
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - CALLOUT NO LONGER RUNNING==========");
						string error2 = e.ToString();
						Game.LogTrivial("ERROR: " + error2);
						Game.LogTrivial("No Need to Report This Error if it Did not Result in an LSPDFR Crash.");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - CALLOUT NO LONGER RUNNING==========");
					}
				}
			});
		}

		
		public override void End()
		{
			base.End();
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				Functions.PlayScannerAudio("ATTENTION_ALL_UNITS WE_ARE_CODE_4");
				Game.DisplayNotification("~g~Code 4~w~, return to patrol.");
			}
			this.CalloutRunning = false;
			bool flag = EntityExtensions.Exists(this.Victim);
			if (flag)
			{
				this.Victim.Tasks.ClearImmediately();
			}
			bool flag2 = EntityExtensions.Exists(this.Victim);
			if (flag2)
			{
				this.Victim.Dismiss();
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
			bool flag5 = EntityExtensions.Exists(this.VictimBlip);
			if (flag5)
			{
				this.VictimBlip.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.House);
			if (flag6)
			{
				this.House.Delete();
			}
			bool flag7 = EntityExtensions.Exists(this.Camera);
			if (flag7)
			{
				this.Camera.Delete();
			}
			Game.LogTrivial("YOBBINCALLOUTS: Photography of Private Property Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private void TalkToVictim()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = this.MainScenario == 0;
				if (flag)
				{
					bool flag2 = EntityExtensions.Exists(this.House);
					if (flag2)
					{
						this.House.Delete();
					}
					this.VictimBlip = new Blip(this.Victim.Position);
					this.VictimBlip.Scale = 0.65f;
					this.VictimBlip.Color = Color.Blue;
					this.VictimBlip.Name = "Resident";
					Game.DisplayHelp("Talk to the ~b~Resident.");
					if (PhotographyOfPrivateProperty.<>o__40.<>p__0 == null)
					{
						PhotographyOfPrivateProperty.<>o__40.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(PhotographyOfPrivateProperty), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					PhotographyOfPrivateProperty.<>o__40.<>p__0.Target(PhotographyOfPrivateProperty.<>o__40.<>p__0, NativeFunction.Natives, this.Victim, this.player, -1);
					while (Game.LocalPlayer.Character.DistanceTo(this.Victim) >= 5f)
					{
						GameFiber.Wait(0);
					}
					bool displayHelp = Config.DisplayHelp;
					if (displayHelp)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + " ~w~to Speak With the ~b~Caller.");
					}
					Random r2 = new Random();
					switch (r2.Next(0, 3))
					{
					case 0:
						CallHandler.Dialogue(this.ResidentOpeningDialogue1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						break;
					case 1:
						CallHandler.Dialogue(this.ResidentOpeningDialogue2, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						break;
					case 2:
						CallHandler.Dialogue(this.ResidentOpeningDialogue3, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						break;
					}
					bool flag3 = EntityExtensions.Exists(this.Victim);
					if (flag3)
					{
						this.Victim.Tasks.ClearImmediately();
					}
					bool flag4 = EntityExtensions.Exists(this.VictimBlip);
					if (flag4)
					{
						this.VictimBlip.Delete();
					}
					CallHandler.IdleAction(this.Victim, false);
					GameFiber.Wait(2000);
					Game.DisplayHelp("Talk to the ~r~Suspect.");
					this.SuspectBlip = this.Suspect.AttachBlip();
					this.SuspectBlip.Scale = 0.65f;
					this.SuspectBlip.Color = Color.Red;
					this.SuspectBlip.IsFriendly = false;
					this.SuspectBlip.Name = "Suspect";
					while (Game.LocalPlayer.Character.DistanceTo(this.Suspect) >= 6.9f)
					{
						GameFiber.Wait(0);
					}
					this.Suspect.Tasks.AchieveHeading(Game.LocalPlayer.Character.Heading - 180f).WaitForCompletion(750);
					bool displayHelp2 = Config.DisplayHelp;
					if (displayHelp2)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to Speak with the ~r~Suspect.");
					}
					Random sam = new Random();
					int SuspectOpening = sam.Next(0, 3);
					bool flag5 = this.SuspectAction <= 1;
					if (flag5)
					{
						switch (SuspectOpening)
						{
						case 0:
							CallHandler.Dialogue(this.SuspectOpeningDialogue1, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							break;
						case 1:
							CallHandler.Dialogue(this.SuspectOpeningDialogue2, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							break;
						case 2:
							CallHandler.Dialogue(this.SuspectOpeningDialogue3, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							break;
						}
						GameFiber.Wait(1000);
						bool flag6 = EntityExtensions.Exists(this.Suspect);
						if (flag6)
						{
							this.Suspect.Tasks.ClearImmediately();
							this.Suspect.Dismiss();
						}
						bool flag7 = EntityExtensions.Exists(this.SuspectBlip);
						if (flag7)
						{
							this.SuspectBlip.Delete();
						}
						GameFiber.Wait(2000);
						Game.DisplayHelp("Inform the ~b~Resident.");
					}
					else
					{
						switch (SuspectOpening)
						{
						case 0:
							CallHandler.Dialogue(this.SuspectOpeningDialogue4, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							break;
						case 1:
							CallHandler.Dialogue(this.SuspectOpeningDialogue5, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							break;
						case 2:
							CallHandler.Dialogue(this.SuspectOpeningDialogue5, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							break;
						}
						GameFiber.Wait(2000);
						bool flag8 = EntityExtensions.Exists(this.Suspect);
						if (flag8)
						{
							this.Suspect.Tasks.ClearImmediately();
						}
						Game.DisplayHelp("Deal With the ~r~Suspect~w~ as You See Fit. When Finished, Speak With the ~b~Resident.");
						this.VictimBlip = this.Victim.AttachBlip();
						this.VictimBlip.Scale = 0.65f;
						this.VictimBlip.Color = Color.Blue;
						this.VictimBlip.Name = "Caller";
						while (Game.LocalPlayer.Character.DistanceTo(this.Victim) >= 6f)
						{
							GameFiber.Wait(0);
						}
						bool displayHelp3 = Config.DisplayHelp;
						if (displayHelp3)
						{
							Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to Finish with the ~b~Caller.");
						}
					}
					bool flag9 = EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect);
					if (flag9)
					{
						Random rondom = new Random();
						int VictimEnding = rondom.Next(0, 3);
						bool flag10 = this.SuspectAction > 1;
						if (flag10)
						{
							switch (VictimEnding)
							{
							case 0:
								CallHandler.Dialogue(this.ResidentEndingDialogue4, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								break;
							case 1:
								CallHandler.Dialogue(this.ResidentEndingDialogue5, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								break;
							case 2:
								CallHandler.Dialogue(this.ResidentEndingDialogue6, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								break;
							}
						}
						else
						{
							switch (VictimEnding)
							{
							case 0:
								CallHandler.Dialogue(this.ResidentEndingDialogue1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								break;
							case 1:
								CallHandler.Dialogue(this.ResidentEndingDialogue2, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								break;
							case 2:
								CallHandler.Dialogue(this.ResidentEndingDialogue3, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
								break;
							}
						}
						this.Victim.Tasks.ClearImmediately();
						GameFiber.Wait(1500);
						this.Victim.Dismiss();
					}
					else
					{
						CallHandler.Dialogue(this.ResidentEndingDialogue7, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						this.Victim.Tasks.ClearImmediately();
						GameFiber.Wait(1500);
						this.Victim.Dismiss();
					}
					bool flag11 = EntityExtensions.Exists(this.Suspect) && this.Suspect.IsCuffed;
					if (flag11)
					{
						Game.DisplayNotification("Dispatch, We Have ~b~Arrested~w~ One Suspect for Taking Pictures of Private Property.");
					}
					else
					{
						Game.DisplayNotification("Dispatch, Dispute is ~b~Resolved~w~. The Suspect ~b~Never Trespassed~w~ On Their Property.");
					}
				}
				else
				{
					while (Game.LocalPlayer.Character.DistanceTo(this.Victim) >= 20f && !Game.IsKeyDown(Config.CalloutEndKey))
					{
						GameFiber.Wait(0);
					}
					bool flag12 = Game.IsKeyDown(Config.CalloutEndKey);
					if (flag12)
					{
						this.End();
					}
					bool flag13 = EntityExtensions.Exists(this.House);
					if (flag13)
					{
						this.House.Delete();
					}
					this.VictimBlip = new Blip(this.Victim.Position);
					this.VictimBlip.Scale = 0.65f;
					this.VictimBlip.Color = Color.Blue;
					Game.DisplayHelp("Talk to the ~b~Caller.");
					if (PhotographyOfPrivateProperty.<>o__40.<>p__1 == null)
					{
						PhotographyOfPrivateProperty.<>o__40.<>p__1 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(PhotographyOfPrivateProperty), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					PhotographyOfPrivateProperty.<>o__40.<>p__1.Target(PhotographyOfPrivateProperty.<>o__40.<>p__1, NativeFunction.Natives, this.Victim, this.player, -1);
					while (Game.LocalPlayer.Character.DistanceTo(this.Victim) >= 5f)
					{
						GameFiber.Wait(0);
					}
					bool displayHelp4 = Config.DisplayHelp;
					if (displayHelp4)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to Speak with the ~b~Caller.");
					}
					CallHandler.Dialogue(this.ResidentOpeningDialogue7, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
					bool flag14 = EntityExtensions.Exists(this.Victim);
					if (flag14)
					{
						this.Victim.Tasks.ClearImmediately();
					}
					bool flag15 = EntityExtensions.Exists(this.VictimBlip);
					if (flag15)
					{
						this.VictimBlip.Delete();
					}
					GameFiber.Wait(2000);
					Game.DisplayHelp("Talk to the ~r~Suspect.");
					this.SuspectBlip = this.Suspect.AttachBlip();
					this.SuspectBlip.Scale = 0.65f;
					this.SuspectBlip.Color = Color.Red;
					this.SuspectBlip.IsFriendly = false;
					while (Game.LocalPlayer.Character.DistanceTo(this.Suspect) >= 6f)
					{
						GameFiber.Wait(0);
					}
					bool displayHelp5 = Config.DisplayHelp;
					if (displayHelp5)
					{
						Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to Speak with the ~r~Suspect.");
					}
					bool flag16 = this.SuspectAction <= 1;
					if (flag16)
					{
						CallHandler.Dialogue(this.SuspectOpeningDialogue7, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						GameFiber.Wait(2000);
						bool flag17 = EntityExtensions.Exists(this.Suspect);
						if (flag17)
						{
							this.Suspect.Tasks.ClearImmediately();
							this.Suspect.Dismiss();
						}
						bool flag18 = EntityExtensions.Exists(this.SuspectBlip);
						if (flag18)
						{
							this.SuspectBlip.Delete();
						}
						GameFiber.Wait(2000);
						Game.DisplayHelp("Talk to the ~b~Caller~w~ to Finish the Callout.");
						this.VictimBlip = new Blip(this.Victim.Position);
						this.VictimBlip.Scale = 0.65f;
						this.VictimBlip.Color = Color.Blue;
						if (PhotographyOfPrivateProperty.<>o__40.<>p__2 == null)
						{
							PhotographyOfPrivateProperty.<>o__40.<>p__2 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(PhotographyOfPrivateProperty), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						PhotographyOfPrivateProperty.<>o__40.<>p__2.Target(PhotographyOfPrivateProperty.<>o__40.<>p__2, NativeFunction.Natives, this.Victim, this.player, -1);
						while (Game.LocalPlayer.Character.DistanceTo(this.Victim) >= 5f)
						{
							GameFiber.Wait(0);
						}
						bool displayHelp6 = Config.DisplayHelp;
						if (displayHelp6)
						{
							Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to Finish with the ~b~Caller.");
						}
						CallHandler.Dialogue(this.ResidentEndingDialogue1, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						this.Victim.Tasks.ClearImmediately();
						GameFiber.Wait(1500);
						bool flag19 = EntityExtensions.Exists(this.VictimBlip);
						if (flag19)
						{
							this.VictimBlip.Delete();
						}
						this.Victim.Dismiss();
						bool flag20 = EntityExtensions.Exists(this.Suspect) && Functions.IsPedArrested(this.Suspect);
						if (flag20)
						{
							Game.DisplayNotification("Dispatch, We Have ~b~Arrested~w~ One Suspect for Taking Pictures Without Consent.");
						}
						else
						{
							Game.DisplayNotification("Dispatch, Dispute is ~b~Resolved~w~. The Suspect ~b~Agreed to Leave.");
						}
					}
					else
					{
						CallHandler.Dialogue(this.SuspectOpeningDialogue6, this.Suspect, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
						GameFiber.Wait(2000);
						bool flag21 = EntityExtensions.Exists(this.Suspect);
						if (flag21)
						{
							this.Suspect.Tasks.ClearImmediately();
						}
						Game.DisplayHelp("Deal With the ~r~Suspect~w~ as You See Fit. When Finished, Speak With the ~b~Caller.");
						this.VictimBlip = new Blip(this.Victim.Position);
						this.VictimBlip.Scale = 0.65f;
						this.VictimBlip.Color = Color.Blue;
						while (Game.LocalPlayer.Character.DistanceTo(this.Victim) >= 5f)
						{
							GameFiber.Wait(0);
						}
						bool displayHelp7 = Config.DisplayHelp;
						if (displayHelp7)
						{
							Game.DisplayHelp("Press~y~ " + Config.MainInteractionKey.ToString() + "~w~ to Finish with the ~b~Caller.");
						}
						bool flag22 = EntityExtensions.Exists(this.Suspect) && !Functions.IsPedArrested(this.Suspect);
						if (flag22)
						{
							CallHandler.Dialogue(this.ResidentEndingDialogue4, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							this.Victim.Tasks.ClearImmediately();
							GameFiber.Wait(1500);
							this.Victim.Dismiss();
						}
						else
						{
							CallHandler.Dialogue(this.ResidentEndingDialogue7, this.Victim, "missfbi3_party_d", "stand_talk_loop_a_male1", -1f, 1);
							this.Victim.Tasks.ClearImmediately();
							GameFiber.Wait(1500);
							this.Victim.Dismiss();
						}
					}
				}
			}
		}

		
		private Vector3 MainSpawnPoint;

		
		private Vector3 SuspectSpawnPoint;

		
		private Blip House;

		
		private Blip VictimBlip;

		
		private Blip SuspectBlip;

		
		private Ped Victim;

		
		private Ped Suspect;

		
		private Ped player = Game.LocalPlayer.Character;

		
		private int MainScenario;

		
		private int SuspectAction;

		
		private bool CalloutRunning = false;

		
		private string Zone;

		
		private Object Camera;

		
		private readonly List<string> ResidentOpeningDialogue1 = new List<string>
		{
			"~b~Caller:~w~ Hey officer, thanks for showing up for a change.",
			"~b~Caller:~w~ That person out there has been standing outside my house for over an hour.",
			"~b~Caller:~w~ At first, I didn't think anything of it, however when he didn't leave I decided to see what he was up to.",
			"~b~Caller:~w~ Turns out he's been taking photos of my house all this time for some reason.",
			"~b~Caller:~w~ The photos are of my property, and I don't want him to take anymore.",
			"~g~You:~w~ Alright, I'll go talk to them. Stay here please."
		};

		
		private readonly List<string> ResidentOpeningDialogue2 = new List<string>
		{
			"~b~Caller:~w~ Hey officer, hope you're doing well.",
			"~g~You:~w~ I'm doing alright, thank you. What seems to be the issue?",
			"~b~Caller:~w~ That person over there keeps taking photos of my house.",
			"~b~Caller:~w~ I don't know why, but when I asked him to leave, he refused.",
			"~g~You:~w~ What would you like me to do?",
			"~b~Caller:~w~ Just get him to leave, I don't feel comfortable with him taking pictures of my home.",
			"~g~You:~w~ Alright, I'll see what's up. Stay right here please."
		};

		
		private readonly List<string> ResidentOpeningDialogue3 = new List<string>
		{
			"~b~Caller:~w~ Thanks for getting here so quickly, officer.",
			"~g~You:~w~ Not a problem. Why did you call us?",
			"~b~Caller:~w~ That person over there keeps taking pictures of my house.",
			"~b~Caller:~w~ I respectfully asked him to leave, but he never did.",
			"~b~Caller:~w~ I don't feel comfortable with him doing that, officer.",
			"~g~You:~w~ Okay, I will talk with them now.",
			"~b~Caller:~w~ Thank you, officer."
		};

		
		private readonly List<string> ResidentOpeningDialogue4 = new List<string>
		{
			"~b~Caller:~w~ Thanks for getting here so quickly, officer.",
			"~g~You:~w~ Not a problem. Why did you call us?",
			"~b~Caller:~w~ That person over there keeps taking pictures of my house.",
			"~b~Caller:~w~ I respectfully asked him to leave, but he never did.",
			"~b~Caller:~w~ I don't feel comfortable with him doing that, officer.",
			"~g~You:~w~ Okay, I will talk with them now.",
			"~b~Caller:~w~ Thank you, officer."
		};

		
		private readonly List<string> ResidentOpeningDialogue5 = new List<string>
		{
			"~b~Caller:~w~ Thanks for getting here so quickly, officer.",
			"~g~You:~w~ Not a problem. Why did you call us?",
			"~b~Caller:~w~ That person over there keeps taking pictures of my house.",
			"~b~Caller:~w~ I respectfully asked him to leave, but he never did.",
			"~b~Caller:~w~ They've also trespassed on my property several times for some reason.",
			"~g~You:~w~ Alright, I'll speak to them. Stay right here please."
		};

		
		private readonly List<string> ResidentOpeningDialogue6 = new List<string>
		{
			"~b~Caller:~w~ Hey officer, hope you're doing well.",
			"~g~You:~w~ I'm doing alright, thank you. What seems to be the issue?",
			"~b~Caller:~w~ That person over there keeps taking photos of my house.",
			"~b~Caller:~w~ I don't know why, but when I asked him to leave, he refused.",
			"~b~Caller:~w~ They've also ventured on my property several times. I was fine with the photos, until they started trespassing.",
			"~g~You:~w~ What would you like me to do?",
			"~b~Caller:~w~ Just get him to leave, I don't feel comfortable with him taking pictures of my home.",
			"~g~You:~w~ Alright, I'll see what's up. Stay right here please."
		};

		
		private readonly List<string> ResidentOpeningDialogue7 = new List<string>
		{
			"~b~Caller:~w~ Hey officer, thanks for showing up for a change.",
			"~b~Caller:~w~ That person out there keeps taking photos of me without my permission.",
			"~b~Caller:~w~ I keep telling them to leave, but they keep following me.",
			"~b~Caller:~w~ If you could just tell them to fuck off, that'd be great.",
			"~g~You:~w~ Alright, I'll go talk to them. Stay here please."
		};

		
		private readonly List<string> SuspectOpeningDialogue1 = new List<string>
		{
			"~g~You:~w~ Hey, sorry to bother you.",
			"~g~You:~w~ May I ask what you are doing here?",
			"~r~Suspect:~w~ Just out taking some pictures, officer.",
			"~g~You:~w~ May I ask what you are taking pictures of?",
			"~g~You:~w~ The resident of that house said you were taking photos of their property.",
			"~r~Suspect:~w~ Is that right? I've just been walking around the neighbourhood taking photos of the trees.",
			"~r~Suspect:~w~ It's not a problem though, I can go somewhere else if you would like.",
			"~g~You:~w~ Yeah, that would be great, if you don't mind. Just to make them a little more comfortable.",
			"~r~Suspect:~w~ Of course, officer. Not an issue.",
			"~g~You:~w~ Thanks so much for your cooperation!"
		};

		
		private readonly List<string> SuspectOpeningDialogue2 = new List<string>
		{
			"~g~You:~w~ Hey, sorry to bother you.",
			"~g~You:~w~ May I ask what you are doing here?",
			"~r~Suspect:~w~ Just out taking some pictures, officer. I got a new camera the other day.",
			"~g~You:~w~ Can I ask you what you are taking photos of?",
			"~r~Suspect:~w~ Yeah, just trying to get some pics of the sky, I want to see how well the photos turn out.",
			"~g~You:~w~ I see. You're not taking any pictures of this house over here?",
			"~r~Suspect:~w~ No officer, why?",
			"~g~You:~w~ The resident was concerned you may have been taking photos of their property.",
			"~r~Suspect:~w~ Oh, it must have been a misunderstanding. I've never taken any photos of that.",
			"~r~Suspect:~w~ It's no problem however, I can go somewhere else if you want me to.",
			"~g~You:~w~ Would you mind? That would make them feel more comfortable I'd say.",
			"~r~Suspect:~w~ Yeah no problem! I can take these photos anywhere, after all.",
			"~g~You:~w~ I really appreciate the cooperation. Have a great day!"
		};

		
		private readonly List<string> SuspectOpeningDialogue3 = new List<string>
		{
			"~g~You:~w~ Sorry to bother you, I was wondering if I could ask what you are doing here?",
			"~r~Suspect:~w~ I'm just out taking some pictures of the houses around here.",
			"~r~Suspect:~w~ I'm a scout for a movie, I'm looking for suitable houses to potentially use for the filming. This is Los Santos, after all.",
			"~g~You:~w~ Oh, cool! Were you, by any chance, taking a photo of that house over there?",
			"~r~Suspect:~w~ Yes I was, it was one of the more suitable houses that met the criteria for the film.",
			"~r~Suspect:~w~ Why do you ask?",
			"~g~You:~w~ Yhe resident was a bit concerned, is all. Sounds like they didn't want their house photographed.",
			"~r~Suspect:~w~ Oh, sorry about that! I never meant to disturb anyone! Would you like me to go somewhere else?",
			"~g~You:~w~ That would be great, if you don't mind.",
			"~r~Suspect:~w~ Yeah, no problem. I guess we won't be using their house for the shoot anytime soon!",
			"~g~You:~w~ Haha, I guess not. Thanks so much for your understanding!"
		};

		
		private readonly List<string> SuspectOpeningDialogue4 = new List<string>
		{
			"~g~You:~w~ Hey, sorry to bother you. I was wondering what you're doing around here?",
			"~r~Suspect:~w~ That's none of your business, officer. I'm not breaking any laws.",
			"~g~You:~w~ I never said you were, I was just asking what you're up to.",
			"~r~Suspect:~w~ I don't have to answer that. What's with all these questions?",
			"~g~You:~w~ One of the residents around here expressed concern about someone taking pictures of their property.",
			"~r~Suspect:~w~ That's not my problem officer. Please stop bothering me.",
			"~g~You:~w~ I'm simply asking you why you're taking photos here. It's not a complicated question.",
			"~r~Suspect:~w~ It isn't illegal to take pictures of somebody's house officer. I'm not obligated to talk to you anymore."
		};

		
		private readonly List<string> SuspectOpeningDialogue5 = new List<string>
		{
			"~g~You:~w~ Hey, sorry to bother you.",
			"~g~You:~w~ I just want to ask what you're doing here.",
			"~r~Suspect:~w~ Why do you ask, officer? Am I doing something wrong?",
			"~g~You:~w~ No, I'm just wondering what you're up to. Some residents want to know what you're doing as well.",
			"~r~Suspect:~w~ I'm just minding my own business. Please stop harassing me if I haven't done anything wrong officer.",
			"~g~You:~w~ I'm not harassing you, I just want to know what you're doing here. Neighbours have said you've been around here awhile.",
			"~r~Suspect:~w~ I'm not going to talk to you anymore officer. Are you gonna arrest me now or what?"
		};

		
		private readonly List<string> SuspectOpeningDialogue6 = new List<string>
		{
			"~g~You:~w~ Hey, sorry to bother you.",
			"~g~You:~w~ I just want to ask what you're doing here.",
			"~r~Suspect:~w~ Why do you ask, officer? Am I doing something wrong?",
			"~g~You:~w~ Bo, I'm just wondering what you're up to. Apparently someone asked you to stop taking pictures of them.",
			"~r~Suspect:~w~ I'm just minding my own business. Please stop harassing me if I haven't done anything wrong officer.",
			"~g~You:~w~ I'm not harassing you, I just want to know why you're taking photos of them. They've said you refused to leave.",
			"~r~Suspect:~w~ I'm not going to talk to you anymore officer. Are you gonna arrest me now or what?"
		};

		
		private readonly List<string> SuspectOpeningDialogue7 = new List<string>
		{
			"~g~You:~w~ hey, sorry to bother you.",
			"~g~You:~w~ I just want to ask what you're doing here.",
			"~r~Suspect:~w~ why do you ask, officer? Am I doing something wrong?",
			"~g~You:~w~ no, I'm just wondering what you're up to. Apparently someone asked you to stop taking pictures of them.",
			"~r~Suspect:~w~ I think that must have been a misunderstanding! I would never take photos of someone without consent!",
			"~g~You:~w~ alright, no problem. All I ask is that you head in another direction from that person over there.",
			"~r~Suspect:~w~ no problem officer. Sorry for the misunderstanding!"
		};

		
		private readonly List<string> ResidentEndingDialogue1 = new List<string>
		{
			"~g~You:~w~ Alright, so I talked to the person and they agreed to move somewhere else.",
			"~g~You:~w~ They were very apologetic, I'm sure they meant no harm.",
			"~b~Caller:~w~ Thanks, officer. I just didn't want any issues, y'know?",
			"~g~You:~w~ Yeah, of course. Is there anything else I can do for you?",
			"~b~Caller:~w~ No, that's it. Thanks for your help!"
		};

		
		private readonly List<string> ResidentEndingDialogue2 = new List<string>
		{
			"~g~You:~w~ Alright, so I spoke with the photographer, and they agreed to go somewhere else.",
			"~g~You:~w~ They didn't want any trouble, and were very understanding.",
			"~b~Caller:~w~ Sounds good officer. I just don't like the idea of people potentially taking pictures of my house, I guess.",
			"~g~You:~w~ Sure, I understand. Is there anything else I can help you with today?",
			"~b~Caller:~w~ No, that's about it. Thanks for the help!"
		};

		
		private readonly List<string> ResidentEndingDialogue3 = new List<string>
		{
			"~g~You:~w~ So I spoke with the person and they were happy to move somewhere else.",
			"~b~Caller:~w~ Great. Thanks so much for that officer.",
			"~g~You:~w~ No problem, they were more than willing to do so, I know they didn't mean any harm.",
			"~b~Caller:~w~ Awesome. Thanks for the help today, officer!"
		};

		
		private readonly List<string> ResidentEndingDialogue4 = new List<string>
		{
			"~b~Caller:~w~ Well, what's the deal?",
			"~g~You:~w~ I spoke with the person, unfortunately they refused to move.",
			"~g~You:~w~ I understand your concern, however there really isn't much I can do here.",
			"~g~You:~w~ They're legally allowed to take pictures of your property, as long as they aren't trespassing.",
			"~b~Caller:~w~ I see. Well, I'm sure it won't be a big deal. Thanks for the help anyways, officer.",
			"~g~You:~w~ No problem. Have a good day."
		};

		
		private readonly List<string> ResidentEndingDialogue5 = new List<string>
		{
			"~g~You:~w~ So I spoke with the person, however they refused to leave unfortunately.",
			"~b~Caller:~w~ So there's nothing you can do about them taking pictures of my property without consent?",
			"~g~You:~w~ Unfortunately no. The first amendment allows them to take photos as long as they aren't trespassing.",
			"~b~Caller:~w~ Really? That seems unreasonable. They've been around here for hours and refuses to leave! What am I supposed to do?",
			"~g~You:~w~ If they trespass on your property, you can call us and we can have them trespassed.",
			"~g~You:~w~ However, as it stands there isn't anything else I can do, unfortunately.",
			"~b~Caller:~w~ Okay, thanks for the explanation. If they do anything else I'll call you guys.",
			"~g~You:~w~ Sounds good, have a nice day."
		};

		
		private readonly List<string> ResidentEndingDialogue6 = new List<string>
		{
			"~b~Caller:~w~ What happened, officer?",
			"~g~You:~w~ I spoke to the person, however they did not want to talk to me.",
			"~g~You:~w~ As far as I can tell, they haven't done anything wrong, and I can't arrest somebody just for taking pictures of your property.",
			"~b~Caller:~w~ Really, officer? They have been out here for hours, and refused to leave. It is making me very uncomfortable!",
			"~g~You:~w~ Unfortunately, they are allowed to do that. They never trespassed on your property, right?",
			"~b~Caller:~w~ Well no, at least not yet.",
			"~g~You:~w~ Okay, well if they ever do, you can let us know and we can have them trespassed. Otherwise I can't do anything else at this point.",
			"~b~Caller:~w~ I see. Thanks for trying anyways.",
			"~g~You:~w~ Not a problem. Take care."
		};

		
		private readonly List<string> ResidentEndingDialogue7 = new List<string>
		{
			"~g~You:~w~ The suspect refused to speak with me, so I've decided to arrest them for the time being.",
			"~b~Caller:~w~ Okay, thanks a lot officer. I really didn't like them doing that.",
			"~g~You:~w~ I understand that. Is there anything else I can do for you?",
			"~b~Caller:~w~ No, that's pretty much it. Thanks for the help."
		};
	}
}
