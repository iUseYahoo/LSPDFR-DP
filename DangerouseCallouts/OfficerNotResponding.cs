using System;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace Dangerouse_Callouts_Not_deleted.Callouts
{
	
	[CalloutInfo("Officer not Responding", 1)]
	internal class OfficerNotResponding : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(300f));
			this.Suspect = new Ped(this.Spawnpoint);
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Officer Not Responding";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("CRIME_ASSAULT_PEACE_OFFICER_03 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.Cop.Kill();
			this.Suspect.Armor = 5;
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_SMG", 70, true);
			this.Suspect.IsPersistent = true;
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			this.Suspect.IsPersistent = true;
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			base.Process();
		}

		
		public override void End()
		{
			base.End();
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
			Game.LogTrivial("Dangerouse Callouts: Officer Not Responding has been cleaned up!");
		}

		
		private Ped Suspect;

		
		private Ped Cop;

		
		private Vehicle SuspectVehicle;

		
		private Blip SuspectBlip;

		
		private LHandle Pursuit;

		
		private Vector3 Spawnpoint;

		
		private bool PursuitCreated;
	}
}
