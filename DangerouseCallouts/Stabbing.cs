using System;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("Person with a knife", 1)]
	internal class Stabbing : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(300f));
			this.Suspect = new Ped(this.Spawnpoint);
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Person with a knife Reported";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT_03 WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_Knife", 100, true);
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			this.SuspectBlip.IsFriendly = false;
			Functions.RequestBackup(this.Spawnpoint, 1, 0);
			GameFiber.Sleep(3000);
			Functions.RequestBackup(this.Spawnpoint, 1, 0);
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			bool flag = Game.LocalPlayer.Character.IsDead || this.Suspect.IsDead || this.Suspect.IsCuffed;
			if (flag)
			{
				this.End();
			}
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
			Game.LogTrivial("Dangerouse Callouts: Stabbing has been cleaned up!");
		}

		
		private Vector3 Spawnpoint;

		
		private Blip SuspectBlip;

		
		private Ped Suspect;
	}
}
