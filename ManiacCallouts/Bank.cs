using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Bank Robbery", 2)]
	public class Bank : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 8))
				{
				case 1:
					this.Calloutloc = new Vector3(-350.0241f, -46.82206f, 49.03683f);
					this.s1 = true;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					break;
				case 2:
					this.Calloutloc = new Vector3(-2966.343f, 482.8001f, 15.69272f);
					this.s1 = false;
					this.s2 = true;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					break;
				case 3:
					this.Calloutloc = new Vector3(-1214.314f, -327.7768f, 37.72892f);
					this.s1 = false;
					this.s2 = false;
					this.s3 = true;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					break;
				case 4:
					this.Calloutloc = new Vector3(150.8908f, -1037.565f, 29.37257f);
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = true;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					break;
				case 5:
					this.Calloutloc = new Vector3(1175.303f, 2703.368f, 38.17269f);
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = true;
					this.s6 = false;
					this.s7 = false;
					break;
				case 6:
					this.Calloutloc = new Vector3(315.1911f, -275.9577f, 54.15064f);
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = true;
					this.s7 = false;
					break;
				case 7:
					this.Calloutloc = new Vector3(-110.8535f, 6462.692f, 31.64077f);
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = true;
					break;
				}
				bool flag = this.Calloutloc.DistanceTo(Game.LocalPlayer.Character.Position) > 200f && this.Calloutloc.DistanceTo(Game.LocalPlayer.Character.Position) < (float)Settings.MaxCalloutDistance;
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
			base.ShowCalloutAreaBlipBeforeAccepting(this.Calloutloc, 30f);
			base.CalloutMessage = "Bank Silent Alarm";
			base.CalloutPosition = this.Calloutloc;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Calloutloc);
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Bank Silent Alarm", "~b~Dispatch: ~w~Follow The ~y~GPS ~w~To The Location. ~w~Respond with ~g~Code 3");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_CRIME_ROBBERY_03 MC_RESPOND_CODE3");
			this._Searcharea = this.Calloutloc.Around2D(1f, 2f);
			this._Blip = new Blip(this._Searcharea, 40f);
			this._Blip.EnableRoute(Color.Yellow);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			bool flag = this.s1;
			if (flag)
			{
				this.FleecaDoorCheck = true;
				this.DoorInside = new Vector3(-355.5635f, -50.42026f, 49.03642f);
				this.VaultDoor = new Vector3(-353.0973f, -53.56806f, 49.03654f);
				this.Teller = new Vector3(-351.1114f, -52.1049f, 49.03651f);
				this.Vaultroom = new Vector3(-352.3316f, -59.28012f, 49.01485f);
				this.Customer = new Vector3(-354.718f, -46.6889f, 49.03635f);
				this.Customer2 = new Vector3(-348.0429f, -49.24176f, 49.03653f);
				this.Suspectloc = new Vector3(-355.8974f, -48.39071f, 49.03637f);
				this.Suspectloc2 = new Vector3(-353.7245f, -58.52271f, 49.01482f);
				this.Fleeca();
			}
			bool flag2 = this.s2;
			if (flag2)
			{
				this.FleecaDoorCheck = true;
				this.DoorInside = new Vector3(-2960.734f, 478.6947f, 15.69693f);
				this.VaultDoor = new Vector3(-2957.644f, 481.902f, 15.69704f);
				this.Teller = new Vector3(-2960.318f, 482.6946f, 15.69701f);
				this.Vaultroom = new Vector3(-2953.058f, 484.209f, 15.67538f);
				this.Customer = new Vector3(-2963.686f, 485.2892f, 15.69702f);
				this.Customer2 = new Vector3(-2964.288f, 477.9016f, 15.69691f);
				this.Suspectloc = new Vector3(-2961.062f, 477.5167f, 15.6969f);
				this.Suspectloc2 = new Vector3(-2953.866f, 483.0999f, 15.67534f);
				this.Fleeca2();
			}
			bool flag3 = this.s3;
			if (flag3)
			{
				this.FleecaDoorCheck = true;
				this.DoorInside = new Vector3(-1215.456f, -334.4157f, 37.78085f);
				this.VaultDoor = new Vector3(-1211.256f, -335.2424f, 37.78101f);
				this.Teller = new Vector3(-1211.573f, -332.6149f, 37.78094f);
				this.Vaultroom = new Vector3(-1206.964f, -338.2041f, 37.75932f);
				this.Customer = new Vector3(-1217.642f, -331.463f, 37.78082f);
				this.Customer2 = new Vector3(-1212.598f, -330.1842f, 37.78703f);
				this.Suspectloc = new Vector3(-1216.576f, -334.4876f, 37.78081f);
				this.Suspectloc2 = new Vector3(-1208.602f, -338.106f, 37.75929f);
				this.Fleeca3();
			}
			bool flag4 = this.s4;
			if (flag4)
			{
				this.FleecaDoorCheck = true;
				this.DoorInside = new Vector3(145.2671f, -1041.172f, 29.36792f);
				this.VaultDoor = new Vector3(147.4357f, -1044.95f, 29.36803f);
				this.Teller = new Vector3(148.8243f, -1042.663f, 29.36798f);
				this.Vaultroom = new Vector3(148.4249f, -1050.152f, 29.34636f);
				this.Customer = new Vector3(145.3892f, -1037.151f, 29.36796f);
				this.Customer2 = new Vector3(151.1434f, -1040.396f, 29.37411f);
				this.Suspectloc = new Vector3(144.7513f, -1039.991f, 29.36786f);
				this.Suspectloc2 = new Vector3(147.1309f, -1049.811f, 29.34633f);
				this.Fleeca4();
			}
			bool flag5 = this.s5;
			if (flag5)
			{
				this.FleecaDoorCheck = true;
				this.DoorInside = new Vector3(1179.249f, 2708.794f, 38.08786f);
				this.VaultDoor = new Vector3(1175.817f, 2711.346f, 38.088f);
				this.Teller = new Vector3(1175.419f, 2708.787f, 38.08794f);
				this.Vaultroom = new Vector3(1175.112f, 2715.057f, 38.06628f);
				this.Customer = new Vector3(1177.654f, 2706.062f, 38.09768f);
				this.Customer2 = new Vector3(1173.071f, 2705.281f, 38.08797f);
				this.Suspectloc = new Vector3(1180.001f, 2707.736f, 38.08786f);
				this.Suspectloc2 = new Vector3(1173.006f, 2716.389f, 38.06632f);
				this.Fleeca5();
			}
			bool flag6 = this.s6;
			if (flag6)
			{
				this.FleecaDoorCheck = true;
				this.DoorInside = new Vector3(309.6263f, -279.4955f, 54.16462f);
				this.VaultDoor = new Vector3(311.8565f, -283.0728f, 54.16475f);
				this.Teller = new Vector3(313.2072f, -280.8549f, 54.1647f);
				this.Vaultroom = new Vector3(311.4577f, -287.8997f, 54.14305f);
				this.Customer = new Vector3(311.9566f, -275.9158f, 54.16457f);
				this.Customer2 = new Vector3(315.2612f, -278.2772f, 54.16469f);
				this.Suspectloc = new Vector3(308.8417f, -278.549f, 54.16458f);
				this.Suspectloc2 = new Vector3(313.1132f, -288.9546f, 54.14307f);
				this.Fleeca6();
			}
			bool flag7 = this.s7;
			if (flag7)
			{
				this.PalDoorCheck = true;
				this.DoorInside = new Vector3(-109.0831f, 6468.447f, 31.62671f);
				this.Vaultroom = new Vector3(-105.541f, 6470.996f, 31.62672f);
				this.VaultDoor = new Vector3(-103.8834f, 6467.712f, 31.62671f);
				this.Teller = new Vector3(-112.3532f, 6465.133f, 31.62671f);
				this.Guardloc = new Vector3(-103.1572f, 6471.351f, 31.62672f);
				this.Customer = new Vector3(-114.2372f, 6465.376f, 31.62673f);
				this.Customer2 = new Vector3(-115.9376f, 6467.028f, 31.6267f);
				this.Customer3 = new Vector3(-117.3636f, 6468.647f, 31.6267f);
				this.Customer4 = new Vector3(-115.4691f, 6471.248f, 31.62671f);
				this.Customer5 = new Vector3(-110.9708f, 6466.909f, 31.62672f);
				this.Suspectloc = new Vector3(-108.0153f, 6466.022f, 31.62672f);
				this.Suspectloc2 = new Vector3(-113.6888f, 6469.539f, 31.62672f);
				this.Suspectloc3 = new Vector3(-118.8732f, 6462.964f, 31.46846f);
				this.Suspectloc4 = new Vector3(-107.13f, 6474.668f, 31.62672f);
				this.Escape = new Vector3(-137.5165f, 6434.614f, 31.46113f);
				this.Palbank();
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
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Bank Silent Alarm", "~b~Dispatch: ~w~All Units ~g~Code 4");
				GameFiber.Sleep(2000);
				this.End();
			}
			bool fleecaDoorCheck = this.FleecaDoorCheck;
			if (fleecaDoorCheck)
			{
				Extensions.DoorControll(73386408L, this.Calloutloc, false, 0f, 0f, 0f);
				Extensions.DoorControll((long)((ulong)-1152174184), this.Calloutloc, false, 0f, 0f, 0f);
			}
			bool palDoorCheck = this.PalDoorCheck;
			if (palDoorCheck)
			{
				Extensions.DoorControll((long)((ulong)-1666470363), this.Calloutloc, false, 0f, 0f, 0f);
				Extensions.DoorControll((long)((ulong)-353187150), this.Calloutloc, false, 0f, 0f, 0f);
			}
			bool isDead = Game.LocalPlayer.Character.IsDead;
			if (isDead)
			{
				this.End();
			}
			base.Process();
		}

		
		private void Fleeca()
		{
			this.Scenariorunning = true;
			this.HostageCuff();
			this.Hostage2Cuff();
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 150f;
						if (flag)
						{
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Suspectvehloc = new Vector3(-367.4673f, -29.03296f, 46.96129f);
									this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 342f);
									this.SuspectVehicle.RandomiseLicencePlate();
									this.SuspectVehicle.IsPersistent = true;
									this.SuspectVehicle.IsStolen = true;
									this.SuspectVehicle.IsEngineOn = true;
								}
							}
							else
							{
								this.Suspectvehloc = new Vector3(-335.0577f, -41.889f, 47.84258f);
								this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 342f);
								this.SuspectVehicle.RandomiseLicencePlate();
								this.SuspectVehicle.IsPersistent = true;
								this.SuspectVehicle.IsStolen = true;
								this.SuspectVehicle.IsEngineOn = true;
							}
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 30f;
						if (flag2)
						{
							Extensions.ClearAreaOfObject(this.DoorInside, 1f);
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							World.SpawnExplosion(this.Teller, 0, 0f, false, true, 0f);
							GameFiber.Wait(500);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							int num3 = new Random().Next(1, 3);
							int num4 = num3;
							if (num4 != 1)
							{
								if (num4 == 2)
								{
									this.Hostage = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Teller, 342f);
									this.Hostage2 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Vaultroom, 165f);
								}
							}
							else
							{
								this.Hostage = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Teller, 342f);
								this.Hostage2 = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Vaultroom, 165f);
							}
							this.Hostage.IsPersistent = true;
							this.Hostage.BlockPermanentEvents = true;
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							this.Hostage2.IsPersistent = true;
							this.Hostage2.BlockPermanentEvents = true;
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage, true);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage2, true);
							this.Custom = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer, 227f);
							this.Custom2 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer2, 86f);
							this.Custom.BlockPermanentEvents = true;
							this.Custom.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom2.BlockPermanentEvents = true;
							this.Custom2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_femalehostage", 6f, 1);
							this.Suspect = new Ped("g_m_m_chicold_01", this.Suspectloc, 308f);
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.SetVariation(2, 0, 0);
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect.Tasks.AimWeaponAt(this.Custom, -1);
							this.Suspect2 = new Ped("g_m_m_chicold_01", this.Suspectloc2, 306f);
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.SetVariation(2, 0, 0);
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Door = new Object(new Model("v_ilev_gb_teldr"), new Vector3(-355.381f, -51.0631f, 48.0158f));
							this.Door.Heading = 161.703f;
							this.Door.IsPositionFrozen = true;
							this.Bag = new Object(new Model("prop_cs_heist_bag_01"), new Vector3(-352.1399f, -58.48894f, 49.85935f));
							this.Bag.Heading = (float)new Random().Next(0, 359);
							this.Cash = new Object(new Model("prop_anim_cash_pile_01"), new Vector3(-351.7207f, -58.77878f, 49.01482f));
							this.Cash2 = new Object(new Model("prop_anim_cash_pile_02"), new Vector3(-351.9157f, -57.21142f, 49.85937f));
							this.Trolly = new Object(this.CashTrolly[new Random().Next(this.CashTrolly.Length)], new Vector3(-350.3012f, -58.36161f, 48.01487f));
							this.Trolly.SetRotationYaw(90f);
							new RelationshipGroup("BAD");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 5f;
						if (flag3)
						{
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							this.Hostage.IsCollisionEnabled = false;
							this.Hostage2.IsCollisionEnabled = false;
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Vault = new Object(new Model("v_ilev_gb_vauldr"), new Vector3(-352.5674f, -53.6453f, 48.03653f));
							this.Vault.IsPositionFrozen = true;
							this.Vault.SetRotationYaw(-180f);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag4 = this.Suspect2.IsAlive && Game.LocalPlayer.Character.DistanceTo(this.Suspect2.Position) < 6f;
						if (flag4)
						{
							this.Suspect2.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
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

		
		private void Fleeca2()
		{
			this.Scenariorunning = true;
			this.HostageCuff();
			this.Hostage2Cuff();
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 150f;
						if (flag)
						{
							this.Suspectvehloc = new Vector3(-2972.053f, 485.532f, 15.43102f);
							this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 354f);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = true;
							this.SuspectVehicle.IsEngineOn = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 30f;
						if (flag2)
						{
							Extensions.ClearAreaOfObject(this.DoorInside, 1f);
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							World.SpawnExplosion(this.Teller, 0, 0f, false, true, 0f);
							GameFiber.Wait(500);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Hostage = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Teller, 89f);
									this.Hostage2 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Vaultroom, 273f);
								}
							}
							else
							{
								this.Hostage = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Teller, 89f);
								this.Hostage2 = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Vaultroom, 273f);
							}
							this.Hostage.IsPersistent = true;
							this.Hostage.BlockPermanentEvents = true;
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							this.Hostage2.IsPersistent = true;
							this.Hostage2.BlockPermanentEvents = true;
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage, true);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage2, true);
							this.Custom2 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer2, 265f);
							this.Custom = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer, 149f);
							this.Custom.BlockPermanentEvents = true;
							this.Custom.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom2.BlockPermanentEvents = true;
							this.Custom2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_femalehostage", 6f, 1);
							this.Suspect = new Ped("g_m_m_chicold_01", this.Suspectloc, 54f);
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.SetVariation(2, 0, 0);
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect.Tasks.AimWeaponAt(this.Custom, -1);
							this.Suspect2 = new Ped("g_m_m_chicold_01", this.Suspectloc2, 54f);
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.SetVariation(2, 0, 0);
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Door = new Object(new Model("v_ilev_gb_teldr"), new Vector3(-2960.19f, 479.034f, 14.6603f));
							this.Door.Heading = 260.527f;
							this.Door.IsPositionFrozen = true;
							this.Bag = new Object(new Model("prop_cs_heist_bag_01"), new Vector3(-2954.102f, 484.2793f, 16.51986f));
							this.Bag.Heading = (float)new Random().Next(0, 359);
							this.Cash = new Object(new Model("prop_anim_cash_pile_01"), new Vector3(-2954.001f, 484.5922f, 16.51986f));
							this.Cash2 = new Object(new Model("prop_anim_cash_pile_02"), new Vector3(-2954.001f, 484.5922f, 16.51986f));
							this.Trolly = new Object(this.CashTrolly[new Random().Next(this.CashTrolly.Length)], new Vector3(-2953.22f, 485.9397f, 14.67542f));
							this.Trolly.SetRotationYaw(90f);
							new RelationshipGroup("BAD");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 5f;
						if (flag3)
						{
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							this.Hostage.IsCollisionEnabled = false;
							this.Hostage2.IsCollisionEnabled = false;
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Vault = new Object(new Model("v_ilev_gb_vauldr"), new Vector3(-2958.58f, 482.259f, 14.6245f));
							this.Vault.IsPositionFrozen = true;
							this.Vault.Heading = 284.827f;
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag4 = this.Suspect2.IsAlive && Game.LocalPlayer.Character.DistanceTo(this.Suspect2.Position) < 6f;
						if (flag4)
						{
							this.Suspect2.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
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

		
		private void Fleeca3()
		{
			this.Scenariorunning = true;
			this.HostageCuff();
			this.Hostage2Cuff();
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 150f;
						if (flag)
						{
							this.Suspectvehloc = new Vector3(-1195.051f, -318.7978f, 37.52604f);
							this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 204f);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = true;
							this.SuspectVehicle.IsEngineOn = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 30f;
						if (flag2)
						{
							Extensions.ClearAreaOfObject(this.DoorInside, 1f);
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							World.SpawnExplosion(this.Teller, 0, 0f, false, true, 0f);
							GameFiber.Wait(500);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Hostage = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Teller, 89f);
									this.Hostage2 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Vaultroom, 349f);
								}
							}
							else
							{
								this.Hostage = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Teller, 89f);
								this.Hostage2 = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Vaultroom, 349f);
							}
							this.Hostage.IsPersistent = true;
							this.Hostage.BlockPermanentEvents = true;
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							this.Hostage2.IsPersistent = true;
							this.Hostage2.BlockPermanentEvents = true;
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage, true);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage2, true);
							this.Custom = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer, 198f);
							this.Custom2 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer2, 124f);
							this.Custom.BlockPermanentEvents = true;
							this.Custom.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom2.BlockPermanentEvents = true;
							this.Custom2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_femalehostage", 6f, 1);
							this.Suspect = new Ped("g_m_m_chicold_01", this.Suspectloc, 54f);
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.SetVariation(2, 0, 0);
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect.Tasks.AimWeaponAt(this.Custom, -1);
							this.Suspect2 = new Ped("g_m_m_chicold_01", this.Suspectloc2, 54f);
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.SetVariation(2, 0, 0);
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Door = new Object(new Model("v_ilev_gb_teldr"), new Vector3(-1214.91f, -334.737f, 36.8208f));
							this.Door.Heading = 202.199f;
							this.Door.IsPositionFrozen = true;
							this.Bag = new Object(new Model("prop_cs_heist_bag_01"), new Vector3(-1207.458f, -337.5863f, 38.60381f));
							this.Bag.Heading = (float)new Random().Next(0, 359);
							this.Cash = new Object(new Model("prop_anim_cash_pile_01"), this.Suspectloc2);
							this.Cash2 = new Object(new Model("prop_anim_cash_pile_02"), this.Vaultroom);
							this.Trolly = new Object(this.CashTrolly[new Random().Next(this.CashTrolly.Length)], new Vector3(-1205.411f, -337.5149f, 36.75936f));
							this.Trolly.SetRotationYaw(90f);
							new RelationshipGroup("BAD");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 5f;
						if (flag3)
						{
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							this.Hostage.IsCollisionEnabled = false;
							this.Hostage2.IsCollisionEnabled = false;
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Vault = new Object(new Model("v_ilev_gb_vauldr"), new Vector3(-1211.26f, -334.567f, 36.801f));
							this.Vault.IsPositionFrozen = true;
							this.Vault.Heading = 215.06f;
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag4 = this.Suspect2.IsAlive && Game.LocalPlayer.Character.DistanceTo(this.Suspect2.Position) < 6f;
						if (flag4)
						{
							this.Suspect2.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
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

		
		private void Fleeca4()
		{
			this.Scenariorunning = true;
			this.HostageCuff();
			this.Hostage2Cuff();
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 150f;
						if (flag)
						{
							this.Suspectvehloc = new Vector3(158.0581f, -1038.223f, 29.21873f);
							this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 338f);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = true;
							this.SuspectVehicle.IsEngineOn = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 30f;
						if (flag2)
						{
							Extensions.ClearAreaOfObject(this.DoorInside, 1f);
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							World.SpawnExplosion(this.Teller, 0, 0f, false, true, 0f);
							GameFiber.Wait(500);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Hostage = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Teller, 171f);
									this.Hostage2 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Vaultroom, 349f);
								}
							}
							else
							{
								this.Hostage = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Teller, 171f);
								this.Hostage2 = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Vaultroom, 349f);
							}
							this.Hostage.IsPersistent = true;
							this.Hostage.BlockPermanentEvents = true;
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							this.Hostage2.IsPersistent = true;
							this.Hostage2.BlockPermanentEvents = true;
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage, true);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage2, true);
							this.Custom = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer, 156f);
							this.Custom2 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer2, 124f);
							this.Custom.BlockPermanentEvents = true;
							this.Custom.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom2.BlockPermanentEvents = true;
							this.Custom2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_femalehostage", 6f, 1);
							this.Suspect = new Ped("g_m_m_chicold_01", this.Suspectloc, 306f);
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.SetVariation(2, 0, 0);
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect.Tasks.AimWeaponAt(this.Custom, -1);
							this.Suspect2 = new Ped("g_m_m_chicold_01", this.Suspectloc2, 54f);
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.SetVariation(2, 0, 0);
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Door = new Object(new Model("v_ilev_gb_teldr"), new Vector3(145.407f, -1041.81f, 28.3372f));
							this.Door.Heading = 154.128f;
							this.Door.IsPositionFrozen = true;
							this.Bag = new Object(new Model("prop_cs_heist_bag_01"), new Vector3(148.3898f, -1048.992f, 30.19084f));
							this.Bag.Heading = (float)new Random().Next(0, 359);
							this.Cash = new Object(new Model("prop_anim_cash_pile_01"), new Vector3(147.2921f, -1048.681f, 29.34632f));
							this.Cash2 = new Object(new Model("prop_anim_cash_pile_02"), new Vector3(149.2878f, -1048.369f, 29.34636f));
							this.Trolly = new Object(this.CashTrolly[new Random().Next(this.CashTrolly.Length)], new Vector3(150.3138f, -1049.516f, 28.34639f));
							this.Trolly.SetRotationYaw(90f);
							new RelationshipGroup("BAD");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 5f;
						if (flag3)
						{
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							this.Hostage.IsCollisionEnabled = false;
							this.Hostage2.IsCollisionEnabled = false;
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Vault = new Object(new Model("v_ilev_gb_vauldr"), new Vector3(148.032f, -1044.35f, 28.3091f));
							this.Vault.IsPositionFrozen = true;
							this.Vault.Heading = 182.82f;
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag4 = this.Suspect2.IsAlive && Game.LocalPlayer.Character.DistanceTo(this.Suspect2.Position) < 6f;
						if (flag4)
						{
							this.Suspect2.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
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

		
		private void Fleeca5()
		{
			this.Scenariorunning = true;
			this.HostageCuff();
			this.Hostage2Cuff();
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 150f;
						if (flag)
						{
							this.Suspectvehloc = new Vector3(1172.951f, 2694.266f, 37.87449f);
							this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 121f);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = true;
							this.SuspectVehicle.IsEngineOn = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 30f;
						if (flag2)
						{
							Extensions.ClearAreaOfObject(this.DoorInside, 1f);
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							World.SpawnExplosion(this.Teller, 0, 0f, false, true, 0f);
							GameFiber.Wait(500);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Hostage = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Teller, 164f);
									this.Hostage2 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Vaultroom, 91f);
								}
							}
							else
							{
								this.Hostage = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Teller, 164f);
								this.Hostage2 = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Vaultroom, 91f);
							}
							this.Hostage.IsPersistent = true;
							this.Hostage.BlockPermanentEvents = true;
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							this.Hostage2.IsPersistent = true;
							this.Hostage2.BlockPermanentEvents = true;
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage, true);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage2, true);
							this.Custom = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer, 255f);
							this.Custom2 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer2, 286f);
							this.Custom.BlockPermanentEvents = true;
							this.Custom.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom2.BlockPermanentEvents = true;
							this.Custom2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_femalehostage", 6f, 1);
							this.Suspect = new Ped("g_m_m_chicold_01", this.Suspectloc, 130f);
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.SetVariation(2, 0, 0);
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect.Tasks.AimWeaponAt(this.Custom, -1);
							this.Suspect2 = new Ped("g_m_m_chicold_01", this.Suspectloc2, 54f);
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.SetVariation(2, 0, 0);
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Door = new Object(new Model("v_ilev_gb_teldr"), new Vector3(1178.87f, 2709.35f, 37.1049f));
							this.Door.Heading = 356.891f;
							this.Door.IsPositionFrozen = true;
							this.Bag = new Object(new Model("prop_cs_heist_bag_01"), new Vector3(1173.7f, 2715.102f, 38.91189f));
							this.Bag.Heading = (float)new Random().Next(0, 359);
							this.Cash = new Object(new Model("prop_anim_cash_pile_01"), new Vector3(1173.769f, 2715.959f, 38.06634f));
							this.Cash2 = new Object(new Model("prop_anim_cash_pile_02"), new Vector3(1172.645f, 2715.999f, 38.06634f));
							this.Trolly = new Object(this.CashTrolly[new Random().Next(this.CashTrolly.Length)], new Vector3(1171.648f, 2716.025f, 37.06635f));
							this.Trolly.SetRotationYaw(90f);
							new RelationshipGroup("BAD");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 5f;
						if (flag3)
						{
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							this.Hostage.IsCollisionEnabled = false;
							this.Hostage2.IsCollisionEnabled = false;
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Vault = new Object(new Model("v_ilev_gb_vauldr"), new Vector3(1175.48f, 2710.88f, 37.098f));
							this.Vault.IsPositionFrozen = true;
							this.Vault.Heading = 19.4117f;
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag4 = this.Suspect2.IsAlive && Game.LocalPlayer.Character.DistanceTo(this.Suspect2.Position) < 6f;
						if (flag4)
						{
							this.Suspect2.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
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

		
		private void Fleeca6()
		{
			this.Scenariorunning = true;
			this.HostageCuff();
			this.Hostage2Cuff();
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 150f;
						if (flag)
						{
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Suspectvehloc = new Vector3(297.6069f, -272.2538f, 53.97948f);
									this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 338f);
									this.SuspectVehicle.RandomiseLicencePlate();
									this.SuspectVehicle.IsPersistent = true;
									this.SuspectVehicle.IsStolen = true;
									this.SuspectVehicle.IsEngineOn = true;
								}
							}
							else
							{
								this.Suspectvehloc = new Vector3(321.6688f, -273.5828f, 53.908f);
								this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 263f);
								this.SuspectVehicle.RandomiseLicencePlate();
								this.SuspectVehicle.IsPersistent = true;
								this.SuspectVehicle.IsStolen = true;
								this.SuspectVehicle.IsEngineOn = true;
							}
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 30f;
						if (flag2)
						{
							Extensions.ClearAreaOfObject(this.DoorInside, 1f);
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							World.SpawnExplosion(this.Teller, 0, 0f, false, true, 0f);
							GameFiber.Wait(500);
							Extensions.ClearAreaOfPeds(this.Teller, 4f);
							int num3 = new Random().Next(1, 3);
							int num4 = num3;
							if (num4 != 1)
							{
								if (num4 == 2)
								{
									this.Hostage = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Teller, 164f);
									this.Hostage2 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Vaultroom, 91f);
								}
							}
							else
							{
								this.Hostage = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Teller, 164f);
								this.Hostage2 = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Vaultroom, 91f);
							}
							this.Hostage.IsPersistent = true;
							this.Hostage.BlockPermanentEvents = true;
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							this.Hostage2.IsPersistent = true;
							this.Hostage2.BlockPermanentEvents = true;
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage, true);
							Functions.SetPedCantBeArrestedByPlayer(this.Hostage2, true);
							this.Custom = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer, 79f);
							this.Custom2 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer2, 73f);
							this.Custom.BlockPermanentEvents = true;
							this.Custom.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom2.BlockPermanentEvents = true;
							this.Custom2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_femalehostage", 6f, 1);
							this.Suspect = new Ped("g_m_m_chicold_01", this.Suspectloc, 130f);
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.SetVariation(2, 0, 0);
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect.Tasks.AimWeaponAt(this.Custom, -1);
							this.Suspect2 = new Ped("g_m_m_chicold_01", this.Suspectloc2, 54f);
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.SetVariation(2, 0, 0);
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Door = new Object(new Model("v_ilev_gb_teldr"), new Vector3(309.726f, -280.16f, 53.1436f));
							this.Door.Heading = 158.563f;
							this.Door.IsPositionFrozen = true;
							this.Bag = new Object(new Model("prop_cs_heist_bag_01"), new Vector3(312.7608f, -287.4007f, 54.98846f));
							this.Bag.Heading = (float)new Random().Next(0, 359);
							this.Cash = new Object(new Model("prop_anim_cash_pile_01"), new Vector3(312.6461f, -286.3261f, 54.14305f));
							this.Cash2 = new Object(new Model("prop_anim_cash_pile_02"), new Vector3(312.6461f, -286.3261f, 54.14305f));
							this.Trolly = new Object(this.CashTrolly[new Random().Next(this.CashTrolly.Length)], new Vector3(314.4081f, -288.619f, 53.1431f));
							this.Trolly.SetRotationYaw(90f);
							new RelationshipGroup("BAD");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 5f;
						if (flag3)
						{
							Extensions.ClearAreaOfObject(this.VaultDoor, 1f);
							this.Hostage.IsCollisionEnabled = false;
							this.Hostage2.IsCollisionEnabled = false;
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Vault = new Object(new Model("v_ilev_gb_vauldr"), new Vector3(312.443f, -282.803f, 53.1047f));
							this.Vault.IsPositionFrozen = true;
							this.Vault.Heading = 177.05f;
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag4 = this.Suspect2.IsAlive && Game.LocalPlayer.Character.DistanceTo(this.Suspect2.Position) < 6f;
						if (flag4)
						{
							this.Suspect2.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
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

		
		private void Palbank()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 150f;
						if (flag)
						{
							this.Suspectvehloc = new Vector3(-124.278f, 6463.401f, 31.42694f);
							this.SuspectVehicle = new Vehicle(this.SVehicle[new Random().Next(this.SVehicle.Length)], this.Suspectvehloc, 86f);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = true;
							World.SpawnExplosion(this.Teller, 0, 0f, false, true, 0f);
							this.Bag2 = new Object(new Model("prop_cs_heist_bag_01"), new Vector3(-120.7824f, 6462.981f, 31.46846f));
							this.Bag2.Heading = (float)new Random().Next(0, 329);
							this.SuspectVehicle.Doors[2].IsFullyOpen = true;
							this.SuspectVehicle.Doors[3].IsFullyOpen = true;
							this.SuspectVehicle.IsEngineOn = true;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 100f;
						if (flag2)
						{
							World.SpawnExplosion(this.Teller, 0, 0f, false, true, 0f);
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Hostage = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Teller, 7f);
									this.Hostage2 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Vaultroom, 234f);
									this.Hostage3 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.VaultDoor, 354f);
								}
							}
							else
							{
								this.Hostage = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.Teller, 7f);
								this.Hostage2 = new Ped(this.Fleecaboy[new Random().Next(this.Fleecaboy.Length)], this.Vaultroom, 234f);
								this.Hostage3 = new Ped(this.Fleecagirl[new Random().Next(this.Fleecagirl.Length)], this.VaultDoor, 354f);
							}
							this.Hostage.IsPersistent = true;
							this.Hostage.BlockPermanentEvents = true;
							this.Hostage.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Hostage2.IsPersistent = true;
							this.Hostage2.BlockPermanentEvents = true;
							this.Hostage2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Hostage3.IsPersistent = true;
							this.Hostage3.BlockPermanentEvents = true;
							this.Hostage3.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer, 322f);
							this.Custom2 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer2, 309f);
							this.Custom3 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer3, 267f);
							this.Custom4 = new Ped(this.Customlist[new Random().Next(this.Customlist.Length)], this.Customer4, 204f);
							this.Custom5 = new Ped("s_m_m_security_01", this.Customer5, 359f);
							this.Custom.IsPersistent = true;
							this.Custom2.IsPersistent = true;
							this.Custom3.IsPersistent = true;
							this.Custom4.IsPersistent = true;
							this.Custom5.IsPersistent = true;
							this.Custom.BlockPermanentEvents = true;
							this.Custom.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom2.BlockPermanentEvents = true;
							this.Custom2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom3.BlockPermanentEvents = true;
							this.Custom3.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
							this.Custom4.BlockPermanentEvents = true;
							this.Custom4.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_femalehostage", 6f, 1);
							this.Custom5.BlockPermanentEvents = true;
							this.Custom5.MovementAnimationSet = new AnimationSet?("move_m@injured");
							this.Custom5.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 49);
							this.Custom5.Inventory.Weapons.Clear();
							this.Suspect = new Ped("mp_m_bogdangoon", this.Suspectloc, 130f);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							if (Bank.<>o__96.<>p__1 == null)
							{
								Bank.<>o__96.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(Bank)));
							}
							Func<CallSite, object, int> target = Bank.<>o__96.<>p__1.Target;
							CallSite <>p__ = Bank.<>o__96.<>p__1;
							if (Bank.<>o__96.<>p__0 == null)
							{
								Bank.<>o__96.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
								{
									typeof(int)
								}, typeof(Bank), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							int BoneIndex = target(<>p__, Bank.<>o__96.<>p__0.Target(Bank.<>o__96.<>p__0, NativeFunction.Natives, this.Suspect, 24817));
							if (Bank.<>o__96.<>p__2 == null)
							{
								Bank.<>o__96.<>p__2 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(Bank), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Bank.<>o__96.<>p__2.Target(Bank.<>o__96.<>p__2, NativeFunction.Natives, this.Bag3, this.Suspect, BoneIndex, 0.0500002f, -0.0500002f, 0.0500002f, -189.481f, 100.29f, -16.65f, true, true, false, false, 2, 1);
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2 = new Ped("mp_m_bogdangoon", this.Suspectloc2, 145f);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3 = new Ped("mp_m_bogdangoon", this.Suspectloc3, 162f);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4 = new Ped("mp_m_bogdangoon", this.Suspectloc4, 220f);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							if (Bank.<>o__96.<>p__4 == null)
							{
								Bank.<>o__96.<>p__4 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(Bank)));
							}
							Func<CallSite, object, int> target2 = Bank.<>o__96.<>p__4.Target;
							CallSite <>p__2 = Bank.<>o__96.<>p__4;
							if (Bank.<>o__96.<>p__3 == null)
							{
								Bank.<>o__96.<>p__3 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
								{
									typeof(int)
								}, typeof(Bank), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							int BoneIndex2 = target2(<>p__2, Bank.<>o__96.<>p__3.Target(Bank.<>o__96.<>p__3, NativeFunction.Natives, this.Suspect4, 24817));
							if (Bank.<>o__96.<>p__5 == null)
							{
								Bank.<>o__96.<>p__5 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(Bank), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Bank.<>o__96.<>p__5.Target(Bank.<>o__96.<>p__5, NativeFunction.Natives, this.Bag4, this.Suspect4, BoneIndex2, 0.0500002f, -0.0500002f, 0.0500002f, -189.481f, 100.29f, -16.65f, true, true, false, false, 2, 1);
							this.Bag = new Object(new Model("prop_cs_heist_bag_01"), new Vector3(-107.8031f, 6467.277f, 31.62672f));
							this.Bag.Heading = (float)new Random().Next(0, 359);
							new RelationshipGroup("BAD");
							new RelationshipGroup("C1");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Hostage.RelationshipGroup = "C1";
							this.Hostage2.RelationshipGroup = "C1";
							this.Hostage3.RelationshipGroup = "C1";
							this.Custom.RelationshipGroup = "C1";
							this.Custom2.RelationshipGroup = "C1";
							this.Custom3.RelationshipGroup = "C1";
							this.Custom4.RelationshipGroup = "C1";
							this.Custom5.RelationshipGroup = "C1";
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect.Accuracy = 55;
							this.Suspect2.Accuracy = 55;
							this.Suspect3.Accuracy = 55;
							this.Suspect4.Accuracy = 55;
							this.Suspect.Tasks.AimWeaponAt(this.Custom5, -1);
							this.Shooting();
							this.Doorwatch();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 50f;
						if (flag3)
						{
							this.Suspect3.Tasks.FollowNavigationMeshToPosition(this.Guardloc, 1f, 2f);
							GameFiber.Sleep(2500);
							this.Custom5.MaxHealth = 200;
							this.Custom5.Health = 130;
							this.Custom5.Tasks.FollowNavigationMeshToPosition(this.Calloutloc, 10f, 1f).WaitForCompletion();
							this.Custom5.Tasks.GoToWhileAiming(this.Escape, this.Calloutloc, 1f, 1f, false, 1566631136);
							GameFiber.Sleep(4000);
							this.Suspect.Tasks.Clear();
							this.Custom5.PlayAmbientSpeech("S_M_M_CIASEC_01_WHITE_MINI_01", "GENERIC_FRIGHTENED_MED", 1, 6);
							this.Suspect.Tasks.FireWeaponAt(this.Custom5, -1, -957453492);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool isDead = this.Custom5.IsDead;
						if (isDead)
						{
							this.Suspect.Tasks.FollowNavigationMeshToPosition(this.Suspectmov, 10f, 2f);
							this.Suspect3.Tasks.AimWeaponAt(this.Hostage3, -1);
							break;
						}
					}
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						this.Hostage3.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 49);
						this.Hostage3.Tasks.FollowNavigationMeshToPosition(this.Hostage3mov, 132.0066f, 1f).WaitForCompletion();
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool isOnFoot = Game.LocalPlayer.Character.IsOnFoot;
						if (isOnFoot)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To hear what the person outside the entrance is shouting", Settings.Interact), false);
							bool flag4 = Game.IsKeyDown(Settings.Interact);
							if (flag4)
							{
								Game.DisplaySubtitle("~y~Hostage: ~w~OFFICER COME HERE FAST!, I HAVE TO TALK TO YOU!");
								this._Blip.Delete();
								this._Blip = this.Hostage3.AttachBlip();
								this._Blip.Color = Color.Yellow;
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag5 = Game.LocalPlayer.Character.DistanceTo(this.Hostage3) < 3f;
						if (flag5)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To listen what hostage have to say", Settings.Interact), false);
							bool flag6 = Game.IsKeyDown(Settings.Interact);
							if (flag6)
							{
								Game.DisplaySubtitle("~y~Hostage: ~w~They tell you have to get out of here or they will start shooting!");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag7 = Game.LocalPlayer.Character.DistanceTo(this.Hostage3) < 3f;
						if (flag7)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To continue talk", Settings.Interact), false);
							bool flag8 = Game.IsKeyDown(Settings.Interact);
							if (flag8)
							{
								Game.DisplaySubtitle("~y~Hostage: ~w~The guard was just a warning of what they could do with the rest of us....");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag9 = Game.LocalPlayer.Character.DistanceTo(this.Hostage3) < 3f;
						if (flag9)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To continue talk", Settings.Interact), false);
							bool flag10 = Game.IsKeyDown(Settings.Interact);
							if (flag10)
							{
								Game.DisplaySubtitle("~y~Hostage: ~w~They told me to go inside after i told you this, but i am so scared, Should i run away?");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag11 = Game.LocalPlayer.Character.DistanceTo(this.Hostage3) < 3f;
						if (flag11)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To continue talk", Settings.Interact), false);
							bool flag12 = Game.IsKeyDown(Settings.Interact);
							if (flag12)
							{
								Game.DisplaySubtitle("~b~Player: ~w~Keep calm we will come up with a solution...");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag13 = Game.LocalPlayer.Character.DistanceTo(this.Hostage3) < 3f;
						if (flag13)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To continue talk", Settings.Interact), false);
							bool flag14 = Game.IsKeyDown(Settings.Interact);
							if (flag14)
							{
								Game.DisplaySubtitle("~b~Player: ~w~You know how many suspect´s there is inside?");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag15 = Game.LocalPlayer.Character.DistanceTo(this.Hostage3) < 3f;
						if (flag15)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To continue talk", Settings.Interact), false);
							bool flag16 = Game.IsKeyDown(Settings.Interact);
							if (flag16)
							{
								Game.DisplaySubtitle("~y~Hostage: ~w~There is four of them!");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag17 = Game.LocalPlayer.Character.DistanceTo(this.Hostage3) < 3f;
						if (flag17)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To continue talk", Settings.Interact), false);
							bool flag18 = Game.IsKeyDown(Settings.Interact);
							if (flag18)
							{
								break;
							}
						}
					}
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						Game.DisplaySubtitle("~y~Hostage: ~w~Leave the area now and fast please!, i need to get inside before they start shooting!");
						GameFiber.Sleep(1000);
						this.Timer();
						this._Blip.Delete();
						this._Blip = new Blip(this._Searcharea, 120f);
						this._Blip.Color = Color.Red;
						this._Blip.Alpha = 0.5f;
						this.Hostage3.Tasks.FollowNavigationMeshToPosition(this.VaultDoor, 354f, 1f).WaitForCompletion();
						this.Hostage3.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		private void Shooting()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (!this.PlayerShooting)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.IsShooting && Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 50f;
						if (flag)
						{
							this.Scenariorunning = false;
							GameFiber.Wait(500);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "C1", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVFEMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "SECURITY_GUARD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PRIVATE_SECURITY", 5);
							this.Hostage.BlockPermanentEvents = false;
							this.Hostage2.BlockPermanentEvents = false;
							this.Hostage3.BlockPermanentEvents = false;
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Custom3.BlockPermanentEvents = false;
							this.Custom4.BlockPermanentEvents = false;
							this.Custom5.BlockPermanentEvents = false;
							this.Custom5.Tasks.Clear();
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(100f, -1);
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

		
		private void HostageCuff()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = EntityExtensions.Exists(this.Hostage);
						if (flag)
						{
							bool flag2 = this.Hostage.IsAlive && this.HostageCuffed && Game.LocalPlayer.Character.DistanceTo(this.Hostage.Position) < 2f;
							if (flag2)
							{
								Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to release hostage", Settings.Interact), false);
								bool flag3 = Game.IsKeyDown(Settings.Interact);
								if (flag3)
								{
									Game.LocalPlayer.Character.Tasks.PlayAnimation("amb@medic@standing@tendtodead@enter", "enter", 6f, 0).WaitForCompletion();
									this.HostageCuffed = false;
									this.Hostage.IsCollisionEnabled = true;
									this.Hostage.Tasks.Clear();
									break;
								}
							}
							bool isDead = this.Hostage.IsDead;
							if (isDead)
							{
								this.HostageCuffed = false;
								this.Hostage.IsCollisionEnabled = true;
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

		
		private void Hostage2Cuff()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = EntityExtensions.Exists(this.Hostage2);
						if (flag)
						{
							bool flag2 = this.Hostage2.IsAlive && this.Hostage2Cuffed && Game.LocalPlayer.Character.DistanceTo(this.Hostage2.Position) < 2f;
							if (flag2)
							{
								Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to release hostage", Settings.Interact), false);
								bool flag3 = Game.IsKeyDown(Settings.Interact);
								if (flag3)
								{
									Game.LocalPlayer.Character.Tasks.PlayAnimation("amb@medic@standing@tendtodead@enter", "enter", 6f, 0).WaitForCompletion();
									this.Hostage2Cuffed = false;
									this.Hostage2.IsCollisionEnabled = true;
									this.Hostage2.Tasks.Clear();
									break;
								}
							}
							bool isDead = this.Hostage2.IsDead;
							if (isDead)
							{
								this.Hostage2Cuffed = false;
								this.Hostage2.IsCollisionEnabled = true;
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

		
		private void Timer()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						Game.DisplayHelp("~w~Get outside the ~r~AREA ~w~fast if you dont want to jeopardize the lives of the hostages", false);
						int num = new Random().Next(1, 3);
						int num2 = num;
						if (num2 != 1)
						{
							if (num2 == 2)
							{
								GameFiber.Sleep(25000);
							}
						}
						else
						{
							GameFiber.Sleep(20000);
						}
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 120f;
						if (flag)
						{
							GameFiber.Wait(500);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "C1", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVFEMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "SECURITY_GUARD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PRIVATE_SECURITY", 5);
							this.Hostage.BlockPermanentEvents = false;
							this.Hostage2.BlockPermanentEvents = false;
							this.Hostage3.BlockPermanentEvents = false;
							this.Hostage3.Tasks.Clear();
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Custom3.BlockPermanentEvents = false;
							this.Custom4.BlockPermanentEvents = false;
							this.Custom5.BlockPermanentEvents = false;
							this.Custom5.Tasks.Clear();
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Scenariorunning = false;
							this.PlayerShooting = true;
							GameFiber.Sleep(1000);
							Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS");
							Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Bank Silent Alarm", "~b~Dispatch: ~w~Cameras inside the bank shows a blood bath!");
						}
						else
						{
							this.Bag.Delete();
							this.Bag2.Delete();
							if (Bank.<>o__100.<>p__1 == null)
							{
								Bank.<>o__100.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(Bank)));
							}
							Func<CallSite, object, int> target = Bank.<>o__100.<>p__1.Target;
							CallSite <>p__ = Bank.<>o__100.<>p__1;
							if (Bank.<>o__100.<>p__0 == null)
							{
								Bank.<>o__100.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
								{
									typeof(int)
								}, typeof(Bank), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							int BoneIndex3 = target(<>p__, Bank.<>o__100.<>p__0.Target(Bank.<>o__100.<>p__0, NativeFunction.Natives, this.Suspect2, 24817));
							if (Bank.<>o__100.<>p__2 == null)
							{
								Bank.<>o__100.<>p__2 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(Bank), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Bank.<>o__100.<>p__2.Target(Bank.<>o__100.<>p__2, NativeFunction.Natives, this.Bag4, this.Suspect2, BoneIndex3, 0.0500002f, -0.0500002f, 0.0500002f, -189.481f, 100.29f, -16.65f, true, true, false, false, 2, 1);
							if (Bank.<>o__100.<>p__4 == null)
							{
								Bank.<>o__100.<>p__4 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(Bank)));
							}
							Func<CallSite, object, int> target2 = Bank.<>o__100.<>p__4.Target;
							CallSite <>p__2 = Bank.<>o__100.<>p__4;
							if (Bank.<>o__100.<>p__3 == null)
							{
								Bank.<>o__100.<>p__3 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
								{
									typeof(int)
								}, typeof(Bank), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							int BoneIndex4 = target2(<>p__2, Bank.<>o__100.<>p__3.Target(Bank.<>o__100.<>p__3, NativeFunction.Natives, this.Suspect3, 24817));
							if (Bank.<>o__100.<>p__5 == null)
							{
								Bank.<>o__100.<>p__5 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(Bank), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Bank.<>o__100.<>p__5.Target(Bank.<>o__100.<>p__5, NativeFunction.Natives, this.Bag5, this.Suspect3, BoneIndex4, 0.0500002f, -0.0500002f, 0.0500002f, -189.481f, 100.29f, -16.65f, true, true, false, false, 2, 1);
							Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_SUSPECTS_FLEEING");
							Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Bank Silent Alarm", "~b~Dispatch: ~w~Cameras at the bank is showing the ~r~Suspect´s ~w~is fleeing the scene!");
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Bankflee();
							GameFiber.Wait(500);
							this.PlayerShooting = true;
							this.Doorguard = false;
						}
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		private void Doorwatch()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Doorguard)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 5f;
						if (flag)
						{
							GameFiber.Wait(500);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "C1", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVFEMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "SECURITY_GUARD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PRIVATE_SECURITY", 5);
							this.Hostage.BlockPermanentEvents = false;
							this.Hostage2.BlockPermanentEvents = false;
							this.Hostage3.BlockPermanentEvents = false;
							this.Hostage3.Tasks.Clear();
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Custom3.BlockPermanentEvents = false;
							this.Custom4.BlockPermanentEvents = false;
							this.Custom5.BlockPermanentEvents = false;
							this.Custom5.Tasks.Clear();
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(100f, -1);
							this.Scenariorunning = false;
							this.PlayerShooting = true;
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

		
		private void Bankflee()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) < 40f;
						if (flag)
						{
							this.Hostage.BlockPermanentEvents = false;
							this.Hostage2.BlockPermanentEvents = false;
							this.Hostage3.BlockPermanentEvents = false;
							this.Hostage3.Tasks.Clear();
							this.Custom.BlockPermanentEvents = false;
							this.Custom2.BlockPermanentEvents = false;
							this.Custom3.BlockPermanentEvents = false;
							this.Custom4.BlockPermanentEvents = false;
							this.Custom5.BlockPermanentEvents = false;
							this.Hostage.Tasks.Flee(this.DoorInside, 100f, -1);
							this.Hostage2.Tasks.Flee(this.DoorInside, 100f, -1);
							this.Hostage3.Tasks.Flee(this.DoorInside, 100f, -1);
							this.Custom.Tasks.Flee(this.DoorInside, 100f, -1);
							this.Custom2.Tasks.Flee(this.DoorInside, 100f, -1);
							this.Custom3.Tasks.Flee(this.DoorInside, 100f, -1);
							this.Custom4.Tasks.Flee(this.DoorInside, 100f, -1);
							this.Custom5.Tasks.Flee(this.DoorInside, 100f, -1);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							GameFiber.Wait(1000);
							this.Pursuit = Functions.CreatePursuit();
							Functions.AddPedToPursuit(this.Pursuit, this.Suspect);
							Functions.AddPedToPursuit(this.Pursuit, this.Suspect4);
							Functions.AddPedToPursuit(this.Pursuit, this.Suspect2);
							Functions.AddPedToPursuit(this.Pursuit, this.Suspect3);
							Functions.SetPursuitIsActiveForPlayer(this.Pursuit, true);
							Functions.SetPursuitDisableAIForPed(this.Suspect4, true);
							Functions.SetPursuitDisableAIForPed(this.Suspect2, true);
							Functions.SetPursuitDisableAIForPed(this.Suspect3, true);
							this._Blip.Delete();
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "C1", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "CIVFEMALE", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "SECURITY_GUARD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PRIVATE_SECURITY", 5);
							GameFiber.Sleep(4000);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(80f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(80f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(80f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = this.Suspect.IsStopped || this.Suspect.IsDead;
						if (flag2)
						{
							Functions.SetPursuitDisableAIForPed(this.Suspect, true);
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(80f);
							this.Suspect.BlockPermanentEvents = false;
							this.Suspect4.BlockPermanentEvents = false;
							this.Suspect2.BlockPermanentEvents = false;
							this.Suspect3.BlockPermanentEvents = false;
							this.Suspect.Tasks.LeaveVehicle(256);
							this.Suspect4.Tasks.LeaveVehicle(256);
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
			this.FleecaDoorCheck = false;
			this.PalDoorCheck = false;
			this.PlayerShooting = false;
			this.Doorguard = false;
			Game.LogTrivial("ManiacCallouts - Bank Silent Alarm Cleaned.");
			bool flag = EntityExtensions.Exists(this.Vault);
			if (flag)
			{
				this.Vault.Dismiss();
			}
			bool flag2 = EntityExtensions.Exists(this.Door);
			if (flag2)
			{
				this.Door.Dismiss();
			}
			bool flag3 = EntityExtensions.Exists(this.Bag);
			if (flag3)
			{
				this.Bag.Dismiss();
			}
			bool flag4 = EntityExtensions.Exists(this.Bag2);
			if (flag4)
			{
				this.Bag2.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.Bag3);
			if (flag5)
			{
				this.Bag3.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this.Bag4);
			if (flag6)
			{
				this.Bag4.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Bag5);
			if (flag7)
			{
				this.Bag5.Dismiss();
			}
			bool flag8 = EntityExtensions.Exists(this.Bag6);
			if (flag8)
			{
				this.Bag6.Dismiss();
			}
			bool flag9 = EntityExtensions.Exists(this.Cash);
			if (flag9)
			{
				this.Cash.Dismiss();
			}
			bool flag10 = EntityExtensions.Exists(this.Cash2);
			if (flag10)
			{
				this.Cash2.Dismiss();
			}
			bool flag11 = EntityExtensions.Exists(this.Trolly);
			if (flag11)
			{
				this.Trolly.Dismiss();
			}
			bool flag12 = EntityExtensions.Exists(this.Hostage);
			if (flag12)
			{
				this.Hostage.Dismiss();
			}
			bool flag13 = EntityExtensions.Exists(this.Hostage2);
			if (flag13)
			{
				this.Hostage2.Dismiss();
			}
			bool flag14 = EntityExtensions.Exists(this.Hostage3);
			if (flag14)
			{
				this.Hostage3.Tasks.Clear();
				this.Hostage3.Dismiss();
			}
			bool flag15 = EntityExtensions.Exists(this.Custom);
			if (flag15)
			{
				this.Custom.Dismiss();
			}
			bool flag16 = EntityExtensions.Exists(this.Custom2);
			if (flag16)
			{
				this.Custom2.Dismiss();
			}
			bool flag17 = EntityExtensions.Exists(this.Custom3);
			if (flag17)
			{
				this.Custom3.Dismiss();
			}
			bool flag18 = EntityExtensions.Exists(this.Custom4);
			if (flag18)
			{
				this.Custom4.Dismiss();
			}
			bool flag19 = EntityExtensions.Exists(this.Custom5);
			if (flag19)
			{
				this.Custom5.Dismiss();
			}
			bool flag20 = EntityExtensions.Exists(this.Suspect);
			if (flag20)
			{
				this.Suspect.Dismiss();
			}
			bool flag21 = EntityExtensions.Exists(this.Suspect2);
			if (flag21)
			{
				this.Suspect2.Dismiss();
			}
			bool flag22 = EntityExtensions.Exists(this.Suspect3);
			if (flag22)
			{
				this.Suspect3.Dismiss();
			}
			bool flag23 = EntityExtensions.Exists(this.Suspect4);
			if (flag23)
			{
				this.Suspect4.Dismiss();
			}
			bool flag24 = EntityExtensions.Exists(this._Blip);
			if (flag24)
			{
				this._Blip.Delete();
			}
			bool flag25 = EntityExtensions.Exists(this.SuspectVehicle);
			if (flag25)
			{
				this.SuspectVehicle.Dismiss();
			}
			base.End();
		}

		
		private Ped Cop;

		
		private Ped Cop2;

		
		private Ped Guard;

		
		private Ped Suspect;

		
		private Ped Suspect2;

		
		private Ped Suspect3;

		
		private Ped Suspect4;

		
		private Ped Custom;

		
		private Ped Custom2;

		
		private Ped Custom3;

		
		private Ped Custom4;

		
		private Ped Custom5;

		
		private Ped Hostage;

		
		private Ped Hostage2;

		
		private Ped Hostage3;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private Vehicle SuspectVehicle;

		
		private Vehicle Playerveh;

		
		private Vehicle Police;

		
		private Object Door;

		
		private Object Vault;

		
		private Object Bag;

		
		private Object Bag2;

		
		private Object Cash;

		
		private Object Cash2;

		
		private Object Trolly;

		
		private readonly Object Bag3 = new Object(new Model("p_ld_heist_bag_s"), new Vector3(0f, 0f, 0f));

		
		private readonly Object Bag4 = new Object(new Model("p_ld_heist_bag_s"), new Vector3(0f, 0f, 0f));

		
		private readonly Object Bag5 = new Object(new Model("p_ld_heist_bag_s"), new Vector3(0f, 0f, 0f));

		
		private readonly Object Bag6 = new Object(new Model("p_ld_heist_bag_s"), new Vector3(0f, 0f, 0f));

		
		private const string Hostagefloor = "missprologueig_2";

		
		private const string Hostagefloor1 = "idle_on_floor_malehostage01";

		
		private const string Hostagefloor2 = "idle_on_floor_gaurd";

		
		private const string Hostagefloor3 = "idle_on_floor_femalehostage";

		
		private const string Arrest = "random@arrests";

		
		private const string Arrest1 = "idle_2_hands_up";

		
		private const string Arrest2 = "kneeling_arrest_idle";

		
		private const string Rescue = "amb@medic@standing@tendtodead@enter";

		
		private const string Rescue2 = "enter";

		
		private Vector3 _Searcharea;

		
		private Vector3 Calloutloc;

		
		private Vector3 DoorInside;

		
		private Vector3 VaultDoor;

		
		private Vector3 Teller;

		
		private Vector3 Vaultroom;

		
		private Vector3 Customer;

		
		private Vector3 Customer2;

		
		private Vector3 Customer3;

		
		private Vector3 Customer4;

		
		private Vector3 Customer5;

		
		private Vector3 Suspectvehloc;

		
		private Vector3 Suspectloc;

		
		private Vector3 Suspectloc2;

		
		private Vector3 Suspectloc3;

		
		private Vector3 Suspectloc4;

		
		private Vector3 Guardloc;

		
		private Vector3 Escape;

		
		private Vector3 Hostage3mov = new Vector3(-116.4736f, 6457.695f, 31.46038f);

		
		private Vector3 Suspectmov = new Vector3(-111.4535f, 6467.529f, 31.62672f);

		
		private Vector3 Dump = new Vector3(757.0891f, 5721.721f, 691.8749f);

		
		private string[] CashTrolly = new string[]
		{
			"hei_prop_hei_cash_trolly_01",
			"hei_prop_hei_cash_trolly_03",
			"hei_prop_hei_cash_trolly_02"
		};

		
		private string[] Weaponlist = new string[]
		{
			"weapon_microsmg",
			"weapon_combatpistol",
			"weapon_sawnoffshotgun",
			"weapon_gusenberg",
			"weapon_pumpshotgun"
		};

		
		private string[] Weaponlist2 = new string[]
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

		
		private string[] Fleecagirl = new string[]
		{
			"a_f_y_business_02",
			"a_f_y_business_04",
			"a_f_y_business_03"
		};

		
		private string[] Fleecaboy = new string[]
		{
			"a_m_y_business_02",
			"a_m_y_business_03"
		};

		
		private string[] Customlist = new string[]
		{
			"a_f_y_bevhills_01",
			"a_m_m_genfat_01",
			"a_f_y_bevhills_03",
			"a_m_o_genstreet_01",
			"a_f_m_tourist_01",
			"a_m_y_genstreet_01",
			"a_f_m_fatbla_01"
		};

		
		private bool PursuitCreated = false;

		
		private bool Scenariorunning = false;

		
		private bool PlayerShooting = false;

		
		private bool FleecaDoorCheck = false;

		
		private bool PalDoorCheck = false;

		
		private bool HostageCuffed = true;

		
		private bool Hostage2Cuffed = true;

		
		private bool Suspect2fight = false;

		
		private bool Doorguard = true;

		
		private bool s1 = false;

		
		private bool s2 = false;

		
		private bool s3 = false;

		
		private bool s4 = false;

		
		private bool s5 = false;

		
		private bool s6 = false;

		
		private bool s7 = false;

		
		private int Headingveh;
	}
}
