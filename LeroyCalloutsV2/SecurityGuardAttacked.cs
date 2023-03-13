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

namespace LeroyCalloutsV2.Callouts
{
	
	[CalloutInfo("LC - SecurityGuardAttacked", 2)]
	internal class SecurityGuardAttacked : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Common.activeCall = "Security Guard Attacked";
			bool result;
			try
			{
				Common.WriteToLog("Picking Spawn Point");
				this.spawnPointIndex = Common.FindSpawnPointIndex(this.possibleSpawns, true, "ATTENTION_ALL_UNITS_GENERIC CRIME_CIVILIAN_NEEDING_ASSISTANCE IN_OR_ON_POSITION", 2, 8000);
				this.securityGuardSpawn = this.possibleSpawns[this.spawnPointIndex];
				base.ShowCalloutAreaBlipBeforeAccepting(this.securityGuardSpawn, 30f);
				base.AddMinimumDistanceCheck(100f, this.securityGuardSpawn);
				base.CalloutMessage = Common.activeCall + "~n~~y~Code 2 Response";
				base.CalloutPosition = this.securityGuardSpawn;
				bool flag = this.spawnPointIndex != -1;
				if (flag)
				{
					result = base.OnBeforeCalloutDisplayed();
				}
				else
				{
					Common.WriteToLog("Unable to find valid spawn point.");
					result = false;
				}
			}
			catch (Exception ex)
			{
				Common.WriteErrorToLog("Before callout displayed: " + ex.ToString());
				result = false;
			}
			return result;
		}

		
		public override bool OnCalloutAccepted()
		{
			this.CalloutHandler();
			return base.OnCalloutAccepted();
		}

		
		private void CalloutHandler()
		{
			try
			{
				GameFiber.StartNew(delegate()
				{
					try
					{
						this.suspect = Common.CreatePed(true, World.GetNextPositionOnStreet(this.securityGuardSpawn.Around(400f, 800f)), true, true);
						int weapon = this.num.Next(0, 3);
						bool flag = weapon == 0;
						if (flag)
						{
							this.suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", -1, false);
						}
						else
						{
							bool flag2 = weapon == 1;
							if (flag2)
							{
								this.suspect.Inventory.GiveNewWeapon("WEAPON_SWITCHBLADE", -1, false);
							}
							else
							{
								this.suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", -1, false);
							}
						}
						this.suspect.RelationshipGroup = "suspect";
						Game.LocalPlayer.Character.RelationshipGroup = "cop";
						this.suspect.Tasks.Wander();
						this.securityGuard = new Ped("s_m_m_security_01", this.securityGuardSpawn, 90f);
						this.securityGuard.IsPersistent = true;
						this.securityGuard.BlockPermanentEvents = true;
						this.securityGuard.Tasks.PlayAnimation("amb@world_human_sunbathe@male@back@idle_a", "idle_a", 1f, 1);
						Common.WriteToLog("Spawning medic");
						this.medic = new Ped("s_m_m_paramedic_01", this.securityGuard.GetOffsetPositionRight(1f), 0f);
						this.medic.Face(this.securityGuard);
						this.medic.Tasks.Clear();
						this.medic.Tasks.PlayAnimation("amb@medic@standing@tendtodead@idle_a", "idle_a", 1f, 1);
						Common.WriteToLog("Spawning securitySupervisor");
						this.securitySupervisor = new Ped("csb_prolsec", this.supervisorSpawns[this.spawnPointIndex], 90f);
						this.securitySupervisor.Face(this.securityGuard);
						this.securitySupervisorBlip = this.securitySupervisor.AttachBlip();
						this.securitySupervisorBlip.Color = Color.Blue;
						this.securitySupervisorBlip.EnableRoute(Color.Yellow);
						Common.WriteToLog("All entities spawned");
					}
					catch (Exception ex2)
					{
						Common.WriteErrorToLog("Could not spawn all entities. Ending callout. This may be caused by one of GTA's \"Bermuda Triangles\".Move to another location and try again. Exception: " + ex2.ToString());
						this.End();
						return;
					}
					this.calloutRunning = true;
					Common.WriteToLog("Callout is now running");
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag3 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.securitySupervisor) < 30f;
						if (flag3)
						{
							Common.WriteToLog("Player arrived at witness");
							this.securitySupervisorBlip.DisableRoute();
							Game.DisplayHelp("Press ~y~" + this.talk.ToString() + " ~s~when near the ~b~supervisor ~s~to advance the conversation");
							if (SecurityGuardAttacked.<>o__24.<>p__0 == null)
							{
								SecurityGuardAttacked.<>o__24.<>p__0 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(SecurityGuardAttacked), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							SecurityGuardAttacked.<>o__24.<>p__0.Target(SecurityGuardAttacked.<>o__24.<>p__0, NativeFunction.Natives, this.securitySupervisor, Game.LocalPlayer.Character, -1);
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag4 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.securitySupervisor) < 10f;
						if (flag4)
						{
							if (SecurityGuardAttacked.<>o__24.<>p__1 == null)
							{
								SecurityGuardAttacked.<>o__24.<>p__1 = CallSite<Action<CallSite, object, Ped, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_TURN_PED_TO_FACE_ENTITY", null, typeof(SecurityGuardAttacked), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							SecurityGuardAttacked.<>o__24.<>p__1.Target(SecurityGuardAttacked.<>o__24.<>p__1, NativeFunction.Natives, this.securitySupervisor, Game.LocalPlayer.Character, -1);
							bool flag5 = Game.IsKeyDown(this.talk);
							if (flag5)
							{
								Common.WriteToLog("Supervisor dialog index: " + this.dialogIndex.ToString());
								Game.DisplaySubtitle(this.dialogWithsecuritySupervisor[this.dialogIndex]);
								this.dialogIndex++;
								bool flag6 = this.dialogIndex == this.dialogWithsecuritySupervisor.Count;
								if (flag6)
								{
									Common.WriteToLog("Routing to suspect");
									this.searchArea = Common.CreateSearchArea(this.suspect.Position, 100f);
									this.searchArea.EnableRoute(Color.Yellow);
									Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", this.suspect.Position);
									Game.DisplayHelp("Talk to the ~r~suspect");
									break;
								}
							}
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag7 = Vector3.Distance(Game.LocalPlayer.Character.Position, this.suspect) < 10f;
						if (flag7)
						{
							Game.DisplayNotification("~b~You: ~s~Dispatch, suspect located. He's running!");
							this.pursuit = Functions.CreatePursuit();
							Functions.AddPedToPursuit(this.pursuit, this.suspect);
							Functions.SetPursuitIsActiveForPlayer(this.pursuit, true);
							this.searchArea.DisableRoute();
							this.searchArea.Delete();
							break;
						}
					}
					while (this.calloutRunning)
					{
						GameFiber.Yield();
						bool flag8 = !EntityExtensions.Exists(this.suspect) || Functions.IsPedArrested(this.suspect) || this.suspect.IsDead;
						if (flag8)
						{
							this.calloutRunning = false;
							Common.WriteToLog("Suspect arrested or dead.");
							Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
							Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
							this.End();
							break;
						}
					}
				});
			}
			catch (Exception ex)
			{
				Common.WriteErrorToLog(ex.ToString());
			}
		}

		
		public override void Process()
		{
			base.Process();
			bool flag = this.calloutRunning;
			if (flag)
			{
				bool flag2 = Game.IsKeyDown(this.end);
				if (flag2)
				{
					this.calloutRunning = false;
					Common.WriteToLog("Player requested end callout.");
					Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
					Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
					this.End();
				}
				bool flag3 = !EntityExtensions.Exists(this.medic) || !EntityExtensions.Exists(this.suspect) || !EntityExtensions.Exists(this.securitySupervisor) || !EntityExtensions.Exists(this.securityGuard);
				if (flag3)
				{
					this.calloutRunning = false;
					Common.WriteErrorToLog("Entity does not exist");
					Game.DisplayNotification("~b~Dispatch: ~s~All units, code 4.");
					Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_GENERIC CODE_FOUR NO_UNITS_REQUIRED");
					this.End();
				}
			}
		}

		
		public override void End()
		{
			Common.WriteToLog("Ending Callout");
			Common.Dismiss(this.suspect);
			Common.Dismiss(this.securitySupervisor);
			Common.Dismiss(this.securityGuard);
			Common.Dismiss(this.medic);
			Common.Dismiss(this.suspectBlip);
			Common.Dismiss(this.securitySupervisorBlip);
			base.End();
		}

		
		private LHandle pursuit;

		
		private Ped securityGuard;

		
		private Random num = new Random();

		
		private Ped medic;

		
		private Ped securitySupervisor;

		
		private Blip securitySupervisorBlip;

		
		private Blip searchArea;

		
		private int dialogIndex = 0;

		
		private int Index;

		
		private bool spawnFound = false;

		
		private Keys talk = Main.TalkKey;

		
		private Keys end = Main.EndCalloutKey;

		
		private bool calloutRunning;

		
		private Vector3 spawnPoint;

		
		private int spawnPointIndex;

		
		private Vector3 securityGuardSpawn;

		
		private int numOfDialog;

		
		private Ped suspect;

		
		private Blip suspectBlip;

		
		private readonly List<Vector3> possibleSpawns = new List<Vector3>
		{
			new Vector3(-1002.671f, -2345.75f, 13.94454f),
			new Vector3(-1044.461f, -495.7453f, 36.20463f),
			new Vector3(-1045.511f, -2773.173f, 4.6398f),
			new Vector3(-15.71852f, 6254.407f, 31.31001f),
			new Vector3(1744.763f, 3293.829f, 41.1055f),
			new Vector3(-270.1782f, -2036.966f, 30.1456f),
			new Vector3(-33.18802f, -662.8012f, 33.48041f),
			new Vector3(916.4164f, -3055.11f, 5.901084f),
			new Vector3(-93.1711f, -1037.04f, 28.04228f)
		};

		
		private readonly List<Vector3> supervisorSpawns = new List<Vector3>
		{
			new Vector3(-999.5349f, -2349.852f, 13.94453f),
			new Vector3(-1046.773f, -500.4347f, 36.0438f),
			new Vector3(-1048.808f, -2770.942f, 4.6398f),
			new Vector3(-14.00195f, 6259.36f, 31.25381f),
			new Vector3(1746.729f, 3290.219f, 41.10484f),
			new Vector3(-270.9854f, -2033.189f, 30.1456f),
			new Vector3(-37.24237f, -663.6208f, 33.48046f),
			new Vector3(911.4318f, -3051.192f, 5.902042f),
			new Vector3(-97.21395f, -1039.779f, 27.67347f)
		};

		
		private List<string> dialogWithsecuritySupervisor = new List<string>
		{
			"~b~Security Supervisor: ~s~How are you doing today officer?",
			"~b~You: ~s~I'm doing fine. What's going on?",
			"~b~Security Supervisor: ~s~Some maniac attacked one of my guards.",
			"~b~You: ~s~Did you see what they looked like?",
			"~b~Security Supervisor: ~s~I wasn't here when it happened but the guard gave the 911 operator a description before he passed out.",
			"~b~You: ~s~Do you know which way he went?",
			"~b~Security Supervisor: ~s~That way, I think.",
			"~b~You: ~s~Alright. I'll see if I can find him."
		};
	}
}
