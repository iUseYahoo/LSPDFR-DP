using System;
using System.Drawing;
using System.Threading;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Backup Traffic Stop", 2)]
	public class BackupTrafficStop : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 34))
				{
				case 1:
					this.Spawnpoint = new Vector3(37.6969f, -135.7885f, 55.08168f);
					this.Headingveh = 67;
					this.Outside = false;
					break;
				case 2:
					this.Spawnpoint = new Vector3(-23.80379f, -141.7431f, 56.60315f);
					this.Headingveh = 249;
					this.Outside = false;
					break;
				case 3:
					this.Spawnpoint = new Vector3(62.63686f, 232.8727f, 108.9438f);
					this.Headingveh = 248;
					this.Outside = false;
					break;
				case 4:
					this.Spawnpoint = new Vector3(184.3901f, 216.1988f, 105.3638f);
					this.Headingveh = 68;
					this.Outside = false;
					break;
				case 5:
					this.Spawnpoint = new Vector3(287.1385f, 150.3098f, 103.7992f);
					this.Headingveh = 250;
					this.Outside = false;
					break;
				case 6:
					this.Spawnpoint = new Vector3(-476.8191f, 119.9959f, 63.66432f);
					this.Headingveh = 266;
					this.Outside = false;
					break;
				case 7:
					this.Spawnpoint = new Vector3(-587.2502f, 139.9274f, 60.66141f);
					this.Headingveh = 92;
					this.Outside = false;
					break;
				case 8:
					this.Spawnpoint = new Vector3(-958.9376f, -14.96106f, 44.30987f);
					this.Headingveh = 215;
					this.Outside = false;
					break;
				case 9:
					this.Spawnpoint = new Vector3(-1165.749f, -142.8915f, 39.37038f);
					this.Headingveh = 243;
					this.Outside = false;
					break;
				case 10:
					this.Spawnpoint = new Vector3(-1266.336f, -615.6788f, 26.75868f);
					this.Headingveh = 125;
					this.Outside = false;
					break;
				case 11:
					this.Spawnpoint = new Vector3(-738.5519f, -630.7793f, 29.89334f);
					this.Headingveh = 358;
					this.Outside = false;
					break;
				case 12:
					this.Spawnpoint = new Vector3(-696.5873f, -963.6437f, 19.22344f);
					this.Headingveh = 269;
					this.Outside = false;
					break;
				case 13:
					this.Spawnpoint = new Vector3(36.46244f, -953.909f, 29.04481f);
					this.Headingveh = 339;
					this.Outside = false;
					break;
				case 14:
					this.Spawnpoint = new Vector3(-83.49078f, -1046.973f, 27.46225f);
					this.Headingveh = 158;
					this.Outside = false;
					break;
				case 15:
					this.Spawnpoint = new Vector3(310.4351f, -1106.723f, 29.07863f);
					this.Headingveh = 358;
					this.Outside = false;
					break;
				case 16:
					this.Spawnpoint = new Vector3(262.3254f, -1562.271f, 28.77928f);
					this.Headingveh = 300;
					this.Outside = false;
					break;
				case 17:
					this.Spawnpoint = new Vector3(-192.1977f, -1774.508f, 29.45073f);
					this.Headingveh = 120;
					this.Outside = false;
					break;
				case 18:
					this.Spawnpoint = new Vector3(1732.354f, 3522.231f, 36.00938f);
					this.Headingveh = 299;
					this.Outside = true;
					break;
				case 19:
					this.Spawnpoint = new Vector3(1598.873f, 3689.93f, 34.18924f);
					this.Headingveh = 32;
					this.Outside = true;
					break;
				case 20:
					this.Spawnpoint = new Vector3(1038.843f, 3530.008f, 33.76154f);
					this.Headingveh = 270;
					this.Outside = true;
					break;
				case 21:
					this.Spawnpoint = new Vector3(1310.527f, 2690.678f, 37.26306f);
					this.Headingveh = 89;
					this.Outside = true;
					break;
				case 22:
					this.Spawnpoint = new Vector3(2395.253f, 3940.208f, 35.77938f);
					this.Headingveh = 135;
					this.Outside = true;
					break;
				case 23:
					this.Spawnpoint = new Vector3(2403.353f, 4651.896f, 36.46068f);
					this.Headingveh = 44;
					this.Outside = true;
					break;
				case 24:
					this.Spawnpoint = new Vector3(2611.5f, 4279.709f, 42.63252f);
					this.Headingveh = 320;
					this.Outside = true;
					break;
				case 25:
					this.Spawnpoint = new Vector3(2089.241f, 5027.213f, 40.68177f);
					this.Headingveh = 314;
					this.Outside = true;
					break;
				case 26:
					this.Spawnpoint = new Vector3(1700.218f, 4722.113f, 42.01181f);
					this.Headingveh = 18;
					this.Outside = true;
					break;
				case 27:
					this.Spawnpoint = new Vector3(1769.211f, 5010.764f, 52.57727f);
					this.Headingveh = 313;
					this.Outside = true;
					break;
				case 28:
					this.Spawnpoint = new Vector3(25.83919f, 6568.95f, 31.05866f);
					this.Headingveh = 134;
					this.Outside = true;
					break;
				case 29:
					this.Spawnpoint = new Vector3(-406.9266f, 6212.828f, 31.23243f);
					this.Headingveh = 358;
					this.Outside = true;
					break;
				case 30:
					this.Spawnpoint = new Vector3(-568.0366f, 5662.241f, 37.96066f);
					this.Headingveh = 332;
					this.Outside = true;
					break;
				case 31:
					this.Spawnpoint = new Vector3(-444.9559f, 5939.111f, 31.86391f);
					this.Headingveh = 141;
					this.Outside = true;
					break;
				case 32:
					this.Spawnpoint = new Vector3(-857.2903f, 5427.56f, 34.4995f);
					this.Headingveh = 297;
					this.Outside = true;
					break;
				case 33:
					this.Spawnpoint = new Vector3(-1107.843f, 2662.975f, 18.08126f);
					this.Headingveh = 310;
					this.Outside = true;
					break;
				}
				bool flag = this.Spawnpoint.DistanceTo(Game.LocalPlayer.Character.Position) > 200f && this.Spawnpoint.DistanceTo(Game.LocalPlayer.Character.Position) < (float)Settings.MaxCalloutDistance;
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
			base.CalloutMessage = "Backup Required Traffic Stop";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			bool outside = this.Outside;
			if (outside)
			{
				this.CVehicle = new string[]
				{
					"sheriff",
					"sheriff2"
				};
				this.Coplist = new string[]
				{
					"s_f_y_sheriff_01",
					"s_m_y_sheriff_01"
				};
			}
			else
			{
				this.CVehicle = new string[]
				{
					"POLICE",
					"POLICE2",
					"POLICE3"
				};
				this.Coplist = new string[]
				{
					"S_M_Y_COP_01",
					"S_F_Y_COP_01"
				};
			}
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Backup Required Traffic Stop", "~b~Dispatch: ~w~Follow The ~b~GPS ~w~To The Location. ~w~Respond with ~g~Code 2");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_ASSISTANCE_REQUIRED_03 MC_RESPOND_CODE_02_02");
			GameFiber.Wait(1000);
			Extensions.ClearAreaOfVehicles(this.Spawnpoint, 10f);
			this.PoliceVehicle = new Vehicle(this.CVehicle[new Random().Next(this.CVehicle.Length)], this.Spawnpoint, (float)this.Headingveh);
			this.PoliceVehicle.IsPersistent = true;
			this.PoliceVehicle.IsSirenSilent = true;
			GameFiber.Wait(1000);
			this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.PoliceVehicle.GetOffsetPosition(Vector3.RelativeFront * 9f), this.PoliceVehicle.Heading);
			this.SuspectVehicle.RandomiseLicencePlate();
			this.SuspectVehicle.IsPersistent = true;
			this._Blip = this.PoliceVehicle.AttachBlip();
			this._Blip.EnableRoute(Color.Blue);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			this.Suspect = new Ped(this.Suspectlist[new Random().Next(this.Suspectlist.Length)], this.Spawnpoint, 0f);
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
			new RelationshipGroup("BAD");
			this.Suspect.RelationshipGroup = "BAD";
			this.Cop = new Ped(this.Coplist[new Random().Next(this.Coplist.Length)], this.Spawnpoint, 0f);
			this.Cop.WarpIntoVehicle(this.PoliceVehicle, -1);
			this.Cop.IsPersistent = true;
			this.Cop.BlockPermanentEvents = true;
			int num = new Random().Next(1, 3);
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					this.Suspectshooting();
				}
			}
			else
			{
				this.Trafficstop();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			base.OnCalloutNotAccepted();
		}

		
		public override void Process()
		{
			bool flag = Game.IsKeyDown(Settings.EndCall);
			if (flag)
			{
				this.End();
			}
			bool isDead = Game.LocalPlayer.Character.IsDead;
			if (isDead)
			{
				this.End();
			}
			base.Process();
		}

		
		private void Trafficstop()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.PoliceVehicle.Position) <= 40f;
						if (flag)
						{
							GameFiber.Wait(100);
							Game.DisplayHelp("~w~Stop behind the ~y~Police Vehicle");
							GameFiber.Sleep(3000);
							Functions.StartPulloverOnParkedVehicle(this.SuspectVehicle, true, false);
							GameFiber.Wait(100);
							this.Cop.Tasks.LeaveVehicle(this.PoliceVehicle, 256);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Cop.Position) <= 10f && Game.LocalPlayer.Character.IsOnFoot && !this.PursuitCreated;
						if (flag2)
						{
							Game.DisplayHelp("~w~Go and talk to the ~b~Police Officer");
							this._Blip.Delete();
							this._Blip2 = this.Cop.AttachBlip();
							this._Blip2.Color = Color.Blue;
							this._Blip2.Alpha = 0.5f;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.DistanceTo(this.Cop.Position) <= 2f && Game.LocalPlayer.Character.IsOnFoot && !this.PursuitCreated;
						if (flag3)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to talk to the Officer!", Settings.Interact), false);
							bool flag4 = Game.IsKeyDown(Settings.Interact);
							if (flag4)
							{
								Game.DisplaySubtitle("~b~Player: ~w~Hello, How can i help?");
								GameFiber.Sleep(2500);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag5 = Game.LocalPlayer.Character.DistanceTo(this.Cop.Position) <= 2f && Game.LocalPlayer.Character.IsOnFoot && !this.PursuitCreated;
						if (flag5)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag6 = Game.IsKeyDown(Settings.Interact);
							if (flag6)
							{
								switch (new Random().Next(1, 5))
								{
								case 1:
									this.Policetalk = "~b~Officer: ~w~I am feeling pretty nervous, can you handle this stop?";
									break;
								case 2:
									this.Policetalk = "~b~Officer: ~w~It was a long time ago i made a stop, can you show how its done?";
									break;
								case 3:
									this.Policetalk = "~b~Officer: ~w~It's my first day out alone and i feel insecure about this, can you take care of it?";
									break;
								case 4:
									this.Policetalk = "~b~Officer: ~w~I feel a little scared because last time I ended up in the hospital, can you take care of it?";
									break;
								}
								Game.DisplaySubtitle(this.Policetalk);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag7 = Game.LocalPlayer.Character.DistanceTo(this.Cop.Position) <= 2f && Game.LocalPlayer.Character.IsOnFoot && !this.PursuitCreated;
						if (flag7)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag8 = Game.IsKeyDown(Settings.Interact);
							if (flag8)
							{
								Game.DisplaySubtitle("~b~Player: ~w~No problem, any reason you made the stop?");
								Functions.SetPedAsCop(this.Cop);
								this.Cop.BlockPermanentEvents = false;
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag9 = Game.LocalPlayer.Character.DistanceTo(this.Cop.Position) <= 2f && Game.LocalPlayer.Character.IsOnFoot && !this.PursuitCreated;
						if (flag9)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag10 = Game.IsKeyDown(Settings.Interact);
							if (flag10)
							{
								bool outside = this.Outside;
								if (outside)
								{
									switch (new Random().Next(1, 6))
									{
									case 1:
										this.Policetalk = "~b~Officer: ~w~The driver pointed his middle finger at me...";
										break;
									case 2:
										this.Policetalk = "~b~Officer: ~w~The driver drove a little faster than the limit here..";
										break;
									case 3:
										this.Policetalk = "~b~Officer: ~w~The driver was driving very slowly...";
										break;
									case 4:
										this.Policetalk = "~b~Officer: ~w~The driver was driving while looking at the phone...";
										break;
									case 5:
										this.Policetalk = "~b~Officer: ~w~The driver threw rubbish..";
										break;
									}
								}
								else
								{
									switch (new Random().Next(1, 9))
									{
									case 1:
										this.Policetalk = "~b~Officer: ~w~The driver drove towards red light..";
										break;
									case 2:
										this.Policetalk = "~b~Officer: ~w~The driver drove a little faster than the limit here..";
										break;
									case 3:
										this.Policetalk = "~b~Officer: ~w~The driver did not use turn signals..";
										break;
									case 4:
										this.Policetalk = "~b~Officer: ~w~The driver ignored a stop sign...";
										break;
									case 5:
										this.Policetalk = "~b~Officer: ~w~The driver was driving very slowly...";
										break;
									case 6:
										this.Policetalk = "~b~Officer: ~w~The driver was driving while looking at the phone...";
										break;
									case 7:
										this.Policetalk = "~b~Officer: ~w~The driver pointed his middle finger at me...";
										break;
									case 8:
										this.Policetalk = "~b~Officer: ~w~The driver threw rubbish..";
										break;
									}
								}
								Game.DisplaySubtitle(this.Policetalk);
								GameFiber.Sleep(4000);
								this.Trafficstopaction();
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

		
		private void Trafficstopaction()
		{
			int num = new Random().Next(1, 3);
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					this.Cop.Tasks.FollowNavigationMeshToPosition(this.SuspectVehicle.GetOffsetPosition(Vector3.RelativeBack * 2f), this.SuspectVehicle.Heading, 1f).WaitForCompletion(1000);
				}
			}
			else
			{
				this.Cop.Tasks.FollowNavigationMeshToPosition(this.SuspectVehicle.GetOffsetPosition(Vector3.RelativeBack * 2f), this.SuspectVehicle.Heading, 1f).WaitForCompletion(1000);
				GameFiber.Sleep(1500);
				this.Suspect.Tasks.CruiseWithVehicle(2.1474836E+09f);
				GameFiber.Sleep(1500);
				Functions.ForceEndCurrentPullover();
				this.Suspect.Tasks.Clear();
				this.Pursuit = Functions.CreatePursuit();
				Functions.AddPedToPursuit(this.Pursuit, this.Suspect);
				Functions.SetPursuitIsActiveForPlayer(this.Pursuit, true);
			}
		}

		
		private void Suspectshooting()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.PoliceVehicle.Position) <= 70f;
						if (flag)
						{
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 500, true);
							GameFiber.Wait(100);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256).WaitForCompletion(1000);
							this.Suspect.Tasks.AimWeaponAt(this.Cop, -1);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.PoliceVehicle.Position) <= 50f;
						if (flag2)
						{
							Functions.SetPedCantBeArrestedByPlayer(this.Suspect, true);
							Game.DisplayHelp("~w~Stop behind the ~y~Police Vehicle");
							GameFiber.Wait(1000);
							this.Suspect.Tasks.FireWeaponAt(this.Cop, -1, -957453492);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool isDead = this.Cop.IsDead;
						if (isDead)
						{
							this._Blip.Delete();
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							this.Suspect.Tasks.EnterVehicle(this.SuspectVehicle, -1).WaitForCompletion(2000);
							this.Pursuit = Functions.CreatePursuit();
							Functions.AddPedToPursuit(this.Pursuit, this.Suspect);
							Functions.SetPursuitIsActiveForPlayer(this.Pursuit, true);
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							Functions.SetPedCantBeArrestedByPlayer(this.Suspect, false);
							break;
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
			this.Scenariorunning = false;
			Functions.ForceEndCurrentPullover();
			Game.LogTrivial("ManiacCallouts - Backup Activity Cleaned.");
			bool flag = EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				bool isDead = this.Suspect.IsDead;
				if (isDead)
				{
					Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_NEUTRALIZED MC_WE_ARE_CODE_4");
					Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Backup Required Traffic Stop", "~b~Dispatch: ~w~All Units Suspect Is Neutralized");
				}
				else
				{
					bool flag2 = Functions.IsPedArrested(this.Suspect);
					if (flag2)
					{
						Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_ARRESTED MC_WE_ARE_CODE_4");
						Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Backup Required Traffic Stop", "~b~Dispatch: ~w~All Units Suspect Is Arrested");
					}
					else
					{
						bool isAlive = this.Suspect.IsAlive;
						if (isAlive)
						{
							Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
							Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Backup Required Traffic Stop", "~b~Dispatch: ~w~All Units ~g~Code 4");
						}
					}
				}
			}
			bool flag3 = !EntityExtensions.Exists(this.Suspect);
			if (flag3)
			{
				Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Backup Required Traffic Stop", "~b~Dispatch: ~w~All Units ~g~Code 4");
			}
			bool flag4 = EntityExtensions.Exists(this._Blip);
			if (flag4)
			{
				this._Blip.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this._Blip2);
			if (flag5)
			{
				this._Blip2.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.Cop);
			if (flag6)
			{
				this.Cop.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Suspect);
			if (flag7)
			{
				this.Suspect.Dismiss();
			}
			bool flag8 = EntityExtensions.Exists(this.Suspect2);
			if (flag8)
			{
				this.Suspect2.Dismiss();
			}
			base.End();
		}

		
		private Ped Suspect;

		
		private Ped Suspect2;

		
		private Ped Cop;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private Vehicle SuspectVehicle;

		
		private Vehicle PoliceVehicle;

		
		private Vector3 Spawnpoint;

		
		private Vector3 Dump = new Vector3(757.0891f, 5721.721f, 691.8749f);

		
		private string[] Suspectlist = new string[]
		{
			"a_f_m_downtown_01",
			"a_m_m_hillbilly_02",
			"a_f_m_fatwhite_01",
			"a_f_y_hippie_01",
			"a_m_m_salton_03",
			"a_m_m_genfat_02",
			"a_m_m_soucent_03",
			"a_f_y_rurmeth_01"
		};

		
		private string[] Coplist;

		
		private string[] Weaponlist = new string[]
		{
			"weapon_pumpshotgun",
			"weapon_assaultrifle",
			"weapon_microsmg",
			"weapon_compactrifle"
		};

		
		private string[] CVehicle;

		
		private string[] SVehicle = new string[]
		{
			"BISON2",
			"BJXL",
			"CAVALCADE",
			"CHEETAH",
			"COGCABRIO",
			"ASEA",
			"ADDER",
			"FELON",
			"FELON2",
			"WARRENER",
			"RAPIDGT",
			"INTRUDER",
			"FELTZER2",
			"FQ2",
			"RANCHERXL",
			"REBEL",
			"SCHWARZER",
			"COQUETTE",
			"CARBONIZZARE",
			"EMPEROR",
			"SULTAN",
			"EXEMPLAR",
			"MASSACRO",
			"DOMINATOR",
			"ASTEROPE",
			"PRAIRIE",
			"NINEF",
			"WASHINGTON",
			"CHINO",
			"CASCO",
			"DILETTANTE",
			"VIRGO",
			"F620",
			"PRIMO",
			"SULTAN",
			"EXEMPLAR",
			"F620",
			"FELON2",
			"FELON",
			"SENTINEL",
			"WINDSOR",
			"DOMINATOR",
			"DUKES",
			"GAUNTLET",
			"VIRGO",
			"ADDER",
			"BUFFALO",
			"MASSACRO",
			"DUKES",
			"BALLER",
			"BALLER2",
			"BISON"
		};

		
		private bool PursuitCreated = false;

		
		private bool Blip = false;

		
		private bool Hint = false;

		
		private bool Foot = false;

		
		private bool Outside = false;

		
		private bool Scenariorunning = false;

		
		private string Policetalk;

		
		private int Headingveh;

		
		private int counter = 0;
	}
}
