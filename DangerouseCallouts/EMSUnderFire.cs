using System;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("EMS Under Fire", 1)]
	internal class EMSUnderFire : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.heading = 66.64632f;
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(300f));
			this.Suspect = new Ped(this.Spawnpoint, 0f);
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Shots Fired by civilian at EMS Personel";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.EMS1 = new Ped("s_m_m_paramedic_01", this.Spawnpoint, this.heading);
			this.EMS2 = new Ped("s_m_m_paramedic_01", this.Spawnpoint, this.heading);
			this.EMS = new Vehicle("AMBULANCE", this.Spawnpoint);
			this.EMS1.WarpIntoVehicle(this.EMS, -1);
			this.EMS2.WarpIntoVehicle(this.EMS, -2);
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 50, true);
			GameFiber.Sleep(3000);
			this.Suspect.Tasks.FightAgainst(this.EMS1);
			this.Suspect.Tasks.FightAgainst(this.EMS2);
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", 50, true);
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Red;
			this.SuspectBlip.IsRouteEnabled = true;
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			base.Process();
			bool flag = Game.LocalPlayer.Character.IsDead || this.Suspect.IsCuffed || this.Suspect.IsDead;
			if (flag)
			{
				this.End();
			}
		}

		
		public override void End()
		{
			base.End();
			bool isDead = this.Suspect.IsDead;
			if (isDead)
			{
				this.Suspect.Dismiss();
			}
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
			Game.LogTrivial("Dangerous Callouts: EMSUnderFire has been cleaned up!");
		}

		
		private Ped Suspect;

		
		private Ped EMS1;

		
		private Ped EMS2;

		
		private Vehicle EMS;

		
		private float heading;

		
		private Blip SuspectBlip;

		
		private Vector3 Spawnpoint;
	}
}
