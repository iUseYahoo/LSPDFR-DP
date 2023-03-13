using System;
using System.Drawing;
using System.Threading;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Suspicious Activity", 3)]
	public class SuspiciousActivity : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 21))
				{
				case 1:
					this.Spawnpoint = new Vector3(-860.9541f, -1224.315f, 5.93534f);
					this.Heading = 137;
					break;
				case 2:
					this.Spawnpoint = new Vector3(621.0653f, -1730.132f, 20.027f);
					this.Heading = 264;
					break;
				case 3:
					this.Spawnpoint = new Vector3(-15.80835f, -80.61652f, 56.68966f);
					this.Heading = 94;
					break;
				case 4:
					this.Spawnpoint = new Vector3(127.6069f, -1269.794f, 29.0834f);
					this.Heading = 56;
					break;
				case 5:
					this.Spawnpoint = new Vector3(290.5129f, -690.3273f, 29.05148f);
					this.Heading = 69;
					break;
				case 6:
					this.Spawnpoint = new Vector3(-1303.721f, -277.2606f, 39.35474f);
					this.Heading = 123;
					break;
				case 7:
					this.Spawnpoint = new Vector3(-685.5023f, -631.5996f, 25.05294f);
					this.Heading = 180;
					break;
				case 8:
					this.Spawnpoint = new Vector3(2345.724f, 3108.778f, 47.96938f);
					this.Heading = 332;
					break;
				case 9:
					this.Spawnpoint = new Vector3(2674.859f, 3513.062f, 52.46383f);
					this.Heading = 191;
					break;
				case 10:
					this.Spawnpoint = new Vector3(840.7313f, 3552.003f, 33.53423f);
					this.Heading = 162;
					break;
				case 11:
					this.Spawnpoint = new Vector3(137.4099f, 3290.6f, 41.79104f);
					this.Heading = 72;
					break;
				case 12:
					this.Spawnpoint = new Vector3(2069.635f, 3763.724f, 32.41343f);
					this.Heading = 323;
					break;
				case 13:
					this.Spawnpoint = new Vector3(2671.695f, 4780.761f, 36.69837f);
					this.Heading = 230;
					break;
				case 14:
					this.Spawnpoint = new Vector3(1826.492f, 5111.474f, 60.3875f);
					this.Heading = 132;
					break;
				case 15:
					this.Spawnpoint = new Vector3(-3078.414f, 1429.428f, 17.74944f);
					this.Heading = 338;
					break;
				case 16:
					this.Spawnpoint = new Vector3(-3078.976f, 812.9061f, 19.08304f);
					this.Heading = 10;
					break;
				case 17:
					this.Spawnpoint = new Vector3(1251.467f, 6519.604f, 19.48358f);
					this.Heading = 106;
					break;
				case 18:
					this.Spawnpoint = new Vector3(-103.7633f, 6501.45f, 31.24192f);
					this.Heading = 41;
					break;
				case 19:
					this.Spawnpoint = new Vector3(-550.4935f, 5811.579f, 37.47649f);
					this.Heading = 114;
					break;
				case 20:
					this.Spawnpoint = new Vector3(-751.9592f, 5652.535f, 25.01168f);
					this.Heading = 114;
					break;
				}
				bool flag = this.Spawnpoint.DistanceTo(Game.LocalPlayer.Character.Position) > 100f && this.Spawnpoint.DistanceTo(Game.LocalPlayer.Character.Position) < (float)Settings.MaxCalloutDistance;
				if (flag)
				{
					break;
				}
				GameFiber.Yield();
				WaitCount++;
				bool flag2 = WaitCount > 100;
				if (flag2)
				{
					goto Block_4;
				}
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.CalloutMessage = "Suspicious activity reported";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Suspicious Activity", "~b~Dispatch: ~w~Follow The ~y~GPS ~w~To The Location. ~w~Respond with ~g~Code 2");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_CRIME_SUSPICIOUS_ACTIVITY MC_RESPOND_CODE_02_02");
			GameFiber.Wait(1000);
			this._Searcharea = this.Spawnpoint.Around2D(1f, 2f);
			this._Blip = new Blip(this._Searcharea, 40f);
			this._Blip.EnableRoute(Color.Yellow);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			this.SuspectVehicle = new Vehicle(this.Carlist[new Random().Next(this.Carlist.Length)], this.Spawnpoint);
			this.SuspectVehicle.Heading = (float)this.Heading;
			this.SuspectVehicle.IsStolen = false;
			this.SuspectVehicle.RandomiseLicencePlate();
			this.Girl = new Ped("a_f_y_topless_01", this.SuspectVehicle.FrontPosition * 1f, (float)this.Heading);
			this.Girl.IsPersistent = true;
			this.Girl.SetVariation(8, 1, 0);
			this.Girl.WarpIntoVehicle(this.SuspectVehicle, 0);
			this.Girl.Inventory.Weapons.Clear();
			this.Driver = new Ped(this.Boys[new Random().Next(this.Boys.Length)], this.SuspectVehicle.FrontPosition * 1f, (float)this.Heading);
			this.Driver.IsPersistent = true;
			this.Driver.WarpIntoVehicle(this.SuspectVehicle, -1);
			this.Driver.Inventory.Weapons.Clear();
			new RelationshipGroup("Love");
			new RelationshipGroup("Good");
			this.Driver.RelationshipGroup = "Love";
			this.Girl.RelationshipGroup = "Love";
			int num = new Random().Next(1, 3);
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					this.h1 = false;
					this.h2 = true;
				}
			}
			else
			{
				this.h1 = true;
				this.h2 = false;
			}
			int num3 = new Random().Next(1, 1);
			int num4 = num3;
			if (num4 == 1)
			{
				this.Makinglove();
			}
			int num5 = new Random().Next(1, 3);
			int num6 = num5;
			if (num6 != 1)
			{
				if (num6 == 2)
				{
					this.Driver.Tasks.PlayAnimation("mini@prostitutes@sexnorm_veh", "sex_loop_male", 6f, 1);
					this.Girl.Tasks.PlayAnimation("mini@prostitutes@sexnorm_veh", "sex_loop_prostitute", 6f, 1);
					this.Sex = true;
				}
			}
			else
			{
				this.Driver.Tasks.PlayAnimation("mini@prostitutes@sexnorm_veh", "bj_loop_male", 6f, 1);
				this.Girl.Tasks.PlayAnimation("mini@prostitutes@sexnorm_veh", "bj_loop_prostitute", 6f, 1);
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			base.OnCalloutNotAccepted();
		}

		
		public override void Process()
		{
			GameFiber.StartNew(delegate()
			{
				bool flag = Game.LocalPlayer.Character.DistanceTo(this.SuspectVehicle) <= 10f && Functions.IsPlayerPerformingPullover() && !this.Pullover;
				if (flag)
				{
					this.Pullover = true;
					this.Driver.Tasks.Clear();
					this.Girl.Tasks.Clear();
				}
				bool flag2 = Game.IsKeyDown(Settings.EndCall);
				if (flag2)
				{
					this.End();
				}
				bool isDead = Game.LocalPlayer.Character.IsDead;
				if (isDead)
				{
					this.End();
				}
				bool flag3 = !this.Sdeath && EntityExtensions.Exists(this.Driver);
				if (flag3)
				{
					bool flag4 = this.Driver.IsDead || this.Driver.IsCuffed;
					if (flag4)
					{
						this.Sdeath = true;
					}
				}
			});
			base.Process();
		}

		
		private void Makinglove()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint) <= 20f;
						if (flag)
						{
							this._Blip.Delete();
							this._Blip = this.SuspectVehicle.AttachBlip();
							this._Blip.Color = Color.Red;
							Game.DisplayHelp("~w~Stop close and walk to the ~r~Vehicle ~w~to interact");
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = !this.Pullover && Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint) <= 7f;
						if (flag2)
						{
							bool sex = this.Sex;
							if (sex)
							{
								switch (new Random().Next(1, 4))
								{
								case 1:
									this.Girl.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_GENERIC_FEM", 1, 6);
									break;
								case 2:
									this.Girl.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_GENERIC_FEM", 2, 6);
									break;
								case 3:
									this.Girl.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_GENERIC_FEM", 3, 6);
									break;
								}
							}
							else
							{
								switch (new Random().Next(1, 4))
								{
								case 1:
									this.Girl.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_ORAL_FEM", 1, 6);
									break;
								case 2:
									this.Girl.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_ORAL_FEM", 2, 6);
									break;
								case 3:
									this.Girl.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_ORAL_FEM", 3, 6);
									break;
								}
							}
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = !this.Pullover && !this.Sdeath && Game.LocalPlayer.Character.DistanceTo(this.Driver) <= 3f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag3)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to make them stop", Settings.Interact), false);
							bool flag4 = Game.IsKeyDown(Settings.Interact);
							if (flag4)
							{
								Game.DisplaySubtitle("~b~Player: ~w~Excuse me, stop with what you guys are doing!");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag5 = !this.Pullover && !this.Sdeath && Game.LocalPlayer.Character.DistanceTo(this.Driver) <= 3f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag5)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag6 = Game.IsKeyDown(Settings.Interact);
							if (flag6)
							{
								bool sex2 = this.Sex;
								if (sex2)
								{
									this.Girl.Tasks.PlayAnimation("mini@prostitutes@sexnorm_veh", "sex_to_proposition_prostitute", 6f, 0);
									this.Driver.Tasks.PlayAnimation("mini@prostitutes@sexnorm_veh", "sex_to_proposition_male", 6f, 0);
								}
								else
								{
									this.Girl.Tasks.PlayAnimation("mini@prostitutes@sexnorm_veh", "bj_to_proposition_prostitute", 6f, 0);
									this.Driver.Tasks.PlayAnimation("mini@prostitutes@sexnorm_veh", "bj_to_proposition_male", 6f, 0);
								}
								switch (new Random().Next(1, 4))
								{
								case 1:
									GameFiber.Sleep(1500);
									Game.DisplaySubtitle("~y~Driver: ~w~Oh sorry......");
									break;
								case 2:
									GameFiber.Sleep(1500);
									Game.DisplaySubtitle("~y~Driver: ~w~Damn, I who was so close to finish.....");
									break;
								case 3:
									GameFiber.Sleep(1500);
									Game.DisplaySubtitle("~y~Driver: ~w~You scared us.....");
									break;
								}
								GameFiber.Sleep(3500);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag7 = !this.Pullover && !this.Sdeath && Game.LocalPlayer.Character.DistanceTo(this.Driver) <= 3f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag7)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag8 = Game.IsKeyDown(Settings.Interact);
							if (flag8)
							{
								Game.DisplaySubtitle("~b~Player: ~w~You can not do this in public!");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag9 = !this.Pullover && !this.Sdeath && Game.LocalPlayer.Character.DistanceTo(this.Driver) <= 3f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag9)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag10 = Game.IsKeyDown(Settings.Interact);
							if (flag10)
							{
								switch (new Random().Next(1, 4))
								{
								case 1:
									Game.DisplaySubtitle("~y~Driver: ~w~If you let us go, we'll go to a hotel instead..");
									break;
								case 2:
									Game.DisplaySubtitle("~y~Driver: ~w~Okay, will not do this again..");
									break;
								case 3:
									Game.DisplaySubtitle("~y~Driver: ~w~Did not know that its not allowed to make some love....");
									break;
								}
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag11 = !this.Pullover && !this.Sdeath && Game.LocalPlayer.Character.DistanceTo(this.Driver) <= 3f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag11)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag12 = Game.IsKeyDown(Settings.Interact);
							if (flag12)
							{
								Game.DisplaySubtitle("~b~Player: ~w~I want you guys to get out of the vehicle before we talk more, passenger put on your bra again!");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag13 = this.h1 && !this.Pullover && !this.Sdeath && Game.LocalPlayer.Character.IsOnFoot;
						if (flag13)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag14 = Game.IsKeyDown(Settings.Interact);
							if (flag14)
							{
								this.Driver.Tasks.LeaveVehicle(this.SuspectVehicle, 0);
								this.Girl.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
								this.Girl.SetVariation(8, 0, new Random().Next(0, 1));
								Functions.SetPedAsStopped(this.Girl, true);
								Functions.SetPedAsStopped(this.Driver, true);
								break;
							}
						}
						bool flag15 = this.h2 && !this.Pullover && !this.Sdeath && Game.LocalPlayer.Character.IsOnFoot;
						if (flag15)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag16 = Game.IsKeyDown(Settings.Interact);
							if (flag16)
							{
								Game.DisplaySubtitle("~y~Driver: ~w~You ruined everything!");
								this.Driver.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 500, true);
								GameFiber.Sleep(1500);
								this.Driver.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
								this.Driver.Tasks.FightAgainst(Game.LocalPlayer.Character);
								break;
							}
						}
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		public override void End()
		{
			bool flag = EntityExtensions.Exists(this.Driver);
			if (flag)
			{
				bool isDead = this.Driver.IsDead;
				if (isDead)
				{
					Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_NEUTRALIZED MC_WE_ARE_CODE_4");
					Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Suspicious activity reported", "~b~Dispatch: ~w~All Units Suspect Is Neutralized");
				}
				else
				{
					bool flag2 = Functions.IsPedArrested(this.Driver);
					if (flag2)
					{
						Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_ARRESTED MC_WE_ARE_CODE_4");
						Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Suspicious activity reported", "~b~Dispatch: ~w~All Units Suspect Is Arrested");
					}
					else
					{
						bool isAlive = this.Driver.IsAlive;
						if (isAlive)
						{
							Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
							Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Suspicious activity reported", "~b~Dispatch: ~w~All Units ~g~Code 4");
						}
					}
				}
			}
			bool flag3 = !EntityExtensions.Exists(this.Driver);
			if (flag3)
			{
				Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Suspicious activity reported", "~b~Dispatch: ~w~All Units ~g~Code 4");
			}
			bool flag4 = EntityExtensions.Exists(this.Driver);
			if (flag4)
			{
				this.Driver.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.Girl);
			if (flag5)
			{
				this.Girl.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this._Blip);
			if (flag6)
			{
				this._Blip.Delete();
			}
			bool flag7 = EntityExtensions.Exists(this._Blip2);
			if (flag7)
			{
				this._Blip2.Delete();
			}
			Game.LogTrivial("ManiacCallouts - Suspicious Activity Cleaned.");
			this.Scenariorunning = false;
			base.End();
		}

		
		private Ped Driver;

		
		private Ped Girl;

		
		private Vehicle SuspectVehicle;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private const string Prostitute = "mini@prostitutes@sexnorm_veh";

		
		private const string Girlbj = "bj_loop_prostitute";

		
		private const string Driverbj = "bj_loop_male";

		
		private const string Girlsex = "sex_loop_prostitute";

		
		private const string Driversex = "sex_loop_male";

		
		private const string Girlsexfinish = "sex_to_proposition_prostitute";

		
		private const string Girlbjfinish = "bj_to_proposition_prostitute";

		
		private const string Driversexfinish = "sex_to_proposition_male";

		
		private const string Driverbjfinish = "bj_to_proposition_male";

		
		private Vector3 _Searcharea;

		
		private Vector3 Spawnpoint;

		
		private Vector3 Dump = new Vector3(-4098.94f, -229.1018f, 24.5826f);

		
		private string[] Boys = new string[]
		{
			"a_m_m_bevhills_02",
			"a_m_m_hillbilly_02",
			"a_m_m_skater_01"
		};

		
		private string[] Carlist = new string[]
		{
			"Radi",
			"BJXL",
			"Virgo2",
			"FQ2",
			"Dominator"
		};

		
		private string[] Weaponlist = new string[]
		{
			"weapon_knife",
			"weapon_machete",
			"weapon_knuckle",
			"weapon_crowbar"
		};

		
		private bool PursuitCreated = false;

		
		private bool Sex = false;

		
		private bool Scenariorunning = false;

		
		private bool Pullover = false;

		
		private bool Sdeath = false;

		
		private bool h1 = false;

		
		private bool h2 = false;

		
		private int Heading;

		
		private int counter = 0;
	}
}
