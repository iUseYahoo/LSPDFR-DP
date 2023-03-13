using System;
using System.Drawing;
using System.Threading;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Panic Button Money Truck", 2)]
	public class MoneyTruck : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 20))
				{
				case 1:
					this.Spawnpoint = new Vector3(0.6489739f, -130.5906f, 56.49238f);
					this.Headingveh = 67;
					this.Outside = false;
					break;
				case 2:
					this.Spawnpoint = new Vector3(-221.1866f, 264.5396f, 91.84614f);
					this.Headingveh = 86;
					this.Outside = false;
					break;
				case 3:
					this.Spawnpoint = new Vector3(-1008.833f, 85.96581f, 51.79568f);
					this.Headingveh = 209;
					this.Outside = false;
					break;
				case 4:
					this.Spawnpoint = new Vector3(-865.1685f, -134.0456f, 37.69039f);
					this.Headingveh = 24;
					this.Outside = false;
					break;
				case 5:
					this.Spawnpoint = new Vector3(-659.1835f, -281.3828f, 35.5134f);
					this.Headingveh = 30;
					this.Outside = false;
					break;
				case 6:
					this.Spawnpoint = new Vector3(-266.2401f, -314.4363f, 29.96515f);
					this.Headingveh = 6;
					this.Outside = false;
					break;
				case 7:
					this.Spawnpoint = new Vector3(-1396.134f, -563.958f, 29.96033f);
					this.Headingveh = 121;
					this.Outside = false;
					break;
				case 8:
					this.Spawnpoint = new Vector3(-1680.37f, -565.5358f, 34.50112f);
					this.Headingveh = 232;
					this.Outside = false;
					break;
				case 9:
					this.Spawnpoint = new Vector3(-1207.716f, -1180.905f, 7.526902f);
					this.Headingveh = 8;
					this.Outside = false;
					break;
				case 10:
					this.Spawnpoint = new Vector3(-851.9908f, -932.9697f, 15.43341f);
					this.Headingveh = 359;
					this.Outside = false;
					break;
				case 11:
					this.Spawnpoint = new Vector3(-269.2286f, -874.4626f, 31.11376f);
					this.Headingveh = 253;
					this.Outside = false;
					break;
				case 12:
					this.Spawnpoint = new Vector3(167.8288f, -872.8312f, 30.26717f);
					this.Headingveh = 337;
					this.Outside = false;
					break;
				case 13:
					this.Spawnpoint = new Vector3(-162.3851f, 6376.363f, 31.17852f);
					this.Headingveh = 134;
					this.Outside = true;
					break;
				case 14:
					this.Spawnpoint = new Vector3(58.31182f, 6621.148f, 31.34854f);
					this.Headingveh = 223;
					this.Outside = true;
					break;
				case 15:
					this.Spawnpoint = new Vector3(1670.478f, 4860.163f, 41.80394f);
					this.Headingveh = 6;
					this.Outside = true;
					break;
				case 16:
					this.Spawnpoint = new Vector3(2474.743f, 4059.042f, 37.45325f);
					this.Headingveh = 155;
					this.Outside = true;
					break;
				case 17:
					this.Spawnpoint = new Vector3(1385.996f, 3582.998f, 34.74415f);
					this.Headingveh = 108;
					this.Outside = true;
					break;
				case 18:
					this.Spawnpoint = new Vector3(1199.457f, 2679.253f, 37.50691f);
					this.Headingveh = 267;
					this.Outside = true;
					break;
				case 19:
					this.Spawnpoint = new Vector3(678.9715f, 2703.685f, 40.37388f);
					this.Headingveh = 92;
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
			base.CalloutMessage = "Money Truck Panic Button";
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
				this.PVehicle = new string[]
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
				this.PVehicle = new string[]
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
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Money Truck Panic Button", "~b~Dispatch: ~w~Follow The ~y~GPS ~w~To The Location. ~w~Respond with ~g~Code 3");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_CRIME_ARMOURED_CAR_ROBBERY MC_RESPOND_CODE3");
			Extensions.ClearAreaOfVehicles(this.Spawnpoint, 20f);
			this.Moneytruck = new Vehicle("Stockade", this.Spawnpoint, (float)this.Headingveh);
			this.Moneytruck.RandomiseLicencePlate();
			this.Moneytruck.IsPersistent = true;
			this.Moneytruck.Doors[0].IsOpen = true;
			this.Moneytruck.Doors[2].IsFullyOpen = true;
			this.Moneytruck.Doors[3].IsFullyOpen = true;
			this.Moneytruck.Wheels[0].BurstTire();
			this.Moneytruck.Wheels[1].BurstTire();
			this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Moneytruck.GetOffsetPosition(Vector3.RelativeFront * 10f), this.Moneytruck.Heading * 10f);
			this.SuspectVehicle.RandomiseLicencePlate();
			this.SuspectVehicle.IsPersistent = true;
			this.SuspectVehicle.IsStolen = true;
			this.SuspectVehicle.Doors[1].Open(true);
			this.SuspectVehicle.Doors[2].Open(true);
			this.SuspectVehicle.Doors[3].Open(true);
			this.Driver = new Ped("mp_m_bogdangoon", this.Spawnpoint, 0f);
			this.Driver.SetVariation(1, 0, 0);
			this.Driver.WarpIntoVehicle(this.SuspectVehicle, -1);
			this.Driver.IsPersistent = true;
			this.Driver.BlockPermanentEvents = true;
			this.Suspect = new Ped("mp_m_bogdangoon", this.Moneytruck.GetOffsetPosition(Vector3.RelativeRight * 4f), 0f);
			this.Suspect.SetVariation(1, 0, 0);
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect2 = new Ped("mp_m_bogdangoon", this.Moneytruck.GetOffsetPosition(Vector3.RelativeBack * 8.5f), this.Moneytruck.Heading);
			this.Suspect2.SetVariation(1, 0, 0);
			this.Suspect2.IsPersistent = true;
			this.Suspect2.BlockPermanentEvents = true;
			this.Suspect3 = new Ped("mp_m_bogdangoon", this.Moneytruck.GetOffsetPosition(Vector3.RelativeBack * 7f), 0f);
			this.Suspect3.SetVariation(1, 0, 0);
			this.Suspect3.IsPersistent = true;
			this.Suspect3.BlockPermanentEvents = true;
			this.Hostage = new Ped("s_m_m_armoured_01", this.Spawnpoint, 0f);
			this.Hostage.WarpIntoVehicle(this.Moneytruck, 2);
			this.Hostage.IsPersistent = true;
			this.Hostage.BlockPermanentEvents = true;
			this.Hostage.Tasks.LeaveVehicle(this.Moneytruck, 256);
			this.Hostage2 = new Ped("s_m_m_armoured_02", this.Spawnpoint, 0f);
			this.Hostage2.WarpIntoVehicle(this.Moneytruck, 0);
			this.Hostage2.IsPersistent = true;
			this.Hostage2.BlockPermanentEvents = true;
			this.Hostage2.Tasks.LeaveVehicle(this.Moneytruck, 256);
			this._Blip = this.Moneytruck.AttachBlip();
			this._Blip.EnableRoute(Color.Yellow);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			new RelationshipGroup("BAD");
			this.Driver.RelationshipGroup = "BAD";
			this.Suspect.RelationshipGroup = "BAD";
			this.Suspect2.RelationshipGroup = "BAD";
			this.Suspect3.RelationshipGroup = "BAD";
			this.Driver.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
			this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
			this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
			this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
			switch (new Random().Next(1, 5))
			{
			case 1:
				this.Flee();
				break;
			case 2:
				this.Shootingpolice();
				break;
			case 3:
				this.Flee();
				break;
			case 4:
				this.Shootingpolice();
				break;
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
				Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Money Truck Panic Button", "~b~Dispatch: ~w~All Units ~g~Code 4");
				GameFiber.Sleep(2000);
				this.End();
			}
			bool isDead = Game.LocalPlayer.Character.IsDead;
			if (isDead)
			{
				this.End();
			}
			base.Process();
		}

		
		private void Shootingpolice()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = !this.Money;
						if (flag)
						{
							this.Police = new Vehicle(this.PVehicle[new Random().Next(this.PVehicle.Length)], this.Moneytruck.GetOffsetPosition(Vector3.RelativeBack * 23f), (float)this.Headingveh);
							this.Police.IsPersistent = true;
							this.Police.IsSirenOn = true;
							this.Cop = new Ped(this.Coplist[new Random().Next(this.Coplist.Length)], this.Spawnpoint, 0f);
							this.Cop.WarpIntoVehicle(this.Police, -1);
							this.Cop.IsPersistent = true;
							this.Cop.BlockPermanentEvents = true;
							this.Cop.Tasks.LeaveVehicle(this.Police, 256);
							this.Cop2 = new Ped(this.Coplist[new Random().Next(this.Coplist.Length)], this.Spawnpoint, 0f);
							this.Cop2.WarpIntoVehicle(this.Police, 0);
							this.Cop2.IsPersistent = true;
							this.Cop2.BlockPermanentEvents = true;
							this.Cop2.Tasks.LeaveVehicle(this.Police, 256);
							this.Cop.MaxHealth = 170;
							this.Cop2.MaxHealth = 170;
							this.Cop.Health = 100;
							this.Cop2.Health = 100;
							this.Hostage.MaxHealth = 120;
							this.Hostage2.MaxHealth = 120;
							this.Hostage.Health = 100;
							this.Hostage2.Health = 100;
							this.Cop.Inventory.GiveNewWeapon("weapon_pistol", 500, true);
							this.Cop2.Inventory.GiveNewWeapon("weapon_pistol", 500, true);
							this.Suspect.Tasks.AimWeaponAt(this.Hostage2, -1);
							this.Suspect3.Tasks.AimWeaponAt(this.Hostage, -1);
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage02", 6f, 1);
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Driver.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Moneytruck.Position) <= 70f;
						if (flag2)
						{
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVFEMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "SECURITY_GUARD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PRIVATE_SECURITY", 5);
							this.Suspect.Accuracy = 55;
							this.Driver.Accuracy = 55;
							this.Suspect2.Accuracy = 55;
							this.Suspect3.Accuracy = 55;
							this.SuspectVehicle.IsDriveable = false;
							this.Cop.Tasks.FightAgainst(this.Suspect2);
							this.Cop2.Tasks.FightAgainst(this.Suspect2);
							this.Suspect.Tasks.FightAgainst(this.Hostage2);
							this.Suspect2.Tasks.FireWeaponAt(this.Cop, -1, -957453492);
							this.Suspect3.Tasks.FireWeaponAt(this.Cop2, -1, -957453492);
							this.Driver.Tasks.FightAgainstClosestHatedTarget(100f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = this.Cop.IsDead && this.Cop2.IsDead;
						if (flag3)
						{
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(100f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(100f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(100f);
							this.Suspect3.Tasks.FightAgainst(this.Hostage);
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

		
		private void Flee()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = !this.Money;
						if (flag)
						{
							this.SuspectVehicle.IsEngineOn = true;
							this.Hostage.MaxHealth = 120;
							this.Hostage2.MaxHealth = 120;
							this.Hostage.Health = 100;
							this.Hostage2.Health = 100;
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage02", 6f, 1);
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVFEMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "SECURITY_GUARD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PRIVATE_SECURITY", 5);
							this.Suspect.Accuracy = 55;
							this.Driver.Accuracy = 55;
							this.Suspect2.Accuracy = 55;
							this.Suspect3.Accuracy = 55;
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Driver.KeepTasks = true;
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Moneytruck.Position) <= 55f;
						if (flag2)
						{
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Driver.Tasks.CruiseWithVehicle(this.SuspectVehicle, 2.1474836E+09f, 262710);
							GameFiber.Sleep(4000);
							this.Hostage.Tasks.Flee(this.Moneytruck.Position, 500f, -1);
							this.Hostage2.Tasks.Flee(this.Moneytruck.Position, 500f, -1);
							this.Pursuit = Functions.CreatePursuit();
							Functions.AddPedToPursuit(this.Pursuit, this.Driver);
							Functions.AddPedToPursuit(this.Pursuit, this.Suspect);
							Functions.AddPedToPursuit(this.Pursuit, this.Suspect2);
							Functions.AddPedToPursuit(this.Pursuit, this.Suspect3);
							Functions.SetPursuitIsActiveForPlayer(this.Pursuit, true);
							Functions.SetPursuitDisableAIForPed(this.Suspect, true);
							Functions.SetPursuitDisableAIForPed(this.Suspect2, true);
							Functions.SetPursuitDisableAIForPed(this.Suspect3, true);
							this.PursuitCreated = true;
							this._Blip.Delete();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool pursuitCreated = this.PursuitCreated;
						if (pursuitCreated)
						{
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(80f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(80f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(80f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = this.Driver.IsStopped || this.Driver.IsDead;
						if (flag3)
						{
							Functions.SetPursuitDisableAIForPed(this.Driver, true);
							this.Driver.Tasks.FightAgainstClosestHatedTarget(80f);
							this.Driver.BlockPermanentEvents = false;
							this.Suspect.BlockPermanentEvents = false;
							this.Suspect2.BlockPermanentEvents = false;
							this.Suspect3.BlockPermanentEvents = false;
							this.Driver.Tasks.LeaveVehicle(256);
							this.Suspect.Tasks.LeaveVehicle(256);
							this.Suspect2.Tasks.LeaveVehicle(256);
							this.Suspect3.Tasks.LeaveVehicle(256);
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
			Game.LogTrivial("ManiacCallouts - Money Truck Cleaned.");
			bool flag = EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				this.Suspect.Dismiss();
			}
			bool flag2 = EntityExtensions.Exists(this.Suspect2);
			if (flag2)
			{
				this.Suspect2.Dismiss();
			}
			bool flag3 = EntityExtensions.Exists(this.Suspect3);
			if (flag3)
			{
				this.Suspect3.Dismiss();
			}
			bool flag4 = EntityExtensions.Exists(this.Driver);
			if (flag4)
			{
				this.Driver.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.Hostage);
			if (flag5)
			{
				this.Hostage.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this.Hostage2);
			if (flag6)
			{
				this.Hostage2.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Police);
			if (flag7)
			{
				this.Police.Dismiss();
			}
			bool flag8 = EntityExtensions.Exists(this._Blip);
			if (flag8)
			{
				this._Blip.Delete();
			}
			bool flag9 = EntityExtensions.Exists(this.SuspectVehicle);
			if (flag9)
			{
				this.SuspectVehicle.Dismiss();
			}
			bool flag10 = EntityExtensions.Exists(this.Moneytruck);
			if (flag10)
			{
				this.Moneytruck.Dismiss();
			}
			bool flag11 = EntityExtensions.Exists(this.Cop);
			if (flag11)
			{
				this.Cop.Dismiss();
			}
			bool flag12 = EntityExtensions.Exists(this.Cop2);
			if (flag12)
			{
				this.Cop2.Dismiss();
			}
			base.End();
		}

		
		private Ped Cop;

		
		private Ped Cop2;

		
		private Ped Driver;

		
		private Ped Suspect;

		
		private Ped Suspect2;

		
		private Ped Suspect3;

		
		private Ped Hostage;

		
		private Ped Hostage2;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private Vehicle SuspectVehicle;

		
		private Vehicle Moneytruck;

		
		private Vehicle Police;

		
		private Object Moneybag;

		
		private const string Hostagefloor = "missprologueig_2";

		
		private const string Hostagefloor1 = "idle_on_floor_malehostage01";

		
		private const string Hostagefloor2 = "idle_on_floor_malehostage02";

		
		private Vector3 Spawnpoint;

		
		private Vector3 Dump = new Vector3(757.0891f, 5721.721f, 691.8749f);

		
		private string[] PVehicle;

		
		private string[] Weaponlist = new string[]
		{
			"weapon_assaultrifle",
			"weapon_carbinerifle",
			"weapon_gusenberg"
		};

		
		private string[] SVehicle = new string[]
		{
			"Youga",
			"Burrito4",
			"Burrito3",
			"Rumpo"
		};

		
		private string[] Coplist;

		
		private bool PursuitCreated = false;

		
		private bool Scenariorunning = false;

		
		private bool Money = false;

		
		private bool Outside = false;

		
		private int Headingveh;

		
		private int counter = 0;
	}
}
