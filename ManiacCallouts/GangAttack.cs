using System;
using System.Drawing;
using System.Threading;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Gang Attack", 3)]
	public class Gangattack : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 6))
				{
				case 1:
					this.Calloutloc = new Vector3(104.3926f, -1940.203f, 20.80372f);
					this.Ballas = true;
					this.Lost = false;
					this.Mex = false;
					this.Vagos = false;
					this.Korea = false;
					break;
				case 2:
					this.Calloutloc = new Vector3(969.1058f, -126.3266f, 74.35925f);
					this.Ballas = false;
					this.Lost = true;
					this.Mex = false;
					this.Vagos = false;
					this.Korea = false;
					break;
				case 3:
					this.Calloutloc = new Vector3(1189.675f, -1642.45f, 41.93192f);
					this.Ballas = false;
					this.Lost = false;
					this.Mex = true;
					this.Vagos = false;
					this.Korea = false;
					break;
				case 4:
					this.Calloutloc = new Vector3(285.3461f, -2063.285f, 17.75205f);
					this.Ballas = false;
					this.Lost = false;
					this.Mex = false;
					this.Vagos = true;
					this.Korea = false;
					break;
				case 5:
					this.Calloutloc = new Vector3(-740.8088f, -918.3542f, 19.40154f);
					this.Ballas = false;
					this.Lost = false;
					this.Mex = false;
					this.Vagos = false;
					this.Korea = true;
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
			base.CalloutMessage = "Gang Conflict Shots Fired";
			base.CalloutPosition = this.Calloutloc;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Calloutloc);
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Gang Conflict Shots Fired", "~b~Dispatch: ~w~Follow The ~y~GPS ~w~To The Location. ~w~Respond with ~g~Code 3");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_CRIME_SHOTS_FIRED_03 MC_RESPOND_CODE3");
			this._Searcharea = this.Calloutloc.Around2D(1f, 2f);
			this._Blip = new Blip(this._Searcharea, 40f);
			this._Blip.EnableRoute(Color.Yellow);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			bool ballas = this.Ballas;
			if (ballas)
			{
				switch (new Random().Next(1, 6))
				{
				case 1:
					this.LostvsBall();
					break;
				case 2:
					this.FamvsBall();
					break;
				case 3:
					this.VagosvsBall();
					break;
				case 4:
					this.MexvsBall();
					break;
				case 5:
					this.KoreavsBall();
					break;
				}
			}
			bool lost = this.Lost;
			if (lost)
			{
				switch (new Random().Next(1, 6))
				{
				case 1:
					this.BallasvsLost();
					break;
				case 2:
					this.FamvsLost();
					break;
				case 3:
					this.VagosvsLost();
					break;
				case 4:
					this.MexvsLost();
					break;
				case 5:
					this.KoreavsLost();
					break;
				}
			}
			bool mex = this.Mex;
			if (mex)
			{
				int num = new Random().Next(1, 3);
				int num2 = num;
				if (num2 != 1)
				{
					if (num2 == 2)
					{
						this.Ballasvsmex();
					}
				}
				else
				{
					this.Lostvsmex();
				}
			}
			bool vagos = this.Vagos;
			if (vagos)
			{
				int num3 = new Random().Next(1, 3);
				int num4 = num3;
				if (num4 != 1)
				{
					if (num4 == 2)
					{
						this.Famvsvagos();
					}
				}
				else
				{
					this.Ballasvsvagos();
				}
			}
			bool korea = this.Korea;
			if (korea)
			{
				switch (new Random().Next(1, 4))
				{
				case 1:
					this.Famvskorea();
					break;
				case 2:
					this.Ballasvskorea();
					break;
				case 3:
					this.Lostvskorea();
					break;
				}
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
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Gang Conflict Shots Fired", "~b~Dispatch: ~w~All Units ~g~Code 4");
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

		
		private void LostvsBall()
		{
			this.Spawnpoint = new Vector3(97.53739f, -1943.622f, 20.77503f);
			this.Headingveh = 274;
			this.Spawnpoint2 = new Vector3(96.34492f, -1929.869f, 20.78288f);
			this.Headingveh2 = 48;
			this.Spawnpoint3 = new Vector3(81.80682f, -1922.674f, 20.85914f);
			this.Headingveh3 = 266;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle("GBurrito", this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle("GBurrito", this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_lost_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped("g_f_y_lost_01", this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_BALLAS", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_BALLAS", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_BALLAS", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
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

		
		private void FamvsBall()
		{
			this.Spawnpoint = new Vector3(87.5397f, -1930.995f, 20.68272f);
			this.Headingveh = 227;
			this.Spawnpoint2 = new Vector3(93.21321f, -1925.634f, 20.71011f);
			this.Headingveh2 = 220;
			this.Spawnpoint3 = new Vector3(109.9618f, -1942.449f, 20.8037f);
			this.Headingveh3 = 45;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_families_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped("g_f_y_families_01", this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_BALLAS", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_BALLAS", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_BALLAS", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void VagosvsBall()
		{
			this.Spawnpoint = new Vector3(82.70754f, -1929.58f, 20.73397f);
			this.Headingveh = 173;
			this.Spawnpoint2 = new Vector3(81.49931f, -1911.467f, 21.16723f);
			this.Headingveh2 = 37;
			this.Spawnpoint3 = new Vector3(114.8843f, -1948.268f, 20.61246f);
			this.Headingveh3 = 241;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_vagos_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped("g_f_y_vagos_01", this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped("g_f_y_vagos_01", this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_BALLAS", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_BALLAS", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_BALLAS", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void MexvsBall()
		{
			this.Spawnpoint = new Vector3(82.70754f, -1929.58f, 20.73397f);
			this.Headingveh = 173;
			this.Spawnpoint2 = new Vector3(96.34492f, -1929.869f, 20.78288f);
			this.Headingveh2 = 48;
			this.Spawnpoint3 = new Vector3(114.8843f, -1948.268f, 20.61246f);
			this.Headingveh3 = 241;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_BALLAS", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_BALLAS", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_BALLAS", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void KoreavsBall()
		{
			this.Spawnpoint = new Vector3(83.28689f, -1923.275f, 20.28173f);
			this.Headingveh = 258;
			this.Spawnpoint2 = new Vector3(75.24734f, -1920.866f, 20.44209f);
			this.Headingveh2 = 228;
			this.Spawnpoint3 = new Vector3(108.1407f, -1930.867f, 20.71495f);
			this.Headingveh3 = 70;
			this.Spawnpoint4 = new Vector3(83.67403f, -1915.23f, 20.34595f);
			this.Headingveh4 = 219;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.Couplist[new Random().Next(this.Couplist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Couplist[new Random().Next(this.Couplist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.SuspectVehicle3 = new Vehicle(this.Couplist[new Random().Next(this.Couplist.Length)], this.Spawnpoint4, (float)this.Headingveh4);
							this.SuspectVehicle3.RandomiseLicencePlate();
							this.SuspectVehicle3.IsPersistent = true;
							this.SuspectVehicle3.IsStolen = false;
							this.GangVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect4 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle2, 0);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect5 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle3, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle3, 256);
							this.Suspect6 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle3, 0);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle3, 256);
							this.Gang1 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_BALLAS", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_BALLAS", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_BALLAS", "BAD2", 0);
							this.Suspect.Accuracy = 60;
							this.Suspect2.Accuracy = 60;
							this.Suspect3.Accuracy = 60;
							this.Suspect4.Accuracy = 60;
							this.Suspect5.Accuracy = 60;
							this.Suspect6.Accuracy = 60;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void BallasvsLost()
		{
			this.Spawnpoint = new Vector3(958.9371f, -133.4358f, 74.40031f);
			this.Headingveh = 12;
			this.Spawnpoint2 = new Vector3(966.775f, -136.6352f, 74.383f);
			this.Headingveh2 = 298;
			this.Spawnpoint3 = new Vector3(977.083f, -141.4218f, 74.22484f);
			this.Headingveh3 = 54;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle("GBurrito", this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_ballas_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped("g_f_y_ballas_01", this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_LOST", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_LOST", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_LOST", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void FamvsLost()
		{
			this.Spawnpoint = new Vector3(969.4575f, -127.0424f, 74.36564f);
			this.Headingveh = 325;
			this.Spawnpoint2 = new Vector3(965.1363f, -135.6344f, 74.41077f);
			this.Headingveh2 = 325;
			this.Spawnpoint3 = new Vector3(977.083f, -141.4218f, 74.22484f);
			this.Headingveh3 = 54;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle("GBurrito", this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = (this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f));
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = (this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f));
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = (this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f));
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = (this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f));
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = (this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f));
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = (this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f));
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_LOST", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_LOST", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_LOST", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void VagosvsLost()
		{
			this.Spawnpoint = new Vector3(969.4575f, -127.0424f, 74.36564f);
			this.Headingveh = 325;
			this.Spawnpoint2 = new Vector3(965.1363f, -135.6344f, 74.41077f);
			this.Headingveh2 = 325;
			this.Spawnpoint3 = new Vector3(977.083f, -141.4218f, 74.22484f);
			this.Headingveh3 = 54;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle("GBurrito", this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped("g_f_y_vagos_01", this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped("g_f_y_vagos_01", this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_LOST", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_LOST", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_LOST", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void MexvsLost()
		{
			this.Spawnpoint = new Vector3(959.1569f, -131.0683f, 74.39478f);
			this.Headingveh = 27;
			this.Spawnpoint2 = new Vector3(966.7035f, -132.4177f, 74.3892f);
			this.Headingveh2 = 318;
			this.Spawnpoint3 = new Vector3(977.083f, -141.4218f, 74.22484f);
			this.Headingveh3 = 54;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle("GBurrito", this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_LOST", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_LOST", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_LOST", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void KoreavsLost()
		{
			this.Spawnpoint = new Vector3(962.4713f, -137.1457f, 74.43183f);
			this.Headingveh = 322;
			this.Spawnpoint3 = new Vector3(977.083f, -141.4218f, 74.22484f);
			this.Headingveh3 = 54;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.GangVehicle = new Vehicle("GBurrito", this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Gang1 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_LOST", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_LOST", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_LOST", "BAD2", 0);
							this.Suspect.Accuracy = 80;
							this.Suspect2.Accuracy = 80;
							this.Suspect3.Accuracy = 80;
							this.Suspect4.Accuracy = 80;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void Lostvsmex()
		{
			this.Spawnpoint = new Vector3(1173.91f, -1672.716f, 36.73808f);
			this.Headingveh = 257;
			this.Spawnpoint2 = new Vector3(1167.676f, -1662.519f, 36.72211f);
			this.Headingveh2 = 320;
			this.Spawnpoint3 = new Vector3(1160.718f, -1646.545f, 36.91955f);
			this.Headingveh3 = 205;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle("GBurrito", this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle("GBurrito", this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped("g_f_y_lost_01", this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped("g_f_y_lost_01", this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_SALVA", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_SALVA", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_SALVA", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void Ballasvsmex()
		{
			this.Spawnpoint = new Vector3(1173.91f, -1672.716f, 36.73808f);
			this.Headingveh = 257;
			this.Spawnpoint2 = new Vector3(1167.676f, -1662.519f, 36.72211f);
			this.Headingveh2 = 320;
			this.Spawnpoint3 = new Vector3(1160.718f, -1646.545f, 36.91955f);
			this.Headingveh3 = 205;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Ballaslist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Ballaslist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Ballaslist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_ballas_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped("g_f_y_ballas_01", this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Ballaslist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped(this.Ballaslist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Mexlist[new Random().Next(this.Mexlist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_SALVA", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_SALVA", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_SALVA", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void Ballasvsvagos()
		{
			this.Spawnpoint = new Vector3(262.2857f, -2085.645f, 16.90363f);
			this.Headingveh = 44;
			this.Spawnpoint2 = new Vector3(261.4137f, -2060.564f, 17.3912f);
			this.Headingveh2 = 248;
			this.Spawnpoint3 = new Vector3(280.9963f, -2081.872f, 16.7516f);
			this.Headingveh3 = 291;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_ballas_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped("g_f_y_ballas_01", this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_MEXICAN", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_MEXICAN", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_MEXICAN", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void Famvsvagos()
		{
			this.Spawnpoint = new Vector3(262.2857f, -2085.645f, 16.90363f);
			this.Headingveh = 44;
			this.Spawnpoint2 = new Vector3(261.4137f, -2060.564f, 17.3912f);
			this.Headingveh2 = 248;
			this.Spawnpoint3 = new Vector3(280.9963f, -2081.872f, 16.7516f);
			this.Headingveh3 = 291;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_families_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped("g_f_y_families_01", this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang3 = new Ped(this.Vaglist[new Random().Next(this.Vaglist.Length)], this.Spawnpoint, 0f);
							this.Gang3.WarpIntoVehicle(this.GangVehicle, 1);
							this.Gang3.IsPersistent = true;
							this.Gang3.BlockPermanentEvents = true;
							this.Gang3.Inventory.GiveNewWeapon("weapon_pistol", 5000, true);
							this.Gang3.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							this.Gang3.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_MEXICAN", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_MEXICAN", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_MEXICAN", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 25;
							this.Gang2.Accuracy = 25;
							this.Gang3.Accuracy = 25;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Gang3.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang3.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void Famvskorea()
		{
			this.Spawnpoint = new Vector3(-727.3762f, -945.7358f, 18.66842f);
			this.Headingveh = 39;
			this.Spawnpoint2 = new Vector3(-729.4245f, -920.0265f, 19.01401f);
			this.Headingveh2 = 268;
			this.Spawnpoint3 = new Vector3(-727.8033f, -934.8575f, 18.35891f);
			this.Headingveh3 = 178;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.Sedanlist[new Random().Next(this.Sedanlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.Couplist[new Random().Next(this.Couplist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_families_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped("g_f_y_families_01", this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped(this.Famlist[new Random().Next(this.Famlist.Length)], this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_WEICHENG", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_WEICHENG", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_WEICHENG", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 65;
							this.Gang2.Accuracy = 65;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void Ballasvskorea()
		{
			this.Spawnpoint = new Vector3(-720.008f, -938.4343f, 19.01702f);
			this.Headingveh = 355;
			this.Spawnpoint2 = new Vector3(-734.468f, -922.0637f, 19.13625f);
			this.Headingveh2 = 78;
			this.Spawnpoint3 = new Vector3(-727.8033f, -934.8575f, 18.35891f);
			this.Headingveh3 = 178;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.SuspectVehicle2 = new Vehicle(this.SUVlist[new Random().Next(this.SUVlist.Length)], this.Spawnpoint2, (float)this.Headingveh2);
							this.SuspectVehicle2.RandomiseLicencePlate();
							this.SuspectVehicle2.IsPersistent = true;
							this.SuspectVehicle2.IsStolen = false;
							this.GangVehicle = new Vehicle(this.Couplist[new Random().Next(this.Couplist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_ballas_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect5 = new Ped("g_f_y_ballas_01", this.Spawnpoint, 0f);
							this.Suspect5.WarpIntoVehicle(this.SuspectVehicle2, -1);
							this.Suspect5.IsPersistent = true;
							this.Suspect5.BlockPermanentEvents = true;
							this.Suspect5.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect5.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect6 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect6.WarpIntoVehicle(this.SuspectVehicle2, 1);
							this.Suspect6.IsPersistent = true;
							this.Suspect6.BlockPermanentEvents = true;
							this.Suspect6.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect6.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Suspect7 = new Ped(this.Ballaslist[new Random().Next(this.Ballaslist.Length)], this.Spawnpoint, 0f);
							this.Suspect7.WarpIntoVehicle(this.SuspectVehicle2, 2);
							this.Suspect7.IsPersistent = true;
							this.Suspect7.BlockPermanentEvents = true;
							this.Suspect7.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 5000, true);
							this.Suspect7.Tasks.LeaveVehicle(this.SuspectVehicle2, 256);
							this.Gang1 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Suspect5.RelationshipGroup = "BAD";
							this.Suspect6.RelationshipGroup = "BAD";
							this.Suspect7.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_WEICHENG", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_WEICHENG", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_WEICHENG", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Suspect5.Accuracy = 30;
							this.Suspect6.Accuracy = 30;
							this.Suspect7.Accuracy = 30;
							this.Gang1.Accuracy = 65;
							this.Gang2.Accuracy = 65;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint2) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Suspect5.KeepTasks = true;
							this.Suspect6.KeepTasks = true;
							this.Suspect7.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect5.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect6.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect7.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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

		
		private void Lostvskorea()
		{
			this.Spawnpoint = new Vector3(-720.008f, -938.4343f, 19.01702f);
			this.Headingveh = 355;
			this.Spawnpoint3 = new Vector3(-727.8033f, -934.8575f, 18.35891f);
			this.Headingveh3 = 178;
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.SuspectVehicle = new Vehicle("GBurrito", this.Spawnpoint, (float)this.Headingveh);
							this.SuspectVehicle.RandomiseLicencePlate();
							this.SuspectVehicle.IsPersistent = true;
							this.SuspectVehicle.IsStolen = false;
							this.GangVehicle = new Vehicle(this.Couplist[new Random().Next(this.Couplist.Length)], this.Spawnpoint3, (float)this.Headingveh3);
							this.GangVehicle.RandomiseLicencePlate();
							this.GangVehicle.IsPersistent = true;
							this.GangVehicle.IsStolen = false;
							this.Suspect = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
							this.Suspect.IsPersistent = true;
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect2 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect2.WarpIntoVehicle(this.SuspectVehicle, 0);
							this.Suspect2.IsPersistent = true;
							this.Suspect2.BlockPermanentEvents = true;
							this.Suspect2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Suspect2.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect3 = new Ped(this.Lostboylist[new Random().Next(this.Lostboylist.Length)], this.Spawnpoint, 0f);
							this.Suspect3.WarpIntoVehicle(this.SuspectVehicle, 1);
							this.Suspect3.IsPersistent = true;
							this.Suspect3.BlockPermanentEvents = true;
							this.Suspect3.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect3.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Suspect4 = new Ped("g_f_y_lost_01", this.Spawnpoint, 0f);
							this.Suspect4.WarpIntoVehicle(this.SuspectVehicle, 2);
							this.Suspect4.IsPersistent = true;
							this.Suspect4.BlockPermanentEvents = true;
							this.Suspect4.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 5000, true);
							this.Suspect4.Tasks.LeaveVehicle(this.SuspectVehicle, 256);
							this.Gang1 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Gang1.WarpIntoVehicle(this.GangVehicle, -1);
							this.Gang1.IsPersistent = true;
							this.Gang1.BlockPermanentEvents = true;
							this.Gang1.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Gang1.Tasks.LeaveVehicle(this.GangVehicle, 256);
							this.Gang2 = new Ped(this.Korlist[new Random().Next(this.Korlist.Length)], this.Spawnpoint, 0f);
							this.Gang2.WarpIntoVehicle(this.GangVehicle, 0);
							this.Gang2.IsPersistent = true;
							this.Gang2.BlockPermanentEvents = true;
							this.Gang2.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist3[new Random().Next(this.Weaponlist3.Length)]), 5000, true);
							this.Gang2.Tasks.LeaveVehicle(this.GangVehicle, 256);
							new RelationshipGroup("BAD");
							new RelationshipGroup("BAD2");
							this.Suspect.RelationshipGroup = "BAD";
							this.Suspect2.RelationshipGroup = "BAD";
							this.Suspect3.RelationshipGroup = "BAD";
							this.Suspect4.RelationshipGroup = "BAD";
							this.Gang1.RelationshipGroup = "BAD2";
							this.Gang2.RelationshipGroup = "BAD2";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "BAD2", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "BAD", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD2", "AMBIENT_GANG_WEICHENG", 0);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "AMBIENT_GANG_WEICHENG", 5);
							Game.SetRelationshipBetweenRelationshipGroups("AMBIENT_GANG_WEICHENG", "BAD2", 0);
							this.Suspect.Accuracy = 30;
							this.Suspect2.Accuracy = 30;
							this.Suspect3.Accuracy = 30;
							this.Suspect4.Accuracy = 30;
							this.Gang1.Accuracy = 65;
							this.Gang2.Accuracy = 65;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint) <= 90f;
						if (flag)
						{
							this.Suspect.KeepTasks = true;
							this.Suspect2.KeepTasks = true;
							this.Suspect3.KeepTasks = true;
							this.Suspect4.KeepTasks = true;
							this.Gang1.KeepTasks = true;
							this.Gang2.KeepTasks = true;
							this.Attack = true;
							this.Suspect.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect2.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect3.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Suspect4.Tasks.FightAgainstClosestHatedTarget(400f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool attack = this.Attack;
						if (attack)
						{
							this.Gang1.Tasks.FightAgainstClosestHatedTarget(400f);
							this.Gang2.Tasks.FightAgainstClosestHatedTarget(400f);
							GameFiber.Wait(1000);
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
			Game.LogTrivial("ManiacCallouts - Gang Attack Cleaned.");
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
			bool flag4 = EntityExtensions.Exists(this.Suspect4);
			if (flag4)
			{
				this.Suspect4.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.Suspect5);
			if (flag5)
			{
				this.Suspect5.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this.Suspect6);
			if (flag6)
			{
				this.Suspect6.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Suspect7);
			if (flag7)
			{
				this.Suspect7.Dismiss();
			}
			bool flag8 = EntityExtensions.Exists(this.Gang1);
			if (flag8)
			{
				this.Gang1.Dismiss();
			}
			bool flag9 = EntityExtensions.Exists(this.Gang2);
			if (flag9)
			{
				this.Gang2.Dismiss();
			}
			bool flag10 = EntityExtensions.Exists(this.Gang3);
			if (flag10)
			{
				this.Gang3.Dismiss();
			}
			bool flag11 = EntityExtensions.Exists(this.SuspectVehicle);
			if (flag11)
			{
				this.SuspectVehicle.Dismiss();
			}
			bool flag12 = EntityExtensions.Exists(this.SuspectVehicle2);
			if (flag12)
			{
				this.SuspectVehicle2.Dismiss();
			}
			bool flag13 = EntityExtensions.Exists(this.SuspectVehicle3);
			if (flag13)
			{
				this.SuspectVehicle3.Dismiss();
			}
			bool flag14 = EntityExtensions.Exists(this.GangVehicle);
			if (flag14)
			{
				this.GangVehicle.Dismiss();
			}
			bool flag15 = EntityExtensions.Exists(this._Blip);
			if (flag15)
			{
				this._Blip.Delete();
			}
			base.End();
		}

		
		private Ped Suspect;

		
		private Ped Suspect2;

		
		private Ped Suspect3;

		
		private Ped Suspect4;

		
		private Ped Suspect5;

		
		private Ped Suspect6;

		
		private Ped Suspect7;

		
		private Ped Gang1;

		
		private Ped Gang2;

		
		private Ped Gang3;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private Vehicle SuspectVehicle;

		
		private Vehicle SuspectVehicle2;

		
		private Vehicle SuspectVehicle3;

		
		private Vehicle GangVehicle;

		
		private Vector3 Calloutloc;

		
		private Vector3 Spawnpoint;

		
		private Vector3 Spawnpoint2;

		
		private Vector3 Spawnpoint3;

		
		private Vector3 Spawnpoint4;

		
		private Vector3 _Searcharea;

		
		private Vector3 Dump = new Vector3(757.0891f, 5721.721f, 691.8749f);

		
		private string[] Weaponlist = new string[]
		{
			"weapon_microsmg",
			"weapon_heavypistol",
			"weapon_pumpshotgun",
			"weapon_sawnoffshotgun",
			"weapon_revolver",
			"weapon_minismg"
		};

		
		private string[] Weaponlist2 = new string[]
		{
			"weapon_microsmg",
			"weapon_sawnoffshotgun",
			"weapon_minismg"
		};

		
		private string[] Weaponlist3 = new string[]
		{
			"weapon_assaultrifle",
			"weapon_pumpshotgun",
			"weapon_carbinerifle",
			"weapon_smg",
			"weapon_specialcarbine"
		};

		
		private string[] SUVlist = new string[]
		{
			"BJXL",
			"Baller",
			"Baller2",
			"Granger"
		};

		
		private string[] Sedanlist = new string[]
		{
			"Ingot",
			"Primo",
			"Emperor2",
			"Asea",
			"Glendale"
		};

		
		private string[] Couplist = new string[]
		{
			"CogCabrio",
			"Jackal",
			"Felon",
			"Zion",
			" Oracle",
			"Sentinel"
		};

		
		private string[] Lostboylist = new string[]
		{
			"g_m_y_lost_01",
			"g_m_y_lost_02",
			"g_m_y_lost_03"
		};

		
		private string[] Ballaslist = new string[]
		{
			"g_m_y_ballaeast_01",
			"g_m_y_ballaorig_01",
			"g_m_y_ballasout_01"
		};

		
		private string[] Famlist = new string[]
		{
			"g_m_y_famdnf_01",
			"g_m_y_famca_01",
			"g_m_y_famfor_01"
		};

		
		private string[] Vaglist = new string[]
		{
			"g_m_y_mexgoon_02",
			"g_m_y_mexgoon_01",
			"g_m_y_mexgoon_03"
		};

		
		private string[] Mexlist = new string[]
		{
			"g_m_y_salvagoon_01",
			"g_m_y_salvagoon_02",
			"g_m_y_salvagoon_03"
		};

		
		private string[] Korlist = new string[]
		{
			"g_m_y_korean_01",
			"g_m_y_korlieut_01",
			"g_m_y_korean_02"
		};

		
		private bool PursuitCreated = false;

		
		private bool Attack = false;

		
		private bool Scenariorunning = false;

		
		private bool Ballas = false;

		
		private bool Lost = false;

		
		private bool Mex = false;

		
		private bool Vagos = false;

		
		private bool Korea = false;

		
		private int Headingveh;

		
		private int Headingveh2;

		
		private int Headingveh3;

		
		private int Headingveh4;

		
		private int counter = 0;
	}
}
