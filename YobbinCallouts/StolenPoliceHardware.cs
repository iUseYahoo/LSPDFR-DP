using System;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Engine.Scripting;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;
using YobbinCallouts.Utilities;

namespace YobbinCallouts.Callouts
{
	
	[CalloutInfo("Stolen Police Hardware", 2)]
	internal class StolenPoliceHardware : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS: Stolen Police Hardware Callout Start==========");
			this.MainSpawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(450f));
			Random r2 = new Random();
			int Scenario = r2.Next(0, 4);
			this.MainScenario = Scenario;
			Game.LogTrivial("YOBBINCALLOUTS: Scenario " + this.MainScenario.ToString() + " Chosen.");
			base.ShowCalloutAreaBlipBeforeAccepting(this.MainSpawnPoint, 20f);
			base.AddMinimumDistanceCheck(20f, this.MainSpawnPoint);
			Functions.PlayScannerAudio("WE_HAVE_01 YC_STOLEN_FIREARM");
			base.CalloutMessage = "Stolen Police Hardware";
			base.CalloutPosition = this.MainSpawnPoint;
			base.CalloutAdvisory = "ANPR Hit on Vehicle Suspected of Carrying Stolen Police Weapons.";
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Stolen Police Hardware Callout Accepted by User");
			Functions.PlayScannerAudio("UNITS_RESPOND_CODE_03_02");
			bool calloutInterface = Main.CalloutInterface;
			if (calloutInterface)
			{
				CalloutInterfaceHandler.SendCalloutDetails(this, "CODE 3", "");
			}
			else
			{
				Game.DisplayNotification("Respond ~r~Code 3.~w~");
			}
			if (StolenPoliceHardware.<>o__13.<>p__0 == null)
			{
				StolenPoliceHardware.<>o__13.<>p__0 = CallSite<<>A{00000018}<CallSite, object, Vector3, Vector3, float, int, float, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetClosestVehicleNodeWithHeading", null, typeof(StolenPoliceHardware), new CSharpArgumentInfo[]
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
			float outheading;
			StolenPoliceHardware.<>o__13.<>p__0.Target(StolenPoliceHardware.<>o__13.<>p__0, NativeFunction.Natives, this.MainSpawnPoint, ref nodePosition, ref outheading, 1, 3f, 0);
			this.SuspectVehicle = CallHandler.SpawnVehicle(this.MainSpawnPoint, outheading, true);
			this.Peds = new string[]
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
			int SuspectModel = r2.Next(0, this.Peds.Length);
			this.Suspect = new Ped(this.Peds[SuspectModel], this.MainSpawnPoint, 69f);
			this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.IsPersistent = true;
			this.Suspect.Tasks.CruiseWithVehicle(this.SuspectVehicle, 15f, 1);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.IsFriendly = false;
			this.SuspectBlip.Scale = 0.8f;
			this.SuspectBlip.IsRouteEnabled = true;
			this.SuspectBlip.Name = "Suspect";
			bool flag = !this.CalloutRunning;
			if (flag)
			{
				this.Callout();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			Game.LogTrivial("YOBBINCALLOUTS: Stolen Police Hardware Callout Not Accepted by User.");
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
						while (Game.LocalPlayer.Character.DistanceTo(this.Suspect) >= 20f && !Game.IsKeyDown(Config.CalloutEndKey))
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
							this.SuspectBlip.IsRouteEnabled = false;
							Game.DisplayHelp("Perform a Traffic Stop on the ~r~Suspect.");
							while (!Functions.IsPlayerPerformingPullover())
							{
								GameFiber.Wait(0);
							}
							bool flag2 = this.MainScenario == 0;
							if (flag2)
							{
								this.Pursuit();
							}
							else
							{
								bool flag3 = this.MainScenario == 1;
								if (flag3)
								{
									this.Shootout();
								}
								else
								{
									bool flag4 = this.MainScenario == 2 || this.MainScenario == 3;
									if (flag4)
									{
										this.Pullover();
									}
								}
							}
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

		
		private void Pursuit()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
				{
					this.Suspect
				});
				GameFiber.Wait(2000);
				Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
				GameFiber.Wait(2000);
				bool flag = !Main.STP;
				if (flag)
				{
					Game.DisplayHelp("Press 'Y' to Search the ~r~Vehicle~w~ for any ~r~Stolen Police Hardware.");
				}
				else
				{
					Game.DisplayHelp("Use ~b~Stop the Ped~w~ to Search the ~r~Vehicle~w~ for any ~r~Stolen Police Hardware.");
				}
				this.SearchVehicle();
			}
		}

		
		private void Shootout()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				GameFiber.Wait(2500);
				this.Suspect.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", -1, true);
				this.Suspect.Tasks.ParkVehicle(this.SuspectVehicle, this.SuspectVehicle.Position, this.SuspectVehicle.Heading).WaitForCompletion(5000);
				this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256).WaitForCompletion();
				this.Suspect.Tasks.AchieveHeading(Game.LocalPlayer.Character.LastVehicle.Heading - 180f).WaitForCompletion(1500);
				this.Suspect.Tasks.AimWeaponAt(Game.LocalPlayer.Character.Position, 1500).WaitForCompletion();
				this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
				bool flag = Functions.IsPlayerPerformingPullover();
				if (flag)
				{
					Functions.ForceEndCurrentPullover();
				}
				GameFiber.Wait(2000);
				Functions.PlayScannerAudio("CRIME_ASSAULT_PEACE_OFFICER_01");
				Functions.RequestBackup(this.Suspect.Position, 1, 0);
				while (!this.Suspect.IsCuffed && !this.Suspect.IsDead)
				{
					GameFiber.Wait(0);
				}
				bool isDead = this.Suspect.IsDead;
				if (isDead)
				{
					GameFiber.Wait(1000);
					Game.DisplayNotification("Dispatch, Suspect Was ~r~Killed~w~ Trying to Assault an Officer.");
					this.SuspectBlip.Delete();
				}
				bool isCuffed = this.Suspect.IsCuffed;
				if (isCuffed)
				{
					GameFiber.Wait(1000);
					Game.DisplayNotification("Dispatch, Suspect is Under ~g~Arrest~w~ For Trying to Assault an Officer");
				}
				GameFiber.Wait(2000);
				Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
				GameFiber.Wait(2000);
				bool flag2 = !Main.STP;
				if (flag2)
				{
					Game.DisplayHelp("Press 'Y' to Search the ~r~Vehicle~w~ for any Other ~r~Stolen Police Hardware.");
				}
				else
				{
					Game.DisplayHelp("Use ~b~Stop the Ped~w~ to Search the ~r~Vehicle~w~ for any Other ~r~Stolen Police Hardware.");
				}
				this.SearchVehicle();
			}
		}

		
		private void Pullover()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				while (Game.LocalPlayer.Character.IsInAnyVehicle(false))
				{
					GameFiber.Wait(0);
				}
				bool flag = this.MainScenario == 2;
				if (flag)
				{
					bool displayHelp = Config.DisplayHelp;
					if (displayHelp)
					{
						Game.DisplayHelp("Ask the ~r~Suspect~w~ to ~y~Exit~w~ the Vehicle, then ~b~Search it.");
					}
					while (this.Suspect.IsInVehicle(this.SuspectVehicle, false))
					{
						GameFiber.Wait(0);
					}
					GameFiber.Wait(3500);
					bool flag2 = !Main.STP;
					if (flag2)
					{
						Game.DisplayHelp("Press 'Y' to Search the ~r~Vehicle~w~ for any ~r~Stolen Police Hardware.");
					}
					else
					{
						Game.DisplayHelp("Use ~b~Stop the Ped~w~ to Search the ~r~Vehicle~w~ for any ~r~Stolen Police Hardware.");
					}
					this.SearchVehicle();
				}
				else
				{
					Functions.ForceEndCurrentPullover();
				}
				this.Pursuit();
			}
		}

		
		private void SearchVehicle()
		{
			bool calloutRunning = this.CalloutRunning;
			if (calloutRunning)
			{
				bool flag = EntityExtensions.Exists(this.SuspectBlip);
				if (flag)
				{
					this.SuspectBlip.Delete();
				}
				this.SuspectBlip = this.SuspectVehicle.AttachBlip();
				this.SuspectBlip.IsFriendly = false;
				while (Game.LocalPlayer.Character.DistanceTo(this.SuspectVehicle) >= 2f && !Main.STP)
				{
					bool flag2 = Game.IsKeyDown(Config.MainInteractionKey);
					if (flag2)
					{
						Game.DisplayHelp("Please Move Closer to the Vehicle to Search.");
					}
					GameFiber.Yield();
				}
				bool flag3 = !Main.STP;
				if (flag3)
				{
					while (Game.LocalPlayer.Character.DistanceTo(this.SuspectVehicle) <= 2f && !Game.IsKeyDown(Config.MainInteractionKey))
					{
						GameFiber.Wait(0);
					}
					bool flag4 = Game.IsKeyDown(Config.MainInteractionKey) && EntityExtensions.Exists(this.SuspectVehicle);
					if (flag4)
					{
						Game.DisplayNotification("~b~Searching the Vehicle.");
						this.SuspectVehicle.OpenDoorsForSearch(false);
						GameFiber.Wait(500);
						Game.LocalPlayer.Character.Tasks.GoStraightToPosition(this.SuspectVehicle.GetOffsetPositionRight(1.5f), 1f, this.SuspectVehicle.Heading - 90f, 1f, -1).WaitForCompletion(500);
						Game.LocalPlayer.Character.Tasks.PlayAnimation("mini@repair", "fixing_a_ped", -1f, 1).WaitForCompletion(4000);
						Game.LocalPlayer.Character.Tasks.Clear();
						Random r2 = new Random();
						switch (r2.Next(0, 3))
						{
						case 0:
							Game.LocalPlayer.Character.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", 0, true);
							StolenPoliceHardware.Weapon = "WEAPON_CARBINERIFLE";
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, We Have Recovered a Stolen Police ~r~Assault Rifle~w~ on Scene.");
							break;
						case 1:
							Game.LocalPlayer.Character.Inventory.GiveNewWeapon("WEAPON_PUMPSHOTGUN", 0, true);
							StolenPoliceHardware.Weapon = "WEAPON_PUMPSHOTGUN";
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, We Have Recovered a Stolen Police ~r~Shotgun~w~ on Scene.");
							break;
						case 2:
							Game.LocalPlayer.Character.Inventory.GiveNewWeapon("WEAPON_ADVANCEDRIFLE", 0, true);
							StolenPoliceHardware.Weapon = "WEAPON_ADVANCEDRIFLE";
							GameFiber.Wait(1000);
							Game.DisplayNotification("Dispatch, We Have Recovered a Stolen Police ~r~Rifle~w~ on Scene.");
							break;
						}
						GameFiber.Wait(2000);
						bool flag5 = this.MainScenario == 1;
						if (flag5)
						{
							Game.DisplayNotification("We Have also Recovered a Stolen ~r~Assault Rifle~w~ Used in the Shootout.");
						}
						GameFiber.Wait(2000);
						Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
						GameFiber.Wait(1500);
						this.SuspectVehicle.CloseDoorsForSearch(false);
					}
					GameFiber.Wait(1500);
				}
				else
				{
					Random r3 = new Random();
					switch (r3.Next(0, 3))
					{
					case 0:
						if (StolenPoliceHardware.<>o__19.<>p__0 == null)
						{
							StolenPoliceHardware.<>o__19.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "searchDriver", typeof(StolenPoliceHardware), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						StolenPoliceHardware.<>o__19.<>p__0.Target(StolenPoliceHardware.<>o__19.<>p__0, this.SuspectVehicle.Metadata, "~r~A Stolen Police Assault Rifle~w~, a ~g~Pair of Glasses~w~, ~g~A Buffalo Bills Cap~w~, and a ~g~Coca-Cola Bottle.");
						this.WeaponName = "Assault Rifle";
						StolenPoliceHardware.Weapon = "WEAPON_CARBINERIFLE";
						break;
					case 1:
						if (StolenPoliceHardware.<>o__19.<>p__1 == null)
						{
							StolenPoliceHardware.<>o__19.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "searchDriver", typeof(StolenPoliceHardware), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						StolenPoliceHardware.<>o__19.<>p__1.Target(StolenPoliceHardware.<>o__19.<>p__1, this.SuspectVehicle.Metadata, "~r~A Stolen Police Shotgun~w~, a ~g~Coconut~w~, a ~g~Green Bay Packers Cap~w~, and a ~g~Book.");
						this.WeaponName = "Shotgun";
						StolenPoliceHardware.Weapon = "WEAPON_PUMPSHOTGUN";
						break;
					case 2:
						if (StolenPoliceHardware.<>o__19.<>p__2 == null)
						{
							StolenPoliceHardware.<>o__19.<>p__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "searchDriver", typeof(StolenPoliceHardware), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						StolenPoliceHardware.<>o__19.<>p__2.Target(StolenPoliceHardware.<>o__19.<>p__2, this.SuspectVehicle.Metadata, "~r~A Stolen Police Rifle~w~, a ~g~Pack of Cigarettes~w~, and a ~g~Candy Bar.");
						this.WeaponName = "Rifle";
						StolenPoliceHardware.Weapon = "WEAPON_ADVANCEDRIFLE";
						break;
					}
					bool flag6 = EntityExtensions.Exists(this.SuspectVehicle);
					if (flag6)
					{
						bool flag7 = this.SuspectVehicle.HasBone("boot");
						if (flag7)
						{
							bool flag8 = this.SuspectVehicle.Doors[5].IsValid();
							if (flag8)
							{
								while (!this.SuspectVehicle.Doors[5].IsOpen)
								{
									GameFiber.Wait(0);
								}
							}
							GameFiber.Wait(6000);
						}
						else
						{
							Game.LogTrivial("YOBBINCALLOUTS: Cannot Detect Suspect Vehicle Door. Ending Callout.");
							this.End();
						}
					}
					else
					{
						this.End();
					}
					Game.DisplayNotification("Dispatch, We Have Recovered a Stolen Police ~r~" + this.WeaponName + " ~w~on Scene.");
					GameFiber.Wait(2000);
					bool flag9 = this.MainScenario == 1;
					if (flag9)
					{
						Game.DisplayNotification("We Have also Recovered a Stolen ~r~Carbine Rifle~w~ Used in the Shootout.");
					}
					GameFiber.Wait(2000);
					Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
					GameFiber.Wait(1500);
				}
				bool flag10 = this.MainScenario == 2;
				if (flag10)
				{
					Random Runaway = new Random();
					int ShouldRunaway = Runaway.Next(0, 2);
					bool flag11 = ShouldRunaway == 1 && EntityExtensions.Exists(this.Suspect);
					if (flag11)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Suspect will attempt to flee after search.");
						bool flag12 = !this.Suspect.IsCuffed && !Functions.IsPedArrested(this.Suspect);
						if (flag12)
						{
							bool flag13 = EntityExtensions.Exists(this.SuspectBlip);
							if (flag13)
							{
								this.SuspectBlip.Delete();
							}
							CallHandler.CreatePursuit(this.MainPursuit, true, true, true, new Ped[]
							{
								this.Suspect
							});
						}
						else
						{
							Game.DisplayHelp("Arrest the ~r~Suspect. ~w~Press ~b~End~w~ When Done.");
							while (!Game.IsKeyDown(Config.CalloutEndKey))
							{
								GameFiber.Wait(0);
							}
							Game.DisplayNotification("Dispatch, We are Code 4. We have ~b~Recovered the Stolen Police Weapons.");
						}
					}
					else
					{
						Game.LogTrivial("YOBBINCALLOUTS: Suspect will NOT attempt to flee after search.");
						Game.DisplayHelp("Arrest the ~r~Suspect. ~w~Press ~b~End~w~ When Done.");
						while (!Game.IsKeyDown(Config.CalloutEndKey))
						{
							GameFiber.Wait(0);
						}
						Game.DisplayNotification("Dispatch, We are Code 4. We have ~b~Recovered the Stolen Police Weapons.");
					}
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
			Game.LogTrivial("YOBBINCALLOUTS: Stolen Police Hardware Callout Finished Cleaning Up.");
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		private Vector3 MainSpawnPoint;

		
		private EWorldZoneCounty WorldZone;

		
		private LHandle MainPursuit;

		
		private Vehicle SuspectVehicle;

		
		private Ped Suspect;

		
		private Blip SuspectBlip;

		
		private bool CalloutRunning = false;

		
		private int MainScenario;

		
		private string[] Vehicles;

		
		private string[] Peds;

		
		public static string Weapon = "WEAPON_CARBINERIFLE";

		
		private string WeaponName;
	}
}
